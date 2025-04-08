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

    Sub addCarrier()
        If frmC.dt IsNot Nothing Then
            Dim newRow As DataRow = frmC.dt.NewRow()

            newRow("Carrier") = "" ' Empty or default value
            newRow("No. of Catches") = 0 ' Default value for "No. of Catches"

            frmC.dt.Rows.Add(newRow)

            frmC.GridControl2.RefreshDataSource()
        Else
            XtraMessageBox.Show("DataTable is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    Sub deleteCarrier()
        If ConfirmDeleteMessage() = True Then
            Using ts As New TransactionScope
                Try
                    mdlCAD.catchActivity_ID = mdlCA.catchActivity_ID
                    mdlCA.Delete()
                    mdlCAD.Delete()

                    ts.Complete()
                Catch ex As Exception
                    Debug.WriteLine("Error: " & ex.Message)
                End Try
            End Using
        End If

        frmC.Close()
    End Sub

End Class
