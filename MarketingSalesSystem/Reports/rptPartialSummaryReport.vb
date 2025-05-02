Imports DevExpress.XtraReports.UI

Public Class rptPartialSummaryReport


    'Calculates Summary Per Band For Future Reference

    'Private Sub XrLabel2_BeforePrint(sender As Object, e As Printing.PrintEventArgs)
    '    Dim label As XRLabel = CType(sender, XRLabel)

    '    Dim currentBuyerType As String = GetCurrentColumnValue("BuyerType").ToString()

    '    Dim subtotal As Decimal = 0

    '    Dim dataList As List(Of PartialSalesInvoice) = CType(Me.DataSource, List(Of PartialSalesInvoice))

    '    For Each item As PartialSalesInvoice In dataList
    '        If item.BuyerType = currentBuyerType Then
    '            subtotal += item.KiloFishMeal
    '            subtotal += item.KiloMix
    '        End If
    '    Next

    '    label.Text = subtotal.ToString("N2")
    'End Sub

    Private Sub XrLabel19_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrLabel19.BeforePrint
        Dim label As XRLabel = CType(sender, XRLabel)

        Dim subtotal = getTotalPerBand(sender, "Canning")

        If subtotal = 0 Then
            label.Text = "-"
        Else
            label.Text = subtotal.ToString("N2")
        End If
    End Sub

    Private Sub XrLabel22_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrLabel22.BeforePrint
        Dim label As XRLabel = CType(sender, XRLabel)

        Dim subtotal = getTotalPerBand(sender, "Local")

        If subtotal = 0 Then
            label.Text = "-"
        Else
            label.Text = subtotal.ToString("N2")
        End If
    End Sub

    Private Sub XrLabel25_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrLabel25.BeforePrint
        Dim label As XRLabel = CType(sender, XRLabel)

        Dim subtotal = getTotalPerBand(sender, "Export")

        If subtotal = 0 Then
            label.Text = "-"
        Else
            label.Text = subtotal.ToString("N2")
        End If
    End Sub

    Private Sub XrLabel23_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrLabel23.BeforePrint
        Dim label As XRLabel = CType(sender, XRLabel)

        Dim subtotal = getTotalPerBand(sender, "Backing")

        If subtotal = 0 Then
            label.Text = "-"
        Else
            label.Text = subtotal.ToString("N2")
        End If
    End Sub

    Private Sub XrLabel28_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrLabel28.BeforePrint
        Dim label As XRLabel = CType(sender, XRLabel)

        Dim subtotal = getTotalPerBand(sender, "Storage")

        If subtotal = 0 Then
            label.Text = "-"
        Else
            label.Text = subtotal.ToString("N2")
        End If
    End Sub

    Function getTotalPerBand(sender As Object, buyerType As String) As Decimal

        Dim subtotal As Decimal = 0

        ' Cast the data source to your list type
        Dim dataList As List(Of PartialSalesInvoice) = CType(Me.DataSource, List(Of PartialSalesInvoice))

        ' Sum only where BuyerType is "Canning"
        For Each item As PartialSalesInvoice In dataList
            If item.BuyerType = buyerType Then
                subtotal += item.KiloFishMeal
                subtotal += item.KiloMix
            End If
        Next

        Return subtotal
    End Function

End Class