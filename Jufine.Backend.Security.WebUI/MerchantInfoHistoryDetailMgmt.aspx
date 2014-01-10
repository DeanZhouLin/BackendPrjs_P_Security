<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True"
    CodeBehind="MerchantInfoHistoryDetailMgmt.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.MerchantInfoHistoryDetailMgmt" %>

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
            </div>
            <div class="clr">
                &nbsp;</div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
                <tr>
                    <td style="width: 90px;">
                        商家：
                    </td>
                    <td>
                         <asp:DropDownList data-placeholder="全部商家" ID="sddlMerchantID" DataTextField="MechantIDAndName" DataValueField="MerchantID" Width="310px" runat="server"></asp:DropDownList>
                    </td>
                     <td style="width: 90px;">
                        联系人：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtContactPerson1" PropertyName="ContactPerson1" Width="200" runat="server"></asp:TextBox>
                    </td>
                       <td style="width: 90px;">
                        联系电话：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtTelephone" PropertyName="Telephone" Width="200" runat="server"></asp:TextBox>
                    </td>
                         <td style="width: 90px;">
                        状态：
                    </td>
                    <td>
                        <asp:DropDownList data-placeholder="请选择" ID="ddlQueryList" runat="server" Width="120px">
                            <asp:ListItem Selected="True" Text="请选择" Value="-2"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                            <asp:ListItem Text="激活" Value="1"></asp:ListItem>
                            <asp:ListItem Text="审核不通过" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                          <td style="width: 90px;">
                        创建时间：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtCreateDate" PropertyName="CreateDate" Width="145" runat="server"
                            Title="创建时间" Rel="datetime"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDate" runat="server" TargetControlID="stxtCreateDate"
                            Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                        -<asp:TextBox ID="stxtCreateDateTo" PropertyName="CreateDateTo" Width="145" runat="server"
                            Title="创建时间" Rel="datetime"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDateTo" runat="server" TargetControlID="stxtCreateDateTo"
                            Format="yyyy-MM-dd 23:59:59" FirstDayOfWeek="Monday" />
                    </td>
                      <td style="width: 90px;">
                        服务电话：
                    </td>
                    <td>
                        <asp:TextBox ID="stxtServicePhone" PropertyName="ServicePhone" Width="200" runat="server"></asp:TextBox>
                    </td>
                 
           
                    <td style="width: 90px;">
                        合作模式：
                    </td>
                    <td colspan="3">
                        <asp:DropDownList data-placeholder="请选择" ID="sddlCooperationModeList" runat="server"
                            Width="205px" TabIndex="1" DataTextField="CodeText" DataValueField="CodeValue" />
                        <%--  <script type="text/javascript"> $("select[data-placeholder]").chosen({ no_results_text: "没有满足条件的", allow_single_deselect: true }); </script>--%>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:GridView ID="gvMerchantList" runat="server" OnSorting="gvMerchantList_Sorting"
                AutoGenerateColumns="False" AllowSorting="true" CssClass="business_list">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="40" HeaderText="详情">
                        <ItemTemplate>
                            <a id="aView" href='<%#BuildUrl( Eval("ID"), "historydetail" ) %>' target="_blank"
                                runat="server">详情</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MerchantID" HeaderText="商家编号" SortExpression="MerchantID"
                        ItemStyle-Width="80" />
                    <asp:BoundField DataField="MerchantName" HeaderText="商家名称" SortExpression="MerchantName"
                        />
                    <asp:BoundField DataField="CooperationModes" HeaderText="合作模式" SortExpression="CooperationMode"
                        ItemStyle-Width="80" />
                    <asp:BoundField DataField="ServicePhone" HeaderText="服务电话" SortExpression="ServicePhone"
                        ItemStyle-Width="120" />
                    <asp:BoundField DataField="OrganizationCode" HeaderText="组织机构代码" SortExpression="OrganizationCode"
                        ItemStyle-Width="130" />
                    <asp:BoundField DataField="Telephone" HeaderText="联系电话" SortExpression="Telephone"
                        ItemStyle-Width="120" />
                    <asp:BoundField DataField="ContactPerson1" HeaderText="联系人" SortExpression="ContactPerson1"
                        ItemStyle-Width="120" />
                    <asp:BoundField DataField="Strstatus" HeaderText="状态" SortExpression="Status" ItemStyle-Width="80" />
                    <asp:BoundField DataField="EditUser" HeaderText="编辑人" SortExpression="EditUser" ItemStyle-Width="120" />
                    <asp:TemplateField HeaderText="编辑时间" SortExpression="EditDate" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="LabelEditDate" runat="server" Text='<%# Eval("EditDate")== null?"": ((DateTime)Eval("EditDate") ).ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
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
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
