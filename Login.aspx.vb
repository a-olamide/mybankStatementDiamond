Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.Services
Imports System.Net
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Partial Class Login
    Inherits System.Web.UI.Page
    Public Shared loginStatus As String = ""
    Public Shared val = ""
    Private Shared Function IsOldUserValid(ByVal user As String, ByVal password As String) As Boolean
        Try

            Dim msg As String() = BLL._authenticateUser(user, password, 1)
            If msg(4) <> user Then
                Return False
            Else

                HttpContext.Current.Session("Company") = msg(6)
                HttpContext.Current.Session("NAME") = msg(0)
                HttpContext.Current.Session("USERID") = msg(4)
                HttpContext.Current.Session("Role") = msg(7)
                HttpContext.Current.Session("BranchName") = msg(10)
                HttpContext.Current.Session("Branch") = msg(11)
                HttpContext.Current.Session("ApprovalInfo") = msg(12)

                HttpContext.Current.Session("Country") = msg(13)
                ' HttpContext.Current.Session("Admin") = msg(5)
                'If HttpContext.Current.Session("Role") = "Admin" Then
                '    HttpContext.Current.Session("BranchName") = "UBN HEAD OFFICE"

                'End If
                Return True
            End If

        Catch ex As Exception

            Return False
        End Try

    End Function
    Private Shared Function IsUserValid(ByVal user As String, ByVal password As String) As Boolean
        Try
            Dim _domain As String = Nothing
            Dim dr As SqlDataReader = BLL._selectCompany()
            While dr.Read()
                If Not IsDBNull(dr.Item("Domain")) Then
                    _domain = dr.Item("Domain")
                    HttpContext.Current.Session("CountryCode") = dr.Item("CountryCode")
                    HttpContext.Current.Session("SenderID") = dr.Item("SenderID")
                    HttpContext.Current.Session("RCNO") = dr.Item("RCNO")
                End If
            End While
            dr.Close()

            'Dim username As String = IIf(user.Contains("@"), user, user & "@" & _domain)
            ''1. Valid user on AD
            ''2. Get Role on AD
            ''3. If existing on mybankStatement then grant access else if not existing on mybankStatement then create and grant access. *Also fetch admin status.
            ''4. If admin then allow additional menu items to its existing role.
            'Dim salt As String = BLL._receiveSaltDetails(user)
            'password = generateSHA256Hash(password, salt)

          '  Dim salt As String = BLL._receiveSaltDetails(user)
          '  password = generateSHA256Hash(password, salt)
            Dim msg As String() = BLL._authenticateUser(user, password, 1)
            If msg(4) <> user Then
                Return False
            Else

                HttpContext.Current.Session("Company") = msg(6)
                HttpContext.Current.Session("NAME") = msg(0)
                HttpContext.Current.Session("USERID") = msg(4)
                HttpContext.Current.Session("Role") = msg(7)
                HttpContext.Current.Session("BranchName") = msg(10)
                HttpContext.Current.Session("Branch") = msg(11)
                HttpContext.Current.Session("Country") = msg(13)
                ' HttpContext.Current.Session("NAME") = "Juachukwu Ugwu"

                HttpContext.Current.Session("ApprovalInfo") = msg(12)
                ' HttpContext.Current.Session("Admin") = msg(5)
                'If HttpContext.Current.Session("Role") = "Admin" Then
                '    HttpContext.Current.Session("BranchName") = "UBN HEAD OFFICE"

                'End If
                Return True
            End If

        Catch ex As Exception

            Return False
        End Try

    End Function
    
