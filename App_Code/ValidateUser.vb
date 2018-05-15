Imports System
Imports System.Configuration
Imports System.Security
Imports System.DirectoryServices
'Imports ADAuth
Imports System.Data
Imports Microsoft.ApplicationBlocks.Data


Public Class ValidateUser

    Public Shared Function AuthenticateUser(user As String, pass As String) As Boolean
        Dim path As String
        path = "LDAP://Diamondbank.com"
        Dim de As New DirectoryEntry(path, user, pass, AuthenticationTypes.Secure)
        Try
            'run a search using those credentials. 
            'If it returns anything, then you're authenticated
            Dim ds As DirectorySearcher = New DirectorySearcher(de)
            ds.FindOne()
            Return True
        Catch ex As Exception
            'otherwise, it will crash out so return false
            Return False
        End Try
    End Function
    Public Shared Function getemployeeid(user As String, pass As String) As String
        Dim path As String
        path = "LDAP://Diamondbankng.com"
        Dim de As New DirectoryEntry(path, user, pass, AuthenticationTypes.Secure)
        Try
            'run a search using those credentials. 
            'If it returns anything, then you're authenticated

            Dim ds As DirectorySearcher = New DirectorySearcher(de)
            ds.Filter = "(SAMAccountName=" + user + ")"
            ds.PropertiesToLoad.Add("employeeid")
            ds.FindOne()
            Dim sr As SearchResult = ds.FindOne()
            'Dim result As SearchResult = ds.FindOne()
            Dim dt As DirectoryEntry = sr.GetDirectoryEntry()
            Dim r As String = dt.Properties("employeeid").Value.ToString
            Return r
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function getRole(user As String, pass As String) As String
        Dim path As String
        path = "LDAP://Diamondbankng.com"
        Dim de As New DirectoryEntry(path, user, pass, AuthenticationTypes.Secure)
        Try
            'run a search using those credentials. 
            'If it returns anything, then you're authenticated

            Dim ds As DirectorySearcher = New DirectorySearcher(de)
            ds.Filter = "(SAMAccountName=" + user + ")"
            ds.PropertiesToLoad.Add("title")
            ds.FindOne()
            Dim sr As SearchResult = ds.FindOne()
            'Dim result As SearchResult = ds.FindOne()
            Dim dt As DirectoryEntry = sr.GetDirectoryEntry()
            Dim r As String = dt.Properties("title").Value.ToString
            Return r
        Catch
            Return Nothing
        End Try
    End Function
    
    'Public Shared Function getBranch(user As String, pass As String) As String
    '    Dim path As String
    '    path = "LDAP://Diamondbankng.com"
    '    Dim de As New DirectoryEntry(path, user, pass, AuthenticationTypes.Secure)
    '    Try
    '        'run a search using those credentials. 
    '        'If it returns anything, then you're authenticated

    '        Dim ds As DirectorySearcher = New DirectorySearcher(de)
    '        ds.Filter = "(SAMAccountName=" + user + ")"
    '        ds.PropertiesToLoad.Add("memberOf")
    '        ds.FindOne()
    '        Dim sr As SearchResult = ds.FindOne()
    '        'Dim result As SearchResult = ds.FindOne()
    '        Dim dt As DirectoryEntry = sr.GetDirectoryEntry()
    '        Dim propertyCount As Int16 = dt.Properties("memberOf").Count


    '        Dim groupNames As New StringBuilder
    '        Dim dn As String
    '        Dim equalsIndex, commaIndex As String

    '        For propertyCounter As Int16 = 0 To propertyCount - 1
    '            dn = CType(sr.Properties("memberOf")(propertyCounter), String)
    '            equalsIndex = dn.IndexOf("=", 1)
    '            commaIndex = dn.IndexOf(",", 1)
    '            If (equalsIndex <> -1) Then
    '                groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1))
    '                groupNames.Append("|")
    '            End If
    '        Next
    '        Dim newStr As String = groupNames.ToString()
    '        equalsIndex = newStr.ToString.IndexOf("AllENG", 1) + 5
    '        commaIndex = newStr.ToString.IndexOf("|", CInt(equalsIndex))
    '        newStr = newStr.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1)
    '        Return Trim(Replace(newStr, "-", ""))



    '        'Dim r1 As String = CType((dt.Properties("memberOf").Value)(0), String)
    '        'Dim r2 As String = CType((dt.Properties("memberOf").Value)(1), String)
    '        'Dim r3 As String = CType((dt.Properties("memberOf").Value)(2), String)
    '        'Dim r4 As String = CType((dt.Properties("memberOf").Value)(3), String)
    '        'Dim r5 As String = CType((dt.Properties("memberOf").Value)(4), String)
    '        'Dim r6 As String = CType((dt.Properties("memberOf").Value)(5), String)
    '        'Dim r7 As String = CType((dt.Properties("memberOf").Value)(6), String)


    '        'Return r1
    '    Catch
    '        Return Nothing
    '    End Try
    'End Function


    Public Shared Function getName(user As String, pass As String) As String
        Dim path As String
        path = "LDAP://Diamondbankng.com"
        Dim de As New DirectoryEntry(path, user, pass, AuthenticationTypes.Secure)
        Try
            'run a search using those credentials. 
            'If it returns anything, then you're authenticated

            Dim ds As DirectorySearcher = New DirectorySearcher(de)
            ds.Filter = "(SAMAccountName=" + user + ")"
            ds.PropertiesToLoad.Add("givenName")
            ds.FindOne()
            Dim sr As SearchResult = ds.FindOne()
            'Dim result As SearchResult = ds.FindOne()
            Dim dt As DirectoryEntry = sr.GetDirectoryEntry()
            Dim r As String = dt.Properties("givenName").Value.ToString
            Return r
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function getSurname(user As String, pass As String) As String
        Dim path As String
        path = "LDAP://Diamondbankng.com"
        Dim de As New DirectoryEntry(path, user, pass, AuthenticationTypes.Secure)
        Try
            'run a search using those credentials. 
            'If it returns anything, then you're authenticated

            Dim ds As DirectorySearcher = New DirectorySearcher(de)
            ds.Filter = "(SAMAccountName=" + user + ")"
            ds.PropertiesToLoad.Add("sn")
            ds.FindOne()
            Dim sr As SearchResult = ds.FindOne()
            'Dim result As SearchResult = ds.FindOne()
            Dim dt As DirectoryEntry = sr.GetDirectoryEntry()
            Dim r As String = dt.Properties("sn").Value.ToString
            Return r
        Catch
            Return Nothing
        End Try
    End Function
 

   
   


End Class
