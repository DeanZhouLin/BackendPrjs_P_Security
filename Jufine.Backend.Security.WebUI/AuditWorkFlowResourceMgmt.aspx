<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True" CodeBehind="AuditWorkFlowResourceMgmt.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.AuditWorkFlowResourceMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="plHeader" runat="server" DefaultButton="btnSearch" CssClass="tools_bar">
        <div class="toolbg toolbgline toolheight nowrap" style="">
            <div class="right">
            </div>
            <div class="nowrap left">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click" Text="查询" />
                <asp:Button ID="btnCreate" runat="server" CssClass="btn" OnClick="btnCreate_Click" Text="新增" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="删除"
                    OnClientClick="return DeleteConfirmByTitle('ckbSelect','数据删除后将无法恢复，是否确认删除');" OnClick="btnBatchDelete_Click" />
            </div>
            <div class="clr">
                &nbsp;
            </div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
                <tr>
                    <td style="width: 100px;">多级审核名称：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtDisplayName" PropertyName="DisplayName" Width="200" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 80px;">资源名称：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtResourceName" PropertyName="ResourceName" Width="200" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 80px;">资源ID：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtResourceID" PropertyName="ResourceID" Width="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:GridView ID="gvAuditWorkFlowResourceList"
                runat="server"
                OnSorting="gvAuditWorkFlowResourceList_Sorting"
                AutoGenerateColumns="False"
                AllowSorting="true"
                CssClass="business_list">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ckbSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" ToolTip='<%# Eval("ResourceID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="85" HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server"
                                RowIndex='<%#Container.DataItemIndex %>'
                                CommandArgument='<%# Eval("ResourceID") %>'
                                OnClick="lnkEdit_Click">编辑</asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server"
                                CommandArgument='<%# Eval("ResourceID") %>'
                                OnClick="lnkDelete_Click" OnClientClick="return confirm('数据删除后将无法恢复，是否确认删除')">删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DisplayName" HeaderText="多级审核名称" SortExpression="DisplayName" />
                    <asp:BoundField DataField="TotalLevel" HeaderText="当前审批总级数" SortExpression="TotalLevel" ItemStyle-Width="150" />
                    <asp:BoundField DataField="ResourceID" HeaderText="资源ID" SortExpression="ResourceID" />
                    <asp:BoundField DataField="ResourceName" HeaderText="资源名称" SortExpression="ResourceName" />
                    <asp:BoundField DataField="ResourceDisplayName" HeaderText="资源显示名称" SortExpression="ResourceDisplayName" />

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
    <asp:Panel CssClass="miniWindow" ID="plDetail" runat="server" Style="display: none; width: 1100px;">
        <div class="container">
            <asp:Panel ID="plTitle" Style="cursor: move;" runat="server">
                <div class="" id="miniWindow_close">
                </div>
                <div class="t" id="miniWindow_handle">
                    <div class="l">
                    </div>
                    <div class="title">
                        <asp:UpdatePanel ID="upTitle" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label runat="server" ID="lblTitle" Text="新增"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="r">
                    </div>
                </div>
            </asp:Panel>
            <div class="c">
                <asp:UpdatePanel ID="upDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="panelDetailInputArea" runat="server">
                            <asp:HiddenField ID="hdID" runat="server" />
                            <div class="c1">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 240px; padding: 5px;" rowspan="5">
                                            <asp:Panel ID="TreeViewPanel" ScrollBars="Auto" BorderWidth="1" BorderStyle="Inset" Width="240px" Height="500px" runat="server">
                                                <span style="padding: 5px;"><strong>系统资源：</strong></span>
                                                <asp:UpdatePanel ID="upAuthorityTreeView" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TreeView ID="treeAuth" OnTreeNodeCheckChanged="treeAuth_TreeNodeCheckChanged"
                                                            OnSelectedNodeChanged="treeAuth_TreeNodeSelectedChanged"
                                                            NodeStyle-BorderStyle="Solid" ShowLines="True" BorderWidth="0px" Font-Size="13px"
                                                            ForeColor="#333333" SelectedNodeStyle-ForeColor="#ff6600" SelectedNodeStyle-Font-Bold="True"
                                                            SelectedNodeStyle-Font-Size="Larger" ParentNodeStyle-BorderStyle="Dotted" runat="server">
                                                            <ParentNodeStyle BorderStyle="None" />
                                                            <NodeStyle BorderStyle="None" />
                                                        </asp:TreeView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 80px;">选择资源：
                                        </td>
                                        <td style="width: 150px">

                                            <asp:TextBox ID="txtResourceName" Enabled="False" PropertyName="ResourceName" ClientIDMode="Static" MaxLength="100" Width="130px" runat="server"></asp:TextBox>
                                            <input type="hidden" id="hdResourceID" runat="server" />
                                        </td>
                                        <td style="width: 80px;">选择用户：
                                        </td>
                                        <td style="width: 250px">
                                            <asp:DropDownList data-placeholder="全部用户" Width="230px" ID="sddlUserInfo" DataTextField="UserName" ClientIDMode="Static"
                                                DataValueField="ID" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 80px;">设置审批等级：
                                        </td>
                                        <td style="width: 50px">
                                            <asp:DropDownList data-placeholder="全部等级" Width="50px" ID="sddlAuditLevel" ClientIDMode="Static" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <div class="toolbg toolbgline toolheight nowrap" style="">
                                                <div class="right">
                                                </div>
                                                <div class="nowrap right">
                                                     <%--OnClientClick="return ConfirmAddWorkFlowResource()"--%>
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="btnAddAuditUserForAuditWorkFlow_Click" Text="分配权限" />
                                                </div>
                                                <div class="clr">
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Panel ID="plAuditResourceList" ScrollBars="Auto" BorderWidth="1" BorderStyle="Inset" Height="440px" runat="server">
                                                <asp:UpdatePanel ID="upAuditResourceList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvAuditResourceList"
                                                            runat="server"
                                                            AutoGenerateColumns="False"
                                                            CssClass="business_list">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="35" HeaderText="操作">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete"
                                                                            runat="server"
                                                                            RowIndex='<%#Container.DataItemIndex %>'
                                                                            ResourceID='<%#Eval("ResourceID")%>'
                                                                            UserName='<%#Eval("UserName")%>'
                                                                            OnClick="lnkDeleteAuditUserForAuditWorkFlow_Click"
                                                                            OnClientClick="return confirm('是否确定删除该条数据')">删除</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="DisplayName" HeaderText="审核名称" ItemStyle-Width="150" />
                                                                <asp:BoundField DataField="ResourceDisplayName" HeaderText="资源显示名称" ItemStyle-Width="130" />
                                                                <asp:BoundField DataField="ResourceName" HeaderText="资源名称" />
                                                                <asp:BoundField DataField="UserName" HeaderText="用户名称" ItemStyle-Width="120" />
                                                                <asp:BoundField DataField="AuditLevel" HeaderText="审核等级" ItemStyle-Width="50" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
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
                        <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="saveexit">
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="退出" />
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
    <script type="text/javascript">
        function ConfirmAddWorkFlowResource() {
            var resourceName = $("#txtResourceName").val();
            var userName = $("#sddlUserInfo").find("option:selected").text();
            var auditLevel = $("#sddlAuditLevel").val();

            var confirmMsg = "确定分配给用户【" + userName + "】对资源【" + resourceName + "】的第【" + auditLevel + "】级控制权限";
            return confirm(confirmMsg);
        }
    </script>
</asp:Content>
