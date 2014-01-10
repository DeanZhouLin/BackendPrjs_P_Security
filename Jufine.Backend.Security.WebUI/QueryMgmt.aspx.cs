using System;
using Com.BaseLibrary.Data;
using System.Data;
using Com.BaseLibrary.Utility;
using Com.BaseLibrary.Common.Cryptography;
using Jufine.Backend.WebModel;
using System.Text.RegularExpressions;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.WebUI
{
    public partial class QueryMgmt : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    AddEnterEscPress(plHeader, btnSearch, btnHide);
                    SetFocusControl(btnSearch);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void QueryData()
        {
            try
            {
                string connString = GetConnString();
                if (string.IsNullOrEmpty(stxtID.Text.ToString().Trim()))
                {
                    ShowMessageBox("请输入正确的查询字符串!");
                    return;
                }
                string sqlQueryString = FilterSqlStr(stxtID.Text.ToLower());
                if (string.IsNullOrEmpty(sqlQueryString))
                {
                    queryList.DataSource = null;
                    queryList.DataBind();
                    return;
                }

                //添加查询日志
                CreateOrUpdate();

                DataSet ds = SqlHelper.ExecuteDataset(connString, System.Data.CommandType.Text, sqlQueryString);
                queryList.DataSource = ds;
                queryList.DataBind();
                //queryList.Width = ds.Tables[0].Columns.Count * 200;

                upList.Update();
            }
            catch (Exception)
            {
                ShowMessageBox("请输入正确的查询字符串！");
                queryList.DataSource = null;
                queryList.DataBind();
            }
        }

        private string GetConnString()
        {
            string connectionString = ConfigurationHelper.GetConnectionString("SecurityConn");
            if (!connectionString.Contains(";"))
            {
                connectionString = Encryptor.Decrypt(connectionString);
            }
            return connectionString;
        }

        private string FilterSqlStr(string sqlString)
        {
            //返回true表示sqlstring 合法字符串 
            try
            {

                string strRegex = "(update |delete |insert |create |alter |drop |grant |deny |revoke |exec |truncate )";
                Regex regex = new System.Text.RegularExpressions.Regex(strRegex);
                //string strSql = sqlString.Trim().Replace(" ", "");
                //if (!strSql.StartsWith("selecttop"))
                //{
                //    ShowMessageBox("查询语句必须以select top 开头！");
                //    return "";
                //}
                if (regex.IsMatch(sqlString))
                {
                    ShowMessageBox("语句中不能包含update,delete,insert,create,alter,drop,grant,deny,revoke,exec,truncate");
                    return "";
                }
                return sqlString;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
                return "";
            }
        }

        private void CreateOrUpdate()
        {
            IQueryLogService service = CreateService<IQueryLogService>();
            QueryLog queryLog = null;
            queryLog = new QueryLog();
            FillEntityWithContentValue<QueryLog>(queryLog, plHeader);
            queryLog.UserID = CurrentUser.UserId.ToString();
            queryLog.CreateUser = CurrentUser.UserName;
            queryLog.CreateDate = DateTime.Now;
            queryLog.EditUser = CurrentUser.UserName;
            queryLog.SqlText = stxtID.Text;
            queryLog.EditDate = DateTime.Now;
            service.Create(queryLog);
            //ShowMessageBox("创建信息成功。");
            //ClearControlInput(plHeader);
            SetFocusControl(plHeader);
        }
    }
}