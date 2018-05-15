<%@ Page Title="eStatementAudit" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="eStatement.aspx.vb" Inherits="eStatement" %>

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
                       url: "eStatement.aspx/FilterActivity",
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
																<th>Ticket</th>
																<th >Timestamp</th>
                                                                <th >Viewed</th>
                                                                <th >Viewed Timestamp</th>
																<th >Status</th>
                                                                
															</tr>
														</thead>
                                                        <tbody  id="tbActivityBody" runat="server">
														 	</tbody><tfoot  id="tbActivityFoot" runat="server"><tr>
                                                            <td colspan="9" style="text-align:right;font-style:italic">Displaying top 
                                                            <asp:Label ID="lblTop" runat="server">3</asp:Label> of <asp:Label ID="lblTotal" runat="server">50</asp:Label> | 
                                                            <a href="javascript:void()" id="aViewAll" >View All</a></td></tr></tfoot>
													</table>
												</div>
											</div> 	</div> 
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

                   if (ID.split("%")[2] == "Sent") {
                       //$.ajax({
                       //    type: "POST",
                       //    url: "DashBoard.aspx/Ticket",
                       //    data: jsonData2,
                       //    contentType: "application/json; charset=UTF-8",
                       //    dataType: "json", // dataType is json format
                       //    success: function (response) {
                               // alert(response.d)
                               //		                    if (String(response.d) == "1") {
                               //		                        window.open("ticket/" + ID.split("%")[1] + ".pdf", "_blank");
                               //		                    } else {
                               //		                        window.open("receipt/" + ID.split("%")[1] + ".pdf", "_blank");
                               //		                        }

                             //  $("#lbPreview").hide();
                               // $("#img1").show();
                              // $("#Div1").hide();
                               $("#Div4").show();
                              $("#Div3").hide();
                               $('#iStatement1').attr('src', "Authorization/" + ID.split("%")[1] + ".pdf", "_blank"); 


                         //  }
                           
                           //error: function (response) {
                           //    $.bigBox({
                           //        title: "Error",
                           //        content: "Error Encountered or System Timed-out",
                           //        color: "#CC0000",
                           //        timeout: 4000
                           //    });
                           //}
                       }//);


                   }
                 
            

		</script>
</asp:Content>

