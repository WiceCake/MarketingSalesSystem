Public Class CatchersActivity
    'Catch Activity
    Public Property catchActivity_ID As Integer
    Public Property catchDate As Date
    Public Property method_ID As Integer
    Public Property longitude As Decimal
    Public Property latitude As Decimal
    Public Property catchReferenceNum As String
    Public Property approvalStatus As Integer

    'Catch Activity Detail
    Public Property catchActivityDetail_ID As Integer
    Public Property vessel_ID As Integer

    'Catch Method
    Public Property catchMethod_ID As Integer
    Public Property catchMethod As String

    Sub New()

    End Sub
End Class
