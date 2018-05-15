<%@ Page Title="Approval" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Approval.aspx.vb" Inherits="Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="page-header" style="padding-top:14px;padding-left:7px">
	 <h1 style="font-size: 15px;" >
								eStatements<small style="font-size: 14px; font-weight:inherit ">
									<i class="icon-double-angle-right"></i>
                                      Preview and send bank statements 
								</small>
							</h1> 	</div>	
                      
                            	<div style="padding-left:30px;padding-right:30px;width:100%" id="Div3">	
                                    <div class="widget-header">
												<div class="widget-title bigger lighter">

													<i class="ace-icon fa fa-table"></i>
													Recent Activity from <asp:Label ID="lblUser" runat="server">Atinuke Daramola</asp:Label>
												</div>

												 <span class="pull-right" style="margin-top:6px;padding-right:10px">
                            	Filter by account no. or ticket no.	  
                                                      <asp:Panel ID="panel2" runat="server" DefaultButton="bSave"	>
                                                     <asp:TextBox ID="txtAccountNo"  Width="150px" runat="server"></asp:TextBox>
                                  	
                     <asp:linkbutton ID="bSave" runat="server" 
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" ><i class="icon-filter"></i></asp:linkbutton>

                                    </asp:Panel> </span> 
												
				 		
											</div>
											<div class="widget-body" style="width:100%">
												<div class="widget-main no-padding">
													<table class="table table-striped table-bordered table-hover"> 
														<thead class="thin-border-bottom" id="tbActivityHead" runat="server">
															<tr>
																<th>
																	<i class="ace-icon fa fa-user"></i>
																	Name
																</th>
																<th>
																	Period
																</th>
																<th >Account</th> 
																<th >Destination</th>
																<th >Ticket</th>
																<th >Timestamp</th>
																<th >Status</th>
                                                                
															</tr>
														</thead>
                                                        <tbody  id="tbActivityBody" runat="server">
														 	</tbody><tfoot  id="tbActivityFoot" runat="server"><tr>
                                                            <td colspan="7" style="text-align:right;font-style:italic">Displaying top 
                                                            <asp:Label ID="lblTop" runat="server">3</asp:Label> of <asp:Label ID="lblTotal" runat="server">50</asp:Label> | 
                                                            <a href="javascript:void()" id="aViewAll" runat="server" >View all</a></td></tr></tfoot>
													</table>
												</div>
											</div> 	</div>  
								 
                                 <%-- style="display:none"--%> 
                                     <div id="Div2" style="display:none" ><div style="padding-left:30px;padding-right:30px;width:100%;">	
                                 <h3>Confirm Request <asp:Linkbutton id="lbCancelRequest" runat="server"   
                                                                  class="btn btn-sm btn-gray pull-right" > Go Back <i class="icon-remove"></i></asp:Linkbutton> </h3>
                                 
                                                                <hr style="height:1px;margin:0px"/><br />
											<div class="widget-body pull-left" style="width:30%">
												<div class="widget-main no-padding">
													<table class="table table-striped table-bordered table-hover"> 
														<tbody>
                                                        	<tr>
																<td >Account No:</td>

																<td id="tdAccountNo_">
																  
																</td>
                                                                 
															</tr>
                                                        	<tr>
																<td >Account Name:</td>

																<td id="tdAccountName_">
																  
																</td>
                                                                 
															</tr>
														<tr>
																<td class="">Ticket No:</td>

																<td id="tdReceiptNo_">
																  
																</td>
                                                                 
															</tr>
                                                            	<tr>
																<td class="">Destination</td>

																<td id="tdDestination_">
																  
																</td>
                                                                 
															</tr>

															<tr>
																<td class="">Applicant(s)</td>

																<td id="tdApplicants_"> 
																</td> 
															</tr>

															<tr>
																<td  >Role</td>
                                                    <td id="tdRole_" >Applicant/Sponsor/Guarantor</td>

																<td id="tdRoleName_" style="display:none">
																	Wallz & Queen Limited
																</td> 
															</tr>
                                                             
															<tr><td class="">Period</td>
                                                            <td id="tdPeriod_">1-Jan-2014 to 31-Mar-2014</td> 
															</tr>
														<tr><td class="">Type</td>
                                                            <td id="tdType_">Current</td> 
															</tr>
														<tr><td class="">Category</td>
                                                            <td id="tdCategory_">Corporate</td> 
															</tr>
														<tr><td class="">Signatories</td>
                                                            <td id="tdSignatories_">Yemisi Oyewole<br />Kemi Oyewole</td> 
															</tr>
														<tr><td>Dr Turnover</td>
                                                            <td id="tdDR_" style="text-align:right"> N 3,000,000,000</td> 
															</tr>
														<tr><td>Cr Turnover</td>
                                                            <td id="tdCR_" style="text-align:right"> N 3,000,000,000</td> 
															</tr>
														<tr><td>Book balance</td>
                                                            <td id="tdClrBal_" style="text-align:right"> N 3,000,000,000</td> 
															</tr>
														<tr><td>Available balance</td>
                                                            <td id="tdAvBal_" style="text-align:right"> N 3,000,000,000</td> 
															</tr>
														<tr><td>Pages</td>
                                                            <td style="text-align:right" id="tdPage_"> 25</td> 
															</tr>
														<tr style="display:none"><td>Cost</td>
                                                            <td style="text-align:right" id="tdBasic_"> N 1,000</td> 
															</tr>
														<tr style="display:none"><td>Processing Fee</td>
                                                            <td style="text-align:right" id="tdAdd_"> N 750</td> 
															</tr>
                                                            <tr><td>VAT (5%)</td>
                                                            <td style="text-align:right" id="tdVat_"> </td> 
															</tr>
														<tr><td>Total Cost</td>
                                                            <td style="text-align:right" id="tdTotal_"> </td> 
															</tr>
														 <tr><td>Waive Charge?</td>
                                                            <td style="text-align:right" id="tdWaive_"> No </td> 
															</tr>
                                                            <tr style="display:none;"><td>Debit Account</td>
                                                            <td style="text-align:right" id="tdDebitAccount_">  </td> 
															</tr>
                                                            <tr><td colspan="2"><asp:LinkButton id="lbCancel" CausesValidation="false" runat="server"
                                                                  CssClass="btn btn-sm   pull-left"> Cancel <i class="icon-remove"></i></asp:LinkButton><button id="btnSend"
                                                                  class="btn btn-sm  btn-purple pull-right" data-toggle="modal" data-target="#myModal" onclick="HideFrame();">
														            Send Statement <i class="icon-ok"></i>
													            </button>
					</td></tr>
														</tbody>
													</table>
												</div>
											</div><div class="pull-right" style="width:70%;padding-left:70px;"> 
                                            <iframe id="iStatement" src="statement/Doc1.pdf?rel=0&amp;wmode=transparent" width="100%" height="700px"></iframe></div> 
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">
					&times;
				</button>
				<h4 class="modal-title" id="myModalLabel">Confirmation</h4>
			</div>
			<div class="modal-body">
             
                        <label id="pAccountName"></label> is sending a bank statement to <label id="pDestination"></label><br /><br />
                        Account No: <label id="pAccountNo"></label> <br />
                        Period: <label id="pPeriod"></label><br />
                        <br />Transaction costs  <label id="pCost"></label><br /> 
						 
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-danger" id="lbCancel2" onclick="jCancel()">
					Decline  <i class="icon-remove"></i>
				</button>
				<a id="lbSend" class="btn btn-success"  href="javascript:void()" onclick="jSend()">
					Approve  <i class="icon-ok"></i>
				</a>
                <div style="width: 50%; margin: 0 auto;">
                    <label class="pull-left" style="display:none;" id="lbComment" for="comment"> Make Comment: </label>
                <textarea name="comment" style="display:none;" title="Give reason for declining" id="txtcomment" rows="5"></textarea>
                <a style="display:none; vertical-align:bottom;" id="acomment" class="btn btn-small btn-danger"  href="javascript:void()" onclick="jComment()">
				  Decline<i class="icon-remove"></i>
				</a>
										<img id="imgLoad3" alt="" src="images/loadi.gif" style="display:none"/>	
                    </div>
			</div>
		</div><!-- /.modal-content -->
	</div><!-- /.modal-dialog -->
