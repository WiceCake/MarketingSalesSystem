Imports DevExpress.XtraTab
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.BandedGrid
Public Class frm_salesInvoice

    Private tabControl As XtraTabControl
    Private txtNote As TextBox
    Private grid As GridControl

    Private ctrlSales As ctrlSales

    Sub New(ByRef ctrlS As ctrlSales)

        InitializeComponent()

        ctrlSales = ctrlS

        tabControl = New XtraTabControl() With {
            .Dock = DockStyle.Fill
        }

        LayoutControl2.Controls.Add(tabControl)

        Dim layoutItem As LayoutControlItem = LayoutControl2.AddItem("", tabControl)
        layoutItem.TextVisible = False

        Dim adGrid = setTab(AddTab(tabControl, "Amount Details"))
        Dim sGrid = setTab(AddTab(tabControl, "Summary"))

        displayAmountDetails(adGrid)
        displaySummary(sGrid)

        txtNote = New TextBox() With {
            .Multiline = True,
            .Height = 80,
            .Dock = DockStyle.Bottom
        }
        LayoutControl2.Controls.Add(txtNote)

        Dim noteLayoutItem As LayoutControlItem = LayoutControl2.AddItem("Note:", txtNote)
        noteLayoutItem.TextVisible = True

    End Sub

    Function setTab(ByRef tab As XtraTabPage) As GridControl
        Dim layout = New LayoutControl With {
                        .Dock = DockStyle.Fill
                    }
        tab.Controls.Add(layout)
        Dim grid = New GridControl() With {
            .Dock = DockStyle.Fill
        }
        layout.Controls.Add(grid)
        Dim layoutItem As LayoutControlItem = layout.AddItem("", grid)
        layoutItem.TextVisible = False

        Return grid
    End Function

    Sub displayAmountDetails(ByRef grid As GridControl)
        Dim gridView As New GridView(grid)
        grid.MainView = gridView
        grid.ViewCollection.Add(gridView)

        gridView.OptionsView.ShowFooter = True
        gridView.OptionsView.AllowCellMerge = True

        grid.ForceInitialize()

        AddHandler gridView.CustomDrawFooter, AddressOf GridView_CustomDrawFooter
    End Sub

    Sub displaySummary(ByRef grid As GridControl)
        Dim bandedView As New BandedGridView(grid)
        grid.MainView = bandedView
        grid.ViewCollection.Add(bandedView)

        bandedView.OptionsView.ShowFooter = True
        bandedView.OptionsView.AllowCellMerge = False

        Dim bandCatcher As GridBand = CreateCenteredBand("CATCHER")
        Dim bandTonnage As GridBand = CreateCenteredBand("TONNAGE")
        Dim bandKilos As GridBand = CreateCenteredBand("KILOS")
        Dim bandAmount As GridBand = CreateCenteredBand("AMOUNT")

        bandTonnage.Children.Add(CreateCenteredBand("CATCHERS PARTIAL"))
        bandTonnage.Children.Add(CreateCenteredBand(" "))
        bandTonnage.Children.Add(CreateCenteredBand("ACTUAL QTY"))

        bandKilos.Children.Add(CreateCenteredBand("ACTUAL QTY"))
        bandKilos.Children.Add(CreateCenteredBand("FISHMEAL"))
        bandKilos.Children.Add(CreateCenteredBand("SPOILAGE"))
        bandKilos.Children.Add(CreateCenteredBand("NET"))

        bandAmount.Children.Add(CreateCenteredBand("ACTUAL QTY"))
        bandAmount.Children.Add(CreateCenteredBand("FISHMEAL"))
        bandAmount.Children.Add(CreateCenteredBand("SPOILAGE"))
        bandAmount.Children.Add(CreateCenteredBand("NET IN USD"))
        bandAmount.Children.Add(CreateCenteredBand("NET IN PHP"))
        bandAmount.Children.Add(CreateCenteredBand(" "))
        bandAmount.Children.Add(CreateCenteredBand("AVERAGE PRICE PER CATCHER"))

        bandedView.Bands.AddRange(New GridBand() {bandCatcher, bandTonnage, bandKilos, bandAmount})

        For Each band As GridBand In bandedView.Bands
            ApplyCenterAlignment(band)
        Next

        grid.ForceInitialize()
    End Sub

    Function CreateCenteredBand(caption As String) As GridBand
        Dim band As New GridBand() With {.Caption = caption}
        band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        band.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        band.AppearanceHeader.Options.UseTextOptions = True
        Return band
    End Function

    Sub ApplyCenterAlignment(band As GridBand)
        band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        band.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        band.AppearanceHeader.Options.UseTextOptions = True

        For Each subBand As GridBand In band.Children
            ApplyCenterAlignment(subBand)
        Next
    End Sub




    Private Sub GridView_CustomDrawFooter(sender As Object, e As Views.Base.RowObjectCustomDrawEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        If view Is Nothing Then Exit Sub

        e.DefaultDraw()

        Dim footerRect As Rectangle = e.Bounds

        Dim footerFont As New Font("Arial", 10, FontStyle.Bold)

        Dim drawFormat As New StringFormat()
        drawFormat.LineAlignment = StringAlignment.Center

        e.Graphics.DrawString(" Total:", footerFont, Brushes.Black, footerRect, drawFormat)

        e.Handled = True
    End Sub

End Class