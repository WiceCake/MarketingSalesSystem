Imports System.Transactions

Public Class ctrlCatchers

    Private isNew As Boolean
    Private mdlCA As CatchActivity
    Private mdlCAD As CatchActivityDetail

    Private frmCA As frm_catchActivity

    Private ucC As ucCatcher

    'Private ucS As ucSales

    Private mkdb As mkdbDataContext

    Sub New(uc As ucCatcher)
        isNew = True

        mkdb = New mkdbDataContext

        mdlCA = New CatchActivity(mkdb)
        mdlCAD = New CatchActivityDetail(mkdb)

        frmCA = New frm_catchActivity(Me)

        ucC = uc

        initCatcherDataTable()

        frmCA.GridControl1.DataSource = frmCA.dt

        With frmCA
            .btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            .btnPost.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            .Text = "Create Catch Activity"
            loadCombo()
            loadCatcher()
            .Show()
        End With
    End Sub

    Sub New(ByRef uc As ucCatcher, ByVal caID As Integer)
        isNew = False

        mkdb = New mkdbDataContext

        mdlCA = New CatchActivity(caID, mkdb)

        Dim mdlCAD_ID = (From i In mkdb.trans_CatchActivityDetails Where i.catchActivity_ID = mdlCA.catchActivity_ID Select i.catchActivityDetail_ID).First
        mdlCAD = New CatchActivityDetail(mdlCAD_ID, mkdb)

        frmCA = New frm_catchActivity(Me)

        ucC = uc

        initCatcherDataTable()

        frmCA.GridControl1.DataSource = frmCA.dt

        With frmCA
            If mdlCA.approvalStatus = 1 Then
                .ribbonTools.Visible = False
            End If
            .layoutBtnAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            .layoutBtnDelete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            .Text = "Catch Activity"

            .dtCreated.EditValue = mdlCA.catchDate
            .cmbMethod.EditValue = mdlCA.method_ID
            .txtLat.EditValue = mdlCA.latitude
            .txtLong.EditValue = mdlCA.longitude
            .txt_catNum.Caption = mdlCA.catchReferenceNum
            loadCombo()
            loadCatcher()
            .Show()
        End With
    End Sub

    Sub loadCombo()
        Dim methods = (From i In mkdb.trans_CatchMethods Where i.active = True Select i.catchMethod_ID, i.catchMethod).ToList
        lookUpTransMode(methods, frmCA.cmbMethod, "catchMethod", "catchMethod_ID", "Select method")
    End Sub

    Sub initCatcherDataTable()
        With frmCA
            .dt = New DataTable

            .dt.Columns.Add("Catcher", GetType(String))
        End With
    End Sub

    Sub save()
        Using ts As New TransactionScope()
            Try
                With mdlCA
                    .catchDate = CDate(frmCA.dtCreated.EditValue)
                    .latitude = CDec(frmCA.txtLat.EditValue)
                    .longitude = CDec(frmCA.txtLong.EditValue)
                    .method_ID = CInt(frmCA.cmbMethod.EditValue)
                    If isNew Then
                        .approvalStatus = Approval_Status.Draft
                        .catchReferenceNum = "Draft"
                        .Add()
                        SuccessfullyAddedUpdatedMessage()
                    Else
                        .approvalStatus = Approval_Status.Submitted
                        .catchReferenceNum = frmCA.txt_catNum.Caption
                        .Save()
                        SuccessfullyAddedUpdatedMessage()
                    End If
                End With

                Dim vesselIDs = (From i In mkdb.trans_CatchActivityDetails Where i.catchActivity_ID = mdlCA.catchActivity_ID Select i.catchActivityDetail_ID).ToList()
                Dim count = 0
                For Each item As DataRow In frmCA.dt.Rows
                    With mdlCAD
                        .catchActivity_ID = mdlCA.catchActivity_ID
                        .vessel_ID = CInt(item("Catcher"))
                        If isNew Then
                            .Add()
                        Else
                            .catchActivityDetail_ID = vesselIDs(count)
                            .Save()
                            count += 1
                        End If
                    End With
                Next
                ts.Complete()
            Catch ex As Exception
                Debug.WriteLine("Error: " & ex.Message)
            End Try
        End Using
        ucC.loadGrid()
        frmCA.Close()
    End Sub

    Sub postedDraft()
        Using ts As New TransactionScope
            Try
                With mdlCA
                    .approvalStatus = Approval_Status.Posted
                    .Posted()
                End With

                ts.Complete()
            Catch ex As Exception
                Debug.WriteLine("Error: " & ex.Message)
            End Try
        End Using
        SuccessfullySavedMessage()
        ucC.refreshData()
        frmCA.Close()
    End Sub

    Sub loadCatcher()
        Dim dr As DataRow = frmCA.dt.NewRow()

        If Not isNew Then
            Dim cadList = (From i In mkdb.trans_CatchActivityDetails Where i.catchActivity_ID = mdlCA.catchActivity_ID Select i).ToList

            For Each cad In cadList
                dr = frmCA.dt.NewRow()
                dr("Catcher") = cad.vessel_ID
                frmCA.dt.Rows.Add(dr)
            Next
            Return
        End If

        dr("Catcher") = ""
        frmCA.dt.Rows.Add(dr)
    End Sub

    Sub addCatcher()
        Dim dr As DataRow = frmCA.dt.NewRow()

        dr("Catcher") = ""

        frmCA.dt.Rows.Add(dr)
    End Sub

    Sub deleteCatch()
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
        ucC.loadGrid()
        frmCA.Close()
    End Sub
End Class
