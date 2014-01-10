<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True" CodeBehind="MerchantAdditionalCertificateMgmt.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.MerchantAdditionalCertificateMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <asp:Panel ID="plHeader" runat="server" DefaultButton="btnSearch" CssClass="tools_bar">
        <div class="toolbg toolbgline toolheight nowrap" style="">
            <div class="right">
            </div>
            <div class="nowrap left">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click" Text="Search" />
                <asp:Button ID="btnCreate" runat="server" CssClass="btn" OnClick="btnCreate_Click" Text="Create" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="Delete" OnClientClick="return DeleteConfirm('ckbSelect');" OnClick="btnDelete_Click" />
            </div>
            <div class="clr">
                &nbsp;</div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
<tr>                <td style="width: 80px;">
                      ID
                    </td>
                    <td>
                        <asp:TextBox ID="stxtID" PropertyName="ID" Width="300" runat="server"></asp:TextBox>
                    </td>
                
                <td style="width: 80px;">
                      MerchantID
                    </td>
                    <td>
                        <asp:TextBox ID="stxtMerchantID" PropertyName="MerchantID" Width="300" runat="server"></asp:TextBox>
                    </td>
                
</tr><tr>                <td style="width: 80px;">
                      ImangeName
                    </td>
                    <td>
                        <asp:TextBox ID="stxtImangeName" PropertyName="ImangeName" Width="300" runat="server"></asp:TextBox>
                    </td>
                
                <td style="width: 80px;">
                      Title
                    </td>
                    <td>
                        <asp:TextBox ID="stxtTitle" PropertyName="Title" Width="300" runat="server"></asp:TextBox>
                    </td>
                
</tr><tr>                <td style="width: 80px;">
                      DisplayOrder
                    </td>
                    <td>
                        <asp:TextBox ID="stxtDisplayOrder" PropertyName="DisplayOrder" Width="300" runat="server"></asp:TextBox>
                    </td>
                
                <td style="width: 80px;">
                      Status
                    </td>
                    <td>
                        <asp:TextBox ID="stxtStatus" PropertyName="Status" Width="300" runat="server"></asp:TextBox>
                    </td>
                
</tr><tr>                <td style="width: 80px;">
                      CreateUser
                    </td>
                    <td>
                        <asp:TextBox ID="stxtCreateUser" PropertyName="CreateUser" Width="300" runat="server"></asp:TextBox>
                    </td>
                
                <td style="width: 80px;">
                      CreateDate
                    </td>
                    <td>
                        <asp:TextBox ID="stxtCreateDate" PropertyName="CreateDate" Width="300" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDate" runat="server" TargetControlID="stxtCreateDate" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                        -<asp:TextBox ID="stxtCreateDateTo" PropertyName="CreateDateTo" Width="300" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDateTo" runat="server" TargetControlID="stxtCreateDateTo" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                    </td>
                
</tr><tr>                <td style="width: 80px;">
                      EditUser
                    </td>
                    <td>
                        <asp:TextBox ID="stxtEditUser" PropertyName="EditUser" Width="300" runat="server"></asp:TextBox>
                    </td>
                
                <td style="width: 80px;">
                      EditDate
                    </td>
                    <td>
                        <asp:TextBox ID="stxtEditDate" PropertyName="EditDate" Width="300" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeEditDate" runat="server" TargetControlID="stxtEditDate" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                        -<asp:TextBox ID="stxtEditDateTo" PropertyName="EditDateTo" Width="300" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeEditDateTo" runat="server" TargetControlID="stxtEditDateTo" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                    </td>
                
