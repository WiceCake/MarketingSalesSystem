Imports DevExpress.XtraTab
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid.Columns

Public Class ucSales
    Inherits ucBase

    Private tabControl As XtraTabControl
    Private gridBuyer As GridControl
    Private gridCatcher As GridControl
    Private currentGrid As GridControl
    Private catcherGridView As GridView
    Private buyerGridView As GridView
    Private refreshCatcher As Boolean = False
    Private refreshBuyer As Boolean = False

    Private catcherTab As XtraTabPage
    Private buyerTab As XtraTabPage

    Sub New(ByVal title As String)
        InitializeComponent()

        MyBase.title = title
        LabelControl1.Text = title

        loadData()

        AddHandler gridBuyer.Load, AddressOf gridBuyerLoaded
        AddHandler gridCatcher.Load, AddressOf gridCatcherLoaded
        AddHandler tabControl.SelectedPageChanging, AddressOf XtraTabControl_SelectedPageChanging

        currentGrid = gridBuyer

        dtFrom.EditValue = Date.Now.AddDays(-15)
        dtFrom.Properties.MaxValue = Date.Now

        dtTo.EditValue = Date.Now
        dtTo.Properties.MaxValue = Date.Now.AddDays(1)

    End Sub

    Private Sub buyerFooter(ByVal sender As System.Object, ByVal e As PageFooterArea)

    End Sub

    Private Sub XtraTabControl_SelectedPageChanging(ByVal sender As System.Object, ByVal e As TabPageChangingEventArgs)
        If e.Page Is catcherTab Then
            currentGrid = gridCatcher
        ElseIf e.Page Is buyerTab Then
            currentGrid = gridBuyer
        End If
    End Sub

    Sub loadData()

        tabControl = New XtraTabControl()

        buyerTab = New XtraTabPage() With {.Name = "buyerTab", .Text = "By Buyer"}
        catcherTab = New XtraTabPage() With {.Name = "catcherTab", .Text = "By Catcher"}

        tabControl.TabPages.Add(buyerTab)
        tabControl.TabPages.Add(catcherTab)

        LayoutControl2.Controls.Add(tabControl)

        Dim mainLayoutItem As LayoutControlItem = LayoutControl2.AddItem("", tabControl)
        mainLayoutItem.TextVisible = False

        Dim layoutBuyer As New LayoutControl() With {.Name = "layoutBuyer", .Dock = DockStyle.Fill}
        Dim layoutCatcher As New LayoutControl() With {.Name = "layoutCatcher", .Dock = DockStyle.Fill}

        buyerTab.Controls.Add(layoutBuyer)
        catcherTab.Controls.Add(layoutCatcher)

        gridBuyer = New GridControl() With {.Dock = DockStyle.Fill}
        gridCatcher = New GridControl() With {.Dock = DockStyle.Fill}

        layoutBuyer.Controls.Add(gridBuyer)
        layoutCatcher.Controls.Add(gridCatcher)

        Dim layoutBuyerItem As LayoutControlItem = layoutBuyer.AddItem("", gridBuyer)
        Dim layoutCatcherItem As LayoutControlItem = layoutCatcher.AddItem("", gridCatcher)
        layoutBuyerItem.TextVisible = False
        layoutCatcherItem.TextVisible = False
    End Sub

    Private Sub gridBuyerLoaded(sender As Object, e As EventArgs)
        loadGridBuyer()
        AddHandler buyerGridView.DoubleClick, AddressOf HandleGridDoubleClick
    End Sub

    Private Sub gridCatcherLoaded(sender As Object, e As EventArgs)
        loadGridCatcher()
        AddHandler catcherGridView.DoubleClick, AddressOf HandleGridDoubleClick
    End Sub

    Sub loadGridCatcher()
        If catcherGridView Is Nothing Then catcherGridView = New GridView() : refreshCatcher = True
        catcherGridView.GridControl = gridCatcher
        gridCatcher.MainView = catcherGridView
        gridCatcher.ViewCollection.Add(catcherGridView)

        Dim dc As New mkdbDataContext()
        Dim mdc As New tpmdbDataContext()

        ' New version but faster
        ' Load sales reports only once
        Dim salesList = New SalesReport(dc).getByDate(CDate(dtFrom.EditValue), CDate(dtTo.EditValue)).ToList()

        ' Store trans_SalesReportCatchers in memory with an anonymous type as the key
        Dim catchList = dc.trans_SalesReportCatchers.
                        Select(Function(src) New With {Key .salesReport_ID = src.salesReport_ID, Key .catchActivityDetail_ID = src.catchActivityDetail_ID}).
                        Distinct().
                        ToList()

        ' Convert vessels into a Dictionary for faster lookup
        Dim vesselDict = mdc.ml_Vessels.ToDictionary(Function(v) v.ml_vID, Function(v) v.vesselName)

        ' Store CatchActivityDetails and CatchActivities for lookup
        Dim catchActivityDetailsDict = dc.trans_CatchActivityDetails.
                                       Join(dc.trans_CatchActivities,
                                            Function(cad) cad.catchActivity_ID,
                                            Function(ca) ca.catchActivity_ID,
                                            Function(cad, ca) New With {cad.catchActivityDetail_ID, cad, ca}).
                                       ToDictionary(Function(x) x.catchActivityDetail_ID, Function(x) x)

        ' Store SalesReportCatchers data for lookup using an anonymous type as the key
        Dim catchersDict = dc.trans_SalesReportCatchers.
                           GroupBy(Function(src) New With {Key .salesReport_ID = src.salesReport_ID, Key .catchActivityDetail_ID = src.catchActivityDetail_ID}).
                           ToDictionary(Function(g) g.Key, Function(g) g.ToList())

        Dim buyerDict = mdc.ml_Suppliers.ToDictionary(Function(s) s.ml_SupID, Function(s) s.ml_Supplier)

        Dim number As Integer

        ' Process the final query
        Dim data = (From cl In catchList
                    Join s In salesList On s.salesReport_ID Equals cl.salesReport_ID
                    Let key = New With {Key .salesReport_ID = cl.salesReport_ID, Key .catchActivityDetail_ID = cl.catchActivityDetail_ID}
                    Let catchData = catchActivityDetailsDict(cl.catchActivityDetail_ID)
                    Let buyer = If(Integer.TryParse(s.buyer, number), buyerDict(CInt(s.buyer)), s.buyer)
                    Let catchersData = catchersDict(key)
                    Let firstCatcher = catchersData.FirstOrDefault()
                    Let secondCatcher = catchersData(1)
                    Let actualQty = sumFields(firstCatcher)
                    Let spoilageQty = sumFields(secondCatcher)
                    Let totalAmount = multiplyFields(firstCatcher) - multiplyFields(secondCatcher)
                    Select New With {
                        .salesReport_ID = s.salesReport_ID,
                        .SalesNo = s.salesNum,
                        .InvoiceNo = s.invoiceNum,
                        .Catcher = vesselDict(catchData.cad.vessel_ID),
                        .CatchReferenceNumber = catchData.ca.catchReferenceNum,
                        .CoveredDate = s.salesDate,
                        .SellingType = s.sellingType,
                        .Buyer = buyer,
                        .ActualQty = actualQty,
                        .Fishmeal = firstCatcher.fishmeal - secondCatcher.fishmeal,
                        .Spoilage = spoilageQty,
                        .NetQty = actualQty - spoilageQty,
                        .SalesInUSD = Math.Round(totalAmount / s.usdRate, 2),
                        .USDRate = s.usdRate,
                        .SalesInPHP = totalAmount,
                        .AveragePrice = totalAmount
                    }).ToList()


        ' Slow Version but working
        'Dim salesList = New SalesReport(dc).getByDate(CDate(dtFrom.EditValue), CDate(dtTo.EditValue)).ToList()
        '
        'Dim catchList = (From src In dc.trans_SalesReportCatchers Select src.salesReport_ID, src.catchActivityDetail_ID).Distinct().ToList()
        'Dim vesselList = mdc.ml_Vessels.ToDictionary(Function(v) v.ml_vID, Function(v) v.vesselName)
        '
        'Dim data = (From cl In catchList
        '            Join s In salesList On s.salesReport_ID Equals cl.salesReport_ID
        '            Let catchData = (From cad In dc.trans_CatchActivityDetails
        '                             Join ca In dc.trans_CatchActivities On cad.catchActivity_ID Equals ca.catchActivity_ID
        '                             Where cl.catchActivityDetail_ID = cad.catchActivityDetail_ID Select cad, ca).FirstOrDefault
        '            Let catchersData = (From src In dc.trans_SalesReportCatchers
        '                                Where src.catchActivityDetail_ID = cl.catchActivityDetail_ID AndAlso src.salesReport_ID = cl.salesReport_ID Select src)
        '            Let actualQty = sumFields(catchersData.FirstOrDefault)
        '            Let spoilageQty = sumFields(catchersData.Skip(1).FirstOrDefault)
        '            Let totalAmount = multiplyFields(catchersData.FirstOrDefault) - multiplyFields(catchersData.Skip(1).FirstOrDefault)
        '            Select New With {
        '                    .salesReport_ID = s.salesReport_ID,
        '                    .SalesNo = s.salesNum,
        '                    .Catcher = vesselList(catchData.cad.vessel_ID),
        '                    .CatchReferenceNumber = catchData.ca.catchReferenceNum,
        '                    .CoveredDate = s.salesDate,
        '                    .SellingType = s.sellingType,
        '                    .Buyer = s.buyer,
        '                    .ActualQty = actualQty,
        '                    .Fishmeal = catchersData.FirstOrDefault.fishmeal - catchersData.Skip(1).FirstOrDefault.fishmeal,
        '                    .Spoilage = spoilageQty,
        '                    .NetQty = actualQty - spoilageQty,
        '                    .SalesInUSD = Math.Round(totalAmount / s.usdRate, 2),
        '                    .USDRate = s.usdRate,
        '                    .SalesInPHP = totalAmount,
        '                    .AveragePrice = totalAmount
        '            })

        catcherGridView.GridControl.DataSource = data
        If Not refreshCatcher Then catcherGridView.PopulateColumns()

        ' Enable footer
        catcherGridView.OptionsView.ShowFooter = True

        gridTransMode(catcherGridView)

    End Sub

    Sub loadGridBuyer()
        If buyerGridView Is Nothing Then buyerGridView = New GridView() : refreshBuyer = True
        buyerGridView.GridControl = gridBuyer
        gridBuyer.MainView = buyerGridView
        gridBuyer.ViewCollection.Add(buyerGridView)

        Dim dc As New mkdbDataContext()
        Dim mdc As New tpmdbDataContext()

        ' Faster approach
        ' Load only needed SalesReports
        Dim salesList = New SalesReport(dc).getByDate(CDate(dtFrom.EditValue), CDate(dtTo.EditValue)).ToList()

        ' Create lookups for catchers grouped by salesReport_ID
        Dim catcherKiloLookup = dc.trans_SalesReportCatchers.
            ToLookup(Function(s) s.salesReport_ID)

        Dim catcherSpoilageLookup = dc.trans_SalesReportCatchers.
            GroupBy(Function(s) s.catchActivityDetail_ID).
            SelectMany(Function(g) g.Skip(1)).
            ToLookup(Function(s) s.salesReport_ID)

        ' Cache catch reference numbers by SalesReportID
        Dim catchRefDict = (From srCatch In dc.trans_SalesReportCatchers
                            Join detail In dc.trans_CatchActivityDetails
                            On srCatch.catchActivityDetail_ID Equals detail.catchActivityDetail_ID
                            Join activity In dc.trans_CatchActivities
                            On detail.catchActivity_ID Equals activity.catchActivity_ID
                            Select New With {
                                .SalesReportID = srCatch.salesReport_ID,
                                .CatchReferenceNum = activity.catchReferenceNum
                            }).Distinct().
                            ToDictionary(Function(x) x.SalesReportID, Function(x) x.CatchReferenceNum)

        ' Final result query
        Dim data = (From s In salesList
                    Let allCatchers = catcherKiloLookup(s.salesReport_ID)
                    Let spoilageCatchers = catcherSpoilageLookup(s.salesReport_ID)
                    Let actualQty = allCatchers.Sum(Function(i) sumFields(i))
                    Let fishMeal = allCatchers.Sum(Function(i) i.fishmeal)
                    Let spoilageQty = spoilageCatchers.Sum(Function(i) sumFields(i))
                    Let actualPrice = allCatchers.Sum(Function(i) multiplyFields(i))
                    Let spoilagePrice = spoilageCatchers.Sum(Function(i) multiplyFields(i))
                    Let totalPrice = actualPrice - spoilagePrice
                    Let catcherRef = If(catchRefDict.ContainsKey(s.salesReport_ID), catchRefDict(s.salesReport_ID), "")
                    Select New With {
                        .salesReport_ID = s.salesReport_ID,
                        .SalesNo = s.salesNum,
                        .InvoiceNo = s.invoiceNum,
                        .CatcherRefNum = catcherRef,
                        .CoveredDate = s.salesDate,
                        .SellingType = s.sellingType,
                        .ActualQty = actualQty,
                        .Fishmeal = fishMeal,
                        .Spoilage = spoilageQty,
                        .NetQty = actualQty - spoilageQty,
                        .SalesInUSD = Math.Round(If(s.usdRate > 0, totalPrice / s.usdRate, 0), 2),
                        .USDRate = s.usdRate,
                        .SalesInPHP = totalPrice,
                        .AveragePrice = totalPrice
                    }).ToList()

            ' Slow Approach
            'Dim salesList = New SalesReport(dc).getByDate(CDate(dtFrom.EditValue), CDate(dtTo.EditValue)).ToList()
            '
            'Dim data = (From s In salesList
            '            Let catchData = (From src In dc.trans_SalesReportCatchers
            '                               Join cad In dc.trans_CatchActivityDetails On src.catchActivityDetail_ID Equals cad.catchActivityDetail_ID
            '                               Join ca In dc.trans_CatchActivities On ca.catchActivity_ID Equals cad.catchActivity_ID
            '                               Where src.salesReport_ID = s.salesReport_ID Select ca).FirstOrDefault
            '            Let catcherData = (From src In dc.trans_SalesReportCatchers
            '                               Where src.salesReport_ID = s.salesReport_ID
            '                               Group By src.catchActivityDetail_ID Into cGroup = Group Select cGroup)
            '            Let actualQty = catcherData.Select(Function(aq) aq.FirstOrDefault).ToList
            '            Let spoilageQty = catcherData.Select(Function(sq) sq.Skip(1).FirstOrDefault).ToList
            '            Let totalAmount = actualQty.Sum(Function(aq) multiplyFields(aq)) - spoilageQty.Sum(Function(sq) multiplyFields(sq))
            '            Select New With {
            '                .salesReport_ID = s.salesReport_ID,
            '                .SalesNo = s.salesNum,
            '                .CatcherRefNum = catchData.catchReferenceNum,
            '                .CoveredDate = s.salesDate,
            '                .SellingType = s.sellingType,
            '                .Buyer = s.buyer,
            '                .ActualQty = actualQty.Sum(Function(aq) sumFields(aq)),
            '                .Fishmeal = actualQty.Sum(Function(aq) aq.fishmeal) - spoilageQty.Sum(Function(sq) sq.fishmeal),
            '                .Spoilage = spoilageQty.Sum(Function(sq) sumFields(sq)),
            '                .NetQty = actualQty.Sum(Function(aq) sumFields(aq)) - spoilageQty.Sum(Function(sq) sumFields(sq)),
            '                .SalesInUSD = Math.Round(totalAmount / s.usdRate, 2),
            '                .USDRate = s.usdRate,
            '                .SalesInPHP = totalAmount,
            '                .AveragePrice = totalAmount
            '            })

        buyerGridView.GridControl.DataSource = data
        If Not refreshBuyer Then buyerGridView.PopulateColumns()

            ' Enable footer
        buyerGridView.OptionsView.ShowFooter = True

        gridTransMode(buyerGridView)
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
        Return If(currentGrid IsNot Nothing, currentGrid, Nothing)
    End Function

    Overrides Sub refreshData()
        loadGridBuyer()
        loadGridCatcher()
    End Sub

    Public Overrides Sub openForm()
        Dim ctrl = New ctrlSales(Me)
    End Sub


End Class
