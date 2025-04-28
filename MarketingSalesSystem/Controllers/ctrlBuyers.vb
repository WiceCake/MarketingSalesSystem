Imports System.Transactions

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

        frmBS.GridControl1.DataSource = frmBS.dtAK

        With frmBS
            If .rBuyer.SelectedIndex = 0 Then
                showTxtBuyer()
            End If

            .btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            .btnPost.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
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

        frmBS.GridControl1.DataSource = frmBS.dtAK

        With frmBS
            If mdlSIB.approvalStatus = Approval_Status.Posted Then
                .rbnTools.Visible = False
            End If

            If Integer.TryParse(mdlSIB.buyerName, 0) Then
                .rBuyer.SelectedIndex = 1
                showCbBuyer()
                .cmbBuyer.EditValue = mdlSIB.buyerName
            Else
                showTxtBuyer()
                .txtBuyer.EditValue = mdlSIB.buyerName
            End If

            loadComboxes()

            .dtEncoded.EditValue = mdlSIB.encodedOn
            .cmbSaleType.EditValue = mdlSIB.sellerType
            .lueInvoice.EditValue = mdlSIB.salesInvoiceID
            .txtSetNo.EditValue = mdlSIB.setNum
            .txtRefNum.Caption = mdlSIB.referenceNum

            .txtAmountPaid.EditValue = mdlSIB.paidAmount
            .txtAdjustments.EditValue = mdlSIB.adjustmentsAmount

            .Show()
        End With
    End Sub

    Sub saveDraft()
        Using ts As New TransactionScope()
            Try
                With frmBS
                    mdlSIB.encodedOn = CDate(.dtEncoded.EditValue)
                    mdlSIB.sellerType = .cmbSaleType.EditValue.ToString
                    mdlSIB.salesInvoiceID = CInt(.lueInvoice.EditValue)
                    If .rBuyer.SelectedIndex = 0 Then
                        mdlSIB.buyerName = .txtBuyer.Text
                    Else
                        mdlSIB.buyerName = .cmbBuyer.EditValue.ToString
                    End If
                    mdlSIB.setNum = .txtSetNo.EditValue.ToString
                    mdlSIB.adjustmentsAmount = CDec(.txtAdjustments.EditValue)
                    mdlSIB.paidAmount = CDec(.txtAmountPaid.EditValue)
                    mdlSIB.encodedBy = "###"
                    mdlSIB.referenceNum = "Draft"
                    If isNew Then
                        mdlSIB.approvalStatus = Approval_Status.Draft
                        mdlSIB.Add()
                    Else
                        mdlSIB.approvalStatus = Approval_Status.Submitted
                        mdlSIB.Save()
                    End If
                End With

                setSalesPrice(mdlSIB.salesInvoiceBuyerID, mdlSIB.salesInvoiceID)

                ts.Complete()
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Using
        frmBS.Close()
        ucB.loadGrid()
    End Sub

    Sub setSalesPrice(ByVal salesInvoiceBuyerID As Integer, ByVal salesInvoiceID As Integer)
        Dim srb As New SalesReportBuyer(mkdb)
        Dim count As Integer
        Dim getSrb As List(Of Integer) = New List(Of Integer)

        If Not isNew Then
            getSrb = (From i In mkdb.trans_SalesReportBuyers Where i.salesInvoiceBuyerID = CInt(salesInvoiceBuyerID) Select i.salesBuyerCatchID).ToList()

            count = 0
        End If

        For Each cols As DataColumn In frmBS.dtAK.Columns

            If Not cols.Caption.Contains("K_Catcher") Then Continue For

            With srb
                .salesInvoiceBuyer_ID = salesInvoiceBuyerID
                .salesInvoice_ID = salesInvoiceID
                .skipjack0_300To0_499 = CDec(frmBS.dtAK.Rows(0)(cols))
                .skipjack0_500To0_999 = CDec(frmBS.dtAK.Rows(1)(cols))
                .skipjack1_0To1_39 = CDec(frmBS.dtAK.Rows(2)(cols))
                .skipjack1_4To1_79 = CDec(frmBS.dtAK.Rows(3)(cols))
                .skipjack1_8To2_49 = CDec(frmBS.dtAK.Rows(4)(cols))
                .skipjack2_5To3_49 = CDec(frmBS.dtAK.Rows(5)(cols))
                .skipjack3_5AndUP = CDec(frmBS.dtAK.Rows(6)(cols))
                .yellowfin0_300To0_499 = CDec(frmBS.dtAK.Rows(7)(cols))
                .yellowfin0_500To0_999 = CDec(frmBS.dtAK.Rows(8)(cols))
                .yellowfin1_0To1_49 = CDec(frmBS.dtAK.Rows(9)(cols))
                .yellowfin1_5To2_49 = CDec(frmBS.dtAK.Rows(10)(cols))
                .yellowfin2_5To3_49 = CDec(frmBS.dtAK.Rows(11)(cols))
                .yellowfin3_5To4_99 = CDec(frmBS.dtAK.Rows(12)(cols))
                .yellowfin5_0To9_99 = CDec(frmBS.dtAK.Rows(13)(cols))
                .yellowfin10AndUpGood = CDec(frmBS.dtAK.Rows(14)(cols))
                .yellowfin10AndUpDeformed = CDec(frmBS.dtAK.Rows(15)(cols))
                .bigeye0_500To0_999 = CDec(frmBS.dtAK.Rows(16)(cols))
                .bigeye1_0To1_49 = CDec(frmBS.dtAK.Rows(17)(cols))
                .bigeye1_5To2_49 = CDec(frmBS.dtAK.Rows(18)(cols))
                .bigeye2_5To3_49 = CDec(frmBS.dtAK.Rows(19)(cols))
                .bigeye3_5To4_99 = CDec(frmBS.dtAK.Rows(20)(cols))
                .bigeye5_0To9_99 = CDec(frmBS.dtAK.Rows(21)(cols))
                .bigeye10AndUP = CDec(frmBS.dtAK.Rows(22)(cols))
                .bonito = CDec(frmBS.dtAK.Rows(23)(cols))
                .fishmeal = CDec(frmBS.dtAK.Rows(24)(cols))
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

    Sub initKiloDataTable(ByVal catchID As Integer) ' Comment
        Dim ca = (From i In mkdb.trans_CatchActivityDetails Where i.catchActivity_ID = catchID Select i).ToList()

        With frmBS
            .dtAK = New DataTable()

            .dtAK.Columns.Add("Class", GetType(String))
            .dtAK.Columns.Add("Size", GetType(String))
            .dtAK.Columns.Add("Price", GetType(Double))
            '.dtAK.Columns.Add("Available_Catch", GetType(Double))
            addColumnDT(.dtAK, "AC_Catcher", ca.Count)
            .dtAK.Columns.Add("Kilo_Total", GetType(Double))
            addColumnDT(.dtAK, "K_Catcher", ca.Count)
            .dtAK.Columns.Add("Amount_Total", GetType(Double))
            addColumnDT(.dtAK, "A_Catcher", ca.Count)
        End With
    End Sub

    Sub initSpoilageDataTable(ByVal catchID As Integer) ' Comment
        Dim ca = (From i In mkdb.trans_CatchActivityDetails Where i.catchActivity_ID = catchID Select i).ToList()

        With frmBS
            .dtS = New DataTable()

            .dtS.Columns.Add("Class", GetType(String))
            .dtS.Columns.Add("Size", GetType(String))
            .dtS.Columns.Add("Price", GetType(Double))
            .dtS.Columns.Add("Kilo_Total", GetType(Double))
            addColumnDT(.dtS, "K_Catcher", ca.Count)
            .dtS.Columns.Add("Amount_Total", GetType(Double))
            addColumnDT(.dtS, "A_Catcher", ca.Count)
        End With
    End Sub

    Sub addColumnDT(dt As DataTable, caption As String, count As Integer)
        For i As Integer = 1 To count
            dt.Columns.Add(caption & i, GetType(Double))
        Next
    End Sub

    Sub updateKiloTotal(ByRef r As DataRow)
        Dim Price As Decimal = CDec(If(IsDBNull(r("Price")), 0, r("Price")))

        Dim Kilo_Total As Decimal = 0
        Dim Amount_Total As Decimal = 0

        For Each col As DataColumn In r.Table.Columns
            Dim colName As String = col.ColumnName

            If colName.StartsWith("K_Catcher") Then
                Dim acColumn As String = colName.Replace("K", "AC")

                Dim kValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))
                Dim acValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(acColumn)), 0, r(acColumn))))

                kValue = Math.Min(kValue, acValue)
                r(colName) = kValue  ' Update the value in the DataRow

                Kilo_Total += kValue

                Dim auaColumn As String = colName.Replace("K", "A")
                If r.Table.Columns.Contains(auaColumn) Then
                    r(auaColumn) = kValue * Price
                    Amount_Total += CDec(r(auaColumn))
                End If
            End If
        Next

        ' Store final totals
        r("Kilo_Total") = Kilo_Total
        r("Amount_Total") = Amount_Total
    End Sub

    Sub updateSpoilageTotal(ByRef r As DataRow)
        Dim Price As Decimal = CDec(If(IsDBNull(r("Price")), 0, r("Price")))

        Dim Kilo_Total As Decimal = 0
        Dim Amount_Total As Decimal = 0

        For Each col As DataColumn In r.Table.Columns
            Dim colName As String = col.ColumnName

            If colName.StartsWith("K_Catcher") Then

                Dim kValue As Decimal = Math.Max(0, CDec(If(IsDBNull(r(colName)), 0, r(colName))))

                Kilo_Total += kValue

                Dim auaColumn As String = colName.Replace("K", "A")
                If r.Table.Columns.Contains(auaColumn) Then
                    r(auaColumn) = kValue * Price
                    Amount_Total += CDec(r(auaColumn))
                End If
            End If
        Next

        ' Store final totals
        r("Kilo_Total") = Kilo_Total
        r("Amount_Total") = Amount_Total
    End Sub

    Sub updateAllKiloTotals()
        For Each r As DataRow In frmBS.dtAK.Rows
            updateKiloTotal(r)
        Next
    End Sub

    Sub updateAllSpoilageTotals()
        For Each r As DataRow In frmBS.dtS.Rows
            updateSpoilageTotal(r)
        Next
    End Sub

    Sub loadKiloRows(salesReportID As Integer)
        Dim fishClasses As New Dictionary(Of String, String()) From {
               {"SKIPJACK", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.39", "1.4 - 1.79", "1.8 - 2.49", "2.5 - 3.49", "3.5 - UP"}},
               {"YELLOWFIN", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP GOOD", "10 - UP DEFORMED"}},
               {"BIGEYE", New String() {"0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP"}},
               {"BONITO", New String() {"ALL SIZES"}},
               {"FISHMEAL", New String() {"ALL SIZES"}}
           }

        Dim columns = frmBS.dtAK.Columns

        Dim priceColumns As List(Of String) = GetType(trans_SalesReportPrice).GetProperties().Select(Function(t) t.Name).ToList()
        priceColumns.RemoveAll(Function(c) c = "salesReportPrice_ID" OrElse c = "salesReport_ID")

        Dim srp = mkdb.trans_SalesReportPrices.
            Where(Function(sp) sp.salesReport_ID = salesReportID).
            FirstOrDefault()

        ' Preload Catch Data for faster lookup
        Dim cdList = mkdb.trans_SalesReportCatchers.
            Where(Function(s) s.salesReport_ID = salesReportID).
            GroupBy(Function(s) s.catchActivityDetail_ID).
            Select(Function(s) s.Skip(0).FirstOrDefault()).ToList()

        Dim bList = mkdb.trans_SalesReportBuyers.
            Where(Function(s) s.salesInvoiceID = salesReportID).
            ToList()

        If Not isNew Then
            bList = mkdb.trans_SalesReportBuyers.
            Where(Function(s) s.salesInvoiceID = salesReportID).
            Where(Function(s) s.salesInvoiceBuyerID = mdlSIB.salesInvoiceBuyerID).
            ToList()
        End If

        Dim priceCount As Integer = 0

        For Each fishClass In fishClasses
            For Each size In fishClass.Value
                Dim priceColumn As String = priceColumns(priceCount)
                Dim propInfo = GetType(trans_SalesReportPrice).GetProperty(priceColumn)

                Dim dr As DataRow = frmBS.dtAK.NewRow()
                dr("Class") = fishClass.Key
                dr("Size") = size

                dr("Price") = CDec(propInfo.GetValue(srp, Nothing))

                Dim countAvailableCatch As Integer = 0
                Dim countKiloCatcher As Integer = 0
                Dim propCatch = GetType(trans_SalesReportCatcher).GetProperty(priceColumn)
                Dim propBuyer = GetType(trans_SalesReportBuyer).GetProperty(priceColumn)

                For Each col As DataColumn In columns
                    If col.ColumnName.Contains("AC_Catcher") Then
                        dr("AC_Catcher" & (countAvailableCatch + 1)) = CDec(propCatch.GetValue(cdList(countAvailableCatch), Nothing))
                        countAvailableCatch += 1
                    ElseIf col.ColumnName.Contains("K_Catcher") AndAlso Not isNew AndAlso bList.Count > 0 Then
                        dr("K_Catcher" & (countKiloCatcher + 1)) = CDec(propBuyer.GetValue(bList(countKiloCatcher), Nothing))
                        countKiloCatcher += 1
                    ElseIf col.ColumnName <> "Class" AndAlso col.ColumnName <> "Size" AndAlso col.ColumnName <> "Price" Then
                        dr(col) = 0
                    End If
                Next

                frmBS.dtAK.Rows.Add(dr)
                priceCount += 1
            Next
        Next
        updateAllKiloTotals()

    End Sub

    Sub loadSpoilageRows(salesReportID As Integer)
        Dim fishClasses As New Dictionary(Of String, String()) From {
               {"SKIPJACK", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.39", "1.4 - 1.79", "1.8 - 2.49", "2.5 - 3.49", "3.5 - UP"}},
               {"YELLOWFIN", New String() {"0.300 - 0.499", "0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP GOOD", "10 - UP DEFORMED"}},
               {"BIGEYE", New String() {"0.500 - 0.999", "1.0 - 1.49", "1.5 - 2.49", "2.5 - 3.49", "3.5 - 4.99", "5.0 - 9.99", "10 - UP"}},
               {"BONITO", New String() {"ALL SIZES"}},
               {"FISHMEAL", New String() {"ALL SIZES"}}
           }

        Dim columns = frmBS.dtS.Columns

        Dim priceColumns As List(Of String) = GetType(trans_SalesReportPrice).GetProperties().Select(Function(t) t.Name).ToList()
        priceColumns.RemoveAll(Function(c) c = "salesReportPrice_ID" OrElse c = "salesReport_ID")

        Dim srp = mkdb.trans_SalesReportPrices.
            Where(Function(sp) sp.salesReport_ID = salesReportID).
            FirstOrDefault()

        ' Preload Catch Data for faster lookup
        Dim cdList = mkdb.trans_SalesReportCatchers.
            Where(Function(s) s.salesReport_ID = salesReportID).
            GroupBy(Function(s) s.catchActivityDetail_ID).
            Select(Function(s) s.Skip(1).FirstOrDefault()).ToList()

        Dim priceCount As Integer = 0

        For Each fishClass In fishClasses
            For Each size In fishClass.Value
                Dim priceColumn As String = priceColumns(priceCount)
                Dim propInfo = GetType(trans_SalesReportPrice).GetProperty(priceColumn)

                Dim dr As DataRow = frmBS.dtS.NewRow()
                dr("Class") = fishClass.Key
                dr("Size") = size

                dr("Price") = CDec(propInfo.GetValue(srp, Nothing))

                Dim countSpoilageCatcher As Integer = 0
                Dim propCatch = GetType(trans_SalesReportCatcher).GetProperty(priceColumn)

                For Each col As DataColumn In columns
                    If col.ColumnName.Contains("K_Catcher") Then
                        dr("K_Catcher" & (countSpoilageCatcher + 1)) = CDec(propCatch.GetValue(cdList(countSpoilageCatcher), Nothing))
                        countSpoilageCatcher += 1
                    ElseIf col.ColumnName <> "Class" AndAlso col.ColumnName <> "Size" AndAlso col.ColumnName <> "Price" Then
                        dr(col) = 0
                    End If
                Next

                frmBS.dtS.Rows.Add(dr)
                priceCount += 1
            Next
        Next
        updateAllSpoilageTotals()

    End Sub

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

        lookUpTransMode(sr, frmBS.lueInvoice, "Invoice", "ID", "Select Invoice")
    End Sub

    Sub loadBuyer()
        Dim b = From i In tpmdb.ML_SULLIER_20170329s Select New With {
                .ID = i.ml_SupID,
                .Name = i.ml_Supplier}

        lookUpTransMode(b, frmBS.cmbBuyer, "Name", "ID", "Select Buyer")
    End Sub
End Class