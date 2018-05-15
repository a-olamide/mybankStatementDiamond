Imports Microsoft.VisualBasic
Imports System
Imports System.Web.Configuration
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports Ionic.Zip
Imports System.Data
Imports Newtonsoft.Json.Linq
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports Newtonsoft.Json

Public Class Utility
    Public Shared Function reformatnuban(nuban As String) As String
        Try
            Dim val(0 To nuban.Count - 1) As String
            Dim result As String = ""
            For i = 0 To nuban.Count - 1
                val(i) = nuban.Chars(i)
            Next
            val(2) = "x"
            val(3) = "x"
            val(4) = "x"
            val(5) = "x"
            val(6) = "x"
            val(7) = "x"
            For j = 0 To nuban.Count - 1
                result += val(j)
            Next
            Return result
        Catch ex As Exception
            Return nuban
        End Try
    End Function
    Public Shared Function generateRandonNo(no As Int16) As String
        Dim str As String = ""
        For i As Int16 = 0 To no
            str &= CStr(CInt(Math.Ceiling(Rnd() * i)) + 1)
        Next

        Return str
    End Function
    Public Shared Function ValidateEmail(ByVal email As String) As Boolean
        Try
            Dim addr As New System.Net.Mail.MailAddress(Trim(email))
            Return addr.Address = Trim(email)

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function createSalt(ByVal size As Int16) As String
        Dim rng = New System.Security.Cryptography.RNGCryptoServiceProvider()
        Dim buff(size) As Byte
        rng.GetBytes(buff)
        Return Convert.ToBase64String(buff)
    End Function
    Public Shared Function generateSHA256Hash(ByVal input As String, ByVal salt As String) As String
        Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(input & salt)
        Dim sha256Hash As System.Security.Cryptography.SHA256Managed = New System.Security.Cryptography.SHA256Managed()
        Dim hash() As Byte = sha256Hash.ComputeHash(bytes)
        Return ByteArrayToHexString(hash)
    End Function
    Public Shared Function ByteArrayToHexString(ByVal bytes As Byte()) As String
        Dim hex As String = BitConverter.ToString(bytes)
        Return hex.Replace("-", "")
    End Function
    Public Shared Sub MergeTwoPdfsToSingle(ByVal inputFile1 As String, ByVal inputFile2 As String, ByVal outputFile As String, ByVal password As String)

        'Step 1: Create a Docuement-Object
        Dim document As Document = New Document(PageSize.A4, 20.0F, 20.0F, 20.0F, 20.0F)
        Try

            'Step 2: we create a writer that listens to the document
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(outputFile, FileMode.Create))

            'Step 3: Open the document
            document.Open()

            Dim cb As PdfContentByte = writer.DirectContent
            Dim page1 As PdfImportedPage
            Dim page2 As PdfImportedPage
            Dim page3 As PdfImportedPage
            Dim page4 As PdfImportedPage
            'we create a reader for the document
            Dim reader1 As PdfReader = New PdfReader(inputFile1, System.Text.Encoding.UTF8.GetBytes(password))
            Dim reader2 As PdfReader = New PdfReader(inputFile2)
            Dim no As Int16 = reader1.NumberOfPages
            'document.SetPageSize(reader2.GetPageSizeWithRotation(1))
            'document.NewPage()
            If no > 2 Then
                page1 = writer.GetImportedPage(reader2, 1)
                page2 = writer.GetImportedPage(reader1, 1)
                page3 = writer.GetImportedPage(reader1, (no - 1))
                page4 = writer.GetImportedPage(reader1, (no))

                cb.AddTemplate(page1, 0.45F, 0, 0, 0.45F, 20, 420)
                'play around to find the exact location for the next pdf
                cb.AddTemplate(page2, 0.45F, 0, 0, 0.45F, 20, 30)
                cb.AddTemplate(page3, 0.45F, 0, 0, 0.45F, 290, 420)
                'play around to find the exact location for the next pdf
                cb.AddTemplate(page4, 0.45F, 0, 0, 0.45F, 290, 30)

            ElseIf no = 2 Then
                document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate())
                page1 = writer.GetImportedPage(reader2, 1)
                page2 = writer.GetImportedPage(reader1, 1)
                page3 = writer.GetImportedPage(reader1, 2)

                cb.AddTemplate(page1, 0.45F, 0, 0, 0.5F, 140, 420)
                'play around to find the exact location for the next pdf
                cb.AddTemplate(page2, 0.45F, 0, 0, 0.5F, 20, 30)
                cb.AddTemplate(page3, 0.45F, 0, 0, 0.5F, 290, 30)
            Else
                document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate())
                page1 = writer.GetImportedPage(reader2, 1)
                page2 = writer.GetImportedPage(reader1, 1)


                cb.AddTemplate(page1, 0.5F, 0, 0, 0.5F, 140, 420)
                'play around to find the exact location for the next pdf
                cb.AddTemplate(page2, 0.5F, 0, 0, 0.5F, 140, 30)

            End If
            document.Close()
            reader1.Close()
            reader2.Close()
        Catch e As Exception

        Finally
            document.Close()
        End Try
    End Sub

    Public Shared Sub RotatePDF(ByVal inputFile As String, ByVal outputFile As String)

        Using outStream As FileStream = New FileStream(outputFile, FileMode.Create)

            Dim reader As iTextSharp.text.pdf.PdfReader = New iTextSharp.text.pdf.PdfReader(inputFile)
            Dim stamper As iTextSharp.text.pdf.PdfStamper = New iTextSharp.text.pdf.PdfStamper(reader, outStream)

            Dim pageDict As iTextSharp.text.pdf.PdfDictionary = reader.GetPageN(1)
            Dim desiredRot As Integer = 90
            ' 90 degrees clockwise from what it is now
            Dim rotation As iTextSharp.text.pdf.PdfNumber = pageDict.GetAsNumber(iTextSharp.text.pdf.PdfName.ROTATE)

            If rotation IsNot Nothing Then

                desiredRot += rotation.IntValue
                desiredRot = desiredRot Mod 360
                '// must be 0, 90, 180, or 270
            End If
            pageDict.Put(iTextSharp.text.pdf.PdfName.ROTATE, New iTextSharp.text.pdf.PdfNumber(desiredRot))

            stamper.Close()
        End Using
    End Sub
    Public Shared Sub RotatePDFPassWord(ByVal inputFile As String, ByVal outputFile As String, ByVal password As String)

        Using outStream As FileStream = New FileStream(outputFile, FileMode.Create)

            Dim reader As iTextSharp.text.pdf.PdfReader = New iTextSharp.text.pdf.PdfReader(inputFile, System.Text.Encoding.UTF8.GetBytes(password))
            Dim stamper As iTextSharp.text.pdf.PdfStamper = New iTextSharp.text.pdf.PdfStamper(reader, outStream)

            For i As Int16 = 1 To reader.NumberOfPages


                Dim pageDict As iTextSharp.text.pdf.PdfDictionary = reader.GetPageN(i)
                Dim desiredRot As Integer = 90
                ' 90 degrees clockwise from what it is now
                Dim rotation As iTextSharp.text.pdf.PdfNumber = pageDict.GetAsNumber(iTextSharp.text.pdf.PdfName.ROTATE)

                If rotation IsNot Nothing Then

                    desiredRot += rotation.IntValue
                    desiredRot = desiredRot Mod 360
                    '// must be 0, 90, 180, or 270
                End If
                pageDict.Put(iTextSharp.text.pdf.PdfName.ROTATE, New iTextSharp.text.pdf.PdfNumber(desiredRot))
            Next
            stamper.Close()
        End Using
    End Sub
    Public Shared Sub ToZIP(ByVal sourcefile As String, ByVal destfile As String, ByVal passcode As String, ticketNo As String)
        Dim zip As New ZipFile
        zip.Password = passcode
        zip.Encryption = EncryptionAlgorithm.PkzipWeak
        zip.AddFile(sourcefile, "")
        zip.Save(destfile)
        zip.Dispose()
    End Sub
    Public Shared Sub ToZIPJson(ByVal sourcefile As String, ByVal destfile As String, ByVal passcode As String, ticketNo As String)
        Dim zip As New ZipFile
        zip.Password = passcode
        zip.Encryption = EncryptionAlgorithm.PkzipWeak
        zip.AddFile(sourcefile, "")
        zip.Save(destfile)
        zip.Dispose()
    End Sub
    Public Shared Function Get4RadomPassword() As String
        Dim s As String = "0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i1 As Integer = 1 To 4
            Dim idx As Integer = r.Next(0, 9)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function
    Public Shared Sub Show(ByRef page As Page, ByVal strMsg As String)
        Try
            Dim lbl As New Label
            lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                        & "window.alert(" & "'" & strMsg & "'" & ")</script>"

            page.Controls.Add(lbl)
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowSweet(ByRef page As Page, ByVal strMsg As String)
        Try
            Dim lbl As New Label
            lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                        & "swal({   title: " & "'" & strMsg & "',   text: ' I will close in 10 seconds.',   timer: 10000,   showConfirmButton: true })</script>"
            page.Controls.Add(lbl)
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowSweet2(ByRef page As Page, ByVal strMsg As String)
        Try
            Dim lbl As New Label
            lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                        & "swal({   title: " & "'" & strMsg & "',   text: ' I will close in 10 seconds.',   timer: 2000,   showConfirmButton: false })</script>"
            page.Controls.Add(lbl)
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowConfirmRedirect(ByRef page As Page)
        Try
            Dim lbl As New Label
            lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                        & "swal({title:'Browser not compatible',text:'Upgrade Browser',type:'warning',showCancelButton: true,confirmButtonColor: '#DD6B55',confirmButtonText: 'Upgrade',closeOnConfirm: false }, function(){swal('Deleted!','Done', 'success'); });</script>"
            page.Controls.Add(lbl)
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub exportReport(dt1 As GridView, ByVal filename As String)

        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", filename))
            HttpContext.Current.Response.ContentType = "application/ms-excel"

            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    Dim table As New Table()

                    table.Font.Name = "Arial"

                    table.Font.Size = 8
                    table.HorizontalAlign = HorizontalAlign.Center

                    Dim total As Integer = dt1.Columns.Count - 1


                    Dim rowcount As Integer = 0

                    Dim colcount As Integer = 0

                    Dim i As Integer = 0

                    ' add each of the data rows to the table

                    For Each row As GridViewRow In dt1.Rows

                        For colcount = 0 To 13
                            If rowcount = 0 Then
                                'row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9")
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: 1; border-left-color: Black;  border-right-style: solid; border-right-width: 1; border-right-color: Black; border-bottom-style: solid; border-bottom-width: 1; border-bottom-color: Black; border-top-style: solid; border-top-width: 1; border-top-color: Black;")
                                row.Cells(colcount).Font.Bold = True
                                row.Cells(colcount).ForeColor = System.Drawing.Color.White
                                row.Cells(colcount).BackColor = System.Drawing.Color.Black
                            End If
                            row.Cells(colcount).Width = 100


                            'If colcount = 10 Then row.Cells(colcount).Style.Add("", "")

                            If colcount >= 4 And colcount < 7 Then
                                row.Cells(colcount).HorizontalAlign = HorizontalAlign.Right
                                row.Cells(colcount).Width = 100
                            End If


                            If rowcount Mod 2 = 0 Then

                                Utility.PrepareControlForExport(row)

                                row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9")
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: thin; border-left-color: Black;  border-right-style: solid; border-right-width: thin; border-right-color: Black; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: Black; border-top-style: solid; border-top-width: thin; border-top-color: Black;")

                                row.Cells(colcount).ForeColor = System.Drawing.Color.Black
                            Else

                                Utility.PrepareControlForExport(row)
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: thin; border-left-color: Black;  border-right-style: solid; border-right-width: thin; border-right-color: Black; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: Black; border-top-style: solid; border-top-width: thin; border-top-color: Black;")


                                row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2")

                                row.Cells(colcount).ForeColor = System.Drawing.Color.Black
                            End If
                        Next



                        table.Rows.Add(row)

                        colcount = 0
                        rowcount = rowcount + 1

                    Next

                    ' render the table into the htmlwriter

                    table.RenderControl(htw)

                    ' render the htmlwriter into the response

                    HttpContext.Current.Response.Write(sw.ToString())

                    HttpContext.Current.Response.[End]()

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
    Public Shared Sub exportAuditReport(dt1 As GridView, ByVal filename As String)

        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", filename))
            HttpContext.Current.Response.ContentType = "application/ms-excel"

            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    Dim table As New Table()

                    table.Font.Name = "Arial"

                    table.Font.Size = 8
                    table.HorizontalAlign = HorizontalAlign.Center

                    Dim total As Integer = dt1.Columns.Count - 1


                    Dim rowcount As Integer = 0

                    Dim colcount As Integer = 0

                    Dim i As Integer = 0

                    ' add each of the data rows to the table

                    For Each row As GridViewRow In dt1.Rows

                        For colcount = 0 To 7
                            If rowcount = 0 Then
                                'row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9")
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: 1; border-left-color: Black;  border-right-style: solid; border-right-width: 1; border-right-color: Black; border-bottom-style: solid; border-bottom-width: 1; border-bottom-color: Black; border-top-style: solid; border-top-width: 1; border-top-color: Black;")
                                row.Cells(colcount).Font.Bold = True
                                row.Cells(colcount).ForeColor = System.Drawing.Color.White
                                row.Cells(colcount).BackColor = System.Drawing.Color.Black
                            End If
                            row.Cells(colcount).Width = 100


                            'If colcount = 10 Then row.Cells(colcount).Style.Add("", "")



                            If rowcount Mod 2 = 0 Then

                                Utility.PrepareControlForExport(row)

                                row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9")
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: thin; border-left-color: Black;  border-right-style: solid; border-right-width: thin; border-right-color: Black; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: Black; border-top-style: solid; border-top-width: thin; border-top-color: Black;")

                                row.Cells(colcount).ForeColor = System.Drawing.Color.Black
                            Else

                                Utility.PrepareControlForExport(row)
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: thin; border-left-color: Black;  border-right-style: solid; border-right-width: thin; border-right-color: Black; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: Black; border-top-style: solid; border-top-width: thin; border-top-color: Black;")


                                row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2")

                                row.Cells(colcount).ForeColor = System.Drawing.Color.Black
                            End If
                        Next



                        table.Rows.Add(row)

                        colcount = 0
                        rowcount = rowcount + 1

                    Next

                    ' render the table into the htmlwriter

                    table.RenderControl(htw)

                    ' render the htmlwriter into the response

                    HttpContext.Current.Response.Write(sw.ToString())

                    HttpContext.Current.Response.[End]()

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
    Private Shared Sub PrepareControlForExport(ByVal control As Control)
        Try
            For i As Integer = 0 To control.Controls.Count - 1

                Dim current As Control = control.Controls(i)

                If TypeOf current Is LinkButton Then

                    control.Controls.Remove(current)

                    control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))

                ElseIf TypeOf current Is ImageButton Then

                    control.Controls.Remove(current)

                    control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))

                ElseIf TypeOf current Is HyperLink Then

                    control.Controls.Remove(current)

                    control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))

                ElseIf TypeOf current Is DropDownList Then

                    control.Controls.Remove(current)

                    control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))

                ElseIf TypeOf current Is CheckBox Then

                    control.Controls.Remove(current)

                    control.Controls.AddAt(i, New LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False")))

                End If

                If current.HasControls() Then

                    Utility.PrepareControlForExport(current)

                End If

            Next

        Catch ex As Exception

        End Try

    End Sub
    Public Shared Function FileToByteArray(ByVal _FileName As String) As Byte()

        Dim _Buffer() As Byte = Nothing

        Try

            ' Open file for reading

            Dim _FileStream As New System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

            ' attach filestream to binary reader

            Dim _BinaryReader As New System.IO.BinaryReader(_FileStream)

            ' get total byte length of the file

            Dim _TotalBytes As Long = New System.IO.FileInfo(_FileName).Length

            ' read entire file into buffer

            _Buffer = _BinaryReader.ReadBytes(CInt(Fix(_TotalBytes)))

            _FileStream.Close()

            _FileStream.Dispose()

            _BinaryReader.Close()

        Catch _Exception As Exception

        End Try

        Return _Buffer

    End Function
    Public Shared Function getIPAddress() As String
        ' Dim ipAdr As String = System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        'If String.IsNullOrEmpty(ipAdr) Then
        '    Return System.Web.HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        'Else
        '    Dim ipArray As String() = ipAdr.Split(New [Char]() {","c})
        '    Return ipArray(0)
        'End If
        Return HttpContext.Current.Request.UserHostAddress
    End Function


    Public Shared Sub exportUser(dt1 As GridView, ByVal filename As String)

        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", filename))
            HttpContext.Current.Response.ContentType = "application/ms-excel"

            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    Dim table As New Table()

                    table.Font.Name = "Arial"

                    table.Font.Size = 8
                    table.HorizontalAlign = HorizontalAlign.Center

                    Dim total As Integer = dt1.Columns.Count - 1


                    Dim rowcount As Integer = 0

                    Dim colcount As Integer = 0

                    Dim i As Integer = 0

                    ' add each of the data rows to the table

                    For Each row As GridViewRow In dt1.Rows

                        For colcount = 0 To 8
                            If rowcount = 0 Then
                                'row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9")
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: 1; border-left-color: Black;  border-right-style: solid; border-right-width: 1; border-right-color: Black; border-bottom-style: solid; border-bottom-width: 1; border-bottom-color: Black; border-top-style: solid; border-top-width: 1; border-top-color: Black;")
                                row.Cells(colcount).Font.Bold = True
                                row.Cells(colcount).ForeColor = System.Drawing.Color.White
                                row.Cells(colcount).BackColor = System.Drawing.Color.Black
                            End If
                            row.Cells(colcount).Width = 100


                            'If colcount = 10 Then row.Cells(colcount).Style.Add("", "")



                            If rowcount Mod 2 = 0 Then

                                Utility.PrepareControlForExport(row)

                                row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#D9D9D9")
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: thin; border-left-color: Black;  border-right-style: solid; border-right-width: thin; border-right-color: Black; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: Black; border-top-style: solid; border-top-width: thin; border-top-color: Black;")

                                row.Cells(colcount).ForeColor = System.Drawing.Color.Black
                            Else

                                Utility.PrepareControlForExport(row)
                                row.Cells(colcount).Attributes.Add("style", " border-left-style: solid; border-left-width: thin; border-left-color: Black;  border-right-style: solid; border-right-width: thin; border-right-color: Black; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: Black; border-top-style: solid; border-top-width: thin; border-top-color: Black;")


                                row.Cells(colcount).BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2")

                                row.Cells(colcount).ForeColor = System.Drawing.Color.Black
                            End If
                        Next



                        table.Rows.Add(row)

                        colcount = 0
                        rowcount = rowcount + 1

                    Next

                    ' render the table into the htmlwriter

                    table.RenderControl(htw)

                    ' render the htmlwriter into the response

                    HttpContext.Current.Response.Write(sw.ToString())

                    HttpContext.Current.Response.[End]()

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub LogException(ByVal LogMessage As String)
        Dim path As String = "D:\mybankStatementRepository\Log.txt"
        'Using sw As StreamWriter = File.AppendText(path)
        '    sw.WriteLine(LogMessage)

        'End Using
        '   Dim fs As FileStream = File.Create(path)
        Dim sw As New System.IO.StreamWriter(path, True, Encoding.Unicode)
        Try

            ' sw = File.AppendText(path)
            sw.WriteLine(LogMessage)
            sw.Flush()

            sw.Close()
        Catch ex As Exception
            sw.Flush()
            sw.Close()
        End Try


    End Sub

    Public Shared Function SendSMS(ByVal gsm As String, ByVal message As String, ByVal ticket As String) As String
        Return Nothing
    End Function


    Public Shared Function InsertSMS(ByVal gsm As String, ByVal message As String, ByVal ticket As String, ByVal nuban As String) As Boolean

        Dim query As String = String.Empty
        query &= "INSERT INTO [proxy].[dbo].[proxytable] ([phone] ,[sender],[text],[inserted_at] ,[nuban],[currency])  "
        query &= "VALUES ( @phone ,@sender, @text, @inserted_at,@nuban, @currency)"

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("mssqlconn_SMS").ToString)
            Using comm As New SqlCommand()
                With comm
                    .Connection = conn
                    .CommandType = CommandType.Text
                    .CommandText = query
                    .Parameters.AddWithValue("@phone", gsm)
                    .Parameters.AddWithValue("@sender", "Diamond")
                    .Parameters.AddWithValue("@text", message)
                    .Parameters.AddWithValue("@inserted_at", Now.ToString())
                    .Parameters.AddWithValue("@nuban", nuban)
                    .Parameters.AddWithValue("@currency", "NGN")
                End With
                Try
                    conn.Open()
                    If comm.ExecuteNonQuery() > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Catch ex As SqlException
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Shared Function getCountryCodeFromCountry(ByVal country As String) As String
        Dim countryCode As String = Nothing
        Select Case country.ToUpper
            Case "NIGERIA"
                countryCode = "NGA"
                ' The following is the only Case clause that evaluates to True.
            Case "GHANA"
                countryCode = "GHN"
            Case "CAMEROON" 'Cameroon
                countryCode = "CMR"
            Case "CHAD" ''Chad
                countryCode = "TCD"
            Case "KENYA"
                countryCode = "KEN"
            Case "LIBERIA"
                countryCode = "LBR"
            Case "SENEGAL"
                countryCode = "SEN"
            Case "COTE D 'IVOIRE"
                countryCode = "CIV"
            Case "CONGO DRC"
                countryCode = "COD"
            Case "BENIN"
                countryCode = "BEN"
            Case "MALI"
                countryCode = "MLI"
            Case "TANZANIA"
                countryCode = "TZA"
            Case "MOZAMBIQUE"
                countryCode = "MOZ"

        End Select
        Return countryCode
    End Function
    Public Shared Function getCountryCode2FromCountry(ByVal country As String) As String
        Dim countryCode As String = Nothing
        Select Case country.ToUpper
            Case "NIGERIA"
                countryCode = "NG"
                ' The following is the only Case clause that evaluates to True.
            Case "GHANA"
                countryCode = "GH"
            Case "CAMEROON" 'Cameroon
                countryCode = "CM"
            Case "CHAD" ''Chad
                countryCode = "TD"
            Case "KENYA"
                countryCode = "KE"
            Case "LIBERIA"
                countryCode = "LR"
            Case "SENEGAL"
                countryCode = "SN"
            Case "COTE D 'IVOIRE"
                countryCode = "CI"
            Case "CONGO DRC"
                countryCode = "CD"

            Case "BENIN"
                countryCode = "BJ"
            Case "MALI"
                countryCode = "ML"
            Case "TANZANIA"
                countryCode = "TZ"
            Case "MOZAMBIQUE"
                countryCode = "MZ"

        End Select
        Return countryCode
    End Function

    Public Shared Function getCurrencyCodeFromCountry(ByVal country As String) As String
        Dim countryCode As String = Nothing
        Select Case country.ToUpper
            Case "NIGERIA"
                countryCode = "NGN"
                ' The following is the only Case clause that evaluates to True.
            Case "GHANA"
                countryCode = "GHS"
            Case "CAMEROON" 'Cameroon
                countryCode = "XAF"
            Case "CHAD" ''Chad
                countryCode = "XAF"
            Case "KENYA"
                countryCode = "KES"
            Case "LIBERIA"
                countryCode = "LRD"
            Case "SENEGAL"
                countryCode = "XOF"
            Case "COTE D 'IVOIRE"
                countryCode = "XOF"
            Case "CONGO DRC"
                countryCode = "CDF"

            Case "BENIN"
                countryCode = "XOF"
            Case "MALI"
                countryCode = "XOF"
            Case "TANZANIA"
                countryCode = "TZS"
            Case "MOZAMBIQUE"
                countryCode = "MZM"

        End Select
        Return countryCode
    End Function

    Public Shared Function getTransactionErrorFromCode(ByVal errorCode As String) As String
        Dim errorVal As String = "Error code " & errorCode
        Select Case errorCode
            Case "06"
                errorVal = "Dormant Account"
                ' The following is the only Case clause that evaluates to True.
            Case "07"
                errorVal = "Invalid Account"
            Case "12" 'Cameroon
                errorVal = "Invalid Transaction"
            Case "26" ''Chad
                errorVal = "Duplicate Record"
            Case "30"
                errorVal = "Format Error"
            Case "57"
                errorVal = "Transation not permitted to sender"
            Case "65"
                errorVal = "Exceed withdrawal frequency"
            Case "63"
                errorVal = "Security Violation"
            Case "21"
                errorVal = "No action taken"
            Case "18"
                errorVal = "Wrong method call"
            Case "13"
                errorVal = "Invalid Amount"
            Case "03"
                errorVal = "Invalid Sender"
            Case "09"
                errorVal = "Request Processing in progress"
            Case "x03"
                errorVal = "Unable to debit customer's account for principal"
            Case "x021"
                errorVal = "Unable to submit transaction at this time"
            Case "x02"
                errorVal = "Timeout occur while connecting to bank's posting service"
            Case "x06"
                errorVal = "From Account or To Account not valid"
        End Select
        Return errorVal
    End Function
    Public Shared Function BinaryToString(binary As String) As String
        If String.IsNullOrEmpty(binary) Then
            Throw New ArgumentNullException("binary")
        End If

        If (binary.Length Mod 8) <> 0 Then
            Throw New ArgumentException("Binary string invalid (must divide by 8)", "binary")
        End If

        Dim builder As New StringBuilder()
        For i As Integer = 0 To binary.Length - 1 Step 8
            Dim section As String = binary.Substring(i, 8)
            Dim ascii As Integer = 0
            Try
                ascii = Convert.ToInt32(section, 2)
            Catch
                Throw New ArgumentException(Convert.ToString("Binary string contains invalid section: ") & section, "binary")
            End Try
            builder.Append(Convert.ToChar(ascii))
        Next
        Return builder.ToString()
    End Function

    Public Shared Function Encrypt(val As [String]) As [String]
        Dim ms As New MemoryStream()
        Dim rsp As String = ""
        Try
            Dim sharedkeyval As String = ""
            Dim sharedvectorval As String = ""
            sharedkeyval = "000000010000001000000011000001010000011100001011000011010001000100010010000100010000110100001011000001110000001000000100000010000000000100000010000000110000010100000111000010110000110100010001"
            sharedkeyval = BinaryToString(sharedkeyval)

            sharedvectorval = "0000000100000010000000110000010100000111000010110000110100010001"
            sharedvectorval = BinaryToString(sharedvectorval)
            Dim sharedkey As Byte() = System.Text.Encoding.GetEncoding("utf-8").GetBytes(sharedkeyval)
            Dim sharedvector As Byte() = System.Text.Encoding.GetEncoding("utf-8").GetBytes(sharedvectorval)

            Dim tdes As New TripleDESCryptoServiceProvider()
            Dim toEncrypt As Byte() = Encoding.UTF8.GetBytes(val)

            Dim cs As New CryptoStream(ms, tdes.CreateEncryptor(sharedkey, sharedvector), CryptoStreamMode.Write)
            cs.Write(toEncrypt, 0, toEncrypt.Length)
            cs.FlushFinalBlock()
        Catch
            'New ErrorLog("There is an issue with the xml received " + val + " Invalid xml")
            'rsp = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><IBSResponse><ResponseCode>57</ResponseCode><ResponseText>Transaction not permitted to sender</ResponseText></IBSResponse>";
            'rsp = Encrypt(rsp, Appid);
            Return rsp
        End Try
        Return Convert.ToBase64String(ms.ToArray())
    End Function
    Public Shared Function Decrypt(val As [String]) As [String]
        Dim ms As New MemoryStream()
        Dim rsp As String = ""
        Try
            Dim sharedkeyval As String = ""
            Dim sharedvectorval As String = ""

            sharedkeyval = "000000010000001000000011000001010000011100001011000011010001000100010010000100010000110100001011000001110000001000000100000010000000000100000010000000110000010100000111000010110000110100010001"
            sharedkeyval = BinaryToString(sharedkeyval)

            sharedvectorval = "0000000100000010000000110000010100000111000010110000110100010001"
            sharedvectorval = BinaryToString(sharedvectorval)

            Dim sharedkey As Byte() = System.Text.Encoding.GetEncoding("utf-8").GetBytes(sharedkeyval)
            Dim sharedvector As Byte() = System.Text.Encoding.GetEncoding("utf-8").GetBytes(sharedvectorval)

            Dim tdes As New TripleDESCryptoServiceProvider()
            Dim toDecrypt As Byte() = Convert.FromBase64String(val)

            Dim cs As New CryptoStream(ms, tdes.CreateDecryptor(sharedkey, sharedvector), CryptoStreamMode.Write)


            cs.Write(toDecrypt, 0, toDecrypt.Length)
            cs.FlushFinalBlock()
        Catch
            ' New ErrorLog("There is an issue with the xml received " + val)
            'rsp = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><IBSResponse><ResponseCode>57</ResponseCode><ResponseText>Transaction not permitted to sender</ResponseText></IBSResponse>";
            'rsp = Encrypt(rsp, Appid);
            Return rsp
        End Try
        Return Encoding.UTF8.GetString(ms.ToArray())
    End Function
    Public Shared Function jsonToDataTable(ByVal json As String) As DataTable

        Try
            Dim table As DataTable = JsonConvert.DeserializeObject(Of DataTable)(json)
            Return table
        Catch ex As Exception
            Return Nothing
        End Try


    End Function
    Public Shared Function getStatusStyle(status As String) As String
        Select Case status
            Case "pending"
                Return "class=""label label-warning arrowed arrowed-right"""
            Case "sent"
                Return "class=""label label-success arrowed-in arrowed-in-right"""
            Case "success"
                Return "class=""label label-success arrowed-in arrowed-in-right"""
            Case "e-mail"
                Return "class=""label label-success arrowed-in arrowed-in-right"""
            Case "print"
                Return "class=""label label-success arrowed-in arrowed-in-right"""
            Case "declined"
                Return "class=""label label-danger arrowed"""
            Case "deleted"
                Return "class=""label label-danger arrowed"""
            Case "ready"
                Return "class=""label label-info arrowed-right arrowed-in"""
            Case "awt.sent"
                Return "class=""label label-warning arrowed arrowed-right"""
            Case "viewed"
                Return "class=""label label-success arrowed arrowed-right sm"""
            Case "not viewed"
                Return "class=""label label-warning arrowed arrowed-right sm"""
            Case Else
                Return "class=""label arrowed"""
        End Select
    End Function
End Class
