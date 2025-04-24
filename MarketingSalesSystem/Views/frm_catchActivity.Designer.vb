<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_catchActivity
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_catchActivity))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarHeaderItem1 = New DevExpress.XtraBars.BarHeaderItem()
        Me.txt_catNum = New DevExpress.XtraBars.BarStaticItem()
        Me.btnSave = New DevExpress.XtraBars.BarButtonItem()
        Me.btnDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.btnPost = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbnActions = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbnTools = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.btnDelete1 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddCatcher = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutBtnAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.layoutBtnDelete = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtLong = New DevExpress.XtraEditors.TextEdit()
        Me.txtLat = New DevExpress.XtraEditors.TextEdit()
        Me.cmbMethod = New DevExpress.XtraEditors.LookUpEdit()
        Me.dtCreated = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutBtnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutBtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLong.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMethod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtCreated.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtCreated.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.BarHeaderItem1, Me.txt_catNum, Me.btnSave, Me.btnDelete, Me.btnPost, Me.BarButtonItem1})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(909, 162)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'BarHeaderItem1
        '
        Me.BarHeaderItem1.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold
        Me.BarHeaderItem1.Appearance.Options.UseFont = True
        Me.BarHeaderItem1.Caption = "Catch Reference No."
        Me.BarHeaderItem1.Id = 1
        Me.BarHeaderItem1.Name = "BarHeaderItem1"
        '
        'txt_catNum
        '
        Me.txt_catNum.Caption = "Draft"
        Me.txt_catNum.Id = 2
        Me.txt_catNum.Name = "txt_catNum"
        '
        'btnSave
        '
        Me.btnSave.Caption = "Save"
        Me.btnSave.Id = 3
        Me.btnSave.ImageOptions.Image = CType(resources.GetObject("btnSave.ImageOptions.Image"), System.Drawing.Image)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnDelete
        '
        Me.btnDelete.Caption = "Delete"
        Me.btnDelete.Id = 4
        Me.btnDelete.ImageOptions.Image = CType(resources.GetObject("btnDelete.ImageOptions.Image"), System.Drawing.Image)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnPost
        '
        Me.btnPost.Caption = "Post"
        Me.btnPost.Id = 5
        Me.btnPost.ImageOptions.Image = CType(resources.GetObject("btnPost.ImageOptions.Image"), System.Drawing.Image)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Print"
        Me.BarButtonItem1.Id = 7
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.rbnActions, Me.rbnTools})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Home"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.AllowTextClipping = False
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarHeaderItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.txt_catNum)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "Information"
        '
        'rbnActions
        '
        Me.rbnActions.AllowTextClipping = False
        Me.rbnActions.ItemLinks.Add(Me.btnSave)
        Me.rbnActions.ItemLinks.Add(Me.btnDelete)
        Me.rbnActions.ItemLinks.Add(Me.btnPost)
        Me.rbnActions.Name = "rbnActions"
        Me.rbnActions.Text = "Actions"
        '
        'rbnTools
        '
        Me.rbnTools.AllowTextClipping = False
        Me.rbnTools.ItemLinks.Add(Me.BarButtonItem1)
        Me.rbnTools.Name = "rbnTools"
        Me.rbnTools.Text = "Tools"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 639)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(909, 26)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.LayoutControl2)
        Me.LayoutControl1.Controls.Add(Me.txtLong)
        Me.LayoutControl1.Controls.Add(Me.txtLat)
        Me.LayoutControl1.Controls.Add(Me.cmbMethod)
        Me.LayoutControl1.Controls.Add(Me.dtCreated)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 162)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1128, 355, 650, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(909, 477)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.btnDelete1)
        Me.LayoutControl2.Controls.Add(Me.btnAddCatcher)
        Me.LayoutControl2.Controls.Add(Me.GridControl1)
        Me.LayoutControl2.Location = New System.Drawing.Point(11, 58)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(887, 409)
        Me.LayoutControl2.TabIndex = 8
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'btnDelete1
        '
        Me.btnDelete1.ImageOptions.Image = CType(resources.GetObject("btnDelete1.ImageOptions.Image"), System.Drawing.Image)
        Me.btnDelete1.Location = New System.Drawing.Point(445, 10)
        Me.btnDelete1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnDelete1.Name = "btnDelete1"
        Me.btnDelete1.Size = New System.Drawing.Size(213, 36)
        Me.btnDelete1.StyleController = Me.LayoutControl2
        Me.btnDelete1.TabIndex = 7
        Me.btnDelete1.Text = "Delete"
        '
        'btnAddCatcher
        '
        Me.btnAddCatcher.ImageOptions.Image = CType(resources.GetObject("btnAddCatcher.ImageOptions.Image"), System.Drawing.Image)
        Me.btnAddCatcher.Location = New System.Drawing.Point(662, 10)
        Me.btnAddCatcher.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAddCatcher.Name = "btnAddCatcher"
        Me.btnAddCatcher.Size = New System.Drawing.Size(214, 36)
        Me.btnAddCatcher.StyleController = Me.LayoutControl2
        Me.btnAddCatcher.TabIndex = 5
        Me.btnAddCatcher.Text = "Add Catcher"
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GridControl1.Location = New System.Drawing.Point(11, 50)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GridControl1.MenuManager = Me.RibbonControl
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(865, 349)
        Me.GridControl1.TabIndex = 4
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 284
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6, Me.layoutBtnAdd, Me.EmptySpaceItem1, Me.layoutBtnDelete})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(887, 409)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.GridControl1
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 40)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(869, 353)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'layoutBtnAdd
        '
        Me.layoutBtnAdd.Control = Me.btnAddCatcher
        Me.layoutBtnAdd.Location = New System.Drawing.Point(651, 0)
        Me.layoutBtnAdd.Name = "layoutBtnAdd"
        Me.layoutBtnAdd.Size = New System.Drawing.Size(218, 40)
        Me.layoutBtnAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutBtnAdd.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(434, 40)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'layoutBtnDelete
        '
        Me.layoutBtnDelete.Control = Me.btnDelete1
        Me.layoutBtnDelete.Location = New System.Drawing.Point(434, 0)
        Me.layoutBtnDelete.Name = "layoutBtnDelete"
        Me.layoutBtnDelete.Size = New System.Drawing.Size(217, 40)
        Me.layoutBtnDelete.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutBtnDelete.TextVisible = False
        '
        'txtLong
        '
        Me.txtLong.Location = New System.Drawing.Point(604, 34)
        Me.txtLong.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtLong.MenuManager = Me.RibbonControl
        Me.txtLong.Name = "txtLong"
        Me.txtLong.Properties.Mask.EditMask = "f9"
        Me.txtLong.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtLong.Properties.MaxLength = 13
        Me.txtLong.Size = New System.Drawing.Size(241, 20)
        Me.txtLong.StyleController = Me.LayoutControl1
        Me.txtLong.TabIndex = 7
        '
        'txtLat
        '
        Me.txtLat.Location = New System.Drawing.Point(604, 10)
        Me.txtLat.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtLat.MenuManager = Me.RibbonControl
        Me.txtLat.Name = "txtLat"
        Me.txtLat.Properties.Mask.EditMask = "f9"
        Me.txtLat.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtLat.Properties.MaxLength = 13
        Me.txtLat.Size = New System.Drawing.Size(241, 20)
        Me.txtLat.StyleController = Me.LayoutControl1
        Me.txtLat.TabIndex = 6
        '
        'cmbMethod
        '
        Me.cmbMethod.Location = New System.Drawing.Point(166, 34)
        Me.cmbMethod.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbMethod.MenuManager = Me.RibbonControl
        Me.cmbMethod.Name = "cmbMethod"
        Me.cmbMethod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMethod.Size = New System.Drawing.Size(250, 20)
        Me.cmbMethod.StyleController = Me.LayoutControl1
        Me.cmbMethod.TabIndex = 5
        '
        'dtCreated
        '
        Me.dtCreated.EditValue = Nothing
        Me.dtCreated.Location = New System.Drawing.Point(166, 10)
        Me.dtCreated.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dtCreated.MenuManager = Me.RibbonControl
        Me.dtCreated.Name = "dtCreated"
        Me.dtCreated.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtCreated.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtCreated.Size = New System.Drawing.Size(250, 20)
        Me.dtCreated.StyleController = Me.LayoutControl1
        Me.dtCreated.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.EmptySpaceItem3, Me.EmptySpaceItem4, Me.EmptySpaceItem2, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(909, 477)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.dtCreated
        Me.LayoutControlItem1.Location = New System.Drawing.Point(51, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(358, 24)
        Me.LayoutControlItem1.Text = "Date: "
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(101, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.cmbMethod
        Me.LayoutControlItem2.Location = New System.Drawing.Point(51, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(358, 24)
        Me.LayoutControlItem2.Text = "Method of Catching: "
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(101, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtLat
        Me.LayoutControlItem3.Location = New System.Drawing.Point(489, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(349, 24)
        Me.LayoutControlItem3.Text = "Latitude: "
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(101, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.txtLong
        Me.LayoutControlItem4.Location = New System.Drawing.Point(489, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(349, 24)
        Me.LayoutControlItem4.Text = "Longitude: "
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(101, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(409, 0)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(80, 48)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(838, 0)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(53, 48)
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(51, 48)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.LayoutControl2
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(891, 413)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'frm_catchActivity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(909, 665)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frm_catchActivity"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "frm_catchActivity"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutBtnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutBtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLong.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMethod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtCreated.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtCreated.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents BarHeaderItem1 As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents txt_catNum As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents rbnActions As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents btnSave As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnDelete As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnPost As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents dtCreated As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbMethod As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtLong As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents btnAddCatcher As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutBtnAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnDelete1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents layoutBtnDelete As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbnTools As DevExpress.XtraBars.Ribbon.RibbonPageGroup


End Class
