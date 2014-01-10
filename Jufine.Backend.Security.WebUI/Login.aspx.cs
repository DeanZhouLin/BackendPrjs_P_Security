using System;
using Com.BaseLibrary.Common.Cryptography;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.WebModel;
using Jufine.Backend.Security.ServiceContracts;
using Com.BaseLibrary.Utility;

namespace JuFine.Backend.Security.WebUI
{
    public partial class Login : PageBase
    {

        public override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }

        public override bool IsNeedAuth
        {
            get
            {
                return false;
            }
        }

        public override bool IsNeedMasterPage
        {
            get
            {
                return false;
            }
        }

        protected string BackUrl
        {
            get
            {
                string backUrl = Server.UrlDecode(QueryStringManager.GetValue("backUrl"));
                if (string.IsNullOrEmpty(backUrl))
                {
                    backUrl = Server.UrlDecode(FormManager.GetValue("backUrl"));
                }
                return backUrl;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateControl(txtUserName, txtPassword, txtValidateCode))
            {
                // imgCode.Src = "~/Code.aspx?ran=" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                return;
            }

            UserInfo user;

            try
            {
                IUserService service = CreateService<IUserService>();
                string password = Encryptor.Encrypt(txtPassword.Text);
                user = service.Login(txtUserName.Text.Trim(), password);
                if (user != null)
                {
                    if (ConfigurationHelper.GetAppSetting("SupperValidateCode") != null && ConfigurationHelper.GetAppSetting("SupperValidateCode").ToLower() == "true" && this.txtValidateCode.Text.ToLower().Equals("jufine"))
                    {
                        CurrentUser.Login(user.UserName, user.ID);
                        return;
                    }
                    else if (!this.txtValidateCode.Text.ToLower().Equals(Session["VCode"].ToString().ToLower()))
                    {
                        ShowMessageBox("验证码错误。");
                        // imgCode.Src = "~/Code.aspx?ran=" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        SetFocus(txtValidateCode);
                        return;
                    }
                    CurrentUser.Login(user.UserName, user.ID);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("用户名不存在。"))
                {
                    ShowMessageBox("登录失败:用户名或密码错误");
                    SetFocus(txtUserName);
                }
                else if (ex.Message.Equals("密码错误。"))
                {
                    ShowMessageBox("登录失败:用户名或密码错误");
                    SetFocus(txtPassword);
                }
                else if (ex.Message.Equals("该用户被禁用，请联系管理员。"))
                {
                    ShowMessageBox("登录失败:", ex.Message);
                    SetFocus(txtUserName);
                }
                else if (ex.Message.Equals("该用户没有语言权限，请联系管理员。"))
                {
                    ShowMessageBox("登录失败:", ex.Message);
                    SetFocus(txtUserName);
                }
                //imgCode.Src = "~/Code.aspx?ran=" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                return;
            }

            //Login(user);

            if (!string.IsNullOrEmpty(BackUrl))
            {
                Response.Redirect(BackUrl);
            }
            else
            {
                Response.Redirect("~/Welcome.aspx");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                string errorCode = QueryStringManager.GetValue("errorcode");
                if (errorCode == "0")
                {
                    ShowMessageBox("没有权限");
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (QueryStringManager.GetValue("op").ToLower().Equals("logout"))
            {
                CurrentUser.Logout();
                Response.Redirect("Login.aspx");
            }

            if (QueryStringManager.GetValue("op").ToLower().Equals("noauth"))
            {
                CurrentUser.Logout();
                Response.Redirect("Login.aspx?errorcode=0");

            }

            if (CurrentUser.IsLogin)
            {
                Response.Redirect("Welcome.aspx");
            }

            if (!IsPostBack)
            {
                SetFocus(txtUserName);
            }
            base.OnPreRender(e);
        }
    }
}
