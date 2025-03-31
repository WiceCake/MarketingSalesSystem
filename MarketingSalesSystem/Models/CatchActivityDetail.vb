Public Class CatchActivityDetail
    Public catchActivityDetail_ID As Integer
    Public catchActivity_ID As Integer
    Public vessel_ID As Integer

    Private dc As mkdbDataContext

    Sub New(ByRef dc_ As mkdbDataContext)
        dc = dc_
    End Sub

    Sub New(ByVal catchActivityDetailID As Integer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim e = From i In dc.trans_CatchActivityDetails Where i.catchActivityDetail_ID = catchActivityDetailID Select i

        For Each i In e
            Me.catchActivityDetail_ID = catchActivityDetailID
            catchActivity_ID = i.catchActivity_ID
            vessel_ID = i.vessel_ID
        Next
    End Sub

    Sub New(ByRef cad As trans_CatchActivityDetail, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim i = cad

        Me.catchActivityDetail_ID = i.catchActivityDetail_ID
        catchActivity_ID = i.catchActivity_ID
        vessel_ID = i.vessel_ID
    End Sub

    Function getRows() As List(Of CatchActivityDetail)
        Dim cadList As New List(Of CatchActivityDetail)

        Dim cads = From item In dc.trans_CatchActivityDetails Select item

        For Each cad In cads
            cadList.Add(New CatchActivityDetail(cad, dc))
        Next

        Return cadList
    End Function

    Sub Add()
        Dim cad As New trans_CatchActivityDetail

        With cad
            .catchActivity_ID = catchActivity_ID
            .vessel_ID = vessel_ID
        End With

        dc.trans_CatchActivityDetails.InsertOnSubmit(cad)
        dc.SubmitChanges()
        Me.catchActivityDetail_ID = cad.catchActivityDetail_ID
    End Sub

    Sub Save()
        Dim cad = From i In dc.trans_CatchActivityDetails Where i.catchActivityDetail_ID = catchActivityDetail_ID Select i

        For Each i In cad
            i.catchActivityDetail_ID = catchActivityDetail_ID
            i.catchActivity_ID = catchActivity_ID
            i.vessel_ID = vessel_ID
            dc.SubmitChanges()
        Next
    End Sub

    Sub Delete()
        Dim sca = From i In dc.trans_CatchActivityDetails Where i.catchActivity_ID = catchActivity_ID Select i

        For Each i In sca
            dc.trans_CatchActivityDetails.DeleteOnSubmit(i)
            dc.SubmitChanges()
        Next
    End Sub
End Class
