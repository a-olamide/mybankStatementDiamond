<%@ Page Title="Manage" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Monitor.aspx.vb" Inherits="Monitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-header" style="padding-top:14px;padding-left:7px">
	 <h1 style="font-size: 15px;"  >
								Monitoring<small style="font-size: 14px; font-weight:inherit ">
									<i class="icon-double-angle-right"></i>
                                      Track all automated statement transfers 
								</small>
							</h1>

                                                   
				 			</div>
<table width="100%" style="border-right-width:thin; border-right-color:black;">
  <tr>
      <td style="padding-left:17px;display:none" >
                <div class="widget-header"  >
<h5>Backup</h5>	</div>
     		<div class="widget-body pull-left" style="width:100%">
												<div class="widget-main no-padding">
<table class="table table-striped table-bordered table-hover" 
    style="background-image: url('http://localhost:40322/mybankStatement/images/GRed.png'); background-repeat: no-repeat;">
    <tr><td width="50%">Initiate backup for past </td>
        <td width="50%"><asp:DropDownList runat="server" ID="ddlbackupselect" Enabled ="false"   >
                 <asp:ListItem Selected="true">1 month</asp:ListItem> 
            <asp:ListItem>1 day</asp:ListItem>
              <asp:ListItem>1 week</asp:ListItem>
         
            </asp:DropDownList>
            &nbsp; &nbsp;
            <asp:LinkButton runat="server" Enabled="false" CausesValidation="false" ID="lnkstartbackup">
                 <i class="icon-play grey"  style="margin-left:-5px;font-size:15px"></i>
            </asp:LinkButton>
               &nbsp;&nbsp;
            <asp:LinkButton ID="lnkstopbackup" CausesValidation="false"  runat="server"> <i class=" icon-pause green"  style="margin-left:-5px;font-size:15px"></i> </asp:LinkButton>
              
       
        </td>
    </tr>

       <tr><td width="50%">Current Task</td>
    <td width="50%">  
           <asp:UpdatePanel runat="server" ID="updatepanel2" UpdateMode="Conditional" >
                                                        <ContentTemplate> 
                                                            <asp:Timer ID="timerbackuptask"  runat="server" Interval="1000"></asp:Timer> 
        <div class="progress progress-striped active" 
                                    style="margin-left:5px;margin-top:5px;margin-bottom:5px;text-align:right; margin-right:10px"
                                       runat="server" id="backupheadertask" data-percent="Backing Up">
										 <div class="progress-bar progress-bar-grey" style="width: 85%" 
                                                    runat="server" id="backupprogresstask" ></div>	</div>
         
              
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel> 
    </td>
       </tr>
         <tr style="display:none"><td width="50%">Last backup</td>
    <td width="50%">1 May 2015 2:34AM</td>
       </tr>
    <tr><td colspan="4">
           <div runat="server">
                                          <div class="widget-header">
												<h5 class="widget-title bigger lighter">
													<i class="ace-icon fa fa-table"></i>
													Summary
												</h5>
                                                   <span class="row pull-right" style="margin-top: 4px;padding-right:20px">
                  <table><tr><td width="50%"></td><td width="50%">	  <asp:DropDownList ID="ddlbackupmonth"  Width="65px" runat="server" AutoPostBack="true"  DataTextField="Month" DataValueField="iMonth">
                  </asp:DropDownList>
                                  	</td><td width="50%">
                   	  <asp:DropDownList ID="ddlbackupyear"  DataTextField="Year" DataValueField="Year" Width="65px" runat="server" AutoPostBack="true"></asp:DropDownList>
                                 
                            </td> </tr></table>
										        <asp:Label ID="lbPaging" runat="server" Visible="false"></asp:Label>
                                  		</span> 		 
											</div>
											<div class="widget-body pull-left" style="width:100%">
												<div class="widget-main no-padding">
												                  <asp:UpdatePanel runat="server" UpdateMode="Conditional"  ID="updatepanel1">
                                                        <ContentTemplate> 
                                                            <asp:Timer ID="timerbackupsummary"  runat="server" Interval="1000"></asp:Timer> 
                                                       
																<table class="table table-striped table-bordered table-hover"> 
													 <tbody id="Tbody1" runat="server">
                                                         <tr><td width="50%"></td><td width="50%"></td></tr>
														<tr>
																<td style="border-right-width:0px; border-left-width:0px; border-top:0px; border-bottom:0px" width="50%">Status</td>

																<td id="_tdStatus" runat="server" width="50%">
															  <div class="progress progress-striped " 
                                    style="margin-left:5px;margin-top:5px;margin-bottom:5px;text-align:right; margin-right:10px"
                                       runat="server" id="divheaderbackup" data-percent="90% backed up">
										 <div class="progress-bar progress-bar-success" style="width: 85%" 
                                                    runat="server" id="divprogressbackup" ></div>	</div>
																</td> 
                                                                 
															</tr>
                                                            	<tr>
																<td class="" width="50%">Total Count</td>
                                                                    
																<td width="50%"> 
                                                    	<a id="_abackuptotal" runat="server" href="javascript:void()">1,500,000</a>
														
       	</td> 
                                                                 
															</tr><tr>
																<td width="50%" class="">Successful</td>

																<td width="50%">
																<a id="_abackupsuccessful" runat="server" href="javascript:void()">1,324,324</a>
																</td> 
                                                                 
															</tr><tr>
																<td width="50%" class="">Failed</td>

																<td>
																<a id="_abackupfailure" runat="server" href="javascript:void()">0</a>
                     <asp:linkbutton ID="lbRefresh" Visible="false" runat="server" Height="22px" Width="20px" 
                                CssClass="btn btn-sm btn-purple pull-right" CausesValidation="False" ><i class="icon-refresh" style="margin-left:-5px;font-size:10px"></i></asp:linkbutton>  
																</td> 
															</tr>
                                                             <tr id="_trSend" runat="server" visible="false">
																<td class=""></td>

																<td>
																
                     <asp:linkbutton ID="bSend" runat="server" 
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" >Send</asp:linkbutton>
																</td> 
															</tr><tr>
																<td class="">Pending </td>

																<td>
																<a id="_abackuppending" runat="server" href="javascript:void()">0%</a>
																</td> 
                                                                 
															</tr>
														</tbody>
													</table>
                                                    	     
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
												</div>
											</div> </div>
        </td></tr>
