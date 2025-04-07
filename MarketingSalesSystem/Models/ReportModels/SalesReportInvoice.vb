Public Class SalesReportInvoice
    ' Sales Report Column
    Public Property SalesReportID As Integer
    Public Property UnloadingVesselID As String
    Public Property UnloadingForeignVessel As String
    Public Property Buyer As String
    Public Property SellingType As String
    Public Property UnloadingType As String
    Public Property InvoiceNum As String
    Public Property ReferenceNum As String
    Public Property SalesNum As String
    Public Property CatchDeliveryNum As String
    Public Property ContractNum As String
    Public Property Remarks As String
    Public Property EncodedBy As String
    Public Property ApprovalStatus As String
    Public Property SalesDate As Date
    Public Property EncodedOn As Date
    Public Property UsdRate As Decimal

    ' Sales Report Catcher
    Public Property SalesReportCatcherID As Integer
    Public Property CatchActivityDetailID As Integer
    Public Property Skipjack0To300To499 As Decimal
    Public Property Skipjack0To500To999 As Decimal
    Public Property Skipjack1To0To1To39 As Decimal
    Public Property Skipjack1To4To1To79 As Decimal
    Public Property Skipjack1To8To2To49 As Decimal
    Public Property Skipjack2To5To3To49 As Decimal
    Public Property Skipjack3To5AndUP As Decimal
    Public Property Yellowfin0To300To499 As Decimal
    Public Property Yellowfin0To500To999 As Decimal
    Public Property Yellowfin1To0To1To49 As Decimal
    Public Property Yellowfin1To5To2To49 As Decimal
    Public Property Yellowfin2To5To3To49 As Decimal
    Public Property Yellowfin3To5To4To99 As Decimal
    Public Property Yellowfin5To0To9To99 As Decimal
    Public Property Yellowfin10AndUpGood As Decimal
    Public Property Yellowfin10AndUpDeformed As Decimal
    Public Property Bigeye0To500To999 As Decimal
    Public Property Bigeye1To0To1To49 As Decimal
    Public Property Bigeye1To5To2To49 As Decimal
    Public Property Bigeye2To5To3To49 As Decimal
    Public Property Bigeye3To5To4To99 As Decimal
    Public Property Bigeye5To0To9To99 As Decimal
    Public Property Bigeye10AndUP As Decimal
    Public Property Bonito As Decimal
    Public Property Fishmeal As Decimal

    ' Sales Report Price
    Public Property SalesReportPriceID As Integer
    Public Property SRPSalesReportCatcherID As Integer
    Public Property SRPCatchActivityDetailID As Integer
    Public Property SRPSkipjack0To300To499 As Decimal
    Public Property SRPSkipjack0To500To999 As Decimal
    Public Property SRPSkipjack1To0To1To39 As Decimal
    Public Property SRPSkipjack1To4To1To79 As Decimal
    Public Property SRPSkipjack1To8To2To49 As Decimal
    Public Property SRPSkipjack2To5To3To49 As Decimal
    Public Property SRPSkipjack3To5AndUP As Decimal
    Public Property SRPYellowfin0To300To499 As Decimal
    Public Property SRPYellowfin0To500To999 As Decimal
    Public Property SRPYellowfin1To0To1To49 As Decimal
    Public Property SRPYellowfin1To5To2To49 As Decimal
    Public Property SRPYellowfin2To5To3To49 As Decimal
    Public Property SRPYellowfin3To5To4To99 As Decimal
    Public Property SRPYellowfin5To0To9To99 As Decimal
    Public Property SRPYellowfin10AndUpGood As Decimal
    Public Property SRPYellowfin10AndUpDeformed As Decimal
    Public Property SRPBigeye0To500To999 As Decimal
    Public Property SRPBigeye1To0To1To49 As Decimal
    Public Property SRPBigeye1To5To2To49 As Decimal
    Public Property SRPBigeye2To5To3To49 As Decimal
    Public Property SRPBigeye3To5To4To99 As Decimal
    Public Property SRPBigeye5To0To9To99 As Decimal
    Public Property SRPBigeye10AndUP As Decimal
    Public Property SRPBonito As Decimal
    Public Property SRPFishmeal As Decimal


    Sub New()

    End Sub
End Class
