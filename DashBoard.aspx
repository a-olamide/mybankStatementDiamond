<%@ Page Title="eStatements" Language="VB" MasterPageFile="~/MasterPage.master"  MaintainScrollPositionOnPostback="true"   EnableViewStateMac="false" 
AutoEventWireup="false" CodeFile="DashBoard.aspx.vb" Inherits="DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    
     <script type="text/javascript">
         window.jQuery || document.write("<script src='assets/js/jquery-2.0.3.min.js'>" + "<" + "/script>");

		</script>
           <script type="text/javascript">

               if (typeof window.JSON == 'undefined') {
                   document.write('<script src="../scripts/json2.js"><\/script>');
               }
               $(function () {
                   $('#txtFlter2').keypress(function (event) {
                       if (event.keyCode == 13) {
                           LoadActivity($("#txtFlter2").val());
                           event.preventDefault();
                       }
                   });
                   //var wage = document.getElementById("txtFlter2");
                   //wage.addEventListener("keydown", function (e) {
                   //    if (e.keyCode === 13) {  //checks whether the pressed key is "Enter"
                   //        LoadActivity($("#txtFlter2").val());
                   //    }
                   //});
                   $("#bSave").click(function () {
                     //  alert("ter");
                       LoadActivity($("#txtFlter2").val());

                   });


                   $("#aViewAll").click(function () {
                       
                       var text = $(this).text();
                       if (text == 'View All') {
                           LoadActivity('Filter-All');
                       } else {
                           LoadActivity('');
                       }
                   });
               });

               function LoadActivity(filter) {
                   // var ControlName = document.getElementById(ddlId.id);
                   
                   var obj = {};
                   obj.type_ = filter;
                   var jsonData = JSON.stringify(obj);

                   $.ajax({
                       type: "POST",
                       url: "DashBoard.aspx/FilterActivity",
                       data: jsonData,
                       contentType: "application/json; charset=UTF-8",
                       dataType: "json", // dataType is json format
                       success: function (response) {
                           document.getElementById('ContentPlaceHolder1_tbActivityBody').innerHTML = String(response.d).split("~/")[2];
                           document.getElementById('ContentPlaceHolder1_lblTop').innerHTML =String(response.d).split("~/")[0];
                           document.getElementById('ContentPlaceHolder1_lblTotal').innerHTML = String(response.d).split("~/")[1];

                           if (filter == '') {
                               $("#aViewAll").text("View All");
                           } else {
                               $("#aViewAll").text("View Top 3");
                           }
                       },
                       error: function (response) {

                       }
                   });
               }
