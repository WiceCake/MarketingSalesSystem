Public Class PartialReport

    'SalesInvoiceBuyer
    Public Property salesInvoiceBuyerID As Integer
    Public Property salesInvoiceID As Integer
    Public Property buyerName As String
    Public Property encodedBy As String
    Public Property referenceNum As String
    Public Property sellerType As String
    Public Property paidAmount As Decimal
    Public Property adjustmentsAmount As Decimal
    Public Property encodedOn As Date
    Public Property dateCreated As Date
    Public Property approvalStatus As Integer
    Public Property paymentStatus As Integer

    'SalesReport
    Public Property salesReport_ID As Integer
    Public Property invoiceNum As String
    'Public Property referenceNum As String
    Public Property salesDate As Date
    Public Property salesNum As String
    Public Property sellingType As String
    Public Property unloadingType As String
    Public Property unloadingVessel_ID As String
    Public Property unloadingForeignVessel As String
    Public Property buyer As String
    Public Property catchtDeliveryNum As String
    Public Property usdRate As Decimal
    Public Property contractNum As String
    Public Property remarks As String
    'Public Property encodedBy As Integer
    'Public Property encodedOn As Date
    'Public Property approvalStatus As Integer

    Sub New()

    End Sub
End Class
