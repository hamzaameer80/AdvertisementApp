<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" EnableEventValidation="false" %>
<!DOCTYPE HTML>
<html>
<head>
<meta charset="utf-8">
<title>Fanancial</title>
<meta http-equiv="cache-control" content="max-age=0" />
<meta http-equiv="cache-control" content="no-cache" />
<meta http-equiv="expires" content="0" />
<meta http-equiv="pragma" content="no-cache" />
<link rel="stylesheet" href="tdc_resources/css/style_tdc.css" type="text/css" />
<script type="text/javascript" src="tdc_resources/js/jquery.min.js"></script>
<script type="text/javascript" src="tdc_resources/js/js.cookie.js"></script>
<script type="text/javascript" src="Scripts/rt.min.js"></script>
<link rel="icon" type="image/png" href="tdc_resources/images/favicon.ico">
<style type="text/css">
 
a.btn_hacims {
	display: inline-block;
	width: 150px;
	padding-left: 20px;
	padding-right: 20px;
	height: 38px;
	color: #fff;
	background: #17469e;
	text-decoration: none;
	font-size: 18px;
	text-align: center;
	line-height: 40px;
	border-radius: 4px;
	transition: background-color 0.2s ease-in-out 0s, color 0.2s ease-in-out 0s, opacity 0.2s ease-in-out 0s;
	-moz-box-sizing: border-box;
	-webkit-box-sizing: border-box;
	box-sizing: border-box;
	outline: none;
	font-family: "Myriad Pro";
	border: 0px;
	cursor: pointer;
	max-width: 160px;
	float: right;
	font-size: 16px;
	font-weight: 500;
}
a.btn_hacims:hover, a.btn_hacims:focus, a.btn_hacims:active {
	outline: none;
	background: #00aae9;
}
.input_txt {
	border: none;
	background: #fff;
	color: #555;
	border-radius: 4px;
	height: 42px;
	line-height: 42px;
	width: 100%;
	text-align: left;
	outline: none;
	border: #d9dbdd solid 1px;
	box-sizing: border-box;
}
.input_txt:focus {
	outline: none;
	border: #0099CC solid 1px;
}
   
   @media screen and (min-width: 800px) {
	   
     body { overflow:hidden;
    }
	
}
   
    
#logo_tdc {
	background: url(tdc_resources/images/finance_logo.png) 0px 0px no-repeat; background-size:contain;
	width: 150px;
	height: 75px;
	display: block;
	margin: 0 auto;
	margin-top:13px; margin-left:25px;
}	

.content_login {
	background: url(tdc_resources/images/BackgroundImage.jpg) 0px 0px no-repeat;
	background-size:100%;
	width: 100%;
	float: left;
	margin-top1: 10px;
}
	.spinc {
	 
	margin: 0 auto;
	top: -20px;
} 
</style>
<script type="text/javascript">
	
  $(document).ready(function(e) { 
    $("#txtusername").attr("placeholder","User Name");
	$("#txtPassword").attr("placeholder","Password");
 	 if($(".any_err").html()!=undefined){ $("#message").show(); $(".login_bx").css("min-height","300px"); }
   	$("#Regex2").hide();
	//$.cookie("selLink", null, { path: '/' });
	
	Cookies.set('pagetitle', null);
	Cookies.set('tabtitle', null);
	
	 		var cdate = new Date();
            var cyear = cdate.getFullYear();
            $("#cyear").html(cyear);
			
	 var opts = {
        lines: 11, // lines 
        length: 5, // line
        width: 2, // thickness
        radius: 5, // inner circle
        rotate: 0, // offset
        color: '#fff',
        speed: 1,
        trail: 60,
        shadow: false,
        hwaccel: false,
        className: 'spinc',
        zIndex: 2e9,
        top: 'auto',
        left: 'auto'
   		 };
    var spinner = new Spinner(opts);
	// spinner.stop();
	// spinner.spin();	
   	//$(".btn_hacims").append(spinner.el);
	//$(".btn_hacims").css("text-indent", "-9999px");		
			
		$(document).on("click",".btn_hacims",function(e) {
		
		  spinner.spin();	
   		 $(".btn_hacims").append(spinner.el);
	     $(".btn_hacims").css("text-indent", "-9999px");
		// spinner.stop(); 
	 
		
	});	
	
	
	//
//	   var features =  ""  
//                        + "menubar=no,toolbar=no,location=no,personalbar=no"
//                        + ",status=no,chrome=yes"
//                        + ",width=1350"        //Width of content window
//                        + ",height=700"       //Height of content window
//                        //+ ",outerWidth=1200"   //Width of window
//                      //  + ",outerHeight=1200"  //Height of window
//                     //   + ",top=0"
//                     //   + ",left=0"	
//                        ;
//        window.open("http://192.168.12.40/TDC/login.aspx","_self",features);
 
	// $(document).on('keyup',"#txtPassword",function(event){		
//	 event.preventDefault();
//		 if (event.keyCode === 13) {
//		  
//   				 $("#btnLogin").trigger('click');
//	
//  }
//	    
//        });	
	
   });
  
  
