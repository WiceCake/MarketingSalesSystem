Imports DevExpress.XtraReports.UI

Public Class rptPartialSummaryReport

    Private hasSetTotalLayout As Boolean = False
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

    'Private Sub GroupHeaderBand1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupHeaderBand1.BeforePrint
    '    If GetCurrentColumnValue("BuyerType") Is Nothing OrElse GetCurrentColumnValue("BuyerType").ToString() = "Backing" Then
    '        e.Cancel = True
    '    End If
    'End Sub

    Private Sub GroupHeaderBand2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupHeaderBand2.BeforePrint
        'hideDetailLabel()
        If GetCurrentColumnValue("BuyerType") Is Nothing OrElse GetCurrentColumnValue("BuyerType").ToString() = "Backing" Then
            e.Cancel = True
        End If

        Select Case GetCurrentColumnValue("BuyerType").ToString()
            Case "Local"
                setItemVisibility(False, XrLabel125, XrLabel106, XrLabel101, XrLabel102)
                setItemVisibility(True, XrLabel9, XrLabel12, XrLabel37, XrLabel38)
                SetPosItem(0, XrLabel9, XrLabel37, XrLabel38)
            Case "Export"
                SetItemVisibility(False, XrLabel101, XrLabel102, XrLabel122)
                SetItemValue("YF 10 UP", XrLabel125)
            Case Else
                SetItemVisibility(False, XrLabel9, XrLabel12, XrLabel37, XrLabel38)
        End Select
    End Sub

    Private Sub hideDetailLabel()
        XrLabel126.Visible = False
        XrLabel105.Visible = False
        XrLabel125.Visible = False
        XrLabel101.Visible = False
        XrLabel102.Visible = False
        XrLabel122.Visible = False
        XrLabel99.Visible = False
        XrLabel100.Visible = False
        XrLabel8.Visible = False
        XrLabel116.Visible = False
        XrLabel106.Visible = False
        XrLabel9.Visible = False
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        If GetCurrentColumnValue("BuyerType") Is Nothing OrElse GetCurrentColumnValue("BuyerType").ToString() = "Backing" Then
            e.Cancel = True
        End If
        Select Case GetCurrentColumnValue("BuyerType").ToString()
            Case "Local"
                SetItemVisibility(False, XrLabel103, XrLabel13, XrLabel117, XrLabel97, XrLabel119)
                SetItemVisibility(True, XrLabel39, XrLabel36)
                SetPosItem(XrLabel117.HeightF, XrLabel36, XRLabel39)
                'SetItemValue("-", XrLabel97, XrLabel119)
            Case "Export"
                SetItemVisibility(False, XrLabel117, XrLabel119)
            Case Else
                SetItemVisibility(False, XrLabel39, XrLabel36)
        End Select
    End Sub

    

    Private Sub GroupFooterBand2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupFooterBand2.BeforePrint
        If GetCurrentColumnValue("BuyerType") Is Nothing OrElse GetCurrentColumnValue("BuyerType").ToString() = "Backing" Then
            e.Cancel = True
        End If

        Select Case GetCurrentColumnValue("BuyerType").ToString()
            Case "Local"
                If Not hasSetTotalLayout Then
                    SetPosItem(XrLabel33.HeightF, XrLabel104, XrLabel99, XrLabel98, XrLabel120,
                           XrLabel118, XrLabel6, XrLabel5, XrLabel7, XrLabel107, XrLabel11,
                           XrLabel10)
                    SetPosItem(XrLabel33.HeightF, XrLine9, XrLine8, XrLine7, XrLine3, XrLine5)
                    hasSetTotalLayout = True
                End If
                SetItemVisibility(False, XrLabel33, XrLabel35)
                SetItemVisibility(True, XrLabel118, XrLabel120)
                SetItemVisibility(True, XrLine7, XrLine8)
                SetItemValue("-", XrLabel98)
            Case "Export"
                If Not hasSetTotalLayout Then
                    SetPosItem(XrLabel33.HeightF, XrLabel104, XrLabel99, XrLabel98, XrLabel120,
                           XrLabel118, XrLabel6, XrLabel5, XrLabel7, XrLabel107, XrLabel11,
                           XrLabel10)
                    SetPosItem(XrLabel33.HeightF, XrLine9, XrLine8, XrLine7, XrLine3, XrLine5)
                    hasSetTotalLayout = True
                End If
                SetItemVisibility(False, XrLabel33, XrLabel35, XrLabel118, XrLabel120)
                SetItemVisibility(False, XrLine7, XrLine8)
        End Select
    End Sub

    Sub SetPosItem(pos As Single, ParamArray labels() As XRLabel)
        For Each label As XRLabel In labels
            Dim newPos = label.LocationF.Y - pos
            If pos = 0 Then newPos = 0
            label.LocationF = New PointF(label.LocationF.X, newPos)
        Next
    End Sub

    Sub SetPosItem(pos As Single, ParamArray lines() As XRLine)
        For Each line As XRLine In lines
            line.LocationF = New PointF(line.LocationF.X, line.LocationF.Y - pos)
        Next
    End Sub

    Sub SetItemVisibility(val As Boolean, ParamArray labels() As XRLabel)
        For Each label As XRLabel In labels
            label.Visible = val
        Next
    End Sub
    Sub SetItemVisibility(val As Boolean, ParamArray lines() As XRLine)
        For Each line As XRLine In lines
            line.Visible = val
        Next
    End Sub

    Sub SetItemValue(val As String, ParamArray labels() As XRLabel)
        For Each Label As XRLabel In labels
            Label.ExpressionBindings.Clear()
            Label.Text = val
        Next
    End Sub

End Class