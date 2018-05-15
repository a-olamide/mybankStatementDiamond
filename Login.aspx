<%@ Page Language="VB" AutoEventWireup="false" EnableViewStateMac="false" CodeFile="Login.aspx.vb" Inherits="Login" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
		<title>mybankStatement - Login</title>

		<meta name="description" content="User login page" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0" />

		<!-- basic styles -->

		<link href="assets/css/bootstrap.min.css" rel="stylesheet" />
		<link rel="stylesheet" href="assets/css/font-awesome.min.css" />
		<link rel="stylesheet" type="text/css" media="screen" href="assets/css/smartadmin-production.css">
         

		<link rel="stylesheet" href="assets/css/ace-fonts.css" />

		<!-- ace styles -->

		<link rel="stylesheet" href="assets/css/ace.min.css" />
		<link rel="stylesheet" href="assets/css/ace-rtl.min.css" />
         
     <%--   <script src="jquery.min.js" type="text/javascript"></script> --%>

     

</head>
  
<body class="login-layout" >
    <%--<div class="pull-right" style="margin-top:15px">
    
<img class="pull-right" src="images/info.png" width="35px" alt="" onclick="jLoad()" style="cursor:pointer;display:none"/> </div>--%>
    <div style="background-color:#b9d507;width:100%;height:10px;"></div>
    <div style="background-color:#ffffff;width:100%;height:60px;"><div class="pull-right">
        <img src="images/diamond.jpg"  height="59" /></div></div>
		<div class="main-container">
			<div class="main-content" >
				<div class="row" >
					<div class="col-sm-10 col-sm-offset-1"  >
						<div class="login-container"  >
				 	<br /><br /><div class="center"  >
								<h1>
