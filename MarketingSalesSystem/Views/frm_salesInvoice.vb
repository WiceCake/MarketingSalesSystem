Imports DevExpress.XtraTab
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors
Imports System.Text

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

    Public confirmClose As Boolean = True

    Sub New(ByRef ctrlS As ctrlSales)
        InitializeComponent()
        ctrlSales = ctrlS
    End Sub

    Private Sub RadioGroup1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rBT.SelectedIndexChanged
        If rBT.SelectedIndex = 0 Then
            ctrlSales.changeBuyerInput()
        Else
            ctrlSales.changeBuyerCombo()
        End If
    End Sub


    Private Sub GridControl1_Load(sender As Object, e As EventArgs) Handles GridControl1.Load

    End Sub

    Sub createBands(count As Integer, catcherID As Integer)
        Dim mkdb As New mkdbDataContext

        ' Fetch all catchers in one go
        Dim catchers As List(Of Integer) = mkdb.trans_CatchActivityDetails.
            Where(Function(j) j.catchActivity_ID = catcherID).
            Select(Function(j) j.vessel_ID).ToList()

        Dim bandClass = AddBand("Class", BandedGridView1)
        Dim bandSize = AddBand("Size", BandedGridView1)
        Dim bandPrice = AddBand("Price", BandedGridView1)
        Dim bandAU = AddBand("Actual Unloading", BandedGridView1)
        Dim bandSpoilage = AddBand("Spoilage", BandedGridView1)
        Dim bandNet = AddBand("Net", BandedGridView1)
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

        ' ---- Assign Columns to Bands ----
        With BandedGridView1
            .PopulateColumns()

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

            bandClass.Fixed = Columns.FixedStyle.Left
            bandSize.Fixed = Columns.FixedStyle.Left

            .OptionsView.ShowFooter = True

            ' ---- Align Headers & Columns ----
            For Each band As DevExpress.XtraGrid.Views.BandedGrid.GridBand In BandedGridView1.Bands
                SetHeaderAlignment(band)
            Next

            For Each col As BandedGridColumn In BandedGridView1.Columns
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
                With BandedGridView1.Columns(caption & countBand)
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

    Private Sub BandedGridView1_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles BandedGridView1.CellValueChanged
        Dim view As BandedGridView = TryCast(sender, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
        If view Is Nothing Then
            Return
        End If
        If e.Column.FieldName = "AUK_Total" Or e.Column.FieldName = "AUA_Total" Or e.Column.FieldName = "SK_Total" _
            Or e.Column.FieldName = "SA_Total" Or e.Column.FieldName = "NK_Total" Or e.Column.FieldName = "NA_Total" Then
            Return
        End If

        Dim r As DataRowView = CType(view.GetRow(view.FocusedRowHandle), DataRowView)
        ctrlSales.updateTotal(r.Row)
    End Sub

    Private Sub GridControl2_Load(sender As Object, e As EventArgs) Handles GridControl2.Load
        Dim bandCatcher = AddBand("Catcher", BandedGridView2)
        Dim bandTonnage = AddBand("Tonnage", BandedGridView2)
        Dim bandKilos = AddBand("Kilos", BandedGridView2)
        Dim bandAmount = AddBand("Amount", BandedGridView2)

        Dim bandCCP = AddBand("Catcher Partial", bandTonnage)
        Dim bandCAQ = AddBand("Actual Qty", bandTonnage)

        Dim bandKAQ = AddBand("Actual Qty", bandKilos)
        Dim bandKF = AddBand("FishMeal", bandKilos)
        Dim bandKS = AddBand("Spoilage", bandKilos)
        Dim bandKN = AddBand("Net", bandKilos)

        Dim bandAAQ = AddBand("Actual Qty", bandAmount)
        Dim bandAF = AddBand("FishMeal", bandAmount)
        Dim bandAS = AddBand("Spoilage", bandAmount)
        Dim bandAN_USD = AddBand("Net In USD", bandAmount)
        Dim bandAN_PHP = AddBand("Net In PHP", bandAmount)
        Dim bandAAP = AddBand("Average Price per Catcher", bandAmount)


        With BandedGridView2
            .PopulateColumns()

            .Columns("Catcher").OwnerBand = bandCatcher
            .Columns("T_CatcherPartial").OwnerBand = bandTonnage
            .Columns("T_ActualQty").OwnerBand = bandTonnage
            .Columns("K_ActualQty").OwnerBand = bandKilos
            .Columns("K_Fishmeal").OwnerBand = bandKilos
            .Columns("K_Spoilage").OwnerBand = bandKilos
            .Columns("K_Net").OwnerBand = bandKilos
            .Columns("A_ActualQty").OwnerBand = bandAmount
            .Columns("A_Fishmeal").OwnerBand = bandAmount
            .Columns("A_Spoilage").OwnerBand = bandAmount
            .Columns("A_NetUSD").OwnerBand = bandAmount
            .Columns("A_NetPHP").OwnerBand = bandAmount
            .Columns("A_AveragePrice").OwnerBand = bandAmount

        End With

        BandedGridView2.OptionsView.ShowFooter = True

        For Each band As DevExpress.XtraGrid.Views.BandedGrid.GridBand In BandedGridView2.Bands
            SetHeaderAlignment(band)
        Next

        With BandedGridView2
            .Columns("T_ActualQty").UnboundType = DevExpress.Data.UnboundColumnType.Decimal
            .Columns("K_ActualQty").UnboundType = DevExpress.Data.UnboundColumnType.Decimal
            .Columns("K_Net").UnboundType = DevExpress.Data.UnboundColumnType.Decimal
            .Columns("A_ActualQty").UnboundType = DevExpress.Data.UnboundColumnType.Decimal
            .Columns("A_NetUSD").UnboundType = DevExpress.Data.UnboundColumnType.Decimal
            .Columns("A_NetPHP").UnboundType = DevExpress.Data.UnboundColumnType.Decimal
            .Columns("A_AveragePrice").UnboundType = DevExpress.Data.UnboundColumnType.Decimal

            .BestFitColumns()
            .OptionsView.ColumnAutoWidth = False
            .OptionsView.ShowColumnHeaders = False
        End With
    End Sub

    Private Sub btnSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSave.ItemClick
        confirmClose = False ' Prevent FormClosing interference
        Dim dateCreated = validateField(dtCreated)
        Dim sellType = validateField(cmbST)
        Dim unloadingVessel = validateField(cmbUV)
        Dim salesNum = validateField(txtSaleNum)
        Dim catchDeliveryNum = validateField(txtCDNum)
        Dim usdRate = validateField(txtUSD)
        Dim contactNum = validateField(txtCNum)
        Dim remark = validateField(txtRemark)

        Dim missingFields As New StringBuilder()
        If Not dateCreated Then missingFields.AppendLine("Date Created")
        If Not sellType Then missingFields.AppendLine("Sell Type")
        If Not unloadingVessel Then missingFields.AppendLine("Unloading Vessel") 'Unloading Vessel
        If Not salesNum Then missingFields.AppendLine("Sales Number")
        If Not catchDeliveryNum Then missingFields.AppendLine("Catch Delivery Number")
        If Not usdRate Then missingFields.AppendLine("USD Rate")
        If Not contactNum Then missingFields.AppendLine("Contact Number")
        If Not remark Then missingFields.AppendLine("Remarks")

        If CInt(rBT.EditValue) = 1 Then
            If validateField(txtBuyer) Then buyerName = txtBuyer.Text : buyerID = Nothing _
                Else missingFields.AppendLine("Buyer")
        ElseIf CInt(rBT.EditValue) = 2 Then
            If validateField(cmbBuyer) Then buyerName = cmbBuyer.Text : buyerID = CInt(cmbBuyer.GetColumnValue("ID")) _
                Else missingFields.AppendLine("Buyer")
        Else
            missingFields.AppendLine("Select Type of Buyer")
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
        BandedGridView1.Bands.Clear()
        Dim catcher = CType(sender, DevExpress.XtraEditors.LookUpEdit)
        'Debug.WriteLine(catcher.EditValue)
        ctrlSales.initSalesDataTable(CInt(catcher.EditValue))
        GridControl1.DataSource = dt
        createBands(rowCount, CInt(catcher.EditValue))
        ctrlSales.loadRows()

        ' Get reference number
        Dim dc As New mkdbDataContext
        Dim getValue = (From i In dc.trans_CatchActivities
                        Where i.catchActivity_ID = CInt(catcher.EditValue)
                        Select i.catchReferenceNum).Distinct().FirstOrDefault

        'Debug.WriteLine(CInt(catcher.EditValue))
        txtCDNum.EditValue = getValue
    End Sub

End Class