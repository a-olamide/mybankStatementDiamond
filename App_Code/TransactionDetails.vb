Imports Microsoft.VisualBasic

Public Class TransactionDetails
    Public Property PTransactionDate As String
    Public Property PValueDate As String
    Public Property PNarration As String
    Public Property PCredit As String
    Public Property PDebit As String
    Public Property PBalance As String

    Public Sub New(TransactionDate As String, ValueDate As String, Narration As String, Credit As String, Debit As String, Balance As String)
        PTransactionDate = TransactionDate
        PValueDate = ValueDate
        PNarration = Narration
        PCredit = Credit
        PDebit = Debit
        PBalance = Balance

    End Sub


End Class
