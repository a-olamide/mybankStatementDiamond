Imports Microsoft.VisualBasic

Public Class AccountEnq

    Private _AccountNumber As String

    Private _AccountName As String

    Private _AccountType As String

    Private _AccountCategory As String
    Private _AvailableBalance As Decimal

    Private _Address As String

    Private _Email As String

    Private _Telephone As String
    Private _BookBalance As Decimal

    Public Property AccountNumber() As String
        Get
            Return _AccountNumber
        End Get
        Set(ByVal AccountNumber As String)
            _AccountNumber = AccountNumber
        End Set
    End Property

    Public Property AccountName() As String
        Get
            Return _AccountName
        End Get
        Set(ByVal AccountName As String)
            _AccountName = AccountName
        End Set
    End Property
    Public Property AccountType() As String
        Get
            Return _AccountType
        End Get
        Set(ByVal AccountType As String)
            _AccountType = AccountType
        End Set
    End Property
    Public Property AvailableBalance() As Decimal
        Get
            Return _AvailableBalance
        End Get
        Set(ByVal AvailableBalance As Decimal)
            _AvailableBalance = AvailableBalance
        End Set
    End Property
    Public Property BookBalance() As Decimal
        Get
            Return _BookBalance
        End Get
        Set(ByVal BookBalance As Decimal)
            _BookBalance = BookBalance
        End Set
    End Property
    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal Address As String)
            _Address = Address
        End Set
    End Property
    Public Property Telephone() As String
        Get
            Return _Telephone
        End Get
        Set(ByVal Telephone As String)
            _Telephone = Telephone
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal Email As String)
            _Email = Email
        End Set
    End Property
    Public Property AccountCategory() As String
        Get
            Return _AccountCategory
        End Get
        Set(ByVal AccountCategory As String)
            _AccountCategory = AccountCategory
        End Set
    End Property
End Class
