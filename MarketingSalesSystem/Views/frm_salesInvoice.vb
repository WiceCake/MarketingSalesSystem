Imports DevExpress.XtraTab
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors
Imports System.Text
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.Data
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns

Public Class frm_salesInvoice

    Public dt As DataTable
    Public dts As DataTable
    Private tabControl As XtraTabControl
    Private txtNote As TextBox

    Private ctrlSales As ctrlSales

    Public buyerName As String
    Public checkBuyer As Boolean
    Public buyerID As Integer
    Public isPosted As Boolean = False
    Public rowCount As Integer

    Private foreignCarrier As Dictionary(Of Integer, String)

    Public confirmClose As Boolean = True

    Dim riLookup As New RepositoryItemLookUpEdit()
    Dim riTextEdit As New RepositoryItemTextEdit()

    Sub New(ByRef ctrlS As ctrlSales)
        InitializeComponent()
        ctrlSales = ctrlS

        gvCarrier.OptionsSelection.MultiSelect = True
        gvCarrier.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
    End Sub

    Sub createBands(catcherID As Integer)
        Dim mkdb As New mkdbDataContext

        ' Fetch all catchers in one go
        Dim catchers As List(Of Integer) = mkdb.trans_CatchActivityDetails.
            Where(Function(j) j.catchActivity_ID = catcherID).
            Select(Function(j) j.vessel_ID).ToList()

        Dim bandClass = AddBand("Class", BandedGridView3)
        Dim bandSize = AddBand("Size", BandedGridView3)
        Dim bandPrice = AddBand("Price", BandedGridView3)
        Dim bandAU = AddBand("Actual Unloading", BandedGridView3)
        Dim bandSpoilage = AddBand("Spoilage", BandedGridView3)
        Dim bandNet = AddBand("Net", BandedGridView3)
        ' Testing Trace

        ' ---- Actual Unloading ----
        Dim bandAUKilos = AddBand("Kilos", bandAU)
        Dim bandAUAmount = AddBand("Amount", bandAU)
        populateBand(bandAUKilos, catchers)
        Dim bandAUKTotal = AddBand("Total", bandAUKilos) ' Restored Total band
        populateBand(bandAUAmount, catchers)
        Dim bandAUATotal = AddBand("Total", bandAUAmount) ' Restored Total band

        ' ---- Spoilage ----
        Dim bandSKilos = AddBand("Kilos", bandSpoilage)
        Dim bandSAmount = AddBand("Amount", bandSpoilage)
        populateBand(bandSKilos, catchers)
        Dim bandSKTotal = AddBand("Total", bandSKilos) ' Restored Total band
        populateBand(bandSAmount, catchers)
        Dim bandSATotal = AddBand("Total", bandSAmount) ' Restored Total band

        ' ---- Net ----
        Dim bandNKilos = AddBand("Kilos", bandNet)
        Dim bandNAmount = AddBand("Amount", bandNet)
        populateBand(bandNKilos, catchers)
        Dim bandNKTotal = AddBand("Total", bandNKilos) ' Restored Total band
        populateBand(bandNAmount, catchers)
        Dim bandNATotal = AddBand("Total", bandNAmount) ' Restored Total band

        BandedGridView3.PopulateColumns()

        ' ---- Assign Columns to Bands ----
        With BandedGridView3

            .Columns("Class").OwnerBand = bandClass
            .Columns("Size").OwnerBand = bandSize
            .Columns("Price").OwnerBand = bandPrice
            setOwnerBand("AUK_Catcher", bandAUKilos)
            .Columns("AUK_Total").OwnerBand = bandAUKTotal
            setOwnerBand("AUA_Catcher", bandAUAmount, True)
            .Columns("AUA_Total").OwnerBand = bandAUATotal
            setOwnerBand("SK_Catcher", bandSKilos)
            .Columns("SK_Total").OwnerBand = bandSKTotal
            setOwnerBand("SA_Catcher", bandSAmount, True)
            .Columns("SA_Total").OwnerBand = bandSATotal
            setOwnerBand("NK_Catcher", bandNKilos, True)
            .Columns("NK_Total").OwnerBand = bandNKTotal
            setOwnerBand("NA_Catcher", bandNAmount, True)
            .Columns("NA_Total").OwnerBand = bandNATotal

            ' ---- Read-Only Settings ----
            .Columns("Class").OptionsColumn.ReadOnly = True
            .Columns("Size").OptionsColumn.ReadOnly = True
            .Columns("AUK_Total").OptionsColumn.ReadOnly = True
            .Columns("AUA_Total").OptionsColumn.ReadOnly = True
            .Columns("SK_Total").OptionsColumn.ReadOnly = True
            .Columns("SA_Total").OptionsColumn.ReadOnly = True
            .Columns("NK_Total").OptionsColumn.ReadOnly = True
            .Columns("NA_Total").OptionsColumn.ReadOnly = True
            .Columns("NA_Total").Summary.Add(SummaryItemType.Sum, "NA_Total", "Total: {0}")

            bandClass.Fixed = Columns.FixedStyle.Left
            bandSize.Fixed = Columns.FixedStyle.Left

            .OptionsView.ShowFooter = True

            ' ---- Align Headers & Columns ----
            For Each band As GridBand In .Bands
                SetHeaderAlignment(band)
            Next


            If isPosted Then
                For Each col As BandedGridColumn In .Columns
                    col.OptionsColumn.ReadOnly = True
                Next
            End If

            For Each col As BandedGridColumn In BandedGridView3.Columns
                col.Width = 100
            Next

            .BestFitColumns()
            .OptionsView.ColumnAutoWidth = False
            .OptionsView.ShowColumnHeaders = False
        End With
    End Sub


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

    Sub setOwnerBand(caption As String, parentBand As GridBand, Optional isReadOnly As Boolean = False)
        Dim countBand = 1
        For Each band As GridBand In parentBand.Children
            If band.Caption IsNot "Total" Then
                With BandedGridView3.Columns(caption & countBand)
                    .OwnerBand = band
                    .UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    If isReadOnly Then .OptionsColumn.ReadOnly = True
                    .Summary.Add(DevExpress.Data.SummaryItemType.Sum, caption & countBand, "Total: {0}")
                End With
            End If
            countBand = countBand + 1
        Next
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

    Private Sub BandedGridView3_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles BandedGridView3.CellValueChanged
        Dim view As BandedGridView = TryCast(sender, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
        If view Is Nothing Then
            Return
        End If

        If e.Column.FieldName = "AUK_Total" Or e.Column.FieldName = "AUA_Total" Or e.Column.FieldName = "SK_Total" _
            Or e.Column.FieldName = "SA_Total" Or e.Column.FieldName = "NK_Total" Or e.Column.FieldName = "NA_Total" Then
            Return
        End If

        If gvCarrier.RowCount = 0 Then Return

        ' Check if any Metric_Ton is empty
        Dim hasEmptyMetricTon As Boolean = False
        For i As Integer = 0 To gvCarrier.RowCount - 1
            Dim row As DataRow = gvCarrier.GetDataRow(i)
            If String.IsNullOrWhiteSpace(row("Metric_Ton").ToString()) Then
                hasEmptyMetricTon = True
                Exit For
            End If
        Next

        ' If there are empty Metric_Ton cells, revert the current cell to 0
        If hasEmptyMetricTon Then
            Dim changedRow As DataRow = view.GetDataRow(e.RowHandle)
            If changedRow IsNot Nothing Then
                changedRow(e.Column.FieldName) = 0
                XtraMessageBox.Show("Please encode metric tons for carries!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            Return
        End If

        Dim r As DataRowView = CType(view.GetRow(view.FocusedRowHandle), DataRowView)
        ctrlSales.updateTotal(r.Row)
    End Sub

    Private Sub btnSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSave.ItemClick
        'Dim displayFCarrier As Object = cmbFCarrier.Properties.GetDisplayText(cmbFCarrier.Properties.GetCheckedItems)
        'Dim displayCCarrier As Object = cmbCCarrier.Properties.GetCheckedItems()
        '
        'Debug.WriteLine(displayFCarrier)
        'Debug.WriteLine(displayCCarrier)

        confirmClose = False ' Prevent FormClosing interference
        Dim dateCreated = validateField(dtCreated)
        Dim sellType = validateField(cmbST)
        'ADD CONDITION FOR CATCHER
        Dim Catcher = validateField(cmbUV)
        Dim salesNum = validateField(txtSaleNum)
        Dim invoiceNum = validateField(txtInvoiceNum)
        Dim catchDeliveryNum = validateField(txtCDNum)
        Dim usdRate = validateField(txtUSD)
        Dim contractNum = validateField(txtCNum)
        Dim remark = validateField(txtRemark)

        Dim missingFields As New StringBuilder()
        If Not dateCreated Then missingFields.AppendLine("Date Created")
        If Not sellType Then missingFields.AppendLine("Sell Type")
        If Not Catcher Then missingFields.AppendLine("Unloading Vessel")
        If Not salesNum Then missingFields.AppendLine("Sales Number")
        If Not invoiceNum Then missingFields.AppendLine("Invoice Number")
        If Not catchDeliveryNum Then missingFields.AppendLine("Catch Delivery Number")
        If Not usdRate Then missingFields.AppendLine("USD Rate")
        If Not contractNum Then missingFields.AppendLine("Contract Number")

        Dim countEmpty = 0
        For i As Integer = 0 To gvCarrier.RowCount - 1
            Dim row As DataRow = gvCarrier.GetDataRow(i)

            If String.IsNullOrWhiteSpace(row("Carrier_Type").ToString()) OrElse String.IsNullOrWhiteSpace(row("Carrier_Name").ToString()) _
                OrElse String.IsNullOrWhiteSpace(row("Metric_Ton").ToString()) Then
                countEmpty = countEmpty + 1
            End If
        Next

        If countEmpty > 0 Then
            missingFields.AppendLine(countEmpty & " Carrier is Empty!")
        End If


        If missingFields.Length > 0 Then
            requiredMessage(missingFields.ToString())
            Return
        End If

        ctrlSales.saveDraft()
    End Sub

    Private Sub btnDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        confirmClose = False
        ctrlSales.deleteSales()
    End Sub

    Private Sub btnPost_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPost.ItemClick
        confirmClose = False
        If ConfirmPostedData() Then
            ctrlSales.postedDraft()
        End If
    End Sub


    Private Sub frm_salesInvoice_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If confirmClose Then
            If Not ConfirmCloseMessage() Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub cmbUV_EditValueChanged(sender As Object, e As EventArgs) Handles cmbUV.EditValueChanged
        BandedGridView3.Bands.Clear()
        Dim catcher = CType(sender, DevExpress.XtraEditors.LookUpEdit)

        ' Get the selected ID
        Dim selectedID As Integer = CInt(catcher.EditValue)

        MessageBox.Show("Selected ID: " & selectedID.ToString(), "Selected report ID", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Continue your original code
        ctrlSales.initSalesDataTable(selectedID)
        GridControl3.DataSource = dt
        createBands(selectedID)
        ctrlSales.loadRows()

        ' Get reference number
        Dim dc As New mkdbDataContext
        Dim getValue = (From i In dc.trans_CatchActivities
                        Where i.catchActivity_ID = selectedID
                        Select i.catchReferenceNum).Distinct().FirstOrDefault()

        txtCDNum.EditValue = getValue

        '' Get the salesReport_ID where catchtDeliveryNum = selectedID using LINQ
        'Dim salesReportID = (From c In dc.trans_SalesReports
        '                     Where c.catchtDeliveryNum = selectedID.ToString() And c.salesReport_ID = mdlSR.salesReport_ID
        '                     Select c.salesReport_ID).FirstOrDefault()

        '' Show the ID in a MessageBox
        'MessageBox.Show("Selected ID: " & salesReportID.ToString(), "Selected report ID", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub



    Private Sub bandedGridView_CustomDrawEmptyForeground(ByVal sender As Object, ByVal e As CustomDrawEventArgs) Handles BandedGridView3.CustomDrawEmptyForeground
        Dim view As BandedGridView = TryCast(sender, BandedGridView)
        If view.RowCount <> 0 Then
            Return
        End If
        Dim drawFormat As New StringFormat()
        drawFormat.LineAlignment = StringAlignment.Center
        drawFormat.Alignment = drawFormat.LineAlignment
        e.Graphics.DrawString("Select catcher to display rows in this grid.", e.Appearance.Font, SystemBrushes.ControlDark, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), drawFormat)
    End Sub

    Private Sub txtUSD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUSD.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtUSD_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUSD.KeyDown
        If e.KeyCode = Keys.Down Then
            If Val(txtUSD.Text) <= 0 Then
                e.SuppressKeyPress = True
            End If
        End If
    End Sub

    'Private Sub cmbCarrier_EditValueChanged(sender As Object, e As EventArgs) Handles cmbCarrier.EditValueChanged
    '    Dim value = TryCast(sender, CheckedComboBoxEdit)
    '    Debug.WriteLine(value.EditValue)
    'End Sub


    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        ctrlSales.print()
    End Sub

    Private Sub gcCarrier_Load(sender As Object, e As EventArgs) Handles gcCarrier.Load
        Dim cmb As New RepositoryItemComboBox()
        cmb.Items.AddRange(New Object() {"Foreign", "Company"})

        gcCarrier.RepositoryItems.Add(cmb)

        Dim dc As New tpmdbDataContext

        Dim carriers = (From i In dc.ml_Vessels Where i.ml_vtID = 1 Select New With {
                        .ID = i.ml_vID,
                        .VesselName = i.vesselName}).ToList()

        lookUpTransMode(carriers, riLookup, "VesselName", "ID", "Select Carrier")
        gcCarrier.RepositoryItems.Add(riLookup)
        gcCarrier.RepositoryItems.Add(riTextEdit)

        Dim gCT As GridColumn = gvCarrier.Columns("Carrier_Type")
        Dim gCN As GridColumn = gvCarrier.Columns("Carrier_Name")
        Dim gMT As GridColumn = gvCarrier.Columns("Metric_Ton")

        gCT.Caption = "Carrier Type"
        gCN.Caption = "Carrier Name"
        gMT.Caption = "Metric Ton"

        gCT.ColumnEdit = cmb

        Dim allRowsHaveCarrierType As Boolean = True

        For Each row As DataRow In dts.Rows
            If String.IsNullOrWhiteSpace(row("Carrier_Type").ToString()) Then
                allRowsHaveCarrierType = False
                Exit For
            End If
        Next

        If allRowsHaveCarrierType Then
            gCN.OptionsColumn.AllowEdit = True
            gMT.OptionsColumn.AllowEdit = True
        Else
            gCN.OptionsColumn.AllowEdit = False
            gMT.OptionsColumn.AllowEdit = False
        End If

        gMT.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
        AddHandler gvCarrier.CellValueChanged, AddressOf gvCarrier_CellValueChanged
        AddHandler gvCarrier.CustomRowCellEdit, AddressOf gvCarrier_CustomRowCellEdit
    End Sub

    Private Sub gvCarrier_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        If e.Column.FieldName = "Carrier_Type" Then

            ' Enable editing for current row only
            gvCarrier.Columns("Metric_Ton").OptionsColumn.AllowEdit = True
            gvCarrier.Columns("Carrier_Name").OptionsColumn.AllowEdit = True
        End If
    End Sub

    Private Sub gvCarrier_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        If e.Column.FieldName = "Carrier_Name" Then
            Dim val = gvCarrier.GetRowCellValue(e.RowHandle, "Carrier_Type").ToString()
            If val = "Foreign" Then
                e.RepositoryItem = riTextEdit ' Lookup for Foreign
            ElseIf val = "Company" Then
                e.RepositoryItem = riLookup ' Plain text for Company
            End If
        End If
    End Sub


    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles btnAddCarrier.Click
        ctrlSales.addCarrier()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles btnDeleteCarrier.Click

        If gvCarrier.SelectedRowsCount < 1 Then
            XtraMessageBox.Show("Please select a row first!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim totalRows As Integer = gvCarrier.RowCount
        Dim selectedRowCount As Integer = gvCarrier.SelectedRowsCount

        If totalRows = 1 Then
            XtraMessageBox.Show("Cannot delete this last row!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If selectedRowCount = totalRows Then
            XtraMessageBox.Show("At least one row must remain. Cannot delete all rows!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirmation = XtraMessageBox.Show("Are you sure you want to delete the selected rows?", APPNAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If confirmation = Windows.Forms.DialogResult.Yes Then
            Dim selectedRows As Integer() = gvCarrier.GetSelectedRows()

            For i As Integer = selectedRows.Length - 1 To 0 Step -1
                Dim rowHandle As Integer = selectedRows(i)
                Dim row As DataRow = gvCarrier.GetDataRow(rowHandle)

                If row IsNot Nothing Then dts.Rows.Remove(row)
            Next

            gcCarrier.DataSource = dts
            gvCarrier.RefreshData()
            gvCarrier.ClearSelection()
        End If
    End Sub

End Class