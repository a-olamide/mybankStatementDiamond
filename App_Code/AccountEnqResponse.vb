Imports Microsoft.VisualBasic

Public Class AccountEnqResponse
    Private _Details As AccountEnq

    Private _ResponseCode As String

    Private _ResponseMessage As String
    Public Property Details() As AccountEnq
        Get
            Return _Details
        End Get
        Set(ByVal Details As AccountEnq)
            _Details = Details
        End Set
    End Property
    Public Property ResponseCode() As String
        Get
            Return _ResponseCode
        End Get
        Set(ByVal ResponseCode As String)
            _ResponseCode = ResponseCode
        End Set
    End Property

    Public Property ResponseMessage() As String
        Get
            Return _ResponseMessage
        End Get
        Set(ByVal ResponseMessage As String)
            _ResponseMessage = ResponseMessage
        End Set
    End Property
End Class
