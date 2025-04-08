Imports DevExpress.XtraEditors
Imports System.Transactions
Public Class ctrlCarrier
    Private isNew As Boolean

    Public Property CarrierTitle As String

    Private mdlCA As CatchActivity
    Private mdlCAD As CatchActivityDetail

    Private frmC As frm_carriersFC

    Private mkdb As mkdbDataContext


    Sub New(ByVal carrierTitle As String)
        isNew = True
        mkdb = New mkdbDataContext
        mdlCA = New CatchActivity(mkdb)
        mdlCAD = New CatchActivityDetail(mkdb)

        Me.CarrierTitle = carrierTitle
        frmC = New frm_carriersFC(Me, carrierTitle)

        With frmC
            .Show()
        End With
    End Sub


    Sub New(ByVal CarrierID As Integer)
        isNew = False

    End Sub

End Class