Private Shared Function IsUserValidAD(ByVal user As String, ByVal password As String) As Boolean
        Dim _domain As String = Nothing
        Dim dr As SqlDataReader = BLL._selectCompany()
        While dr.Read()
            If Not IsDBNull(dr.Item("Domain")) Then
                _domain = dr.Item("Domain")
                HttpContext.Current.Session("CountryCode") = dr.Item("CountryCode")
                HttpContext.Current.Session("SenderID") = dr.Item("SenderID")
                HttpContext.Current.Session("RCNO") = dr.Item("RCNO")
                HttpContext.Current.Session("Company") = dr.Item("CompanyName")
            End If
        End While
        dr.Close()
        Try

            ' ValidateUser.AuthenticateUser(user, password)
            ' Return IsUserValid(user, password)
            '  If ValidateUser.AuthenticateUser(user, password) = True Then
            Dim username As String = IIf(user.Contains("@"), user, user & "@" & "diamondbank.com")
            ' Dim employeeid As String = ValidateUser.getemployeeid(user, password)
            Dim userInfo As DataRow = Nothing

            Dim adclass As New DiamondService.Service
            Dim resArr() As String = adclass.getStaffDetails(user, password)
            'utility.LogException(resArr(1))
            If resArr(0) = "00" Then
                Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(resArr(1))

                ' Authenticated","AuthCode":1,"IsAccountActive":true
                If j("AuthCode") = 1 And j("IsAccountActive") = True Then
                    If BLL._checkNewUser(username) = "1" Then
                        Return IsOldUserValid(username, "test")
                    Else
                        HttpContext.Current.Session("USERID") = j("Email")
                        Dim branchCode As String = j("BranchCode")
                        Dim branchName As String = j("BranchCode")
                        HttpContext.Current.Session("Branch") = branchCode
                        HttpContext.Current.Session("BranchName") = branchName
                        HttpContext.Current.Session("NAME") = j("DisplayName")
                        HttpContext.Current.Session("ApprovalInfo") = ""
                        Dim roleAD As String = j("Department")
                        Dim role As String = BLL._selectSystemRoleByAD(Trim(roleAD))
                        If role Is Nothing Then
                            role = roleAD
                        End If
                        HttpContext.Current.Session("Role") = role
                        BLL._insertUserAD(j("FirstName"), j("LastName"), j("Email"), role, 0, branchName, branchCode, "", Nothing, branchCode)
                        Return True
                    End If
                ElseIf j("AuthCode") = 1 And j("IsAccountActive") = False Then
                    Utility.LogException("User " & user & " has been deactivated")
                    loginStatus = "inactive"
                    Return False
                Else

                    Return IsUserValid(user, password)
                End If
            Else
                Return IsUserValid(user, password)
            End If




            'Else
            '    Return IsUserValid(user, password)
            '    'errorLabel.Text = "Authentication did not succeed. Check user name and password."
            'End If

        Catch ex As Exception
            loginStatus = ex.Message
            Return False
            ' errorLabel.Text = "Error authenticating. " & ex.Message
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim serv As New AccEnqService.banks
            'Dim res As DataTable = serv.getAccountFullInfo("0029846503").Tables(0)
            'Dim servVAT As New Posting.IBSServices
            'Dim strVAT As String = "<?xml version=""1.0"" encoding=""UTF-8"" ?><IBSRequest> <ReferenceID>" & "12asweddfdsaf231000023" & "</ReferenceID> <RequestType>102</RequestType> <FromAccount>" & "0017548985" & "</FromAccount> <ToAccount>" & "0029846503" & "</ToAccount> <Amount>" & "1.500" & "</Amount> <PaymentReference>" & "Charge for mybankStatement" & "</PaymentReference> </IBSRequest>"
            'Dim resultVAT As String = Utility.Decrypt(servVAT.IBSBridge(Utility.Encrypt(strVAT), 54))


            'Dim oWS As New net.mybankstatement.WebService()

            ' Dim proxyVal As New WebProxy("http://10.0.0.120:80", True)
            ' oWS.Proxy = proxyVal
            'Dim res As Boolean = oWS._confirmRequestStatus("618", "Confirm", "1000010-1", "ntxq", Nothing, "PreReg123*8!")
            'Dim appl As String() = {"Olamide Dawood", "Ayo Davies"}
            'Dim val As String = oWS.insertStatementRequest("0026423990", "1", Nothing, "9", "25 Feb 2017", "26 May 2017",
            '                                     "Applicants", "a.olamide@wallzandqueenltd.com", Nothing, "Nigeria", "07039335391", appl, "PreReg123*8!")
            ' 
            Dim str As String = ""
        Catch ex As Exception

        End Try

        If Not IsPostBack Then
            'Session.Abandon()

            LoadLoginPage()

        End If
        '  End If
    End Sub
    Sub LoadLoginPage()
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine &
                   "   jQuery('#login_box').addClass('visible'); " & _
               " </script>"
        lbl.ID = "aakk"
        Me.Controls.Add(lbl)
    End Sub
    Sub LoadRedirectPage()
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine &
                   "   jQuery('#redirect').addClass('visible'); " & _
               " </script>"
        lbl.ID = "aakk1"
        Me.Controls.Add(lbl)
    End Sub
    Sub LoadRedirectPageIE()
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine &
                   "   jQuery('#IEredirect').addClass('visible'); " & _
               " </script>"
        lbl.ID = "aakk2"
        Me.Controls.Add(lbl)
    End Sub
    Sub LoadRedirectPageFF()
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine &
                   "   jQuery('#FirefoxRedirect').addClass('visible'); " & _
               " </script>"
        lbl.ID = "aakk3"
        Me.Controls.Add(lbl)
    End Sub
    Private Shared Function createSalt(ByVal size As Int16) As String
        Dim rng = New System.Security.Cryptography.RNGCryptoServiceProvider()
        Dim buff(size) As Byte
        rng.GetBytes(buff)
        Return Convert.ToBase64String(buff)
    End Function

    Private Shared Function generateSHA256Hash(ByVal input As String, ByVal salt As String) As String
        Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(input & salt)
        Dim sha256Hash As System.Security.Cryptography.SHA256Managed = New System.Security.Cryptography.SHA256Managed()
        Dim hash() As Byte = sha256Hash.ComputeHash(bytes)
        Return ByteArrayToHexString(hash)
    End Function
    Public Shared Function ByteArrayToHexString(ByVal bytes As Byte()) As String
        Dim hex As String = BitConverter.ToString(bytes)
        Return hex.Replace("-", "")
    End Function
    '0025514745
    <WebMethod()> _
    Public Shared Function _login(ByVal value As String, ByVal pass As String) As String


        'BLL._insertSP(value, pass)
        ', ByVal surname As String, ByVal firstname As String, ByVal jobTitle As String, ByVal BankBranchID As String, ByVal BankBranchName As String, Supervisor As String

        SqlConnection.ClearAllPools()

        If pass = "" Or value = "" Then
            Return "False%NoEntry"
        End If
        Try
            If IsUserValidAD(Trim(value).ToLower, pass) = True Then

                If pass = "Password123" Then
                    Return "True%First"
                ElseIf HttpContext.Current.Session("Role") = "Initiator" Then
                    BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), "N/A", HttpContext.Current.Session("Role"), "N/A", "Login", Now, HttpContext.Current.Session("Branch"))
                    Return "True%Dashboard.aspx"
                ElseIf HttpContext.Current.Session("Role") = "Admin" Or HttpContext.Current.Session("Role") = "Admin" Then
                    BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), "N/A", HttpContext.Current.Session("Role"), "N/A", "Login", Now, HttpContext.Current.Session("Branch"))
                    Return "True%CreateUser.aspx"
                ElseIf HttpContext.Current.Session("Role") = "Approver" Then
                    BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), "N/A", HttpContext.Current.Session("Role"), "N/A", "Login", Now, HttpContext.Current.Session("Branch"))
                    Return "True%Approval.aspx"
                ElseIf HttpContext.Current.Session("Role") = "Reviewer" Then
                    BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), "N/A", HttpContext.Current.Session("Role"), "N/A", "Login", Now, HttpContext.Current.Session("Branch"))
                    Return "True%Preview.aspx"
                ElseIf HttpContext.Current.Session("Role") = "Auditor" Then
                    BLL._insertAuditLogsBranch(HttpContext.Current.Session("USERID"), "N/A", HttpContext.Current.Session("Role"), "N/A", "Login", Now, HttpContext.Current.Session("Branch"))
                    Return "True%AuditLogs.aspx"
                Else
                    Return "False%NoPermission"
                End If
            Else
                If loginStatus = "NoExceed" Then
                    Return "False%NoExceed"
                ElseIf loginStatus = "AuthError" Then
                    Return "False%AuthError"
                ElseIf loginStatus = "inactive" Then
                    Return "False%Inactive"
                Else
                    Return "False"
                End If
            End If
        Catch ex As Exception
            Return "False"
        End Try

    End Function
    <WebMethod()> _
    Public Shared Function _updatePassword(ByVal value As String, ByVal pass As String) As String
        Try
            Dim salt As String = BLL._receiveSaltDetails(value)
            pass = generateSHA256Hash(pass, salt)
            If BLL._updatePassword(Trim(value).ToLower, pass) = "1" Then Return "True%Dashboard.aspx" Else Return "False"
        Catch ex As Exception
            Return "False"
        End Try
        Return ""
    End Function


    Private Function detectBrowser() As String
        Dim ie As Int16 = Convert.ToInt16(WebConfigurationManager.AppSettings("ie").ToString())
        Dim firefox As Int16 = Convert.ToInt16(WebConfigurationManager.AppSettings("firefox").ToString())
        Dim google As Int16 = Convert.ToInt16(WebConfigurationManager.AppSettings("google").ToString())
        If (HttpContext.Current.Request.Browser.Type.Contains("Firefox")) And (HttpContext.Current.Request.Browser.MajorVersion < firefox) Then

            Return "Falseb%firefox"
        ElseIf (HttpContext.Current.Request.Browser.Type.ToUpper().Contains("INTERNETEXPLORER")) And (HttpContext.Current.Request.Browser.MajorVersion < ie) Then

            Return "Falseb%ie"
        ElseIf (HttpContext.Current.Request.Browser.Type.ToUpper().Contains("CHROME")) Then
            If (HttpContext.Current.Request.Browser.MajorVersion < google) Then

                Return "Falseb%chrome"

            Else
                Return "True%"
            End If
        Else
            Return "True%"
        End If

    End Function


End Class
