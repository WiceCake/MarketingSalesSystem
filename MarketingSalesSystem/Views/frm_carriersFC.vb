Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Repository
Public Class frm_carriersFC
    Private ctrlC As ctrlCarrier

    Sub New(ByRef ctrl As ctrlCatchers)
        InitializeComponent()

        GridView1.OptionsSelection.MultiSelect = True
        GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub


    Private Sub GridControl1_Load(sender As Object, e As EventArgs) Handles GridControl1.Load


    End Sub
End Class