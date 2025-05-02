Public Class SalesInvoiceReport
    Public salesInvoiceReport_ID As Integer
    Public salesInvoiceBuyer_ID As Integer
    Public previousReport_ID As Integer?
    Public dateCreated As Date?

    Private dc As mkdbDataContext

    Sub New(ByRef dc_ As mkdbDataContext)
        dc = dc_
    End Sub

    Sub New(ByVal salesInvoiceReportID As Integer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim e = (From i In dc.trans_SalesInvoiceReports Where i.salesInvoiceReport_ID = salesInvoiceReportID Select i)

        For Each i In e
            salesInvoiceReport_ID = salesInvoiceReport_ID
            salesInvoiceBuyer_ID = i.salesInvoiceBuyer_ID
            previousReport_ID = i.previousReport_ID
            dateCreated = i.dateCreated
        Next
    End Sub

    Sub Add()
        Dim sir As New trans_SalesInvoiceReport

        With sir
            .salesInvoiceBuyer_ID = salesInvoiceBuyer_ID
            .previousReport_ID = previousReport_ID
        End With

        dc.trans_SalesInvoiceReports.InsertOnSubmit(sir)
        dc.SubmitChanges()
        Me.salesInvoiceReport_ID = sir.salesInvoiceReport_ID
    End Sub

    Sub Save()
        Dim e = From i In dc.trans_SalesInvoiceReports Where i.salesInvoiceReport_ID = salesInvoiceReport_ID Select i

        For Each i In e
            i.salesInvoiceReport_ID = salesInvoiceReport_ID
            i.salesInvoiceBuyer_ID = salesInvoiceBuyer_ID
            i.previousReport_ID = previousReport_ID
            dc.SubmitChanges()
        Next
    End Sub

    Sub Delete()
        Dim sir = From i In dc.trans_SalesInvoiceReports Where i.salesInvoiceReport_ID = salesInvoiceReport_ID Select i

        For Each i In sir
            dc.trans_SalesInvoiceReports.DeleteOnSubmit(i)
            dc.SubmitChanges()
        Next
    End Sub
End Class
