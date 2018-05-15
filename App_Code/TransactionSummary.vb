Imports Microsoft.VisualBasic
Imports System.Data
Imports iTextSharp.text.pdf

Public Class TransactionSummary
    Private TransactionDetails As DataTable

    Public Sub New(dt As DataTable)
        TransactionDetails = dt
    End Sub

    Public Function getTranactionSummary() As PdfPTable
        Try

            Dim table As PdfPTable = New PdfPTable(4)
            table.TotalWidth = 300.0F
            table.LockedWidth = True
            table.HorizontalAlignment = 1
            Dim widths() As Single = {4.0F, 4.0F, 8.0F, 8.0F}
            table.SetWidths(widths)
            Dim cell_ As New PdfPCell(Format.getcell2_("Transaction Summary", 40))
            cell_.Colspan = 4


            table.AddCell(cell_)
            '  table.SetWidths(widths2)
            table.AddCell(Format.getcell2_("Year", 4))
            table.AddCell(Format.getcell2_("Month", 4))
            table.AddCell(Format.getcell2_("Total Debit", 4))
            table.AddCell(Format.getcell2_("Total Credit", 4))


            For i = 0 To TransactionDetails.Rows.Count - 1
                For j = 0 To 3
                    If j > 1 Then
                        If Not IsDBNull(TransactionDetails.Rows(i).Item(j)) Then
                            table.AddCell(Format.getcell(CDbl(TransactionDetails.Rows(i).Item(j)).ToString("#,##0.00"), 4))
                        Else
                            table.AddCell(Format.getcell("", 4))
                        End If
                    Else
                        If Not IsDBNull(TransactionDetails.Rows(i).Item(j)) Then
                            table.AddCell(Format.getcell(TransactionDetails.Rows(i).Item(j), 2))
                        Else
                            table.AddCell(Format.getcell("", 2))
                        End If

                    End If
                Next
            Next
            Return table

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
