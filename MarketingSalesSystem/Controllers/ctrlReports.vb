Imports DevExpress.XtraReports.UI

Public Class ctrlReports
    Private isNew As Boolean

    Private mkdb As mkdbDataContext
    Private tpmdb As tpmdbDataContext

    Private frmRB As frm_reportBuilder

    Sub New()
        mkdb = New mkdbDataContext
        tpmdb = New tpmdbDataContext

        frmRB = New frm_reportBuilder(Me)

        With frmRB
            .cmbStatus.Enabled = False
            .lueInvoice.Enabled = False
            .cmbSet.Enabled = False
            .lueCarrier.Enabled = False

            loadCombos()
            .Show()
        End With
    End Sub

    Sub loadCombos()
        loadReport()
        loadStatus()
        loadInvoice()
        loadCarrier()
        loadSets()
    End Sub

    Sub loadReport()
        Dim cmb = New List(Of String)

        cmb.Add("Sales Invoice")
        cmb.Add("Invoice Summary")

        With frmRB.cmbReport
            .Properties.PopupSizeable = False
            .Properties.Items.AddRange(cmb)
            .Properties.NullText = "Select Report"
        End With
    End Sub

    Sub loadStatus()
        Dim cmb = New List(Of String)

        cmb.Add("Partial")
        cmb.Add("Partial Final")
        cmb.Add("Final")

        With frmRB.cmbStatus
            .Properties.PopupSizeable = False
            .Properties.Items.AddRange(cmb)
            .Properties.NullText = "Select Status"
        End With
    End Sub

    Sub loadInvoice(Optional status As Integer = 0)
        Dim src As IEnumerable(Of Object) = Nothing

        If status <> 0 Then
            src = (From i In mkdb.trans_SalesReports
                       Join j In mkdb.trans_SalesInvoiceBuyers On i.salesReport_ID Equals j.salesInvoiceID
                       Where j.paymentStatus = status AndAlso j.approvalStatus = 1
                       Select New With {
                           .ID = i.salesReport_ID,
                           .Value = "#" & i.salesReport_ID & "-" & i.invoiceNum}).Distinct().ToList()
        End If

        lookUpTransMode(src, frmRB.lueInvoice, "Value", "ID", "Select Invoice")
    End Sub

    Sub loadSets(Optional salesInvoiceID As Integer = 0, Optional status As Integer = 0)
        Dim cmb As New List(Of String)

        If salesInvoiceID <> 0 Then
            cmb = (From i In mkdb.trans_SalesInvoiceBuyers
                       Where i.salesInvoiceID = salesInvoiceID AndAlso i.paymentStatus = status AndAlso
                       i.approvalStatus = 1 Select i.setNum).Distinct().ToList()
        End If

        With frmRB.cmbSet
            .Properties.PopupSizeable = False
            .Properties.Items.AddRange(cmb)
            .Properties.NullText = "Select Status"
        End With
    End Sub

    Sub loadCarrier(Optional salesInvoiceID As Integer = 0, Optional status As Integer = 0)
        Dim carrierList As New List(Of Object)

        If salesInvoiceID <> 0 Then
            Dim carriers = (From i In mkdb.trans_SalesUnloadeds
                          Where i.SalesReportID = salesInvoiceID Select New With {
                              .ID = i.CarrierID,
                              .Name = i.CarrierName})

            Dim carrierCompany = (From i In tpmdb.ml_Vessels Select i).ToDictionary(Function(s) s.ml_vID, Function(v) v.vesselName)

            For Each c In carriers
                If Integer.TryParse(c.Name, 0) Then
                    carrierList.Add(New With {.ID = c.ID, .Name = carrierCompany(CInt(c.Name))})
                Else
                    carrierList.Add(New With {.ID = c.ID, .Name = c.Name})
                End If
            Next

        End If

        lookUpTransMode(carrierList, frmRB.lueCarrier, "Name", "ID", "Select Carrier")
    End Sub

    Sub print()
        With frmRB
            Dim salesInvoiceID = CInt(.lueInvoice.EditValue)
            Dim setNum = .cmbSet.EditValue.ToString
            Dim carrier = CInt(.lueCarrier.EditValue)

            Select Case .cmbReport.EditValue.ToString
                Case "Sales Invoice"
                    Dim tool As ReportPrintTool

                    Dim rp = New rptSalesInvoiceReport()
                    rp.DataSource = getReportSalesInvoice(salesInvoiceID, setNum, carrier)
                    tool = New ReportPrintTool(rp)
                    tool.ShowPreviewDialog()
                Case "Invoice Summary"
                    Dim tool As ReportPrintTool

                    Dim rp = New rptPartialSummaryReport()
                    rp.DataSource = getPartialSalesInvoice(salesInvoiceID, setNum, carrier)
                    tool = New ReportPrintTool(rp)
                    tool.ShowPreviewDialog()
            End Select
        End With
    End Sub
End Class
