<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True"
    CodeBehind="ModifyPassword.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.ModifyPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
input.butt_true {background:url(MasterPageDir/Themes/Resource/miniwindow/butt_true.gif) no-repeat;border:none;width:42px;height:19px; cursor:pointer}
input.butt_cancel {background:url(MasterPageDir/Themes/Resource/miniwindow/butt_cancel.gif) no-repeat;border:none;width:42px;height:19px; cursor:pointer}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td style="text-align: left; vertical-align: top; padding: 10px;">
            <asp:Panel ID="pnlModifyPassword" runat="server">
                <asp:UpdatePanel ID="upModifyPassword" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <table  >
                        <tr>
                            <td style="width: 80px">
                                用户名：
                            </td>
                            <td>
                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                当前密码：
                            </td>
                            <td>
                                <asp:TextBox MaxLength="16" Width="200px" Title="当前密码" Rel="req|len:1~16" TextMode="Password"
                                    ID="txtCurrentPassword" runat="server"></asp:TextBox><span runat="server" id="starPassword"
                                        style="color: Red">*</span>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                新密码：
                            </td>
                            <td>
                                <asp:TextBox MaxLength="16" Width="200px" Title="新密码" Rel="req|len:6~16" TextMode="Password"
                                    ID="txtNewPassword" runat="server"></asp:TextBox><span runat="server" id="Span1"
                                        style="color: Red">*</span>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                确认密码：
                            </td>
                            <td>
                                <asp:TextBox MaxLength="16" Width="200px" Title="确认密码" Rel="confirm:txtNewPassword" TextMode="Password"
                                    ID="txtNewPasswrod2" runat="server"></asp:TextBox><span runat="server" id="Span2"
                                        style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr>
                        <td></td>
                        <td align="right">  <asp:Button ID="btnOK" runat="server" OnClick="btnOk_OnClick"  Text="保存" />
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick"  Text="取消" /></td>
                        </tr>
                 
                    </table>
                    </ContentTemplate>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnOK" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            </td>
        </tr>
    </table>


                                      
                                    
</asp:Content>