<span style="color:#b9d507">mybank</span>
									<span class="white" style="margin-left:-8px">Statement<SUP> &reg; </SUP></span>
								</h1>
								
							</div>

							<div class="position-relative">
							<form runat="server" id="form1">
                            	<div id="login_box" class="login-box widget-box no-border"   >
                              		<div class="widget-body">
										<div class="widget-main">
											<h4 class="header lighter bigger"> 
												Please enter your credentials
											</h4>

											<div class="space-6"></div>

									 
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input id="txtUsername" type="text" class="form-control" placeholder="Username" onkeyup="return doClick(event);" /> 
                                                &nbsp;<i class="icon-user"></i></span></label><label class="block clearfix"><span class="block input-icon input-icon-right">
                                                <input id= "txtPassword"  type="password" class="form-control" placeholder="Password" onkeyup="return doClick(event);" />
                                                   	<i class="icon-lock"></i>
														</span>
													</label>

													<div class="space"></div>

													<div class="clearfix" >
														<label class="inline" style="visibility:hidden">
															<input type="checkbox" class="ace" />
															<span class="lbl"> Remember Me</span>
														</label> 
                                                        <img id="imgLoad2" alt="" src="images/wait.gif" style="display:none"/>
														<button id="btnLogin" type="button" class="width-35 pull-right btn btn-sm btn-purple" onclick="jLogin()"
                                                        style="margin:0px auto 10px auto;visibility:visible">
															<i class="icon-key"></i>
															Login
														</button>
													</div>

												</fieldset>
										 
										
										</div> 
                                            <div class="toolbar clearfix" style="background:#b9d507;border-top: 2px solid #059da2;height:39px">
											<div>
												<a href="javascript:void" onclick="jResetDisplay()" class="forgot-password-link" 
                                                style="color:White;font-style:italic;font-size:12px;display:none">
													<i class="icon-arrow-left"></i>
													I forgot my password
												</a>
											</div>

											 
										</div>
									</div><!-- /widget-body --> 
								</div><!-- /login-box -->


         <div id="redirect" class="login-box widget-box no-border"   >
             <div class="widget-body">
                <div class="widget-main">
					<h4 class="header lighter bigger"> <center>	Browser not compatible with mybankStatement</center></h4>
                                            	<div class="space-6"></div>
                                            <fieldset>
													<div class="space"></div>
													<div class="clearfix" >
                                                    <center><a id="btnRedirect" target="_blank" href="http://www.support.google.com/chrome/answer/95346?hl=en" class="width-35 btn btn-sm btn-purple"
                                                       style="margin:0px auto 10px auto;visibility:visible">
															<i class="icon-arrow-up"></i>Upgrade</a></center>
													</div>	

                                            </fieldset>
										</div> 
                                          
									</div><!-- /google-redirect-body --> 
								</div><!-- /google-redirect-box -->
                                 <div id="IEredirect" class="login-box widget-box no-border"   >
             <div class="widget-body">
                <div class="widget-main">
					<h4 class="header lighter bigger"> <center>	Browser not compatible with mybankStatement</center></h4>
                                            	<div class="space-6"></div>
                                            <fieldset>
													<div class="space"></div>
													<div class="clearfix" >
                                                    <center><a id="btnIEredirect" target="_blank" href="https://www.microsoft.com/en-us/download/internet-explorer.aspx" class="width-35 btn btn-sm btn-purple"
                                                       style="margin:0px auto 10px auto;visibility:visible">
															<i class="icon-arrow-up"></i>Upgrade</a></center>
													</div>	

                                            </fieldset>
										</div> 
                                          
									</div><!-- /google-redirect-body --> 
								</div><!-- /google-redirect-box -->
                                 <div id="FirefoxRedirect" class="login-box widget-box no-border"   >
             <div class="widget-body">
                <div class="widget-main">
					<h4 class="header lighter bigger"> <center>	Browser not compatible with mybankStatement</center></h4>
                                            	<div class="space-6"></div>
                                            <fieldset>
													<div class="space"></div>
													<div class="clearfix" >
                                                    <center><a id="btnFirefoxRedirect" target="_blank" href="https://www.mozilla.org/en-US/firefox/new/" class="width-35 btn btn-sm btn-purple"
                                                       style="margin:0px auto 10px auto;visibility:visible">
															<i class="icon-arrow-up"></i>Upgrade</a></center>
													</div>	

                                            </fieldset>
										</div> 
                                          
									</div><!-- /google-redirect-body --> 
								</div><!-- /google-redirect-box -->
                                <div id="changepassword_box"  class="login-box widget-box no-border"> 
									<div class="widget-body">
										<div class="widget-main">
											<h4 class="header lighter bigger">
												<i class="icon-key"></i>
												Change Password
											</h4>

											<div class="space-6"></div>
											
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="password" id="txtChangePassword"  class="form-control" placeholder="Enter Password" />
															<i class="icon-key"></i>
														</span>
													</label>
                                                    </fieldset>
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="password"  id="txtconfirmpassword"  class="form-control" placeholder="Confirm Password" />
															<i class="icon-key"></i>
														</span>
													</label>
                                                    </fieldset>
                                                    <fieldset>
													<div  class="clearfix"   > 
                                                        <button type="button" id="lnkCHange" class="width-35 pull-right btn btn-sm btn-purple" onclick="jChange()">
                                                        <i class="icon-key"></i>
															Change
                                                        </button> 

															 
													</div>
												</fieldset> 
										</div><!-- /widget-main -->
                                    
										<div class="toolbar clearfix"   style="background:#660000;border-top: 2px solid #7f0000;height:39px
										;padding:10px 0px 10px 10px">
											<a href="javascript:void" onclick="jLoginDisplay()" class="forgot-password-link"  
                                            style="color:White;font-style:italic;font-size:12px">
												<i class="icon-arrow-left"></i>
												Back to login
											</a>
										</div>
									</div><!-- /widget-body --> 
								</div>
								<!-- /forgot-box -->

								<div id="resetbox" class="login-box widget-box no-border">
									<div class="widget-body">
										<div class="widget-main">
											<h4 class="header lighter bigger">
												<i class="icon-lock"></i>
												Reset Password
											</h4>

											<div class="space-6"></div>
											<p> Enter your email: </p>


												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="email" class="form-control" placeholder="Email" />
															<i class="icon-envelope"></i>
														</span>
													</label>
  
   

													<div class="clearfix" style="margin:0px auto 20px auto">  
                                                    <center>
														<button type="button" class="width-65 btn btn-sm btn-purple" onclick="jReset()">
															<i class="icon-refresh"></i>
															Reset
														</button></center>
													</div>
												</fieldset>
									 
										</div>

										<div class="toolbar clearfix"   style="background:#660000;height:39px;border-top: 2px solid #7f0000;
										padding:10px 0px 10px 10px">
											<a href="javascript:void" onclick="jLoginDisplay()" class="forgot-password-link" 
                                            style="color:White;font-style:italic;font-size:12px">
												<i class="icon-arrow-left"></i>Back to login
											</a>
										</div>
									</div><!-- /widget-body -->
								</div><!-- /signup-box -->
            



                              
							</form>
                            </div><!-- /position-relative -->
						</div>
					</div><!-- /.col -->
				</div><!-- /.row -->
 </div>
		</div><!-- /.main-container -->
        
          <table runat="server" id="tblShowError" visible="false"><tr><td colspan="10"> <asp:Label runat="server" ID="lblShowError" ForeColor="Red" ></asp:Label></td></tr></table>
 <div id="footer"  style="  border-top-color:#059da2; 
    border-top-style:solid; padding-right:50px; padding-left:50px;
    border-top-width:5px; 
    position:fixed;
   left:0px;
   bottom:0px;
   height:60px;
   width:100%;
   background:#ffffff;color:#059da2;vertical-align:middle; line-height:60px;"> Copyright &copy; <a href="http://www.wallzandqueenltd.com" style="text-decoration : none; color : #059da2"> Wallz and Queen Limited</a></div>

