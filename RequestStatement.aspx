<%@ Page Title="Request Statement" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RequestStatement.aspx.vb" Inherits="RequestStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link rel="stylesheet" href="assets/css/font-awesome.min.css" /> 
	    
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css">
  <link rel="stylesheet" href="assets/telInput/build/css/intlTelInput.css">
    <script type="text/javascript">
        window.jQuery || document.write("<script src='assets/js/jquery-2.0.3.min.js'>" + "<" + "/script>");

		</script>
    <script type="text/javascript" src="assets/telInput/build/js/intlTelInput.js"></script>

    <script type="text/javascript">
        if (typeof window.JSON == 'undefined') {
            document.write('<script src="../scripts/json2.js"><\/script>');
        }
        $(function () {
            //   $("#alert").hide();
            $("div .alert").hide();
            LoadRequest('');

            $("[data-hide]").on("click", function () {
                $(this).closest("." + $(this).attr("data-hide")).hide();
            });

            $("#lnkSearch2").click(function () {

                LoadRequest($("#txtFlter2").val());

            });

            $("#aViewAll2").click(function () {
                var text = $(this).text();
                if (text == 'View All') {
                    LoadRequest('Filter-All');
                } else {
                    LoadRequest('');
                }
            });
        });



        function LoadRequest(filter) {
            // var ControlName = document.getElementById(ddlId.id);

            var obj = {};
            obj.filter = filter;
            var jsonData = JSON.stringify(obj);

            $.ajax({
                type: "POST",
                url: "RequestStatement.aspx/FilterActivity",
                data: jsonData,
                contentType: "application/json; charset=UTF-8",
                dataType: "json", // dataType is json format
                success: function (response) {
                    document.getElementById('tbRequest').innerHTML = String(response.d).split("~/")[2];
                    $("#lblTop").text(String(response.d).split("~/")[0]);
                    $("#lblTotal").text(String(response.d).split("~/")[1]);

                    if (filter == '') {
                        $("#aViewAll2").text("View All");
                    } else {
                        $("#aViewAll2").text("View Top 3");
                    }
                },
                error: function (response) {

                }
            });
        }
</script>
    <!-- Plugin CSS -->
    <link rel="stylesheet" href="css/animate.min.css" type="text/css">

    <!-- Custom CSS -->
    <link rel="stylesheet" href="css/creative.css" type="text/css">





    <style type="text/css">

        table {
    border-collapse: collapse;
}

td {
    padding-top: .5em;
    padding-bottom: .5em;
}
.clickable{
    cursor: pointer;   
}

.panel-heading span {
	margin-top: -20px;
	font-size: 15px;
}
    </style>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#demo").intlTelInput({
                    numberType: "MOBILE",
                    onlyCountries: ["ng", "bj", "cm", "td", "ci", "gh", "ke"],
                    preferredCountries: ["ng", "gh"],
                    utilsScript: "assets/telInput/build/js/utils.js"
                });

                var counter = 2;

                $("#addButton").on('click', function () {
                    if (counter > 10) {
                        alert("Only 10 textboxes allow");
                        return false;
                    }
                    var newTextBoxDiv = $(document.createElement('div'))
                         .attr("id", 'TextBoxDiv' + counter);

                    newTextBoxDiv.after().html('<br /><input type="text" class="txtApp" name="textbox' + counter +
                          '" id="textbox' + counter + '" value="" style="width:150px" >');

                    newTextBoxDiv.appendTo("#TextBoxesGroup");
                    counter++;
                });

                $("#removeButton").on('click', function () {
                    if (counter == 0) {
                        alert("No more textbox to remove");
                        return false;
                    }
                    counter--;
                    $("#TextBoxDiv" + counter).remove();
                });

            });

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="page-header" style="padding-top:14px;padding-left:7px">
	 <h1 style="font-size: 15px;" >
								eStatements<small style="font-size: 14px; font-weight:inherit ">
									<i class="icon-double-angle-right"></i>
                                      Request bank statements 
								</small>
							</h1> 	</div>	
       
          
        <div style="padding-left:30px;padding-right:30px;width:100%" id="Div3">	
            <div id="alert" style="display:none;" class="alert alert-info alert-dismissable">
          <a style="cursor:none;" class="panel-close close" data-hide="alert">×</a> 
          <i class="fa fa-coffee"></i>
          
        </div>
            <%--<div id="alertFail" style="display:none;" class="alert alert-info alert-dismissable">
          <a style="cursor:none;" class="panel-close close" data-hide="alert">×</a> 
          <i class="fa fa-coffee"></i>Request failed due to service failure
          
        </div>
            <div id="alertInternet" style="display:none;" class="alert alert-info alert-dismissable">
          <a style="cursor:none;" class="panel-close close" data-hide="alert">×</a> 
          <i class="fa fa-coffee"></i>Service unreachable. Check your internet connection or contact support
          
        </div>
