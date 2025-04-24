Public Class SalesReportBuyer
    Public salesBuyerCatch_ID As Integer
    Public salesInvoiceBuyer_ID As Integer
    Public salesInvoice_ID As Integer
    Public skipjack0_300To0_499 As Decimal
    Public skipjack0_500To0_999 As Decimal
    Public skipjack1_0To1_39 As Decimal
    Public skipjack1_4To1_79 As Decimal
    Public skipjack1_8To2_49 As Decimal
    Public skipjack2_5To3_49 As Decimal
    Public skipjack3_5AndUP As Decimal
    Public yellowfin0_300To0_499 As Decimal
    Public yellowfin0_500To0_999 As Decimal
    Public yellowfin1_0To1_49 As Decimal
    Public yellowfin1_5To2_49 As Decimal
    Public yellowfin2_5To3_49 As Decimal
    Public yellowfin3_5To4_99 As Decimal
    Public yellowfin5_0To9_99 As Decimal
    Public yellowfin10AndUpGood As Decimal
    Public yellowfin10AndUpDeformed As Decimal
    Public bigeye0_500To0_999 As Decimal
    Public bigeye1_0To1_49 As Decimal
    Public bigeye1_5To2_49 As Decimal
    Public bigeye2_5To3_49 As Decimal
    Public bigeye3_5To4_99 As Decimal
    Public bigeye5_0To9_99 As Decimal
    Public bigeye10AndUP As Decimal
    Public bonito As Decimal
    Public fishmeal As Decimal

    Private dc As mkdbDataContext

    Sub New(ByRef dc_ As mkdbDataContext)
        dc = dc_
    End Sub

    Sub New(ByVal salesBuyerCatch_ID As Integer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim e = From i In dc.trans_SalesReportBuyers Where i.salesBuyerCatchID = salesBuyerCatch_ID Select i

        For Each i In e
            Me.salesBuyerCatch_ID = salesBuyerCatch_ID
            salesInvoiceBuyer_ID = i.salesInvoiceBuyerID
            salesInvoice_ID = i.salesInvoiceID
            skipjack0_300To0_499 = i.skipjack0_300To0_499
            skipjack0_500To0_999 = i.skipjack0_500To0_999
            skipjack1_0To1_39 = i.skipjack1_0To1_39
            skipjack1_4To1_79 = i.skipjack1_4To1_79
            skipjack1_8To2_49 = i.skipjack1_8To2_49
            skipjack2_5To3_49 = i.skipjack2_5To3_49
            skipjack3_5AndUP = i.skipjack3_5AndUP
            yellowfin0_300To0_499 = i.yellowfin0_300To0_499
            yellowfin0_500To0_999 = i.yellowfin0_500To0_999
            yellowfin1_0To1_49 = i.yellowfin1_0To1_49
            yellowfin1_5To2_49 = i.yellowfin1_5To2_49
            yellowfin2_5To3_49 = i.yellowfin2_5To3_49
            yellowfin3_5To4_99 = i.yellowfin3_5To4_99
            yellowfin5_0To9_99 = i.yellowfin5_0To9_99
            yellowfin10AndUpGood = i.yellowfin10AndUpGood
            yellowfin10AndUpDeformed = i.yellowfin10AndUpDeformed
            bigeye0_500To0_999 = i.bigeye0_500To0_999
            bigeye1_0To1_49 = i.bigeye1_0To1_49
            bigeye1_5To2_49 = i.bigeye1_5To2_49
            bigeye2_5To3_49 = i.bigeye2_5To3_49
            bigeye3_5To4_99 = i.bigeye3_5To4_99
            bigeye5_0To9_99 = i.bigeye5_0To9_99
            bigeye10AndUP = i.bigeye10AndUP
            bonito = i.bonito
            fishmeal = i.fishmeal
        Next
    End Sub

    Sub Add()
        Dim srp As New trans_SalesReportBuyer

        With srp
            .salesInvoiceBuyerID = salesInvoiceBuyer_ID
            .salesInvoiceID = salesInvoice_ID
            .skipjack0_300To0_499 = skipjack0_300To0_499
            .skipjack0_500To0_999 = skipjack0_500To0_999
            .skipjack1_0To1_39 = skipjack1_0To1_39
            .skipjack1_4To1_79 = skipjack1_4To1_79
            .skipjack1_8To2_49 = skipjack1_8To2_49
            .skipjack2_5To3_49 = skipjack2_5To3_49
            .skipjack3_5AndUP = skipjack3_5AndUP
            .yellowfin0_300To0_499 = yellowfin0_300To0_499
            .yellowfin0_500To0_999 = yellowfin0_500To0_999
            .yellowfin1_0To1_49 = yellowfin1_0To1_49
            .yellowfin1_5To2_49 = yellowfin1_5To2_49
            .yellowfin2_5To3_49 = yellowfin2_5To3_49
            .yellowfin3_5To4_99 = yellowfin3_5To4_99
            .yellowfin5_0To9_99 = yellowfin5_0To9_99
            .yellowfin10AndUpGood = yellowfin10AndUpGood
            .yellowfin10AndUpDeformed = yellowfin10AndUpDeformed
            .bigeye0_500To0_999 = bigeye0_500To0_999
            .bigeye1_0To1_49 = bigeye1_0To1_49
            .bigeye1_5To2_49 = bigeye1_5To2_49
            .bigeye2_5To3_49 = bigeye2_5To3_49
            .bigeye3_5To4_99 = bigeye3_5To4_99
            .bigeye5_0To9_99 = bigeye5_0To9_99
            .bigeye10AndUP = bigeye10AndUP
            .bonito = bonito
            .fishmeal = fishmeal
        End With

        dc.trans_SalesReportBuyers.InsertOnSubmit(srp)
        dc.SubmitChanges()
        salesBuyerCatch_ID = srp.salesBuyerCatchID
    End Sub

    Sub Delete()
        Dim srb = From i In dc.trans_SalesReportBuyers Where i.salesInvoiceBuyerID = salesInvoiceBuyer_ID Select i

        For Each i In srb
            dc.trans_SalesReportBuyers.DeleteOnSubmit(i)
            dc.SubmitChanges()
        Next
    End Sub

    Sub Save()
        Dim srp = From i In dc.trans_SalesReportBuyers Where i.salesBuyerCatchID = salesBuyerCatch_ID Select i

        For Each i In srp
            i.salesBuyerCatchID = salesBuyerCatch_ID
            i.salesInvoiceBuyerID = salesInvoiceBuyer_ID
            i.salesInvoiceID = salesInvoice_ID
            i.skipjack0_300To0_499 = skipjack0_300To0_499
            i.skipjack0_500To0_999 = skipjack0_500To0_999
            i.skipjack1_0To1_39 = skipjack1_0To1_39
            i.skipjack1_4To1_79 = skipjack1_4To1_79
            i.skipjack1_8To2_49 = skipjack1_8To2_49
            i.skipjack2_5To3_49 = skipjack2_5To3_49
            i.skipjack3_5AndUP = skipjack3_5AndUP
            i.yellowfin0_300To0_499 = yellowfin0_300To0_499
            i.yellowfin0_500To0_999 = yellowfin0_500To0_999
            i.yellowfin1_0To1_49 = yellowfin1_0To1_49
            i.yellowfin1_5To2_49 = yellowfin1_5To2_49
            i.yellowfin2_5To3_49 = yellowfin2_5To3_49
            i.yellowfin3_5To4_99 = yellowfin3_5To4_99
            i.yellowfin5_0To9_99 = yellowfin5_0To9_99
            i.yellowfin10AndUpGood = yellowfin10AndUpGood
            i.yellowfin10AndUpDeformed = yellowfin10AndUpDeformed
            i.bigeye0_500To0_999 = bigeye0_500To0_999
            i.bigeye1_0To1_49 = bigeye1_0To1_49
            i.bigeye1_5To2_49 = bigeye1_5To2_49
            i.bigeye2_5To3_49 = bigeye2_5To3_49
            i.bigeye3_5To4_99 = bigeye3_5To4_99
            i.bigeye5_0To9_99 = bigeye5_0To9_99
            i.bigeye10AndUP = bigeye10AndUP
            i.bonito = bonito
            i.fishmeal = fishmeal
            dc.SubmitChanges()
        Next
    End Sub
End Class
