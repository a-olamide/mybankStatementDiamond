Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.OracleClient
Imports System.Diagnostics

Public Class DALOracle
    Public Shared Function GetAccountDetails(ByVal myNuban As String) As DataTable

        Dim dr As OracleDataReader

        '  Dim oErrorLog As New ErrorLog
        Dim retVal As New List(Of String)


        Dim cnstring As String = System.Configuration.ConfigurationManager.ConnectionStrings("oracleConn").ConnectionString

        Dim conn As New OracleConnection()
        conn.ConnectionString = cnstring


        '    Dim sql As String = "Select b.cust_ac_no account_number, b.cust_ac_no nuban_ac_no, b.alt_ac_no old_ac_no,a.trn_dt, " _
        '+ "a.value_dt,ROUND(decode(a.drcr_ind,'D',decode(ac_ccy,get_bank_local_lcy,lcy_amount,fcy_amount)),2) paid_out, " _
        '+ "ROUND(decode(a.drcr_ind,'C',decode(ac_ccy,get_bank_local_lcy,lcy_amount,fcy_amount)),2) paid_in " _
        '+ "from acvw_all_ac_entries a,sttm_cust_account b where a.ac_no = b.cust_ac_no and b.cust_ac_no= '" + myNuban + "'" _
        '+ " and trn_dt between '" + myStartDate + "' and '" + myEndDate + "'"

        Dim sql As String = "Select cust_ac_no account_number, ac_desc account_holder_name, " _
+ "decode(b.account_type, 'S', 'SAVINGS', 'CURRENT') acct_type, " _
+ "decode(h.customer_type, 'I', 'INDIVIDUAL', 'CORPORATE') account_category, LCY_CURR_BALANCE, " _
+ "lcy_curr_balance avl_balance_in_naira, cust_ac_no nuban_acc_no, b.address1 || ' '|| b.address2 || ' '|| b.address3 address " _
+ "from sttm_cust_account b, sttm_customer h where cust_no = customer_no and b.cust_ac_no= '" + myNuban + "'"

        Dim ds As New DataSet
        Try

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim adapter As New OracleDataAdapter()
            Dim cmd As New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 5000
            adapter.SelectCommand = cmd

            adapter.Fill(ds)

            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
           
            Return ds.Tables(0)
        Catch ex As Exception
            Dim str As String = ex.Message()
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try



    End Function
    Public Shared Function GetTransactionDetails(ByVal myNuban As String, ByVal myStartDate As String, ByVal myEndDate As String) As DataTable

        Dim dr As OracleDataReader

        '  Dim oErrorLog As New ErrorLog
        Dim retVal As New List(Of String)
        myStartDate = Convert.ToDateTime(myStartDate).ToString("dd-MMM-yyyy")
        myEndDate = Convert.ToDateTime(myEndDate).ToString("dd-MMM-yyyy")
        Dim cnstring As String = System.Configuration.ConfigurationManager.ConnectionStrings("oracleConn").ConnectionString

        Dim conn As New OracleConnection()
        conn.ConnectionString = cnstring


        Dim sql As String = "Select a.trn_dt, a.value_dt,bouser.get_stmt_acct_ecobank_new1(a.trn_ref_no, a.event_sr_no, a.module,a.ac_entry_sr_no, a.ac_no ) narration," _
+ "nvl(ROUND(decode(a.drcr_ind,'D',decode(ac_ccy,get_bank_local_lcy,lcy_amount,fcy_amount)),2),0) paid_out," _
 + "nvl(ROUND(decode(a.drcr_ind,'C',decode(ac_ccy,get_bank_local_lcy,lcy_amount,fcy_amount)),2),0) paid_in, '1000.00' " _
    + "from acvw_all_ac_entries a,sttm_cust_account b where a.ac_no = b.cust_ac_no and b.cust_ac_no= '" + myNuban + "'" _
    + " and trn_dt between '" + myStartDate + "' and '" + myEndDate + "'"



        Dim ds As New DataSet
        Try

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim adapter As New OracleDataAdapter()
            Dim cmd As New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 5000
            adapter.SelectCommand = cmd

            adapter.Fill(ds)

            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            Return ds.Tables(0)
        Catch ex As Exception
           Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try



    End Function


    Public Shared Function GetSignatories(ByVal myNuban As String) As DataTable

        
       
        Dim cnstring As String = System.Configuration.ConfigurationManager.ConnectionStrings("oracleConn").ConnectionString

        Dim conn As New OracleConnection()
        conn.ConnectionString = cnstring


        Dim sql As String = "   select cust_ac_no Nuban,initcap(cif_sig_name) cif_sig_name,field_val_10 BVN " _
+ "from fccngn.svtm_cif_sig_master a,fccngn.sttm_cust_account b,fccngn.CSTM_FUNCTION_USERDEF_FIELDS where function_id='STDCIF' " _
+ "and cif_id=cust_no and substr(rec_key,1,8)=cust_no and cust_ac_no= '" + myNuban + "'"

        Dim ds As New DataSet
        Try

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim adapter As New OracleDataAdapter()
            Dim cmd As New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 5000
            adapter.SelectCommand = cmd

            adapter.Fill(ds)

            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

        Catch ex As Exception
            Try

                Return ds.Tables(0)
            Catch ex1 As Exception

                Return ds.Tables(0)
            End Try
        End Try

        Return ds.Tables(0)

    End Function


 
End Class
