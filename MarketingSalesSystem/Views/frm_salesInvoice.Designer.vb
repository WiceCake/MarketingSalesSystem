﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_salesInvoice
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_salesInvoice))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarHeaderItem1 = New DevExpress.XtraBars.BarHeaderItem()
        Me.txt_refNum = New DevExpress.XtraBars.BarStaticItem()
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
        Me.btnDeleteCarrier = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddCarrier = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gcCarrier = New DevExpress.XtraGrid.GridControl()
        Me.gvCarrier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Carrier = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.cmbCarrier = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.txtInvoiceNum = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.GridControl3 = New DevExpress.XtraGrid.GridControl()
        Me.BandedGridView3 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtUSD = New DevExpress.XtraEditors.TextEdit()
        Me.txtCDNum = New DevExpress.XtraEditors.TextEdit()
        Me.txtSaleNum = New DevExpress.XtraEditors.TextEdit()
        Me.dtCreated = New DevExpress.XtraEditors.DateEdit()
        Me.cmbST = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtCNum = New DevExpress.XtraEditors.TextEdit()
        Me.txtRemark = New DevExpress.XtraEditors.TextEdit()
        Me.cmbUV = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.f0 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem7 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem8 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem9 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.BandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridControl2 = New DevExpress.XtraGrid.GridControl()
        Me.BandedGridView2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.gcCarrier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCarrier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Carrier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCarrier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvoiceNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.GridControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUSD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCDNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSaleNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtCreated.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtCreated.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbST.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbUV.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.f0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.BarHeaderItem1, Me.txt_refNum, Me.btnSave, Me.btnDelete, Me.btnPost, Me.BarButtonItem1})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1209, 162)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'BarHeaderItem1
        '
        Me.BarHeaderItem1.Caption = "Reference No."
        Me.BarHeaderItem1.Id = 1
        Me.BarHeaderItem1.Name = "BarHeaderItem1"
        '
        'txt_refNum
        '
        Me.txt_refNum.Caption = "xxxxxxxxxx"
        Me.txt_refNum.Id = 2
        Me.txt_refNum.Name = "txt_refNum"
        '
        'btnSave
        '
        Me.btnSave.Caption = "Save"
        Me.btnSave.Id = 3
        Me.btnSave.ImageOptions.Image = CType(resources.GetObject("btnSave.ImageOptions.Image"), System.Drawing.Image)
        Me.btnSave.ImageOptions.LargeImage = CType(resources.GetObject("btnSave.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnSave.Name = "btnSave"
        '
        'btnDelete
        '
        Me.btnDelete.Caption = "Delete"
        Me.btnDelete.Id = 4
        Me.btnDelete.ImageOptions.Image = CType(resources.GetObject("btnDelete.ImageOptions.Image"), System.Drawing.Image)
        Me.btnDelete.ImageOptions.LargeImage = CType(resources.GetObject("btnDelete.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnDelete.Name = "btnDelete"
        '
        'btnPost
        '
        Me.btnPost.Caption = "Post"
        Me.btnPost.Id = 5
        Me.btnPost.ImageOptions.Image = CType(resources.GetObject("btnPost.ImageOptions.Image"), System.Drawing.Image)
        Me.btnPost.ImageOptions.LargeImage = CType(resources.GetObject("btnPost.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnPost.Name = "btnPost"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Print"
        Me.BarButtonItem1.Id = 7
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        Me.BarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.rbnActions, Me.rbnTools})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Home"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarHeaderItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.txt_refNum)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "Information"
        '
        'rbnActions
        '
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
        Me.rbnTools.ShowCaptionButton = False
        Me.rbnTools.Text = "Tools"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 839)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1209, 26)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.btnDeleteCarrier)
        Me.LayoutControl1.Controls.Add(Me.btnAddCarrier)
        Me.LayoutControl1.Controls.Add(Me.Label1)
        Me.LayoutControl1.Controls.Add(Me.gcCarrier)
        Me.LayoutControl1.Controls.Add(Me.txtInvoiceNum)
        Me.LayoutControl1.Controls.Add(Me.LayoutControl2)
        Me.LayoutControl1.Controls.Add(Me.txtUSD)
        Me.LayoutControl1.Controls.Add(Me.txtCDNum)
        Me.LayoutControl1.Controls.Add(Me.txtSaleNum)
        Me.LayoutControl1.Controls.Add(Me.dtCreated)
        Me.LayoutControl1.Controls.Add(Me.cmbST)
        Me.LayoutControl1.Controls.Add(Me.txtCNum)
        Me.LayoutControl1.Controls.Add(Me.txtRemark)
        Me.LayoutControl1.Controls.Add(Me.cmbUV)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 162)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1270, 333, 650, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1209, 677)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'btnDeleteCarrier
        '
        Me.btnDeleteCarrier.ImageOptions.Image = CType(resources.GetObject("btnDeleteCarrier.ImageOptions.Image"), System.Drawing.Image)
        Me.btnDeleteCarrier.Location = New System.Drawing.Point(1064, 10)
        Me.btnDeleteCarrier.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnDeleteCarrier.Name = "btnDeleteCarrier"
        Me.btnDeleteCarrier.Size = New System.Drawing.Size(101, 22)
        Me.btnDeleteCarrier.StyleController = Me.LayoutControl1
        Me.btnDeleteCarrier.TabIndex = 31
        Me.btnDeleteCarrier.Text = "Remove Carrier"
        '
        'btnAddCarrier
        '
        Me.btnAddCarrier.ImageOptions.Image = CType(resources.GetObject("btnAddCarrier.ImageOptions.Image"), System.Drawing.Image)
        Me.btnAddCarrier.Location = New System.Drawing.Point(963, 10)
        Me.btnAddCarrier.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAddCarrier.Name = "btnAddCarrier"
        Me.btnAddCarrier.Size = New System.Drawing.Size(97, 22)
        Me.btnAddCarrier.StyleController = Me.LayoutControl1
        Me.btnAddCarrier.TabIndex = 30
        Me.btnAddCarrier.Text = "Add Carrier"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(758, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(201, 22)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Add carrier local and foreign:"
        '
        'gcCarrier
        '
        Me.gcCarrier.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gcCarrier.Location = New System.Drawing.Point(758, 46)
        Me.gcCarrier.MainView = Me.gvCarrier
        Me.gcCarrier.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gcCarrier.MenuManager = Me.RibbonControl
        Me.gcCarrier.Name = "gcCarrier"
        Me.gcCarrier.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.Carrier, Me.cmbCarrier})
        Me.gcCarrier.Size = New System.Drawing.Size(407, 206)
        Me.gcCarrier.TabIndex = 28
        Me.gcCarrier.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCarrier})
        '
        'gvCarrier
        '
        Me.gvCarrier.DetailHeight = 284
        Me.gvCarrier.GridControl = Me.gcCarrier
        Me.gvCarrier.Name = "gvCarrier"
        Me.gvCarrier.OptionsView.ShowGroupPanel = False
        '
        'Carrier
        '
        Me.Carrier.AutoHeight = False
        Me.Carrier.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Carrier.Name = "Carrier"
        '
        'cmbCarrier
        '
        Me.cmbCarrier.AutoHeight = False
        Me.cmbCarrier.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCarrier.Name = "cmbCarrier"
        '
        'txtInvoiceNum
        '
        Me.txtInvoiceNum.Location = New System.Drawing.Point(142, 174)
        Me.txtInvoiceNum.MenuManager = Me.RibbonControl
        Me.txtInvoiceNum.Name = "txtInvoiceNum"
        Me.txtInvoiceNum.Size = New System.Drawing.Size(220, 20)
        Me.txtInvoiceNum.StyleController = Me.LayoutControl1
        Me.txtInvoiceNum.TabIndex = 27
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.GridControl3)
        Me.LayoutControl2.Location = New System.Drawing.Point(11, 256)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(1187, 371)
        Me.LayoutControl2.TabIndex = 14
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'GridControl3
        '
        Me.GridControl3.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GridControl3.Location = New System.Drawing.Point(11, 10)
        Me.GridControl3.MainView = Me.BandedGridView3
        Me.GridControl3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GridControl3.MenuManager = Me.RibbonControl
        Me.GridControl3.Name = "GridControl3"
        Me.GridControl3.Size = New System.Drawing.Size(1165, 351)
        Me.GridControl3.TabIndex = 4
        Me.GridControl3.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.BandedGridView3})
        '
        'BandedGridView3
        '
        Me.BandedGridView3.DetailHeight = 284
        Me.BandedGridView3.GridControl = Me.GridControl3
        Me.BandedGridView3.Name = "BandedGridView3"
        Me.BandedGridView3.OptionsView.ShowGroupPanel = False
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1187, 371)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.GridControl3
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(1169, 355)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'txtUSD
        '
        Me.txtUSD.Location = New System.Drawing.Point(487, 150)
        Me.txtUSD.MenuManager = Me.RibbonControl
        Me.txtUSD.Name = "txtUSD"
        Me.txtUSD.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtUSD.Properties.Mask.EditMask = "n2"
        Me.txtUSD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtUSD.Properties.MaxLength = 13
        Me.txtUSD.Size = New System.Drawing.Size(243, 20)
        Me.txtUSD.StyleController = Me.LayoutControl1
        Me.txtUSD.TabIndex = 12
        '
        'txtCDNum
        '
        Me.txtCDNum.Location = New System.Drawing.Point(487, 126)
        Me.txtCDNum.MenuManager = Me.RibbonControl
        Me.txtCDNum.Name = "txtCDNum"
        Me.txtCDNum.Size = New System.Drawing.Size(243, 20)
        Me.txtCDNum.StyleController = Me.LayoutControl1
        Me.txtCDNum.TabIndex = 11
        '
        'txtSaleNum
        '
        Me.txtSaleNum.Location = New System.Drawing.Point(487, 102)
        Me.txtSaleNum.MenuManager = Me.RibbonControl
        Me.txtSaleNum.Name = "txtSaleNum"
        Me.txtSaleNum.Size = New System.Drawing.Size(243, 20)
        Me.txtSaleNum.StyleController = Me.LayoutControl1
        Me.txtSaleNum.TabIndex = 8
        '
        'dtCreated
        '
        Me.dtCreated.EditValue = Nothing
        Me.dtCreated.Location = New System.Drawing.Point(142, 102)
        Me.dtCreated.MenuManager = Me.RibbonControl
        Me.dtCreated.Name = "dtCreated"
        Me.dtCreated.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtCreated.Properties.Appearance.Options.UseFont = True
        Me.dtCreated.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtCreated.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtCreated.Properties.Mask.EditMask = ""
        Me.dtCreated.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.dtCreated.Properties.MaxValue = New Date(9999, 12, 31, 0, 0, 0, 0)
        Me.dtCreated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dtCreated.Size = New System.Drawing.Size(220, 20)
        Me.dtCreated.StyleController = Me.LayoutControl1
        Me.dtCreated.TabIndex = 5
        '
        'cmbST
        '
        Me.cmbST.Location = New System.Drawing.Point(142, 126)
        Me.cmbST.MenuManager = Me.RibbonControl
        Me.cmbST.Name = "cmbST"
        Me.cmbST.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbST.Size = New System.Drawing.Size(220, 20)
        Me.cmbST.StyleController = Me.LayoutControl1
        Me.cmbST.TabIndex = 6
        '
        'txtCNum
        '
        Me.txtCNum.EditValue = ""
        Me.txtCNum.Location = New System.Drawing.Point(487, 174)
        Me.txtCNum.MenuManager = Me.RibbonControl
        Me.txtCNum.Name = "txtCNum"
        Me.txtCNum.Properties.MaxLength = 50
        Me.txtCNum.Size = New System.Drawing.Size(243, 20)
        Me.txtCNum.StyleController = Me.LayoutControl1
        Me.txtCNum.TabIndex = 13
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(11, 647)
        Me.txtRemark.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtRemark.MenuManager = Me.RibbonControl
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(1187, 20)
        Me.txtRemark.StyleController = Me.LayoutControl1
        Me.txtRemark.TabIndex = 24
        '
        'cmbUV
        '
        Me.cmbUV.Location = New System.Drawing.Point(142, 150)
        Me.cmbUV.MenuManager = Me.RibbonControl
        Me.cmbUV.Name = "cmbUV"
        Me.cmbUV.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbUV.Properties.NullText = ""
        Me.cmbUV.Properties.PopupSizeable = False
        Me.cmbUV.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cmbUV.Size = New System.Drawing.Size(220, 20)
        Me.cmbUV.StyleController = Me.LayoutControl1
        Me.cmbUV.TabIndex = 7
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.EmptySpaceItem2, Me.LayoutControlItem1, Me.EmptySpaceItem3, Me.EmptySpaceItem4, Me.LayoutControlItem14, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.f0, Me.LayoutControlItem9, Me.LayoutControlItem10, Me.EmptySpaceItem6, Me.EmptySpaceItem5, Me.EmptySpaceItem7, Me.EmptySpaceItem8, Me.EmptySpaceItem9, Me.LayoutControlItem11, Me.LayoutControlItem15, Me.LayoutControlItem16})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1209, 677)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(355, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(24, 246)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(723, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(24, 246)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.LayoutControl2
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 246)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1191, 375)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(34, 246)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(1158, 0)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(33, 246)
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.txtRemark
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 621)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(1191, 40)
        Me.LayoutControlItem14.Text = "Note:"
        Me.LayoutControlItem14.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.dtCreated
        Me.LayoutControlItem2.Location = New System.Drawing.Point(34, 92)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(321, 24)
        Me.LayoutControlItem2.Text = "Date:"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.cmbST
        Me.LayoutControlItem3.Location = New System.Drawing.Point(34, 116)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(321, 24)
        Me.LayoutControlItem3.Text = "Sell Type:"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.cmbUV
        Me.LayoutControlItem4.Location = New System.Drawing.Point(34, 140)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(321, 24)
        Me.LayoutControlItem4.Text = "Catcher:"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtSaleNum
        Me.LayoutControlItem5.Location = New System.Drawing.Point(379, 92)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(344, 24)
        Me.LayoutControlItem5.Text = "Sales No.:"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.txtInvoiceNum
        Me.LayoutControlItem7.Location = New System.Drawing.Point(34, 164)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(321, 24)
        Me.LayoutControlItem7.Text = "Invoice No.:"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.txtCDNum
        Me.LayoutControlItem8.Location = New System.Drawing.Point(379, 116)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(344, 24)
        Me.LayoutControlItem8.Text = "Catch Delivery No.:"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(94, 13)
        '
        'f0
        '
        Me.f0.Control = Me.txtUSD
        Me.f0.Location = New System.Drawing.Point(379, 140)
        Me.f0.Name = "f0"
        Me.f0.Size = New System.Drawing.Size(344, 24)
        Me.f0.Text = "USD Rate:"
        Me.f0.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.gcCarrier
        Me.LayoutControlItem9.Location = New System.Drawing.Point(747, 36)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(411, 210)
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.txtCNum
        Me.LayoutControlItem10.Location = New System.Drawing.Point(379, 164)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(344, 24)
        Me.LayoutControlItem10.Text = "Contract No.:"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(94, 13)
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.AllowHotTrack = False
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(34, 0)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(321, 92)
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.AllowHotTrack = False
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(379, 0)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(344, 92)
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem7
        '
        Me.EmptySpaceItem7.AllowHotTrack = False
        Me.EmptySpaceItem7.Location = New System.Drawing.Point(379, 188)
        Me.EmptySpaceItem7.Name = "EmptySpaceItem7"
        Me.EmptySpaceItem7.Size = New System.Drawing.Size(344, 58)
        Me.EmptySpaceItem7.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem8
        '
        Me.EmptySpaceItem8.AllowHotTrack = False
        Me.EmptySpaceItem8.Location = New System.Drawing.Point(34, 188)
        Me.EmptySpaceItem8.Name = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Size = New System.Drawing.Size(321, 58)
        Me.EmptySpaceItem8.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem9
        '
        Me.EmptySpaceItem9.AllowHotTrack = False
        Me.EmptySpaceItem9.Location = New System.Drawing.Point(747, 26)
        Me.EmptySpaceItem9.Name = "EmptySpaceItem9"
        Me.EmptySpaceItem9.Size = New System.Drawing.Size(411, 10)
        Me.EmptySpaceItem9.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.Label1
        Me.LayoutControlItem11.Location = New System.Drawing.Point(747, 0)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(205, 26)
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextVisible = False
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.btnAddCarrier
        Me.LayoutControlItem15.Location = New System.Drawing.Point(952, 0)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(101, 26)
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextVisible = False
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.btnDeleteCarrier
        Me.LayoutControlItem16.Location = New System.Drawing.Point(1053, 0)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(105, 26)
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem16.TextVisible = False
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(14, 14)
        Me.GridControl1.MainView = Me.BandedGridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GridControl1.MenuManager = Me.RibbonControl
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1164, 337)
        Me.GridControl1.TabIndex = 4
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.BandedGridView1, Me.GridView1})
        '
        'BandedGridView1
        '
        Me.BandedGridView1.GridControl = Me.GridControl1
        Me.BandedGridView1.Name = "BandedGridView1"
        Me.BandedGridView1.OptionsView.ShowGroupPanel = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1192, 365)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.GridControl1
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(1168, 341)
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextVisible = False
        '
        'GridControl2
        '
        Me.GridControl2.Location = New System.Drawing.Point(14, 14)
        Me.GridControl2.MainView = Me.BandedGridView2
        Me.GridControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridControl2.MenuManager = Me.RibbonControl
        Me.GridControl2.Name = "GridControl2"
        Me.GridControl2.Size = New System.Drawing.Size(1164, 338)
        Me.GridControl2.TabIndex = 4
        Me.GridControl2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.BandedGridView2, Me.GridView2})
        '
        'BandedGridView2
        '
        Me.BandedGridView2.DetailHeight = 431
        Me.BandedGridView2.GridControl = Me.GridControl2
        Me.BandedGridView2.Name = "BandedGridView2"
        Me.BandedGridView2.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full
        Me.BandedGridView2.OptionsView.ColumnAutoWidth = False
        Me.BandedGridView2.OptionsView.ShowColumnHeaders = False
        Me.BandedGridView2.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.GridControl2
        Me.GridView2.Name = "GridView2"
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem13})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(1192, 366)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.GridControl2
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(1168, 342)
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextVisible = False
        '
        'frm_salesInvoice
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[False]
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1209, 865)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frm_salesInvoice"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "frmSalesInvoice"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.gcCarrier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCarrier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Carrier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCarrier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvoiceNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.GridControl3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUSD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCDNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSaleNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtCreated.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtCreated.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbST.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbUV.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.f0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents BarHeaderItem1 As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents txt_refNum As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents btnSave As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnDelete As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnPost As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbnActions As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtCDNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSaleNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtUSD As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtCreated As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbST As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents f0 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtCNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtRemark As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbUV As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents BandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridControl2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents BandedGridView2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridControl3 As DevExpress.XtraGrid.GridControl
    Friend WithEvents BandedGridView3 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents txtInvoiceNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbnTools As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gcCarrier As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCarrier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbCarrier As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents Carrier As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem7 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem8 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem9 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnDeleteCarrier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddCarrier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem


End Class