<div id="alertError" style="display:none;" class="alert alert-info alert-dismissable">
          <a style="cursor:none;" class="panel-close close" data-hide="alert">×</a> 
          <i class="fa fa-coffee"></i>Service unreachable. Check your internet connection or contact support
          
        </div>--%>
          <div id="alert2" style="display:none;background-color:pink;" class="alert alert-info alert-dismissable">
          <a style="cursor:none;" class="panel-close close" data-hide="alert">×</a> 
          <i class="fa fa-coffee"></i>All field must be filled
          
        </div>
                                    <div class="widget-header"> 
								 <span style="display:inline; font-size:larger;" class="pull-left"; >Statement Request</span> <span style="display:inline;" class="pull-right" >
                            	Filter by account no.
                                                     <%--<asp:Panel ID="panel4" runat="server" DefaultButton="lnkSearch2">	--%>
                                                       <input id="txtFlter2" style="width:150px" />
                                  	
                     <a id="lnkSearch2" style="height:26px" class="btn btn-sm btn-purple"><i class="icon-filter"></i></a>
                                   <%-- </asp:Panel>--%>

												 </span> 
				 		
											</div>
      
											<div class="widget-body" style="width:100%">
                                                
												<div class="widget-main no-padding">
                                                    
													<table class="table table-striped table-bordered table-hover"> 
														<thead class="thin-border-bottom" id="thRequest" runat="server">
														</thead>
                                                        <tbody  id="tbRequest">
														 	</tbody><tfoot  id="tfRequest"><tr>
                                                            <td colspan="10" style="text-align:right;font-style:italic">Displaying top 
                                                            <label id="lblTop" ></label> of <label id="lblTotal"></label> | 
                                                            <a href="javascript:void()" id="aViewAll2" >View All</a></td></tr></tfoot>
													</table>
                                                    
												</div>
											</div> 	</div>
      
    <div id="Div1" style="padding-left:30px;padding-right:30px;width:100%">	<h3>New Request</h3>
                              <table><tr style="margin-bottom:50px;"><td> Account No.</td><td> 
                                  <asp:TextBox ID="txtAccountSearch"  Width="150px" runat="server"></asp:TextBox></td><td>&nbsp;</td><td>Bank.</td><td>
                                  <asp:DropDownList ID="ddlBankName" AutoPostBack="false" Width="150px" runat="server">
                                     <%-- <asp:ListItem Text="Access Bank" Value="6"></asp:ListItem>
                                      <asp:ListItem Text="Eco Bank" Value="32"></asp:ListItem>
                                      <asp:ListItem Text="Heritage Bank" Value="7"></asp:ListItem>
                                      <asp:ListItem Text="Skye Bank" Value="2"></asp:ListItem>
                                      <asp:ListItem Text="UBA" Value="14"></asp:ListItem>
                                      <asp:ListItem Text="Wema Bank" Value="12"></asp:ListItem>
                                      <asp:ListItem Text="Choose" Value="0" Selected="True"></asp:ListItem>--%>
                                  </asp:DropDownList>	</td></tr>
                                  
                                  <tr>
                                    <td>Start Date</td><td>
                                    <asp:TextBox  class="form-control date-picker" data-date-format="M dd, yyyy"  ID="txtStartDate" Width="150px" runat="server"></asp:TextBox>
                                   </td><td>&nbsp;</td><td>End Date</td><td> <asp:TextBox data-date-format="M dd, yyyy" ID="txtEndDate" class="form-control date-picker"   Width="150px" runat="server" ></asp:TextBox>
                   </td></tr>

                                   <tr>
                           <td>Role</td><td><asp:DropDownList ID="ddlRole" runat="server"  Width="150px"  
                                                                  DataTextField="Role" DataValueField="Role"  >
                                                                  <asp:ListItem Selected="True" Value="0">Choose...</asp:ListItem>
                                                                  <asp:ListItem Value="Applicant">Applicant</asp:ListItem>
                                                                  <asp:ListItem Value="Guarantor">Guarantor</asp:ListItem>
                                                                  <asp:ListItem Value="Sponsor">Sponsor</asp:ListItem> 
                                                              </asp:DropDownList></td><td>&nbsp;</td>
                                       <td>Country</td><td>
                                       <%--    <asp:DropDownList ID="ddlCountry" runat="server"  Width="150px" > 
                                                                  <asp:ListItem Selected="True" Value="Nigeria">Nigeria</asp:ListItem>
                                                                  <asp:ListItem Value="Ghana">Ghana</asp:ListItem>
                                                                  <asp:ListItem Value="Kenya">Kenya</asp:ListItem> 
                               <asp:ListItem Value="Benin">Benin</asp:ListItem>
                                                                  <asp:ListItem Value="Togo">Togo</asp:ListItem> 
                                           <asp:ListItem Value="Senegal">Senegal</asp:ListItem> 
                                                              </asp:DropDownList>--%>
                                           <input type="tel" id="demo" placeholder="Ex. 08012..." style="width:150px;" />
                                                       </td><td>&nbsp;</td><td>&nbsp;</td></tr>
                          <tr><td style="vertical-align:top">Applicants</td> <td style="vertical-align:top"><div id='TextBoxesGroup'><div id="TextBoxDiv1"><input type='text' class='txtApp' id='textbox1' style="width:150px" /></div></div></td><td>&nbsp;</td>
                              <td> <a id="addButton" href="javascript:void()" style="height:29px" class="btn btn-sm btn-purple" title="Add new applicant" >  <i class="icon-plus"></i> </a>
                               <br /><br />
                               <a id="removeButton" href="javascript:void()" style="height:29px" class="btn btn-sm btn-purple"  title="remove applicant">  <i class="icon-remove"></i> </a>
