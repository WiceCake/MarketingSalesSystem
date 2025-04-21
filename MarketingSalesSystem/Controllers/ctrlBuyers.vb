Public Class ctrlBuyers
    Private isNew As Boolean
    Private mdlSIB As SalesInvoiceBuyer
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
        mdlSR = New SalesReport(mkdb)

        frmBS = New frm_buyerSales(Me)

        ucB = uc

        'initKiloDataTable()

        frmBS.GridControl1.DataSource = frmBS.dtAK

        With frmBS
            If .rBuyer.SelectedIndex = 0 Then
                showTxtBuyer()
            End If

            .txtAmountPaid.ReadOnly = True
            .txtAdjustments.ReadOnly = True
            .txtAmountPaid.ReadOnly = True

            loadComboxes()
            'loadDataRows()
            .Show()
        End With
    End Sub

    Sub New(ByRef uc As ucBuyer, ByVal sibID As Integer)
        isNew = False

        mkdb = New mkdbDataContext

        mdlSIB = New SalesInvoiceBuyer(sibID, mkdb)

        frmBS = New frm_buyerSales(Me)

        ucB = uc

        With frmBS
            .Show()
        End With
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

        Dim srcList = mkdb.trans_SalesReportCatchers.
                Where(Function(i) i.salesReport_ID = salesReportID).
                Select(Function(i) i.catchActivityDetail_ID).
                Distinct().ToList()

        ' Preload Catch Data for faster lookup
        Dim catchDataDict = mkdb.trans_SalesReportCatchers.
            Where(Function(i) i.salesReport_ID = salesReportID).
            GroupBy(Function(i) i.catchActivityDetail_ID).
            ToDictionary(Function(g) g.Key, Function(g) g.ToList())

        Dim priceCount As Integer = 0

        If isNew Then
            For Each fishClass In fishClasses
                For Each size In fishClass.Value
                    Dim priceColumn As String = priceColumns(priceCount)
                    Dim propInfo = GetType(trans_SalesReportPrice).GetProperty(priceColumn)

                    Dim dr As DataRow = frmBS.dtAK.NewRow()
                    dr("Class") = fishClass.Key
                    dr("Size") = size

                    dr("Price") = CDec(propInfo.GetValue(srp, Nothing))

                    Dim countKiloCatcher As Integer = 0
                    Dim propCatch = GetType(trans_SalesReportCatcher).GetProperty(priceColumn)

                    For Each col As DataColumn In columns
                        If col.ColumnName.Contains("AC_Catcher") Then
                            If countKiloCatcher < srcList.Count AndAlso catchDataDict.ContainsKey(srcList(countKiloCatcher)) Then
                                Dim catchList = catchDataDict(srcList(countKiloCatcher))
                                dr("AC_Catcher" & (countKiloCatcher + 1)) = CDec(propCatch.GetValue(catchList(0), Nothing))
                            End If
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
        Else
            ' For Next Update
        End If

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

        Dim srcList = mkdb.trans_SalesReportCatchers.
                Where(Function(i) i.salesReport_ID = salesReportID).
                Select(Function(i) i.catchActivityDetail_ID).
                Distinct().ToList()

        ' Preload Catch Data for faster lookup
        Dim catchDataDict = mkdb.trans_SalesReportCatchers.
            Where(Function(i) i.salesReport_ID = salesReportID).
            GroupBy(Function(i) i.catchActivityDetail_ID).
            ToDictionary(Function(g) g.Key, Function(g) g.ToList())

        Dim priceCount As Integer = 0

        If isNew Then
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
                            If countSpoilageCatcher < srcList.Count AndAlso catchDataDict.ContainsKey(srcList(countSpoilageCatcher)) Then
                                Dim catchList = catchDataDict(srcList(countSpoilageCatcher))
                                dr("K_Catcher" & (countSpoilageCatcher + 1)) = CDec(propCatch.GetValue(catchList(1), Nothing))
                            End If
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
        Else
            ' For Next Update
        End If

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