</table> 
      </div>

                                                    </div>
                                            		 
										
      </td>
    <td   width="50%">
                  <div class="widget-header"  >
          <h5>Generate</h5></div>
        <div class="widget-body pull-left" style="width:100%">
												<div class="widget-main no-padding">
<table class="table table-striped table-bordered table-hover">
    <tr><td width="50%">Initiate generation</td>
        <td width="50%"><asp:DropDownList runat="server" Enabled="false" visible="false"  ID="ddlgenerateselect"  >
                       <asp:ListItem Selected="true">1 month</asp:ListItem> 
               <asp:ListItem>1 day</asp:ListItem>
              <asp:ListItem>1 week</asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp;
            <asp:LinkButton runat="server" Enabled="false" ID="lnkstartgenerate" CausesValidation="false">
                <i class="icon-play grey"  style="margin-left:-5px;font-size:15px"></i>
            </asp:LinkButton>
                &nbsp;&nbsp;
            <asp:LinkButton runat="server" ID="lnkstopgenerate">
       <i class=" icon-pause green"  style="margin-left:-5px;font-size:15px"></i> 
            </asp:LinkButton>
         
 
      
        </td>
    </tr>

       <tr><td width="50%">Current Task</td>
    <td width="50%">         <asp:UpdatePanel runat="server" ID="updatepanel3" UpdateMode="Conditional" >
                                                        <ContentTemplate> 
                                                            <asp:Timer ID="timergeneratetask"  runat="server" Interval="1000"></asp:Timer> 
      
        <div class="progress progress-striped active" 
                                    style="margin-left:5px;margin-top:5px;margin-bottom:5px;text-align:right; margin-right:10px"
                                       runat="server" id="generateheadertask" data-percent="Generating">
										 <div class="progress-bar progress-bar-grey" style="width: 75%" 
                                                    runat="server" id="generateprogresstask" ></div>	</div>
                                                            </ContentTemplate>
        </asp:UpdatePanel>
         
               
    </td>
       </tr>
         <tr style="display:none"><td width="50%">Last Generation</td>
    <td width="50%">5 Feb 2015 2:34AM</td>
       </tr>
    <tr ><td colspan="4">
           <div>
                                          <div class="widget-header">
												<h5 class="widget-title bigger lighter">
													<i class="ace-icon fa fa-table"></i>
													Summary
												</h5>
                                                   <span class="row pull-right" style="margin-top: 4px;padding-right:20px">
                  <table><tr><td width="50%"></td><td width="50%">
                   	  <asp:DropDownList ID="ddlgenerateyear" DataTextField="Year" DataValueField="Year"  Width="65px" runat="server" AutoPostBack="true" ></asp:DropDownList>
                                 
                            </td> <td width="50%">	  <asp:DropDownList ID="ddlgeneratemonth" DataTextField="Month" DataValueField="iMonth"  Width="65px" runat="server" AutoPostBack="true" >
                  </asp:DropDownList>
                                  	</td></tr></table>
										        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                  		</span> 		 
											</div>
											<div class="widget-body pull-left" style="width:100%">
												<div class="widget-main no-padding">
                                                    		                  <asp:UpdatePanel runat="server" UpdateMode="Conditional"  ID="updatepanel4">
                                                        <ContentTemplate> 
                                                            <asp:Timer ID="timergeneratesummary"  runat="server" Interval="1000"></asp:Timer> 
                                                       
										
													<table class="table table-striped table-bordered table-hover"> 
													 <tbody id="Tbody2" runat="server">
                                                         <tr><td width="50%"></td><td width="50%"></td></tr>
														<tr>
																<td style="border-right-width:0px; border-left-width:0px; border-top:0px; border-bottom:0px" width="50%">Status</td>

																<td id="Td1" runat="server" width="50%">
															  <div class="progress progress-striped active" 
                                    style="margin-left:5px;margin-top:5px;margin-bottom:5px;text-align:right; margin-right:10px"
                                       runat="server" id="divheadergenerate"  data-percent="Not Running">
										 <div class="progress-bar progress-bar-gray" style="width: 0%" 
                                                    runat="server" id="divprogressgenerate" ></div>	</div>
																</td> 
                                                                 
															</tr>
                                                            	<tr>
																<td class="" width="50%">Total Count</td>

																<td width="50%">
																<a id="_ageneratetotal" runat="server" href="javascript:void()">0</a>
																</td> 
                                                                 
															</tr><tr>
																<td width="50%" class="">Successful</td>

																<td width="50%">
																<a id="_ageneratesuccessful" runat="server" href="javascript:void()">0</a>
																</td> 
                                                                 
															</tr><tr>
																<td width="50%" class="">Failed</td>

																<td>
																<a id="_ageneratefailed" runat="server" href="javascript:void()">0</a>
                     <asp:linkbutton ID="Linkbutton1" Visible="false" runat="server" Height="22px" Width="20px" 
                                CssClass="btn btn-sm btn-purple pull-right" CausesValidation="False" ><i class="icon-refresh" style="margin-left:-5px;font-size:10px"></i></asp:linkbutton>  
																</td> 
															</tr>
                                                             <tr id="Tr1" runat="server" visible="false">
																<td class=""></td>

																<td>
																
                     <asp:linkbutton ID="Linkbutton2" runat="server" 
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" >Send</asp:linkbutton>
																</td> 
															</tr><tr >
																<td class="">Pending </td>

																<td>
																<a id="_ageneratepending" runat="server" href="javascript:void()">0%</a>
																</td> 
                                                                 
															</tr>
														</tbody>
													</table>
                                                            </ContentTemplate>
                                                                                  </asp:UpdatePanel>
												</div>
											</div> </div>
        </td></tr>
