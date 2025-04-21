Public Class CarrierDetails
    Public CarrierID As Integer
    Public SalesReportID As Integer
    Public CarrierName As String
    Public CarrierType As String
    Public UnloadedValue As Decimal
    Public DateCreated As Date
    'Hello
    Private dc As mkdbDataContext


    Sub New(ByRef dc_ As mkdbDataContext)
        dc = dc_
    End Sub

    Sub New(ByVal CarrierID As Integer, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Dim e = From i In dc.trans_SalesUnloadeds Where i.CarrierID = CarrierID Select i

        For Each i In e
            Me.CarrierID = i.CarrierID
            SalesReportID = i.SalesReportID
            CarrierName = i.CarrierName
            CarrierType = i.CarrierType
            UnloadedValue = i.UnloadedValue
            DateCreated = i.DateCreated
        Next
    End Sub

    Sub New(ByRef carrier As trans_SalesUnloaded, ByRef dc_ As mkdbDataContext)
        dc = dc_

        Me.CarrierID = carrier.CarrierID
        SalesReportID = carrier.SalesReportID
        CarrierName = carrier.CarrierName
        CarrierType = carrier.CarrierType
        UnloadedValue = carrier.UnloadedValue
        DateCreated = carrier.DateCreated
    End Sub

    Sub Add()
        Dim carrier As New trans_SalesUnloaded

        With carrier
            .SalesReportID = SalesReportID
            .CarrierName = CarrierName
            .CarrierType = CarrierType
            .UnloadedValue = UnloadedValue
            .DateCreated = DateCreated
        End With

        dc.trans_SalesUnloadeds.InsertOnSubmit(carrier)
        dc.SubmitChanges()
        Me.CarrierID = carrier.CarrierID
    End Sub

    Sub Save()
        Dim carrier = From i In dc.trans_SalesUnloadeds Where i.CarrierID = CarrierID Select i


        For Each i In carrier
            i.SalesReportID = SalesReportID
            i.CarrierName = CarrierName
            i.CarrierType = CarrierType
            i.UnloadedValue = UnloadedValue
            i.DateCreated = DateCreated
            dc.SubmitChanges()
        Next
    End Sub

    Sub Delete()
        Dim carrier = From i In dc.trans_SalesUnloadeds Where i.CarrierID = CarrierID Select i

        For Each i In carrier
            dc.trans_SalesUnloadeds.DeleteOnSubmit(i)
            dc.SubmitChanges()
        Next
    End Sub
End Class
