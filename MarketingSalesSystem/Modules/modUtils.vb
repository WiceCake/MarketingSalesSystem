Imports DevExpress.XtraTab
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Utils

Module modUtils
    Function getServerDate() As Date
        Return Date.Now
    End Function

    Function AddTab(ByRef tabControl As XtraTabControl, ByVal tabTitle As String) As XtraTabPage
        Dim newTab As New XtraTabPage() With {
            .Text = tabTitle
        }
        tabControl.TabPages.Add(newTab)

        Return newTab
    End Function

    Sub gridTransMode(ByRef grid As GridView, Optional editable As Boolean = False)
        Try
            grid.BestFitColumns()
            grid.OptionsBehavior.Editable = editable
            grid.OptionsSelection.EnableAppearanceFocusedRow = True
            grid.Columns(0).Visible = False
            grid.OptionsCustomization.AllowColumnMoving = True
        Catch ex As Exception

        End Try
    End Sub

    Sub checkComboTransMode(dataSource As Object, ByRef ccmb As CheckedComboBoxEdit, valueName As String, idName As String)
        With ccmb.Properties
            .DataSource = dataSource
            .DisplayMember = valueName
            .ValueMember = idName
        End With
    End Sub

    Sub checkComboTransMode(dataSource As Object, ByRef ccmb As CheckedComboBoxEdit)
        With ccmb.Properties
            .DataSource = dataSource
        End With
    End Sub

    Sub lookUpTransMode(ByVal dataSource As Object, ByRef lookUpEdit As LookUpEdit, valueName As String, idName As String, defaultValue As String)
        With lookUpEdit.Properties
            .DataSource = dataSource
            .DisplayMember = valueName
            .ValueMember = idName
            .NullText = defaultValue
            .ShowHeader = False
            .ShowFooter = False
            .Columns.Clear()
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo(valueName, ""))
        End With
    End Sub


    Sub lookUpTransMode(ByVal dataSource As Object, ByRef lookUpEdit As RepositoryItemLookUpEdit, valueName As String, idName As String, defaultValue As String)
        With lookUpEdit
            .DataSource = dataSource
            .DisplayMember = valueName
            .ValueMember = idName
            .NullText = defaultValue
            .ShowHeader = False
            .ShowFooter = False
            .Columns.Clear()
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo(valueName, ""))
            .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
        End With
    End Sub

    Function validateField(ByRef control As DateEdit) As Boolean
        Return (Not control.EditValue Is Nothing)
    End Function

    Function validateField(ByRef control As TextEdit) As Boolean
        Return (Not control.EditValue Is Nothing)
    End Function

    Function validateField(ByRef control As LookUpEdit) As Boolean
        Return (Not control.EditValue Is Nothing)
    End Function

    Function validateField(ByRef control As ComboBoxEdit) As Boolean
        Return (Not control.EditValue Is Nothing)
    End Function

    Function validateField(ByRef control As CheckedComboBoxEdit) As Boolean
        Return (Not String.IsNullOrWhiteSpace(control.EditValue.ToString()))
    End Function

    Sub requiredMessage(ByVal fields As String)
        XtraMessageBox.Show("Required Fields: " + vbNewLine + fields, APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Public Sub gvCount(ByRef gridview As GridView)
        If gridview.RowCount > 0 Then
            Dim col = gridview.Columns(1)
            col.Summary.Add(DevExpress.Data.SummaryItemType.Count, col.FieldName, "Count:{0}")
        End If
    End Sub

    Function ConfirmCloseMessage() As Boolean
        Return XtraMessageBox.Show("Are you sure you close this form?", APPNAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes
    End Function

    Function ConfirmCloseWithoutSaving() As Boolean
        Return XtraMessageBox.Show("Are you sure you want to close without saving?", APPNAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes
    End Function

    Function SuccessfullySavedMessage() As Integer
        Return XtraMessageBox.Show("Your record is successfully saved in the database.", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Function ConfirmPostedData() As Boolean
        Return XtraMessageBox.Show("Are you sure you want to post this report?", APPNAME, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes
    End Function

    Function ConfirmDeleteMessage() As Boolean
        Return XtraMessageBox.Show("Are you sure you want to delete this record?", APPNAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes
    End Function

    Function SelectFirstRow() As Integer
        Return XtraMessageBox.Show("Please select row first!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Function SuccessfullyAddedUpdatedMessage() As System.Windows.Forms.DialogResult
        Return XtraMessageBox.Show("Your record is successfully added or updated in the database.", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Sub SetLayoutVisibility(ParamArray items() As Object)
        For i = 0 To items.Length - 1 Step 2
            Dim ctrl = CType(items(i), DevExpress.XtraLayout.LayoutControlItem)
            Dim visible = CBool(items(i + 1))
            ctrl.Visibility = If(visible, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        Next
    End Sub

    Sub SetBarVisibility(ParamArray items() As Object)
        For i = 0 To items.Length - 1 Step 2
            Dim ctrl = CType(items(i), DevExpress.XtraBars.BarItem)
            Dim visible = CBool(items(i + 1))
            ctrl.Visibility = If(visible, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        Next
    End Sub

    Sub SetBarEnabled(ParamArray items() As Object)
        For i = 0 To items.Length - 1 Step 2
            Dim ctrl = CType(items(i), DevExpress.XtraBars.BarItem)
            Dim isEnabled = CBool(items(i + 1))

            ctrl.Enabled = isEnabled
        Next
    End Sub

    Sub SetCheckboxVisibility(ParamArray items() As Object)
        For i = 0 To items.Length - 1 Step 2
            Dim ctrl = CType(items(i), DevExpress.XtraBars.BarCheckItem)
            Dim checkBoxVisible = CBool(items(i + 1))

            ctrl.CheckBoxVisibility = If(checkBoxVisible, DevExpress.XtraBars.CheckBoxVisibility.BeforeText, DevExpress.XtraBars.CheckBoxVisibility.None)
        Next
    End Sub

    Sub ClearEditValues(ParamArray controls() As BaseEdit)
        For Each ctrl In controls
            ctrl.EditValue = Nothing
        Next
    End Sub

    Sub ZeroEditValues(ParamArray controls() As BaseEdit)
        For Each ctrl In controls
            ctrl.EditValue = 0D
        Next
    End Sub

    Sub SetReadOnlyFields(ParamArray controls() As BaseEdit)
        For Each ctrl In controls
            ctrl.ReadOnly = True
        Next
    End Sub

    Sub UnSetReadOnlyFields(ParamArray controls() As BaseEdit)
        For Each ctrl In controls
            ctrl.ReadOnly = False
        Next
    End Sub

End Module
