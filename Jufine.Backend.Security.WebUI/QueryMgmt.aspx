<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True"
    CodeBehind="QueryMgmt.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.QueryMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .queryMgmtHeader
        {
            white-space: nowrap;
            margin: 1px 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="plHeader" runat="server" CssClass="tools_bar">
        <div class="toolbg toolbgline toolheight nowrap" style="">
            <div class="right">
            </div>
            <div class="nowrap left">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                    Text="查询" /><asp:Button ID="btnHide" runat="server" Text="btn_hide" Visible="false" />
            </div>
            <div class="clr">
                &nbsp;</div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
                <tr>
                    <td style="width: 600px">
                        <asp:TextBox ID="stxtID" TextMode="MultiLine" Width="600" Height="200" runat="server"></asp:TextBox>
                    </td>
                    <td valign="top" style="white-space: normal">
                        <span id="Span2" style="color: Red">*</span>
                        <ol>                        
                            <li>1.<b>请使用TOP</b></li>
                            <li>2.<b>请写明选择列，请勿使用 * </b></li>
                            <li>3.<b>查询语句中不能包含以下关键字：</b>"update","delete","insert","create","alter","drop","grant","deny","revoke","exec","truncate"</li>
                            <li>4.任何查询都会记录到日志文件</li>
                        </ol>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="querydiv">
                <asp:GridView ID="queryList" runat="server" AutoGenerateColumns="True" HeaderStyle-CssClass="queryMgmtHeader"
                    CssClass="business_list" Width="500">
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
