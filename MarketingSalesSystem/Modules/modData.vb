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

    Function getReportCatcherAct(reportID1 As Integer) As List(Of CatchersReportActivity)
        Dim retList1 As New List(Of CatchersReportActivity)

        Dim dc As New mkdbDataContext
        Dim dc2 As New tpmdbDataContext

        Dim cr = (From i In dc.trans_CatchActivities Where i.catchActivity_ID = reportID1).ToList

        Dim cri = New CatchersReportActivity

        For Each c In cr
            cri.catchDate = c.catchDate

            retList1.Add(cri)
        Next
        Return retList1
    End Function
End Module

