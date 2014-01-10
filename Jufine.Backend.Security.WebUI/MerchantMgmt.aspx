<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True"
    CodeBehind="MerchantMgmt.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.MerchantMgmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="plHeader" runat="server" DefaultButton="btnSearch" CssClass="tools_bar">
        <div class="toolbg toolbgline toolheight nowrap" style="">
            <div class="right">
            </div>
            <div class="nowrap left">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                    Text="查询" />
                <asp:Button ID="btnCreate" runat="server" CssClass="btn" OnClick="btnCreate_Click"
                    Text="新增" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="删除" OnClientClick="return DeleteConfirmByTitle('ckbSelect','将会删除该商家下的所有商品,和申请上架（待审核），销售价（待审核），成本价（待审核），赠品（待审核，审核通过），团购（待审核，审核不通过，草稿）的记录，确认删除吗?');"
                    OnClick="btnDelete_Click" />
                <asp:Button ID="btnActive" runat="server" Text="激活" OnClientClick="return ActionConfirm('ckbSelect','你确定激活选中的信息吗？');"
                    OnClick="btnActive_Click" />
                <asp:Button ID="btnLock" runat="server" Text="禁用" OnClientClick="return ActionConfirm('ckbSelect','你确定禁用选中的信息吗？');"
                    OnClick="btnLock_Click" />
                <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="导出" OnClick="btnExport_Click" />
            </div>
            <div class="clr">
                &nbsp;
            </div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
                <tr>
                    <td style="width: 60px;">商家：
                    </td>
                    <td style="width: 380px;">
                        <asp:DropDownList data-placeholder="全部商家" Width="367px" ID="sddlMerchantID" DataTextField="MechantIDAndName"
                            DataValueField="MerchantID" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 70px;">服务电话：
                    </td>
                    <td style="width: 170px;">
                        <asp:TextBox ID="stxtServicePhone" ClientIDMode="Static" PropertyName="S_ServicePhone"
                            Title="服务电话" Rel="len:0~25" Width="140px" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 70px;">创建时间：
                    </td>
                    <td style="width: 340px;">
                        <asp:TextBox ID="stxtCreateDate" ClientIDMode="Static" PropertyName="MI_CreateDate"
                            Width="155" runat="server" Title="创建时间" Rel="datetime"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDate" runat="server" TargetControlID="stxtCreateDate"
                            Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                        -<asp:TextBox ID="stxtCreateDateTo" PropertyName="MI_CreateDateTo" Width="155" runat="server"
                            Title="创建时间" Rel="datetime"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDateTo" runat="server" TargetControlID="stxtCreateDateTo"
                            Format="yyyy-MM-dd 23:59:59" FirstDayOfWeek="Monday" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px;">联系人：
                    </td>
                    <td style="width: 380px;">
                        <asp:TextBox ID="stxtContactPerson1" ClientIDMode="Static" MaxLength="200" PropertyName="S_ContactPerson1"
                            Width="140px" runat="server"></asp:TextBox>
                        &nbsp;&nbsp; 联系电话：
                        <asp:TextBox ID="stxtTelephone" ClientIDMode="Static" PropertyName="S_Telephone"
                            Title="联系电话" Rel="len:0~25" Width="140px" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 70px;">合作模式：
                    </td>
                    <td style="width: 170px;">
                        <asp:DropDownList data-placeholder="请选择" ID="sddlCooperationModeList" runat="server"
                            Width="147px" TabIndex="1" DataTextField="CodeText" DataValueField="CodeValue" />
                    </td>
                    <td style="width: 70px;">商家状态：
                    </td>
                    <td style="width: 340px">
                        <asp:RadioButtonList ID="rblStatus" PropertyName="S_Status" RepeatDirection="Horizontal"
                            ToolTip="商家状态" runat="server">
                            <asp:ListItem Selected="True" Text="全部" Value="-2"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                            <asp:ListItem Text="激活" Value="1"></asp:ListItem>
                            <asp:ListItem Text="草稿" Value="2"></asp:ListItem>
                            <asp:ListItem Text="待审核" Value="4"></asp:ListItem>
                            <asp:ListItem Text="审核不通过" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:GridView ID="gvMerchantList" runat="server" OnSorting="gvMerchantList_Sorting"
                OnRowDataBound="gvMerchantList_RowDataBound" AutoGenerateColumns="False" AllowSorting="true"
                CssClass="business_list">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ckbSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" ToolTip='<%# Eval("CurrentMerchantID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="200px" HeaderText="编辑" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <a id="aView" visible='<%#Eval("S_ViewVisual")%>' href='<%#Eval("S_ViewURL")%>' onclick="ChangeColor(this);"
                                target="_blank" runat="server">查看</a>
                            <asp:LinkButton ID="lnkEdit" runat="server" RowIndex='<%#Container.DataItemIndex %>'
                                CommandArgument='<%# Eval("CurrentMerchantID") %>' OnClick="lnkEdit_Click">编辑</asp:LinkButton>
                            <asp:LinkButton ID="lnkBtnAudit" NoUse="not use" runat="server" Visible='False' CommandArgument='<%# Eval("S_AuditURL") %>'
                                title='<%#Eval("S_AuditToolTip")%>' ItemID='<%# Eval("ID")%>' MerchantID='<%# Eval("MerchantID") %>'
                                OnClientClick="ChangeColor(this);" OnClick="lnkAudit_Click">审核
                            </asp:LinkButton>
                            <a id="aAudit" visible='<%#Eval("S_AuditVisual")%>' href='<%#Eval("S_AuditURL")%>'
                                title='<%#Eval("S_AuditToolTip")%>' target="_blank" onclick="ChangeColor(this);"
                                runat="server">审核</a>
                            <asp:LinkButton ID="lnkImage" runat="server" CommandArgument='<%# Eval("MerchantID") %>'
                                MerchantName='<%# Eval("MerchantName") %>' OnClick="lnkImage_Click">上传证件</asp:LinkButton>
                            <asp:LinkButton ID="lnkCreateUser" runat="server" Visible='<%# Eval("S_ShowUser") %>'
                                CommandArgument='<%# Eval("MerchantID") %>' OnClick="lnkCreateUser_Click">用户</asp:LinkButton>
                            <a id="aHistory" href='<%#Eval("S_HistoryURL") %>' target="_blank" onclick="ChangeColor(this);"
                                visible='<%#Eval("S_HistoryVisual") %>' runat="server">历史</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="组织机构代码" SortExpression="S_OrganizationCode" ItemStyle-Width="110px">
                        <ItemTemplate>
                            <asp:Label ID="LabelOrganizationCode" runat="server" Width="110px" Text='<%#Eval("S_OrganizationCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商家编号" SortExpression="S_MerchantID" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:Label ID="LabelMerchantID" runat="server" Width="60px" Text='<%#Eval("S_MerchantID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商家名称" SortExpression="S_MerchantName" ItemStyle-Width="180px">
                        <ItemTemplate>
                            <asp:Label ID="LabelMerchantName" runat="server" Width="180px" Text='<%#Eval("S_MerchantName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="服务电话" SortExpression="S_ServicePhone" ItemStyle-Width="110px">
                        <ItemTemplate>
                            <asp:Label ID="LabelServicePhone" runat="server" Width="100px" Text='<%#Eval("S_ServicePhone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="联系人" SortExpression="S_ContactPerson1" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:Label ID="LabelContactPerson1" runat="server" Width="70px" Text='<%#Eval("S_ContactPerson1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="联系电话" SortExpression="S_Telephone" ItemStyle-Width="110px">
                        <ItemTemplate>
                            <asp:Label ID="LabelTelephone" runat="server" Width="110px" Text='<%#Eval("S_Telephone")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="合作模式" SortExpression="S_CooperationMode" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:Label ID="LabelCooperationModeDisplay" runat="server" Width="70px" Text='<%#Eval("S_CooperationModeDisplay") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商家状态" SortExpression="S_Status" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:Label ID="LabelMerchantStrstatus" ToolTip='<%#Eval("S_StatusToolTipText")%>'
                                runat="server" Width="60px" Text='<%#Eval("S_Strstatus")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编辑人" SortExpression="S_EditUser" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:Label ID="LabelEditUser" runat="server" Width="70px" Text='<%#Eval("S_EditUser")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编辑时间" SortExpression="S_EditDate" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="LabelEditDate" runat="server" Width="115px" Text='<%#Eval("S_StrEditDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
            <asp:AsyncPostBackTrigger ControlID="btnActive" />
            <asp:AsyncPostBackTrigger ControlID="btnLock" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel CssClass="miniWindow" ID="plDetail" runat="server" Style="display: none; width: 1200px;">
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
                                <asp:Label runat="server" ID="lblTiltle" Text="新增"></asp:Label>
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
                        <asp:Panel ID="panelDetailInputArea" runat="server" DefaultButton="btnSave">
                            <%--<asp:HiddenField ID="hdID" runat="server" />--%>
                            <div class="c1">
                                <%-- ---------------     --%>
                                <ajaxToolkit:TabContainer ID="TabBox" runat="server" Width="100%" Height="450" AutoPostBack="true"
                                    OnActiveTabChanged="TabBox_ActiveTabChanged">
                                    <ajaxToolkit:TabPanel ID="tabMerchantPreview" runat="server" HeaderText="商家预览" CssClass="CustomTabStyle"
                                        Style="border: none;">
                                        <ContentTemplate>
                                            <asp:Panel ID="panelDetailInputAreaMerchantPreview" runat="server" DefaultButton="btnSaveMerchantPreview">
                                                <asp:UpdatePanel ID="upOpMerchantPreview" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <label for="txtMerchantName">
                                                                                    商家名称：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMerchantName" ClientIDMode="Static" PropertyName="MerchantName"
                                                                                    MaxLength="200" Width="200px" Title="商家名称" Rel="req|len:1~100" runat="server"></asp:TextBox><span
                                                                                        runat="server" style="color: Red;">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtDisplayName">
                                                                                    商家显示名称：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDisplayName" PropertyName="DisplayName" ClientIDMode="Static"
                                                                                    MaxLength="100" Width="200px" Title="商家显示名称" Rel="req|len:1~100" runat="server"></asp:TextBox><span
                                                                                        runat="server" style="color: Red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtLegalRepresentative">
                                                                                    法人：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLegalRepresentative" PropertyName="LegalRepresentative" MaxLength="50"
                                                                                    ClientIDMode="Static" Title="法人" Rel="req|len:1~25" Width="200px" runat="server"></asp:TextBox><span
                                                                                        runat="server" style="color: Red">*</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label for="txtTaxNO">
                                                                                    税务登记号：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTaxNO" ClientIDMode="Static" PropertyName="TaxNO" MaxLength="50"
                                                                                    Width="200px" Title="税务登记号" Rel="req|len:1~50" runat="server"></asp:TextBox><span
                                                                                        runat="server" style="color: Red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtOrganizationCode">
                                                                                    组织机构代码：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtOrganizationCode" ClientIDMode="Static" PropertyName="OrganizationCode"
                                                                                    MaxLength="200" Title="组织机构代码" Rel="req|len:1~30" Width="200px" runat="server"></asp:TextBox><span
                                                                                        runat="server" style="color: Red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtOpeningBank">
                                                                                    开户行：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtOpeningBank" ClientIDMode="Static" PropertyName="OpeningBank"
                                                                                    MaxLength="200" Title="开户行" Rel="req|len:1~100" Width="200px" runat="server"></asp:TextBox><span
                                                                                        runat="server" id="Span20" style="color: Red">*</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label for="txtBankCardNO">
                                                                                    银行账号：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtBankCardNO" ClientIDMode="Static" PropertyName="BankCardNO" MaxLength="100"
                                                                                    Width="200px" Title="银行账号" Rel="req|len:1~50" runat="server"></asp:TextBox><span
                                                                                        runat="server" id="Span21" style="color: Red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    开户行所在省：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList data-placeholder="请选择" Width="200px" ID="ddlBankProvince" DataTextField="AreaName"
                                                                                    DataValueField="ID" runat="server" AutoPostBack="true" PropertyName="BankProvinceID"
                                                                                    Rel="req" title="开户行所在省" OnSelectedIndexChanged="ddlBankProvince_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="-1">请选择</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    开户行所在市：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList data-placeholder="请选择" Width="200px" Rel="req" title="开户行所在市" ID="ddlBankCity"
                                                                                    DataTextField="AreaName" DataValueField="ID" runat="server">
                                                                                    <asp:ListItem Value="-1">请选择</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label for="txtTelephone">
                                                                                    联系电话：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTelephone" ClientIDMode="Static" PropertyName="Telephone" MaxLength="200"
                                                                                    Width="200px" Title="联系电话" Rel="req|len:1~20" runat="server"></asp:TextBox><span
                                                                                        runat="server" id="Span22" style="color: Red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtFax">
                                                                                    传真：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFax" ClientIDMode="Static" PropertyName="Fax" MaxLength="200"
                                                                                    Width="200px" Title="传真" Rel="len:1~20" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtFax2">
                                                                                    传真2：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFax2" ClientIDMode="Static" PropertyName="Fax2" MaxLength="200"
                                                                                    Width="200px" Title="传真2" Rel="len:1~20" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label for="txtAddress">
                                                                                    地址：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAddress" ClientIDMode="Static" PropertyName="Address" MaxLength="500"
                                                                                    Title="地址" Rel="req|len:1~250" Width="200px" runat="server"></asp:TextBox><span runat="server"
                                                                                        id="Span23" style="color: Red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtPostalCode">
                                                                                    邮编：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPostalCode" ClientIDMode="Static" PropertyName="PostalCode" MaxLength="200"
                                                                                    Width="200px" Title="邮编" Rel="req|number|len:6~6" runat="server"></asp:TextBox><span
                                                                                        runat="server" id="Span24" style="color: Red">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtEmail">
                                                                                    邮箱：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtEmail" ClientIDMode="Static" PropertyName="Email" MaxLength="64"
                                                                                    Width="200px" Title="邮箱" Rel="email|len:1~64" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label for="txtWebsite">
                                                                                    官方网站：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWebsite" ClientIDMode="Static" PropertyName="Website" MaxLength="200"
                                                                                    Title="官方网站" Rel="len:1~100" Width="200px" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtServicePhone">
                                                                                    服务电话：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtServicePhone" ClientIDMode="Static" PropertyName="ServicePhone"
                                                                                    MaxLength="50" Width="200px" Title="服务电话" Rel="len:1~25" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label for="txtProductManager">
                                                                                    经办人：</label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtProductManager" ClientIDMode="Static" PropertyName="ProductManager"
                                                                                    MaxLength="500" Width="200px" Title="经办人" Rel="req" runat="server"></asp:TextBox><span
                                                                                        runat="server" id="Span1" style="color: Red">*</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="height: 245px;">
                                                                            <td style="width: 90px; vertical-align: top; height: 245px;" rowspan="3">
                                                                                <label for="ckEditorMerchantDescription">
                                                                                    商家描述：</label>
                                                                            </td>
                                                                            <td rowspan="5" colspan="5" style="vertical-align: top; height: 245px">
                                                                                <CKEditor:CKEditorControl ResizeEnabled="False" Height="170px" ClientIDMode="Static"
                                                                                    Width="92%" ID="ckEditorMerchantDescription" DefaultLanguage="zh-cn" Toolbar="Source
