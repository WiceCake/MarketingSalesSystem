Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors

Public Class frm_carriersFC
    Private ctrlC As ctrlCarrier
    Public dt As DataTable

    Public Property CarrierType As String

    Sub New(ByRef ctrl_c As ctrlCarrier, ByVal title As String)
        InitializeComponent()

        ctrlC = ctrl_c
        Me.CarrierType = title
        FormTitle.Text = title

        If dt Is Nothing Then
            dt = New DataTable()
        End If

        If dt.Columns.Count = 0 Then
            dt.Columns.Add("Carrier")
            dt.Columns.Add("No. of Catches") ' Add the new column here
        End If


        GridControl2.DataSource = dt

        GridView2.OptionsSelection.MultiSelect = True
        GridView2.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub

    Private Sub GridControl1_Load(sender As Object, e As EventArgs)
        Dim lookupEdit As New RepositoryItemLookUpEdit()

        Dim dc = New tpmdbDataContext
    End Sub

    Private Sub btnAddCarrier_Click(sender As Object, e As EventArgs) Handles btnAddCarrier.Click
        ctrlC.addCarrier()
    End Sub

    Private Sub btnDelete1_Click(sender As Object, e As EventArgs) Handles btnDelete1.Click
        If GridView2.SelectedRowsCount < 1 Then
            XtraMessageBox.Show("Please select a row first!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim totalRows As Integer = GridView2.RowCount
        Dim selectedRowCount As Integer = GridView2.SelectedRowsCount

        If totalRows = 1 Then
            XtraMessageBox.Show("Cannot delete this last row!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If selectedRowCount = totalRows Then
            XtraMessageBox.Show("At least one row must remain. Cannot delete all rows!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirmation = XtraMessageBox.Show("Are you sure you want to delete the selected rows?", APPNAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If confirmation = Windows.Forms.DialogResult.Yes Then
            Dim selectedRows As Integer() = GridView2.GetSelectedRows()

            For i As Integer = selectedRows.Length - 1 To 0 Step -1
                Dim rowHandle As Integer = selectedRows(i)
                Dim row As DataRow = GridView2.GetDataRow(rowHandle)

                If row IsNot Nothing Then dt.Rows.Remove(row)
            Next

            GridControl2.RefreshDataSource()
            GridView2.RefreshData()
            GridView2.ClearSelection()
        End If
    End Sub

End Class