</script>
</head>
<body>
<header>
  <!--<div id="logo_tdc"><span></span></div>-->
   <div id="logo_tdc"></div> 
</header>
<div class="content_login">
  <div class="login_bx"  >
    <input id="tab-1" type="radio" name="tab" style="display:none;" class="sign-in" checked>
    <form id="form1" runat="server" autocomplete="off">
      <ul class="form_signin">
        <li><h2 style="font-size:20px; font-weight:bold; width:100%; position:relative; text-align:center; float:left; margin-top:-43px; margin-left1:25%;">User Login</h2></li>
        <li  id="message" style="height:44px;margin-bottom:10px; display:none;" >
          <asp:Label ID="lblMessage" CssClass="any_err" runat="server"></asp:Label>
        </li>
        <li>
          <asp:TextBox ID="txtusername" CssClass="input_txt" style="padding-left: 30px; background:#fff url(tdc_resources/images/txt_icons.png) 5px 7px no-repeat;" runat="server" ValidationGroup="log" AutoCompleteType="disabled" TabIndex="1"></asp:TextBox>
        </li>
        <li style="margin-top:15px;">
          <asp:TextBox ID="txtPassword" CssClass="input_txt"  Style="padding-left:30px; background:#fff url(tdc_resources/images/txt_icons.png) 5px -97px no-repeat;" runat="server" ValidationGroup="log" MaxLength="40" TextMode="Password" TabIndex="2"></asp:TextBox>
          <asp:RegularExpressionValidator ValidationGroup="log" ID="Regex2" runat="server" ControlToValidate="txtPassword"
                                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$" Display="dynamic"
                                            ErrorMessage="**" ForeColor="Red" ToolTip="Minimum 6 characters atleast 1 Alphabet, 1 Number and 1 Special Character" />
          <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </li>
        <li style="margin-top: 15px;"><a href="forgotpassword.aspx"
								class="fpassword_link" tabindex="4" style="margin-top:10px; float:left;">Forgot password?</a>
          <asp:linkButton ID="btnLogin" CssClass="btn_hacims"  runat="server" ValidationGroup="log" CausesValidation="true"  Text="Log In" TabIndex="3"></asp:LinkButton>
        </li>
      </ul>
    </form>
  </div>
  <!--login_bx--> 
  
</div>
<!--content_login-->

<%-- <div class="copyright">&copy; <span id="cyear"></span> - All rights reserved - <a href="#" target="_blank">Power By Tab-Sole Technologies</a></div>--%>
    
</body>
</html>
<asp:Label ID="LabelDate" runat="server" Style="display:none;"></asp:Label>
<asp:sqldatasource runat="server" ConnectionString="<%$ ConnectionStrings:FixedAssetConnectionString2 %>" SelectCommand="SELECT * FROM [Assest_Warrenty_Record]"></asp:sqldatasource>
