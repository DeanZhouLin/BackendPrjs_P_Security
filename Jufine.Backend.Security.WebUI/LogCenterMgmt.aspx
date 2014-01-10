<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True"
    CodeBehind="LogCenterMgmt.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.LogCenterMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="plHeader" runat="server" DefaultButton="btnSearch" CssClass="tools_bar">
        <div class="toolbg toolbgline toolheight nowrap" style="">
            <div class="right">
            </div>
            <div class="nowrap left">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                    Text="Search" />
                <asp:Button ID="btnCreate" runat="server" CssClass="btn" OnClick="btnCreate_Click"
                    Visible="false" Text="Create" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="Delete" OnClientClick="return DeleteConfirm('ckbSelect');"
                    Visible="false" OnClick="btnDelete_Click" />
            </div>
            <div class="clr">
                &nbsp;</div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
                <tr>
                    <td style="width: 120px;">
                        ID
                    </td>
                    <td>
                        <asp:TextBox ID="stxtID" PropertyName="ID" Width="300" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 120px;">
                        ApplicationName
                    </td>
                    <td>
                        <asp:DropDownList data-placeholder="请选择" Width="300" ID="sddlApplicationNameID" PropertyName="ApplicationName"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        Module
                    </td>
                    <td>
                        <asp:DropDownList data-placeholder="请选择" Width="300" ID="sddlModuleID" PropertyName="Module"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 120px;">
                        LogType
                    </td>
                    <td>
                        <asp:DropDownList data-placeholder="请选择" Width="300" ID="sddlLogTypeID" PropertyName="LogType"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        Title
                    </td>
                    <td>
                        <asp:TextBox ID="stxtTitle" PropertyName="Title" Width="300" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 120px;">
                        Detail
                    </td>
                    <td>
                        <asp:TextBox ID="stxtDetail" PropertyName="Detail" Width="300" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        LogTime
                    </td>
                    <td>
                        <asp:TextBox ID="stxtLogTime" PropertyName="LogTime" Width="300" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeLogTime" runat="server" TargetControlID="stxtLogTime"
                            Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                        -<asp:TextBox ID="stxtLogTimeTo" PropertyName="LogTimeTo" Width="300" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeLogTimeTo" runat="server" TargetControlID="stxtLogTimeTo"
                            Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:GridView ID="gvLogCenterList" runat="server" OnSorting="gvLogCenterList_Sorting"
                AutoGenerateColumns="False" AllowSorting="true" CssClass="business_list">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ckbSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" ToolTip='<%# Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60" HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" RowIndex='<%#Container.DataItemIndex %>'
                                CommandArgument='<%# Eval("ID") %>' OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ItemStyle-Width="60" />
                    <asp:BoundField DataField="ApplicationName" HeaderText="ApplicationName" SortExpression="ApplicationName"
                        ItemStyle-Width="120" />
                    <asp:BoundField DataField="Module" HeaderText="Module" SortExpression="Module" ItemStyle-Width="120" />
                    <asp:BoundField DataField="LogType" HeaderText="LogType" SortExpression="LogType"
                        ItemStyle-Width="120" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="LogTime" HeaderText="LogTime" SortExpression="LogTime"
                        ItemStyle-Width="120" />
                </Columns>
            </asp:GridView>
            <div class="pagination">
                <asp:ListPager Width="100%" ID="listPager" runat="server" FirstPageText="首页" LastPageText="尾页"
                    NextPageText="下一页" OnPageChanged="listPager_PageChanged" PageSize="15" PrevPageText="上一页"
                    ShowPageIndexBox="Always" PageIndexBoxType="TextBox" ShowNavigationToolTip="True"
                    CustomInfoTextAlign="Left" ShowCustomInfoSection="Left" CustomInfoHTML="&nbsp;&nbsp;第 %CurrentPageIndex% 页，共 %PageCount% 页"
                    SubmitButtonClass="pages_butt" TextBeforePageIndexBox="到第" TextAfterPageIndexBox="页  "
                    CustomInfoStyle="padding-top:3px!important;padding-top:6px;height:20px;" CustomInfoSectionWidth="20%">
                </asp:ListPager>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="listPager" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnDelete" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel CssClass="miniWindow" ID="plDetail" runat="server" Style="display: none;
        width: 900px;">
        <div class="container">
            <asp:Panel ID="plTitle" Style="cursor: move;" runat="server">
                <div class="" id="miniWindow_close">
                </div>
                <div class="t" id="miniWindow_handle">
                    <div class="l">
                    </div>
                    <div class="title">
                        Add or update LogCenter</div>
                    <div class="r">
                    </div>
                </div>
            </asp:Panel>
            <div class="c">
                <asp:UpdatePanel ID="upDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="panelDetailInputArea" runat="server" DefaultButton="btnSave">
                            <asp:HiddenField ID="hdID" runat="server" />
                            <div class="c1">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">
                                            ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtID" PropertyName="ID" MaxLength="8" Width="200" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 120px;">
                                            ApplicationName
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtApplicationName" PropertyName="ApplicationName" MaxLength="500"
                                                Width="200" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">
                                            Module
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtModule" PropertyName="Module" MaxLength="500" Width="200" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 120px;">
                                            LogType
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLogType" PropertyName="LogType" MaxLength="50" Width="200" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">
                                            Title
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTitle" PropertyName="Title" MaxLength="500" Width="200" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 120px;">
                                            LogTime
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLogTime" PropertyName="LogTime" MaxLength="20" Width="200" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            Detail
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtDetail" TextMode="MultiLine" PropertyName="Detail" Rows="20"
                                                MaxLength="16" Width="98%" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <div class="prenext">
                            <asp:Button ID="btnPreviousItem" runat="server" Text="<上一条" OnClick="btnPreviousItem_Click" />
                            <asp:Button ID="btnNextItem" runat="server" Text="下一条>" OnClick="btnNextItem_Click" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnPreviousItem" />
                        <asp:AsyncPostBackTrigger ControlID="btnNextItem" />
                        <asp:AsyncPostBackTrigger ControlID="btnCreate" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                        <asp:AsyncPostBackTrigger ControlID="sddlModuleID" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="saveexit">
                    <asp:Button ID="btnSave" Visible="false" runat="server" OnClick="btnSave_Click" Text="Save" />
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="退出" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="modalPopupExtender" runat="server" TargetControlID="OkButton"
        PopupControlID="plDetail" BackgroundCssClass="modalBackground" Drag="true" PopupDragHandleControlID="plTitle">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="OkButton" runat="server" Text="" />
</asp:Content>
