<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MerchantSearchAuditMgmt.aspx.cs"
    Inherits="Jufine.Backend.Security.WebUI.MerchantSearchAuditMgmt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="~/MasterPageDir/Themes/style/global.css" />
    <link type="text/css" rel="stylesheet" href="~/MasterPageDir/Javascripts/chosen/chosen.css" />
</head>
<body>
    <form id="form1" runat="server">
    <script src='<%=BuildStaticResourceUrl("Javascripts/comm.js")%>' type="text/javascript"></script>
    <script src='<%=BuildStaticResourceUrl("Javascripts/jquery-1.7.2.min.js")%>' type="text/javascript"></script>
    <script src='<%=BuildStaticResourceUrl("Javascripts/chosen/chosen.jquery.js")%>'
        type="text/javascript"></script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="upMerchantInfo" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <div align="right" style="width: 1380px">
                <asp:Button ID="btnPass" runat="server" OnClick="btnPass_Click"  OnClientClick="return PriceCheckd()" 
                    Text="审核通过" />
                <asp:Button ID="btnNotPass" runat="server" OnClick="btnNotPass_Click"  OnClientClick="return ApproveBack()" 
                    Text="审核不通过" />
                <input type="button" id="btnCancel" onclick="javascript:window.close();window.opener.MySearch()"
                    value="退出" />
            </div>
            <div align="center">
                <asp:Panel ID="panelMerchantItem" runat="server" Font-Bold="True">
                    <asp:Panel ID="MerchantReason" runat="server" GroupingText="审批" Width="1200">
                        <div style="font-weight: normal">
                            <table>
                                <tr runat="server" id="traudit">
                                    <td style="width: 100px">
                                        审批人:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuditUser" PropertyName="AuditUser" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        审批时间:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAuditDate" PropertyName="AuditDate" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90px; vertical-align: top;" rowspan="3">
                                        审批原因:
                                    </td>
                                    <td colspan="5" style="vertical-align: top; height: 100px">
                                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Height="100" Width="820px"
                                            PropertyName="Reason" title="审批原因"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="plMerchantBasicInformation" GroupingText="商家基本信息" Width="1200" runat="server">
                        <div style="font-weight: normal">
                            <div>
                                <asp:Label runat="server" ID="labItemString" Style="color: Red;"></asp:Label>
                            </div>
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        商家名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMerchantName" PropertyName="MerchantName" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        商家显示名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDisplayName" PropertyName="DisplayName" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        官方网站：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWebsite" PropertyName="Website" Enabled="False" Width="200" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        法人：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLegalRepresentative" PropertyName="LegalRepresentative" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        税务登记号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxNO" PropertyName="TaxNO" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        组织机构代码：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOrganizationCode" PropertyName="OrganizationCode" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        开户行：
                                    </td>
                                    <td>
                                        <asp:DropDownList Width="205" data-placeholder="请选择" runat="server" ID="ddlProvince"
                                            PropertyName="BankProvinceID" DataTextField="AreaName" DataValueField="ID" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList Width="113" data-placeholder="请选择" runat="server" ID="ddlCity"
                                            PropertyName="BankCityID" DataTextField="AreaName" DataValueField="ID" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOpeningBank" PropertyName="OpeningBank" Enabled="False" Width="200"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        银行账号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBankCardNO" PropertyName="BankCardNO" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        联系电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelephone" PropertyName="Telephone" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        传真：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax" PropertyName="Fax" Width="200" Enabled="False" Rel="len:1~20"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        传真2：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax2" PropertyName="Fax2" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        地址：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress" PropertyName="Address" Enabled="False" Width="200" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        邮编：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPostalCode" PropertyName="PostalCode" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        邮箱：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" PropertyName="Email" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        服务电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtServicePhone" PropertyName="ServicePhone" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        经办人名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProductManager" PropertyName="ProductManager" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height: 200px">
                                    <td style="width: 90px; vertical-align: top;" rowspan="3">
                                        商家描述：
                                    </td>
                                    <td rowspan="5" colspan="5" style="vertical-align: top; height: 180px">
                                        <asp:TextBox TextMode="MultiLine" Height="170" Width="99%" runat="server" PropertyName="MerchantDescription"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="plMerchantCooperationMode" GroupingText="商家合作方式" Width="1200" runat="server">
                        <div style="font-weight: normal">
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        合作模式：
                                    </td>
                                    <td>
                                        <asp:DropDownList Width="205" data-placeholder="请选择" ID="ddlCooperationModeList"
                                            runat="server" PropertyName="CooperationMode" DataTextField="CodeText" DataValueField="CodeValue"
                                            Enabled="False" />
                                    </td>
                                    <td style="width: 100px">
                                        合同开始：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContractStart" PropertyName="ContractStart" Width="200" runat="server"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        合同结束：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContractEnd" PropertyName="ContractEnd" Width="200" runat="server"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        保证金：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGuarantyFunds" PropertyName="GuarantyFunds" MaxLength="15" Width="200"
                                            Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        使用费用：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUsageCharges" PropertyName="UsageCharges" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        账期：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPaymentCycle" PropertyName="PaymentCycle" Enabled="False" Width="200"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        佣金比率：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommissionRatio" PropertyName="CommissionRatio" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        返商家比率：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReturnMerchantRatio" PropertyName="ReturnMerchantRatio" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        返商城比率：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReturnMallRatio" PropertyName="ReturnMallRatio" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        开票方：
                                    </td>
                                    <td>
                                        <asp:DropDownList data-placeholder="请选择" Width="205" ID="ddlInvoiceBy" runat="server"
                                            PropertyName="InvoiceBy" DataTextField="CodeText" DataValueField="CodeValue"
                                            Enabled="False" />
                                    </td>
                                    <td>
                                        运费承担方：
                                    </td>
                                    <td>
                                        <%-- TransitCostBy--%>
                                        <asp:DropDownList data-placeholder="请选择" Width="205" ID="ddlTransitCostBy" runat="server"
                                            PropertyName="TransitCostBy" DataTextField="CodeText" DataValueField="CodeValue"
                                            Enabled="False" />
                                    </td>
                                    <td>
                                        免运费订单金额：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFreeTransitAmount" PropertyName="FreeTransitAmount" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="plMerchantContactPerson" GroupingText="商家联系人" Width="1200" runat="server">
                        <div style="font-weight: normal">
                            <table>
                                <tr>
                                    <td colspan="8">
                                        <span><strong>业务联系人：</strong></span>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        联系人：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactPerson1" PropertyName="ContactPerson1" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        联系人职位：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPost1" PropertyName="Post1" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        联系人部门：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDept1" PropertyName="Dept1" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        联系人手机：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile1" PropertyName="Mobile1" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelephone1" PropertyName="Telephone1" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人邮箱：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail1" PropertyName="Email1" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <span><strong>售后联系人：</strong></span>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        联系人2：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactPerson2" PropertyName="ContactPerson2" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人2职位：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPost2" PropertyName="Post2" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人2部门：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDept2" PropertyName="Dept2" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        联系人2手机：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile2" PropertyName="Mobile2" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人2电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" PropertyName="Telephone2" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人2邮箱：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail2" PropertyName="Email2" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <span><strong>财务联系人：</strong></span>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        联系人3：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactPerson3" PropertyName="ContactPerson3" Enabled="False"
                                            Width="200" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人3职位：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPost3" PropertyName="Post3" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人3部门：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDept3" PropertyName="Dept3" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        联系人3手机：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile3" PropertyName="Mobile3" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人3电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelephone3" PropertyName="Telephone3" Width="200" Enabled="False"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系人3邮箱：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail3" PropertyName="Email3" Width="200" Enabled="False" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="plMerchantTax" GroupingText="商家证照" Width="1200" runat="server">
                        <div style="font-weight: normal">
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        税务登记证：
                                    </td>
                                    <td rowspan="4">
                                        <a id="linkMerchantImageTax" target="_blank" runat="server">
                                            <asp:Image ID="merchantImageTax" Width="205" Height="150" runat="server" />
                                        </a>
                                    </td>
                                    <td style="width: 100px">
                                        营业执照：
                                    </td>
                                    <td rowspan="4">
                                        <a id="linkMerchantImageBusiness" target="_blank" runat="server">
                                            <asp:Image ID="merchantImageBusiness" Width="205" Height="150" runat="server" />
                                        </a>
                                    </td>
                                    <td style="width: 100px">
                                        Logo：
                                    </td>
                                    <td rowspan="4">
                                        <a id="linkMerchantImage" target="_blank" runat="server">
                                            <asp:Image ID="merchantImage" Width="205" Height="150" runat="server" />
                                        </a>
                                    </td>
                                </tr>
                            </table>
                            <div style="height: 40px">
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPass" />
            <asp:AsyncPostBackTrigger ControlID="btnNotPass" />
        </Triggers>
    </asp:UpdatePanel>
    <div style="height: 40px">
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        var reasonText = document.getElementById("<%=txtReason.ClientID%>");
        function setchosen() {
            $("select[data-placeholder]:not(:disabled)").chosen();
        }
        setchosen();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(
            function (sender, e) {
                setchosen();
            }
        );
        function PriceCheckd() {
            if (!isOver(reasonText, 500)) {
                alert("审批原因长度超过500");
                reasonText.focus();
                return false;
            }
            if (!confirm("确认审核通过吗？")) {
                return false;
            }
            return true;
        }
        function ApproveBack() {
            if (!isOver(reasonText, 500)) {
                alert("审批原因长度超过500");
                reasonText.focus();
                return false;
            }
            if (reasonText.value.trim() == "") {
                alert('请填写不同意原因！');
                reasonText.focus();
                return false;
            }
            if (!confirm("确认审核不通过吗?")) {
                return false;
            }
            return true;
        }
        function isOver(sText, len) {
            var intlen = sText.value.trim().length;
            return intlen <= len;
        }

    </script>
</body>
</html>