</tr>            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
                    <asp:GridView ID="gvMerchantAdditionalCertificateList" 
                        runat="server" 
                        OnSorting="gvMerchantAdditionalCertificateList_Sorting" 
                        AutoGenerateColumns="False" 
                        AllowSorting="true" 
                        CssClass="business_list">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="ckbSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" runat="server"  ToolTip='<%# Eval("ID") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="60" HeaderText="Edit">
                                <ItemTemplate>
                                   <asp:LinkButton ID="lnkEdit" runat="server" RowIndex='<%#Container.DataItemIndex %>' CommandArgument='<%# Eval("ID") %>' OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ItemStyle-Width="60" />
                            <asp:BoundField DataField="MerchantID" HeaderText="MerchantID" SortExpression="MerchantID" ItemStyle-Width="60" />
                            <asp:BoundField DataField="ImangeName" HeaderText="ImangeName" SortExpression="ImangeName" ItemStyle-Width="60" />
                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" ItemStyle-Width="60" />
                            <asp:BoundField DataField="DisplayOrder" HeaderText="DisplayOrder" SortExpression="DisplayOrder" ItemStyle-Width="60" />
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ItemStyle-Width="60" />
                            <asp:BoundField DataField="CreateUser" HeaderText="CreateUser" SortExpression="CreateUser" ItemStyle-Width="60" />
                            <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" ItemStyle-Width="60" />
                            <asp:BoundField DataField="EditUser" HeaderText="EditUser" SortExpression="EditUser" ItemStyle-Width="60" />
                            <asp:BoundField DataField="EditDate" HeaderText="EditDate" SortExpression="EditDate" ItemStyle-Width="60" />
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
        </contenttemplate>
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="listPager" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnDelete" />
        </triggers>
    </asp:UpdatePanel>
    <asp:Panel CssClass="miniWindow" ID="plDetail" runat="server" Style="display: none; width: 680px;">
                        <div class="container">
                            <asp:Panel ID="plTitle" Style="cursor: move;" runat="server">
                                <div class="" id="miniWindow_close">
                                </div>
                                <div class="t" id="miniWindow_handle">
                                    <div class="l">
                                    </div>
                                    <div class="title">
                                      Add or update MerchantAdditionalCertificate</div>
                                    <div class="r">
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="c">
                                <asp:UpdatePanel ID="upDetail" runat="server" UpdateMode="Conditional">
                                    <contenttemplate>
                                        <asp:Panel ID="panelDetailInputArea" runat="server" DefaultButton="btnSave">
                                        <asp:HiddenField ID="hdID" runat="server" />
                                            <div class="c1">
                                                    <table style="width: 100%;">
<tr>                                                        <td style="width: 80px;">
                                                            ID
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtID" PropertyName="ID" MaxLength="4" Width="200" runat="server"></asp:TextBox>
                                                            </td>
                                                        
                                                        <td style="width: 80px;">
                                                            MerchantID
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMerchantID" PropertyName="MerchantID" MaxLength="4" Width="200" runat="server"></asp:TextBox>
                                                            </td>
                                                        
</tr><tr>                                                        <td style="width: 80px;">
                                                            ImangeName
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtImangeName" PropertyName="ImangeName" MaxLength="200" Width="200" runat="server"></asp:TextBox>
                                                            </td>
                                                        
                                                        <td style="width: 80px;">
                                                            Title
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTitle" PropertyName="Title" MaxLength="200" Width="200" runat="server"></asp:TextBox>
                                                            </td>
                                                        
</tr><tr>                                                        <td style="width: 80px;">
                                                            DisplayOrder
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDisplayOrder" PropertyName="DisplayOrder" MaxLength="4" Width="200" runat="server"></asp:TextBox>
                                                            </td>
                                                        
                                                        <td style="width: 80px;">
                                                            Status
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtStatus" PropertyName="Status" MaxLength="4" Width="200" runat="server"></asp:TextBox>
                                                            </td>
                                                        
</tr><tr>                                                        <td style="width: 80px;">
                                                            CreateUser
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCreateUser" PropertyName="CreateUser" MaxLength="50" Width="200" runat="server"></asp:TextBox>
                                                            </td>
                                                        
                                                        <td style="width: 80px;">
                                                            CreateDate
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCreateDate" PropertyName="CreateDate" MaxLength="20" Width="200" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="cdeCreateDate" runat="server" TargetControlID="txtCreateDate" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                                                            </td>
                                                        
</tr><tr>                                                        <td style="width: 80px;">
                                                            EditUser
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEditUser" PropertyName="EditUser" MaxLength="50" Width="200" runat="server"></asp:TextBox>
                                                            </td>
                                                        
                                                        <td style="width: 80px;">
                                                            EditDate
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEditDate" PropertyName="EditDate" MaxLength="20" Width="200" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="cdeEditDate" runat="server" TargetControlID="txtEditDate" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                                                            </td>
                                                        
</tr>                                                    </table>
                            </div>
                            </asp:Panel>
                            <div class="prenext">
                            <asp:Button ID="btnPreviousItem" runat="server" Text="<上一条" OnClick="btnPreviousItem_Click" />
                            <asp:Button ID="btnNextItem" runat="server" Text="下一条>" OnClick="btnNextItem_Click" />
                            </div>
                        </ContentTemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnPreviousItem" />
                                <asp:AsyncPostBackTrigger ControlID="btnNextItem" />
                                <asp:AsyncPostBackTrigger ControlID="btnCreate" />
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                            </triggers>
                        </asp:UpdatePanel>
                        <div class="saveexit">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="Exit" />
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="modalPopupExtender" runat="server" TargetControlID="OkButton" PopupControlID="plDetail" BackgroundCssClass="modalBackground" Drag="true" PopupDragHandleControlID="plTitle">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="OkButton" runat="server" Text="" />
</asp:Content>