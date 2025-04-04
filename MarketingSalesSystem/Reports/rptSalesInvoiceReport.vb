Public Class rptSalesInvoiceReport

    Public salesReport_ID As Integer

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub rptPaySummEmpSections_DataSourceDemanded(sender As Object, e As EventArgs) Handles MyBase.DataSourceDemanded
        Dim adapter = New MKDBDataSetTableAdapters.trans_SalesReportPriceTableAdapter
        Dim dataset = New MKDBDataSet.trans_SalesReportPriceDataTable
        adapter.Fill(dataset, salesReport_ID)

        Me.DataSource = dataset
    End Sub

End Class