</div><!-- /.modal -->

									</div></div>  
		<script src="js/SmartNotification.min.js" type="text/javascript"></script>
		<script type="text/javascript">		    
		    function jCancel() {
		        $("#lbCancel2").hide();
		        $("#lbSend").hide();
		        $("#txtcomment").show();
		        $("#acomment").show();
		        $("#lbComment").show();
		        

		    }
		    function jComment() {
		        var _Print;
		        if (document.getElementById('tdDestination_').innerHTML == "Print Only") {
		            _Print = "PRINT";

		        }
		        else {
		            _Print = "SENT";
		        }
		        var obj = {};
		        obj.value = document.getElementById('tdReceiptNo_').innerHTML + "~/" + document.getElementById('pDestination').innerHTML + "~/" + _Print + "~/" + document.getElementById('txtcomment').value;;
		        var jsonData = JSON.stringify(obj);
		        $.ajax({
		            type: "POST",
		            url: "Approval.aspx/declineStatement",
		            data: jsonData,
		            contentType: "application/json; charset=utf-8",
		            dataType: "json", // dataType is json format
		            success: function OnSuccess(response) {
		                if (String(response.d) == "1") {
		                    
		                    $.bigBox({
		                        title: "Error",
		                        content: "Transaction declined",
		                        color: "#CC0000",
		                        timeout: 4000
		                    });
		                    setTimeout(function () { window.location.replace('Approval.aspx'); }, 2000);
		                }
		            },
		            error: function OnErrorCall(response) {
		                $.bigBox({
		                    title: "Error",
		                    content: "Error Encountered or System Timed-out",
		                    color: "#CC0000",
		                    timeout: 4000
		                });
		            }
		        });



		        if ($get('iStatement') != null) $get('iStatement').style.display = '';
		        $("#imgLoad3").hide();
		       // $("#lbSend").show();
		    }
		    function jSend() {

		        $("#lbSend").hide();
		        $("#lbCancel").hide();
		        $("#imgLoad3").show();
		        var _Print;
		        if (document.getElementById('tdDestination_').innerHTML == "Print Only") {
		            _Print = "PRINT";

		        }
		        else if (document.getElementById('tdDestination_').innerHTML == "E-Mail") {
		            _Print = "E-Mail";

		        }
		        else {
		            _Print = "SENT";
		        }
		        //            
		        var obj = {};
		        obj.value = document.getElementById('pAccountName').innerHTML + "_" + document.getElementById('pDestination').innerHTML + "_" + _Print + "_" + document.getElementById('tdReceiptNo_').innerHTML.split('-')[0];
		        var jsonData = JSON.stringify(obj);
		        $.ajax({
		            type: "POST",
		            url: "Approval.aspx/sendStatement",
		            data: jsonData,
		            contentType: "application/json; charset=utf-8",
		            dataType: "json", // dataType is json format
		            success: function OnSuccess(response) {

		                //DashBoard.sendStatement(document.getElementById('pAccountName').innerHTML + "_" + document.getElementById('pDestination').innerHTML + "_" + _Print, function (res) {
		                if (String(response.d).split("%")[0] == "True") {
		                    $.bigBox({
		                        title: "Info.",
		                        content: "Statement sent succesfully",
		                        color: "Green",
		                        timeout: 2000
		                    });
		                    setTimeout(function () {
		                       // window.open(String(response.d).split("%")[1], "_blank");
		                        window.location.replace('Approval.aspx');
		                    }, 2000);

		                   
		                }
		                else if (String(response.d).split("%")[0] == "Approval") {
		                    window.location.replace('Approval.aspx');

		                }
		                else {
		                    $.bigBox({
		                        title: "Error.",
		                        content: String(response.d).split("%")[1],
		                        color: "Red",
		                        timeout: 4000
		                    });
		                    setTimeout(function () {
		                        window.location.replace('Approval.aspx');
		                    }, 7000);
		                }
		                $("#lbSend").show();
		                $("#lbCancel2").show();
		                $("#imgLoad3").hide();

		            },
		            error: function OnErrorCall(response) {
		                $.bigBox({
		                    title: "Error",
		                    content: "Error Encountered or System Timed-out",
		                    color: "#CC0000",
		                    timeout: 4000
		                });
		            }
		        });
		        
		       

		    }
		    //$("#lbPreview").on("click", function (e) {
		    function jPending(ID) {
		    if (ID.split("%")[2] == "Make Payment") {
		        var obj = {};

		        obj.ticket = ID.split("%")[1];
		        //var value = $("#txtApplicants").val()
		        var jsonData = JSON.stringify(obj);
		        $("#lbPreview").hide();
		        $("#img1").show();

		        $.ajax({
		            type: "POST",
		            url: "Approval.aspx/_MakePayment",
		            data: jsonData,
		            contentType: "application/json; charset=UTF-8",
		            dataType: "json", // dataType is json format
		            success: function OnSuccess(res) {

		                if (String(res.d).split("%")[0] == "True") {
		                    window.open(String(res.d).split("%")[1], "_blank");
		                    window.location.replace('Dashboard.aspx');
		                }
		                else {
		                    $.bigBox({
		                        title: "Error",
		                        content: "Transaction Failed",
		                        color: "#CC0000",
		                        timeout: 4000
		                    });
		                }
		                    

		            },
		            error: function OnErrorCall(response) {
		                $.bigBox({
		                    title: "Error",
		                    content: "Ready Error Encountered or System Timed-out",
		                    color: "#CC0000",
		                    timeout: 4000
		                });
		            }
		        });

		    } else {
		        var obj = {};
		        
		        obj.ticket = ID;
		        //var value = $("#txtApplicants").val()
		        var jsonData = JSON.stringify(obj);
		        $("#lbPreview").hide();
		        $("#img1").show();
		       
		                        $.ajax({
		                            type: "POST",
		                            url: "Approval.aspx/_ApprovalInfo",
		                            data: jsonData,
		                            contentType: "application/json; charset=utf-8",
		                            dataType: "json", // dataType is json format
		                            success: function OnSuccess(res) {

		                                $("#Div1").hide();
		                                $("#Div2").show();
		                                $("#Div3").hide();
		                                if (String(res.d).split("%")[22] == "0") {
		                                    document.getElementById('btnSend').innerHTML = "Insufficient Balance <i class='icon-remove'></i>";
		                                    document.getElementById('btnSend').value = "Insufficient Balance";
		                                    if (String(res.d).split("%")[5] == "Print Only") {
		                                        document.getElementById('tdDestination_').innerHTML = "Print Only";
		                                    }
		                                    else if (String(res.d).split("%")[5] == "E-Mail" || String(res.d).split("%")[5] == "E-mail") {
		                                        document.getElementById('tdDestination_').innerHTML = "E-Mail";
		                                    }
		                                    else {
		                                        document.getElementById('tdDestination_').innerHTML = String(res.d).split("%")[5];
		                                    }
		                                }
		                                else {
		                                    if (String(res.d).split("%")[5] == "Print Only") {
		                                        document.getElementById('btnSend').innerHTML = "Print <i class='icon-print'></i>";
		                                        document.getElementById('tdDestination_').innerHTML = "Print Only";
		                                    }
		                                    else if (String(res.d).split("%")[5] == "E-Mail" || String(res.d).split("%")[5] == "E-mail") {
		                                        document.getElementById('btnSend').innerHTML = "Send as Mail <i class='icon-envelope-alt'></i>";
		                                        document.getElementById('tdDestination_').innerHTML = "E-Mail";
		                                    }
		                                    else {
		                                        document.getElementById('btnSend').innerHTML = "Send Statement <i class='icon-ok'></i>";
		                                        document.getElementById('tdDestination_').innerHTML = String(res.d).split("%")[5];
		                                    }
		                                }
		                                $('#iStatement').attr('src', String(res.d).split("%")[1] + "?rel=0&amp;wmode=transparent");
		                                document.getElementById('tdAccountNo_').innerHTML = String(res.d).split("%")[2];
		                                document.getElementById('tdAccountName_').innerHTML = String(res.d).split("%")[3];
		                                document.getElementById('tdReceiptNo_').innerHTML = String(res.d).split("%")[4];
		                                document.getElementById('tdApplicants_').innerHTML = String(res.d).split("%")[6];
		                                document.getElementById('tdRole_').innerHTML = String(res.d).split("%")[7];
		                                document.getElementById('tdRoleName_').innerHTML = String(res.d).split("%")[8];
		                                document.getElementById('tdPeriod_').innerHTML = String(res.d).split("%")[9];
		                                document.getElementById('tdType_').innerHTML = String(res.d).split("%")[12];
		                                document.getElementById('tdCategory_').innerHTML = String(res.d).split("%")[11];
		                                document.getElementById('tdSignatories_').innerHTML = String(res.d).split("%")[10];
		                                document.getElementById('tdDR_').innerHTML = String(res.d).split("%")[13];
		                                document.getElementById('tdCR_').innerHTML = String(res.d).split("%")[14];
		                                document.getElementById('tdClrBal_').innerHTML = String(res.d).split("%")[15];
		                                document.getElementById('tdAvBal_').innerHTML = String(res.d).split("%")[16];
		                                document.getElementById('tdPage_').innerHTML = String(res.d).split("%")[17];
		                                document.getElementById('tdBasic_').innerHTML = String(res.d).split("%")[18];
		                                document.getElementById('tdAdd_').innerHTML = String(res.d).split("%")[19];
		                                document.getElementById('tdVat_').innerHTML = String(res.d).split("%")[21];
                                        document.getElementById('tdTotal_').innerHTML = String(res.d).split("%")[20];
                                        document.getElementById('tdWaive_').innerHTML = String(res.d).split("%")[23];
                                       // document.getElementById('tdDebitAccount_').innerHTML = String(res.d).split("%")[24];

		                                document.getElementById('pAccountName').innerHTML = String(res.d).split("%")[3];
		                                document.getElementById('pDestination').innerHTML = String(res.d).split("%")[5];
		                                document.getElementById('pAccountNo').innerHTML = String(res.d).split("%")[2];
		                                document.getElementById('pPeriod').innerHTML = String(res.d).split("%")[9];
		                                document.getElementById('pCost').innerHTML = String(res.d).split("%")[20];
		                                $("#lbPreview").show();
		                                $("#img1").hide();


		                            },
		                            error: function OnErrorCall(response) {
		                                $.bigBox({
		                                    title: "Error",
		                                    content: "Error Encountered or System Timed-out",
		                                    color: "#CC0000",
		                                    timeout: 4000
		                                });
		                            }
		                        });
		                        
		                        
		                    }
		              
            }

		</script>
  <%--   
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
     </asp:GridView>--%>
     <script language="javascript" type="text/javascript" defer="defer">
         function HideFrame() {
             $("#lbCancel2").show();
             $("#lbSend").show();
             $("#txtcomment").hide();
             $("#acomment").hide();
             $("#lbComment").hide();
             if (window.ActiveXObject || "ActiveXObject" in window || navigator.userAgent.indexOf("MSIE") != -1) {
                 if ($get('iStatement') != null) $get('iStatement').style.display = 'none';
             }
             else {
                 if ($get('iStatement') != null) $get('iStatement').style.display = '';
             }
             if (document.getElementById('btnSend').value == "Insufficient Balance") {
                 $("#lbSend").hide();
                 //$get('lbSend').style.display = 'none';
                 swal({ title: 'Your balance is too low to complete this transaction', text: 'i will close in 2 seconds...', timer: 3000, showConfirmButton: false })
             }
         }
         function ShowFrame() {
             if ($get('iStatement') != null) $get('iStatement').style.display = '';
         } </script>
</asp:Content>

