Module modData
    Function getReportSalesInvoice(reportID As Integer) As List(Of SalesReportInvoice)
        Dim retList As New List(Of SalesReportInvoice)

        Dim dc As New mkdbDataContext
        Dim dc2 As New tpmdbDataContext

        Dim sr = (From i In dc.trans_SalesReports Where i.salesReport_ID = reportID).ToList

        Dim sri = New SalesReportInvoice

        For Each s In sr
            sri.SalesDate = s.salesDate

            retList.Add(sri)
        Next

        Return retList
    End Function
End Module