</script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:GridView ID="acctetails" runat="server"></asp:GridView>
                                    <asp:GridView ID="transacdetail" runat="server"></asp:GridView>
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
													Recent Activity from <asp:Label ID="lblUser" runat="server"></asp:Label>
												</div>
                                         <span class="pull-right" style="margin-top:6px;padding-right:10px">
                            	  
                                                    
                                                    <input type="text" id="txtFlter2" style="width:150px;"  />
                                  	
                     <a id="bSave" class="btn btn-sm btn-purple"><i class="icon-filter"></i></a></span>

								<span style="display:inline;padding-top:10px;" class="pull-right">Filter by account no / ticket no / Status	</span>			
                                        	
												
				 		
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
                                                            <a href="javascript:void()" id="aViewAll" >View All</a></td></tr></tfoot>
													</table>
												</div>
											</div> 	</div>  
								<div id="Div1" style="padding-left:30px;padding-right:30px;width:100%">	<h3>New Request</h3>
                              <table><tr><td style="vertical-align:top">  
                                  <span  style="margin-top:0px;padding-right:10px">
                            	Account No. 	   <asp:Panel ID="panel1" runat="server" DefaultButton="bSearch">
                                  <%--<asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                                      <ContentTemplate>--%>
                                  <asp:TextBox ID="txtAccountSearch"  Width="150px" runat="server"></asp:TextBox>
                                  	
                     <asp:linkbutton ID="bSearch" runat="server" 
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" >Generate</asp:linkbutton>
                                         <%-- </ContentTemplate>
                                  </asp:UpdatePanel>--%><asp:GridView ID="GridView1" runat="server"></asp:GridView>
                                    
                                      </asp:Panel> </span> 
				 		  <br /> <br />
										<img id="imgLoad2" alt="" src="images/loadi.gif" style="display:none"/>	
                                        <div class="widget-body pull-left"  id="dv1" runat="server"  style="display:none;width:100%">
												<div class="widget-main no-padding"><table class="table table-striped table-bordered table-hover"> 
											<asp:Label ID="lbRequest" runat="server" style="display:none"></asp:Label>		 <tbody  id="tbAccount" runat="server"> </tbody></table>
													
												</div>
                                            
											</div></td><td style="vertical-align:top">  
                                            <div  class="pull-left" style="width:100%;padding-left:70px;display:none" id="dv2" runat="server"  >
                                         	<div class="widget-body">	<div class="widget-main no-padding">
												<table class="table table-striped table-bordered table-hover" > 
														<tbody id="tbCriteria"   runat="server" style="display:none">
                                                        
															<tr><td style="vertical-align:middle">Period</td>
                                                            <td style="vertical-align:middle"> 
                                                         <table>
                                                         <tr><td> <asp:TextBox  class="form-control date-picker" data-date-format="M dd, yyyy"  ID="txtStartDate" Width="120px" runat="server"></asp:TextBox></td>
                                                         <td style="vertical-align:middle">to</td><td> <asp:TextBox data-date-format="M dd, yyyy" ID="txtEndDate" class="form-control date-picker"   Width="120px" runat="server" ></asp:TextBox> </td></tr></table>  
                                                            </td> 
															</tr><tr><td colspan="2">
                                                           <label class="pull-left" >Print</label>
                                                            <input id="chkPrint" class="pull-left" style="margin-left:5px" type="checkbox" name="chkPrint" onclick="jChkPrint()" disabled="disabled" />
                                                           
                                                              <label class="pull-left" style="padding-left:20px">Send</label>
                                                              <input id="chkSend" class="pull-left"  style="margin-left:5px" type="checkbox" name="chkSend" onclick="jChkSend()" checked="checked"/>
                                                            
                                                                 <label class="pull-left" style="padding-left:20px">E-Mail</label>
                                                              <input id="chkMail" class="pull-left"  style="margin-left:5px" type="checkbox" name="chkSend" onclick="jChkMail()" disabled="disabled"/>

															         </td></tr>
                                                            	<tr id="trCountry">
																<td style="vertical-align:middle">Country</td>

																<td style="padding-right:100px">
																<asp:DropDownList ID="ddlCountry" runat="server"  Width="200px"  
                                                                  DataTextField="Name" DataValueField="ID" AutoPostBack="false"  > 
                                                                  <asp:ListItem Selected="True" Value="NG">Nigeria</asp:ListItem>
                                                                  <asp:ListItem  Value="AD">Andorra</asp:ListItem>
                                                                  <asp:ListItem Value="AE">United Arab Emirates</asp:ListItem>
                                                                  <asp:ListItem Value="AF">Afghanistan</asp:ListItem> 
                                                              </asp:DropDownList>
																</td>
                                                                 
															</tr>
                                                            <tr id="trCategory">
																<td style="vertical-align:middle">Category</td>

																<td style="padding-right:100px">
																<asp:DropDownList ID="ddlCategory" runat="server"  Width="200px"  
                                                                  DataTextField="Name" DataValueField="ID" AutoPostBack="false" onchange="HideTextBox(this)"> 
                                                                  <asp:ListItem selected="True" Value="0">Choose</asp:ListItem>
                                                                  <asp:ListItem Value="1">Commercial Bank</asp:ListItem>
                                                                  <asp:ListItem Value="2">Other Financial Institutions</asp:ListItem> 
                                                                  <asp:ListItem Value="3">Embassy</asp:ListItem> 
                                                              </asp:DropDownList>
																</td>
                                                                 
															</tr>
                                                            <tr id="trDestination">
																<td style="vertical-align:middle">Destination</td>

																<td style="padding-right:100px">
																<asp:DropDownList ID="ddlCompany" runat="server"  Width="200px"  
                                                                  DataTextField="Name" DataValueField="ID"> 
                                                                  <asp:ListItem Value="1">Diamond Bank</asp:ListItem>
                                                                  <asp:ListItem Value="2">Skye Bank</asp:ListItem> 
                                                                  <asp:ListItem Value="3">First Bank</asp:ListItem> 
                                                                  <asp:ListItem Value="4">Key Stone Bank</asp:ListItem> 
                                                                  <asp:ListItem Value="5">FCM Bank</asp:ListItem> 
                                                              </asp:DropDownList>
																</td>
                                                                 
															</tr>
                                                            	<tr id="trRole">
																<td  style="vertical-align:middle">Role</td>

																<td>
																<asp:DropDownList ID="ddlRole" runat="server"  Width="120px"  
                                                                  DataTextField="Role" DataValueField="Role"  >
                                                                  <asp:ListItem Selected="True" Value="0">Choose...</asp:ListItem>
                                                                  <asp:ListItem Value="Applicant">Applicant</asp:ListItem>
                                                                  <asp:ListItem Value="Guarantor">Guarantor</asp:ListItem>
                                                                  <asp:ListItem Value="Sponsor">Sponsor</asp:ListItem> 
                                                              </asp:DropDownList>
																</td>
                                                                 
															</tr>  
															<tr id="trAddApplicant">
																<td style="vertical-align:middle">Add applicants</td>

																<td>
                                                               <div class="row" style="padding-left:11px"> 
                                                                   <%--<input id="txtApplicants" type="text" style="Width:100px" />--%>
                                                                   <%--<asp:TextBox ID="txtApplicants"  Width="100px" runat="server"  onkeydown="return (event,keyCode!=13;)"></asp:TextBox>--%>
                                                                   <asp:TextBox ID="txtApplicants"  Width="160px" runat="server" onkeydown="return (event,keyCode!=13;)"></asp:TextBox>
                                                                   <%--<asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtApplicants" ValidationExpression="^[a-zA-Z]+$" ValidationGroup="check"></asp:RegularExpressionValidator>--%>
                                                              <a id="addapplicant" href="javascript:void()" onclick="jAddApplicant()" style="height:29px" class="btn btn-sm btn-purple" > Add <i class="icon-plus"></i> </a>
                                                                   <img id="imgAddApp" alt="" src="images/wait.gif" style="display:none"/>	
                                                               </div>
                      
																</td> 
															</tr>
															<tr id="trApplicant">
																<td class="">Applicant(s)</td> 
																<td>
                                                                <table class="pull-left">  
												<tbody ID="tbApplicants" runat="server">	 </tbody></table> 
                      
																</td> 
															</tr> 
															 
                                                            <tr id="trDebit">
																<td style="vertical-align:middle;display:none;">Debit Account</td>

																<td style="padding-right:100px;display:none;">
																<asp:DropDownList ID="ddlDebitAccount" runat="server"  Width="200px"  
                                                                  AutoPostBack="false"  > 
                                                                 
                                                              </asp:DropDownList>
																</td>
                                                                 
															</tr>
                                                            <tr id="trWaive">
																<td style="vertical-align:middle">Waive Charge</td>

																<td style="padding-right:100px">
																<input class="pull-left"  style="margin-left:5px" type="checkbox" id="chkWaive" name="chkWaive"/>
																</td>
                                                                 
															</tr>
														<tr><td style="text-align:center" colspan="2">  <asp:Linkbutton id="lbDeleteRequest" runat="server"   
                                                                  class="btn btn-sm btn-gray pull-left" > Delete Request <i class="icon-remove"></i></asp:Linkbutton> 
                                                               <a id="lbPreview"  
                                                                  class="btn btn-sm btn-purple pull-right" onclick="jSave()">
														            Preview <i class="icon-ok"></i>
													            </a> <img id="img1" alt="" src="images/loadi.gif" style="display:none"/> </td></tr>
														</tbody>
													</table>	
                                                     </div></div>
									</div></td></tr></table>
                                
                                             </div> 
                                 <%-- style="display:none"--%> 
                                     <div id="Div2" style="display:none" ><div style="padding-left:30px;padding-right:30px;width:100%;">	
                                 <h3>Confirm Request <asp:Linkbutton id="lbCancelRequest" runat="server"   
                                                                  class="btn btn-sm btn-gray pull-right" > Go Back <i class="icon-remove"></i></asp:Linkbutton> 
                                     <asp:Linkbutton id="btnEdit" runat="server" class="btn btn-sm  btn-gray pull-right">
                                    Edit <i class="icon-edit"></i>
                                                                        </asp:Linkbutton>

                                 </h3>
                                 
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
                                                            <tr><td>Cost Per Page</td>
                                                            <td style="text-align:right" id="tdPerPage">50 </td> 
															</tr>
														<tr style="display:none"><td>Cost</td>
                                                            <td style="text-align:right" id="tdBasic_"> N 1,000</td> 
															</tr>
														<tr><td>Processing Fee</td>
                                                            <td style="text-align:right" id="tdAdd_"> N 750</td> 
															</tr>
                                                            <tr><td>totalCharge</td>
                                                            <td style="text-align:right" id="tdCharge_"> N 750</td> 
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
														 <tr style="display:none"><td>Debit Account</td>
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
                <label style="display:none" id="lblmailOption" class="pull-left" for="mailOption">Also send this statement to customer's mail .</label>
                <input style="display:none" type="checkbox" id="mailOption" class="btn btn-sm pull-left" />
				<button type="button" class="btn btn-default" data-dismiss="modal" id="lbCancel2" onclick="jCancel()">
					Cancel  <i class="icon-remove"></i>
				</button>
				<a id="lbSend" class="btn btn-success"  href="javascript:void()" onclick="jSend()">
					Approve  <i class="icon-ok"></i>
				</a>
										<img id="imgLoad3" alt="" src="images/loadi.gif" style="display:none"/>	
			</div>
		</div><!-- /.modal-content -->
	</div><!-- /.modal-dialog -->
