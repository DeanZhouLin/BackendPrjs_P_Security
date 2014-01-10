<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JuFine.Backend.Security.WebUI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>聚好商城后台管理系统</title>
    <link type="text/css" rel="stylesheet" href="global.css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="home_page">
        <div id="home_header">
            <div style="float: left;padding-top: 5px;">
            <h1 id="logo">
               <a href="#"><img src="images/logo_home.png" alt="商家管理信息平台"></a>
                    <strong>商家管理信息平台</strong></h1></div>
            <ul class="site_link">
                <li>联系电话：400-603-6300<b>|</b></li>
                <li><a href="mailto:customer@jufine.com">邮件我们</a></li>
            </ul>
        </div>
        <div id="home_container">
            <div class="posterbj">
                <img src="images/bg_home.jpg" alt="" />
            </div>
            <div class="form">
                <h2>
                    用户登录</h2>
                <div class="form-list">
                    <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="jh_item">
                                <span class="jh_label">
                                    <label for="jh_nick">
                                        用户名：</label></span>
                                <asp:TextBox CssClass="i-text" ID="txtUserName" Title="用户名" Rel="req" runat="server"
                                    placeholder="请输入用户名"></asp:TextBox>
                                <div class="msg-box" style="display: none">
                                    <div class="msg-error">
                                        不能为空</div>
                                </div>
                            </div>
                            <div class="jh_item">
                                <span class="jh_label">
                                    <label for="jh_Pwd">
                                        密&nbsp;&nbsp;&nbsp;码：</label></span>
                                <asp:TextBox CssClass="i-text" TextMode="Password" ID="txtPassword" Title="密码" Rel="req"
                                    runat="server" placeholder="请输入密码"></asp:TextBox>
                                <div class="msg-box" style="display: none">
                                    <div class="msg-error">
                                        不能为空</div>
                                </div>
                            </div>
                            <div class="jh_item">
                                <span class="jh_label">
                                    <label for="jh_Pwd">
                                        验证码：</label></span>
                                <asp:TextBox CssClass="i-text" ID="txtValidateCode" Title="验证码" Rel="req" Width="80"
                                    runat="server" placeholder="请输入验证码"></asp:TextBox>
                                <img id="imgCode" src='Code.aspx?ran=<%=DateTime.Now.ToString("yyyyMMddHHmmssfff") %>'
                                    style="height: 30px;" />
                                <a href="#" onclick="GetCode()">换一张</a>
                                <div class="msg-box" style="display: none">
                                    <div class="msg-error">
                                        不能为空</div>
                                </div>
                            </div>
                            <div class="jh_item">
                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" CssClass="ui_btn"
                                    Text="登 录" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLogin" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="home_footer">
            <p>
                Copyright&copy;2012 上海聚好信息技术有限公司</p>
        </div>
    </div>
    <script type="text/javascript">
        function GetCode() {
            document.getElementById('imgCode').src = "Code.aspx?ran=" + Math.random();
        }

    </script>
    </form>
</body>
</html>
