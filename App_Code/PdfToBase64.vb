Imports Microsoft.VisualBasic
Imports System.IO
Public Class PdfToBase64
    Private oFileInfo As FileInfo


    Public Sub New(FileName As FileInfo)
        oFileInfo = FileName

    End Sub
    Public Function getBase64String() As String
        Try
            Dim base64String As String = Nothing
            Dim oFileByte As Byte()
            'Using oFileStream As FileStream = File.Open(oFileInfo.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

            '    'declare the byte array of the file
            '    oFileByte = New Byte(oFileStream.Length - 1) {}

            '    'break the file into bytes and place into the byte object
            '    oFileStream.Read(oFileByte, 0, oFileStream.Length)
            'End Using
            oFileByte = File.ReadAllBytes(oFileInfo.FullName)
            base64String = System.Convert.ToBase64String(oFileByte)
            Return base64String
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    'Public Function getPDF(ByVal arr As String) As String
    '    Try
    '        Dim base64String As String = Nothing
    '        Dim oFileByte2 As Byte()
    '        'Using oFileStream As FileStream = File.Open(oFileInfo.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

    '        '    'declare the byte array of the file
    '        '    oFileByte = New Byte(oFileStream.Length - 1) {}

    '        '    'break the file into bytes and place into the byte object
    '        '    oFileStream.Read(oFileByte, 0, oFileStream.Length)
    '        'End Using
    '        '  oFileByte = File.ReadAllBytes(oFileInfo.FullName)
    '        oFileByte2 = System.Convert.FromBase64String(arr)

    '        Using objFileStream As FileStream = IO.File.Open(System.Web.Hosting.HostingEnvironment.MapPath("~\images\ticket.pdf"), IO.FileMode.Create, IO.FileAccess.Write)
    '            Dim lngLen As Long = oFileByte2.Length
    '            objFileStream.Write(oFileByte2, 0, CInt(lngLen))
    '        End Using
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function

End Class
