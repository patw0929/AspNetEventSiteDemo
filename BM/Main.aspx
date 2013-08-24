<%@ Page Title="" Language="C#" MasterPageFile="~/BM/MasterPage.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="BM_main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBreadcrumb" Runat="Server">

    <!-- Top Breadcrumb Start -->
    <div id="breadcrumb">
    	<ul>	
        	<li><img src="img/icons/icon_breadcrumb.png" alt="Location" /></li>
        	<li><strong>Location:</strong></li>
            <li><a href="main.aspx" title="">後台</a></li>
            <li>/</li>
            <li class="current">歡迎頁</li>
        </ul>
    </div>
    <!-- Top Breadcrumb End -->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">
        
        <div class="status info">
        	<p><img src="img/icons/icon_info.png" alt="Information" /><span>您好，請選擇左方功能列進行管理。</span></p>
        </div>

</asp:Content>

