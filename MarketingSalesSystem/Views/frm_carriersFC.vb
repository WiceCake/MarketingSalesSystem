Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Repository
Public Class frm_carriersFC
    Private ctrlC As ctrlCarrier
    Public Property CarrierType As String

    Sub New(ByRef ctrl_c As ctrlCarrier, ByVal title As String)
        InitializeComponent()

        ctrlC = ctrl_c
        Me.CarrierType = title
        FormTitle.Text = title ' Set the label here

        GridView2.OptionsSelection.MultiSelect = True
        GridView2.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
    End Sub

    Private Sub GridControl1_Load(sender As Object, e As EventArgs)
        Dim lookupEdit As New RepositoryItemLookUpEdit()

        Dim dc = New tpmdbDataContext

    End Sub

End Class