Bold|Italic|Underline|Strike|-|Subscript|Superscript
NumberedList|BulletedList|-|Outdent|Indent"
                                                                                    Title="商家描述" runat="server"></CKEditor:CKEditorControl>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; padding-right: 70px;">
                                                                    <asp:Button ID="btnSaveMerchantPreview" ClientIDMode="Static" runat="server" CommandArgument="MerchantPreview"
                                                                        OnClick="btnSave_Click" OnClientClick="return SaveConfirm('商家预览')" Text="保存" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSaveMerchantPreview" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tabCooperation" runat="server" HeaderText="合作方式" Style="height: 80%; overflow: auto;">
                                        <ContentTemplate>
                                            <asp:Panel ID="panelDetailInputAreaCooperation" runat="server" DefaultButton="btnSaveCooperation">
                                                <asp:UpdatePanel ID="upOpCooperation" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 90px;">合作模式：
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList Width="205" data-placeholder="请选择" ID="ddlCooperationModeList"
                                                                                    runat="server" PropertyName="CooperationMode" DataTextField="CodeText" DataValueField="CodeValue"
                                                                                    Title="合作模式" Rel="req" />
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                            <td style="width: 90px;">合同开始：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtContractStart" PropertyName="ContractStart" Width="200" runat="server"
                                                                                    Title="合同开始" Rel="req|datetime"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="scdeContractStart" runat="server" TargetControlID="txtContractStart"
                                                                                    Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                            <td style="width: 90px;">合同结束：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtContractEnd" PropertyName="ContractEnd" Width="200" runat="server"
                                                                                    Title="合同结束" Rel="req|datetime"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="scdeContractEnd" runat="server" TargetControlID="txtContractEnd"
                                                                                    Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>保证金：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtGuarantyFunds" PropertyName="GuarantyFunds" MaxLength="15" Width="200"
                                                                                    Title="保证金" Rel="req|DECIMAL:2|CHECKDECIMAL:0~*" runat="server"></asp:TextBox>
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                            <td>使用费用：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtUsageCharges" PropertyName="UsageCharges" MaxLength="15" ClientIDMode="Static"
                                                                                    Width="200" Title="使用费用" Rel="req|DECIMAL:2|CHECKDECIMAL:0~*" runat="server"></asp:TextBox>
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                            <td style="width: 90px;">账期：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPaymentCycle" PropertyName="PaymentCycle" MaxLength="8" Title="账期"
                                                                                    Rel="req|PINUM|len:1~100" Width="200" runat="server"></asp:TextBox>
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 90px;">佣金比率：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCommissionRatio" PropertyName="CommissionRatio" MaxLength="8"
                                                                                    Title="佣金比率" Rel="DECIMAL:2|CHECKDECIMAL:0~*" Width="200" runat="server"></asp:TextBox>
                                                                                <span style="color: Red">%</span> <span style="color: Red">*</span>
                                                                            </td>
                                                                            <td style="width: 90px;">返商家比率：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtReturnMerchantRatio" PropertyName="ReturnMerchantRatio" MaxLength="8"
                                                                                    Title="返商家比率" Rel="DECIMAL:2|CHECKDECIMAL:0~*" Width="200" runat="server"></asp:TextBox>
                                                                                <span style="color: Red">%</span> <span style="color: Red">*</span>
                                                                            </td>
                                                                            <td style="width: 90px;">返商城比率：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtReturnMallRatio" PropertyName="ReturnMallRatio" MaxLength="8"
                                                                                    Title="返商城比率" Rel="DECIMAL:2|CHECKDECIMAL:0~*" Width="200" runat="server"></asp:TextBox>
                                                                                <span style="color: Red">%</span> <span style="color: Red">*</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 90px;">开票方：
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList data-placeholder="请选择" Width="205" ID="ddlInvoiceBy" runat="server"
                                                                                    PropertyName="InvoiceBy" DataTextField="CodeText" DataValueField="CodeValue"
                                                                                    Title="开票方" />
                                                                                <span style="color: Red">*</span>
                                                                            </td>
                                                                            <td style="width: 110px;">运费成本承担方：
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList data-placeholder="请选择" Width="205" ID="ddlTransitCostBy" runat="server"
                                                                                    PropertyName="TransitCostBy" DataTextField="CodeText" DataValueField="CodeValue"
                                                                                    Title="运费成本承担方" />
                                                                            </td>
                                                                            <td style="width: 110px;">订单免运费金额：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFreeTransitAmount" ClientIDMode="Static" PropertyName="FreeTransitAmount"
                                                                                    MaxLength="8" Title="订单免运费金额" Rel="req|DECIMAL:2|CHECKDECIMAL:0~*" Width="80px"
                                                                                    runat="server"></asp:TextBox><span style="color: Red">*</span>
                                                                                <input id="btnMinFreeTA" type="button" runat="server" value="全场免运费" onclick="GetFreeTransitAmount(0.00);" />
                                                                                <input id="btnMaxFreeTA" type="button" runat="server" value="全场不免运费" onclick="GetFreeTransitAmount(99999999.00);" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="float: right; margin-top: 10px; margin-right: 68px;">
                                                                        <asp:Button ID="btnSaveCooperation" runat="server" CommandArgument="Cooperation" ClientIDMode="Static"
                                                                            OnClick="btnSave_Click" OnClientClick="return SaveConfirm('合作方式')" Text="保存" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSaveCooperation" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tabContactperson" runat="server" HeaderText="联系人" Style="height: 100px;">
                                        <ContentTemplate>
                                            <asp:Panel ID="panelDetailInputAreaContactperson" runat="server" DefaultButton="btnSaveContactperson">
                                                <asp:UpdatePanel ID="upOpContactperson" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <b>业务联系人</b>
                                                                            </td>
                                                                            <td colspan="5"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>联系人：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtContactPerson1" PropertyName="ContactPerson1" MaxLength="200"
                                                                                    Title="联系人" Rel="len:1~100" Width="200" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人职位：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPost1" PropertyName="Post1" MaxLength="200" Width="200" Title="联系人职位"
                                                                                    Rel="len:1~100" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人部门：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDept1" PropertyName="Dept1" MaxLength="200" Width="200" Title="联系人部门"
                                                                                    Rel="len:1~100" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>联系人手机：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMobile1" PropertyName="Mobile1" MaxLength="200" Width="200" Title="联系人手机"
                                                                                    Rel="phone" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人电话：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTelephone1" PropertyName="Telephone1" MaxLength="50" Width="200"
                                                                                    Title="联系人电话" Rel="len:1~25" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人邮箱：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtEmail1" PropertyName="Email1" MaxLength="64" Width="200" Title="联系人邮箱"
                                                                                    Rel="email|len:1~64" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="height: 20px">
                                                                            <td colspan="6"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>售后系人</b>
                                                                            </td>
                                                                            <td colspan="5"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>联系人2：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtContactPerson2" PropertyName="ContactPerson2" MaxLength="200"
                                                                                    Title="联系人2" Rel="len:1~100" Width="200" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人2职位：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPost2" PropertyName="Post2" MaxLength="200" Width="200" Title="联系人2职位"
                                                                                    Rel="len:1~100" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人2部门：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDept2" PropertyName="Dept2" MaxLength="200" Width="200" Title="联系人2部门"
                                                                                    Rel="len:1~100" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>联系人2手机：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMobile2" PropertyName="Mobile2" MaxLength="200" Width="200" Title="联系人2手机"
                                                                                    Rel="phone" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人2电话：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTelephone2" PropertyName="Telephone2" MaxLength="50" Width="200"
                                                                                    Title="联系人2电话" Rel="len:1~25" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人2邮箱：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtEmail2" PropertyName="Email2" MaxLength="64" Width="200" Title="联系人2邮箱"
                                                                                    Rel="email|len:1~64" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="height: 20px">
                                                                            <td colspan="6"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>财务联系人</b>
                                                                            </td>
                                                                            <td colspan="5"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>联系人3：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtContactPerson3" PropertyName="ContactPerson3" MaxLength="200"
                                                                                    Title="联系人2" Rel="len:1~100" Width="200" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人3职位：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPost3" PropertyName="Post3" MaxLength="200" Width="200" Title="联系人3职位"
                                                                                    Rel="len:1~100" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人3部门：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDept3" PropertyName="Dept3" MaxLength="200" Width="200" Title="联系人3部门"
                                                                                    Rel="len:1~100" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>联系人3手机：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMobile3" PropertyName="Mobile3" MaxLength="200" Width="200" Title="联系人3手机"
                                                                                    Rel="phone" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人3电话：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTelephone3" PropertyName="Telephone3" MaxLength="50" Width="200"
                                                                                    Title="联系人3电话" Rel="len:1~25" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>联系人3邮箱：
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtEmail3" PropertyName="Email3" MaxLength="64" Width="200" Title="联系人3邮箱"
                                                                                    Rel="email|len:1~64" runat="server"></asp:TextBox>
                                                                            </td>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="float: right; margin-top: 10px; margin-right: 60px;">
                                                                        <asp:Button ID="btnSaveContactperson" runat="server" CommandArgument="Contactperson" ClientIDMode="Static"
                                                                            OnClick="btnSave_Click" OnClientClick="return SaveConfirm('联系人')" Text="保存" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSaveContactperson" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tabLicense" runat="server" HeaderText="证照" Style="height: 100px;">
                                        <ContentTemplate>
                                            <asp:Panel ID="panelDetailInputAreaLicense" runat="server" DefaultButton="btnSaveLicense">
                                                <asp:UpdatePanel ID="upOpLicense" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>税务登记证<span runat="server" id="Span25" style="color: Red">*</span>：
                                                                            </td>
                                                                            <td>
                                                                                <asp:UCUploadFile ID="merchantImageTax" runat="server" HideFrameBorder="false" FrameWidth="200"
                                                                                    FrameHeight="92" FileUploadPathAppSettingKey="MerchantImageUploadPath" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>营业执照<span runat="server" id="Span26" style="color: Red">*</span>：
                                                                            </td>
                                                                            <td>
                                                                                <asp:UCUploadFile ID="merchantImageBusiness" runat="server" HideFrameBorder="false"
                                                                                    FrameWidth="200" FrameHeight="92" FileUploadPathAppSettingKey="MerchantImageUploadPath" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Logo<span runat="server" id="Span27" style="color: Red">*</span>：
                                                                            </td>
                                                                            <td>
                                                                                <asp:UCUploadFile ID="merchantImage" runat="server" HideFrameBorder="false" FrameWidth="200"
                                                                                    FrameHeight="92" FileUploadPathAppSettingKey="MerchantImageUploadPath" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="float: right; margin-top: 10px; margin-right: 580px;">
                                                                        <asp:Button ID="btnSaveLicense" runat="server" CommandArgument="License" ClientIDMode="Static" OnClick="btnSave_Click"
                                                                            OnClientClick="return SaveConfirm('证照')" Text="保存" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSaveLicense" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                                <%--  -------------------------      --%>
                            </div>
                        </asp:Panel>
                        <div class="prenext">
                            <asp:Button ID="btnPreviousItem" runat="server" Text="<上一条" OnClick="btnPreviousItem_Click" />
                            <asp:Button ID="btnNextItem" runat="server" Text="下一条>" OnClick="btnNextItem_Click" />
                        </div>
                        <div class="saveexit">
                            <asp:UpdatePanel ID="upBtnChangeStatus" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btnUnDo" runat="server" OnClientClick="return SubmitConfirm('将删除已经保存的【商家预览】、【合作方式】、【联系人】、【证照】信息，是否确认撤销')"
                                        OnClick="btnUnDo_Click" Text="撤消" />
                                    <asp:Button ID="btnBack" runat="server" OnClientClick="return SubmitConfirm('是否确认撤回')"
                                        OnClick="btnBack_Click" Text="撤回" />
                                    <asp:Button ID="btnSave" runat="server" OnClientClick="return SubmitConfirm('是否确认全部保存')"
                                        OnClick="btnSave_Click" CommandArgument="All" Text="全部保存" />
                                    <asp:Button ID="btnSubmitAuditing" runat="server" OnClick="btnSubmitAuditing_Click"
                                        OnClientClick="return SubmitConfirm('是否确认提交审核');" Text="提交审核" />
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="退出" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCreate" />
                        <asp:AsyncPostBackTrigger ControlID="btnPreviousItem" />
                        <asp:AsyncPostBackTrigger ControlID="btnNextItem" />
                        <asp:AsyncPostBackTrigger ControlID="btnBack" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubmitAuditing" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="clear">
                </div>
            </div>
            <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:Panel CssClass="miniWindow" ID="plUploadImage" runat="server" Style="display: none; width: 850px;">
        <div class="container">
            <asp:Panel ID="plTitleUploadImage" Style="cursor: move;" runat="server">
                <div class="" id="Div1">
                </div>
                <div class="t" id="Div2">
                    <div class="l">
                    </div>
                    <div class="title">
                        上传证件图片——附加图片
                    </div>
                    <div class="r">
                    </div>
                </div>
            </asp:Panel>
            <asp:UpdatePanel ID="upUploadImage" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="panelUploadImageInputArea" runat="server" DefaultButton="btnSaveOtherImage">
                        <asp:HiddenField ID="hdCertificateID" runat="server" />
                        <asp:HiddenField ID="hdMerchantID" runat="server" />
                        <div class="c1">
                            <table>
                                <tr>
                                    <td style="text-align: left; vertical-align: top; padding: 10px;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 80px;">商家：
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label runat="server" ID="lblMerchantName"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80px;">标题：
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="otxtTitle" PropertyName="Title" MaxLength="200" Width="300" runat="server"
                                                        Title="标题" Rel="Req|len:1~100"></asp:TextBox><span runat="server" id="Span12" style="color: Red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80px;">显示顺序：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="otxtDisplayOrder" PropertyName="DisplayOrder" MaxLength="4" Width="100"
                                                        Title="显示顺序" Rel="req|pinum" runat="server"></asp:TextBox><span runat="server" id="Span13"
                                                            style="color: Red">*</span>
                                                </td>
                                                <td style="width: 60px;">状态：
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ckbImageStatus" Checked="true" runat="server" Text="激活" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left; vertical-align: top; padding: 10px;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 80px; vertical-align: top;">证件图片<span style="color: Red">*</span>：
                                                </td>
                                                <td>
                                                    <asp:UCUploadFile ID="merchantOtherImage" runat="server" HideFrameBorder="false"
                                                        FrameWidth="200" FrameHeight="92" FileUploadPathAppSettingKey="MerchantImageUploadPath" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <div class="prenext">
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSaveOtherImage" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelOtherImage" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upCertificateList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:GridView ID="gvMerchantAdditionalCertificateList" runat="server" OnSorting="gvMerchantAdditionalCertificateList_Sorting"
                        AutoGenerateColumns="False" AllowSorting="true" CssClass="business_list">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="60" HeaderText="编辑">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditCertificate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                        OnClick="lnkEditCertificate_Click">编辑</asp:LinkButton>
                                    <asp:LinkButton ID="lnkDeleteCertificate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                        OnClientClick="return confirm('确认要删除吗?')" OnClick="lnkDeleteCertificate_Click">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="证件图片" SortExpression="ImangeName" ItemStyle-Width="60">
                                <ItemTemplate>
                                    <a target="_blank" href="<%# BuildMerchantImageUrl(Eval("ImangeName")) %>">
                                        <asp:Image ImageUrl='<%# BuildMerchantImageUrl(Eval("ImangeName")) %>' Height="40"
                                            Width="60" runat="server" ID="imgUrlCertificate" />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Title" HeaderText="标题" SortExpression="Title" ItemStyle-Width="150" />
                            <asp:BoundField DataField="DisplayOrder" HeaderText="显示顺序" SortExpression="DisplayOrder"
                                ItemStyle-Width="40" />
                            <asp:TemplateField HeaderText="状态" SortExpression="Status" ItemStyle-Width="40">
                                <ItemTemplate>
                                    <asp:Label ID="LabelCertificateStatus" runat="server" Text='<%# (Eval("Status") != null && Eval("Status").ToString().Trim() == "1") ?"激活":"禁用" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EditUser" HeaderText="编辑人" SortExpression="EditUser" ItemStyle-Width="60" />
                            <asp:TemplateField HeaderText="编辑时间" SortExpression="EditDate" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="LabelCertificateEditDateDate" runat="server" Text='<%# Eval("EditDate")== null?"": ((DateTime)Eval("EditDate") ).ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="pagination">
                        <asp:ListPager Width="100%" ID="listPagerCertificate" runat="server" FirstPageText="首页"
                            LastPageText="尾页" NextPageText="下一页" OnPageChanged="listPagerCertificate_PageChanged"
                            PageSize="5" PrevPageText="上一页" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                            ShowNavigationToolTip="True" CustomInfoTextAlign="Left" ShowCustomInfoSection="Left"
                            CustomInfoHTML="&nbsp;&nbsp;第 %CurrentPageIndex% 页，共 %PageCount% 页" SubmitButtonClass="pages_butt"
                            TextBeforePageIndexBox="到第" TextAfterPageIndexBox="页  " CustomInfoStyle="padding-top:3px!important;padding-top:6px;height:20px;"
                            CustomInfoSectionWidth="20%">
                        </asp:ListPager>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="listPagerCertificate" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveOtherImage" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelOtherImage" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="c">
                <div class="saveexit">
                    <asp:Button ID="btnSaveOtherImage" runat="server" OnClick="btnSaveOtherImage_Click"
                        Text="保存" />
                    <asp:Button ID="btnCancelOtherImage" runat="server" OnClick="btnCancelOtherImage_Click"
                        Text="退出" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="modalPopupExtender" runat="server" TargetControlID="OkButton"
        BehaviorID="modalPopupExtender" PopupControlID="plDetail" BackgroundCssClass="modalBackground"
        Drag="true" PopupDragHandleControlID="plTitle">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="OkButton" runat="server" Text="" />
    <asp:ModalPopupExtender ID="modalPopupExtenderUploadImage" runat="server" TargetControlID="OKButton1"
        PopupControlID="plUploadImage" BackgroundCssClass="modalBackground" Drag="true"
        PopupDragHandleControlID="plTitleUploadImage">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="OKButton1" runat="server" Text="" />
    <script type="text/javascript">
        function SaveConfirm(param) {
            if (!confirm("是否确认保存【" + param + "】信息")) {
                return false;
            }
            return true;
        }
        function SubmitConfirm(str) {
            if (!confirm(str)) {
                return false;
            }
            return true;
        }
        function GetFreeTransitAmount(value) {
            $("#txtFreeTransitAmount").val(value);
        }
        var allSelAs = new Array();

        function ChangeColor(t) {

            var delPos = -1;
            for (var i = 0; i < allSelAs.length; i++) {
                var href1 = $(allSelAs[i]).attr('href');
                var href2 = $(t).attr('href');
                if (href1 == href2) {
                    delPos = i;
                    break;
                }
            }

            if (delPos >= 0) {
                allSelAs.splice(delPos, 1);
            }

            allSelAs.push(t);

            $.each(allSelAs, function (j, value) {
                $(value).css('color', 'purple');
            });

        }
        function GvOnMousemove(rowId) {
            $("#" + rowId + "[class!='selTr']").removeClass("currTr").addClass("currTr").siblings().removeClass("currTr");
        }

        function GvOnClick(rowId) {
            if ($("#" + rowId).hasClass('selTr')) {
                $("#" + rowId).removeClass("currTr").removeClass("selTr");
            } else {
                $("#" + rowId).removeClass("currTr").addClass("selTr");
            }
        }

    </script>
    <style type="text/css">
        .currTr {
            background-color: #AACDEC;
        }

        .selTr {
            background-color: #AACDEC;
        }
    </style>
</asp:Content>
