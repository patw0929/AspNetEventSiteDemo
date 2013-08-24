<%@ Page Title="" Language="C#" MasterPageFile="~/BM/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Draw.aspx.cs" Inherits="BM_Draw" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="patwControls" Namespace="patwControls.patwGridView" TagPrefix="patw" %>
<%@ Register Assembly="patwControls" Namespace="patwControls" TagPrefix="patw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1 {
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBreadcrumb" Runat="Server">

    <!-- Top Breadcrumb Start -->
    <div id="breadcrumb">
    	<ul>	
        	<li><img src="img/icons/icon_breadcrumb.png" alt="Location" /></li>
        	<li><strong>Location:</strong></li>
            <li><a href="Main.aspx" title="">後台</a></li>
            <li>/</li>
            <li>名單</li>
            <li>/</li>
            <li class="current"><a href="Draw.aspx">抽獎</a></li>
        </ul>
    </div>
    <!-- Top Breadcrumb End -->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" Runat="Server">

    <div class="style1">
        抽出 <asp:TextBox ID="tbDrawCount" runat="server" Text="15"></asp:TextBox> 名（中獎名額 <asp:Label ID="lbMaxQuota" runat="server" Text=""></asp:Label>）
        <br /><br />
        抽獎：
        <asp:Button ID="btnDraw" runat="server" Text="抽獎" OnClick="btnDraw_Click" CssClass="btn" />
        <asp:Button ID="btnExport" runat="server" Text="匯出成 Excel" OnClick="btnExport_Click" Visible="false" CssClass="btnalt" />
        <asp:Button ID="btnSave" runat="server" Text="存入" onclick="btnSave_Click" Visible="false" CssClass="btnalt" />

        <br />
        <br />
        <asp:Label ID="lbTotal" runat="server" Text=""></asp:Label>
    </div>

    <patw:patwGridView ID="patwGridView1" runat="server" BackColor="White"
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" EmptyShowHeader="True" 
        EmptyDataText="沒有任何資料。" DataKeyNames="sID" 
        AutoGenerateColumns="false" HorizontalAlign="Center">
        <Columns>
        
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sID" HeaderText="sID"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sFBUID" HeaderText="sFBUID"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sFBDisplayName" HeaderText="sFBDisplayName"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sName" HeaderText="sName"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sGender" HeaderText="sGender"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sBirth" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="sBirth"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sFBEmail" HeaderText="sFBEmail"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sEmail" HeaderText="sEmail"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sMobile" HeaderText="sMobile"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sLocation" HeaderText="sLocation"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sIP" HeaderText="sIP"></patw:patwBoundField>
        	<patw:patwTemplateField HeaderText="狀態" Visible="True" IsExport="True">
                <ItemTemplate>
                    <patw:patwButton ID="btnValid" Text='<%#Eval("sValid")%>' UseBooleanValue="true" TrueText="有效" FalseText="無效" CommandName="UpdatasValid" CommandArgument='<%#Eval("sID") %>' runat="server" />
                </ItemTemplate>
            </patw:patwTemplateField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sCreatetime" HeaderText="sCreatetime"></patw:patwBoundField>
        	<patw:patwBoundField Visible="True" IsExport="True" DataField="sWin" HeaderText="sWin"></patw:patwBoundField>
        </Columns>
    </patw:patwGridView>
    <br />
    <br />
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首頁" InvalidPageIndexErrorMessage="頁索引不是有效的數值"
        LastPageText="最後一頁" NextPageText="下一頁" PageIndexBoxType="DropDownList" PageIndexOutOfRangeErrorMessage="頁索引超出範圍"
        PrevPageText="上一頁" ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="頁"
        TextBeforePageIndexBox="跳到第 " CustomInfoHTML="目前第 %CurrentPageIndex% 頁 / 共 %PageCount% 頁"
        Font-Size="15px" UrlPaging="false" 
        OnPageChanged="AspNetPager1_PageChanged" HorizontalAlign="Center">
    </webdiyer:AspNetPager>

</asp:Content>
