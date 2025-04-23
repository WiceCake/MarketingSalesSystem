Module modData
    Function getReportSalesInvoice(reportID As Integer) As List(Of SalesReportInvoice)
        Dim retList As New List(Of SalesReportInvoice)

        Dim dc As New mkdbDataContext
        Dim dc2 As New tpmdbDataContext

        Dim sr = (From i In dc.trans_SalesReports
                  Join j In dc.trans_SalesReportCatchers On i.salesReport_ID Equals j.salesReport_ID
                  Join k In dc.trans_SalesReportPrices On i.salesReport_ID Equals k.salesReport_ID
                  Where i.salesReport_ID = reportID Select i, j, k).ToList

        Dim groupSR = sr.
        GroupBy(Function(x) x.j.catchActivityDetail_ID).
        Select(Function(g) g.First()).
        ToList()

        Dim cd = (From i In dc.trans_CatchActivityDetails Select i).ToDictionary(Function(i) i.catchActivityDetail_ID, Function(i) i.vessel_ID)
        Dim vessels = (From i In dc2.ml_Vessels Select i).ToDictionary(Function(i) i.ml_vID, Function(i) i.vesselName)

        For Each s In groupSR
            Dim sri As New SalesReportInvoice

            ' Sales Report Column
            sri.SalesReportID = s.i.salesReport_ID
            sri.UnloadingVesselID = s.i.unloadingVessel_ID
            sri.UnloadingForeignVessel = s.i.unloadingForeignVessel
            sri.Buyer = s.i.buyer
            sri.SellingType = s.i.sellingType
            sri.UnloadingType = s.i.unloadingType
            sri.InvoiceNum = s.i.invoiceNum
            sri.ReferenceNum = s.i.referenceNum
            sri.SalesNum = s.i.salesNum
            sri.CatchDeliveryNum = s.i.catchtDeliveryNum
            sri.ContractNum = s.i.contractNum
            sri.Remarks = s.i.remarks
            sri.EncodedBy = s.i.encodedBy
            sri.ApprovalStatus = s.i.approvalStatus
            sri.SalesDate = s.i.salesDate
            sri.EncodedOn = s.i.encodedOn
            sri.UsdRate = s.i.usdRate
            sri.VesselName = vessels(cd(s.j.catchActivityDetail_ID))

            ' Sales Report Catcher
            sri.SalesReportCatcherID = s.j.salesReportCatcher_ID
            sri.CatchActivityDetailID = s.j.catchActivityDetail_ID
            sri.Skipjack0To300To499 = s.j.skipjack0_300To0_499
            sri.Skipjack0To500To999 = s.j.skipjack0_500To0_999
            sri.Skipjack1To0To1To39 = s.j.skipjack1_0To1_39
            sri.Skipjack1To4To1To79 = s.j.skipjack1_4To1_79
            sri.Skipjack1To8To2To49 = s.j.skipjack1_8To2_49
            sri.Skipjack2To5To3To49 = s.j.skipjack2_5To3_49
            sri.Skipjack3To5AndUP = s.j.skipjack3_5AndUP
            sri.Yellowfin0To300To499 = s.j.yellowfin0_300To0_499
            sri.Yellowfin0To500To999 = s.j.yellowfin0_500To0_999
            sri.Yellowfin1To0To1To49 = s.j.yellowfin1_0To1_49
            sri.Yellowfin1To5To2To49 = s.j.yellowfin1_5To2_49
            sri.Yellowfin2To5To3To49 = s.j.yellowfin2_5To3_49
            sri.Yellowfin3To5To4To99 = s.j.yellowfin3_5To4_99
            sri.Yellowfin5To0To9To99 = s.j.yellowfin5_0To9_99
            sri.Yellowfin10AndUpGood = s.j.yellowfin10AndUpGood
            sri.Yellowfin10AndUpDeformed = s.j.yellowfin10AndUpDeformed
            sri.Bigeye0To500To999 = s.j.bigeye0_500To0_999
            sri.Bigeye1To0To1To49 = s.j.bigeye1_0To1_49
            sri.Bigeye1To5To2To49 = s.j.bigeye1_5To2_49
            sri.Bigeye2To5To3To49 = s.j.bigeye2_5To3_49
            sri.Bigeye3To5To4To99 = s.j.bigeye3_5To4_99
            sri.Bigeye5To0To9To99 = s.j.bigeye5_0To9_99
            sri.Bigeye10AndUP = s.j.bigeye10AndUP
            sri.Bonito = s.j.bonito
            sri.Fishmeal = s.j.fishmeal

            ' Sales Report Price
            sri.SalesReportPriceID = s.k.salesReportPrice_ID
            sri.SRPSkipjack0To300To499 = s.k.skipjack0_300To0_499
            sri.SRPSkipjack0To500To999 = s.k.skipjack0_500To0_999
            sri.SRPSkipjack1To0To1To39 = s.k.skipjack1_0To1_39
            sri.SRPSkipjack1To4To1To79 = s.k.skipjack1_4To1_79
            sri.SRPSkipjack1To8To2To49 = s.k.skipjack1_8To2_49
            sri.SRPSkipjack2To5To3To49 = s.k.skipjack2_5To3_49
            sri.SRPSkipjack3To5AndUP = s.k.skipjack3_5AndUP
            sri.SRPYellowfin0To300To499 = s.k.yellowfin0_300To0_499
            sri.SRPYellowfin0To500To999 = s.k.yellowfin0_500To0_999
            sri.SRPYellowfin1To0To1To49 = s.k.yellowfin1_0To1_49
            sri.SRPYellowfin1To5To2To49 = s.k.yellowfin1_5To2_49
            sri.SRPYellowfin2To5To3To49 = s.k.yellowfin2_5To3_49
            sri.SRPYellowfin3To5To4To99 = s.k.yellowfin3_5To4_99
            sri.SRPYellowfin5To0To9To99 = s.k.yellowfin5_0To9_99
            sri.SRPYellowfin10AndUpGood = s.k.yellowfin10AndUpGood
            sri.SRPYellowfin10AndUpDeformed = s.k.yellowfin10AndUpDeformed
            sri.SRPBigeye0To500To999 = s.k.bigeye0_500To0_999
            sri.SRPBigeye1To0To1To49 = s.k.bigeye1_0To1_49
            sri.SRPBigeye1To5To2To49 = s.k.bigeye1_5To2_49
            sri.SRPBigeye2To5To3To49 = s.k.bigeye2_5To3_49
            sri.SRPBigeye3To5To4To99 = s.k.bigeye3_5To4_99
            sri.SRPBigeye5To0To9To99 = s.k.bigeye5_0To9_99
            sri.SRPBigeye10AndUP = s.k.bigeye10AndUP
            sri.SRPBonito = s.k.bonito
            sri.SRPFishmeal = s.k.fishmeal

            retList.Add(sri)
        Next

        Return retList
    End Function

    Function getReportCatcherAct(reportID1 As Integer) As List(Of CatchersReportActivity)
        Dim retList1 As New List(Of CatchersReportActivity)

        Dim dc As New mkdbDataContext
        Dim dc2 As New tpmdbDataContext

        Dim cr = (From i In dc.trans_CatchActivities Where i.catchActivity_ID = reportID1).ToList

        Dim cri = New CatchersReportActivity

        For Each c In cr
            cri.catchDate = c.catchDate

            retList1.Add(cri)
        Next
        Return retList1
    End Function

    Function getReportPartial(reportID2 As Integer) As List(Of PartialReport)
        Dim retList2 As New List(Of PartialReport)

        Dim dc As New mkdbDataContext
        Dim dc2 As New tpmdbDataContext

        Dim pr = (From i In dc.trans_SalesInvoiceBuyers Where i.salesInvoiceBuyerID = reportID2).ToList

        Dim pri = New PartialReport

        For Each p In pr
            pri.encodedOn = p.encodedOn

            retList2.Add(pri)
        Next

        Return retList2
    End Function
End Module

