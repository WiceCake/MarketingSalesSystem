Imports DevExpress.XtraGrid
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid.Views.Grid

Public Class ucBuyer
    Inherits ucBase

    Public grid As GridControl

    Sub New(ByVal title As String)
        InitializeComponent()

        MyBase.title = title
        LabelControl1.Text = title

        loadData()

        AddHandler grid.Load, AddressOf gridLoaded

        dtFrom.EditValue = Date.Now.AddDays(-15)
        dtFrom.Properties.MaxValue = Date.Now

        dtTo.EditValue = Date.Now
        dtTo.Properties.MaxValue = Date.Now.AddDays(1)

    End Sub

    Sub loadData()
        grid = New GridControl() With {
            .Dock = DockStyle.Fill
        }

        LayoutControl2.Controls.Add(grid)

        Dim layoutItem As LayoutControlItem = LayoutControl2.AddItem("", grid)
        layoutItem.TextVisible = False
    End Sub

    Sub gridLoaded(sender As Object, e As EventArgs)
        loadGrid()
    End Sub

    Sub loadGrid()

        Dim dc As New mkdbDataContext
        Dim tsp As New tpmdbDataContext

        ' Preload data only once, avoid calling .ToList() multiple times
        Dim buyerList = New SalesInvoiceBuyer(dc).getByDate(CDate(dtFrom.EditValue), CDate(dtTo.EditValue)).ToList()

        Dim buyerNames = tsp.ML_SULLIER_20170329s.
            ToDictionary(Function(s) s.ml_SupID, Function(s) s.ml_Supplier)

        ' Preprocess actual kilos
        Dim buyerKilosDict = dc.trans_SalesReportBuyers.
            AsEnumerable().
            GroupBy(Function(s) s.salesInvoiceBuyerID).
            ToDictionary(
                Function(g) g.Key,
                Function(g) g.Select(Function(s) multiplyFields(s)).Sum()
            )

        ' Preprocess spoilage kilos
        Dim spoilageDict = dc.trans_SalesReportCatchers.
            AsEnumerable().
            GroupBy(Function(s) s.catchActivityDetail_ID).
            SelectMany(Function(g) g.Skip(1)).
            GroupBy(Function(s) s.salesReport_ID).
            ToDictionary(
                Function(g) g.Key,
                Function(g) g.Select(Function(s) multiplyFields(s)).Sum()
            )

        ' Preload sales report dictionary
        Dim transReportsDict = dc.trans_SalesReports.
            ToDictionary(Function(r) r.salesReport_ID)

        ' Final projection with max speed
        Dim buyers = From bl In buyerList
                     Let invoiceId = bl.salesInvoiceID
                     Let sr = If(transReportsDict.TryGetValue(invoiceId, Nothing), transReportsDict(invoiceId), Nothing)
                     Where sr IsNot Nothing AndAlso buyerKilosDict.ContainsKey(bl.salesInvoiceBuyerID)
                     Let actualKilo = buyerKilosDict(bl.salesInvoiceBuyerID)
                     Let spoilageKilo = If(spoilageDict.ContainsKey(invoiceId), spoilageDict(invoiceId), 0D)
                     Let rb = actualKilo - spoilageKilo
                     Let orb = rb - bl.adjustmentsAmount
                     Let buyerName = If(Integer.TryParse(bl.buyerName, 0) AndAlso buyerNames.ContainsKey(CInt(bl.buyerName)),
                                        buyerNames(CInt(bl.buyerName)),
                                        bl.buyerName)
                     Select New With {
                         .SalesInvoiceBuyerID = bl.salesInvoiceBuyerID,
                         .DateCreated = bl.encodedOn,
                         .InvoiceNo = sr.invoiceNum & "-" & bl.setNum,
                         .Buyer = buyerName,
                         .AmountPaid = bl.paidAmount,
                         .RemainingBalance = Math.Round(CDec(orb) - CDec(bl.paidAmount), 2),
                         .Adjustments = bl.adjustmentsAmount,
                         .PaidInPercentage = If(rb > 0, Math.Round((CDec(bl.paidAmount) / rb) * 100, 2) & "%", "0%")
                     }

        Dim gridView = New GridView()
        AddHandler gridView.DoubleClick, AddressOf HandleGridDoubleClick
        gridView.GridControl = grid
        '
        grid.MainView = gridView
        grid.ViewCollection.Add(gridView)

        gridView.GridControl.DataSource = buyers
        gridView.PopulateColumns()

        gridView.OptionsView.ShowFooter = True

        gridTransMode(gridView)
    End Sub

    Function multiplyFields(qty As trans_SalesReportBuyer) As Decimal
        Dim total As Decimal = 0

        Dim dc = New mkdbDataContext

        Dim price = (From i In dc.trans_SalesReportPrices Where qty.salesInvoiceID = i.salesReport_ID Select i).FirstOrDefault

        total += qty.skipjack0_300To0_499 * price.skipjack0_300To0_499
        total += qty.skipjack0_500To0_999 * price.skipjack0_500To0_999
        total += qty.skipjack1_0To1_39 * price.skipjack1_0To1_39
        total += qty.skipjack1_4To1_79 * price.skipjack1_4To1_79
        total += qty.skipjack1_8To2_49 * price.skipjack1_8To2_49
        total += qty.skipjack2_5To3_49 * price.skipjack2_5To3_49
        total += qty.skipjack3_5AndUP * price.skipjack3_5AndUP
        total += qty.yellowfin0_300To0_499 * price.yellowfin0_300To0_499
        total += qty.yellowfin0_500To0_999 * price.yellowfin0_500To0_999
        total += qty.yellowfin1_0To1_49 * price.yellowfin1_0To1_49
        total += qty.yellowfin1_5To2_49 * price.yellowfin1_5To2_49
        total += qty.yellowfin2_5To3_49 * price.yellowfin2_5To3_49
        total += qty.yellowfin3_5To4_99 * price.yellowfin3_5To4_99
        total += qty.yellowfin5_0To9_99 * price.yellowfin5_0To9_99
        total += qty.yellowfin10AndUpGood * price.yellowfin10AndUpGood
        total += qty.yellowfin10AndUpDeformed * price.yellowfin10AndUpDeformed
        total += qty.bigeye0_500To0_999 * price.bigeye0_500To0_999
        total += qty.bigeye1_0To1_49 * price.bigeye1_0To1_49
        total += qty.bigeye1_5To2_49 * price.bigeye1_5To2_49
        total += qty.bigeye2_5To3_49 * price.bigeye2_5To3_49
        total += qty.bigeye3_5To4_99 * price.bigeye3_5To4_99
        total += qty.bigeye5_0To9_99 * price.bigeye5_0To9_99
        total += qty.bigeye10AndUP * price.bigeye10AndUP
        total += qty.bonito * price.bonito
        total += qty.fishmeal * price.fishmeal

        Return Math.Round(total, 2)
    End Function

    Function multiplyFields(qty As trans_SalesReportCatcher) As Decimal
        Dim total As Decimal = 0

        Dim dc = New mkdbDataContext

        Dim price = (From i In dc.trans_SalesReportPrices Where qty.salesReport_ID = i.salesReport_ID Select i).FirstOrDefault

        total += qty.skipjack0_300To0_499 * price.skipjack0_300To0_499
        total += qty.skipjack0_500To0_999 * price.skipjack0_500To0_999
        total += qty.skipjack1_0To1_39 * price.skipjack1_0To1_39
        total += qty.skipjack1_4To1_79 * price.skipjack1_4To1_79
        total += qty.skipjack1_8To2_49 * price.skipjack1_8To2_49
        total += qty.skipjack2_5To3_49 * price.skipjack2_5To3_49
        total += qty.skipjack3_5AndUP * price.skipjack3_5AndUP
        total += qty.yellowfin0_300To0_499 * price.yellowfin0_300To0_499
        total += qty.yellowfin0_500To0_999 * price.yellowfin0_500To0_999
        total += qty.yellowfin1_0To1_49 * price.yellowfin1_0To1_49
        total += qty.yellowfin1_5To2_49 * price.yellowfin1_5To2_49
        total += qty.yellowfin2_5To3_49 * price.yellowfin2_5To3_49
        total += qty.yellowfin3_5To4_99 * price.yellowfin3_5To4_99
        total += qty.yellowfin5_0To9_99 * price.yellowfin5_0To9_99
        total += qty.yellowfin10AndUpGood * price.yellowfin10AndUpGood
        total += qty.yellowfin10AndUpDeformed * price.yellowfin10AndUpDeformed
        total += qty.bigeye0_500To0_999 * price.bigeye0_500To0_999
        total += qty.bigeye1_0To1_49 * price.bigeye1_0To1_49
        total += qty.bigeye1_5To2_49 * price.bigeye1_5To2_49
        total += qty.bigeye2_5To3_49 * price.bigeye2_5To3_49
        total += qty.bigeye3_5To4_99 * price.bigeye3_5To4_99
        total += qty.bigeye5_0To9_99 * price.bigeye5_0To9_99
        total += qty.bigeye10AndUP * price.bigeye10AndUP
        total += qty.bonito * price.bonito
        total += qty.fishmeal * price.fishmeal

        Return Math.Round(total, 2)
    End Function

    Private Sub HandleGridDoubleClick(sender As Object, e As EventArgs)
        Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim value = gridView.GetRowCellValue(gridView.FocusedRowHandle, "SalesInvoiceBuyerID")
        Dim ctrlB = New ctrlBuyers(Me, CInt(value))
    End Sub

    Protected Overrides Function GetGridControl() As GridControl
        Return If(grid IsNot Nothing, grid, Nothing)
    End Function

    Overrides Sub refreshData()
        loadGrid()
    End Sub

    Public Overrides Sub openForm()
        Dim ctrlB = New ctrlBuyers(Me)
        loadGrid()
    End Sub
End Class
