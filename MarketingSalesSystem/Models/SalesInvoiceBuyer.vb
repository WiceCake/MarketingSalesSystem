Public Class SalesInvoiceBuyer
    Public salesInvoiceBuyerID, salesInvoiceID As Integer
    Public buyerName, encodedBy, referenceNum, sellerType, setNum, invoiceNum, containerNum As String
    Public paidAmount, adjustmentsAmount As Decimal?
    Public encodedOn As Date
    Public carrier As Integer
    Public backing As Integer?
    Public dateCreated As Date?
    Public approvalStatus, paymentStatus As Integer

    Private dc As mkdbDataContext

    Sub New(ByRef dc_ As mkdbDataContext)
        dc = dc_
    End Sub

    Sub New(ByVal sibID As Integer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim e = From i In dc.trans_SalesInvoiceBuyers Where i.salesInvoiceBuyerID = sibID Select i

        For Each i In e
            salesInvoiceBuyerID = sibID
            salesInvoiceID = i.salesInvoiceID
            invoiceNum = i.invoiceNum
            setNum = i.setNum
            paidAmount = i.paidAmount
            adjustmentsAmount = i.adjustmentsAmount
            referenceNum = i.referenceNum
            buyerName = i.buyerName
            carrier = i.carrier
            backing = i.backing
            sellerType = i.sellerType
            containerNum = i.containerNum
            paymentStatus = i.paymentStatus
            approvalStatus = i.approvalStatus
            encodedOn = i.encodedOn
            encodedBy = i.encodedBy
            dateCreated = i.dateCreated
        Next
    End Sub

    Sub New(sib As trans_SalesInvoiceBuyer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        salesInvoiceBuyerID = sib.salesInvoiceBuyerID
        salesInvoiceID = sib.salesInvoiceID
        invoiceNum = sib.invoiceNum
        setNum = sib.setNum
        paidAmount = sib.paidAmount
        adjustmentsAmount = sib.adjustmentsAmount
        referenceNum = sib.referenceNum
        buyerName = sib.buyerName
        carrier = sib.carrier
        backing = sib.backing
        sellerType = sib.sellerType
        containerNum = sib.containerNum
        paymentStatus = sib.paymentStatus
        approvalStatus = sib.approvalStatus
        encodedOn = sib.encodedOn
        encodedBy = sib.encodedBy
        dateCreated = sib.dateCreated
    End Sub

    Sub Add()
        Dim sib As New trans_SalesInvoiceBuyer

        With sib
            .salesInvoiceID = salesInvoiceID
            .invoiceNum = invoiceNum
            .setNum = setNum
            .paidAmount = paidAmount
            .adjustmentsAmount = adjustmentsAmount
            .referenceNum = referenceNum
            .buyerName = buyerName
            .carrier = carrier
            .backing = backing
            .sellerType = sellerType
            .containerNum = containerNum
            .paymentStatus = paymentStatus
            .approvalStatus = approvalStatus
            .encodedOn = encodedOn
            .encodedBy = encodedBy
            .dateCreated = dateCreated
        End With

        dc.trans_SalesInvoiceBuyers.InsertOnSubmit(sib)
        dc.SubmitChanges()
        Me.salesInvoiceBuyerID = sib.salesInvoiceBuyerID
    End Sub

    Sub Save()
        Dim e = From i In dc.trans_SalesInvoiceBuyers Where i.salesInvoiceBuyerID = salesInvoiceBuyerID Select i

        For Each i In e
            i.salesInvoiceBuyerID = salesInvoiceBuyerID
            i.salesInvoiceID = salesInvoiceID
            i.invoiceNum = invoiceNum
            i.setNum = setNum
            i.paidAmount = paidAmount
            i.adjustmentsAmount = adjustmentsAmount
            i.referenceNum = referenceNum
            i.buyerName = buyerName
            i.carrier = carrier
            i.sellerType = sellerType
            i.containerNum = containerNum
            i.paymentStatus = paymentStatus
            i.approvalStatus = approvalStatus
            i.encodedOn = encodedOn
            i.encodedBy = encodedBy
            i.dateCreated = dateCreated
            dc.SubmitChanges()
        Next
    End Sub

    Sub Delete()
        Dim srb = From i In dc.trans_SalesInvoiceBuyers Where i.salesInvoiceBuyerID = salesInvoiceBuyerID Select i

        For Each i In srb
            dc.trans_SalesInvoiceBuyers.DeleteOnSubmit(i)
            dc.SubmitChanges()
        Next
    End Sub

    Sub Posted()
        Dim sr = From i In dc.trans_SalesInvoiceBuyers Where i.salesInvoiceBuyerID = salesInvoiceBuyerID Select i

        For Each i In sr
            i.referenceNum = GenerateRefNum()
            i.approvalStatus = approvalStatus
            dc.SubmitChanges()
        Next
    End Sub

    Function getRows() As List(Of SalesInvoiceBuyer)
        Dim sibList = New List(Of SalesInvoiceBuyer)

        Dim e = (From i In dc.trans_SalesInvoiceBuyers Select i).ToList

        For Each i In e
            sibList.Add(New SalesInvoiceBuyer(i, dc))
        Next

        Return sibList
    End Function

    Function getByDate(Optional ByVal startDate As Date = #1/1/1900#, Optional ByVal endDate As Date = Nothing) As List(Of SalesInvoiceBuyer)
        If endDate = Nothing Then
            endDate = Date.Now
        End If

        If startDate = Nothing Then
            startDate = #1/1/1900#
        End If

        Dim sibList As New List(Of SalesInvoiceBuyer)

        Dim sibs = From sib In dc.trans_SalesInvoiceBuyers
                   Where sib.encodedOn >= startDate.Date AndAlso sib.encodedOn <= endDate.Date
                   Select sib

        For Each sib In sibs
            sibList.Add(New SalesInvoiceBuyer(sib, dc))
        Next

        Return sibList
    End Function

    Function GenerateRefNum() As String
        Dim yearMonth = Date.Now.ToString("yyyyMM") ' Format year and month as YYYYMM

        Dim prefix As String = "SIB-" & yearMonth

        ' Get the last reference number that starts with the prefix
        Dim lastRef = (From sr In dc.trans_SalesInvoiceBuyers
                       Where sr.referenceNum.StartsWith(prefix)
                       Order By sr.referenceNum Descending
                       Select sr.referenceNum).FirstOrDefault()

        Dim newNum As Integer

        If lastRef IsNot Nothing Then
            ' Extract the numeric part of the last reference number
            Dim lastNumStr As String = lastRef.Substring(9) ' Get the part after "SR-YYYYMM"
            Dim lastNum As Integer
            If Integer.TryParse(lastNumStr, lastNum) Then
                newNum = lastNum + 1 ' Increment the last number
            End If
        Else
            newNum = 1 ' Start from 1 if no previous reference number exists
        End If

        ' Format the new reference number to ensure it has 3 digits
        Return String.Format("{0}{1:D3}", prefix, newNum)
    End Function
End Class
