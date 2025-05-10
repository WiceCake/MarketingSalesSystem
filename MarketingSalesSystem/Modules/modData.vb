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

    Public Class Carrier
        Public Property ID As Integer
        Public Property Name As String
    End Class

    Function getPartialSalesInvoice(reportID As Integer, setNum As String, carrier As Integer) As List(Of PartialSalesInvoice)
        Dim retList As New List(Of PartialSalesInvoice)

        Dim dc As New mkdbDataContext
        Dim dc2 As New tpmdbDataContext

        Dim groupSib = (From i In dc.trans_SalesInvoiceBuyers
                        Join k In dc.trans_SalesReports On i.salesInvoiceID Equals k.salesReport_ID
                        Where i.salesInvoiceID = CInt(reportID) AndAlso i.setNum = setNum AndAlso i.carrier = CInt(carrier) AndAlso
                        i.approvalStatus = Approval_Status.Posted
                        Select i, k).ToList()

        'Dim totalKilo = sumFields(groupSib.First().j)

        Dim backingData = (From i In dc.trans_SalesInvoiceBuyers
                           Where i.salesInvoiceID = CInt(reportID) AndAlso i.setNum = setNum AndAlso i.carrier = CInt(carrier) AndAlso
                           i.approvalStatus = Approval_Status.Posted AndAlso i.sellerType = "Backing"
                           Select i).ToLookup(Function(s) s.backing)

        Dim backingKilos = (From i In dc.trans_SalesInvoiceBuyers
                            Join j In dc.trans_SalesReportBuyers On i.salesInvoiceBuyerID Equals j.salesInvoiceBuyerID
                            Where i.salesInvoiceID = CInt(reportID) AndAlso i.setNum = setNum AndAlso i.carrier = CInt(carrier) AndAlso
                            i.approvalStatus = Approval_Status.Posted AndAlso i.sellerType = "Backing"
                            Select i, j).ToLookup(Function(s) s.i.backing)

        Dim sibKilos = (From i In dc.trans_SalesInvoiceBuyers
                        Join j In dc.trans_SalesReportBuyers On i.salesInvoiceBuyerID Equals j.salesInvoiceBuyerID
                        Join k In dc.trans_SalesReports On i.salesInvoiceID Equals k.salesReport_ID
                        Where i.salesInvoiceID = CInt(reportID) AndAlso i.setNum = setNum AndAlso i.carrier = CInt(carrier) AndAlso
                        i.approvalStatus = Approval_Status.Posted
                        Select i, j).ToLookup(Function(s) s.i.salesInvoiceBuyerID)


        ' Step 2: Perform the query using LINQ to Objects
        Dim catcherList = (From i In dc.trans_SalesReportCatchers
            Join j In dc.trans_CatchActivityDetails On i.catchActivityDetail_ID Equals j.catchActivityDetail_ID
            Where i.salesReport_ID = CInt(reportID)
            Select j.vessel_ID).Distinct().ToList()

        Dim vessels = (From i In dc2.ml_Vessels Select i).ToDictionary(Function(s) s.ml_vID, Function(s) s.vesselName)

        Dim catchers As New List(Of String)

        For Each catcher In catcherList
            catchers.Add(vessels(catcher))
        Next

        Dim catcherName = String.Join(", ", catchers)

        'Dim carriers = (From i In dc.trans_SalesUnloadeds Where i.SalesReportID = CInt(reportID) Select i.CarrierID)

        'Dim soldKilos = (From i In dc.trans_SalesReportBuyers Where i.salesInvoiceID = CInt(reportID) Select i).ToLookup(Function(s) s.salesInvoiceBuyerID)



        'Dim carrierName = (From i In dc2.ML_SULLIER_20170329s Where i.ml_SupID = CInt(carriers.FirstOrDefault()) Select i.ml_Supplier).FirstOrDefault()

        Dim buyers = (From i In dc2.ML_SULLIER_20170329s Select i).ToLookup(Function(s) s.ml_SupID)

        Dim dates = groupSib.Select(Function(s) s.i.encodedOn).ToList

        Dim carriers = (From i In dc.trans_SalesUnloadeds
                      Where i.SalesReportID = reportID Select New With {
                          .ID = i.CarrierID,
                          .Name = i.CarrierName})

        Dim carrierCompany = (From i In dc2.ml_Vessels Select i).ToDictionary(Function(s) s.ml_vID, Function(v) v.vesselName)

        Dim carrierList As New List(Of Carrier)

        For Each c In carriers
            If Integer.TryParse(c.Name, 0) Then
                carrierList.Add(New Carrier With {.ID = c.ID, .Name = carrierCompany(CInt(c.Name))})
            Else
                carrierList.Add(New Carrier With {.ID = c.ID, .Name = c.Name})
            End If
        Next

        For Each s In groupSib
            Dim sib As New PartialSalesInvoice
            Dim kiloMix = 0D
            Dim kiloFishMeal = 0D
            Dim totalSoldUSD = 0D

            If sibKilos.Contains(s.i.salesInvoiceBuyerID) Then
                Dim totalCount = sibKilos(s.i.salesInvoiceBuyerID).Count()
                Dim actualKilo = sibKilos(s.i.salesInvoiceBuyerID).Take(totalCount \ 2)
                Dim spoilageKilo = sibKilos(s.i.salesInvoiceBuyerID).Skip(totalCount \ 2)
                Dim kiloActualMix = 0D
                Dim kiloSpoilageMix = 0D
                Dim kiloActualFishMeal = 0D
                Dim kiloSpoilageFishMeal = 0D
                Dim soldActual = 0D
                Dim soldSpoilage = 0D

                Select Case s.i.sellerType.ToString
                    Case "Local"
                        kiloActualMix = actualKilo.Sum(Function(v) sumFields(v.j, "Local"))
                        kiloSpoilageMix = spoilageKilo.Sum(Function(v) sumFields(v.j, "Local"))
                        soldActual = actualKilo.Sum(Function(v) multiplyFields(v.j, "Local"))
                        soldSpoilage = spoilageKilo.Sum(Function(v) multiplyFields(v.j, "Local"))
                    Case "Export"
                        kiloActualMix = actualKilo.Sum(Function(v) sumFields(v.j, "Export"))
                        kiloSpoilageMix = spoilageKilo.Sum(Function(v) sumFields(v.j, "Export"))
                        soldActual = actualKilo.Sum(Function(v) multiplyFields(v.j, "Export"))
                        soldSpoilage = spoilageKilo.Sum(Function(v) multiplyFields(v.j, "Export"))
                    Case "Canning", "Backing"
                        kiloActualMix = actualKilo.Sum(Function(v) sumFields(v.j))
                        kiloSpoilageMix = spoilageKilo.Sum(Function(v) sumFields(v.j))
                        kiloActualFishMeal = actualKilo.Sum(Function(v) v.j.fishmeal)
                        kiloSpoilageFishMeal = spoilageKilo.Sum(Function(v) v.j.fishmeal)
                        soldActual = actualKilo.Sum(Function(v) multiplyFields(v.j))
                        soldSpoilage = spoilageKilo.Sum(Function(v) multiplyFields(v.j))
                End Select

                kiloMix = kiloActualMix - kiloSpoilageMix
                kiloFishMeal = kiloActualFishMeal - kiloSpoilageFishMeal
                totalSoldUSD = soldActual - soldSpoilage
            End If

            'Dim kiloFishMeal = Math.Round(s.j.fishmeal, 2)
            'Dim totalPrice = Math.Round(soldKilos(s.j.salesInvoiceBuyerID).Sum(Function(sa) multiplyFields(sa)), 2)
            Dim buyerName = If(Integer.TryParse(s.i.buyerName, 0), buyers(CInt(s.i.buyerName)).FirstOrDefault.ml_Supplier, s.i.buyerName)
            Dim backerName = If(backingData.Contains(CInt(s.i.salesInvoiceBuyerID)), backingData(CInt(s.i.salesInvoiceBuyerID)).FirstOrDefault().buyerName, "")
            Dim backerActual = 0D
            Dim backerSpoilage = 0D
            Dim backerTotalKilos = 0D
            If backingKilos.Contains(CInt(s.i.salesInvoiceBuyerID)) Then
                Dim totalBacking = backingKilos(CInt(s.i.salesInvoiceBuyerID)).Count()
                backerActual = backingKilos(CInt(s.i.salesInvoiceBuyerID)).Take(totalBacking \ 2).Sum(Function(v) sumFields(v.j))
                backerSpoilage = backingKilos(CInt(s.i.salesInvoiceBuyerID)).Skip(totalBacking \ 2).Sum(Function(v) sumFields(v.j))
                backerTotalKilos = backerActual - backerSpoilage
            End If


            sib.CatcherName = catcherName
            sib.CarrierName = carrierList.Where(Function(a) a.ID = s.i.carrier).Select(Function(a) a.Name).FirstOrDefault
            sib.InvoiceNum = s.k.invoiceNum
            sib.SetNum = s.i.setNum
            sib.PriceTotalPeso = totalSoldUSD * s.k.usdRate
            sib.usdRate = s.k.usdRate
            sib.PriceTotalUSD = totalSoldUSD
            sib.DateFrom = CDate(dates.Min)
            sib.DateTo = CDate(dates.Max)
            sib.KiloMix = kiloMix
            sib.KiloFishMeal = kiloFishMeal
            sib.BuyerName = buyerName
            sib.BuyerType = s.i.sellerType
            sib.BackingName = backerName
            sib.BackingValue = backerTotalKilos

            retList.Add(sib)
        Next

        Return retList
    End Function

    Function sumFields(record As trans_SalesReportBuyer, Optional category As String = "Canning") As Decimal
        If category = "Local" Then
            Return record.yellowfin0_300To0_499 +
               record.yellowfin0_500To0_999 + record.yellowfin1_0To1_49 +
               record.yellowfin1_5To2_49 + record.yellowfin2_5To3_49 +
               record.yellowfin3_5To4_99 + record.yellowfin5_0To9_99 +
               record.yellowfin10AndUpGood + record.yellowfin10AndUpDeformed
        ElseIf category = "Export" Then
            Return record.yellowfin10AndUpGood + record.yellowfin10AndUpDeformed
        End If

        Return record.skipjack0_300To0_499 + record.skipjack0_500To0_999 +
               record.skipjack1_0To1_39 + record.skipjack1_4To1_79 +
               record.skipjack1_8To2_49 + record.skipjack2_5To3_49 +
               record.skipjack3_5AndUP + record.yellowfin0_300To0_499 +
               record.yellowfin0_500To0_999 + record.yellowfin1_0To1_49 +
               record.yellowfin1_5To2_49 + record.yellowfin2_5To3_49 +
               record.yellowfin3_5To4_99 + record.yellowfin5_0To9_99 +
               record.yellowfin10AndUpGood + record.yellowfin10AndUpDeformed +
               record.bigeye0_500To0_999 + record.bigeye1_0To1_49 +
               record.bigeye1_5To2_49 + record.bigeye2_5To3_49 +
               record.bigeye3_5To4_99 + record.bigeye5_0To9_99 +
               record.bigeye10AndUP + record.bonito
    End Function

    Function multiplyFields(qty As trans_SalesReportBuyer, Optional category As String = "Canning") As Decimal
        Dim total As Decimal = 0

        Dim dc = New mkdbDataContext

        Dim price = (From i In dc.trans_SalesReportPrices Where qty.salesInvoiceID = i.salesReport_ID Select i).FirstOrDefault

        If category = "Local" Then
            total += qty.yellowfin0_300To0_499 * price.yellowfin0_300To0_499
            total += qty.yellowfin0_500To0_999 * price.yellowfin0_500To0_999
            total += qty.yellowfin1_0To1_49 * price.yellowfin1_0To1_49
            total += qty.yellowfin2_5To3_49 * price.yellowfin2_5To3_49
            total += qty.yellowfin3_5To4_99 * price.yellowfin3_5To4_99
            total += qty.yellowfin5_0To9_99 * price.yellowfin5_0To9_99
            total += qty.yellowfin10AndUpGood * price.yellowfin10AndUpGood
            total += qty.yellowfin10AndUpDeformed * price.yellowfin10AndUpDeformed
        ElseIf category = "Export" Then
            total += qty.yellowfin10AndUpGood * price.yellowfin10AndUpGood
            total += qty.yellowfin10AndUpDeformed * price.yellowfin10AndUpDeformed
        Else
            total += qty.skipjack0_300To0_499 * price.skipjack0_300To0_499
            total += qty.skipjack0_500To0_999 * price.skipjack0_500To0_999
            total += qty.skipjack1_0To1_39 * price.skipjack1_0To1_39
            total += qty.skipjack1_4To1_79 * price.skipjack1_4To1_79
            total += qty.skipjack1_8To2_49 * price.skipjack1_8To2_49
            total += qty.skipjack2_5To3_49 * price.skipjack2_5To3_49
            total += qty.skipjack3_5AndUP * price.skipjack3_5AndUP
            total += qty.yellowfin0_300To0_499 * price.yellowfin0_300To0_499
            total += qty.yellowfin0_500To0_999 * price.yellowfin0_500To0_999
            total += qty.yellowfin1_0To1_49 * price.yellowfin1_0To1_49
            total += qty.yellowfin1_5To2_49 * price.yellowfin1_5To2_49
            total += qty.yellowfin2_5To3_49 * price.yellowfin2_5To3_49
            total += qty.yellowfin3_5To4_99 * price.yellowfin3_5To4_99
            total += qty.yellowfin5_0To9_99 * price.yellowfin5_0To9_99
            total += qty.yellowfin10AndUpGood * price.yellowfin10AndUpGood
            total += qty.yellowfin10AndUpDeformed * price.yellowfin10AndUpDeformed
            total += qty.bigeye0_500To0_999 * price.bigeye0_500To0_999
            total += qty.bigeye1_0To1_49 * price.bigeye1_0To1_49
            total += qty.bigeye1_5To2_49 * price.bigeye1_5To2_49
            total += qty.bigeye2_5To3_49 * price.bigeye2_5To3_49
            total += qty.bigeye3_5To4_99 * price.bigeye3_5To4_99
            total += qty.bigeye5_0To9_99 * price.bigeye5_0To9_99
            total += qty.bigeye10AndUP * price.bigeye10AndUP
            total += qty.bonito * price.bonito
            total += qty.fishmeal * price.fishmeal
        End If

        Return Math.Round(total, 2)
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
End Module