<%--<input type='button' value='Get TextBox Value' id='getButtonValue' />--%>

                           </td>
                           <td class="pull-right" style="vertical-align:bottom">
                               
	<a  id="sendRequest" onclick="sendRequest()" class="btn btn-sm btn-purple pull-right"  title="Verify account and sent request" >Send <i class="icon-ok"></i></a>
                               <img id="imgAddApp" alt="" src="images/wait.gif" style="display:none"/>
                           </td>
                       </tr>           
                                 </table> 
        <div id="displayMessage" style="background-color:pink;color:black;"></div>
          <asp:Label ID="lblNotif" Height="20px" BackColor="#ff9999" ForeColor="White" runat="server"></asp:Label><span id="icnRemove" runat="server" visible="false"><asp:LinkButton runat="server" ID="lnkIcnRemove"><i class="icon-remove"></i></asp:LinkButton></span>
                                      </div> 
  
    <div id="Div2" style="padding-left:30px;padding-right:30px;width:100%;display:none;" >	<h3>Confirm Request</h3>
                              <table><tr>
                                  <td style="vertical-align:top"> 
                            	Request ID. <span style="padding-right:20px;"></span>
                                      <asp:TextBox id="txtRequestID"  Width="100px" runat="server"></asp:TextBox></td>
                                  <td>
                                     Ticket No.<span style="padding-right:20px;"></span> <asp:TextBox id="txtTicket"  Width="100px" runat="server"></asp:TextBox></td>
                                  <td>	Password. <span style="padding-right:20px;"></span><asp:TextBox id="txtPsw"  Width="100px" runat="server"></asp:TextBox>	
                                 <%--  <asp:Panel ID="panel3" runat="server" DefaultButton="lnkConfirm">--%>
                                  <%--<asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                                      <ContentTemplate>--%></td></tr>
                                  
                                  
                                  
                                    <tr><td>
                                    <a onclick="lnkNewRequest()" style="cursor:pointer">Make new request</a></td>
                                        <td>&nbsp;</td>
                 <td>
                                     <a id="aConfirm" class="btn btn-sm btn-purple pull-right" onclick="Confirm()">Confirm</a></td>
                                       </tr>
                                  <tr>
                                      <td style="vertical-align:top" colspan="3"> 
                                    <asp:Label ID="lblConfirm" Height="20px" BackColor="#ff9999" ForeColor="White" runat="server"></asp:Label><span id="spnConform" style="display:none;"><asp:LinkButton runat="server" ID="lnkConfirmRomove"><i class="icon-remove"></i></asp:LinkButton></span>
                                  <div id="confirmMessage" style="background-color:pink;color:black;"></div>

				 		 
                             
                                         </td>
                                     </tr>
                                   
                              </table>
                                </div>
					         <table>
                                 <tbody id="tb" runat="server">
                                     <%--<tr runat="server"><td runat="server">Omokore J. Ayodele</td><td>1000069</td><td>Access Bank Plc</td><td>Nigeria</td><td>omokore.ayodele@gmail.com</td><td>12/10/2015 4:11:00 PM</td><td runat="server"><asp:LinkButton runat="server" OnClick="download_ServerClick" CommandArgument="1000069-11">Download</asp:LinkButton></td></tr>--%>
                                 </tbody>

					         </table>
    <asp:Label ID="acctName" runat="server" Visible="false">Wallz & Queen Ltd</asp:Label>
		<script src="js/SmartNotification.min.js" type="text/javascript"></script>
      	<script type="text/javascript">
      	   
      	    function Confirm() {
      	        if (document.getElementById('ContentPlaceHolder1_txtRequestID').value == "" ||
      	            document.getElementById('ContentPlaceHolder1_txtTicket').value == "" ||
      	        document.getElementById('ContentPlaceHolder1_txtPsw').value == "") {
      	            document.getElementById('confirmMessage').innerHTML = "<b>All fields must be filled.";

      	        } else {
      	            var obj = {};
      	            obj.requestID = document.getElementById('ContentPlaceHolder1_txtRequestID').value;
      	            obj.ticketNo = document.getElementById('ContentPlaceHolder1_txtTicket').value;
      	            obj.psw = document.getElementById('ContentPlaceHolder1_txtPsw').value;
      	            var jsonData = JSON.stringify(obj);
      	            $.ajax({
      	                type: "POST",
      	                url: "RequestStatement.aspx/Confirm",
      	                data: jsonData,
      	                contentType: "application/json; charset=UTF-8",
      	                dataType: "json", // dataType is json format
      	                success: function (response) {
      	                    $("#alert").show();
      	                    if (String(response.d) == "0") {
      	                        document.getElementById('confirmMessage').innerHTML = "<b>Request confirmation failed.";
      	                    } else if (String(response.d) == "1") {
      	                        document.getElementById('confirmMessage').innerHTML = "<b>Error encountered while confirming the request.";

      	                    } else if (String(response.d) == "notupdated") {
      	                        document.getElementById('confirmMessage').innerHTML = "<b>Request cannot be updated.";

      	                    } else if (String(response.d) == "invalid") {
      	                        document.getElementById('confirmMessage').innerHTML = "<b>Password or ticketID not valid for the request.";

      	                    } else if (String(response.d) == "validateError") {
      	                        document.getElementById('confirmMessage').innerHTML = "<b>Error occurred during ticket validation";

      	                    } else {
      	                        document.getElementById('tbRequest').innerHTML = String(response.d).split("~/")[2];
      	                        $("#lblTotal").text(String(response.d).split("~/")[1]);
      	                        $("#lblTop").text(String(response.d).split("~/")[0]);
      	                        document.getElementById("aViewAll2").text = "View All";
      	                        document.getElementById('alert').innerHTML = String(response.d).split("~/")[3];
      	                        document.getElementById('ContentPlaceHolder1_txtRequestID').value = "";
      	                    }
      	                },
      	                //error: OnErrorCall
      	            });
      	        }
      	    }
      	    function jDelete(ID) {
      	        var id = ID.split("%")[0];
      	        swal({
      	            title: "Are you sure?",
      	            text: "This request will no longer be viewed on screen!",
      	            type: "warning",
      	            showCancelButton: true,
      	            confirmButtonColor: "#DD6B55",
      	            confirmButtonText: "Yes, delete it!",
      	            closeOnConfirm: true
      	        },
function () {
    
    var obj = {};
    obj.id = id;

    var jsonData = JSON.stringify(obj);
    $.ajax({
        type: "POST",
        url: "RequestStatement.aspx/JDelete",
        data: jsonData,
        contentType: "application/json; charset=UTF-8",
        dataType: "json", // dataType is json format
        success: function (response) {
            if (String(response.d) == "1") {
                  // swal("Deleted!", "Request deleted.", "success");
                var searchString = id;
                $("#tbRequest tr td:contains('" + searchString + "')").each(function () {
                    if ($(this).text() == searchString) {
                        $(this).parent().remove();
                    }
                });
                $("#lblTotal").text(parseInt( $("#lblTotal").text()) -1);
                $("#lblTop").text(parseInt($("#lblTotal").text()) - 1);

            } else if (String(response.d) == "0") {
               // swal("Deleted!", "Cannot find request. It may have been removed.", "success");
            } else {
                swal("Not Deleted!", "Request could not be deleted.", "success");
            }
        },
        error: function (response) {
            swal("Deleted!", "Request could not be deleted.", "success");
        }

    });
    
});
      	    }

      	    function jPending(ID) {
      	        if (ID.split("%")[1] == "Ticket") {
      	            $("#Div1").hide();
      	            $("#Div2").show();
      	            document.getElementById('ContentPlaceHolder1_txtRequestID').value = ID.split("%")[0];
      	            document.getElementById('ContentPlaceHolder1_txtTicket').value = "";
      	            document.getElementById('ContentPlaceHolder1_txtPsw').value = "";
      	        } else if (ID.split("%")[1] == "Confirming") {
      	            $.bigBox({
      	                title: "Info",
      	                content: "Statement generation request has been sent. You will be notified by mail if succesful.",
      	                color: "red",
      	                timeout: 5000
      	            });
      	        } else if (ID.split("%")[1] == "Invalid") {
      	            $.bigBox({
      	                title: "Info",
      	                content: "The account you requested is invalid.",
      	                color: "red",
      	                timeout: 5000
      	            });
      	        } else if (ID.split("%")[1] == "Error") {
      	            $.bigBox({
      	                title: "Info",
      	                content: "Request encountered error.",
      	                color: "red",
      	                timeout: 5000
      	            });
      	        }
      	        else if (ID.split("%")[1] == "Sent") {
      	            $.bigBox({
      	                title: "Info",
      	                content: "Statement has been sent. You can check your inbox to view",
      	                color: "red",
      	                timeout: 5000
      	            });
      	        }
      	        else if (ID.split("%")[1] == "Pending") {
      	            $.bigBox({
      	                title: "Info",
      	                content: "Statement request is pending evaluation. Refresh for update",
      	                color: "blue",
      	                timeout: 5000
      	            });
      	        }
      	        else if (ID.split("%")[1] == "Charging") {
      	            $.bigBox({
      	                title: "Info",
      	                content: "Request to charge customer. You can refresh for update",
      	                color: "blue",
      	                timeout: 5000
      	            });
      	        }
      	        else if (ID.split("%")[1] == "Ins. Fund" || ID.split("%")[1] == "Billing Failure") {
      	            var obj = {};
      	            obj.id = ID.split("%")[0];

      	            var jsonData = JSON.stringify(obj);
      	            $.ajax({
      	                type: "POST",
      	                url: "RequestStatement.aspx/BillCustomer",
      	                data: jsonData,
      	                contentType: "application/json; charset=UTF-8",
      	                dataType: "json", // dataType is json format
      	                success: function (response) {
      	                    $("#alert").show();
      	                    if (String(response.d) == "0") {
      	                        document.getElementById('confirmMessage').innerHTML = "<b> Fail to debit customer.";
      	                    } else if (String(response.d) == "1") {
      	                        document.getElementById('confirmMessage').innerHTML = "<b>Error encountered.";

      	                    } else if (String(response.d) == "notupdated") {
      	                        document.getElementById('confirmMessage').innerHTML = "<b>Service failure. Try again";

      	                    }  else {
      	                        document.getElementById('tbRequest').innerHTML = String(response.d).split("~/")[2];
      	                        $("#lblTotal").text(String(response.d).split("~/")[1]);
      	                        $("#lblTop").text(String(response.d).split("~/")[0]);
      	                        document.getElementById("aViewAll2").text = "View All";
      	                        document.getElementById('alert').innerHTML = String(response.d).split("~/")[3];
      	                        
      	                    }
      	                },
      	                error: function (response) {
      	                    
      	                }

      	            });
      	        }
      	    }
      	    function lnkNewRequest() {
      	        $("#Div1").show();
      	        $("#Div2").hide();
      	    }

      	    function sendRequest() {
      	        var countryData = $("#demo").intlTelInput("getSelectedCountryData");
      	        var intlNumber = $("#demo").intlTelInput("getNumber");
      	        //    var extension = $("#demo").intlTelInput("getExtension");



      	        var msg = '';
      	        $(".txtApp").each(function () {
      	            msg += $(this).val() + '|';
      	        });
      	        if (msg.indexOf('|') > -1) {
      	            msg = msg.slice(0, -1);
      	        }

      	        if (document.getElementById('ContentPlaceHolder1_txtAccountSearch').value == "" ||
                   document.getElementById('ContentPlaceHolder1_txtStartDate').value == "" ||
                    document.getElementById('ContentPlaceHolder1_txtEndDate').value == "" ||
                    $.trim($("[id*=ddlRole]").val()) == 0 ||
                   countryData.name == "" ||
                    msg == "") {
      	            //  document.getElementById('alert').innerHTML = "<b>All Fields need to be filled";

      	            $("#alert2").show();
      	        } else {

      	            $("#alert2").hide();
      	            $("#imgAddApp").show();
      	            var obj = {};

      	            obj.Nuban = document.getElementById('ContentPlaceHolder1_txtAccountSearch').value;
      	            obj.BankID = $.trim($("[id*=ddlBankName]").val());
      	            // obj.BankID = "14";
      	            //Note this is hard coded. You will get this from users logon for Nibbs ID to the MFB
      	            // obj.NibssClientID = 27;
      	            //   obj.StartDate = $("#txtStartDate").val();
      	            obj.StartDate = document.getElementById('ContentPlaceHolder1_txtStartDate').value;
      	            // obj.EndDate = $("#txtEndDate").val();
      	            obj.EndDate = document.getElementById('ContentPlaceHolder1_txtEndDate').value;
      	            obj.Role = $.trim($("[id*=ddlRole]").val());
      	            // obj.Role = "Applicant";
      	            //   obj.Username = value;
      	            obj.Country = countryData.name;
      	            //  obj.Country = "NGN";
      	            obj.ApplicantNames = msg;
      	            obj.tel = intlNumber;
      	            //   obj.ApplicantNames = "Olamide";
      	            var jsonData = JSON.stringify(obj);

      	            $.ajax({
      	                type: "POST",
      	                url: "RequestStatement.aspx/RequestStatement",
      	                data: jsonData,
      	                contentType: "application/json; charset=UTF-8",
      	                dataType: "json", // dataType is json format
      	                success: function (response) {
      	                   
      	                    $("div .alert").hide();
      	                    //  $("#alert").show();
      	                    $("#imgAddApp").hide();
      	                    if (String(response.d).split("~/")[0] == "0") {
      	                        document.getElementById('alert').innerHTML = '<a style="cursor:none;" class="panel-close close" data-hide="alert">×</a>  <i class="fa fa-coffe"></i> Request failed due to service failure';
      	                        $("#alert").show();
      	                    } else if (String(response.d).split("~/")[0] == "1") {
      	                        document.getElementById('alert').innerHTML = '<a style="cursor:none;" class="panel-close close" data-hide="alert">×</a>  <i class="fa fa-coffe"></i> System time-out. Please refresh.';
      	                        ementById('alert').innerHTML = String(response.d).split("~/")[1];
      	                        $("#alert").show();
      	                        // document.getElementById('alert').innerHTML = "<a style='cursor:none;' class='panel-close close' data-dismiss='alert'>×</a>  <i class='fa fa-coffee'></i> Service unreachable. Check your internet connection or contact support"

      	                    }
      	                    else if (String(response.d).split("~/")[0] == "2") {
      	                        document.getElementById('alert').innerHTML = '<a style="cursor:none;" class="panel-close close" data-hide="alert">×</a>  <i class="fa fa-coffe"></i> Error encountered during statement request.';
      	                        // document.getElementById('alert').innerHTML = "<a style='cursor:none;' class='panel-close close' data-hide='alert'>×</a>  <i class='fa fa-coffee'></i>  Error encountered during statement request."
      	                        $("#alert").show();
      	                    }
      	                    else if (String(response.d).split("~/")[0] == "-1") {
      	                        document.getElementById('alert').innerHTML = '<a style="cursor:none;" class="panel-close close" data-hide="alert">×</a>  <i class="fa fa-coffe"></i> System time-out. Please refresh.';
      	                        $("#alert").show();
      	                        //  document.getElementById('alert').innerHTML = "<a style='cursor:none;' class='panel-close close' data-hide='alert'>×</a>  <i class='fa fa-coffee'></i> Request failed due to service failure"

      	                    } else {
      	                        document.getElementById('tbRequest').innerHTML = String(response.d).split("~/")[2];
      	                        // $('#tbActivityFoot').hide()
      	                        $("#lblTotal").text(String(response.d).split("~/")[1]);
      	                        $("#lblTop").text(String(response.d).split("~/")[0]);
      	                        document.getElementById("aViewAll2").text = "View All";
      	                        //   document.getElementById('alert').innerHTML = String(response.d).split("~/")[3];
      	                        document.getElementById('alert').innerHTML = '<a style="cursor:none;" class="panel-close close" data-hide="alert">×</a>  <i class="fa fa-coffe"></i> Request sent successfully';
      	                        document.getElementById('ContentPlaceHolder1_txtAccountSearch').value = "";
      	                        //  $.trim($("[id*=ddlBankName]").val('0'))
                                reset();
      	                    }


      	                    //  window.location.replace('RequestStatement.aspx');
      	                },

      	                error: function (response) {
      	                    $("#imgAddApp").hide();
      	                    $.bigBox({
      	                        title: "Error",
      	                        content: "Error encountered or system timed out. Please refresh",
      	                        color: "#CC0000",
      	                        timeout: 4000
      	                    });
      	                }
      	            });
      	        }
      	    }
      	    function reset() {
      	        document.getElementById('ContentPlaceHolder1_txtAccountSearch').value = '';
      	        $.trim($("[id*=ddlBankName]").val('0'));
      	        document.getElementById('ContentPlaceHolder1_txtStartDate').value = '';
      	        document.getElementById('ContentPlaceHolder1_txtEndDate').value = '';
      	        $.trim($("[id*=ddlRole]").val('0'));

      	    }
      	 
      	</script>
</asp:Content>

