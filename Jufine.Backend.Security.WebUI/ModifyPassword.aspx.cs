using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.BaseLibrary.Common.Cryptography;
using Jufine.Backend.WebModel;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.WebUI
{
    public partial class ModifyPassword : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserName.Text = CurrentUser.UserName;
                SetFocusControl(txtCurrentPassword);
            }
        }

        public override bool IsNeedAuth
        {
            get
            {
                return false;
            }
        }

        protected void btnOk_OnClick(object sender, EventArgs e)
        {
            try
            {
                bool isValid = ValidateControl(pnlModifyPassword);
                if (!isValid)
                {
                    return;
                }

                if (!txtNewPassword.Text.Equals(txtNewPasswrod2.Text))
                {
                    ShowMessageBox("提交失败，发生以下错误：", "确认密码和新密码不一致。");
                    SetFocusControl(txtNewPasswrod2);
                    return;
                }

                IUserService userService = CreateService<IUserService>();
                UserInfo userInfo = userService.Get(CurrentUser.UserId);
                if (!Encryptor.Encrypt(txtCurrentPassword.Text).Equals(userInfo.Password))
                {
                    ShowMessageBox("当前密码不正确。");
                    return;
                }
                userInfo.Password = Encryptor.Encrypt(txtNewPassword.Text);
                userService.Update(userInfo);
                ShowMessageBox("修改成功。");
                ResetForm();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            txtCurrentPassword.Text = txtNewPassword.Text = txtNewPasswrod2.Text = string.Empty;
            SetFocusControl(txtCurrentPassword);
        }

    }
}