</div><!-- /.modal -->

									</div></div>  
    <div id="Div4" style="display:none;" ><div style="padding-left:30px;padding-right:30px;width:100%;">      
                                 <h3>Print Item <a id="aCloseIFrame" onclick="closeIframe()" 
                                                                  class="btn btn-sm btn-gray pull-right" >
                                                                                                            Go Back <i class="icon-remove"></i>
                                                                                                     </a> </h3>
                                
                                                                <hr style="height:1px;margin:0px"/><br />
                                                                           <div class="pull-right" style="width:100%;padding-left:70px;">
                                            <iframe id="iStatement1" src="" width="100%" height="700px"></iframe></div>
<div class="modal fade" id="Div5" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
     
</div><!-- /.modal -->
                                                              </div></div>
		<script src="js/SmartNotification.min.js" type="text/javascript"></script>
		<script type="text/javascript">
		    function jAddApplicant() {
		        //e.preventDefault();


		        var obj = {};

		        obj.value = $.trim($("[id*=txtApplicants]").val());

		        var jsonData = JSON.stringify(obj);
		        if (document.getElementById('ContentPlaceHolder1_txtApplicants').value == "") {
		            //if ($("#txtApplicants").val() == "") {
		            $.bigBox({
		                title: "Enter Name",
		                content: "Input the applicants name.",
		                color: "#CC0000",
		                timeout: 4000
		            });
		        }
		        else {
		            //$("#ContentPlaceHolder1_tbCriteria").hide();
		            //$("#ContentPlaceHolder1_dv1").hide();
		            //$("#ContentPlaceHolder1_dv2").hide();
		             $("#imgAddApp").show();

		            $.ajax({
		                type: "POST",
		                url: "DashBoard.aspx/addApplicant",
		                data: jsonData,
		                contentType: "application/json; charset=UTF-8",
		                dataType: "json", // dataType is json format
		                success: function (response) {
		                    // alert(response.d)

		                    document.getElementById('ContentPlaceHolder1_tbAccount').innerHTML = String(response.d).split("%")[0];
		                    document.getElementById('ContentPlaceHolder1_tbApplicants').innerHTML = String(response.d).split("%")[1];
		                    $("#imgAddApp").hide();
		                    //$("#ContentPlaceHolder1_tbCriteria").show();
		                    //$("#ContentPlaceHolder1_dv1").show();
		                    //$("#ContentPlaceHolder1_dv2").show();
		                    document.getElementById('ContentPlaceHolder1_txtApplicants').value = "";
		                },
		                error: function (response) {
		                    $("#imgAddApp").hide();
		                    $.bigBox({
		                        title: "Error",
		                        content: "Error adding applicants name.",
		                        color: "#CC0000",
		                        timeout: 4000
		                    });
		                }
		            });



		        }

		    }
		    function jDeleteApplicant(value) {
		        var obj = {};
		        obj.value = value;

		        var jsonData = JSON.stringify(obj);

		        $.ajax({
		            type: "POST",
		            url: "DashBoard.aspx/deleteApplicant",
		            data: jsonData,
		            contentType: "application/json; charset=UTF-8",
		            dataType: "json", // dataType is json format
		            success: function (response) {
		                document.getElementById('ContentPlaceHolder1_tbApplicants').innerHTML = String(response.d);
		            },
		            //error: OnErrorCall
		        });

		    }

		    function jChkSend() {
		        document.getElementById("chkSend").checked = true;
		        document.getElementById("chkMail").checked = false;
		        document.getElementById("chkPrint").checked = false;
		        //if (document.getElementById("chkSend").checked) {
		        document.getElementById('ContentPlaceHolder1_ddlCountry').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlCountry').disabled = false;
		        document.getElementById('ContentPlaceHolder1_ddlCategory').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlCategory').disabled = false;
		        document.getElementById('ContentPlaceHolder1_ddlCompany').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlCompany').disabled = false;
		        document.getElementById('ContentPlaceHolder1_ddlRole').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlRole').disabled = false;
		        document.getElementById("chkPrint").checked = false;
		        $("#trCountry").show();
		        $("#trCategory").show();
		        $("#trDestination").show();
		        $("#trApplicant").show();
		        $("#trAddApplicant").show();
		        $("#trRole").show();
		    }


		    function jChkPrint() {
		        document.getElementById("chkSend").checked = false;
		        document.getElementById("chkMail").checked = false;
		        document.getElementById("chkPrint").checked = true;
		        // if (document.getElementById("chkPrint").checked) {
		        document.getElementById('ContentPlaceHolder1_ddlCountry').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlCountry').disabled = true;
		        document.getElementById('ContentPlaceHolder1_ddlCategory').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlCategory').disabled = true;
		        document.getElementById('ContentPlaceHolder1_ddlCompany').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlCompany').disabled = true
		        document.getElementById('ContentPlaceHolder1_ddlRole').value = "Applicant";
		        document.getElementById('ContentPlaceHolder1_ddlRole').disabled = true;

		        $("#trCountry").hide();
		        $("#trCategory").hide();
		        $("#trDestination").hide();
		        $("#trApplicant").hide();
		        $("#trAddApplicant").hide();
		        $("#trRole").hide();
		    }
		    function jChkMail() {
		        document.getElementById("chkSend").checked = false;
		        document.getElementById("chkMail").checked = true;
		        document.getElementById("chkPrint").checked = false;
		        //document.getElementById('ContentPlaceHolder1_ddlCountry').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlCountry').disabled = true;
		        document.getElementById('ContentPlaceHolder1_ddlCategory').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlCategory').disabled = true;
		        document.getElementById('ContentPlaceHolder1_ddlCompany').value = "E-mail";
		        document.getElementById('ContentPlaceHolder1_ddlCompany').disabled = true
		        document.getElementById('ContentPlaceHolder1_ddlRole').value = 0;
		        document.getElementById('ContentPlaceHolder1_ddlRole').disabled = true;

		        $("#trCountry").hide();
		        $("#trCategory").hide();
		        $("#trDestination").hide();
		        $("#trApplicant").hide();
		        $("#trAddApplicant").hide();
		        $("#trRole").hide();
		    }

		    function jCancel() {
		        if ($get('iStatement') != null) $get('iStatement').style.display = '';
		        $("#imgLoad3").hide();
		        $("#lbSend").show();
		    }
		    function jSend() {

		        $("#lbSend").hide();
		        $("#lbCancel").hide();
		        $("#imgLoad3").show();
		        var _Print;
		        var mailOption="0";
		        if (document.getElementById("mailOption").checked) {
		            mailOption = "1";

		        }
		        if (document.getElementById("chkPrint").checked || document.getElementById('tdDestination_').innerHTML == "Print Only") {
		            _Print = "PRINT";

		        }

		        
		        else if (document.getElementById("chkMail").checked || document.getElementById('tdDestination_').innerHTML == "E-Mail") {
		            _Print = "E-Mail";

		        }
		        else {
		            _Print = "SENT";
		        }
		        //            
		        var obj = {};
		        obj.value = document.getElementById('pAccountName').innerHTML + "_" + document.getElementById('pDestination').innerHTML + "_" + _Print + "_" + mailOption + "_" + document.getElementById('tdReceiptNo_').innerHTML.split('-')[0];
		        var jsonData = JSON.stringify(obj);
		        $.ajax({
		            type: "POST",
		            url: "DashBoard.aspx/sendStatement",
		            data: jsonData,
		            contentType: "application/json; charset=UTF-8",
		            dataType: "json", // dataType is json format
		            success: function (response) {

		                //DashBoard.sendStatement(document.getElementById('pAccountName').innerHTML + "_" + document.getElementById('pDestination').innerHTML + "_" + _Print, function (res) {
		                if (String(response.d).split("%")[0] == "True") {
		                    
		                    
		                        window.open(String(response.d).split("%")[1], "_blank");
		                        window.location.replace('Dashboard.aspx');
		                   

		                    
		                }
		                else if (String(response.d).split("%")[0] == "Approval") {
		                    $.bigBox({
		                        title: "Info.",
		                        content: "Statement sent for approval",
		                        color: "Green",
		                        timeout: 4000
		                    });
		                    setTimeout(function () {
		                        window.location.replace('Dashboard.aspx');
		                    }, 3000);

		                }
		                else {
		                    $.bigBox({
		                        title: "Error.",
		                        content: String(response.d).split("%")[1],
		                        color: "Red",
		                        timeout: 4000
		                    });
		                    setTimeout(function () {
		                        window.location.replace('Dashboard.aspx');
		                    }, 4000);
		                }
		                $("#lbSend").show();
		                $("#lbCancel2").show();
		                $("#imgLoad3").hide();

		            },
		            error: function (response) {
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
		    function jSave() {
		        var obj = {};
		        obj.startDate = $.trim($("[id*=txtStartDate]").val());
		        obj.endDate = $.trim($("[id*=txtEndDate]").val());
		        if (document.getElementById("chkPrint").checked) {
		            obj.Destination = "0"
		        }
		        else if (document.getElementById("chkMail").checked) {
		            obj.Destination = "E-Mail"
		        } else {
		            obj.Destination = $.trim($("[id*=ddlCompany]").val());
		        }
		        obj.Role = $.trim($("[id*=ddlRole]").val());
		        
		        var name = document.getElementById('tdAcctName_').innerHTML;
		        obj.Name = name;
		        obj.ticketNo = document.getElementById('tdTicket__').innerHTML;
		        var address = document.getElementById('tdAddress_').innerHTML;
		        obj.Address = address;
                obj.Nuban = document.getElementById('ContentPlaceHolder1_txtAccountSearch').value;
                if (document.getElementById("chkWaive").checked) {
                    obj.waive = "1";
                } else {
                    obj.waive = "0";
                }
                obj.debitAccount =""// $.trim($("[id*=ddlDebitAccount]").val());
		        //var value = $("#txtApplicants").val()
		        var jsonData = JSON.stringify(obj);
		        $("#lbPreview").hide();
		        $("#img1").show();
		        var applicant = document.getElementById('ContentPlaceHolder1_tbApplicants').innerHTML;
		        var timestamp = Date.parse(document.getElementById('ContentPlaceHolder1_txtStartDate').value)
		        var timestamp2 = Date.parse(document.getElementById('ContentPlaceHolder1_txtEndDate').value)
		        if (isNaN(timestamp) == false && isNaN(timestamp2) == false && timestamp < timestamp2) {
		            if (document.getElementById('ContentPlaceHolder1_ddlCompany').value != 0 || document.getElementById("chkPrint").checked || document.getElementById("chkMail").checked) {
		                if (document.getElementById('ContentPlaceHolder1_ddlRole').value != 0 || document.getElementById("chkMail").checked) {
		                    if (applicant.indexOf("No applicant") > -1 && document.getElementById("chkSend").checked == true) {
		                        $.bigBox({
		                            title: "Add applicant(s)",
		                            content: "Include the name(s) of the applicant(s).",
		                            color: "#CC0000",
		                            timeout: 4000
		                        });
		                        $("#lbPreview").show();
		                        $("#img1").hide();
		                    }
		                    else {

		                        $.ajax({
		                            type: "POST",
		                            url: "DashBoard.aspx/_updateRequest",
		                            data: jsonData,
		                            contentType: "application/json; charset=UTF-8",
		                            dataType: "json", // dataType is json format
		                            success: function (res) {

		                                if (String(res.d).split("%")[0] == "False") {
		                                    $.bigBox({
		                                        title: "Error",
		                                        content: String(res.d).split("%")[1],
		                                        color: "#CC0000",
		                                        timeout: 4000
		                                    });
		                                    $("#lbPreview").show();
		                                    $("#img1").hide();
		                                }
		                                else {
		                                    $("#Div1").hide();
		                                    $("#Div2").show();
		                                    $("#Div3").hide();
		                                    if (String(res.d).split("%")[22] == "0") {
		                                        document.getElementById('btnSend').innerHTML = "Insufficient Balance <i class='icon-remove'></i>";
		                                        document.getElementById('btnSend').value = "Insufficient Balance";
		                                        if (document.getElementById("chkPrint").checked) {
		                                            document.getElementById('tdDestination_').innerHTML = "Print Only";
		                                        }
		                                        else if (document.getElementById("chkMail").checked) {
		                                            document.getElementById('tdDestination_').innerHTML = "E-Mail";
		                                        }
		                                        else {
		                                            document.getElementById('tdDestination_').innerHTML = String(res.d).split("%")[5];
		                                        }
		                                    }
		                                    else {
		                                        if (document.getElementById("chkPrint").checked) {
		                                            document.getElementById('btnSend').innerHTML = "Print <i class='icon-print'></i>";
		                                            document.getElementById('tdDestination_').innerHTML = "Print Only";
		                                        }
		                                        else if (document.getElementById("chkMail").checked) {
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
                                            document.getElementById('tdPerPage').innerHTML = String(res.d).split("%")[23];
                                            document.getElementById('tdCharge_').innerHTML = String(res.d).split("%")[24];
                                            document.getElementById('tdWaive_').innerHTML = String(res.d).split("%")[25];
                                         //   document.getElementById('tdDebitAccount_').innerHTML = String(res.d).split("%")[26];
                                            
		                                    document.getElementById('pAccountName').innerHTML = String(res.d).split("%")[3];
		                                    document.getElementById('pDestination').innerHTML = String(res.d).split("%")[5];
		                                    document.getElementById('pAccountNo').innerHTML = String(res.d).split("%")[2];
		                                    document.getElementById('pPeriod').innerHTML = String(res.d).split("%")[9];
		                                    document.getElementById('pCost').innerHTML = String(res.d).split("%")[20];
		                                    $("#lbPreview").show();
		                                    $("#img1").hide();
		                                }

		                            },
		                            error: function (response) {
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
		                else {
		                    $.bigBox({
		                        title: "Choose role",
		                        content: "Select the customer's role.",
		                        color: "#CC0000",
		                        timeout: 4000
		                    });
		                    $("#lbPreview").show();
		                    $("#img1").hide();
		                }
		            }
		            else {
		                $.bigBox({
		                    title: "Choose destination",
		                    content: "Select a valid destination.",
		                    color: "#CC0000",
		                    timeout: 4000
		                });
		                $("#lbPreview").show();
		                $("#img1").hide();
		            }
		        }
		        else {
		            $.bigBox({
		                title: "Choose valid period",
		                content: "Select valid dates, start date must be earlier than end date.",
		                color: "#CC0000",
		                timeout: 4000

		            });
		            $("#lbPreview").show();
		            $("#img1").hide();
		        }
		    }


		    function HideTextBox(ddlId) {
		        var ControlName = document.getElementById(ddlId.id);

		        var obj = {};

		        obj.country = document.getElementById('ContentPlaceHolder1_ddlCountry').value;
		        obj.value = document.getElementById('ContentPlaceHolder1_ddlCategory').value;
		        var jsonData = JSON.stringify(obj);

		        $.ajax({
		            type: "POST",
		            url: "DashBoard.aspx/LoadDestinations",
		            data: jsonData,
		            contentType: "application/json; charset=UTF-8",
		            dataType: "json", // dataType is json format
		            success: function (response) {
		                var newoptionselect = String(response.d);
		                var newOption = "<option value='" + "1" + "'>" + "test" + "</option>" + "<option value='" + "2" + "'>" + "test1" + "</option>";
		                $('#ContentPlaceHolder1_ddlCompany').empty();
		                $("#ContentPlaceHolder1_ddlCompany").append(newoptionselect);
		                newoptionselect = "";
		            },
		            error: function (response) {
		                $.bigBox({
		                    title: "Error",
		                    content: "Error populating destintion",
		                    color: "#CC0000",
		                    timeout: 4000
		                });
		            }
		        });




		    }
		    function closeIframe() {
		        $("#Div1").show();
		        $("#Div4").hide();
		        $("#Div3").show();
		    }
		    function jPending(ID) {
		        var obj2 = {};
		        obj2.value = ID.split("%")[1];
		        var jsonData2 = JSON.stringify(obj2);

		        var obj = {};
		        obj.index = "1";
		        obj.value = ID;
		        //var value = $("#txtApplicants").val()
		        var jsonData = JSON.stringify(obj);

                if (ID.split("%")[2] == "Sent" || ID.split("%")[2] == "Test") {
		            $.ajax({
		                type: "POST",
		                url: "DashBoard.aspx/Ticket",
		                data: jsonData2,
		                contentType: "application/json; charset=UTF-8",
		                dataType: "json", // dataType is json format
		                success: function (response) {
		                    // alert(response.d)
//		                    if (String(response.d) == "1") {
//		                        window.open("ticket/" + ID.split("%")[1] + ".pdf", "_blank");
//		                    } else {
//		                        window.open("receipt/" + ID.split("%")[1] + ".pdf", "_blank");
//		                        }

		                    $("#lbPreview").hide();
		                   // $("#img1").show();
		                    $("#Div1").hide();
		                    $("#Div4").show();
		                    $("#Div3").hide();
		                    if (String(response.d) == "1") { $('#iStatement1').attr('src', "ticket/" + ID.split("%")[1] + ".pdf", "_blank"); }
		                    else { $('#iStatement1').attr('src', "receipt/" + ID.split("%")[1] + ".pdf", "_blank"); }
		            

		                }
                        ,
		                error: function (response) {
		                    $.bigBox({
		                        title: "Error",
		                        content: "Error Encountered or System Timed-out",
		                        color: "#CC0000",
		                        timeout: 4000
		                    });
		                }
		            });


		        }
		        else if (ID.split("%")[2] == "Print") {
		           //  window.open("Authorization/" + ID.split("%")[1] + ".pdf", "_blank");
		           
		            $("#lbPreview").hide();
		            $("#img1").show();
		            $("#Div1").hide();
		            $("#Div4").show();
		            $("#Div3").hide();
		            $('#iStatement1').attr('src', "Authorization/" + ID.split("%")[1] + ".pdf", "_blank");



		        }
		        else if (ID.split("%")[2] == "E-Mail") {
		            window.open("Authorization/" + ID.split("%")[1] + ".pdf", "_blank");
                }
                else if (ID.split("%")[2] == "PrintS") {
                //    window.open("Authorization/" + ID.split("%")[1] + ".pdf", "_blank");
                    $("#lbPreview").hide();
                    // $("#img1").show();
                    $("#Div1").hide();
                    $("#Div4").show();
                    $("#Div3").hide();
                    $('#iStatement1').attr('src', "Authorization/" + ID.split("%")[1] + ".pdf", "_blank"); 
                }
		        else if (ID.split("%")[2] == "Ready") {
		            var obj = {};

		            obj.ticket = ID.split("%")[1];
		            //var value = $("#txtApplicants").val()
		            var jsonData = JSON.stringify(obj);
		            $("#lbPreview").hide();
		            $("#img1").show();

		            $.ajax({
		                type: "POST",
		                url: "DashBoard.aspx/_ApprovalInfo",
		                data: jsonData,
		                contentType: "application/json; charset=UTF-8",
		                dataType: "json", // dataType is json format
		                success: function OnSuccess(res) {

		                    // $('#btnEdit').attr('style', 'display:normal');
		                    $('#btnEdit').show();
		                    $("#Div1").hide();
		                    $("#Div2").show();
		                    $("#Div3").hide();
		                    if (String(res.d).split("%")[22] == "0") {
		                        document.getElementById('btnSend').innerHTML = "Insufficient Balance <i class='icon-remove'></i>";
		                        document.getElementById('btnSend').value = "Insufficient Balance";
		                        if (document.getElementById("chkPrint").checked) {
		                            document.getElementById('tdDestination_').innerHTML = "Print Only";
		                        }
		                        else if (document.getElementById("chkMail").checked) {
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
                            document.getElementById('tdPerPage').innerHTML = String(res.d).split("%")[23];
                            document.getElementById('tdWaive_').innerHTML = String(res.d).split("%")[25];
                         //   document.getElementById('tdDebitAccount_').innerHTML = String(res.d).split("%")[26];

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
		                        content: "Ready Error Encountered or System Timed-out",
		                        color: "#CC0000",
		                        timeout: 4000
		                    });
		                }
		            });
		           
		            
		        }
		        else if (ID.split("%")[2] == "Pending") {
		            document.getElementById('ContentPlaceHolder1_txtAccountSearch').value = ID.split("%")[0];
		            $("#ContentPlaceHolder1_tbCriteria").hide();
		            $("#ContentPlaceHolder1_dv1").hide();
		            $("#ContentPlaceHolder1_dv2").hide();
		            $("#imgLoad2").show();
		            document.getElementById('ContentPlaceHolder1_tbApplicants').innerHTML = "";
		            document.getElementById('ContentPlaceHolder1_txtStartDate').value = "";
		            document.getElementById('ContentPlaceHolder1_txtEndDate').value = "";
		            document.getElementById('ContentPlaceHolder1_ddlCompany').value = "";
		            document.getElementById('ContentPlaceHolder1_ddlRole').value = "";
		            // DashBoard.Pending(ID, function (res) {
		            $.ajax({
		                type: "POST",
		                url: "DashBoard.aspx/Pending",
		                data: jsonData,
		                contentType: "application/json; charset=UTF-8",
		                dataType: "json", // dataType is json format
		                success: function (response) {
		                    // alert(response.d)
		                    if (String(response.d).split("%")[0] == "False") {
		                        $("#ContentPlaceHolder1_tbCriteria").hide();
		                        $("#ContentPlaceHolder1_dv1").hide();
		                        $("#ContentPlaceHolder1_dv2").hide();
		                        document.getElementById('ContentPlaceHolder1_tbAccount').innerHTML = "";
		                    }
		                    else {
		                        $("#ContentPlaceHolder1_tbCriteria").show();
		                        $("#ContentPlaceHolder1_dv1").show();
		                        $("#ContentPlaceHolder1_dv2").show();
		                        document.getElementById('ContentPlaceHolder1_tbAccount').innerHTML = String(response.d).split("%")[1];
		                    }
		                    document.getElementById('ContentPlaceHolder1_tbApplicants').innerHTML = String(response.d).split("%")[2];
                           
		                    document.getElementById('ContentPlaceHolder1_txtStartDate').value = String(response.d).split("%")[3];
		                    document.getElementById('ContentPlaceHolder1_txtEndDate').value = String(response.d).split("%")[4];
		                    document.getElementById('ContentPlaceHolder1_ddlCompany').value = String(response.d).split("%")[5];
		                    document.getElementById('ContentPlaceHolder1_ddlRole').value = String(response.d).split("%")[6];

		                    $("#imgLoad2").hide();

		                },
		                error: function (response) {
		                    $.bigBox({
		                        title: "Error",
		                        content: "Error Encountered or System Timed-out",
		                        color: "#CC0000",
		                        timeout: 4000
		                    });
		                }
		            });
		        }
		        
		        else if (ID.split("%")[2] == "Make Payment") {
		            var obj = {};

		            obj.ticket = ID.split("%")[1];
		            //var value = $("#txtApplicants").val()
		            var jsonData = JSON.stringify(obj);
		            $("#lbPreview").hide();
		            $("#img1").show();

		            $.ajax({
		                type: "POST",
		                url: "DashBoard.aspx/_MakePayment",
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
		                            content: "Transaction Failed " + String(res.d).split("%")[1],
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

		        }
		        else if (ID.split("%")[2] == "Declined") {
		            var obj = {};
		            obj.ticket = ID.split("%")[1];
		            var jsonData = JSON.stringify(obj);
		            $.ajax({
		                type: "POST",
		                url: "DashBoard.aspx/ViewComment",
		                data: jsonData,
		                contentType: "application/json; charset=UTF-8",
		                dataType: "json", // dataType is json format
		                success: function (response) {
		                    $.bigBox({
		                        title: "Transaction declined",
		                        content: String(response.d),
		                        color: "red",
		                        timeout: 4000
		                    });
		                }
                        ,
		                error: function (response) {
		                    $.bigBox({
		                        title: "Error",
		                        content: "Error Encountered or System Timed-out",
		                        color: "#CC0000",
		                        timeout: 4000
		                    });
		                }
		            });
		        }
                else if (ID.split("%")[2] == "Awt. Sent") {
           sweetAlert({
               title: "Awaiting Approval?",
               text: "Do you want to recall this request?",
               type: "warning",
               showCancelButton: true,
               confirmButtonColor: "#00ff00",
               confirmButtonText: "Yes, recall!",
               cancelButtonText: "No",
               inputType: null,
               inputValue: null,
               closeOnConfirm: true,
               closeOnCancel: true
                    
           },
        function (isConfirm) {
            if (isConfirm) {
                var obj = {};
                obj.id = ID.split("%")[1];
                
                var jsonData = JSON.stringify(obj);
                $.ajax({
                    type: "POST",
                    url: "DashBoard.aspx/Recall",
                    data: jsonData,
                    contentType: "application/json; charset=UTF-8",
                    dataType: "json", // dataType is json format
                    success: function (response) {
                        if (String(response.d) == "1") {
                            LoadActivity('')
                            //$.each($("input[name=chk]:checked").closest("td").siblings("td"),
                            //   function () {
                                  
                            //       if ($(this).text() == "Pending") {
                            //           $(this).text("Paid");
                            //           $(this).closest('td').next('td').find("input[name=chk]:checked").attr('disabled', true);
                            //       }
                            //   });
                        
                        
                            swal("Thanks!", "Request recalled!", "success");
                        } else {
                            swal("Sorry!", "Could not recall request", "error");
                        }
                    },
                    error: function (response) {

                    }

                });
            } else {
                //$.each($("input[name=chk]:checked").closest("td").siblings("td"),
                //               function () {
                          
                //                   $(this).closest("td").next('td').find("input[name=chk]:checked").attr('checked', false);
                               
                //               });

                swal("Ok!", "Your request is pending approval", "error");
            }
        });

            
       }

		        else {
		            $.bigBox({
		                title: "Info",
		                content: "No action.",
		                color: "green",
		                timeout: 1000
		            });
		        }
		    }

		</script>
  <%--   
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
     </asp:GridView>--%>
     <script language="javascript" type="text/javascript" defer="defer">
         function HideFrame() {
             if (document.getElementById("chkPrint").checked || document.getElementById('chkMail').checked
                 || document.getElementById('tdDestination_').innerHTML == "Print Only"
                 || document.getElementById('tdDestination_').innerHTML == "E-Mail") {
                 $("#lblmailOption").hide();
                 $("#mailOption").hide();

             }
             else {

                 var obj = {};
                 obj.value = document.getElementById('tdReceiptNo_').innerHTML;

                 var jsonData = JSON.stringify(obj);

                 $.ajax({
                     type: "POST",
                     url: "DashBoard.aspx/mailOption",
                     data: jsonData,
                     contentType: "application/json; charset=UTF-8",
                     dataType: "json", // dataType is json format
                     success: function (response) {
                         if (String(response.d) == "1") {
                             document.getElementById('lblmailOption').innerHTML = "Copy of the Statement may be required, Send  "
                             document.getElementById('mailOption').checked = true
                             document.getElementById('mailOption').disabled = true
                         } else {
                             //$("#lblmailOption").hide();
                             //$("#mailOption").hide();
                         }

                     },
                     //error: OnErrorCall
                 });


             }

             
                
             
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

