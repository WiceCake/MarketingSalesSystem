Imports System.Transactions
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Utils

Public Class ctrlBuyers
    Private isNew As Boolean

    Private mdlSIB As SalesInvoiceBuyer
    Private mdlSRB As SalesReportBuyer
    Private mdlSR As SalesReport

    Private frmBS As frm_buyerSales

    Private ucB As ucBuyer

    Private mkdb As mkdbDataContext
    Private tpmdb As tpmdbDataContext

    Sub New(uc As ucBuyer)
        isNew = True

        mkdb = New mkdbDataContext
        tpmdb = New tpmdbDataContext

        mdlSIB = New SalesInvoiceBuyer(mkdb)
        mdlSRB = New SalesReportBuyer(mkdb)
        mdlSR = New SalesReport(mkdb)

        frmBS = New frm_buyerSales(Me)

        ucB = uc

        'initKiloDataTable()

        frmBS.GridControl1.DataSource = frmBS.dt

        With frmBS
            If .rBuyer.SelectedIndex = 0 Then
                showTxtBuyer()
            End If

            .rbnAction.Visible = False

            SetLayoutVisibility(.conReport, False,
                          .conContainer, False,
                          .conBacking, False,
                          .conCarrierInvoice, False,
                          .conCarrier, False)

            SetBarVisibility(.btnDelete, False,
                             .btnPost, False)

            .dtEncoded.Properties.MaxValue = Date.Now()

            .txtAmountPaid.ReadOnly = True
            .txtAdjustments.ReadOnly = True

            loadComboxes()
            'loadDataRows()
            .Show()
        End With
    End Sub

    Sub New(ByRef uc As ucBuyer, ByVal sibID As Integer)
        isNew = False

        mkdb = New mkdbDataContext
        tpmdb = New tpmdbDataContext

        mdlSIB = New SalesInvoiceBuyer(sibID, mkdb)
        mdlSRB = New SalesReportBuyer(mkdb)
        mdlSR = New SalesReport(mkdb)

        frmBS = New frm_buyerSales(Me)

        ucB = uc

        frmBS.GridControl1.DataSource = frmBS.dt

        Dim reports = (From i In mkdb.trans_SalesInvoiceReports
                       Where i.salesInvoiceBuyer_ID = CInt(mdlSIB.salesInvoiceBuyerID)
                       Select i.previousReport_ID).FirstOrDefault()

        With frmBS
            .rbnTools.Visible = (mdlSIB.approvalStatus <> Approval_Status.Posted)
            .rbnAction.Visible = Not .rbnTools.Visible

            loadComboxes()
            .lueCarrierInvoice.ReadOnly = True
            .dtEncoded.EditValue = mdlSIB.encodedOn
            .cmbSaleType.EditValue = mdlSIB.sellerType
            .lueCarrierInvoice.EditValue = mdlSIB.salesInvoiceID
            .txtSetNo.EditValue = mdlSIB.setNum
            .txtRefNum.Caption = mdlSIB.referenceNum
            .txtInvoiceNum.EditValue = mdlSIB.invoiceNum
            .lueCarrier.EditValue = mdlSIB.carrier

            SetLayoutVisibility(.conReport, False)

            SetCheckboxVisibility(.barPartial, False,
                                  .barPartialFinal, False,
                                  .barFinal, False)

            SetBarEnabled(.barPartial, False,
                          .barPartialFinal, False,
                          .barFinal, False)

            SetBarVisibility(.barPartial, False,
                             .barPartialFinal, False,
                             .barFinal, False)

            Select Case mdlSIB.paymentStatus
                Case Payment_Status.Partial_
                    SetBarVisibility(.barPartial, True)
                Case Payment_Status.Partial_Final
                    SetBarVisibility(.barPartialFinal, True)
                    SetLayoutVisibility(.conReport, True)
                    .lueReport.EditValue = reports
                    loadPartialReport()
                Case Payment_Status.Final_
                    SetBarVisibility(.barFinal, True)
                    SetLayoutVisibility(.conReport, True)
                    .lueReport.EditValue = reports
            End Select

            .txtAmountPaid.EditValue = mdlSIB.paidAmount
            .txtAdjustments.EditValue = mdlSIB.adjustmentsAmount

            Dim isPosted As Boolean = (mdlSIB.approvalStatus = Approval_Status.Posted)

            If Not isPosted Then
                If Integer.TryParse(mdlSIB.buyerName, 0) Then
                    .rBuyer.SelectedIndex = 1
                    showCbBuyer()
                    .cmbBuyer.EditValue = mdlSIB.buyerName
                Else
                    showTxtBuyer()
                    .txtBuyer.EditValue = mdlSIB.buyerName
                End If
            Else
                .cmbBuyer.ReadOnly = True
                .txtBuyer.ReadOnly = True
                .dtEncoded.ReadOnly = True
                .cmbSaleType.ReadOnly = True
                .lueCarrierInvoice.ReadOnly = True
                .txtSetNo.ReadOnly = True
                .txtInvoiceNum.ReadOnly = True
                .lueCarrier.ReadOnly = True
                .lueReport.ReadOnly = True
                .txtAmountPaid.ReadOnly = True
                .txtAdjustments.ReadOnly = True

                ' Disable any layout controls if needed
                .conReport.Enabled = False

                ' Still show the correct control based on the type, but don't allow editing
                If Integer.TryParse(mdlSIB.buyerName, 0) Then
                    showCbBuyer()
                    .cmbBuyer.EditValue = mdlSIB.buyerName
                    .cmbBuyer.ReadOnly = True
                Else
                    showTxtBuyer()
                    .txtBuyer.EditValue = mdlSIB.buyerName
                    .txtBuyer.ReadOnly = True
                End If
                .rBuyer.Enabled = False
            End If

            .Show()
        End With
    End Sub

    Sub saveDraft()
        Using ts As New TransactionScope()
            Try
                With frmBS
                    mdlSIB.encodedOn = CDate(.dtEncoded.EditValue)
                    mdlSIB.sellerType = .cmbSaleType.EditValue.ToString
                    mdlSIB.salesInvoiceID = CInt(.lueCarrierInvoice.EditValue)
                    If .cmbSaleType.EditValue Is "Export" Then
                        mdlSIB.containerNum = .txtContainerNum.EditValue.ToString
                    ElseIf .cmbSaleType.EditValue Is "Backing" Then
                        mdlSIB.backing = CInt(.lueBacking.EditValue)
                    End If
                    If .barPartial.Checked Then
                        mdlSIB.paymentStatus = Payment_Status.Partial_
                    ElseIf .barPartialFinal.Checked Then
                        mdlSIB.paymentStatus = Payment_Status.Partial_Final
                    Else
                        mdlSIB.paymentStatus = Payment_Status.Final_
                    End If
                    If .rBuyer.SelectedIndex = 0 Then
                        mdlSIB.buyerName = .txtBuyer.Text
                    Else
                        mdlSIB.buyerName = .cmbBuyer.EditValue.ToString
                    End If
                    mdlSIB.setNum = .txtSetNo.EditValue.ToString
                    mdlSIB.adjustmentsAmount = CDec(.txtAdjustments.EditValue)
                    mdlSIB.carrier = CInt(.lueCarrier.EditValue)
                    mdlSIB.paidAmount = CDec(.txtAmountPaid.EditValue)
                    mdlSIB.invoiceNum = .txtInvoiceNum.EditValue.ToString
                    mdlSIB.encodedBy = "###"
                    mdlSIB.referenceNum = "Draft"
                    If isNew Then
                        mdlSIB.approvalStatus = Approval_Status.Draft
                        mdlSIB.Add()
                    Else
                        mdlSIB.approvalStatus = Approval_Status.Submitted
                        mdlSIB.Save()
                    End If

                    If .barPartialFinal.Checked Then
                        setInvoiceReport(mdlSIB.salesInvoiceBuyerID, CInt(.lueReport.EditValue))
                    ElseIf .barFinal.Checked Then
                        setInvoiceReport(mdlSIB.salesInvoiceBuyerID, CInt(.lueReport.EditValue))
                    End If
                End With



                setSalesPrice(mdlSIB.salesInvoiceBuyerID, mdlSIB.salesInvoiceID, "AUK_Catcher")
                setSalesPrice(mdlSIB.salesInvoiceBuyerID, mdlSIB.salesInvoiceID, "SK_Catcher", True)

                ts.Complete()
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Using
        frmBS.Close()
        ucB.loadGrid()
    End Sub

    Sub setInvoiceReport(ByVal salesInvoiceBuyerID As Integer, Optional ByVal previousReport As Integer = Nothing)
        Dim sir As New SalesInvoiceReport(mkdb)

        If Not isNew AndAlso Not mdlSIB.paymentStatus = Payment_Status.Partial_ Then
            Dim reps = (From i In mkdb.trans_SalesInvoiceReports
                        Where i.salesInvoiceBuyer_ID = salesInvoiceBuyerID AndAlso i.previousReport_ID = previousReport
                        Select i).FirstOrDefault()
            sir.salesInvoiceReport_ID = reps.salesInvoiceReport_ID
        End If

        sir.previousReport_ID = previousReport
        sir.salesInvoiceBuyer_ID = salesInvoiceBuyerID
        If isNew Then
            sir.Add()
        Else
            sir.Save()
        End If
    End Sub

    Sub setSalesPrice(ByVal salesInvoiceBuyerID As Integer, ByVal salesInvoiceID As Integer, colName As String, Optional isSkip As Boolean = False)
        Dim srb As New SalesReportBuyer(mkdb)
        Dim count As Integer
        Dim getSrb As List(Of Integer) = New List(Of Integer)

        If Not isNew Then
            getSrb = (From i In mkdb.trans_SalesReportBuyers Where i.salesInvoiceBuyerID = CInt(salesInvoiceBuyerID) Select i.salesBuyerCatchID).ToList()

            If isSkip Then
                getSrb = getSrb.Skip(getSrb.Count \ 2).ToList
            End If

            count = 0
        End If

        For Each cols As DataColumn In frmBS.dt.Columns

            If Not cols.Caption.Contains(colName) Then Continue For

            With srb
                .salesInvoiceBuyer_ID = salesInvoiceBuyerID
                .salesInvoice_ID = salesInvoiceID
                .skipjack0_300To0_499 = CDec(frmBS.dt.Rows(0)(cols))
                .skipjack0_500To0_999 = CDec(frmBS.dt.Rows(1)(cols))
                .skipjack1_0To1_39 = CDec(frmBS.dt.Rows(2)(cols))
                .skipjack1_4To1_79 = CDec(frmBS.dt.Rows(3)(cols))
                .skipjack1_8To2_49 = CDec(frmBS.dt.Rows(4)(cols))
                .skipjack2_5To3_49 = CDec(frmBS.dt.Rows(5)(cols))
                .skipjack3_5AndUP = CDec(frmBS.dt.Rows(6)(cols))
                .yellowfin0_300To0_499 = CDec(frmBS.dt.Rows(7)(cols))
                .yellowfin0_500To0_999 = CDec(frmBS.dt.Rows(8)(cols))
                .yellowfin1_0To1_49 = CDec(frmBS.dt.Rows(9)(cols))
                .yellowfin1_5To2_49 = CDec(frmBS.dt.Rows(10)(cols))
                .yellowfin2_5To3_49 = CDec(frmBS.dt.Rows(11)(cols))
                .yellowfin3_5To4_99 = CDec(frmBS.dt.Rows(12)(cols))
                .yellowfin5_0To9_99 = CDec(frmBS.dt.Rows(13)(cols))
                .yellowfin10AndUpGood = CDec(frmBS.dt.Rows(14)(cols))
                .yellowfin10AndUpDeformed = CDec(frmBS.dt.Rows(15)(cols))
                .bigeye0_500To0_999 = CDec(frmBS.dt.Rows(16)(cols))
                .bigeye1_0To1_49 = CDec(frmBS.dt.Rows(17)(cols))
                .bigeye1_5To2_49 = CDec(frmBS.dt.Rows(18)(cols))
                .bigeye2_5To3_49 = CDec(frmBS.dt.Rows(19)(cols))
                .bigeye3_5To4_99 = CDec(frmBS.dt.Rows(20)(cols))
                .bigeye5_0To9_99 = CDec(frmBS.dt.Rows(21)(cols))
                .bigeye10AndUP = CDec(frmBS.dt.Rows(22)(cols))
                .bonito = CDec(frmBS.dt.Rows(23)(cols))
                .fishmeal = CDec(frmBS.dt.Rows(24)(cols))
                If isNew Then
                    .Add()
                Else
                    .salesBuyerCatch_ID = CInt(getSrb(count))
                    .Save()
                    count += 1
                End If
            End With
        Next

    End Sub

    Sub deleteBuyer()
        Using ts As New TransactionScope
            Try
                mdlSRB.salesInvoiceBuyer_ID = mdlSIB.salesInvoiceBuyerID
                mdlSRB.Delete()
                mdlSIB.Delete()

                ts.Complete()
            Catch ex As Exception
                Debug.WriteLine("Error: " & ex.Message)
            End Try
        End Using
        ucB.loadGrid()
        frmBS.Close()
    End Sub

    Sub postBuyer()
        Using ts As New TransactionScope
            Try
                mdlSIB.approvalStatus = Approval_Status.Posted
                mdlSIB.Posted()

                If mdlSIB.paymentStatus = Payment_Status.Partial_ Then
                    setInvoiceReport(mdlSIB.salesInvoiceBuyerID)
                End If

                ts.Complete()
            Catch ex As Exception
                Debug.WriteLine("Error: " & ex.Message)
            End Try
        End Using
        ucB.loadGrid()
        frmBS.Close()
    End Sub

    'Sub loadDataRows()
    '    loadKiloRows()
    'End Sub

    Sub initDataTable(ByVal catchID As Integer) ' Comment
        Dim ca = (From i In mkdb.trans_CatchActivityDetails Where i.catchActivity_ID = catchID Select i).ToList()

        With frmBS
            .dt = New DataTable()

            .dt.Columns.Add("Class", GetType(String))
            .dt.Columns.Add("Size", GetType(String))
            .dt.Columns.Add("Price", GetType(Double))
            .dt.Columns.Add("AUK_Total", GetType(Double))
            addColumnDT(.dt, "AUK_Catcher", ca.Count)
            .dt.Columns.Add("AUA_Total", GetType(Double))
            addColumnDT(.dt, "AUA_Catcher", ca.Count)
            .dt.Columns.Add("SK_Total", GetType(Double))
            addColumnDT(.dt, "SK_Catcher", ca.Count)
            .dt.Columns.Add("SA_Total", GetType(Double))
            addColumnDT(.dt, "SA_Catcher", ca.Count)
        End With
    End Sub

    'Sub initSpoilageDataTable(ByVal catchID As Integer) ' Comment
    '    Dim ca = (From i In mkdb.trans_CatchActivityDetails Where i.catchActivity_ID = catchID Select i).ToList()

    '    With frmBS
    '        .dtS = New DataTable()

    '        .dtS.Columns.Add("Class", GetType(String))
    '        .dtS.Columns.Add("Size", GetType(String))
    '        .dtS.Columns.Add("Price", GetType(Double))
    '        .dtS.Columns.Add("Kilo_Total", GetType(Double))
    '        addColumnDT(.dtS, "K_Catcher", ca.Count)
    '        .dtS.Columns.Add("Amount_Total", GetType(Double))
    '        addColumnDT(.dtS, "A_Catcher", ca.Count)
    '    End With
    'End Sub

    Sub addColumnDT(dt As DataTable, caption As String, count As Integer)
        For i As Integer = 1 To count
            dt.Columns.Add(caption & i, GetType(Double))
        Next
    End Sub

    Sub updateTotal(ByRef r As DataRow)
        Dim Price As Decimal = CDec(If(IsDBNull(r("Price")), 0, r("Price")))

        Dim AUK_Total As Decimal = 0
        Dim AUA_Total As Decimal = 0
        Dim SK_Total As Decimal = 0
        Dim SA_Total As Decimal = 0

        For Each col As DataColumn In r.Table.Columns
            Dim colName As String = col.ColumnName

            If colName.StartsWith("AUK_Catcher") Then

                Dim kValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))

                AUK_Total += kValue

                Dim auaColumn As String = colName.Replace("AUK", "AUA")
                If r.Table.Columns.Contains(auaColumn) Then
                    r(auaColumn) = kValue * Price
                    AUA_Total += CDec(r(auaColumn))
                End If
            End If

            If colName.StartsWith("SK_Catcher") Then

                Dim aukColumn As String = colName.Replace("SK", "AUK")
                Dim skValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))
                Dim aukValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(aukColumn)), 0, r(aukColumn))))


                skValue = Math.Min(skValue, aukValue)
                r(colName) = skValue

                SK_Total += skValue

                Dim saColumn As String = colName.Replace("SK", "SA")
                If r.Table.Columns.Contains(saColumn) Then
                    r(saColumn) = skValue * Price
                    SA_Total += CDec(r(saColumn))
                End If
            End If
        Next

        ' Store final totals
        r("AUK_Total") = AUK_Total
        r("AUA_Total") = AUA_Total
        r("SK_Total") = SK_Total
        r("SA_Total") = SA_Total
    End Sub

    'Sub updateSpoilageTotal(ByRef r As DataRow)
    '    Dim Price As Decimal = CDec(If(IsDBNull(r("Price")), 0, r("Price")))

    '    Dim Kilo_Total As Decimal = 0
    '    Dim Amount_Total As Decimal = 0

    '    For Each col As DataColumn In r.Table.Columns
    '        Dim colName As String = col.ColumnName

    '        If colName.StartsWith("K_Catcher") Then

    '            Dim kValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))

    '            Kilo_Total += kValue

    '            Dim auaColumn As String = colName.Replace("K", "A")
    '            If r.Table.Columns.Contains(auaColumn) Then
    '                r(auaColumn) = kValue * Price
    '                Amount_Total += CDec(r(auaColumn))
    '            End If
    '        End If
    '    Next

    '    ' Store final totals
    '    r("Kilo_Total") = Kilo_Total
    '    r("Amount_Total") = Amount_Total
    'End Sub

    Sub updateAllTotals()
        For Each r As DataRow In frmBS.dt.Rows
            updateTotal(r)
        Next
    End Sub

    'Sub updateAllSpoilageTotals()
    '    For Each r As DataRow In frmBS.dtS.Rows
    '        updateSpoilageTotal(r)
    '    Next
    'End Sub

    Sub loadRows(salesReportID As Integer)
        Dim fishClasses As New Dictionary(Of String, String()) From {
               {"SKIPJACK", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.39", "1.4 - 1.79", "1.8 - 2.49", "2.5 - 3.49", "3.5 - UP"}},
               {"YELLOWFIN", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP GOOD", "10 - UP DEFORMED"}},
               {"BIGEYE", New String() {"0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP"}},
               {"BONITO", New String() {"ALL SIZES"}},
               {"FISHMEAL", New String() {"ALL SIZES"}}
           }

        Dim columns = frmBS.dt.Columns

        Dim priceColumns As List(Of String) = GetType(trans_SalesReportPrice).GetProperties().Select(Function(t) t.Name).ToList()
        priceColumns.RemoveAll(Function(c) c = "salesReportPrice_ID" OrElse c = "salesReport_ID")

        Dim srp = mkdb.trans_SalesReportPrices.
            Where(Function(sp) sp.salesReport_ID = salesReportID).
            FirstOrDefault()

        ' Preload Catch Data for faster lookup
        Dim kiloList = mkdb.trans_SalesReportCatchers.
            Where(Function(s) s.salesReport_ID = salesReportID).
            GroupBy(Function(s) s.catchActivityDetail_ID).
            Select(Function(s) s.Skip(0).FirstOrDefault()).ToList()

        Dim spoilageList = mkdb.trans_SalesReportCatchers.
            Where(Function(s) s.salesReport_ID = salesReportID).
            GroupBy(Function(s) s.catchActivityDetail_ID).
            Select(Function(s) s.Skip(1).FirstOrDefault()).ToList()

        'Debug.WriteLine(cdList.Count)

        Dim bList = mkdb.trans_SalesReportBuyers.
            Where(Function(s) s.salesInvoiceID = salesReportID).
            ToList()

        Dim baAUList As New List(Of trans_SalesReportBuyer)
        Dim baSList As New List(Of trans_SalesReportBuyer)

        If Not isNew Then
            bList = mkdb.trans_SalesReportBuyers.
            Where(Function(s) s.salesInvoiceID = salesReportID).
            Where(Function(s) s.salesInvoiceBuyerID = mdlSIB.salesInvoiceBuyerID).
            ToList()

            Dim halfCount As Integer = bList.Count \ 2

            baAUList = bList.Take(halfCount).ToList
            baSList = bList.Skip(halfCount).Take(halfCount).ToList
        End If

        Dim priceCount As Integer = 0

        For Each fishClass In fishClasses
            For Each size In fishClass.Value
                Dim priceColumn As String = priceColumns(priceCount)
                Dim propInfo = GetType(trans_SalesReportPrice).GetProperty(priceColumn)

                Dim dr As DataRow = frmBS.dt.NewRow()
                dr("Class") = fishClass.Key
                dr("Size") = size

                dr("Price") = CDec(propInfo.GetValue(srp, Nothing))

                Dim countAvailableCatch As Integer = 0
                Dim countKiloCatcher As Integer = 0
                Dim countSpoilageCatcher As Integer = 0
                Dim propCatch = GetType(trans_SalesReportCatcher).GetProperty(priceColumn)
                Dim propBuyer = GetType(trans_SalesReportBuyer).GetProperty(priceColumn)

                For Each col As DataColumn In columns
                    If col.ColumnName.Contains("AUK_Catcher") AndAlso Not isNew AndAlso bList.Count <> 0 Then
                        dr("AUK_Catcher" & (countKiloCatcher + 1)) = CDec(propBuyer.GetValue(baAUList(countKiloCatcher), Nothing))
                        countKiloCatcher += 1
                    ElseIf col.ColumnName.Contains("AUK_Catcher") Then
                        dr("AUK_Catcher" & (countKiloCatcher + 1)) = CDec(propCatch.GetValue(kiloList(countKiloCatcher), Nothing))
                        countKiloCatcher += 1
                    ElseIf col.ColumnName.Contains("SK_Catcher") AndAlso Not isNew AndAlso bList.Count <> 0 Then
                        dr("SK_Catcher" & (countSpoilageCatcher + 1)) = CDec(propBuyer.GetValue(baSList(countSpoilageCatcher), Nothing))
                        countSpoilageCatcher += 1
                    ElseIf col.ColumnName.Contains("SK_Catcher") Then
                        dr("SK_Catcher" & (countSpoilageCatcher + 1)) = CDec(propCatch.GetValue(spoilageList(countSpoilageCatcher), Nothing))
                        countSpoilageCatcher += 1
                    ElseIf col.ColumnName <> "Class" AndAlso col.ColumnName <> "Size" AndAlso col.ColumnName <> "Price" Then
                        dr(col) = 0
                    End If
                Next

                frmBS.dt.Rows.Add(dr)
                priceCount += 1
            Next
        Next
        updateAllTotals()

    End Sub

    'Sub loadSpoilageRows(salesReportID As Integer)
    '    Dim fishClasses As New Dictionary(Of String, String()) From {
    '           {"SKIPJACK", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.39", "1.4 - 1.79", "1.8 - 2.49", "2.5 - 3.49", "3.5 - UP"}},
    '           {"YELLOWFIN", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP GOOD", "10 - UP DEFORMED"}},
    '           {"BIGEYE", New String() {"0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP"}},
    '           {"BONITO", New String() {"ALL SIZES"}},
    '           {"FISHMEAL", New String() {"ALL SIZES"}}
    '       }

    '    Dim columns = frmBS.dt.Columns

    '    Dim priceColumns As List(Of String) = GetType(trans_SalesReportPrice).GetProperties().Select(Function(t) t.Name).ToList()
    '    priceColumns.RemoveAll(Function(c) c = "salesReportPrice_ID" OrElse c = "salesReport_ID")

    '    Dim srp = mkdb.trans_SalesReportPrices.
    '        Where(Function(sp) sp.salesReport_ID = salesReportID).
    '        FirstOrDefault()

    '    ' Preload Catch Data for faster lookup
    '    Dim cdList = mkdb.trans_SalesReportCatchers.
    '        Where(Function(s) s.salesReport_ID = salesReportID).
    '        GroupBy(Function(s) s.catchActivityDetail_ID).
    '        Select(Function(s) s.Skip(1).FirstOrDefault()).ToList()

    '    Dim priceCount As Integer = 0

    '    For Each fishClass In fishClasses
    '        For Each size In fishClass.Value
    '            Dim priceColumn As String = priceColumns(priceCount)
    '            Dim propInfo = GetType(trans_SalesReportPrice).GetProperty(priceColumn)

    '            Dim dr As DataRow = frmBS.dt.NewRow()
    '            dr("Class") = fishClass.Key
    '            dr("Size") = size

    '            dr("Price") = CDec(propInfo.GetValue(srp, Nothing))

    '            Dim countSpoilageCatcher As Integer = 0
    '            Dim propCatch = GetType(trans_SalesReportCatcher).GetProperty(priceColumn)

    '            For Each col As DataColumn In columns
    '                If col.ColumnName.Contains("K_Catcher") Then
    '                    dr("K_Catcher" & (countSpoilageCatcher + 1)) = CDec(propCatch.GetValue(cdList(countSpoilageCatcher), Nothing))
    '                    countSpoilageCatcher += 1
    '                ElseIf col.ColumnName <> "Class" AndAlso col.ColumnName <> "Size" AndAlso col.ColumnName <> "Price" Then
    '                    dr(col) = 0
    '                End If
    '            Next

    '            frmBS.dt.Rows.Add(dr)
    '            priceCount += 1
    '        Next
    '    Next
    '    updateAllTotals()

    'End Sub

    Sub showTxtBuyer()
        With frmBS
            .lciTxtBuyer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            .lciCbBuyer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End With
    End Sub

    Sub showCbBuyer()
        With frmBS
            .lciCbBuyer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            .lciTxtBuyer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End With
    End Sub

    Sub loadComboxes()
        loadTypeOfSeller()
        loadInvoice()
        loadBuyer()
    End Sub

    Sub loadTypeOfSeller()
        Dim tos = New List(Of String) From {"Canning", "Backing", "Export", "Local"}

        frmBS.cmbSaleType.Properties.Items.AddRange(tos)
    End Sub

    Sub loadInvoice()
        Dim sr = From i In mkdb.trans_SalesReports Where i.approvalStatus = 1 Select New With {
                 .ID = i.salesReport_ID,
                 .Invoice = i.invoiceNum}

        lookUpTransMode(sr, frmBS.lueCarrierInvoice, "Invoice", "ID", "Select Invoice")
    End Sub

    Sub loadBuyer()
        Dim b = From i In tpmdb.ML_SULLIER_20170329s Select New With {
                .ID = i.ml_SupID,
                .Name = i.ml_Supplier}

        lookUpTransMode(b, frmBS.cmbBuyer, "Name", "ID", "Select Buyer")
    End Sub

    Sub loadPartialReport()

        Dim pr = (From i In mkdb.trans_SalesInvoiceReports Join
                 j In mkdb.trans_SalesInvoiceBuyers On i.salesInvoiceBuyer_ID Equals j.salesInvoiceBuyerID
                 Where j.paymentStatus = Payment_Status.Partial_
                 Where j.approvalStatus = Approval_Status.Posted
                 Select New With {
                     .ID = i.salesInvoiceBuyer_ID,
                     .Value = j.invoiceNum}).ToList

        lookUpTransMode(pr, frmBS.lueReport, "Value", "ID", "Select Previous Report")
    End Sub

    Sub loadPartialFinalReport()

        Dim pr = (From i In mkdb.trans_SalesInvoiceReports Join
                 j In mkdb.trans_SalesInvoiceBuyers On i.salesInvoiceBuyer_ID Equals j.salesInvoiceBuyerID
                 Where j.paymentStatus = Payment_Status.Partial_Final
                 Where j.approvalStatus = Approval_Status.Posted
                 Select New With {
                     .ID = i.salesInvoiceBuyer_ID,
                     .Value = j.invoiceNum}).ToList

        lookUpTransMode(pr, frmBS.lueReport, "Value", "ID", "Select Previous Report")
    End Sub

    Sub print()
        Dim tool As ReportPrintTool

        Dim rp = New rptPartialSummaryReport()
        rp.DataSource = getPartialSalesInvoice(mdlSIB.salesInvoiceID, mdlSIB.setNum)
        tool = New ReportPrintTool(rp)
        tool.ShowPreviewDialog()
    End Sub

End Class
