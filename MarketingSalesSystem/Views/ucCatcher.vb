Imports DevExpress.XtraGrid
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid.Views.Grid

Public Class ucCatcher
    Inherits ucBase

    Public grid As GridControl

    Sub New(ByVal title As String)
        InitializeComponent()

        MyBase.title = title
        LabelControl1.Text = title

        loadData()

        AddHandler grid.Load, AddressOf gridLoaded

        dtFrom.EditValue = Nothing
        dtFrom.Properties.MaxValue = Date.Now
        dtTo.EditValue = Nothing
        dtTo.Properties.MaxValue = Date.Now

    End Sub

    Sub loadData()
        grid = New GridControl() With {
            .Dock = DockStyle.Fill
        }

        LayoutControl2.Controls.Add(grid)

        Dim layoutItem As LayoutControlItem = LayoutControl2.AddItem("", grid)
        layoutItem.TextVisible = False
    End Sub

    Sub gridLoaded(sender As Object, e As EventArgs)
        loadGrid()
    End Sub

    Sub loadGrid()
        Dim dc As New mkdbDataContext

        Dim caList = New CatchActivity(dc).getByDate(CDate(dtFrom.EditValue), CDate(dtTo.EditValue)).ToList()
        Dim cadList = (From ca In caList
                       Join cm In dc.trans_CatchMethods On cm.catchMethod_ID Equals ca.method_ID
                       Select New With {
                           .catchActivity_ID = ca.catchActivity_ID,
                           .CatchReferenceNumber = ca.catchReferenceNum,
                           .CatchDate = ca.catchDate,
                           .CatchMethod = cm.catchMethod,
                           .Longitude = ca.longitude,
                           .Latitude = ca.latitude
                           }).ToList

        Dim gridView = New GridView()
        AddHandler gridView.DoubleClick, AddressOf HandleGridDoubleClick
        gridView.GridControl = grid

        grid.MainView = gridView
        grid.ViewCollection.Add(gridView)


        gridView.GridControl.DataSource = cadList
        gridView.PopulateColumns()

        gridTransMode(gridView)
    End Sub

    Private Sub HandleGridDoubleClick(sender As Object, e As EventArgs)
        Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim value = gridView.GetRowCellValue(gridView.FocusedRowHandle, "catchActivity_ID")
        Dim ctrlCA = New ctrlCatchers(Me, CInt(value))
    End Sub

    Protected Overrides Function GetGridControl() As GridControl
        Return If(grid IsNot Nothing, grid, Nothing)
    End Function

    Overrides Sub refreshData()
        loadGrid()
    End Sub

    Public Overrides Sub openForm()
        Dim ctrlCA = New ctrlCatchers(Me)
    End Sub

End Class