<!-- BS JavaScript -->
        <%--<script src="//code.jquery.com/jquery-1.11.0.min.js"></script>--%>
    	<script type="text/javascript">
    	    window.jQuery || document.write("<script src='assets/js/jquery-2.0.3.min.js'>" + "<" + "/script>");
		</script> 
        <script type="text/javascript">
            if (typeof window.JSON == 'undefined') {
                document.write('<script src="../scripts/json2.js"><\/script>');
            }
</script>

	
         
		<script src="js/SmartNotification.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
		<script type="text/javascript">

		    function doClick(e) {
		        //if (window.event)
		        e = e || window.event;
		        //else
		        //    key = e.which;
		        if (e.keyCode == 13) {
		            document.getElementById('btnLogin').click();
		            return false

		        }
		        return true
		    }


		    if ("ontouchend" in document) { document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>") };

		    function jLoad() {
		        $.smallBox({
		            title: "Welcome!",
		            content: "Please login with the test user crededntials given to you. If you do not have one, contact <a href='mailto:support@wallzandqueenltd.com' style='text-decoration:underline;color:White'>support@wallzandqueenltd.com</a>   for assistance.",// <p class='text-align-right'><a href='javascript:void(0);' class='btn btn-primary btn-sm'>Yes</a> <a href='javascript:void(0);'  onclick='noAnswer();' class='btn btn-danger btn-sm'>No</a></p>",
		            color: "#7F0000",
		            timeout: 8000//,
		            //icon: "fa fa-bell swing animated"
		        });
		    }
		    function jResetDisplay() {
		        jQuery('#login_box').removeClass('visible');
		        jQuery('#resetbox').addClass('visible');
		    }
		    function jLoginDisplay() {
		        jQuery('#login_box').addClass('visible');
		        jQuery('#resetbox').removeClass('visible');
		        jQuery('#changepassword_box').removeClass('visible');
		    }
		    function jLogin() {
		        $("#imgLoad2").show();
		        //var qryAllEmployees = "http://10.100.5.195/GetSupervisor/odata/$metadata#HrStaffSupervisors";


		        //$.getJSON(qryAllEmployees, function (results) {
		        //    //  $('#CustomersList').text("");
		        //    $.each(results.d, function (i, item) {
		        //        obj.surname = item.surname;
		        //        obj.firstname = item.firstName;
		        //        obj.jobTitle = item.sDivisionName;
		        //        obj.BankBranchID = item.BankBranchID;
		        //        obj.BankBranchName = item.BankBranchName;
		        //        obj.Supervisor = item.SupervUser;
		        //        //  $('#CustomersList').append("<li>Contact Names: " + item.ContactName + "<br/><br/></li>");
		        //        // $('#StatusDiv').hide();
		        //    });
		        //    // $('#status').text("");
		        //});
		       
		        var obj = {};
		        //if (document.getElementById('txtUsername').value != "" && document.getElementById('txtPassword').value != "") {
		            obj.value = document.getElementById('txtUsername').value;
		            obj.pass = document.getElementById('txtPassword').value;
		           
		            //var value = $("#txtApplicants").val()
		            var jsonData = JSON.stringify(obj);
		            $.ajax({
		                type: "POST",
		                url: "Login.aspx/_login",
		                data: jsonData,
		                contentType: "application/json; charset=UTF-8",
		                dataType: "json", // dataType is json format
		                success: OnSuccess,
		                error: OnErrorCall
		            });
		            function OnSuccess(response) {
		                if (String(response.d) == "Falseb%chrome") {
		                    // jQuery.noConflict();
		                    //jQuery('#myModal').modal('show');
		                    (function ($) {
		                        $('#myModal').modal('show');
		                    })
		                    $("#imgLoad2").hide();
		                }
		                
		                else if (String(response.d) == "False%NoPermission") {
		                    $.bigBox({

		                        title: "Invalid user",
		                        content: "You do not have access to use this application. Contact IT for support",
		                        color: "Red",
		                        timeout: 6000

		                    });
		                    $("#imgLoad2").hide();
		                }
		                else if (String(response.d) == "False%NoEntry") {
		                    $.bigBox({

		                        title: "Invalid user",
		                        content: "Please Enter your credentials",
		                        color: "Red",
		                        timeout: 6000

		                    });
		                    $("#imgLoad2").hide();
		                }
		                else if (String(response.d) == "False%AuthError") {
		                    $.bigBox({

		                        title: "Invalid user",
		                        content: "Error authenticating user",
		                        color: "Red",
		                        timeout: 6000

		                    });
		                    $("#imgLoad2").hide();
                        }
                        else if (String(response.d) == "False%Inactive") {
                            $.bigBox({

                                title: "Invalid user",
                                content: "Your account is Inactive",
                                color: "Red",
                                timeout: 6000

                            });
                            $("#imgLoad2").hide();
                        }
		                else if (String(response.d) == "False%NoExceed") {
		                    $.bigBox({

		                        title: "Invalid user",
		                        content: "Exceed database Error. User account may not exist",
		                        color: "Red",
		                        timeout: 6000

		                    });
		                    $("#imgLoad2").hide();
		                }
		                else if (String(response.d) == "False") {
		                    $.bigBox({

		                        title: "Invalid user",
		                        content: "Kindly confirm the username and password.",
		                        color: "Red",
		                        timeout: 4000
		                        

		                    });
		                    $("#imgLoad2").hide();
		                }
		                else if (String(response.d) == "True%First") {
		                    //jQuery("#btnLogin").removeClass('visible');
		                    //jQuery("#imgLoad").addClass('visible');
		                    jQuery('#changepassword_box').addClass('visible');
		                    jQuery('#login_box').removeClass('visible');
		                    //jQuery("#imgLoad").removeClass('visible');  
		                    $("#imgLoad2").hide();
		                }
		                else if (String(response.d).split("%")[0] == "True") {
		                    $("#imgLoad2").hide();
		                    window.location.replace(String(response.d).split("%")[1]);
		                }

		            }
		            function OnErrorCall(response) {
		                $.bigBox({
		                    title: "Error",
		                    content: "Error Encountered or System Timed-out",
		                    color: "#CC0000",
		                    timeout: 4000
		                });
		                $("#imgLoad2").hide();
		            }

		        //}
		    }
		    function jChange() {
		        if (document.getElementById('txtconfirmpassword').value == document.getElementById('txtChangePassword').value) {
		            var obj = {};
		            obj.value = document.getElementById('txtUsername').value;
		            obj.pass = document.getElementById('txtChangePassword').value;
		            //var value = $("#txtApplicants").val()
		            var jsonData = JSON.stringify(obj);
		            $.ajax({
		                type: "POST",
		                url: "Login.aspx/_updatePassword",
		                data: jsonData,
		                contentType: "application/json; charset=UTF-8",
		                dataType: "json", // dataType is json format
		                success: OnSuccess,
		                error: OnErrorCall
		            });
		            function OnSuccess(response) {
		                if (String(response.d) == "True%Dashboard.aspx") {
		                    window.location.replace('Dashboard.aspx');
		                }
		            }
		        }
		        else {
		            $.bigBox({
		                title: "Password mismatch",
		                content: "Please reconfirm the passwords",
		                color: "Red",
		                timeout: 4000
		            });
		        }
		        
		        function OnErrorCall(response) {
		            $.bigBox({
		                title: "Error",
		                content: "Error Encountered or System Timed-out",
		                color: "#CC0000",
		                timeout: 4000
		            });
		        }
		    }
		    function jReset() {
		        jLoginDisplay();
		        //jQuery("#imgLoad1").addClass('visible');
		    }



		</script> 
	

</body>
</html>