</table>
      </div>

                                                    </div>
    </td>
    <td style=" padding-right:7px" width="50%">
                  <div class="widget-header"  >
    <h5>Send</h5>
                      
											</div><div class="widget-body pull-left" style="width:100%">
												<div class="widget-main no-padding">
<table class="table table-striped table-bordered table-hover">
    <tr><td width="50%">Initiate mail sending</td>
        <td width="50%"><asp:DropDownList runat="server" Enabled="false" Visible="false" ID="ddlmailselect"  >
               <asp:ListItem Selected="true">All</asp:ListItem>
            <asp:ListItem>Top 100,000</asp:ListItem>
              <asp:ListItem>Top 500,000</asp:ListItem>
              <asp:ListItem>Top 1,000,000</asp:ListItem> 
            </asp:DropDownList>
            &nbsp; &nbsp;
            <asp:LinkButton runat="server" ID="lnkstartsending" CausesValidation="false" Enabled="false">
<i class="icon-play grey"  style="margin-left:-5px;font-size:15px"></i>
            </asp:LinkButton>
                &nbsp;&nbsp;
                <asp:linkbutton runat="server" ID="lnkstopsending" CausesValidation="false" >
                    <i class=" icon-pause green"  style="margin-left:-5px;font-size:15px"></i> 

                </asp:linkbutton>
      
        </td>
    </tr>

       <tr><td width="50%">Current Task</td>
    <td width="50%">
                          		                  <asp:UpdatePanel runat="server" UpdateMode="Conditional"  ID="updatepanel5">
                                                        <ContentTemplate> 
                                                            <asp:Timer ID="timersendtask"  runat="server" Interval="1000"></asp:Timer> 
                                                       
									
        <div class="progress progress-striped active" 
                                    style="margin-left:5px;margin-top:5px;margin-bottom:5px;text-align:right; margin-right:10px"
                                       runat="server" id="divheadersend" data-percent="Sending">
										 <div class="progress-bar progress-bar-grey" style="width: 50%" 
                                                    runat="server" id="divprogresssend" ></div>	</div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
         
               
    </td>
       </tr>
         <tr style="display:none"><td width="50%">Last mail sent</td>
    <td width="50%">1 May 2015 2:34AM</td>
       </tr>
    <tr><td colspan="4">
           <div>
                                          <div class="widget-header">
												<h5 class="widget-title bigger lighter">
													<i class="ace-icon fa fa-table"></i>
													Summary
												</h5>
                                                   <span class="row pull-right" style="margin-top: 4px;padding-right:20px">
                  <table><tr><td width="50%"></td><td width="50%">
                   	  <asp:DropDownList ID="ddlsendyear"  DataTextField="Year" DataValueField="Year" Width="65px" runat="server" AutoPostBack="true" ></asp:DropDownList>
                                 
                            </td><td width="50%">	  <asp:DropDownList ID="ddlsendmonth"  Width="65px" runat="server" AutoPostBack="true"   DataTextField="Month" DataValueField="iMonth">
                  </asp:DropDownList>
                                  	</td> </tr></table>
										        <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
                                  		</span> 		 
											</div>
											<div class="widget-body pull-left" style="width:100%">
												<div class="widget-main no-padding">
                                                       <asp:UpdatePanel runat="server" UpdateMode="Conditional"  ID="updatepanel6">
                                                        <ContentTemplate> 
                                                            <asp:Timer ID="timersendsummary"  runat="server" Interval="1000"></asp:Timer> 
                                                       
													<table class="table table-striped table-bordered table-hover"> 
													 <tbody id="Tbody3" runat="server">
                                                         <tr><td width="50%"></td><td width="50%"></td></tr>
														<tr>
																<td style="border-right-width:0px; border-left-width:0px; border-top:0px; border-bottom:0px" width="50%">Status</td>

																<td id="Td2" runat="server" width="50%">
															  <div class="progress progress-striped active" 
                                    style="margin-left:5px;margin-top:5px;margin-bottom:5px;text-align:right; margin-right:10px"
                                       runat="server" id="divaheadersend" data-percent="65% sent">
										 <div class="progress-bar progress-bar-gray" style="width: 0%" 
                                                    runat="server" id="divaprogresssend" ></div>	</div>
																</td> 
                                                                 
															</tr>
                                                            	<tr>
																<td class="" width="50%">Total Count</td>

																<td width="50%">
																<a id="_asendtotal" runat="server" href="javascript:void()">0</a>
																</td> 
                                                                 
															</tr><tr>
																<td width="50%" class="">Successful</td>

																<td width="50%">
																<a id="_asendsuccessful" runat="server" href="javascript:void()">0</a>
																</td> 
                                                                 
															</tr><tr>
																<td width="50%" class="">Failed</td>

																<td>
																<a id="_asendfailed" runat="server" href="javascript:void()">0</a>
                     <asp:linkbutton ID="Linkbutton3" Visible="false" runat="server" Height="22px" Width="20px" 
                                CssClass="btn btn-sm btn-purple pull-right" CausesValidation="False" ><i class="icon-refresh" style="margin-left:-5px;font-size:10px"></i></asp:linkbutton>  
																</td> 
															</tr>
                                                             <tr id="Tr2" runat="server" visible="false">
																<td class=""></td>

																<td>
																
                     <asp:linkbutton ID="Linkbutton4" runat="server" 
                                CssClass="btn btn-sm btn-purple" CausesValidation="False" >Send</asp:linkbutton>
																</td> 
															</tr><tr>
																<td class="">Pending </td>

																<td>
																<a id="_asendpending" runat="server" href="javascript:void()">0%</a>
																</td> 
                                                                 
															</tr>
														</tbody>
													</table>
                                                            </ContentTemplate>
                                                           </asp:UpdatePanel>
												</div>
											</div> </div>
        </td></tr>
</table>
      </div>

                                                    </div>
    </td>
                    </tr></table>
</asp:Content>

