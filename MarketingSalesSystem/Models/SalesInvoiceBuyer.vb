Public Class SalesInvoiceBuyer
    Public salesInvoiceBuyerID, salesInvoiceID As Integer
    Public buyerName, encodedBy, referenceNum, sellerType, setNum As String
    Public paidAmount, adjustmentsAmount As Decimal?
    Public encodedOn As Date
    Public dateCreated As Date?
    Public approvalStatus As Integer

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
            referenceNum = i.referenceNum
            setNum = i.setNum
            buyerName = i.buyerName
            sellerType = i.sellerType
            paidAmount = i.paidAmount
            adjustmentsAmount = i.adjustmentsAmount
            encodedBy = i.encodedBy
            encodedOn = i.encodedOn
            dateCreated = i.dateCreated
            approvalStatus = i.approvalStatus
        Next
    End Sub

    Sub New(sib As trans_SalesInvoiceBuyer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        salesInvoiceBuyerID = sib.salesInvoiceBuyerID
        salesInvoiceID = sib.salesInvoiceID
        referenceNum = sib.referenceNum
        setNum = sib.setNum
        buyerName = sib.buyerName
        sellerType = sib.sellerType
        paidAmount = sib.paidAmount
        adjustmentsAmount = sib.adjustmentsAmount
        encodedBy = sib.encodedBy
        encodedOn = sib.encodedOn
        dateCreated = sib.dateCreated
        approvalStatus = sib.approvalStatus
    End Sub

    Sub Add()
        Dim sib As New trans_SalesInvoiceBuyer

        With sib
            .salesInvoiceID = salesInvoiceID
            .referenceNum = referenceNum
            .setNum = setNum
            .buyerName = buyerName
            .sellerType = sellerType
            .paidAmount = paidAmount
            .adjustmentsAmount = adjustmentsAmount
            .encodedOn = encodedOn
            .encodedBy = encodedBy
            .dateCreated = dateCreated
            .approvalStatus = approvalStatus
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
            i.referenceNum = referenceNum
            i.setNum = setNum
            i.buyerName = buyerName
            i.sellerType = sellerType
            i.paidAmount = paidAmount
            i.adjustmentsAmount = adjustmentsAmount
            i.encodedOn = encodedOn
            i.encodedBy = encodedBy
            i.dateCreated = dateCreated
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
