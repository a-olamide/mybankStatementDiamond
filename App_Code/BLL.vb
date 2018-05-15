Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Sql
Imports System.Configuration
Imports System
Imports System.Data.DataSet
Imports System.IO
Imports System.Web
Imports System.Web.Configuration
Imports System.Net

Public Class BLL
    Public Shared Sub _updateNoCredit(ByVal type As String, ByVal status As String, ByVal ticketNo As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updateNoCredit", type, status, ticketNo)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function _authenticateUser(ByVal UserName As String, ByVal Password As String, ByVal status As Integer) As String()
        Dim dr As SqlDataReader = Nothing
        Dim Details(0 To 15) As String
        Try
            dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_authenticateUser", UserName, Password, status)
            While dr.Read
                If dr.IsDBNull(dr.GetOrdinal("FirstName")) = False Then
                    Details(0) = CStr(IIf(IsDBNull(dr("Name")), "", dr("Name")))
                    Details(1) = CStr(IIf(IsDBNull(dr("FirstName")), "", dr("FirstName")))
                    Details(2) = CStr(IIf(IsDBNull(dr("Surname")), "", dr("Surname")))
                    Details(3) = CStr(IIf(IsDBNull(dr("PhoneNumber")), "", dr("PhoneNumber")))
                    Details(4) = CStr(IIf(IsDBNull(dr("Email")), "", dr("Email")))
                    Details(5) = CStr(IIf(IsDBNull(dr("Admin")), "", dr("Admin")))
                    Details(6) = CStr(IIf(IsDBNull(dr("Company")), "", dr("Company")))
                    Details(7) = CStr(IIf(IsDBNull(dr("Role")), "N/A", dr("Role")))
                    Details(8) = CStr(IIf(IsDBNull(dr("Status")), "N/A", dr("Status")))
                    Details(9) = CStr(IIf(IsDBNull(dr("LockDate")), "N/A", dr("LockDate")))
                    Details(10) = CStr(IIf(IsDBNull(dr("BranchName")), "", dr("BranchName")))
                    Details(11) = CStr(IIf(IsDBNull(dr("BranchID")), "", dr("BranchID")))
                    Details(12) = CStr(IIf(IsDBNull(dr("supervisor")), "", dr("supervisor")))
                    Details(13) = CStr(IIf(IsDBNull(dr("Country")), "", dr("Country")))
                Else
                    Details(0) = ""
                End If
            End While
            dr.Close()
            Return Details
        Catch ex As Exception
            Return Nothing
        Finally
            dr.Close()
        End Try
    End Function
    Public Shared Function _receiveSaltDetails(ByVal UserName As String) As String


        Try
            Return SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_receiveSaltDetails", UserName)


        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    Public Shared Function allUser() As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "allUser")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub updateUserRow(pass As String, salt As String, email As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "updateUserRow", pass, salt, email)
    End Sub
    Public Shared Function _selectDestinationName(ByVal ID As Integer) As String()
        Dim dr As SqlDataReader = Nothing
        Dim Details(0 To 3) As String
        Try
            dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_selectDestinationName", ID)
            While dr.Read
                If dr.IsDBNull(dr.GetOrdinal("Name")) = False Then
                    Details(0) = CStr(IIf(IsDBNull(dr("Name")), "", dr("Name")))
                    Details(1) = CStr(IIf(IsDBNull(dr("cost")), "0", dr("cost")))
                    Details(2) = CStr(IIf(IsDBNull(dr("csvStatus")), "0", dr("csvStatus")))
                    Details(3) = CStr(IIf(IsDBNull(dr("newTicket")), "0", dr("newTicket")))
                Else
                    Details(0) = ""
                End If
            End While
            dr.Close()
            Return Details
        Catch ex As Exception
            Return Nothing
        Finally
            dr.Close()
        End Try
    End Function
    Public Shared Function _selectCSVStatusByCompany(ByVal id As Integer) As String
        Dim status As String
        Try
            status = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_selectCSVStatusByCompany", id)
            Return status
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectCSVStatus(ByVal id As String) As String
        Dim status As String
        Try
            status = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_selectCSVStatus", id)
            Return status
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectMetaData(ByVal ID As Integer) As String()
        Dim dr As SqlDataReader = Nothing
        Dim Details(0 To 23) As String
        Try
            dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_selectMetaData", ID)
            While dr.Read
                If dr.IsDBNull(dr.GetOrdinal("TicketNo")) = False Then
                    Details(0) = CStr(IIf(IsDBNull(dr("TicketNo")), "", dr("TicketNo")))
                    Details(1) = CStr(IIf(IsDBNull(dr("username")), "", dr("username")))
                    Details(2) = CStr(IIf(IsDBNull(dr("NUBAN")), "", dr("NUBAN")))
                    Details(3) = CStr(IIf(IsDBNull(dr("DestinationName")), "", dr("DestinationName")))
                    Details(4) = CStr(IIf(IsDBNull(dr("userid")), "", dr("userid")))
                    Details(5) = CStr(IIf(IsDBNull(dr("name")), "", dr("name")))
                    Details(6) = CStr(IIf(IsDBNull(dr("Role")), "", dr("Role")))
                    Details(7) = CStr(IIf(IsDBNull(dr("StartDate")), "", dr("StartDate")))
                    Details(8) = CStr(IIf(IsDBNull(dr("EndDate")), "", dr("EndDate")))
                    Details(9) = CStr(IIf(IsDBNull(dr("Page")), "", dr("Page")))
                    Details(10) = CDbl(IIf(IsDBNull(dr("Basic")), "0", dr("Basic")))
                    Details(11) = CDbl(IIf(IsDBNull(dr("additional")), "0", dr("additional")))
                    Details(12) = CStr(IIf(IsDBNull(dr("total")), "0", dr("total")))
                    Details(13) = CInt(IIf(IsDBNull(dr("destination")), "0", dr("destination")))
                    Details(14) = CInt(IIf(IsDBNull(dr("SourceID")), "0", dr("SourceID")))
                    Details(15) = CStr(IIf(IsDBNull(dr("Source")), "", dr("Source")))
                    Details(16) = CStr(IIf(IsDBNull(dr("ApplicationNo")), "", dr("ApplicationNo")))
                    Details(17) = CStr(IIf(IsDBNull(dr("country")), "", dr("country")))
                    Details(18) = CStr(IIf(IsDBNull(dr("email")), "", dr("email")))
                    Details(19) = CStr(IIf(IsDBNull(dr("category")), "", dr("category")))
                    Details(20) = CStr(IIf(IsDBNull(dr("randPsw")), "", dr("randPsw")))
                    Details(21) = CStr(IIf(IsDBNull(dr("bvn")), "", dr("bvn")))
                    Details(22) = CStr(IIf(IsDBNull(dr("currency")), "", dr("currency")))
                    Details(23) = CStr(IIf(IsDBNull(dr("phone")), "", dr("phone")))
                Else
                    Details(0) = ""
                End If
            End While
            dr.Close()
            Return Details
        Catch ex As Exception
            Return Nothing
        Finally
            dr.Close()
        End Try
    End Function

    Public Shared Function _selectLastSystemUpdate() As DateTime
        Dim dr As SqlDataReader = Nothing
        Dim Datee As DateTime = Nothing
        Try
            dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_selectLastSystemUpdate")
            While dr.Read
                If dr.IsDBNull(dr.GetOrdinal("ID")) = False Then
                    Datee = CType((IIf(IsDBNull(dr("lastsystemupdate")), CDate("1-1-1901"), dr("lastsystemupdate"))), DateTime)
                Else
                    Datee = CType("1-1-1901", DateTime)
                End If
            End While
            dr.Close()
            Return Datee
        Catch ex As Exception
            Return Nothing
        Finally
            dr.Close()
        End Try
    End Function

    Public Shared Function _selectCurrentDestinations() As SqlDataReader
        Dim dr As SqlDataReader = Nothing
        Try
            dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_selectCurrentDestinations")
            Return dr
        Catch ex As Exception
            Return Nothing
        Finally
            dr.Close()
        End Try
    End Function
    Public Shared Function _selectCurrentCountries() As SqlDataReader
        Dim dr As SqlDataReader = Nothing
        Try
            dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_selectCurrentCountries")
            Return dr
        Catch ex As Exception
            Return Nothing
        Finally
            dr.Close()
        End Try
    End Function
    Public Shared Function _selectPendingStatements() As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_selectPendingStatements")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectTransactions(ByVal startDate As Date, ByVal endDate As Date, ByVal requestid As Integer) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_selectTransactions", startDate, endDate, requestid)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectTransactionsTest(ByVal startDate As Date, ByVal endDate As Date, ByVal requestid As Integer) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_selectTransactionsTest", startDate, endDate, requestid)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _updateRequest(ByVal startDate As Date, ByVal endDate As Date, ByVal Destination As String, ByVal Role As String,
                                           ByVal requestid As Integer, ByVal cr As Double, ByVal dr As Double, ByVal PageCount As Integer, ByVal _
                                           Basic As Double, ByVal Additional As Double, ByVal Total As Double, ByVal vat As Double,
                                          Optional ByVal waive As String = "0", Optional ByVal debitAccount As String = "") As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_updateRequest", startDate, endDate, Destination, Role, requestid, cr, dr, PageCount, Basic, Additional, Total, vat, waive, debitAccount)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _updatePassword(ByVal UserName As String, ByVal Password As String) As String
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_updatePassword", UserName, Password)
            Return "1"
        Catch ex As Exception
            Return "0"
        End Try
    End Function
    Public Shared Function _updateStatus(ByVal requestid As Integer, ByVal status As String) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_updateStatus", requestid, status)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _updateDestination(ByVal ID As Integer, ByVal value As String, ByVal float As Decimal, ByVal CID As Integer) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_updateDestination", ID, value, float, CID)
            _updateLastSystemUpdate(Now)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _updateCountries(ByVal ID As Integer, ByVal value As String, ByVal CID As Integer) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_updateCountries", ID, value, CID)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _updateLastSystemUpdate(ByVal Datee As DateTime) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_updateLastSystemUpdate", Datee)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _insertDestination(ByVal ID As Integer, ByVal CountryCode As String, ByVal CategoryID As Integer, ByVal Name As String,
                                              ByVal Cost As Decimal, ByVal csv As String, ByVal newTicket As String, ByVal status As String,
                                              ByVal mail As String, ByVal reqStatus As String) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_insertDestination", ID, CountryCode, CategoryID, Name, Cost, csv, newTicket, status, mail, reqStatus)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _InsertDestinationFormat(ByVal destID As Integer, ByVal FormatID As Integer, ByVal status As String) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_InsertDestinationFormat", destID, FormatID, status)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _insertStatementRole(ByVal ID As Integer, ByVal Role As String) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_insertStatementRole", ID, Role)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _insertCountry(ByVal ID As Integer, ByVal CountryCode As String, ByVal Country As String, ByVal Active As Integer, ByVal Continent As String) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_insertCountry", ID, CountryCode, Country, Active, Continent)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _insertApplicant(ByVal name As String, ByVal requestID As Integer) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                "_insertApplicant", name, requestID)
            Return dID
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return 0
        Catch ex As Exception
            Return 0

        End Try
    End Function
    Public Shared Function _insertSP(ByVal name As String, ByVal requestID As String) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                "_insertSP", name, requestID)
            Return dID
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return 0
        Catch ex As Exception
            Return 0

        End Try
    End Function
    Public Shared Function _insertReceived(ByVal ticketno As String, ByVal name As String, ByVal source As String, ByVal userid As String,
                                           ByVal Nuban As String, ByVal sourceid As String, ByVal id As String, ByVal country As String, ByVal csv As String, ByVal timestamp As Date) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                "_insertReceived", name, ticketno, source, userid, Nuban, sourceid, id, country, csv, timestamp)
            Return dID
        Catch ex As Exception
            Return 0

        End Try
    End Function
    Public Shared Function _insertRequest(ByVal name As String, ByVal source As String, ByVal userid As String,
                                          ByVal Nuban As String, ByVal sourceid As String, ByVal id As String, ByVal country As String, ByVal sDate As Date, ByVal edate As Date) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                "_insertRequest", name, source, userid, Nuban, sourceid, id, country, sDate, edate)
            Return dID
        Catch ex As Exception
            Return -1

        End Try
    End Function
    Public Shared Function _deleteApplicant(ByVal ID As Integer) As Integer
        Dim int As Integer = 0
        Try
            int = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                      "_deleteApplicant", ID)
            Return int
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _deleteActivity(ByVal ID As Integer) As Integer
        Dim int As Integer = 0
        Try
            int = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                      "_deleteActivity", ID)
            Return int
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectDestinations() As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectDestinations")
            Return dreader
        Catch ex As Exception
            Return Nothing
        Finally
            dreader.Close()
        End Try
    End Function
    Public Shared Function _selectCountry() As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectCountry")
            Return dreader
        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    Public Shared Function _selectRequestDestination() As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectRequestDestination")
            Return dreader
        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    Public Shared Function _selectUBAUserCountry(ByVal country As String) As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUBAUserCountry", country)
            Return dreader
        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    Public Shared Function _selectCategory(ByVal countrycode As String) As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectCategory", countrycode)
            Return dreader
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectDestinationByID(ByVal countrycode As String, ByVal category As String) As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectDestinationByID", countrycode, category)
            Return dreader
        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    Public Shared Function _selectAccountbyRequestID(ByVal value As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectAccountbyRequestID", value)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectActivitybyRequestID(ByVal value As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectActivitybyRequestID", value)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectApplicant(ByVal value As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectApplicant", value)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectSignatories(ByVal value As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectSignatories", value)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectActivitybyFilter(ByVal userID As String, ByVal value As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectActivitybyFilter", userID, value)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectSentActivitybyFilter(ByVal userID As String, ByVal value As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectSentActivitybyFilter", userID, value)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectActivity(ByVal userID As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectActivity", userID)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectSentActivity(ByVal userID As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectSentActivity", userID)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectReceived() As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectReceived")
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectRequest() As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectRequest")
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectReceivedByFilter(ByVal ticketNo As String, ByVal filter As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectReceivedByFilter", ticketNo, filter)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectRequestByFilter(ByVal filter As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectRequestByFilter", filter)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectAccount(ByVal value As String, ByVal userID As String, ByVal user As String, ByVal requestid As Integer, ByVal branchName As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectAccount", value, userID, user, requestid, branchName)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectAccountOra(ByVal acctNo As String, ByVal name As String, ByVal type As String, ByVal category As String,
                                              ByVal CLR_BAL As String, ByVal AVL_BAL As String, ByVal address As String, ByVal email As String,
                                             ByVal userID As String, ByVal user As String, ByVal requestid As Integer, ByVal branchName As String,
                                             ByVal custBranch As String, ByVal currency As String, ByVal bvn As String, ByVal telephone As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectAccountOra",
                                          acctNo, name, type, category, CLR_BAL, AVL_BAL, address, email, userID, user, requestid, branchName, custBranch, currency, bvn, telephone)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _insertUser(ByVal FirstName As String, ByVal Surname As String,
    ByVal UserName As String, ByVal Role As String, ByVal Type As Integer) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertUser", FirstName, Surname, UserName, Role, Type)
            Return dID

        Catch ex As Exception
            Return 0

        End Try
    End Function
    Public Shared Sub _insertAuditLogs(ByVal UserName As String, ByVal Destination As String, ByVal Role As String,
                                            ByVal TicketID As String, ByVal Action As String, ByVal Timestamp As DateTime)

        Dim dID As Integer = Nothing
        Try
            Dim IP As String = Utility.getIPAddress()
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertAuditLogs", UserName, Destination, Role, TicketID, Action, Timestamp, IP)

        Catch ex As Exception


        End Try
    End Sub


    Public Shared Function _deleteUser(ByVal email As String) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_dropUser", email)
            Return dID
        Catch ex As Exception
            Return 0

        End Try
    End Function
    Public Shared Function _deleteADROle(ByVal id As String) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_deleteADROle", id)
            Return dID
        Catch ex As Exception
            Return 0

        End Try
    End Function

    Public Shared Function _selectUsers(ByVal status As Integer) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUsers", status)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectUsersbyfilter(ByVal filter As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUsersbyfilter", filter)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _checkNewUser(ByVal email As String) As String

        Try
            Dim str As String
            str = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_CheckNewUser", email)
            Return str
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectADRole(ByVal status As Integer) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectADRole", status)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectCompany() As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectCompany")
            Return dreader
            'Catch exe As InvalidOperationException
            '    HttpContext.Current.Response.Redirect("Login.aspx")
        Catch ex As Exception
		Utility.LogException(ex.Message())
            Return Nothing

        End Try
    End Function
    Public Shared Function _selectAuditLogs() As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectAuditLogs")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectAuditLogs(ByVal uname As String, ByVal sDate As String, ByVal eDate As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectAuditLogs", uname, sDate, eDate)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectAuditLogsReport(ByVal uname As String, ByVal sDate As String, ByVal eDate As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectAuditLogsReport", uname, sDate, eDate)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectUserbyID(ByVal email As String) As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUserbyID", email)
            Return dreader
        Catch ex As Exception
            Return Nothing
        Finally
            ' dreader.Close()
        End Try
    End Function
    Public Shared Function _selectADRolerbyID(ByVal id As String) As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectADRolerbyID", id)
            Return dreader
        Catch ex As Exception
            Return Nothing
        Finally
            ' dreader.Close()
        End Try
    End Function
    Public Shared Function _selectRole() As SqlDataReader
        Dim drRole As IDataReader = Nothing
        Try
            drRole = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectRole")
            Return drRole
        Catch ex As Exception
            Return Nothing
        Finally
            'drRole.Close()
        End Try
    End Function
    Public Shared Function _selectStatementRole() As SqlDataReader
        Dim drRole As IDataReader = Nothing
        Try
            drRole = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectStatementRole")
            Return drRole
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectRoleAdmin() As SqlDataReader
        Dim drRole As IDataReader = Nothing
        Try
            drRole = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectRoleAdmin")
            Return drRole
        Catch ex As Exception
            Return Nothing
        Finally
            'drRole.Close()
        End Try
    End Function

    Public Shared Function _insertStatementUnsentLog(ByVal TicketNo As Integer, ByVal destinationID As Integer, ByVal SourceID As Integer,
                                 ByVal type As String, ByVal status As String) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertStatementUnsentLog", TicketNo, destinationID, SourceID, type, status)
            Return dID.ToString
        Catch ex As Exception
            Return "Fail"

        End Try
    End Function
    Public Shared Sub _updatePdfCsvStatsus(ByVal status As Byte, ByVal type As String, ByVal ticketNo As Int64)
        Try

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updatePdfCsvStatsus",
                                     status, type, ticketNo)

        Catch ex As Exception

        End Try

    End Sub

    Public Shared Function _selectUnsentData() As DataTable
        Try
            Return SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                            "_selectUnsentData").Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function doCSVFileUploadViaWebService(ByVal csvFileInfo As FileInfo, ByVal TicketNo As String) As String
        Dim outcome As String = ""
        Dim details As String() = Nothing
        Dim tenor, IP As String
        Dim oFileByte As Byte()
        Dim statusCSV As String
        'doCSVFileUploadViaWebService(csvFileInfo, details(13), TicketNo)
        Dim oFile As File
        'Dim oFileStream As FileStream
        Dim oWS As New net.mybankstatement.WebService()
        Dim proxyVal As New WebProxy("http://10.100.21.9:8080", True)
        oWS.Proxy = proxyVal
        Dim strReturn As String = ""
        Try
            details = BLL._selectMetaData(TicketNo)
            statusCSV = BLL._selectCSVStatus(details(13))

            If statusCSV = "True" Then
                'Result returned from the webservice 
                Try


                    'Make sure that the file exists before trying to upload
                    If File.Exists(csvFileInfo.FullName) = False Then
                        Return "CSV File doesnt exist"
                        'Throw New Exception("CSV File doesnt exist")
                    End If


                    'Set the webservice's URL based of the value in the config file
                    'oWS.Url = System.Configuration.ConfigurationSettings.AppSettings("ContractFileServiceURL")

                    'Open the file and break it down into a file stream
                    Using oFileStream As FileStream = File.Open(csvFileInfo.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

                        'declare the byte array of the file
                        oFileByte = New Byte(oFileStream.Length - 1) {}

                        'break the file into bytes and place into the byte object
                        oFileStream.Read(oFileByte, 0, oFileStream.Length)
                    End Using
                    'upload the file to the webservice
                    IP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList(0).ToString()

                    tenor = ((Year(CDate(details(8))) - Year(CDate(details(7)))) * 12) + (Month(CDate(details(8))) - Month(CDate(details(7)))).ToString() & " Month(s)"
                    oWS.SendCSVStatement(TicketNo, details(13), "mybankStatement88*", csvFileInfo.Name, oFileByte, strReturn)

                    If strReturn = "File sent successfully" Then
                        Return "File Delivered successfully"

                    Else
                        outcome = BLL._insertStatementUnsentLog(details(0), details(13), details(14), Nothing, "0")
                        If outcome = "Fail" Then Return "fail" Else Return "File Sent successfully"
                    End If


                Catch ex As System.Net.WebException
                    outcome = BLL._insertStatementUnsentLog(details(0), details(13), details(14), Nothing, "0")
                    If outcome = "Fail" Then Return "fail" Else Return "File Sent successfully"
                    BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
                Catch ex As System.Web.Services.Protocols.SoapException
                    BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
                    outcome = BLL._insertStatementUnsentLog(details(0), details(13), details(14), Nothing, "0")
                    If outcome = "Fail" Then Return "fail" Else Return "File Sent successfully"

                Catch ex As Exception
                    'display errors
                    BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
                    Return ex.Message.ToString()
                Finally
                    'cleanup
                    oWS = Nothing
                    oFile = Nothing
                    'oFileStream.Close()
                End Try
            Else
                Return "File Delivered successfully"
            End If
        Catch ex As Exception
            BLL._insertExceptionLog(details(14), ex.Message.ToString(), Now, details(15), HttpContext.Current.Session("BranchName"))
            Return ex.Message.ToString()
        End Try
    End Function
    Public Shared Sub ResendFile(ByVal TicketNo As Integer, ByVal destinationID As Integer, ByVal SourceID As Integer,
                           ByVal type As String, ByVal status As String)

        If type = "pdf" Then

            Dim oFileInfo As FileInfo = New FileInfo(("D:\mybankStatementRepository\statement\" & TicketNo & ".pdf"))
            Dim sendToService As New SendToService(oFileInfo, TicketNo.ToString)
            If sendToService.SendPDF().ToLower = "file delivered successfully" Then
                ' If doFileUploadViaWebService(oFileInfo, TicketNo).ToLower = "file delivered successfully" Then
                BLL.DeleteUnsentLog(TicketNo, type)
                BLL._UpdateEmailAndSMSAlert(TicketNo.ToString, "1")
            End If
        ElseIf type = "csv" Then
            Dim csvFileInfo As FileInfo = New FileInfo(("D:\mybankStatementRepository\statementCSV\" & TicketNo & ".zip"))
            Dim sendToService As New SendCSVtoService(csvFileInfo, TicketNo.ToString)
            If sendToService.doCSVFileUploadViaWebService().ToLower = "file delivered successfully" Then
                BLL.DeleteUnsentLog(TicketNo, type)
            End If

        ElseIf type = "json" Then
            Dim jsonFileInfo As FileInfo = New FileInfo(("D:\mybankStatementRepository\statementJSON\" & TicketNo & ".zip"))
            Dim sendToService As New SendJsonToService(jsonFileInfo, TicketNo.ToString)
            If sendToService.doJSONFileUploadViaWebService().ToLower = "file delivered successfully" Then
                BLL.DeleteUnsentLog(TicketNo, type)
            End If

        End If

    End Sub
    Public Shared Sub DeleteUnsentLog(ByVal ticketNo As String, ByVal type As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_deleteUnsentLog", ticketNo, type)
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub receiveInbox()
        Dim oWS As New net.mybankstatement.WebService()
        Dim proxyVal As New WebProxy("http://10.0.0.120:80", True)
        oWS.Proxy = proxyVal
        Dim senderid As Integer = 1
        Dim company As String = ""
        Dim csv As String = ""
        Try

            csv = _selectCSVStatusByCompany(senderid)
            Dim tblTicketNo As New DataTable
            tblTicketNo = oWS.checkInbox(senderid)
            If tblTicketNo.Rows.Count > 0 Then
                For i As Int16 = 0 To tblTicketNo.Rows.Count - 1
                    Dim ticket As Integer = CInt(tblTicketNo.Rows(i).Item(0))
                    Dim objFile As Byte() = oWS.receiveStatement(CInt(tblTicketNo.Rows(i).Item(0)), senderid)
                    If objFile IsNot Nothing Then
                        Dim strBaseDirectory As String = String.Empty
                        strBaseDirectory = System.Web.Hosting.HostingEnvironment.MapPath("~\received")
                        If strBaseDirectory.EndsWith("\") = False Then
                            strBaseDirectory += "\"
                        End If

                        If Not IO.Directory.Exists(strBaseDirectory) Then
                            Throw New Exception("Base Folder does not exist and could not be created.")
                        End If
                        If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\received\" & tblTicketNo.Rows(i).Item(0).ToString & "-" & tblTicketNo.Rows(i).Item(5).ToString & ".pdf"))) Then
                            File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~\received\" & tblTicketNo.Rows(i).Item(0).ToString & "-" & tblTicketNo.Rows(i).Item(5).ToString & ".pdf"))
                        End If
                        Using objFileStream As FileStream = IO.File.Open(System.Web.Hosting.HostingEnvironment.MapPath("~\received\" & tblTicketNo.Rows(i).Item(0).ToString & "-" & tblTicketNo.Rows(i).Item(5).ToString & ".pdf"), IO.FileMode.Create, IO.FileAccess.Write)
                            Dim lngLen As Long = objFile.Length
                            objFileStream.Write(objFile, 0, CInt(lngLen))
                        End Using
                        'objFileStream.Flush()
                        'objFileStream.Close()
                        BLL._insertReceived(tblTicketNo.Rows(i).Item(0).ToString, tblTicketNo.Rows(i).Item(1).ToString,
                                        tblTicketNo.Rows(i).Item(2).ToString, tblTicketNo.Rows(i).Item(3).ToString,
                                        tblTicketNo.Rows(i).Item(4).ToString, tblTicketNo.Rows(i).Item(5).ToString,
                                        tblTicketNo.Rows(i).Item(6).ToString, tblTicketNo.Rows(i).Item(7).ToString,
                                        tblTicketNo.Rows(i).Item(8).ToString, tblTicketNo.Rows(i).Item(9).ToString)
                        oWS.Delivery(tblTicketNo.Rows(i).Item(0))
                        BLL._updateLastSystemUpdate(Now)
                    End If
                    If csv = "True" Then
                        Try
                            Dim objFileCSV As Byte() = oWS.receiveStatementCSV(CInt(tblTicketNo.Rows(i).Item(0)), senderid)
                            If objFileCSV IsNot Nothing Then
                                Dim strBaseDirectory1 As String = String.Empty
                                strBaseDirectory1 = System.Web.Hosting.HostingEnvironment.MapPath("~\receivedCSV")
                                If strBaseDirectory1.EndsWith("\") = False Then
                                    strBaseDirectory1 += "\"
                                End If

                                If Not IO.Directory.Exists(strBaseDirectory1) Then
                                    Throw New Exception("Base Folder does not exist and could not be created.")
                                End If
                                If (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~\receivedCSV\" & tblTicketNo.Rows(i).Item(0).ToString & "-" & tblTicketNo.Rows(i).Item(5).ToString & ".zip"))) Then
                                    File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~\receivedCSV\" & tblTicketNo.Rows(i).Item(0).ToString & "-" & tblTicketNo.Rows(i).Item(5).ToString & ".zip"))
                                End If
                                Using objFileStreamCSV As FileStream = IO.File.Open(System.Web.Hosting.HostingEnvironment.MapPath("~\receivedCSV\" & tblTicketNo.Rows(i).Item(0).ToString & "-" & tblTicketNo.Rows(i).Item(5).ToString & ".zip"), IO.FileMode.Create, IO.FileAccess.Write)
                                    Dim lngLen1 As Long = objFileCSV.Length
                                    objFileStreamCSV.Write(objFileCSV, 0, CInt(lngLen1))
                                End Using
                                'objFileStreamCSV.Flush()
                                'objFileStreamCSV.Close()
                            End If
                        Catch ex As Exception
                        End Try
                    End If

                    ' HttpContext.Current.Session("Mode") = "| Online |"
                Next
            End If
        Catch ex As System.Net.WebException
            BLL._insertExceptionLog(senderid, ex.Message.ToString(), Now, company, HttpContext.Current.Session("BranchName"))

        Catch ex As System.Web.Services.Protocols.SoapException
            BLL._insertExceptionLog(senderid, ex.Message.ToString(), Now, company, HttpContext.Current.Session("BranchName"))
        Catch exc As System.UnauthorizedAccessException
            BLL._insertExceptionLog(senderid, exc.Message.ToString(), Now, company, HttpContext.Current.Session("BranchName"))
        Catch exc As SqlException
            BLL._insertExceptionLog(senderid, exc.Message.ToString(), Now, company, HttpContext.Current.Session("BranchName"))
        Catch exc As Exception
            'Throw New Exception("Exception Occurred. Error: ")
            '
            exc.Message.ToString()
        Finally

        End Try
    End Sub

    Public Shared Function _selectCompanyName() As DataTable
        Dim List As DataTable = Nothing
        Try
            List = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectCompany").Tables(0)
            Return List
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Sub _updateTestCharge(ByVal ticketNo As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updateTestCharge",
                                  ticketNo)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub _insertExceptionLog(ByVal spookeid As String, ByVal Exception As String, ByVal Timestamp As DateTime,
                                          ByVal SpokeName As String, ByVal branchName As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertExceptionLog",
                                  spookeid, Exception, Timestamp, SpokeName, branchName)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub _deleteExceptionLog()
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_deleteExceptionLog")
    End Sub
    Public Shared Function _selectExceptionLog() As DataTable
        Dim List As DataTable = Nothing
        Try
            List = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectExceptionLog").Tables(0)
            Return List
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectvalidprice() As String
        Dim value As String
        Try
            value = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectvalidprice")
            Return value
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function __Selectpricing() As DataTable
        Try
            Dim result As DataSet
            result = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "__Selectpricing")
            Return result.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function __SelectCompanyInfo() As DataTable
        Try
            Dim result As DataSet
            result = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "__CompanyProfile")
            Return result.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub __UpdateMakerChecker(ByVal val As String)
        Try

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "__UpdateMakerChecker", val)

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function __SelectPricingMode() As String
        Try
            Return SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "__SelectPricingMode")
        Catch ex As Exception
            Return "0"
        End Try
    End Function
    Public Shared Function __Insertpricing(Price As String, Userid As String, flat As String, staff As String, mgt As String, vendor As String) As String
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "__Insertpricing", Price, Userid, flat, staff, mgt, vendor)
            Return "1"
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectApprovalbyRequestID(ByVal value As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectApprovalbyRequestID", value)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectApprovalbyFilter(ByVal userID As String, ByVal value As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectAprrovalbyFilter", userID, value)
            Return ds.Tables(0)
        Catch exe As InvalidOperationException
            HttpContext.Current.Response.Redirect("Login.aspx")
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectApproval(ByVal userID As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectApproval", userID)
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Sub _insertTicketMailAlert(ByVal TicketID As String, ByVal Name As String, ByVal Destination As String,
                                  ByVal DestinationID As String, ByVal Source As String, ByVal SourceID As String, ByVal Receiver As String,
                                  ByVal Subject As String, ByVal Body As String, ByVal Status As String, type As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertTicketMailAlert",
                                      TicketID, Name, Destination, DestinationID, Source, SourceID, Receiver, Subject, Body, Status, type)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub _insertSMSAlert(ByVal TicketID As String, ByVal Name As String, ByVal Destination As String,
                                  ByVal DestinationID As String, ByVal Source As String, ByVal SourceID As String, ByVal Nuban As String,
                                  ByVal Subject As String, ByVal Body As String, ByVal Status As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertSMSAlert",
                                      TicketID, Name, Destination, DestinationID, Source, SourceID, Nuban, Subject, Body, Status)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub _insertApprovalMailAlert(ByVal ApproverEmail As String, ByVal ApproverName As String, ByVal SenderName As String,
                                   ByVal SenderEmail As String, ByVal TicketID As String, ByVal Mode As String, ByVal subject As String, ByVal body As String, ByVal status As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_InsertApprovalMailAlert",
                                      ApproverEmail, ApproverName, SenderName, SenderEmail, TicketID, Mode, subject, body, status)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub _insertScheduleMails(ByVal nuban As String, ByVal name As String, ByVal type As String, ByVal email As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertScheduleMails",
                                      nuban, name, type, email)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function _selectUnsentTicketMailAlert() As DataTable
        Dim List As DataTable = Nothing
        Try
            List = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUnsentTicketMailAlert").Tables(0)
            Return List
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectUnsentSMSAlert() As DataTable
        Dim List As DataTable = Nothing
        Try
            List = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUnsentSMSAlert").Tables(0)
            Return List
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectScheduleMails() As DataTable
        Dim List As DataTable = Nothing
        Try
            List = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectScheduleMails").Tables(0)
            Return List
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectUnsentApprovalMailAlert() As DataTable
        Dim List As DataTable = Nothing
        Try
            List = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUnsentApprovalMailAlert").Tables(0)
            Return List
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub _deleteSentTicketMailAlert(ticketid As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                  "_deleteSentTicketMailAlert", ticketid)
    End Sub
    Public Shared Sub _deleteSentSMSAlert(ticketid As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                  "_deleteSentSMSAlert", ticketid)
    End Sub
    Public Shared Sub _deleteScheduleMails(ByVal nuban As String, ByVal stype As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                  "_deleteScheduleMails", nuban, stype)
    End Sub
    Public Shared Sub _deleteSentApprovalMailAlert(ticketid As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                  "_deleteSentApprovalMailAlert", ticketid)
    End Sub

    Public Shared Sub _deleteDeclineAcknowledgement(ticketid As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                  "_deleteDeclineAcknowledgement", ticketid)
    End Sub
    Public Shared Function _selectCheckerstatus() As String
        Try
            Dim status As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectCheckerstatus")
            Return status
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function _selectApprovalDetails(ByVal Branchname As String) As DataTable
        Try
            Dim dt As DataTable
            dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectApprovalDetails",
                                      Branchname).Tables(0)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _ApprovalInfo(ByVal ticket As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_ApprovalInfo", ticket)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _SelectTicketStatus(ByVal ID As String) As String
        Try
            Dim status As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_SelectTicketStatus", ID)
            Return status
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Sub _SendApproveAcknowledgement(ByVal ticket As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_SendApproveAcknowledgement", ticket)
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub _SendDeclineAcknowledgement(ByVal ticket As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_SendDeclineAcknowledgement", ticket)
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Function _SelectTicketStatusFromTicketID(ByVal TicketID As String) As String
        Try
            Dim status As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_SelectTicketStatusFromTicketID", TicketID)
            Return status
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function _insertRealUser(ByVal user As String, ByVal pwd As String) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertRealUser", user, pwd)
            Return dID
        Catch ex As Exception
            Return 0


        End Try
    End Function
    Public Shared Function _insertUserAD(ByVal FirstName As String, ByVal Surname As String,
    ByVal UserName As String, ByVal Role As String, ByVal Type As Integer, ByVal branchName As String, ByVal branchID As String, ByVal supervisor As String, ByVal country As String, ByVal solID As String) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertUserAD", FirstName, Surname, UserName, Role, Type, branchName, branchID, supervisor, country, solID)
            Return dID
        Catch ex As Exception
            Return 0

        End Try
    End Function
    Public Shared Function _selectOutboxAdmin(ByVal byBranch As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectOutboxAdmin", byBranch)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectReport(ByVal startDate As String, ByVal endDate As String, ByVal byBranch As String, ByVal filter As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectReport", startDate, endDate, byBranch, filter)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectReportAdmin(ByVal startDate As String, ByVal endDate As String, ByVal byBranch As String, ByVal filter As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectReportAdmin", startDate, endDate, byBranch, filter)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectReportExport(ByVal startDate As String, ByVal endDate As String, ByVal byBranch As String, ByVal filter As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectReportExport", startDate, endDate, byBranch, filter)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectReportAdminExport(ByVal startDate As String, ByVal endDate As String, ByVal byBranch As String, ByVal filter As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectReportAdminExport", startDate, endDate, byBranch, filter)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectDistinctBranchName() As DataTable
        Dim dt As DataTable = Nothing
        Try
            dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectDistinctBranchName").Tables(0)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectMetaDataEmail(ByVal ID As String) As String()
        ' Dim dr As SqlDataReader = Nothing
        Dim Details(0 To 2) As String
        Try
            Using dr As SqlDataReader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_selectMetaDataEmail", ID)
                While dr.Read
                    If dr.IsDBNull(dr.GetOrdinal("email")) = False Then
                        Details(0) = CStr(IIf(IsDBNull(dr("name")), "", dr("name")))
                        Details(1) = CStr(IIf(IsDBNull(dr("email")), "", dr("email")))
                        Details(2) = CStr(IIf(IsDBNull(dr("Nuban")), "", dr("Nuban")))
                    Else
                        Details(0) = ""
                    End If
                End While
                'dr.Close()
            End Using
            Return Details
        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Public Shared Function _insertNewUser(ByVal FirstName As String, ByVal Surname As String,
  ByVal UserName As String, ByVal Role As String, ByVal branch As String, ByVal Admin As Int16, ByVal phone As String, ByVal active As Int16) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertNewUser", FirstName, Surname, UserName, Role, branch, Admin, phone, active)
            Return dID
        Catch ex As Exception
            Return 0

        End Try
    End Function
    Public Shared Function _insertNewUserAdmin(ByVal FirstName As String, ByVal Surname As String,
ByVal UserName As String, ByVal Role As String, ByVal branch As String, ByVal Admin As Int16, ByVal phone As String,
ByVal active As Int16, ByVal branchID As String, ByVal Supervisor As String, salt As String, pass As String, solID As String) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertNewUserAdmin", FirstName, Surname, UserName, Role, branch, Admin, phone, active, branchID, Supervisor, salt, pass, solID)
            Return dID
        Catch ex As Exception
            Return 0

        End Try
    End Function
    Public Shared Function _insertADROle(ByVal id As String, ByVal ADTitle As String, ByVal SystemRole As String, ByVal active As Boolean) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertADROle", id, ADTitle, SystemRole, active)
            Return dID
        Catch ex As Exception
            Return 0

        End Try
    End Function

    Public Shared Function _selectSystemRoleByAD(ByVal ADRole As String) As String
        Try
            Return SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectSystemRoleByAD", ADRole)
        Catch
            Return Nothing
        End Try

    End Function
    '*********************new Sunction for monthly sending of statement. the SP yet to be created
    Public Shared Function _getGStatusDaily() As DataTable
        Dim value As DataSet
        Try
            value = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_getGStatusDaily")
            Return value.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _getGStatusMonthly() As DataTable
        Dim value As DataSet
        Try
            value = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_getGStatusMonthly")
            Return value.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _getSStatusMonthly() As DataTable
        Dim value As DataSet
        Try
            value = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_getSStatusMonthly")
            Return value.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _getGStatusWeekly() As DataTable
        Dim value As DataSet
        Try
            value = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_getGStatusWeekly")
            Return value.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _getGStatusQuarterly() As DataTable
        Dim value As DataSet
        Try
            value = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_getGStatusQuarterly")
            Return value.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _getGStatusForthnightly() As DataTable
        Dim value As DataSet
        Try
            value = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_getGStatusForthnightly")
            Return value.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    '**************Monitoring
    'Public Shared Function _selectPeriodYear() As Object()
    '    Dim dreader As DataSet = Nothing
    '    Try
    '        dreader = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectPeriodYear")
    '        Return {dreader.Tables(0), dreader.Tables(0).CreateDataReader}
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Shared Function _selectPeriodMonth() As Object()
    '    Dim dreader As DataSet = Nothing
    '    Try
    '        dreader = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectPeriodMonth")
    '        Return {dreader.Tables(0), dreader.Tables(0).CreateDataReader}
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function
    Public Shared Function _selectPeriodYear() As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectPeriodYear")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectPeriodMonth(ByVal year As String) As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectPeriodMonth", year)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#Region "Status"
    Enum Statuses
        Notbackedup = 0
        Errorbackingup = -1
        Backedup = 1
        Generated = 2
        Errorgenerating = -2
        Stamped = 3
        Notstamped = -3
        Sent = 4
        Notsent = -4
    End Enum
    Enum Tasks
        Backup = 1
        Generation = 2
        Sending = 3
    End Enum

    Enum Trigger
        Drop = 0
        Begin = 1
    End Enum
    Enum Frequency
        Quarterly = 1
        Monthly = 2
        Weekly = 3
        Daily = 4
        ForthNigthly = 5
    End Enum
#End Region





    Public Shared Function _selecttrigger() As DataTable

        Try
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selecttrigger")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectstatuscount(ByVal status As String) As String()
        Try
            ' Dim dreader As IDataReader
            Dim val(0 To 3) As String
            Using dreader As IDataReader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectstatuscount", status)
                While dreader.Read
                    val(0) = dreader.Item("ncount")
                    val(1) = dreader.Item("Percentage")
                    val(2) = dreader.Item("Remain")
                    val(3) = dreader.Item("Total")
                End While
            End Using
            Return val
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Public Shared Function _changetrigger(ByVal element As Integer, ByVal triggervalue As Integer) As Integer

        Try
            Dim i As Integer
            i = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_changetrigger", element, triggervalue)
            Return i
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectrecords(ByVal month As String, ByVal year As String, ByVal status As Integer, ByVal tasks As Integer) As SqlDataReader
        Dim dreader As IDataReader = Nothing
        Try
            dreader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectrecords", month, year, status, tasks)
            Return dreader
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectMonthlyNuban() As DataTable

        Try
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectMonthlyNuban")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectMonthlyNubanLate() As DataTable

        Try
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectMonthlyNubanLate")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub _insertScheduleM(ByVal nuban As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertScheduleM", nuban)
    End Sub
    Public Shared Sub _insertLastScheduleM(ByVal nuban As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertLastScheduleM", nuban)
    End Sub
    Public Shared Sub _deleteScheduleM(ByVal nuban As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_deleteScheduleM", nuban)
    End Sub
    Public Shared Sub _deleteScheduleMLate(ByVal nuban As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_deleteScheduleMLate", nuban)
    End Sub
    Public Shared Sub _deleteAllScheduleM()
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_deleteAllScheduleM")
    End Sub
    Public Shared Sub _insertScheduleStatus(ByVal status As String, ByVal dateIns As Date)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertScheduleStatus", status, dateIns)
    End Sub
    Public Shared Function _checkScheduleStatus() As String

        Try
            Dim str As String
            str = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_checkScheduleStatus", Date.Today)
            Return str
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _checkLastScheduleStatus() As String

        Try
            Dim str As String
            str = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_checkLastScheduleStatus", Date.Today)
            Return str
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Sub _insertGenerateSentReport(ByVal date1 As Date, ByVal total As Integer, ByVal generated As Integer, ByVal sent As Integer)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertGenerateSentReport", date1, total, generated, sent)
        Catch
        End Try

    End Sub

    Public Shared Sub _UpdatePws(ByVal ticketNo As String, ByVal pws As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_UpdatePws", ticketNo, pws)
        Catch
        End Try
    End Sub
    Public Shared Sub _UpdateEmailAndSMSAlert(ByVal ticketNo As String, ByVal status As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "UpdateEmailAndSMSAlert", ticketNo, status)
        Catch
        End Try
    End Sub
    Public Shared Function _SelectPws(ByVal ticketNo As String) As String
        Try
            Return SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_SelectPws", ticketNo)
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function _SelectMailOption(ByVal ticketNo As String) As String
        Try
            Return SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_SelectMailOption", ticketNo)
        Catch
            Return "0_0"
        End Try

    End Function


    Public Shared Sub _UpdateMailOption(ByVal ticketNo As String, ByVal status As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_UpdateMailOption", ticketNo, status)
        Catch
        End Try
    End Sub
    Public Shared Function _selectTransactionSummary(ByVal sDate As Date, eDate As Date, Nuban As String) As DataTable
        Try
            Dim dt As DataTable = Nothing
            dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectTransactionSummary", sDate, eDate, Nuban).Tables(0)
            Return dt
        Catch
            Return Nothing
        End Try
    End Function
    '*************corporate
    Public Shared Function GetRequestedActivity(ByVal ID As String) As DataTable
        Try
            Dim dt As DataTable = Nothing
            dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionStringCorporate").ToString, "_selectStatementRequest", ID).Tables(0)
            Return dt
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function _GetApprovedCorporateActivity(ByVal ID As String) As DataTable
        Try
            Dim dt As DataTable = Nothing
            dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionStringCorporate").ToString, "_GetApprovedCorporateActivity", ID).Tables(0)
            Return dt
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function _CheckValidEmail(ByVal email As String, ByVal acctNo As String) As String
        Try
            Dim str As String = Nothing
            str = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_CheckValidEmail", email, acctNo)
            Return str
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectSolIDFromBranchID(ByVal email As String) As String
        Try
            Dim str As String = Nothing
            str = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectSolIDFromBranchID", email)
            Return str
        Catch
            Return Nothing
        End Try
    End Function

    Public Shared Sub _insertTicketMailAlertCorporate(ByVal AccountNo As String, ByVal Type As String, ByVal Receiver As String,
                                 ByVal Subject As String, ByVal Body As String, ByVal Status As String, ByVal ticket As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertTicketMailAlertCorporate",
                                      AccountNo, Type, Receiver, Subject, Body, Status, ticket)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function _insertActivityCorporate(ByVal ID As String, ByVal type As String, ByVal nuban As String, ByVal username As String,
                                          ByVal status As String, ByVal period As String, ByVal destination As String, ByVal timestamp As String, ByVal role As String,
                                          ByVal sDate As String, ByVal eDate As String, Optional page As String = Nothing, Optional basic As String = Nothing, Optional additional As String = Nothing,
                                          Optional total As String = Nothing, Optional applicant As String = Nothing, Optional vat As String = Nothing, Optional currency As String = Nothing, Optional randPsw As String = Nothing, Optional mailOption As String = Nothing) As String

        Dim dID As Integer = Nothing
        Try
            dID = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                "_insertActivityCorporate", ID, type, nuban, username, status, period, destination, timestamp, role, sDate, eDate, page, basic, additional, total, applicant, vat, currency, randPsw, mailOption)
            Return dID
        Catch ex As Exception
            Return 0

        End Try
    End Function

    Public Shared Sub _updateActivityCorporate(ByVal ID As String, ByVal status As String, ByVal comment As String, Optional email As String = Nothing)
        Try

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionStringCorporate").ToString, "_updateActivityCorporate", ID, status, comment, email)

        Catch

        End Try
    End Sub
    Public Shared Sub _updateScheduleCorporate(ByVal ID As String, ByVal status As String, ByVal comment As String, Optional email As String = Nothing)
        Try

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionStringCorporate").ToString, "_updateScheduleCorporate", ID, status, comment, email)

        Catch

        End Try
    End Sub

    Public Shared Function _GetFormat(ByVal formatID As String) As String

        Try
            Dim res As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_GetOtherFormat", formatID)


            Return res
        Catch ex As Exception
            Return "No"

        End Try
    End Function

    Public Shared Sub GetInitiatedActivity()

        Try
            Dim oWS As New net.mybankstatement.WebService()
            Dim proxyVal As New WebProxy("http://10.0.0.120:80", True)
            oWS.Proxy = proxyVal
            Dim senderid As Integer = 1
            'Dim company As String = ""
            'Using dr As SqlDataReader = BLL._selectCompany()
            '    While dr.Read()
            '        If Not IsDBNull(dr.Item("Domain")) Then
            '            senderid = dr.Item("SenderID")
            '            company = dr.Item("CompanyName")
            '        End If
            '    End While
            'End Using

            Dim dtRequests As DataTable = oWS.SelectInitiatedRequest(senderid, "PreReg123*8!")
            If dtRequests.Rows.Count > 0 Then
                For i As Int16 = 0 To dtRequests.Rows.Count - 1
                    Try
                        If dtRequests.Rows(i).Item(1).ToString().ToLower = "ticket" Then
                            'update the hub with status 'Confirm'
                            'You will do this by modifying the updateStatus service so that the passcode et all is not erased.
                            'put a condition to only modify status if passcode is null
                            If oWS.updateRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "TicketSent", Nothing, Nothing, Nothing, "PreReg123*8!") = True Then
                                BLL.updateInitiatedRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Ticket")
                                Dim name As String = oWS._selectAccountNameByRequestID(dtRequests.Rows(i).Item(0).ToString(), "PreReg123*8!")
                                _updateRequestAccountName(dtRequests.Rows(i).Item(0).ToString(), name)
                            End If

                        ElseIf dtRequests.Rows(i).Item(1).ToString().ToLower = "sent" Then
                            If oWS.updateRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Success", Nothing, Nothing, Nothing, "PreReg123*8!") = True Then
                                BLL.updateInitiatedRequestStatus(dtRequests.Rows(i).Item(0).ToString(), dtRequests.Rows(i).Item(1).ToString())
                            End If
                        ElseIf dtRequests.Rows(i).Item(1).ToString().ToLower = "insfund" Then
                            If oWS.updateRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "InsFundSent", Nothing, Nothing, Nothing, "PreReg123*8!") = True Then
                                BLL.updateInitiatedRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Ins. Fund")
                            End If
                        ElseIf dtRequests.Rows(i).Item(1).ToString().ToLower = "billfailure" Then
                            If oWS.updateRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "BillFailureSent", Nothing, Nothing, Nothing, "PreReg123*8!") = True Then
                                BLL.updateInitiatedRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Billing Failure")
                            End If
                        ElseIf dtRequests.Rows(i).Item(1).ToString().ToLower = "sendfailure" Then
                            '  If oWS.updateRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "BillFailureSent", Nothing, Nothing, Nothing, "PreReg123*8!") = True Then
                            BLL.updateInitiatedRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Delivery Failure")
                            ' End If
                        ElseIf dtRequests.Rows(i).Item(1).ToString().ToLower = "fail" Or dtRequests.Rows(i).Item(1).ToString().ToLower = "error" Then
                            If oWS.updateRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Fail", Nothing, Nothing, Nothing, "PreReg123*8!") = True Then
                                BLL.updateInitiatedRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Fail")
                            End If
                        ElseIf dtRequests.Rows(i).Item(1).ToString().ToLower = "invalid" Then
                            If oWS.updateRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Fail", Nothing, Nothing, Nothing, "PreReg123*8!") = True Then
                                BLL.updateInitiatedRequestStatus(dtRequests.Rows(i).Item(0).ToString(), "Invalid")
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                Next

            End If
        Catch ex As Exception
            Utility.LogException("Calling GetInitiatedActivity error " & " error message " & ex.Message.ToString() & " " & Now.ToString)

        End Try



    End Sub
    'Sample dataTable

    Public Shared Sub GetRequestedActivity()
        'Dim objFileStream As IO.FileStream = Nothing
        'Dim objFileStreamCSV As IO.FileStream = Nothing
        Dim oWS As New net.mybankstatement.WebService()
        Dim typeException As String = ""
        Dim senderid As Integer = 1
        Dim company As String = ""
        Dim subj As String = ""
        Dim body As String = ""
        Dim tblTicketNo As New DataTable
        Try
            'Using dr As SqlDataReader = BLL._selectCompany()
            '    While dr.Read()
            '        If Not IsDBNull(dr.Item("Domain")) Then
            '            senderid = dr.Item("SenderID")
            '            company = dr.Item("CompanyName")
            '        End If
            '    End While
            'End Using
            'dr.Close()
            ' csv = _selectCSVStatusByCompany(senderid)

            'This will be a web service

            ' tblTicketNo = GetTableInbox()
            Dim proxyVal As New WebProxy("http://10.0.0.120:80", True)
            oWS.Proxy = proxyVal
            tblTicketNo = oWS.GetRequestedActivity(senderid, "PreReg123*8!")

            If tblTicketNo.Rows.Count > 0 Then
                For i As Int16 = 0 To tblTicketNo.Rows.Count - 1
                    Try
                        ' typeException = tblTicketNo.Rows(i).Item(1).ToString
                        'If tblTicketNo.Rows(i).Item(10).ToString.ToLower = "pending" Then

                        '    Dim validAccount As String = DAL_API.ValidateAccountWithPhone(tblTicketNo.Rows(i).Item(1).ToString, tblTicketNo.Rows(i).Item("telephone").ToString, Utility.getCountryCodeFromCountry(tblTicketNo.Rows(i).Item("country").ToString))
                        '    If validAccount = "invalid" Then
                        '        'Email senderid should be wallz & queen
                        '        subj = "mybankStatement Ticket"
                        '        body = "Hi " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ",</br></br> Ticket could not be generated for your request with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + " </b> because of an invalid account number <b> " + tblTicketNo.Rows(i).Item(1).ToString + "</b> input.</br></br>Regards"
                        '        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Error", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)

                        '        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Invalid", Nothing, Nothing, Nothing, "PreReg123*8!")
                        '    ElseIf validAccount = "phoneInvalid" Then
                        '        'Email senderid should be wallz & queen
                        '        subj = "mybankStatement Ticket"
                        '        body = "Hi " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>Ticket could not be generated for your request with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> because of a mismatched telephone number <b> " + tblTicketNo.Rows(i).Item("telephone").ToString + "</b>. </br></br>Regards"
                        '        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Error", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)

                        '        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Invalid", Nothing, Nothing, Nothing, "PreReg123*8!")

                        '    ElseIf validAccount = "valid" Then
                        '        'Change this
                        '        Dim dtAccount As DataTable = DAL_API.AccountDetails(tblTicketNo.Rows(i).Item(1).ToString)
                        '        ' Dim dtAccount As DataTable = BLL._selectAccountPrint("4077336701")
                        '        'This should be a service
                        '        Dim applicantNames As String() = oWS.selectRequestApplicantFromSpoke(tblTicketNo.Rows(i).Item(0).ToString, "PreReg123*8!").Split("|")
                        '        'this is sample data
                        '        ' Dim applicantNames As String() = {"Ola", "Baba", "Egbon"}
                        '        'this should be service
                        '        Dim passcode As String = Utility.Get4RadomPassword()
                        '        Dim holderEmail As String = Nothing
                        '        If dtAccount.Rows(0).Item("Email") Is Nothing Then
                        '            holderEmail = "N/A"
                        '        ElseIf Utility.ValidateEmail(dtAccount.Rows(0).Item("Email").ToString) Then
                        '            holderEmail = dtAccount.Rows(0).Item("Email").ToString
                        '        Else
                        '            holderEmail = "N/A"
                        '        End If
                        '        'Change this
                        '        Dim ticketNo As String = Option2BLL.insertStatementRequestToSpoke(tblTicketNo.Rows(i).Item(0).ToString, tblTicketNo.Rows(i).Item(1).ToString,
                        '                                                 tblTicketNo.Rows(i).Item(3).ToString, tblTicketNo.Rows(i).Item(4).ToString,
                        '                                                 tblTicketNo.Rows(i).Item(5).ToString, tblTicketNo.Rows(i).Item(6).ToString,
                        '                                                  tblTicketNo.Rows(i).Item(7).ToString, dtAccount.Rows(0).Item("CUS_SHO_NAME").ToString,
                        '                                                  passcode, dtAccount.Rows(0).Item("AccountGroup").ToString, dtAccount.Rows(0).Item("ADD_LINE1").ToString,
                        '                                                 holderEmail, dtAccount.Rows(0).Item("Currency_Code").ToString,
                        '                                                  dtAccount.Rows(0).Item("CustomerStatusDeecp").ToString, dtAccount.Rows(0).Item("CLE_BAL").ToString,
                        '                                                  dtAccount.Rows(0).Item("CRNT_BAL").ToString, dtAccount.Rows(0).Item("BVN"), applicantNames)

                        '        'Dim ticketNo As String = Option2BLL.insertStatementRequestToSpoke(tblTicketNo.Rows(i).Item(0).ToString, tblTicketNo.Rows(i).Item(1).ToString,
                        '        '                                        tblTicketNo.Rows(i).Item(3).ToString, tblTicketNo.Rows(i).Item(4).ToString,
                        '        '                                        tblTicketNo.Rows(i).Item(5).ToString, tblTicketNo.Rows(i).Item(6).ToString,
                        '        '                                         tblTicketNo.Rows(i).Item(7).ToString, dtAccount.Rows(0).Item("NAME").ToString,
                        '        '                                         passcode, dtAccount.Rows(0).Item("TYPE").ToString, dtAccount.Rows(0).Item("ADDRESS").ToString,
                        '        '                                        holderEmail, dtAccount.Rows(0).Item(11).ToString,
                        '        '                                          dtAccount.Rows(0).Item("CATEGORY").ToString, dtAccount.Rows(0).Item("CLR_BAL").ToString,
                        '        '                                         dtAccount.Rows(0).Item("AVL_BAL").ToString, "1234567890", applicantNames)

                        '        '  Utility.SendSMS(tblTicketNo.Rows(i).Item(1).ToString, SMSbody)
                        '        'update the hub by updating _tblRequest with password, status on requestID and ticketNO
                        '        Dim dtActivity As DataTable = Option2BLL.SelectActivityFromSpokeBYRequestID(tblTicketNo.Rows(i).Item(0).ToString)
                        '        'This is a test transaction details. change to origibnal
                        '        'Pls change this
                        '        ' Dim dtTranaction As DataTable = BLL._selectTransactionsTest(CDate("07 Jan 2014"), CDate("07 Mar 2014"), "4077336701")
                        '        Dim dttranaction As DataTable = DAL_API.TransactionDetails(tblTicketNo.Rows(i).Item(1).ToString, CDate(tblTicketNo.Rows(i).Item(4).ToString), _
                        '                                                                    CDate(tblTicketNo.Rows(i).Item(5).ToString))

                        '        '  Dim applicantNames As String() = Option2BLL.selectRequestApplicantFromSpoke(tblTicketNo.Rows(i).Item(0).ToString).Split("|")

                        '        '  Dim applicantNames As String() = {"Ola"}

                        '        Dim pdf As New GeneratePDFService(dtTranaction, dtActivity, tblTicketNo.Rows(i).Item(4).ToString, _
                        '                                           tblTicketNo.Rows(i).Item(5).ToString, _
                        '                                           "Request", tblTicketNo.Rows(i).Item(6).ToString, _
                        '                                            dtActivity.Rows(0).Item(7).ToString, dtActivity.Rows(0).Item(24).ToString, _
                        '                                            dtActivity.Rows(0).Item(0).ToString, applicantNames, _
                        '                                             dtActivity.Rows(0).Item(25).ToString, Utility.getCountryCodeFromCountry(tblTicketNo.Rows(i).Item(11).ToString))

                        '        Dim res As String() = pdf.GeneratePDF()
                        '        Dim PageCount As Integer = 0
                        '        Dim vat As Double = 0.0
                        '        Dim Basic As Double = 0
                        '        Dim Additional As Double = 0
                        '        Dim Total As Double = 0
                        '        Dim Unitprice As Double

                        '        Dim noOfPages As Int16 = 0
                        '        Dim validPrice As String = ""
                        '        'If HttpContext.Current.Session("Country") Is Nothing Then
                        '        validPrice = BLL._selectvalidprice()
                        '        'Else
                        '        ' validPrice = BLL._selectvalidprice(HttpContext.Current.Session("Country"))
                        '        ' End If

                        '        Unitprice = CDbl(validPrice.Split("|")(0))
                        '        noOfPages = CInt(res(2))
                        '        Basic = noOfPages * Unitprice
                        '        'Additional = details(1)
                        '        Additional = CDbl(_selectDestinationName(tblTicketNo.Rows(i).Item("destinationID"))(1))
                        '        ' vat = (noOfPages * Unitprice * 0.05)
                        '        Total = (Basic + Additional) * 1.05
                        '        vat = (Basic + Additional) * 0.05
                        '        Option2BLL.updateActivityByRequestID(tblTicketNo.Rows(i).Item(0).ToString, CDbl(res(1)), CDbl(res(0)), noOfPages, Basic, Additional, Total, vat)
                        '        Dim details As String() = BLL._selectDestinationName(tblTicketNo.Rows(i).Item("destinationID").ToString)
                        '        If details(2) = "True" Then
                        '            '  Try


                        '            Dim csv As New GenerateCSV(dtTranaction, dtActivity, tblTicketNo.Rows(i).Item(4).ToString, tblTicketNo.Rows(i).Item(5).ToString, _
                        '                                           tblTicketNo.Rows(i).Item("destinationID").ToString, tblTicketNo.Rows(i).Item(6).ToString, _
                        '                                            dtActivity.Rows(0).Item(7).ToString, dtActivity.Rows(0).Item(24).ToString, _
                        '                                            dtActivity.Rows(0).Item(0).ToString, "Request", _
                        '                                            passcode, Utility.getCountryCodeFromCountry(tblTicketNo.Rows(i).Item(11).ToString))
                        '            csv.generateAndSaveZipedPasswordedCSV()
                        '            'Catch ex As Exception
                        '            '    Utility.LogException("DestinationID " & tblTicketNo.Rows(i).Item("destinationID").ToString & " RequestID " & tblTicketNo.Rows(i).Item(0).ToString & " " & "***** Error generating CSV format  *** " & ex.Message())

                        '            'End Try
                        '        End If

                        '        'Json Format
                        '        If BLL._GetFormat("1", ticketNo) = "1" Then
                        '            '  Try
                        '            Dim json As New GenerateJson(dtTranaction, dtActivity, tblTicketNo.Rows(i).Item(4).ToString, tblTicketNo.Rows(i).Item(5).ToString, _
                        '                                       tblTicketNo.Rows(i).Item("destinationID").ToString, dtActivity.Rows(0).Item(7).ToString, _
                        '                                         dtActivity.Rows(0).Item(0).ToString, "Request", applicantNames, _
                        '                                        passcode, Utility.getCountryCodeFromCountry(tblTicketNo.Rows(i).Item(11).ToString))

                        '            json.generateAndSaveZipedPasswordedJSON()

                        '            'Catch ex As Exception
                        '            '    Utility.LogException("DestinationID " & tblTicketNo.Rows(i).Item("destinationID").ToString & " RequestID " & tblTicketNo.Rows(i).Item(0).ToString & " " & "***** Error generating JSON format  *** " & ex.Message())
                        '            'End Try
                        '        End If
                        '        Dim ticket As New GenerateTicket(ticketNo, dtActivity.Rows(0).Item("Name").ToString, _
                        '                           BLL._selectDestinationName(tblTicketNo.Rows(i).Item(3).ToString)(0), senderid, _
                        '                                    "Request", passcode)


                        '        Dim ticketResult As String = ticket.getTicket()
                        '        If ticketResult = "True" Then
                        '            If Utility.ValidateEmail(dtActivity.Rows(0).Item("email").ToString) = True Then
                        '                subj = "mybankStatement Ticket"
                        '                body = "Hello " & dtActivity.Rows(0).Item("Name").ToString & ", <br /><br />" & details(0) & " has requested for your bank statement for the period of " & CDate(tblTicketNo.Rows(i).Item(4).ToString).ToString("dd-MMM-yyyy") & " to " & CDate(tblTicketNo.Rows(i).Item(5).ToString).ToString("dd-MMM-yyyy") & ".<br /><br />To authorize their access and be charged for this service, hand over the attached ticket. <br /><br />Ticket No " & ticketNo.ToString & "-1<br /><br /> Password : " & passcode & "<br /><br /> No of Pages : <b>" + noOfPages.ToString() & "</b><br /><br />Total Charges : <b>" + CDbl(Total).ToString("#,##0") + " NGN</b><br /><br /> Regards"
                        '                BLL._insertTicketMailAlert(ticketNo, dtActivity.Rows(0).Item("Name").ToString, details(0), tblTicketNo.Rows(i).Item("destinationID"), "Diamond Bank Plc.", "1", dtAccount.Rows(0).Item("email").ToString, subj, body, "1", "request")

                        '            End If
                        '            BLL._insertSMSAlert(ticketNo, dtActivity.Rows(0).Item("Name").ToString, details(0), tblTicketNo.Rows(i).Item("destinationID"), "Diamond Bank Plc.", "1", tblTicketNo.Rows(i).Item("telephone").ToString.Replace("+234", "0"), "E-Statement", "Your E-statement ticket details requested by " + details(0) + ". TicketNo:" + ticketNo + "-1 Password:" + passcode + " Fee:" + CDbl(Total).ToString("#,##0") + " NGN", "1")
                        '            oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Ticket", ticketNo, passcode, dtActivity.Rows(0).Item("Name").ToString, "PreReg123*8!")

                        '            subj = "mybankStatement Ticket Sent"
                        '            body = "Hi " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>Ticket for your request with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> has been generated and sent to <b> " + dtActivity.Rows(0).Item("Name").ToString + "</b>. </br></br>Regards"
                        '            BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "ticket sent", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                        '            BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, tblTicketNo.Rows(i).Item(1).ToString, "Request", ticketNo, "Statement ticket sent", Now)

                        '        Else
                        '            oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Error", ticketNo, passcode, dtActivity.Rows(0).Item("Name").ToString, "PreReg123*8!")

                        '            subj = "mybankStatement Ticket Sent"
                        '            body = "Hi " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>Ticket for your request with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> for <b> " + dtActivity.Rows(0).Item("Name").ToString + "</b>. could not be generated due to system failure. Contact wallz & Queen for support   </br></br>Regards"
                        '            BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Ticket not gnerated", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                        '            BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, tblTicketNo.Rows(i).Item(1).ToString, "Request", ticketNo, "Ticket generation failed", Now)

                        '        End If
                        '        'Nibss passcode has been replaced with account name

                        '    ElseIf validAccount = "noservice" Then
                        '        subj = "mybankStatement Ticket (Issuer Inoperative)"
                        '        body = "Hi </br></br> Could not generate ticket. Account enquiry service is down at Diamond Bank. Contact wallz and Queen for support</br></br> Regards"
                        '        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Error", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)

                        '        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Error", Nothing, Nothing, Nothing, "PreReg123*8!")
                        '        BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, tblTicketNo.Rows(i).Item(1).ToString, "Request", Nothing, "Error generating statement", Now)

                        '    Else
                        '        subj = "mybankStatement Ticket (Issuer Inoperative)"
                        '        body = "Hi </br></br> Could not generate ticket. Issuer inoperative</br></br> Regards"
                        '        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Error", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)

                        '        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Error", Nothing, Nothing, Nothing, "PreReg123*8!")
                        '        BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, tblTicketNo.Rows(i).Item(1).ToString, "Request", Nothing, "Error generating statement", Now)
                        '    End If

                        If tblTicketNo.Rows(i).Item(10).ToString.ToLower = "confirm" Or tblTicketNo.Rows(i).Item(10).ToString.ToLower = "charge" Then

                            Dim dtActivity As DataTable = Option2BLL.SelectActivityFromSpokeBYRequestID(tblTicketNo.Rows(i).Item(0).ToString)
                            '   If outcome.ToLower = "file sent successfully" Or outcome.ToLower = "file delivered successfully" Then

                            '  Dim solID As String = "0999"
                            Dim validPrice As String = BLL._selectvalidprice()

                            Dim wqAmount As Decimal = 0.0
                            Dim custAmount As Decimal = CDec(dtActivity.Rows(0).Item("total"))
                            Dim DiamondAmount As Decimal = 0.0

                            If validPrice.Split("|")(1).ToString = "0" Then
                                wqAmount = (CDec(dtActivity.Rows(0).Item("pages")) * CDec(validPrice.Split("|")(4)) + (0.7 * CDec(dtActivity.Rows(0).Item("additional")))) * 1.05
                            Else
                                wqAmount = (CDec(validPrice.Split("|")(4)) + 0.7 * (CDec(dtActivity.Rows(0).Item("additional")))) * 1.05
                            End If
                            DiamondAmount = ((CDec(validPrice.Split("|")(0)) - CDec(validPrice.Split("|")(4))) * CDec(dtActivity.Rows(0).Item("pages")) + 0.3 * (CDec(dtActivity.Rows(0).Item("additional"))))
                            Dim vatAmount As Decimal = CDec(dtActivity.Rows(0).Item("total")) - (wqAmount + DiamondAmount)
                            '  Dim vatAcctNo As String = "NGN" & solID & "2511001"


                            If dtActivity.Rows(0).Item(3).ToString = "0029846503" Then
                                Dim sentResult As String = sendRequestToService(dtActivity.Rows(0).Item(0).ToString)
                                If sentResult.Split("%")(0) = "True" Then

                                    '  BLL._UpdateEmailAndSMSAlert(dtActivity.Rows(0).Item(0).ToString, "1")
                                    BLL._updateStatus(dtActivity.Rows(0).Item(0).ToString, "Test")
                                    oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Sent", Nothing, Nothing, Nothing, "PreReg123*8!")

                                    subj = "mybankStatement Statement Delivered"
                                    body = "Hello " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>The bank statement requested from Diamond bank with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> has been delivered</b>. </br></br>Regards"
                                    BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "ticket sent", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                                Else
                                    oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "SendFailure", Nothing, Nothing, Nothing, "PreReg123*8!")
                                    subj = "mybankStatement"
                                    body = "Hello " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>The bank statement requested from Diamond bank with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> failed while being routed to destination</br></br> Regards"
                                    BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Sending failed", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                                End If
                            Else
                                Dim billRes As String = DAL_API.Billing(dtActivity.Rows(0).Item(3), custAmount,
                                                                        WebConfigurationManager.AppSettings("wqAcctNo").ToString(),
                                                                        wqAmount,
                                                                       WebConfigurationManager.AppSettings("DiamondAcctNoHO").ToString(), DiamondAmount,
                                                                        WebConfigurationManager.AppSettings("VATAcctNoHO").ToString(), vatAmount,
                                                                        dtActivity.Rows(0).Item(0).ToString,
                                                                        "Payment for E-Statement service and VAT")

                                If billRes.ToLower = "success" Then
                                    'The email and sms has been composed and saved in db. Updating to 1 allows it to be sent
                                    '  BLL._UpdateEmailAndSMSAlert(dtActivity.Rows(0).Item(0).ToString, "1")
                                    Dim sentResult As String = sendRequestToService(dtActivity.Rows(0).Item(0).ToString)
                                    If sentResult.Split("%")(0) = "True" Then
                                        BLL._updateStatus(dtActivity.Rows(0).Item(0).ToString, "RequestSent")
                                        BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, dtActivity.Rows(0).Item("destination").ToString, "Request", dtActivity.Rows(0).Item(0).ToString, "Request Sent", Now)
                                        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Sent", Nothing, Nothing, Nothing, "PreReg123*8!")

                                        subj = "mybankStatement Statement Delivered"
                                        body = "Hello " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>The bank statement requested from Diamond bank with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> has been delivered</b>. </br></br>Regards"
                                        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "ticket sent", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                                        BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, tblTicketNo.Rows(i).Item(1).ToString, "Request", dtActivity.Rows(0).Item(0).ToString, "Bank Statement sent", Now)
                                    Else
                                        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "SendFailure", Nothing, Nothing, Nothing, "PreReg123*8!")
                                        subj = "mybankStatement"
                                        body = "Hello " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>The bank statement requested from Diamond bank with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> failed while being routed to destination</br></br> Regards"
                                        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Sending failed", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                                    End If
                                ElseIf billRes.ToLower = "insfund" Then
                                    oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "BillFailure", Nothing, Nothing, Nothing, "PreReg123*8!")
                                    BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, dtActivity.Rows(0).Item("destination").ToString, "Request", dtActivity.Rows(0).Item(0).ToString, "Insufficient Fund", Now)
                                    subj = "mybankStatement Insufficient fund"
                                    body = "Hello " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>The bank statement requested from Diamond bank with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> could not be delivered becasue your customer, " + dtActivity.Rows(0).Item("Name").ToString + ", do not have sufficient fund to complete the transaction. Click Retry for this request if the account has been funded.</br></br> Regards"
                                    BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Insufficient Fund", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                                    BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Insufficient Fund", "support@wallzandqueenltd.com", subj, body, "1", Nothing)
                                Else
                                    Dim sentResult As String = sendRequestToService(dtActivity.Rows(0).Item(0).ToString)
                                    If sentResult.Split("%")(0) = "True" Then
                                        BLL._updateStatus(dtActivity.Rows(0).Item(0).ToString, "Not Sent")
                                        BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, dtActivity.Rows(0).Item("destination").ToString, "Request", dtActivity.Rows(0).Item(0).ToString, "Posting Error", Now)
                                        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Sent", Nothing, Nothing, Nothing, "PreReg123*8!")

                                        subj = "mybankStatement Statement Delivered"
                                        body = "Hello " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>The bank statement requested from Diamond bank with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> has been delivered.</b>. </br></br>Regards"
                                        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "ticket sent", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                                    Else
                                        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "SendFailure", Nothing, Nothing, Nothing, "PreReg123*8!")
                                        subj = "mybankStatement"
                                        body = "Hello " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>The bank statement requested from Diamond bank with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> failed while being routed to destination</br></br> Regards"
                                        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Sending failed", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)

                                    End If

                                    'oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "BillFailure", Nothing, Nothing, Nothing, "PreReg123*8!")
                                    'BLL._insertAuditLogs(tblTicketNo.Rows(i).Item(7).ToString, dtActivity.Rows(0).Item("destination").ToString, "Request", dtActivity.Rows(0).Item(0).ToString, "Billing failure. Error:" + billRes, Now)
                                    'subj = "mybankStatement Debit error"
                                    'body = "Hi " + tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) + ", </br></br>The bank statement requested from Diamond bank with request no <b> " + tblTicketNo.Rows(i).Item(0).ToString + "</b> could not be delivered because customer could not be debited. (Error detail : " & billRes & ") </br></br> Regards"
                                    'BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Billing Failure", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                                    'BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Billing Failure", "support@wallzandqueenltd.com", subj, body, "1", Nothing)

                                End If
                            End If


                        End If
                    Catch ex As Threading.ThreadAbortException
                        Utility.LogException("Initiator " & tblTicketNo.Rows(i).Item(1).ToString & " error message " & ex.Message.ToString() & "RequestID" & tblTicketNo.Rows(i).Item(0).ToString & " " & Now.ToString)
                    Catch ex As Exception
                        oWS.updateRequestStatus(tblTicketNo.Rows(i).Item(0).ToString, "Error", Nothing, Nothing, Nothing, "PreReg123*8!")
                        ' BLL._insertExceptionLog(senderid, ex.Message.ToString() & "RequestID" & tblTicketNo.Rows(i).Item(0).ToString, Now, company, "Request: " & typeException)
                        Utility.LogException("Initiator " & tblTicketNo.Rows(i).Item(1).ToString & " error message " & ex.Message.ToString() & "RequestID" & tblTicketNo.Rows(i).Item(0).ToString & " " & Now.ToString)
                        subj = "mybankStatement Request Error"
                        body = "Hello " & tblTicketNo.Rows(i).Item(7).ToString.Split("@")(0) & ", </br></br>The bank statement request with request no <b> " & tblTicketNo.Rows(i).Item(0).ToString & "</b> failed. Contact support at mailto:support@wallzandqueenltd.com <br /><br /> Regards"
                        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Error Encountered", tblTicketNo.Rows(i).Item(7).ToString, subj, body, "1", Nothing)
                        BLL._insertTicketMailAlertCorporate(tblTicketNo.Rows(i).Item(1).ToString, "Error Encountered", "support@wallzandqueenltd.com", subj, body & "</b> due to error : </br></br> " & ex.Message(), "1", Nothing)
                    End Try
                Next
            End If
        Catch ex As System.Net.WebException
            ' BLL._insertExceptionLog(senderid, ex.Message.ToString(), Now, company, "Request: " & typeException)
            Utility.LogException("Calling getRequestedActivity error " & " error message " & ex.Message.ToString() & " " & Now.ToString)

        Catch ex As System.Web.Services.Protocols.SoapException
            '  BLL._insertExceptionLog(senderid, ex.Message.ToString(), Now, company, "Request: " & typeException)
            Utility.LogException("Calling getRequestedActivity error " & " error message " & ex.Message.ToString() & " " & Now.ToString)
        Catch ex As Exception
            Utility.LogException("Calling getRequestedActivity error " & " error message " & ex.Message.ToString() & " " & Now.ToString)

        Finally

            'If (objFileStream IsNot Nothing) Then
            '    objFileStream.Close()
            'End If

        End Try
    End Sub
    Shared Function sendRequestToService(ByVal ticketNo As String) As String
        Try

            Dim oFileInfo As FileInfo = New FileInfo("D:\mybankStatementRepository\statement\" & ticketNo & ".pdf")

            Dim sendToService As New SendToService(oFileInfo, ticketNo, "request")
            Dim outcome As String = sendToService.SendPDF()
            '     Dim outcome As String = BLL.doFileUploadViaWebService(oFileInfo, ticketNo)            
            If outcome.ToLower = "file sent successfully" Or outcome.ToLower = "file delivered successfully" Then
                Dim statusCSV As String
                statusCSV = BLL._selectCSVStatus(ticketNo)

                If statusCSV = "1" Then
                    Dim csvFileInfo As FileInfo = New FileInfo(("D:\mybankStatementRepository\statementCSV\" & ticketNo & ".zip"))
                    Dim sendCsvToService As New SendCSVtoService(csvFileInfo, ticketNo)
                    sendCsvToService.doCSVFileUploadViaWebService()
                End If
                If BLL._GetFormat("1", ticketNo) = "1" Then
                    Dim jsonFileInfo As FileInfo = New FileInfo(("D:\mybankStatementRepository\statementJSON\" & ticketNo & ".zip"))
                    Dim sendJsonToService As New SendJsonToService(jsonFileInfo, ticketNo)
                    sendJsonToService.doJSONFileUploadViaWebService()
                End If
                Return "True%" & outcome
            Else
                Return "False%" & outcome
            End If

        Catch ex As Exception
            Return "False%"
        End Try
    End Function
    Public Shared Sub _updateAddAccountFromSpoke(ByVal email As String, ByVal bankID As String, ByVal acctNo As String, ByVal hubID As String, ByVal status As String, ByVal comment As String)
        SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionStringCorporate").ToString, "_updateAddAccountFromSpoke", email, bankID, acctNo, hubID, status, comment)
    End Sub
    Public Shared Sub _updateActivityFromSpoke(ByVal hubID As String, ByVal status As String, ByVal comment As String)
        SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionStringCorporate").ToString, "_updateActivityFromSpoke", hubID, status, comment)
    End Sub
    Public Shared Function _selectUnsentTicketMailAlertCorporate() As DataTable
        Dim List As DataTable = Nothing
        Try
            List = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUnsentTicketMailAlertCorporate").Tables(0)
            Return List
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub _deleteSentTicketMailAlertCorporate(ticketid As String)
        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                  "_deleteSentTicketMailAlertCorporate", ticketid)
    End Sub
    Public Shared Function _selectTransactionsTest(ByVal startDate As Date, ByVal endDate As Date, ByVal nuban As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_selectTransactionsTest", startDate, endDate, nuban)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectAccountPrint(ByVal nuban As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_selectAccountPrint", nuban)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectAccountService(ByVal nuban As String, ByVal period As String, ByVal email As String) As DataTable
        Dim ds As DataSet = Nothing
        Try

            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_selectAccountService", nuban, period, email)
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Shared Function _InsertScheduleCorporate(ByVal nuban As String, schID As String, statementType As String, pdf As String,
                                      excel As String, Mt940 As String, userID As String, hubId As String, time As Int16, state As String) As String
        Try

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                      "_InsertSchedule", nuban, schID, statementType, pdf, excel, Mt940, userID, hubId, time, state)
            Return "1"
        Catch ex As Exception
            Return "0"
        End Try
    End Function



    Public Shared Function _selectUsersReport() As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "exportUser")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '*************Prreg
    Public Shared Function _GetPreRegActivity(ByVal ID As String) As DataTable
        Try
            Dim dt As DataTable = Nothing
            dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionStringPreReg").ToString, "_GetPreRegActivity", ID).Tables(0)
            Return dt
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function _CheckValidPhone(ByVal phone As String, ByVal acctNo As String) As String
        Try
            Dim str As String = Nothing
            str = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_CheckValidPhone", phone, acctNo)
            Return str
        Catch
            Return "error"
        End Try
    End Function
    Public Shared Sub _updatePreRegAddAccountFromSpoke(ByVal email As String, ByVal bankID As String, ByVal acctNo As String, ByVal hubID As String, ByVal status As String, ByVal comment As String, ByVal activationCode As String, ByVal phone As String, ByVal sourceID As String)
        SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionStringPreReg").ToString, "_updatePreRegAddAccountFromSpoke", email, bankID, acctNo, hubID, status, comment, activationCode, phone, sourceID)
    End Sub

    Public Shared Sub _updatePreRegFromSpoke(ByVal email As String, ByVal bankID As String, ByVal acctNo As String, ByVal hubID As String, ByVal status As String, ByVal comment As String, ByVal activationCode As String, ByVal phone As String, ByVal sourceID As String)
        SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionStringPreReg").ToString, "_updatePreRegFromSpoke", email, bankID, acctNo, hubID, status, comment, activationCode, phone, sourceID)
    End Sub



    Public Shared Sub updateNewRequestStatus(ByVal requestID As String, ByVal status As String)
        SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updateNewRequestStatus", requestID, status)
    End Sub

    Public Shared Sub updateInitiatedRequestStatus(ByVal requestID As String, ByVal status As String)
        SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updateInitiatedRequestStatus", requestID, status)
    End Sub


    '*****Last Pricing addition
    Public Shared Function _insertCategory(ByVal ID As Integer, ByVal Category As String, ByVal CountryCode As String) As Integer
        Dim ds As Integer = Nothing
        Try

            ds = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                          "_insertCategory", ID, Category, CountryCode)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _selectUBACountriesAndCurrencies() As DataTable
        Dim ds As DataSet = Nothing
        Try
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUBACountriesAndCurrencies")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function _deleteRequest(id As String) As String
        Try
            Dim res As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                                  "_deleteRequest", id)
            Return res
        Catch ex As Exception
            Return "-1"
        End Try

    End Function
    Public Shared Function _updateRequestAccountName(ByVal id As String, name As String) As Integer
        Try
            Return SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updateRequestAccountName", id, name)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Shared Function _getCostPerPage() As Decimal

        Try
            Dim str As Decimal
            str = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_getCostPerPage")
            Return str
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function _GetFormat(ByVal formatID As String, ByVal destinationID As String) As String

        Try
            Dim res As String = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                    "_GetOtherFormat", formatID, destinationID)


            Return res
        Catch ex As Exception
            Return "No"

        End Try
    End Function
    Public Shared Sub _insertDeclinedComment(ByVal comment As String, ByVal ticketNo As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertDeclinedComment",
                                      comment.Replace("/", ""), ticketNo)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function _selectDeclinedComment(ByVal ticketNo As String) As String

        Try
            Dim str As String
            str = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectDeclinedComment", ticketNo)
            Return str
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub _resetUserProfile()
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_resetUserProfile")
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub _updateActivityWithBVN(ByVal bvn As String, ticketNo As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_updateActivityWithBVN", bvn, ticketNo)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub _insertBranch(ByVal branchCode As String, ByVal BranchName As String)
        Try

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertBranch",
                                     branchCode, BranchName)

        Catch ex As Exception

        End Try

    End Sub
    Public Shared Sub _insertWQPayment(ByVal ticketNo As String)
        Try
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertWQPayment", ticketNo)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function _insertSignatories(ByVal ticketid As String, name As String, bvn As String, email As String, phone As String, dob As String, nuban As String) As Int16
        Try
            Return SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_insertSignatories",
                                             ticketid, name, bvn, email, phone, dob, nuban)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Shared Function _selectSignatoriesNew(ByVal ticketID As String) As DataTable
        Try
            Dim dt As DataTable = Nothing
            dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectSignatoriesNew", ticketID).Tables(0)
            Return dt
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function _selectApplicantsString(ByVal ticketNo As String) As String
        Try
            Dim dt21 As DataTable = BLL._selectApplicant(ticketNo)
            Dim applicants As String = "N/A"
            If dt21.Rows.Count = 0 Then
                applicants = "N/A"
            Else
                For i As Integer = 0 To dt21.Rows.Count - 1
                    If i = dt21.Rows.Count - 1 Then applicants += dt21.Rows(i).Item(1) Else applicants += dt21.Rows(i).Item(1) & ";"
                Next
            End If
            Return applicants
        Catch ex As Exception
            Return "N/A"
        End Try
    End Function
    Public Shared Sub _insertAuditLogsBranch(ByVal UserName As String, ByVal Destination As String, ByVal Role As String,
                                            ByVal TicketID As String, ByVal Action As String, ByVal Timestamp As DateTime, branchID As String)

        Dim dID As Integer = Nothing
        Try
            Dim IP As String = Utility.getIPAddress()
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_insertAuditLogsBranch", UserName, Destination, Role, TicketID, Action, Timestamp, IP, branchID)

        Catch ex As Exception


        End Try
    End Sub
    Public Shared Function _selectUnviewedPDf() As DataTable
        Try
            Dim dt As DataTable = Nothing
            dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString, "_selectUnviewedPDf").Tables(0)
            Return dt
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Sub _updateViewPDF(ByVal ticketID As String, ByVal timestamp As DateTime)

        Dim dID As Integer = Nothing
        Try
            'Dim IP As String = Utility.getIPAddress()
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("iStatementConnectionString").ToString,
                           "_updateViewPDF", ticketID, timestamp)

        Catch ex As Exception


        End Try
    End Sub
End Class

