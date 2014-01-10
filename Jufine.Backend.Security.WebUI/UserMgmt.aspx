<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True" MaintainScrollPositionOnPostback="true" CodeBehind="UserMgmt.aspx.cs" EnableEventValidation="false"
    Inherits="Jufine.Backend.Security.WebUI.UserMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        // 点击复选框时触发事件
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                //这里的第一个参数是UpdatePanel ID，因为我使用了MS的ASPAJAX来实现局部刷新
                //如果没有使用MS的ASPAJAX，这里的两个参数都可以为空
                __doPostBack("<%=treeAuth.ClientID %>", "");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="plHeader" runat="server" DefaultButton="btnSearch" CssClass="tools_bar">
        <div class="toolbg toolbgline toolheight nowrap" style="">
            <div class="right">
            </div>
            <div class="nowrap left">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnCreate" runat="server" CssClass="btn" Text="新建" OnClick="btnCreate_Click" Enabled="false" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="删除" OnClientClick="return DeleteConfirm('ckbSelect');" OnClick="btnDelete_Click" />
                <asp:Button ID="btnActive" runat="server" Text="激活" OnClientClick="return ActionConfirm('ckbSelect','你确定激活选中的信息吗？');" OnClick="btnActive_Click" />
                <asp:Button ID="btnLock" runat="server" Text="禁用" OnClientClick="return ActionConfirm('ckbSelect','你确定禁用选中的信息吗？');" OnClick="btnLock_Click" />
                <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="导出用户" OnClick="btnExport_Click" />
            </div>
            <div class="clr">
                &nbsp;
            </div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
                <tr>
                    <td style="width: 80px;">商家：
                    </td>
                    <td>
                        <%--<asp:SearchableListBox ID="ucSearchableListBox" runat="server" Width="250" OnDoSearch="SearchableListBox_DoSearch" OnSelectedIndexChanged="SearchableListBox_SelectedIndexChanged" />--%>
                        <asp:DropDownList data-placeholder="全部商家" ID="sddlMerchantID" runat="server"></asp:DropDownList>
                    </td>
                    <td style="width: 80px;">用户名：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtUserName" PropertyName="UserName" Width="300" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">姓名：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtFullName" PropertyName="FullName" Width="300" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 80px;">性别：
                    </td>
                    <td>
                        <asp:RadioButton ID="rbQuerySexAll" runat="server" Checked="true" GroupName="SexQuery" Text="全部" />
                        <asp:RadioButton ID="rbQueryMale" runat="server" GroupName="SexQuery" Text="男" />
                        <asp:RadioButton ID="rbQueryFemale" runat="server" GroupName="SexQuery" Text="女" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">身份证号码：
                    </td>
                    <td>
                        <asp:TextBox ID="IDCard" PropertyName="IDCard" Width="300" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 80px;">状态：
                    </td>
                    <td>
                        <asp:RadioButton ID="rbQueryAll" runat="server" Checked="true" GroupName="StatusQuery" Text="全部" />
                        <asp:RadioButton ID="rbQueryActive" runat="server" GroupName="StatusQuery" Text="激活" />
                        <asp:RadioButton ID="rbQueryLock" runat="server" GroupName="StatusQuery" Text="禁用" />
                    </td>
                </tr>
                <%--<tr>
                    <td style="width: 80px;">
                        邮箱：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtEmail" PropertyName="Email" Width="300" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 90px;">编辑人
                    </td>
                    <td>
                        <asp:TextBox ID="stxtEditUser" PropertyName="EditUser" Width="300" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 90px;">编辑时间
                    </td>
                    <td>
                        <asp:TextBox ID="stxtEditDate" PropertyName="EditDate" Width="145" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeEditDate" runat="server" TargetControlID="stxtEditDate" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                        -<asp:TextBox ID="stxtEditDateTo" PropertyName="EditDateTo" Width="145" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeEditDateTo" runat="server" TargetControlID="stxtEditDateTo" Format="yyyy-MM-dd 23:59:59" FirstDayOfWeek="Monday" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
		
            <asp:GridView ID="gvUserList" runat="server" OnSorting="gvUserList_Sorting" AutoGenerateColumns="False" AllowSorting="true" CssClass="business_list">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ckbSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" ToolTip='<%# Eval("ID") %>' Visible='<%# (bool)(Eval("UserName")!=null&&Eval("UserName").ToString()!="admin") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="80" AccessibleHeaderText="Operation" HeaderText="编辑">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" RowIndex='<%#Container.DataItemIndex %>' runat="server" CommandArgument='<%# Eval("ID") %>' MerchantID='<%#Eval("MerchantID") %>' OnClick="lnkEdit_Click">编辑</asp:LinkButton>
                            <asp:LinkButton ID="lnkMerchant" runat="server" CommandArgument='<%# Eval("ID") %>' ToolTip='<%# Eval("UserName") %>' MerchantID='<%#Eval("MerchantID") %>' MerchantName='<%# Eval("MerchantName") %>'
                                OnClick="lnkMerchant_Click">商家</asp:LinkButton>
                            <asp:LinkButton ID="lnkRole" runat="server" CommandArgument='<%# Eval("ID") %>' ToolTip='<%# Eval("UserName") %>' OnClick="lnkRole_Click" MerchantName='<%# Eval("MerchantName") %>'
                                MerchantID='<%#Eval("MerchantID") %>'>角色</asp:LinkButton>
                            <asp:LinkButton ID="lnkAuthority" runat="server" ToolTip='<%# Eval("UserName") %>' MerchantName='<%# Eval("MerchantName") %>' MerchantID='<%#Eval("MerchantID") %>'
                                CommandArgument='<%# Eval("ID") %>' OnClick="lnkAuthority_Click">权限</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MerchantID" HeaderText="商家编号" SortExpression="MerchantID" ItemStyle-Width="50" />
                    <asp:BoundField DataField="MerchantName" HeaderText="商家名称" SortExpression="MerchantName" ItemStyle-Width="140" />
                    <asp:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName" ItemStyle-Width="60" />
                    <asp:BoundField DataField="FullName" HeaderText="姓名" SortExpression="FullName" ItemStyle-Width="60" />
                    <asp:TemplateField HeaderText="性别" SortExpression="Sex" ItemStyle-Width="20">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# (Eval("Sex") != null && Eval("Sex").ToString().Trim() == "0") ?"男":"女" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IDCard" HeaderText="身份证号码" SortExpression="IDCard" ItemStyle-Width="130" />
                    <asp:TemplateField HeaderText="状态" SortExpression="Status" ItemStyle-Width="30">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# (Eval("Status") != null && Eval("Status").ToString().Trim() == "1") ?"激活":"禁用" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="Email" HeaderText="邮箱" SortExpression="Email" ItemStyle-Width="60" />--%>
                    <%-- <asp:TemplateField HeaderText="是否为管理员" SortExpression="IsAdmin" ItemStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="LableIsAdmin" runat="server" Text='<%# (Eval("IsAdmin") != null && Eval("IsAdmin").ToString().Trim() == "1") ?"是":"否" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:BoundField DataField="CreateUser" HeaderText="创建人" SortExpression="CreateUser"
                        ItemStyle-Width="60" />
                    <asp:TemplateField HeaderText="创建时间" SortExpression="CreateDate" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="LabelCreateDate" runat="server" Text='<%# Eval("CreateDate")== null?"": ((DateTime)Eval("CreateDate") ).ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="EditUser" HeaderText="编辑人" SortExpression="EditUser" ItemStyle-Width="60" />
                    <asp:TemplateField HeaderText="编辑时间" SortExpression="EditDate" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="LabelEditDate" runat="server" Text='<%# Eval("EditDate")== null?"": ((DateTime)Eval("EditDate") ).ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--                            <asp:BoundField DataField="EditUser" HeaderText="EditUser" SortExpression="EditUser" ItemStyle-Width="60" />
                            <asp:BoundField DataField="EditDate" HeaderText="EditDate" SortExpression="EditDate" ItemStyle-Width="60" />--%>
                </Columns>
            </asp:GridView>
		
            <div class="pagination">
                <asp:ListPager Width="100%" ID="listPager" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" OnPageChanged="listPager_PageChanged" PageSize="15"
                    PrevPageText="上一页" ShowPageIndexBox="Always"
                    PageIndexBoxType="TextBox" ShowNavigationToolTip="True" CustomInfoTextAlign="Left" ShowCustomInfoSection="Left" CustomInfoHTML="&nbsp;&nbsp;第 %CurrentPageIndex% 页，共 %PageCount% 页"
                    SubmitButtonClass="pages_butt"
                    TextBeforePageIndexBox="到第" TextAfterPageIndexBox="页  " CustomInfoStyle="padding-top:3px!important;padding-top:6px;height:20px;" CustomInfoSectionWidth="20%">
                </asp:ListPager>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="listPager" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnDelete" />
            <asp:AsyncPostBackTrigger ControlID="btnActive" />
            <asp:AsyncPostBackTrigger ControlID="btnLock" />
        </Triggers>
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
                        新增/编辑用户
                    </div>
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
                                        <td style="width: 80px;">商家编号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMerchantID" PropertyName="MerchantID" MaxLength="4" Width="300" noclear="true" Text="1" Enabled="false" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">商家名称：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMerchantName" Width="300" Enabled="false" runat="server" noclear="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">用户名：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUserName" PropertyName="UserName" MaxLength="20" Width="300" Title="用户名" Rel="req|useraccount|len:1~20" runat="server"></asp:TextBox>
                                            <span runat="server" id="starUserName" style="color: Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">密码：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" PropertyName="Password" MaxLength="16" Width="300" Title="密码" Rel="len:6~16" runat="server" TextMode="Password"></asp:TextBox>
                                            <span runat="server" id="Span4" style="color: Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">真实姓名：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFullName" PropertyName="FullName" MaxLength="20" Width="300" Title="真实姓名" Rel="req|len:1~10" runat="server"></asp:TextBox><span runat="server"
                                                id="Span1" style="color: Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">性别：
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbMale" runat="server" GroupName="Sex" Text="男" Checked="true" />
                                            <asp:RadioButton ID="rbFemale" runat="server" GroupName="Sex" Text="女" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">身份证号码：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIDCard" PropertyName="IDCard" MaxLength="20" Width="300" runat="server" Title="身份证号码" Rel="req|en_num|len:15~18"></asp:TextBox><span runat="server"
                                                id="Span2" style="color: Red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">状态：
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ckbStatus" Checked="true" runat="server" Text="激活" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">邮箱：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" PropertyName="Email" MaxLength="200" Width="300" runat="server" Title="邮箱" Rel="req|email"></asp:TextBox><span runat="server" id="Span3"
                                                style="color: Red">*</span>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td style="width: 80px;">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ckbIsAdmin" Checked="true" runat="server" Text="管理员" />
                                        </td>
                                    </tr>--%>
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
                    </Triggers>
                </asp:UpdatePanel>
                <div class="saveexit">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="退出" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:Panel CssClass="miniWindow" ID="plAuthority" runat="server" Style="display: none; width: 410px;">
        <div class="container">
            <asp:Panel ID="plAuthTitle" Style="cursor: move;" runat="server">
                <div class="" id="Div1">
                </div>
                <div class="t" id="Div2">
                    <div class="l">
                    </div>
                    <div class="title">
                        用户权限分配
                    </div>
                    <div class="r">
                    </div>
                </div>
            </asp:Panel>
            <div class="c">
                <asp:UpdatePanel ID="upAuthority" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:Panel ID="panelAuthorityInputArea" runat="server" DefaultButton="btnSaveAuth">
                            <asp:HiddenField ID="hdUserID" runat="server" />
                            <div class="c1">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="label3" runat="server" Text="用户名:"></asp:Label>
                                            <asp:Label ID="lblAuthorityUserName" runat="server" Text="用户名"></asp:Label>
                                            (<asp:Label ID="lblAuthorityMerchantName" runat="server" Text="商家名"></asp:Label>)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="label5" runat="server" Text="商家:"></asp:Label>
                                            <asp:DropDownList data-placeholder="请选择" Width="200px" ID="ddlMerchantForAuthority" DataTextField="MerchantName" DataValueField="MerchantID" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlMerchantForAuthority_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; vertical-align: top; padding: 10px;">
                                            <asp:Panel ID="TreeViewPanel" ScrollBars="Auto" BorderWidth="1" BorderStyle="Inset" Width="240px" Height="300px" runat="server">
                                                <span style="padding: 5px;"><strong>用户权限分配：</strong></span>
                                                <asp:UpdatePanel ID="upAuthorityTreeView" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TreeView runat="server" NodeStyle-BorderStyle="Solid" ShowLines="True" BorderWidth="0px" OnTreeNodeCheckChanged="treeAuth_TreeNodeCheckChanged" Font-Size="13px"
                                                            ForeColor="#333333" SelectedNodeStyle-ForeColor="#ff6600"
                                                            ParentNodeStyle-BorderStyle="Dotted" ShowCheckBoxes="All" ID="treeAuth">
                                                            <ParentNodeStyle BorderStyle="None" />
                                                            <NodeStyle BorderStyle="None" />
                                                        </asp:TreeView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 120px; text-align: left; vertical-align: bottom;">
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnSelectAllAuthority" runat="server" OnClick="btnSelectAllAuthority_Click" Text="全选" />
                                            </div>
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnSelectInverseAuthority" runat="server" OnClick="btnSelectInverseAuthority_Click" Text="反选" />
                                            </div>
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnClearAuthority" runat="server" OnClick="btnClearAuthority_Click" Text="清空" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <div class="prenext">
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveAuth" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelAuth" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="saveexit">
                    <asp:Button ID="btnSaveAuth" runat="server" OnClick="btnSaveAuth_Click" Text="保存" />
                    <asp:Button ID="btnCancelAuth" runat="server" OnClick="btnCancelAuth_OnClick" Text="退出" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="modalPopupExtender" runat="server" TargetControlID="OkButton1" PopupControlID="plDetail" BackgroundCssClass="modalBackground" Drag="true"
        PopupDragHandleControlID="plTitle">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="OkButton1" runat="server" Text="" />
    <asp:ModalPopupExtender ID="modalPopupExtenderAuth" runat="server" TargetControlID="OkButton" PopupControlID="plAuthority" BackgroundCssClass="modalBackground" Drag="true"
        PopupDragHandleControlID="plAuthTitle">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="OkButton" runat="server" Text="" />
    <asp:Panel CssClass="miniWindow" ID="plMerchant" runat="server" Style="display: none; width: 410px;">
        <div class="container">
            <asp:Panel ID="plMerchantTitle" Style="cursor: move;" runat="server">
                <div class="" id="Div3">
                </div>
                <div class="t" id="Div4">
                    <div class="l">
                    </div>
                    <div class="title">
                        用户商家配置
                    </div>
                    <div class="r">
                    </div>
                </div>
            </asp:Panel>
            <div class="c">
                <asp:UpdatePanel ID="upMerchant" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:Panel ID="panelMerchantInputArea" runat="server" DefaultButton="btnSaveMerchant">
                            <asp:HiddenField ID="hdMerchantUserId" runat="server" />
                            <div class="c1">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="label4" runat="server" Text="用户名:"></asp:Label>
                                            <asp:Label ID="lblMerchantUserName" runat="server" Text="用户名"></asp:Label>
                                            (<asp:Label ID="lblMerchantMerchantName" runat="server" Text="商家名"></asp:Label>)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; vertical-align: top; padding: 10px;">
                                            <asp:Panel ID="Panel4" ScrollBars="Auto" BorderWidth="1" BorderStyle="Inset" Width="240px" Height="300px" runat="server">
                                                <span style="padding: 5px;"><strong>用户商家配置：</strong></span>
                                                <asp:CheckBoxList ID="ckbMerchantList" runat="server" DataTextField="MerchantName" DataValueField="ID">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 120px; text-align: left; vertical-align: bottom;">
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnSelectAllMerchant" runat="server" OnClick="btnSelectAllMerchant_Click" Text="全选" />
                                            </div>
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnSelectInverseMerchant" runat="server" OnClick="btnSelectInverseMerchant_Click" Text="反选" />
                                            </div>
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnClearMerchant" runat="server" OnClick="btnClearMerchant_Click" Text="清空" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <div class="prenext">
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveMerchant" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelMerchant" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="saveexit">
                    <asp:Button ID="btnSaveMerchant" runat="server" OnClick="btnSaveMerchant_Click" Text="保存" />
                    <asp:Button ID="btnCancelMerchant" runat="server" OnClick="btnCancelMerchant_OnClick" Text="退出" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="modalPopupExtenderMerchant" runat="server" TargetControlID="LinkButton1" PopupControlID="plMerchant" BackgroundCssClass="modalBackground"
        Drag="true" PopupDragHandleControlID="plMerchantTitle">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="LinkButton1" runat="server" Text="" />
    <asp:Panel CssClass="miniWindow" ID="plRole" runat="server" Style="display: none; width: 410px;">
        <div class="container">
            <asp:Panel ID="plRoleTitle" Style="cursor: move;" runat="server">
                <div class="" id="Div5">
                </div>
                <div class="t" id="Div6">
                    <div class="l">
                    </div>
                    <div class="title">
                        用户角色分配
                    </div>
                    <div class="r">
                    </div>
                </div>
            </asp:Panel>
            <div class="c">
                <asp:UpdatePanel ID="upRole" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:Panel ID="panelRoleInputArea" runat="server" DefaultButton="btnSaveRole">
                            <asp:HiddenField ID="hdUserIDForRole" runat="server" />
                            <div class="c1">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="label6" runat="server" Text="用户名:"></asp:Label>
                                            <asp:Label ID="lblUserNameForRole" runat="server" Text="用户名"></asp:Label>
                                            (<asp:Label ID="lblMerchantNameForRole" runat="server" Text="商家名"></asp:Label>)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="label9" runat="server" Text="商家:"></asp:Label>
                                            <asp:DropDownList Width="200px" data-placeholder="请选择" ID="ddlMerchantListForRole" DataTextField="MerchantName" DataValueField="MerchantID" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlMerchantForRole_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; vertical-align: top; padding: 10px;">
                                            <asp:Panel ID="Panel5" ScrollBars="Auto" BorderWidth="1" BorderStyle="Inset" Width="240px" Height="300px" runat="server">
                                                <span style="padding: 5px;"><strong>用户角色分配：</strong></span>
                                                <asp:UpdatePanel ID="upRoleList" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:CheckBoxList ID="ckbRoleList" runat="server" DataTextField="RoleNameDisplay" DataValueField="ID">
                                                        </asp:CheckBoxList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 120px; text-align: left; vertical-align: bottom;">
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnSelectAllRole" runat="server" OnClick="btnSelectAllRole_Click" Text="全选" />
                                            </div>
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnSelectInverseRole" runat="server" OnClick="btnSelectInverseRole_Click" Text="反选" />
                                            </div>
                                            <div style="padding: 5px;">
                                                <asp:Button ID="btnClearRole" runat="server" OnClick="btnClearRole_Click" Text="清空" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <div class="prenext">
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveRole" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelRole" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="saveexit">
                    <asp:Button ID="btnSaveRole" runat="server" OnClick="btnSaveRole_Click" Text="保存" />
                    <asp:Button ID="btnCancelRole" runat="server" OnClick="btnCancelRole_OnClick" Text="退出" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="modalPopupExtenderRole" runat="server" TargetControlID="LinkButton2" PopupControlID="plRole" BackgroundCssClass="modalBackground" Drag="true"
        PopupDragHandleControlID="plRoleTitle">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="LinkButton2" runat="server" Text="" />
</asp:Content>
