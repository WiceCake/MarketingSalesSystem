
﻿Module modData
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

    Function getReportCatcherAct(reportID As Integer) As List(Of CatchersActivity)
        Dim retList As New List(Of CatchersActivity)

        Dim dc As New mkdbDataContext
        Dim dc2 As New tpmdbDataContext

        Dim cr = (From i In dc.trans_CatchActivities Where i.catchActivity_ID = reportID).ToList

        Dim cri = New CatchersActivity

        For Each c In cr
            cri.catchDate = c.catchDate

            retList.Add(cri)
        Next
        Return retList
    End Function

End Module

