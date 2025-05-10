Imports System.ComponentModel
Imports System.Text


Partial Public Class frmMain

    Public ucList As New List(Of ucBase)

    Shared Sub New()
        DevExpress.UserSkins.BonusSkins.Register()
    End Sub

    Sub New()
        InitializeComponent()
    End Sub

    Private Sub addTab(ByVal uc As ucBase, ByVal title As String)
        Dim existingTabIndex As Integer = InTabs(title)

        If existingTabIndex <> -1 Then
            xtraTab.SelectedTabPage = xtraTab.TabPages(existingTabIndex)
            Return
        End If

        ucList.Add(uc)
        Dim newTab As New DevExpress.XtraTab.XtraTabPage With {.Text = title}
        xtraTab.TabPages.Add(newTab)

        uc.Parent = newTab
        uc.Dock = DockStyle.Fill
        xtraTab.SelectedTabPage = newTab
    End Sub

    Private Function InTabs(title As String) As Integer
        Dim normalizedTitle As String = title.Trim().ToLower()

        For i As Integer = 0 To xtraTab.TabPages.Count - 1
            If xtraTab.TabPages(i).Text.Trim().ToLower() = normalizedTitle Then
                Return i
            End If
        Next

        Return -1
    End Function

    Private Sub btn_salesInvc_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btn_salesInvc.ItemClick
        Dim title = "List Of Sales Invoice"
        Dim intab = InTabs(title)

        If intab = -1 Then
            Dim uc As New ucSales(title)
            addTab(uc, title)
        Else
            xtraTab.SelectedTabPage = xtraTab.TabPages(intab)
        End If
    End Sub

    Private Sub xtraTab_CloseButtonClick(sender As Object, e As EventArgs) Handles xtraTab.CloseButtonClick
        Dim page As DevExpress.XtraTab.XtraTabPage = TryCast(xtraTab.SelectedTabPage, DevExpress.XtraTab.XtraTabPage)

        If page IsNot Nothing Then
            Dim ucToRemove = ucList.FirstOrDefault(Function(uc) uc.Parent Is page)

            If ucToRemove IsNot Nothing Then
                ucList.Remove(ucToRemove)
            End If

            xtraTab.TabPages.Remove(page)
        End If
    End Sub


    'Private Sub btn_weightSlips_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btn_weightSlips.ItemClick
    '    Dim title = "Weight Slip"
    '    Dim intab = InTabs("ucWeightSlip")
    '    If intab = -1 Then
    '        Dim uc As New ucWeightSlip(title)
    '        addTab(uc, title)
    '    Else
    '        xtraTab.TabPages(intab).Show()
    '    End If
    'End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim title = "Catcher Activities"
        Dim intab = InTabs("ucCatcher")
        If intab = -1 Then
            Dim uc As New ucCatcher(title)
            addTab(uc, title)
        Else
            xtraTab.TabPages(intab).Show()
        End If
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim title = "Buyer Sales"
        Dim intab = InTabs("ucBuyer")
        If intab = -1 Then
            Dim uc As New ucBuyer(title)
            addTab(uc, title)
        Else
            xtraTab.TabPages(intab).Show()
        End If
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick

    End Sub
End Class
