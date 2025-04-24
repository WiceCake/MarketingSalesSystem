Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports System.Transactions
Imports DevExpress.XtraReports.UI

Public Class ctrlSales

    Private isNew As Boolean
    Private mdlSR As SalesReport
    Private mdlSRP As SalesReportPrice
    Private mdlSRC As SalesReportCatcher

    Private frmSI As frm_salesInvoice

    Private ucS As ucSales

    Private mkdb As mkdbDataContext
    Private tpmdb As tpmdbDataContext

    Sub New(ByRef uc As ucSales)
        isNew = True

        mkdb = New mkdbDataContext
        tpmdb = New tpmdbDataContext

        mdlSR = New SalesReport(mkdb)
        mdlSRP = New SalesReportPrice(mkdb)

        frmSI = New frm_salesInvoice(Me)

        'initSalesDataTable()
        initCarrierDataTable()

        frmSI.GridControl1.DataSource = frmSI.dt
        frmSI.gcCarrier.DataSource = frmSI.dts

        ucS = uc

        With frmSI
            .Text = "Create Sales Invoice"
            .btnPost.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            .btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            .dtCreated.Properties.MaxValue = Date.Now
            .txtCDNum.Properties.ReadOnly = True
            .txt_refNum.Caption = "Draft"
            .rbnTools.Visible = False
            loadCombo()
            loadCarrier()
            .Show()
        End With
    End Sub

    Sub New(ByRef uc As ucSales, ByVal salesID As Integer)
        isNew = False

        mkdb = New mkdbDataContext
        tpmdb = New tpmdbDataContext

        mdlSR = New SalesReport(salesID, mkdb)
        mdlSRP = New SalesReportPrice(mkdb)
        mdlSRC = New SalesReportCatcher(mkdb)

        frmSI = New frm_salesInvoice(Me)

        'initSalesDataTable()
        initCarrierDataTable()

        frmSI.GridControl1.DataSource = frmSI.dt
        frmSI.gcCarrier.DataSource = frmSI.dts

        ucS = uc

        With frmSI
            .rbnTools.Visible = False
            .gvCarrier.OptionsSelection.MultiSelect = False
            If mdlSR.approvalStatus = Approval_Status.Posted Then
                .rbnActions.Visible = False
                .isPosted = True
                .rbnTools.Visible = True
            End If

            .Text = "Sales Invoice"
            .dtCreated.Properties.MaxValue = Date.Now
            loadCombo()
            loadCarrier()
            .cmbUV.Properties.ReadOnly = True
            .txtCDNum.Properties.ReadOnly = True
            .btnAddCarrier.Enabled = False
            .btnDeleteCarrier.Enabled = False

            'Fields
            .dtCreated.EditValue = mdlSR.salesDate
            .cmbST.EditValue = mdlSR.sellingType
            .txtSaleNum.EditValue = mdlSR.salesNum
            .txtInvoiceNum.EditValue = mdlSR.invoiceNum
            .cmbUV.EditValue = mdlSR.catchtDeliveryNum

            .txtUSD.EditValue = mdlSR.usdRate
            .txtCNum.EditValue = mdlSR.contractNum
            .txtRemark.EditValue = mdlSR.remarks
            .txt_refNum.Caption = mdlSR.referenceNum

            .Show()
        End With
    End Sub

    Sub initSalesDataTable(ByVal catchID As Integer)
        Dim ca = (From i In mkdb.trans_CatchActivityDetails Where i.catchActivity_ID = catchID Select i).ToList()

        With frmSI
            .dt = New DataTable()

            .dt.Columns.Add("Class", GetType(String))
            .dt.Columns.Add("Size", GetType(String))
            .dt.Columns.Add("Price", GetType(Double))
            addColumnDT(.dt, "AUK_Catcher", ca.Count)
            .dt.Columns.Add("AUK_Total", GetType(Double))
            addColumnDT(.dt, "AUA_Catcher", ca.Count)
            .dt.Columns.Add("AUA_Total", GetType(Double))
            addColumnDT(.dt, "SK_Catcher", ca.Count)
            .dt.Columns.Add("SK_Total", GetType(Double))
            addColumnDT(.dt, "SA_Catcher", ca.Count)
            .dt.Columns.Add("SA_Total", GetType(Double))
            addColumnDT(.dt, "NK_Catcher", ca.Count)
            .dt.Columns.Add("NK_Total", GetType(Double))
            addColumnDT(.dt, "NA_Catcher", ca.Count)
            .dt.Columns.Add("NA_Total", GetType(Double))
            .rowCount = ca.Count
        End With
    End Sub

    Sub initCarrierDataTable()
        With frmSI
            .dts = New DataTable()

            .dts.Columns.Add("Carrier_Type", GetType(String))
            .dts.Columns.Add("Carrier_Name", GetType(String))
            .dts.Columns.Add("Metric_Ton", GetType(Double))
        End With
    End Sub

    Sub addCarrier()
        Dim dr As DataRow = frmSI.dts.NewRow()

        dr("Carrier_Type") = ""

        frmSI.dts.Rows.Add(dr)
    End Sub

    Sub loadCarrier()
        Dim dr As DataRow = frmSI.dts.NewRow()

        If Not isNew Then
            Dim cdList = (From i In mkdb.trans_SalesUnloadeds Where i.SalesReportID = mdlSR.salesReport_ID Select i).ToList

            For Each cd In cdList
                dr = frmSI.dts.NewRow()
                dr("Carrier_Type") = cd.CarrierType
                dr("Carrier_Name") = cd.CarrierName
                dr("Metric_Ton") = cd.UnloadedValue
                frmSI.dts.Rows.Add(dr)
            Next
            Return
        End If

        dr("Carrier_Name") = ""
        frmSI.dts.Rows.Add(dr)
    End Sub

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
        Dim NK_Total As Decimal = 0
        Dim NA_Total As Decimal = 0

        Dim totalAUK_Catcher As Decimal = 0
        Dim totalMetricTon As Decimal = frmSI.dts.AsEnumerable().Sum(Function(row) CDec(If(IsDBNull(row("Metric_Ton")), 0, row("Metric_Ton"))))

        For Each row As DataRow In frmSI.dt.Rows
            If row IsNot r Then
                For Each col As DataColumn In row.Table.Columns
                    Dim colName As String = col.ColumnName

                    If colName.StartsWith("AUK_Catcher") Then
                        Dim catcherValue As Decimal = Math.Max(0, CDec(If(IsDBNull(row(colName)), 0, row(colName))))

                        totalAUK_Catcher += catcherValue
                    End If
                Next
            End If
        Next

        For Each col As DataColumn In r.Table.Columns
            Dim colName As String = col.ColumnName

            If colName.StartsWith("AUK_Catcher") Then
                Dim catcherValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))

                totalAUK_Catcher += catcherValue

                If totalAUK_Catcher > totalMetricTon Then
                    Dim excess As Decimal = totalAUK_Catcher - totalMetricTon
                    catcherValue -= excess

                    If catcherValue < 0 Then catcherValue = 0

                    r(colName) = catcherValue

                    totalAUK_Catcher = totalMetricTon
                End If
                Debug.WriteLine(catcherValue)
                AUK_Total += catcherValue
            End If
        Next

        For Each col As DataColumn In r.Table.Columns
            Dim colName As String = col.ColumnName

            If colName.StartsWith("AUK_Catcher") Then
                Dim auaColumn As String = colName.Replace("AUK", "AUA")
                Dim aukValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))

                If r.Table.Columns.Contains(auaColumn) Then
                    r(auaColumn) = aukValue * Price
                    AUA_Total += CDec(r(auaColumn))
                End If
            End If
        Next

        For Each col As DataColumn In r.Table.Columns
            Dim colName As String = col.ColumnName

            If colName.StartsWith("SK_Catcher") Then
                Dim aukColumn As String = colName.Replace("SK", "AUK")
                Dim skValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))
                Dim aukValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(aukColumn)), 0, r(aukColumn))))

                skValue = Math.Min(skValue, aukValue)
                r(colName) = skValue

                SK_Total += skValue
            End If
        Next

        For Each col As DataColumn In r.Table.Columns
            Dim colName As String = col.ColumnName

            If colName.StartsWith("SK_Catcher") Then
                Dim saColumn As String = colName.Replace("SK", "SA")
                Dim skValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))

                If r.Table.Columns.Contains(saColumn) Then
                    r(saColumn) = skValue * Price
                    SA_Total += CDec(r(saColumn))
                End If
            End If
        Next

        For Each col As DataColumn In r.Table.Columns
            Dim colName As String = col.ColumnName

            If colName.StartsWith("AUK_Catcher") Then
                Dim skColumn As String = colName.Replace("AUK", "SK")
                Dim nkColumn As String = colName.Replace("AUK", "NK")

                Dim aukValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))
                Dim skValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(skColumn)), 0, r(skColumn))))

                If r.Table.Columns.Contains(nkColumn) Then
                    r(nkColumn) = Math.Max(0, aukValue - skValue)
                    NK_Total += CDec(r(nkColumn))
                End If
            ElseIf colName.StartsWith("AUA_Catcher") Then
                Dim saColumn As String = colName.Replace("AUA", "SA")
                Dim naColumn As String = colName.Replace("AUA", "NA")

                Dim auaValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))
                Dim saValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(saColumn)), 0, r(saColumn))))

                If r.Table.Columns.Contains(naColumn) Then
                    r(naColumn) = Math.Max(0, auaValue - saValue)
                    NA_Total += CDec(r(naColumn))
                End If
            End If
        Next

        ' Update row totals
        r("AUK_Total") = AUK_Total
        r("AUA_Total") = AUA_Total
        r("SK_Total") = SK_Total
        r("SA_Total") = SA_Total
        r("NK_Total") = NK_Total
        r("NA_Total") = NA_Total
    End Sub

    Sub updateAllTotals()
        For Each r As DataRow In frmSI.dt.Rows
            updateTotal(r)
        Next
    End Sub

    Sub saveDraft()
        'Debug.WriteLine(frmSI.cmbFCarrier.Properties.GetDisplayText(frmSI.cmbFCarrier.Properties.GetCheckedItems).ToString)
        'Return
        Using ts As New TransactionScope()
            Try

                With frmSI
                    mdlSR.salesDate = CDate(.dtCreated.EditValue)
                    mdlSR.salesNum = .txtSaleNum.Text
                    mdlSR.invoiceNum = .txtInvoiceNum.Text
                    mdlSR.sellingType = .cmbST.EditValue.ToString
                    mdlSR.unloadingType = "###"
                    mdlSR.buyer = "###"
                    mdlSR.unloadingForeignVessel = "###"
                    mdlSR.unloadingVessel_ID = "###"
                    mdlSR.catchtDeliveryNum = .cmbUV.EditValue.ToString
                    mdlSR.usdRate = CDec(.txtUSD.EditValue)
                    mdlSR.contractNum = .txtCNum.Text
                    mdlSR.remarks = .txtRemark.Text
                    mdlSR.encodedBy = 1
                    If Not isNew Then
                        mdlSR.approvalStatus = Approval_Status.Submitted
                        mdlSR.encodedOn = Date.Now
                        mdlSR.Save()

                        Debug.WriteLine("saved post...")
                        SuccessfullyAddedUpdatedMessage()
                    Else
                        mdlSR.approvalStatus = Approval_Status.Draft
                        mdlSR.encodedOn = CDate(.dtCreated.EditValue)
                        mdlSR.Add()

                        Debug.WriteLine("add post...")
                        SuccessfullySavedMessage()
                    End If
                End With

                setSalesPrice(mdlSR.salesReport_ID)
                setCatcherPrice("AUK_Catcher", mdlSR.salesReport_ID)
                setCatcherPrice("SK_Catcher", mdlSR.salesReport_ID)
                setCarrierData(mdlSR.salesReport_ID)

                ts.Complete()
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Using
        ucS.loadGridCatcher()
        ucS.loadGridBuyer()
        frmSI.Close()
    End Sub


    Sub deleteSales()
        ' Confirm deletion with the user
        If ConfirmDeleteMessage() = True Then
            Using ts As New TransactionScope
                Try
                    mdlSRP.salesReport_ID = mdlSR.salesReport_ID
                    mdlSRC.salesReport_ID = mdlSR.salesReport_ID
                    mdlSRC.Delete()
                    mdlSRP.Delete()
                    mdlSR.Delete()

                    ts.Complete()
                Catch ex As Exception
                    Debug.WriteLine("Error: " & ex.Message)
                End Try
            End Using
        End If
        ucS.loadGridBuyer()
        ucS.loadGridCatcher()
        frmSI.Close()
    End Sub

    Sub postedDraft()
        Using ts As New TransactionScope
            Try
                With mdlSR
                    .approvalStatus = Approval_Status.Posted
                    .Posted()
                End With

                ts.Complete()
            Catch ex As Exception
                Debug.WriteLine("Error: " & ex.Message)
            End Try
        End Using
        ucS.loadGridCatcher()
        ucS.loadGridBuyer()
        frmSI.Close()
    End Sub

    Sub setSalesPrice(ByVal salesReportID As Integer)
        Dim srp As New SalesReportPrice(mkdb)

        If Not isNew Then
            Dim getSrp = (From i In mkdb.trans_SalesReportPrices Where i.salesReport_ID = salesReportID Select i.salesReportPrice_ID).FirstOrDefault
            srp = New SalesReportPrice(getSrp, mkdb)
        End If

        With srp
            .salesReport_ID = salesReportID
            .skipjack0_300To0_499 = CDec(frmSI.dt.Rows(0)("Price"))
            .skipjack0_500To0_999 = CDec(frmSI.dt.Rows(1)("Price"))
            .skipjack1_0To1_39 = CDec(frmSI.dt.Rows(2)("Price"))
            .skipjack1_4To1_79 = CDec(frmSI.dt.Rows(3)("Price"))
            .skipjack1_8To2_49 = CDec(frmSI.dt.Rows(4)("Price"))
            .skipjack2_5To3_49 = CDec(frmSI.dt.Rows(5)("Price"))
            .skipjack3_5AndUP = CDec(frmSI.dt.Rows(6)("Price"))
            .yellowfin0_300To0_499 = CDec(frmSI.dt.Rows(7)("Price"))
            .yellowfin0_500To0_999 = CDec(frmSI.dt.Rows(8)("Price"))
            .yellowfin1_0To1_49 = CDec(frmSI.dt.Rows(9)("Price"))
            .yellowfin1_5To2_49 = CDec(frmSI.dt.Rows(10)("Price"))
            .yellowfin2_5To3_49 = CDec(frmSI.dt.Rows(11)("Price"))
            .yellowfin3_5To4_99 = CDec(frmSI.dt.Rows(12)("Price"))
            .yellowfin5_0To9_99 = CDec(frmSI.dt.Rows(13)("Price"))
            .yellowfin10AndUpGood = CDec(frmSI.dt.Rows(14)("Price"))
            .yellowfin10AndUpDeformed = CDec(frmSI.dt.Rows(15)("Price"))
            .bigeye0_500To0_999 = CDec(frmSI.dt.Rows(16)("Price"))
            .bigeye1_0To1_49 = CDec(frmSI.dt.Rows(17)("Price"))
            .bigeye1_5To2_49 = CDec(frmSI.dt.Rows(18)("Price"))
            .bigeye2_5To3_49 = CDec(frmSI.dt.Rows(19)("Price"))
            .bigeye3_5To4_99 = CDec(frmSI.dt.Rows(20)("Price"))
            .bigeye5_0To9_99 = CDec(frmSI.dt.Rows(21)("Price"))
            .bigeye10AndUP = CDec(frmSI.dt.Rows(22)("Price"))
            .bonito = CDec(frmSI.dt.Rows(23)("Price"))
            .fishmeal = CDec(frmSI.dt.Rows(24)("Price"))
            If isNew Then
                .Add()
            Else
                .Save()
            End If
        End With
    End Sub

    Sub setCarrierData(salesReportID As Integer)
        Dim cd As New CarrierDetails(mkdb)
        Dim count As Integer
        Dim getCd As List(Of Integer) = New List(Of Integer)

        If Not isNew Then
            getCd = (From i In mkdb.trans_SalesUnloadeds Where i.SalesReportID = salesReportID Select i.CarrierID).ToList()
            count = 0
        End If

        For Each row As DataRow In frmSI.dts.Rows
            With cd
                .SalesReportID = salesReportID
                .CarrierName = row("Carrier_Name").ToString
                .CarrierType = row("Carrier_Type").ToString
                .UnloadedValue = CDec(row("Metric_Ton"))
                If Not isNew Then
                    .CarrierID = CInt(getCd(count))
                    .Save()
                    count += 1
                Else
                    .Add()
                End If
            End With
        Next
    End Sub

    Sub setCatcherPrice(rowName As String, ByVal salesReportID As Integer)
        Dim ca = (From i In mkdb.trans_CatchActivities
                  Join j In mkdb.trans_CatchActivityDetails On i.catchActivity_ID Equals j.catchActivity_ID
                  Where i.catchActivity_ID = CInt(frmSI.cmbUV.EditValue)
                  Select j).ToList()

        Dim count = 1

        For Each item In ca

            Dim src As New SalesReportCatcher(mkdb)

            If Not isNew Then
                Dim getAllSrc = (From i In mkdb.trans_SalesReportCatchers Where i.salesReport_ID = salesReportID Select i.catchActivityDetail_ID).Distinct.ToList
                Dim getID As trans_SalesReportCatcher = Nothing
                Dim getSrc = (From i In mkdb.trans_SalesReportCatchers Where i.salesReport_ID = salesReportID AndAlso i.catchActivityDetail_ID = getAllSrc(count - 1) Select i).ToList

                If rowName Is "AUK_Catcher" Then
                    getID = getSrc.FirstOrDefault
                Else
                    getID = getSrc(1)
                End If
                src = New SalesReportCatcher(getID.salesReportCatcher_ID, mkdb)
            End If

            With src
                .catchActivityDetail_ID = item.catchActivityDetail_ID
                .salesReport_ID = salesReportID
                .skipjack0_300To0_499 = CDec(frmSI.dt.Rows(0)(rowName & count))
                .skipjack0_500To0_999 = CDec(frmSI.dt.Rows(1)(rowName & count))
                .skipjack1_0To1_39 = CDec(frmSI.dt.Rows(2)(rowName & count))
                .skipjack1_4To1_79 = CDec(frmSI.dt.Rows(3)(rowName & count))
                .skipjack1_8To2_49 = CDec(frmSI.dt.Rows(4)(rowName & count))
                .skipjack2_5To3_49 = CDec(frmSI.dt.Rows(5)(rowName & count))
                .skipjack3_5AndUP = CDec(frmSI.dt.Rows(6)(rowName & count))
                .yellowfin0_300To0_499 = CDec(frmSI.dt.Rows(7)(rowName & count))
                .yellowfin0_500To0_999 = CDec(frmSI.dt.Rows(8)(rowName & count))
                .yellowfin1_0To1_49 = CDec(frmSI.dt.Rows(9)(rowName & count))
                .yellowfin1_5To2_49 = CDec(frmSI.dt.Rows(10)(rowName & count))
                .yellowfin2_5To3_49 = CDec(frmSI.dt.Rows(11)(rowName & count))
                .yellowfin3_5To4_99 = CDec(frmSI.dt.Rows(12)(rowName & count))
                .yellowfin5_0To9_99 = CDec(frmSI.dt.Rows(13)(rowName & count))
                .yellowfin10AndUpGood = CDec(frmSI.dt.Rows(14)(rowName & count))
                .yellowfin10AndUpDeformed = CDec(frmSI.dt.Rows(15)(rowName & count))
                .bigeye0_500To0_999 = CDec(frmSI.dt.Rows(16)(rowName & count))
                .bigeye1_0To1_49 = CDec(frmSI.dt.Rows(17)(rowName & count))
                .bigeye1_5To2_49 = CDec(frmSI.dt.Rows(18)(rowName & count))
                .bigeye2_5To3_49 = CDec(frmSI.dt.Rows(19)(rowName & count))
                .bigeye3_5To4_99 = CDec(frmSI.dt.Rows(20)(rowName & count))
                .bigeye5_0To9_99 = CDec(frmSI.dt.Rows(21)(rowName & count))
                .bigeye10AndUP = CDec(frmSI.dt.Rows(22)(rowName & count))
                .bonito = CDec(frmSI.dt.Rows(23)(rowName & count))
                .fishmeal = CDec(frmSI.dt.Rows(24)(rowName & count))
                If isNew Then
                    .Add()
                Else
                    .Save()
                End If
            End With

            count = count + 1
        Next
    End Sub

    Sub loadRows()
        Dim fishClasses As New Dictionary(Of String, String()) From {
               {"SKIPJACK", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.39", "1.4 - 1.79", "1.8 - 2.49", "2.5 - 3.49", "3.5 - UP"}},
               {"YELLOWFIN", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP GOOD", "10 - UP DEFORMED"}},
               {"BIGEYE", New String() {"0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP"}},
               {"BONITO", New String() {"ALL SIZES"}},
               {"FISHMEAL", New String() {"ALL SIZES"}}
           }

        Dim columns = frmSI.dt.Columns

        If isNew Then
            For Each fishClass In fishClasses
                For Each size In fishClass.Value
                    Dim dr As DataRow = frmSI.dt.NewRow()
                    dr("Class") = fishClass.Key
                    dr("Size") = size

                    For Each col As DataColumn In columns
                        If col.ColumnName <> "Class" AndAlso col.ColumnName <> "Size" Then dr(col) = 0
                    Next

                    frmSI.dt.Rows.Add(dr)
                Next
            Next
        Else
            Dim priceColumns As List(Of String) = GetType(trans_SalesReportPrice).GetProperties().Select(Function(t) t.Name).ToList()
            priceColumns.RemoveAll(Function(c) c = "salesReportPrice_ID" OrElse c = "salesReport_ID")

            Dim srp = mkdb.trans_SalesReportPrices.
                Where(Function(sp) sp.salesReport_ID = mdlSR.salesReport_ID).
                FirstOrDefault()

            Dim srcList = mkdb.trans_SalesReportCatchers.
                Where(Function(i) i.salesReport_ID = mdlSR.salesReport_ID).
                Select(Function(i) i.catchActivityDetail_ID).
                Distinct().ToList()

            ' Preload Catch Data for faster lookup
            Dim catchDataDict = mkdb.trans_SalesReportCatchers.
                Where(Function(i) i.salesReport_ID = mdlSR.salesReport_ID).
                GroupBy(Function(i) i.catchActivityDetail_ID).
                ToDictionary(Function(g) g.Key, Function(g) g.ToList())

            Dim priceCount As Integer = 0

            For Each fishClass In fishClasses
                For Each size In fishClass.Value
                    If priceCount >= priceColumns.Count Then Exit For
                    Dim priceColumn As String = priceColumns(priceCount)
                    Dim propInfo = GetType(trans_SalesReportPrice).GetProperty(priceColumn)

                    Dim dr As DataRow = frmSI.dt.NewRow()
                    dr("Class") = fishClass.Key
                    dr("Size") = size

                    ' Fetch Price
                    dr("Price") = CDec(propInfo.GetValue(srp, Nothing))

                    Dim countKiloCatcher As Integer = 0
                    Dim countSpoilageCatcher As Integer = 0
                    Dim propCatch = GetType(trans_SalesReportCatcher).GetProperty(priceColumn)

                    ' Assign values to columns without querying inside loops
                    For Each col As DataColumn In columns
                        If col.ColumnName.Contains("AUK_Catcher") Then
                            If countKiloCatcher < srcList.Count AndAlso catchDataDict.ContainsKey(srcList(countKiloCatcher)) Then
                                Dim catchList = catchDataDict(srcList(countKiloCatcher))
                                dr("AUK_Catcher" & (countKiloCatcher + 1)) = CDec(propCatch.GetValue(catchList(0), Nothing))
                            End If
                            countKiloCatcher += 1
                        ElseIf col.ColumnName.Contains("SK_Catcher") Then
                            If countSpoilageCatcher < srcList.Count AndAlso catchDataDict.ContainsKey(srcList(countSpoilageCatcher)) Then
                                Dim catchList = catchDataDict(srcList(countSpoilageCatcher))
                                dr("SK_Catcher" & (countSpoilageCatcher + 1)) = CDec(propCatch.GetValue(catchList(1), Nothing))
                            End If
                            countSpoilageCatcher += 1
                        ElseIf col.ColumnName <> "Class" AndAlso col.ColumnName <> "Size" AndAlso col.ColumnName <> "Price" Then
                            dr(col) = 0
                        End If
                    Next

                    frmSI.dt.Rows.Add(dr)
                    priceCount += 1
                Next
            Next

            updateAllTotals()
        End If

    End Sub

    Sub loadCombo()
        Dim sc = (From i In tpmdb.ml_SupplierCategories Select i.ml_supCat).ToArray
        frmSI.cmbST.Properties.Items.AddRange(sc)

        Dim uv = (From ca In mkdb.trans_CatchActivities
                  Where ca.approvalStatus = 1
                  Select New With {
                    .catchActivity_ID = ca.catchActivity_ID,
                    .catchDate = ca.catchDate,
                    .refNum = ca.catchReferenceNum
                    }).ToList()

        Dim formattedUv = uv.Select(Function(ca) New With {
            .catchActivity_ID = ca.catchActivity_ID,
            .catchDate = ca.catchDate.ToString("yyyy-MM-dd") & " - " & ca.refNum}).ToList()

        lookUpTransMode(formattedUv, frmSI.cmbUV, "catchDate", "catchActivity_ID", "Select catcher")

    End Sub

    Sub print()
        Dim tool As ReportPrintTool

        Dim rp = New rptSalesInvoiceReport()
        rp.DataSource = getReportSalesInvoice(mdlSR.salesReport_ID)
        tool = New ReportPrintTool(rp)
        tool.ShowPreviewDialog()
    End Sub

End Class