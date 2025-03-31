Imports DevExpress.XtraTab
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class ucSales
    Inherits ucBase

    Private tabControl As XtraTabControl
    Private grid As GridControl

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

    Private Sub gridLoaded(sender As Object, e As EventArgs)
        loadGrid()
    End Sub

    Sub loadGrid()
        Dim gridView = New GridView()
        gridView.GridControl = grid
        AddHandler gridView.DoubleClick, AddressOf HandleGridDoubleClick
        grid.MainView = gridView
        grid.ViewCollection.Add(gridView)

        Dim dc As New mkdbDataContext()
        Dim mdc As New tpmdbDataContext()

        Dim salesList = New SalesReport(dc).getByDate(CDate(dtFrom.EditValue), CDate(dtTo.EditValue)).ToList()

        Dim catchList = (From s In salesList
                         Join srp In dc.trans_SalesReportPrices On srp.salesReport_ID Equals s.salesReport_ID
                         Join src In dc.trans_SalesReportCatchers On src.salesReport_ID Equals s.salesReport_ID
                         Join cad In dc.trans_CatchActivityDetails On cad.catchActivityDetail_ID Equals src.catchActivityDetail_ID
                         Join ca In dc.trans_CatchActivities On ca.catchActivity_ID Equals cad.catchActivity_ID
                         Join v In mdc.ml_Vessels On cad.vessel_ID Equals v.ml_vID
                         Select s, src, srp, cad, ca, v).ToList()

        Dim salesData = (From s In salesList
                         Let groupedCatchQ = catchList.Where(Function(j) s.salesReport_ID = j.s.salesReport_ID).
                                                       GroupBy(Function(j) j.src.catchActivityDetail_ID).ToList()
                         Let actualQty = groupedCatchQ.Select(Function(g) g.FirstOrDefault().src).ToList()
                         Let spoilage = groupedCatchQ.Select(Function(g) If(g.Count() > 1, g.Skip(1).FirstOrDefault().src, Nothing)).ToList()
                         Let totalAmount = actualQty.Sum(Function(j) multiplyFields(j)) - spoilage.Sum(Function(j) multiplyFields(j))
                         Select New With {
                                           .salesReport_ID = s.salesReport_ID,
                                           .SalesNo = s.salesNum,
                                           .Catcher = (From j In catchList Where s.salesReport_ID = j.s.salesReport_ID Select j.ca.catchReferenceNum).Distinct().First,
                                           .CoveredDate = s.salesDate,
                                           .SellingType = s.sellingType,
                                           .Buyer = s.buyer,
                                           .ActualQty = actualQty.Sum(Function(j) sumFields(j)),
                                           .Fishmeal = actualQty.Sum(Function(j) j.fishmeal) - spoilage.Sum(Function(j) j.fishmeal),
                                           .Spoilage = spoilage.Sum(Function(j) sumFields(j)),
                                           .NetQty = actualQty.Sum(Function(j) sumFields(j)) - spoilage.Sum(Function(j) sumFields(j)),
                                           .SalesInUSD = Math.Round(totalAmount / s.usdRate, 2),
                                           .USDRate = s.usdRate,
                                           .SalesInPHP = totalAmount,
                                           .AveragePrice = totalAmount
                                       }).ToList


        '.Vessels = (From j In catchList Where s.salesReport_ID = j.s.salesReport_ID Select j.v.vesselName).Distinct.ToList,

        gridView.GridControl.DataSource = salesData
        gridView.PopulateColumns()
        gridTransMode(gridView)
    End Sub

    Function sumFields(record As trans_SalesReportCatcher) As Decimal
        Return record.skipjack0_300To0_499 + record.skipjack0_500To0_999 +
               record.skipjack1_0To1_39 + record.skipjack1_4To1_79 +
               record.skipjack1_8To2_49 + record.skipjack2_5To3_49 +
               record.skipjack3_5AndUP + record.yellowfin0_300To0_499 +
               record.yellowfin0_500To0_999 + record.yellowfin1_0To1_49 +
               record.yellowfin1_5To2_49 + record.yellowfin2_5To3_49 +
               record.yellowfin3_5To4_99 + record.yellowfin5_0To9_99 +
               record.yellowfin10AndUpGood + record.yellowfin10AndUpDeformed +
               record.bigeye0_500To0_999 + record.bigeye1_0To1_49 +
               record.bigeye1_5To2_49 + record.bigeye2_5To3_49 +
               record.bigeye3_5To4_99 + record.bigeye5_0To9_99 +
               record.bigeye10AndUP + record.bonito + record.fishmeal
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
        Dim value = gridView.GetRowCellValue(gridView.FocusedRowHandle, "salesReport_ID")
        Dim ctrlSI = New ctrlSales(Me, CInt(value))
    End Sub


    Protected Overrides Function GetGridControl() As GridControl
        Return If(grid IsNot Nothing, grid, Nothing)
    End Function

    Overrides Sub refreshData()
        loadGrid()
    End Sub

    Public Overrides Sub openForm()
        Dim ctrl = New ctrlSales(Me)
    End Sub


End Class
