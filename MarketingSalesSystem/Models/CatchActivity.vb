Public Class CatchActivity
    Public catchActivity_ID As Integer
    Public catchDate As Date
    Public method_ID As Integer
    Public longitude As Decimal
    Public latitude As Decimal
    Public catchReferenceNum As String
    Public approvalStatus As Integer

    Private dc As mkdbDataContext

    Sub New(ByRef dc_ As mkdbDataContext)
        dc = dc_
    End Sub

    Sub New(ByVal catchActivityID As Integer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim e = From i In dc.trans_CatchActivities Where i.catchActivity_ID = catchActivityID Select i

        For Each i In e
            Me.catchActivity_ID = catchActivityID
            catchDate = i.catchDate
            method_ID = i.method_ID
            longitude = i.longitude
            latitude = i.latitude
            catchReferenceNum = i.catchReferenceNum
            approvalStatus = i.approvalStatus
        Next

    End Sub

    Sub New(ByRef sr As trans_CatchActivity, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim i = sr

        Me.catchActivity_ID = i.catchActivity_ID
        catchDate = i.catchDate
        method_ID = i.method_ID
        longitude = i.longitude
        latitude = i.latitude
        catchReferenceNum = i.catchReferenceNum
        approvalStatus = i.approvalStatus
    End Sub

    Sub Add()
        Dim ca = New trans_CatchActivity

        With ca
            .catchDate = catchDate
            .method_ID = method_ID
            .longitude = longitude
            .latitude = latitude
            .catchReferenceNum = catchReferenceNum
            .approvalStatus = approvalStatus
        End With

        dc.trans_CatchActivities.InsertOnSubmit(ca)
        dc.SubmitChanges()
        Me.catchActivity_ID = ca.catchActivity_ID
    End Sub

    Sub Save()
        Dim sca = From i In dc.trans_CatchActivities Where i.catchActivity_ID = catchActivity_ID Select i

        For Each i In sca
            i.catchActivity_ID = catchActivity_ID
            i.catchDate = catchDate
            i.method_ID = method_ID
            i.catchReferenceNum = catchReferenceNum
            i.latitude = latitude
            i.longitude = longitude
            i.approvalStatus = approvalStatus
        Next
    End Sub

    Sub Delete()
        Dim sca = From i In dc.trans_CatchActivities Where i.catchActivity_ID = catchActivity_ID Select i

        For Each i In sca
            dc.trans_CatchActivities.DeleteOnSubmit(i)
            dc.SubmitChanges()
        Next
    End Sub

    Function getRows() As List(Of CatchActivity)
        Dim caList As New List(Of CatchActivity)

        Dim cas = From item In dc.trans_CatchActivities Select item

        For Each ca In cas
            caList.Add(New CatchActivity(ca, dc))
        Next

        Return caList
    End Function

    Function getByDate(Optional ByVal startDate As Date = #1/1/1900#, Optional ByVal endDate As Date = Nothing) As List(Of CatchActivity)
        If endDate = Nothing Then
            endDate = Date.Now
        End If

        If startDate = Nothing Then
            startDate = #1/1/1900#
        End If

        Dim caList As New List(Of CatchActivity)

        Dim cas = From ca In dc.trans_CatchActivities
                   Where ca.catchDate >= startDate.Date AndAlso ca.catchDate <= endDate.Date
                   Select ca

        For Each ca In cas
            caList.Add(New CatchActivity(ca, dc))
        Next

        Return caList
    End Function

    Function GenerateRefNum() As String
        Dim yearMonth = Date.Now.ToString("yyyyMM")
        'Dim yearMonth = Date.Now.Year & Date.Now.Month

        Dim prefix As String = "CA-" & yearMonth

        Dim lastRef = (From sr In dc.trans_CatchActivities
                       Where sr.catchReferenceNum.StartsWith(prefix)
                       Order By sr.catchReferenceNum Descending
                       Select sr.catchReferenceNum).FirstOrDefault()

        Dim newNum As Integer

        If lastRef IsNot Nothing Then
            Dim lastNumStr As String = lastRef.Substring(9) ' Get the part after "SR-YYYYMM"
            Dim lastNum As Integer
            If Integer.TryParse(lastNumStr, lastNum) Then
                newNum = lastNum + 1 ' Increment the last number
            End If
        Else
            newNum = 1 ' Start from 1 if no previous reference number exists
        End If

        Return String.Format("{0}{1:D3}", prefix, newNum)
        'Return String.Format("CA-{0}{1:D3}", yearMonth, newNum)
    End Function
End Class
