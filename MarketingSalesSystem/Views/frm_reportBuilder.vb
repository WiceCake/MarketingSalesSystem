Imports DevExpress.XtraEditors
Imports System.Text

Public Class frm_reportBuilder

    Private ctrlRB As ctrlReports
    Private status As Integer
    Sub New(ByRef ctrl As ctrlReports)
        InitializeComponent()

        ctrlRB = ctrl
    End Sub

    Private Sub cmbReport_EditValueChanged(sender As Object, e As EventArgs) Handles cmbReport.EditValueChanged
        cmbStatus.Enabled = True
    End Sub

    Private Sub cmbStatus_EditValueChanged(sender As Object, e As EventArgs) Handles cmbStatus.EditValueChanged
        Dim val = CType(sender, ComboBoxEdit).EditValue
        status = 0

        Select Case val.ToString
            Case "Partial"
                status = Payment_Status.Partial_
            Case "Partial Final"
                status = Payment_Status.Partial_Final
            Case "Final"
                status = Payment_Status.Final_
        End Select

        ctrlRB.loadInvoice(status)
        lueInvoice.Enabled = True
    End Sub

    Private Sub lueInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles lueInvoice.EditValueChanged
        Dim val = CType(sender, LookUpEdit).EditValue

        ctrlRB.loadCarrier(CInt(val), status)
        ctrlRB.loadSets(CInt(val), status)
        lueCarrier.Enabled = True
        cmbSet.Enabled = True
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim errors As New StringBuilder

        Dim report = validateField(cmbReport)
        Dim status = validateField(cmbStatus)
        Dim invoice = validateField(lueInvoice)
        Dim carrier = validateField(lueCarrier)
        Dim setNum = validateField(cmbSet)

        If Not report Then errors.AppendLine("Report")
        If Not status Then errors.AppendLine("Status")
        If Not invoice Then errors.AppendLine("Invoice")
        If Not carrier Then errors.AppendLine("Carrier")
        If Not setNum Then errors.AppendLine("Set No.")

        If errors.Length > 0 Then
            requiredMessage(errors.ToString)
            Return
        End If

        ctrlRB.print()
    End Sub
End Class