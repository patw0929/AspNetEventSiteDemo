<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="BM_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Admin Template - Login</title>
<link href="styles/layout.css" rel="stylesheet" type="text/css" />
<link href="styles/login.css" rel="stylesheet" type="text/css" />
<!-- Theme Start -->
<link href="themes/blue/styles.css" rel="stylesheet" type="text/css" />
<!-- Theme End -->
</head>
<body>
    <form id="form1" runat="server">
	<div id="logincontainer">
    	<div id="loginbox">
        	<div id="loginheader">
            	<img src="themes/blue/img/cp_logo_login.png" alt="Control Panel Login" />
            </div>
            <div id="innerlogin">
            	<form action="index.html">
                	<p>帳號:</p>
                	<asp:TextBox ID="txtAct" runat="server" CssClass="logininput"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ActVD1" runat="server" 
                          ControlToValidate="txtAct" Display="Dynamic" ErrorMessage="請輸入帳號">*</asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="ActVD2" runat="server" 
                          ErrorMessage="帳號必須為英文或數字" ControlToValidate="txtAct" 
                          Display="Dynamic" ValidationExpression="\w+">*</asp:RegularExpressionValidator>
                    <p>密碼:</p>
                	<asp:TextBox ID="txtPWD" runat="server" TextMode="Password" CssClass="logininput"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PWDVD1" runat="server" 
                          ControlToValidate="txtPWD" Display="Dynamic" ErrorMessage="請輸入密碼">*</asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="PWDVD2" runat="server" 
                          ControlToValidate="txtPWD" Display="Dynamic" ValidationExpression="\w+" 
                          ErrorMessage="密碼必須為英文或數字">*</asp:RegularExpressionValidator>
                    <p>驗證碼:</p>
                	<asp:TextBox ID="VCode" runat="server" MaxLength="6" CssClass="logininput"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="VCodeVD1" runat="server" 
                          ControlToValidate="VCode" Display="Dynamic" ErrorMessage="請輸入驗證碼">*</asp:RequiredFieldValidator> 
					    <asp:Image ID="imgCode" runat="server" ImageAlign="AbsMiddle" />
                   
                   	<asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" CssClass="loginbtn" Text="登入" /><br />
                </form>
            </div>
        </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" 
          ShowSummary="False" />
        <img src="img/login_fade.png" alt="Fade" />
    </div>
    </form>
</body>
</html>
