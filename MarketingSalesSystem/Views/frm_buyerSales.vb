Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid
Imports System.Text

Public Class frm_buyerSales

    Private ctrlB As ctrlBuyers

    Public dtAK As DataTable
    Public dtS As DataTable

    Sub New(ctrl As ctrlBuyers)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrlB = ctrl
    End Sub

    Private Sub rBuyer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rBuyer.SelectedIndexChanged
        Dim val = TryCast(sender, RadioGroup)

        With ctrlB
            If val.SelectedIndex = 0 Then
                .showTxtBuyer()
            Else
                .showCbBuyer()
            End If
        End With
    End Sub

    Sub createKiloBands(catcherID As Integer)
        Dim mkdb As New mkdbDataContext

        Dim catchers As List(Of Integer) = mkdb.trans_CatchActivityDetails.
            Where(Function(j) j.catchActivity_ID = catcherID).
            Select(Function(j) j.vessel_ID).ToList()

        Dim bandClass = AddBand("Class", BandedGridView1)
        Dim bandSize = AddBand("Size", BandedGridView1)
        Dim bandPrice = AddBand("Price", BandedGridView1)

        Dim bandKilos = AddBand("Kilos", BandedGridView1)
        Dim bandAmount = AddBand("Amount", BandedGridView1)
        populateBand(bandKilos, catchers)
        Dim bandKTotal = AddBand("Total", bandKilos) ' Restored Total band
        populateBand(bandAmount, catchers)
        Dim bandATotal = AddBand("Total", bandAmount) ' Restored Total band

        BandedGridView1.PopulateColumns()

        With BandedGridView1

            .Columns("Class").OwnerBand = bandClass
            .Columns("Size").OwnerBand = bandSize
            .Columns("Price").OwnerBand = bandPrice
            setOwnerBand("K_Catcher", bandKilos, BandedGridView1)
            setOwnerBand("A_Catcher", bandAmount, BandedGridView1, True)
            .Columns("Kilo_Total").OwnerBand = bandKTotal
            .Columns("Amount_Total").OwnerBand = bandATotal

            .Columns("Class").OptionsColumn.ReadOnly = True
            .Columns("Size").OptionsColumn.ReadOnly = True
            .Columns("Price").OptionsColumn.ReadOnly = True
            .Columns("Kilo_Total").OptionsColumn.ReadOnly = True
            .Columns("Amount_Total").OptionsColumn.ReadOnly = True

            .BestFitColumns()
            .OptionsView.ColumnAutoWidth = False
            .OptionsView.ShowColumnHeaders = False

            bandClass.Fixed = Columns.FixedStyle.Left
            bandSize.Fixed = Columns.FixedStyle.Left

            .OptionsView.ShowFooter = True

            ' ---- Align Headers & Columns ----
            For Each band As GridBand In .Bands
                SetHeaderAlignment(band)
            Next
        End With
    End Sub

    Sub createSpoilageBand(catcherID As Integer)
        Dim mkdb As New mkdbDataContext

        Dim catchers As List(Of Integer) = mkdb.trans_CatchActivityDetails.
            Where(Function(j) j.catchActivity_ID = catcherID).
            Select(Function(j) j.vessel_ID).ToList()

        Dim bandClass = AddBand("Class", BandedGridView2)
        Dim bandSize = AddBand("Size", BandedGridView2)
        Dim bandPrice = AddBand("Price", BandedGridView2)

        Dim bandKilos = AddBand("Kilos", BandedGridView2)
        Dim bandAmount = AddBand("Amount", BandedGridView2)
        populateBand(bandKilos, catchers)
        Dim bandKTotal = AddBand("Total", bandKilos) ' Restored Total band
        populateBand(bandAmount, catchers)
        Dim bandATotal = AddBand("Total", bandAmount) ' Restored Total band

        BandedGridView2.PopulateColumns()

        With BandedGridView2

            .Columns("Class").OwnerBand = bandClass
            .Columns("Size").OwnerBand = bandSize
            .Columns("Price").OwnerBand = bandPrice
            setOwnerBand("K_Catcher", bandKilos, BandedGridView2, True)
            setOwnerBand("A_Catcher", bandAmount, BandedGridView2, True)
            .Columns("Kilo_Total").OwnerBand = bandKTotal
            .Columns("Amount_Total").OwnerBand = bandATotal

            .Columns("Class").OptionsColumn.ReadOnly = True
            .Columns("Size").OptionsColumn.ReadOnly = True
            .Columns("Price").OptionsColumn.ReadOnly = True
            .Columns("Kilo_Total").OptionsColumn.ReadOnly = True
            .Columns("Amount_Total").OptionsColumn.ReadOnly = True

            .BestFitColumns()
            .OptionsView.ColumnAutoWidth = False
            .OptionsView.ShowColumnHeaders = False

            bandClass.Fixed = Columns.FixedStyle.Left
            bandSize.Fixed = Columns.FixedStyle.Left

            .OptionsView.ShowFooter = True

            ' ---- Align Headers & Columns ----
            For Each band As GridBand In .Bands
                SetHeaderAlignment(band)
            Next
        End With
    End Sub

    Private Sub GridControl1_Load(sender As Object, e As EventArgs) Handles GridControl1.Load
        ' For testing
    End Sub

    Sub SetHeaderAlignment(ByVal band As DevExpress.XtraGrid.Views.BandedGrid.GridBand)
        band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        For Each subBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand In band.Children.Cast(Of DevExpress.XtraGrid.Views.BandedGrid.GridBand)()
            SetHeaderAlignment(subBand)
        Next
    End Sub

    Function AddBand(ByVal caption As String, ByRef parent As GridBand) As GridBand
        Dim band As New GridBand() With {.Caption = caption}

        parent.Children.Add(band)
        Return band
    End Function

    Function AddBand(ByVal caption As String, ByRef gridView As BandedGridView) As GridBand
        Dim band As New GridBand() With {.Caption = caption}
        gridView.Bands.Add(band)
        Return band
    End Function

    Sub populateBand(ByRef parent As GridBand, catchers As List(Of Integer))
        Dim tspdb As New tpmdbDataContext

        ' Fetch all vessel names in one query and store them in a Dictionary
        Dim vesselDict As Dictionary(Of Integer, String) = tspdb.ml_Vessels.
            Where(Function(i) catchers.Contains(i.ml_vID)).
            ToDictionary(Function(i) i.ml_vID, Function(i) i.vesselName)

        ' Iterate only once and add bands
        For Each catcher In catchers
            Dim vesselName As String = If(vesselDict.ContainsKey(catcher), vesselDict(catcher), "Unknown")
            Dim band As New GridBand() With {.Caption = vesselName}
            parent.Children.Add(band)
        Next
    End Sub

    Sub setOwnerBand(caption As String, parentBand As GridBand, bandGV As BandedGridView, Optional isReadOnly As Boolean = False)
        Dim countBand = 1
        For Each band As GridBand In parentBand.Children
            If band.Caption IsNot "Total" Then
                With bandGV.Columns(caption & countBand)
                    .OwnerBand = band
                    .UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    If isReadOnly Then .OptionsColumn.ReadOnly = True
                    .Summary.Add(DevExpress.Data.SummaryItemType.Sum, caption & countBand, "Total: {0}")
                End With
            End If
            countBand = countBand + 1
        Next
    End Sub

    Private Sub BandedGridView1_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles BandedGridView1.CellValueChanged
        Dim view As BandedGridView = TryCast(sender, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
        If view Is Nothing Then
            Return
        End If
        If e.Column.FieldName = "Kilo_Total" Or e.Column.FieldName = "Amount_Total" Then
            Return
        End If

        Dim r As DataRowView = CType(view.GetRow(view.FocusedRowHandle), DataRowView)
        ctrlB.updateKiloTotal(r.Row)

        getActualUnloadingTotal()
    End Sub

    Private Sub lueInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles lueCarrierInvoice.EditValueChanged
        BandedGridView1.Bands.Clear()
        BandedGridView2.Bands.Clear()
        Dim invoice = CType(sender, DevExpress.XtraEditors.LookUpEdit)

        If invoice.EditValue Is Nothing Then Return

        Dim mkdb As New mkdbDataContext
        Dim tpmdb As New tpmdbDataContext

        Dim catcher = From i In mkdb.trans_SalesReportCatchers
                      Join j In mkdb.trans_CatchActivityDetails On i.catchActivityDetail_ID Equals j.catchActivityDetail_ID
                      Where i.salesReport_ID = CInt(invoice.EditValue) Select j.catchActivity_ID

        Dim catcherID = catcher.FirstOrDefault
        Dim val = CInt(invoice.EditValue)

        loadCarrier(mkdb, tpmdb, val)

        ctrlB.initKiloDataTable(catcherID)
        ctrlB.initSpoilageDataTable(catcherID)
        GridControl1.DataSource = dtAK
        GridControl2.DataSource = dtS

        createKiloBands(catcherID)
        createSpoilageBand(catcherID)
        ctrlB.loadKiloRows(val)
        ctrlB.loadSpoilageRows(val)

        showRows()

        getActualUnloadingTotal()
        getSpoilageTotal()
    End Sub

    Sub loadCarrier(mkdb As mkdbDataContext, tpmdb As tpmdbDataContext, invoiceID As Integer)

        Dim carriers = (From i In mkdb.trans_SalesUnloadeds
                       Where i.SalesReportID = invoiceID Select New With {
                           .ID = i.CarrierID,
                           .Name = i.CarrierName})

        Dim carrierCompany = (From i In tpmdb.ml_Vessels Select i).ToDictionary(Function(s) s.ml_vID, Function(v) v.vesselName)

        Dim carrierList As New List(Of Object)

        For Each c In carriers
            If Integer.TryParse(c.Name, 0) Then
                carrierList.Add(New With {.ID = c.ID, .Name = carrierCompany(CInt(c.Name))})
            Else
                carrierList.Add(New With {.ID = c.ID, .Name = c.Name})
            End If
        Next

        conCarrier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lookUpTransMode(carrierList, lueCarrier, "Name", "ID", "Select Carrier")
    End Sub

    Sub showRows()
        Dim val = cmbSaleType.EditValue
        Dim filters As New List(Of DevExpress.Data.Filtering.CriteriaOperator)

        If val Is "Export" Then
            filters = New List(Of DevExpress.Data.Filtering.CriteriaOperator) From {
                New DevExpress.Data.Filtering.BinaryOperator("Class", "SKIPJACK", DevExpress.Data.Filtering.BinaryOperatorType.NotEqual),
                New DevExpress.Data.Filtering.BinaryOperator("Class", "BIGEYE", DevExpress.Data.Filtering.BinaryOperatorType.NotEqual),
                New DevExpress.Data.Filtering.BinaryOperator("Class", "FISHMEAL", DevExpress.Data.Filtering.BinaryOperatorType.NotEqual),
                New DevExpress.Data.Filtering.BinaryOperator("Class", "BONITO", DevExpress.Data.Filtering.BinaryOperatorType.NotEqual),
                New DevExpress.Data.Filtering.BinaryOperator("Class", "YELLOWFIN", DevExpress.Data.Filtering.BinaryOperatorType.Equal),
                New DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.Or,
                    New DevExpress.Data.Filtering.BinaryOperator("Size", "10 - UP GOOD", DevExpress.Data.Filtering.BinaryOperatorType.Equal),
                    New DevExpress.Data.Filtering.BinaryOperator("Size", "10 - UP DEFORMED", DevExpress.Data.Filtering.BinaryOperatorType.Equal)
                )
            }
        ElseIf val Is "Local" Then
            filters = New List(Of DevExpress.Data.Filtering.CriteriaOperator) From {
                New DevExpress.Data.Filtering.BinaryOperator("Class", "SKIPJACK", DevExpress.Data.Filtering.BinaryOperatorType.NotEqual),
                New DevExpress.Data.Filtering.BinaryOperator("Class", "BIGEYE", DevExpress.Data.Filtering.BinaryOperatorType.NotEqual),
                New DevExpress.Data.Filtering.BinaryOperator("Class", "FISHMEAL", DevExpress.Data.Filtering.BinaryOperatorType.NotEqual),
                New DevExpress.Data.Filtering.BinaryOperator("Class", "BONITO", DevExpress.Data.Filtering.BinaryOperatorType.NotEqual)
            }
        Else
            filters = Nothing
        End If

        If filters IsNot Nothing Then
            BandedGridView1.ActiveFilterCriteria = New DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.And, filters)
            BandedGridView2.ActiveFilterCriteria = New DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.And, filters)
        End If

        BandedGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        BandedGridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

    Private Function SumAmount(ByVal dt As DataTable, Optional ByVal columnName As String = "Amount_Total") As Decimal
        Dim amount As Decimal
        Dim val = cmbSaleType.EditValue

        If val Is "Export" Then
            amount = dt.AsEnumerable().Where(Function(row) row("Class") Is "YELLOWFIN" AndAlso (row("Price") Is "10 - UP GOOD" OrElse row("Price") Is "10 - UP DEFORMED")).Sum(Function(row) CDec(row(columnName)))
        ElseIf val Is "Local" Then
            amount = dt.AsEnumerable().Where(Function(row) row("Class") Is "YELLOWFIN").Sum(Function(row) CDec(row(columnName)))
        Else
            amount = dt.AsEnumerable().Sum(Function(row) CDec(row(columnName)))
        End If

        Return Math.Round(amount, 2)
    End Function

    Sub getActualUnloadingTotal()
        txtActualUnloading.EditValue = SumAmount(dtAK)
    End Sub

    Sub getSpoilageTotal()
        txtSpoilage.EditValue = SumAmount(dtS)
    End Sub

    Sub getAmountTotal()
        Dim totalAUAmount As Decimal = SumAmount(dtAK)
        Dim totalSAmount As Decimal = SumAmount(dtS)
        txtTotalAmount.EditValue = Math.Round(totalAUAmount - totalSAmount, 2)
    End Sub

    Private Sub txtAU_EditValueChanged(sender As Object, e As EventArgs) Handles txtActualUnloading.EditValueChanged
        If Not CDec(txtActualUnloading.EditValue) = 0 Then
            getAmountTotal()

            txtAdjustments.ReadOnly = False
            txtAdjustments.EditValue = 0
            txtAmountPaid.ReadOnly = False
            txtAmountPaid.EditValue = 0
        End If
    End Sub

    Private Sub txtAdj_EditValueChanged(sender As Object, e As EventArgs) Handles txtAdjustments.EditValueChanged
        Dim maxTAVal = Math.Max(0, CDec(txtTotalAmount.EditValue))
        Dim maxAdj As Decimal = 0

        If Not IsDBNull(txtAdjustments.EditValue) AndAlso Not String.IsNullOrWhiteSpace(txtAdjustments.EditValue.ToString()) AndAlso IsNumeric(txtAdjustments.EditValue) Then
            maxAdj = CDec(txtAdjustments.EditValue)
        End If

        Dim totalAmount As Decimal = 0

        If maxAdj < maxTAVal Then
            totalAmount = maxTAVal - maxAdj
        Else
            txtAdjustments.EditValue = 0
            totalAmount = maxTAVal
        End If
        txtOverallTotalAmount.EditValue = Math.Round(totalAmount, 2)
    End Sub

    Private Sub txtAmountPaid_EditValueChanged(sender As Object, e As EventArgs) Handles txtAmountPaid.EditValueChanged
        If txtAmountPaid.EditValue Is Nothing Then Return

        Dim paidAmount As Decimal = 0
        Dim totalPercentage As Decimal = 0
        Dim remainingBalance As Decimal = 0

        If Not IsDBNull(txtAmountPaid.EditValue) AndAlso Not String.IsNullOrWhiteSpace(txtAmountPaid.EditValue.ToString()) AndAlso IsNumeric(txtAmountPaid.EditValue) Then
            paidAmount = CDec(txtAmountPaid.EditValue)
        End If

        If txtOverallTotalAmount.EditValue IsNot Nothing Then
            Dim totalDebt = txtOverallTotalAmount.EditValue

            totalPercentage = Math.Round((CDec(paidAmount) / CDec(totalDebt)) * 100, 2)
            txtAmountInPercentage.EditValue = "% " & totalPercentage
            remainingBalance = CDec(totalDebt) - CDec(paidAmount)
            txtRemainingBalance.EditValue = remainingBalance
        End If

    End Sub

    Private Sub btnSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSave.ItemClick

        Dim dateCreated = validateField(dtEncoded)
        Dim typeOfSale = validateField(cmbSaleType)
        Dim invoice = validateField(lueCarrierInvoice)
        Dim setNo = validateField(txtSetNo)
        Dim amountPaid = validateField(txtAmountPaid)
        Dim buyer = False

        If rBuyer.SelectedIndex = 0 Then
            buyer = validateField(txtBuyer)
        Else
            buyer = validateField(cmbBuyer)
        End If

        Dim missingFields As New StringBuilder()
        If Not dateCreated Then missingFields.AppendLine("Date Created")
        If Not typeOfSale Then missingFields.AppendLine("Sale Type")
        If Not invoice Then missingFields.AppendLine("Invoice")
        If Not setNo Then missingFields.AppendLine("Set No.")
        If Not amountPaid Then missingFields.AppendLine("Amount Paid")
        If Not buyer Then missingFields.AppendLine("Buyer")

        If cmbSaleType.EditValue Is "Export" Then
            If Not validateField(txtContainerNum) Then missingFields.AppendLine("Container No.")
        ElseIf cmbSaleType.EditValue Is "Backing" Then
            If Not validateField(lueBacking) Then missingFields.AppendLine("Backing")
        End If

        If BarCheckItem4.Checked OrElse BarCheckItem5.Checked Then
            If Not validateField(lueReport) Then missingFields.AppendLine("Report")
        End If

        If missingFields.Length > 0 Then
            requiredMessage(missingFields.ToString())
            Return
        End If

        ctrlB.saveDraft()
    End Sub

    Private Sub btnDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        ctrlB.deleteBuyer()
    End Sub

    Private Sub btnPost_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPost.ItemClick
        ctrlB.postBuyer()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        ctrlB.print()
    End Sub

    Private Sub cmbSaleType_EditValueChanged(sender As Object, e As EventArgs) Handles cmbSaleType.EditValueChanged
        Dim val = TryCast(sender, ComboBoxEdit).EditValue()

        conCarrierInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        lueCarrierInvoice.EditValue = Nothing
        lueCarrier.EditValue = Nothing
        lueCarrier.Properties.DataSource = Nothing
        GridControl1.DataSource = Nothing
        GridControl2.DataSource = Nothing
        txtActualUnloading.EditValue = Nothing
        txtSpoilage.EditValue = Nothing
        txtTotalAmount.EditValue = Nothing
        txtAdjustments.EditValue = Nothing
        txtOverallTotalAmount.EditValue = Nothing
        txtAmountPaid.EditValue = Nothing
        txtAmountInPercentage.EditValue = Nothing
        txtRemainingBalance.EditValue = Nothing

        txtAdjustments.ReadOnly = True
        txtAmountPaid.ReadOnly = True

        conCarrier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conBacking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If val Is "Export" Then
            conContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf val Is "Backing" Then
            conBacking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    Private Sub BarCheckItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarCheckItem3.ItemClick
        setBarPartialItems()
    End Sub

    Private Sub BarCheckItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarCheckItem4.ItemClick
        setBarFinalItems()
    End Sub

    Private Sub BarCheckItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarCheckItem5.ItemClick
        setBarFinalItems()
    End Sub

    Sub setBarPartialItems()
        lueReport.EditValue = Nothing
        dtEncoded.EditValue = Nothing
        cmbSaleType.EditValue = Nothing
        lueCarrierInvoice.EditValue = Nothing
        lueCarrier.EditValue = Nothing
        cmbBuyer.EditValue = Nothing
        txtBuyer.EditValue = Nothing
        txtSetNo.EditValue = Nothing
        txtInvoiceNum.EditValue = Nothing
        txtActualUnloading.EditValue = Nothing
        txtSpoilage.EditValue = Nothing
        txtTotalAmount.EditValue = Nothing
        txtAdjustments.EditValue = Nothing
        txtOverallTotalAmount.EditValue = Nothing
        txtAmountPaid.EditValue = Nothing
        txtAmountInPercentage.EditValue = Nothing
        txtRemainingBalance.EditValue = Nothing

        lueCarrier.Properties.DataSource = Nothing
        GridControl1.DataSource = Nothing
        GridControl2.DataSource = Nothing

        conReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conCarrier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conBacking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conCarrierInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        dtEncoded.ReadOnly = False
        cmbSaleType.ReadOnly = False
        lueCarrierInvoice.ReadOnly = False
        lueCarrier.ReadOnly = False
        cmbBuyer.ReadOnly = False
        txtBuyer.ReadOnly = False
        txtSetNo.ReadOnly = False
        txtInvoiceNum.ReadOnly = False
        txtAdjustments.ReadOnly = True
        txtAmountPaid.ReadOnly = True
    End Sub

    Sub setBarFinalItems()
        Dim mkdb As New mkdbDataContext
        Dim reports = Nothing

        If BarCheckItem4.Checked Then
            reports = (From i In mkdb.trans_SalesInvoiceBuyers
                       Where i.paymentStatus = 2
                       Select New With {
                           .ID = i.salesInvoiceBuyerID,
                           .Value = i.salesInvoiceBuyerID & "-" & i.invoiceNum
                       }).ToList()
        Else
            reports = (From i In mkdb.trans_SalesInvoiceBuyers
                       Where i.paymentStatus = 3
                       Select New With {
                           .ID = i.salesInvoiceBuyerID,
                           .Value = i.salesInvoiceBuyerID & "-" & i.invoiceNum
                       }).ToList()
        End If

        lueReport.EditValue = Nothing
        dtEncoded.EditValue = Nothing
        cmbSaleType.EditValue = Nothing
        lueCarrierInvoice.EditValue = Nothing
        lueCarrier.EditValue = Nothing
        cmbBuyer.EditValue = Nothing
        txtBuyer.EditValue = Nothing
        txtSetNo.EditValue = Nothing
        txtInvoiceNum.EditValue = Nothing
        txtActualUnloading.EditValue = Nothing
        txtSpoilage.EditValue = Nothing
        txtTotalAmount.EditValue = Nothing
        txtAdjustments.EditValue = Nothing
        txtOverallTotalAmount.EditValue = Nothing
        txtAmountPaid.EditValue = Nothing
        txtAmountInPercentage.EditValue = Nothing
        txtRemainingBalance.EditValue = Nothing

        lueCarrier.Properties.DataSource = Nothing
        GridControl1.DataSource = Nothing
        GridControl2.DataSource = Nothing

        lookUpTransMode(reports, lueReport, "Value", "ID", "Select Report")

        conReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        conCarrier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conBacking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        conCarrierInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        dtEncoded.ReadOnly = True
        cmbSaleType.ReadOnly = True
        lueCarrierInvoice.ReadOnly = True
        lueCarrier.ReadOnly = True
        cmbBuyer.ReadOnly = True
        txtBuyer.ReadOnly = True
        txtSetNo.ReadOnly = True
        txtInvoiceNum.ReadOnly = True
        txtAdjustments.ReadOnly = True
        txtAmountPaid.ReadOnly = True
    End Sub

    Private Sub lueReport_EditValueChanged(sender As Object, e As EventArgs) Handles lueReport.EditValueChanged

    End Sub

    Private Sub txtTotalAmount_EditValueChanged(sender As Object, e As EventArgs) Handles txtTotalAmount.EditValueChanged
        txtOverallTotalAmount.EditValue = Math.Round(CDec(txtTotalAmount.EditValue), 2)
    End Sub

    Private Sub txtOverallTotalAmount_EditValueChanged(sender As Object, e As EventArgs) Handles txtOverallTotalAmount.EditValueChanged
        If CDec(txtAmountPaid.EditValue) = 0 Then
            txtRemainingBalance.EditValue = txtOverallTotalAmount.EditValue
        End If
    End Sub
End Class