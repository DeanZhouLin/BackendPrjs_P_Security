<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="ResourceMgmt.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.ResourceMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="phHasAuth" runat="server">
        <asp:Panel ID="plHeader" runat="server" DefaultButton="btnRefresh">
            <div class="toolbg toolbgline toolheight nowrap" style="">
                <div class="right">
                </div>
                <div class="nowrap left">
                    <asp:Button ID="btnNewRootNode" runat="server" Text="新增根节点" OnClick="btnNewRootNode_Click" />
                    <asp:Button ID="btnNewAdjacentNode" runat="server" Text="新增相邻节点" OnClick="btnNewAdjacentNode_Click" />
                    <asp:Button ID="btnNewChildNode" runat="server" Text="新增子节点" OnClick="btnNewChildNode_Click" />
                    <asp:Button ID="btnRefresh" Style="text-align: center" runat="server" Text="刷新" OnClick="btnRefresh_Click" />
                    <asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" OnClientClick=" return confirm('您确定删除选中的数据吗？');" />
                    <asp:Button ID="btnIFunctionResource" runat="server" Text="导入功能按钮资源" OnClick="btnIFunctionResource_Click" OnClientClick=" return confirm('您确定导入吗？');" style="display:none;" />
                </div>
                <div class="clr">
                    &nbsp;</div>
            </div>
            <div style="padding: 10px;">
                <table width="100%">
                    <tr>
                        <td height="500" width="21%" style="vertical-align: top; padding-top: 0px">
                            <asp:Panel ID="panelResourceTree" runat="server" BorderStyle="Groove" BorderColor="LightGray"
                                BorderWidth="1" ScrollBars="Vertical" Height="500">
                                <asp:UpdatePanel ID="upTreeResource" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TreeView runat="server" OnSelectedNodeChanged="tvResource_SelectedNodeChanged"
                                            EnableClientScript="false" NodeStyle-BorderStyle="None" ShowLines="True" BorderWidth="0px"
                                            Font-Size="13px" ForeColor="#333333" SelectedNodeStyle-ForeColor="#ff6600" ParentNodeStyle-BorderStyle="Dotted"
                                            ID="tvResource" SelectedNodeStyle-Font-Bold="True" SelectedNodeStyle-Font-Size="Larger">
                                            <ParentNodeStyle BorderStyle="None" />
                                            <NodeStyle BorderStyle="None" />
                                        </asp:TreeView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
                                        <asp:AsyncPostBackTrigger ControlID="btnDel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                        <td align="left" valign="top" style="vertical-align: top;">
                            <asp:Panel ID="panel1" runat="server" BorderStyle="Inset" BorderColor="LightGray"
                                BorderWidth="1" Height="500">
                                <asp:UpdatePanel ID="upEditResource" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table style="line-height: 30px; width: 80%; margin: 0px 5px 5px 20px">
                                            <tr>
                                                <td style="width: 100px;">
                                                    <asp:HiddenField ID="hdfResourceId" runat="server" />
                                                    <asp:HiddenField ID="hdfParentResourceId" runat="server" />
                                                    <asp:Label ID="Label2" runat="server" Text="父节点：" Width="85px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtParentResourceName" Width="300" runat="server" Title="父节点" Rel="req"
                                                        Enabled="false"></asp:TextBox>
                                                    <%--<asp:DropDownList data-placeholder="请选择"  ID="ddlParentResourceID" Width="308" DataTextField="DisplayName" DataValueField="ID" runat="server" PropertyName="ParentID">
													</asp:DropDownList>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="labResourceName" runat="server" Text="资源名称：" Width="85px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtResourceName" Width="300" PropertyName="ResourceName" runat="server"
                                                        Title="资源名称" MaxLength="50" Rel="req|len:1~50|en_num"></asp:TextBox>
                                                    <span runat="server" id="starResourceName" style="color: Red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="资源显示名称："></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDisplayName" Width="300" PropertyName="DisplayName" runat="server"
                                                        Title="资源显示名称" Rel="req|len:1~25" MaxLength="50"></asp:TextBox>
                                                    <span runat="server" id="Span1" style="color: Red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="资源路径："></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtResourceAddress" Text="~/" Width="300" PropertyName="ResourceAddress"
                                                        runat="server" Title="资源路径" Rel="req|len:1~500"></asp:TextBox>
                                                    <span runat="server" id="Span2" style="color: Red">*</span>(相对路径请加'~/')(参数通配符：*)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblResourceType" runat="server" Text="资源类型："></asp:Label>
                                                </td>
                                                <td>
                                                    <%--<asp:DropDownList data-placeholder="请选择"  ID="ddlResourceType" Width="308" runat="server" DataTextField="CodeText" DataValueField="CodeValue" >
													</asp:DropDownList>--%>
                                                    <asp:RadioButtonList ID="rblResourceType" runat="server" DataTextField="CodeText"
                                                        DataValueField="CodeValue" RepeatDirection="Horizontal">
                                                       
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="labDisplayOrder" runat="server" Text="显示顺序：" Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDisplayOrder" runat="server" PropertyName="DisplayOrder" Title="显示顺序"
                                                        Rel="req|len:1~5|pinum" Width="100px"></asp:TextBox>
                                                    <span runat="server" id="starDisplayOrder" style="color: Red">*</span>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox ID="ckbShowInMenu" runat="server" Text="在菜单显示" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text="状态：" Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ckbStatus" Checked="true" runat="server" Text="激活" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="background-color: Transparent; text-align: center; width: 80%">
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSave" Text="保存" Width="100" Height="30" Style="text-align: center"
                                                        runat="server" OnClick="btnSave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tvResource" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
                                        <asp:AsyncPostBackTrigger ControlID="btnDel" />
                                        <asp:AsyncPostBackTrigger ControlID="btnNewRootNode" />
                                        <asp:AsyncPostBackTrigger ControlID="btnNewAdjacentNode" />
                                        <asp:AsyncPostBackTrigger ControlID="btnNewChildNode" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
