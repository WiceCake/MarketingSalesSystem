Public Class SalesReport
    Public salesReport_ID As Integer
    Public referenceNum As String
    Public salesDate As Date
    Public salesNum As String
    Public sellingType As String
    Public unloadingType As String
    Public unloadingVessel_ID As Integer?
    Public unloadingForeignVessel As String
    Public buyer As String
    Public catchtDeliveryNum As String
    Public usdRate As Decimal
    Public contractNum As String
    Public remarks As String
    Public encodedBy As Integer
    Public encodedOn As Date
    Public approvalStatus As Integer

    Private dc As mkdbDataContext

    Sub New(ByRef dc_ As mkdbDataContext)
        dc = dc_
    End Sub

    Sub New(ByVal salesReportID As Integer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim e = From i In dc.trans_SalesReports Where i.salesReport_ID = salesReportID Select i

        For Each i In e
            Me.salesReport_ID = salesReportID
            referenceNum = i.referenceNum
            salesDate = i.salesDate
            salesNum = i.salesNum
            sellingType = i.sellingType
            unloadingType = i.unloadingType
            unloadingVessel_ID = i.unloadingVessel_ID
            unloadingForeignVessel = i.unloadingForeignVessel
            buyer = i.buyer
            catchtDeliveryNum = i.catchtDeliveryNum
            usdRate = i.usdRate
            contractNum = i.contractNum
            remarks = i.remarks
            encodedBy = i.encodedBy
            encodedOn = i.encodedOn
            approvalStatus = i.approvalStatus
        Next
    End Sub

    Sub New(ByRef sr As trans_SalesReport, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim i = sr

        Me.salesReport_ID = i.salesReport_ID
        referenceNum = i.referenceNum
        salesDate = i.salesDate
        salesNum = i.salesNum
        sellingType = i.sellingType
        unloadingType = i.unloadingType
        unloadingVessel_ID = i.unloadingVessel_ID
        unloadingForeignVessel = i.unloadingForeignVessel
        buyer = i.buyer
        catchtDeliveryNum = i.catchtDeliveryNum
        usdRate = i.usdRate
        contractNum = i.contractNum
        remarks = i.remarks
        encodedBy = i.encodedBy
        encodedOn = i.encodedOn
        approvalStatus = i.approvalStatus
    End Sub

    Sub Add()
        Dim sr As New trans_SalesReport

        With sr
            .salesDate = salesDate
            .referenceNum = "Draft"
            .salesNum = salesNum
            .sellingType = sellingType
            .unloadingType = unloadingType
            .unloadingVessel_ID = unloadingVessel_ID
            .unloadingForeignVessel = unloadingForeignVessel
            .buyer = buyer
            .catchtDeliveryNum = catchtDeliveryNum
            .usdRate = usdRate
            .contractNum = contractNum
            .remarks = remarks
            .encodedBy = encodedBy
            .encodedOn = encodedOn
            .approvalStatus = approvalStatus
        End With

        dc.trans_SalesReports.InsertOnSubmit(sr)
        dc.SubmitChanges()
        salesReport_ID = sr.salesReport_ID
    End Sub

    Sub Save()
        Dim sr = From i In dc.trans_SalesReports Where i.salesReport_ID = salesReport_ID Select i

        For Each i In sr
            i.salesDate = salesDate
            i.referenceNum = i.referenceNum
            i.salesNum = salesNum
            i.sellingType = sellingType
            i.unloadingType = unloadingType
            i.unloadingVessel_ID = unloadingVessel_ID
            i.unloadingForeignVessel = unloadingForeignVessel
            i.buyer = buyer
            i.catchtDeliveryNum = catchtDeliveryNum
            i.usdRate = usdRate
            i.contractNum = contractNum
            i.remarks = remarks
            i.encodedBy = encodedBy
            i.encodedOn = encodedOn
            i.approvalStatus = approvalStatus
            dc.SubmitChanges()
        Next

    End Sub

    Sub Delete()
        Dim sr = From i In dc.trans_SalesReports Where i.salesReport_ID = salesReport_ID Select i

        For Each i In sr
            dc.trans_SalesReports.DeleteOnSubmit(i)
            dc.SubmitChanges()
        Next

    End Sub

    Sub Posted()
        Dim sr = From i In dc.trans_SalesReports Where i.salesReport_ID = salesReport_ID Select i

        For Each i In sr
            i.referenceNum = GenerateRefNum()
            i.approvalStatus = approvalStatus
            dc.SubmitChanges()
        Next
    End Sub

    Function getRows() As List(Of SalesReport)
        Dim srList As New List(Of SalesReport)

        Dim srs = From item In dc.trans_SalesReports Select item

        For Each sr In srs
            srList.Add(New SalesReport(sr, dc))
        Next

        Return srList
    End Function

    Function getByDate(Optional ByVal startDate As Date = #1/1/1900#, Optional ByVal endDate As Date = Nothing) As List(Of SalesReport)
        If endDate = Nothing Then
            endDate = Date.Now
        End If

        If startDate = Nothing Then
            startDate = #1/1/1900#
        End If

        Dim srList As New List(Of SalesReport)

        Dim srs = From sr In dc.trans_SalesReports
                   Where sr.salesDate >= startDate.Date AndAlso sr.salesDate <= endDate.Date
                   Select sr

        For Each sr In srs
            srList.Add(New SalesReport(sr, dc))
        Next

        Return srList
    End Function

    Function GenerateRefNum() As String
        Dim yearMonth = Date.Now.Year & Date.Now.Month

        Dim prefix As String = "SR-" & yearMonth

        Dim lastRef = (From sr In dc.trans_SalesReports
                       Where sr.referenceNum.StartsWith(prefix)
                       Order By sr.referenceNum Descending
                       Select sr.referenceNum).FirstOrDefault()

        Dim newNum As Integer

        If lastRef IsNot Nothing Then
            Dim lastNumStr As String = lastRef.Substring(9) ' Extract the numeric part
            Dim lastNum As Integer
            If Integer.TryParse(lastNumStr, lastNum) Then
                newNum = lastNum + 1 ' Increment the last number
            End If
        Else
            newNum = 1 ' Start from 1 if no previous reference number exists
        End If

        Return String.Format("SR-{0}{1:D3}", yearMonth, newNum) ' Format to 3 digits
    End Function

End Class
