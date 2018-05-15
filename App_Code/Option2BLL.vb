Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Public Class Option2BLL
    '***********************NIBSSSSSSSSSSSSSSSSS*****************************************
    'This insert new request on spoke
    Public Shared Function insertRequest(accountName As String, BankName As String, StartDate As String, EndDate As String, _
                                    username As String, AccountNo As String, BankID As String, DestinationID As String, Role As String, ApplicantNames() As String) As String
        Try
            Dim requestID As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_InsertRequest", accountName, BankName, _
                                          username, AccountNo, BankID, DestinationID, "NGN", CDate(StartDate), CDate(EndDate), Role)
            For Each item As String In ApplicantNames
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_InsertRequestApplicants", requestID, _
                                          item)
            Next
            Return requestID
        Catch ex As Exception
            Return "fail"
        End Try
    End Function

    'This belongs on the Hub.
    Public Shared Function ConfirmStatement(RequestID As String, TicketNo As String, Passcode As String, NibbssPassword As String) As Int16
        Try


            Dim res As Int16 = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_confirmTicket", RequestID, _
                                              TicketNo, Passcode)
            If res = "1" Then
                Return 1
            ElseIf res = "0" Then
                Return 0
            Else
                Return -1

            End If
        Catch ex As Exception
            Return -1
        End Try
    End Function
    'This insert request at Nibss. once that is done, the insertStatementRequest returns the request ID which is used to insert applicants
    Public Shared Function insertStatementRequestNibss(requestID As String, AccountNo As String, BankID As String, NibbsClientID As String, StartDate As String, _
                                     EndDate As String, Role As String, username As String, accountName As String, companyName As String, country As String, phone As String, ApplicantNames() As String) As String
        Try
            Dim Id As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_InsertRequest", requestID, _
                                          accountName, Nothing, username, AccountNo, BankID, NibbsClientID, country, CDate(StartDate), CDate(EndDate), Role, phone)
            For Each item As String In ApplicantNames
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_InsertRequestApplicants", requestID, _
                                          item)
            Next
            Return Id
        Catch ex As Exception
            Return "0"
        End Try
    End Function


    'This updates records on mybankStatement service
    Public Shared Function updateRequestStatus(RequestID As String, Status As String, TicketNO As String, Password As String, BibssPassword As String) As Boolean
        Try
            Dim Id As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionStringOption2").ToString, "_updateRequestStatus", _
                                           RequestID, Status, TicketNO, Password)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'This belongs to the Hub. to bee called from the spoke
    Public Shared Function GetRequestedActivity(ByVal ID As String) As DataTable
        Try
            Return SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectStatementRequest", ID).Tables(0)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'This belongs to the Nibbss
    Public Shared Function selectRequestApplicants(ByVal RequestID As String) As String
        Try
            Return SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionStringOtion2").ToString, "_selectRequestApplicants", RequestID)

        Catch ex As Exception

            Return Nothing
        End Try

    End Function
    'This belongs to the Hub. to bee called from the spoke
    Public Shared Function selectRequestApplicantFromSpoke(ByVal RequestID As String) As String
        Try
            Return SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectRequestApplicantFromSpoke", RequestID)

        Catch ex As Exception

            Return Nothing
        End Try

    End Function
    'This belong to the spoke. where values are entered into tblActvity
    Public Shared Function insertStatementRequestToSpoke(RequestID As String, AccountNo As String, destinationID As String, StartDate As String, _
                                    EndDate As String, Role As String, username As String, accountName As String, psw As String, _
                                    type As String, address As String, email As String, currency As String, _
                                     category As String, clr_bal As Decimal, avl_bal As Decimal, bvn As String, ApplicantNames() As String) As String
        Try
            Dim requestIDRes As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_InsertStatementRequestToSpoke", RequestID, _
                                           AccountNo, destinationID, CDate(StartDate), CDate(EndDate), Role, username, accountName, psw, type, _
                                           address, email, currency, category, clr_bal, avl_bal, bvn)
            For Each item As String In ApplicantNames
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_InsertRequestApplicants", RequestID, item)
            Next
            Return requestIDRes
        Catch ex As Exception
            Return -1
        End Try
    End Function

    'This belongs to spoke where additional info is queried to generate statement
    Public Shared Function SelectActivityFromSpokeBYRequestID(ByVal RequestID As String) As DataTable
        Try
            Return SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectActivityFromSpokeBYRequestID", RequestID).Tables(0)

        Catch ex As Exception

            Return Nothing
        End Try

    End Function
    'This belongs to spoke the activity are updated with no of pages amount etc
    Public Shared Function updateActivityByRequestID(RequestID As String, cr As Double, dr As Double, pageCount As Int16, basic As Double, additional As Double, total As Double, vat As Double) As Boolean
        Try
            Dim Id As String = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updateActivityByRequestID", _
                                           RequestID, cr, dr, pageCount, basic, additional, total, vat)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function updateNewRequestStatus(RequestID As String, Status As String) As Boolean
        Try
            Dim Id As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updateNewRequestStatus", _
                                           RequestID, Status)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    
End Class
