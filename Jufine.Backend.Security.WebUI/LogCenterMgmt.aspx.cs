using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Utility;

using Jufine.Backend.WebModel;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Com.BaseLibrary.Data;
using Com.BaseLibrary.Common.Cryptography;
using System.Data;

namespace Jufine.Backend.Security.WebUI
{
    public partial class LogCenterMgmt : PageBase
    {
        private QueryConditionInfo<LogCenter> QueryCondition
        {
            get
            {
                QueryConditionInfo<LogCenter> queryCondition
                    = ViewState["LOGCENTER_QUERYCONDITION"] as QueryConditionInfo<LogCenter>;
                if (queryCondition == null)
                {
                    queryCondition = new QueryConditionInfo<LogCenter>();
                    ViewState["LOGCENTER_QUERYCONDITION"] = queryCondition;
                }
                return queryCondition;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    QueryCondition.Condtion.ApplicationName = "全部";
                    QueryCondition.Condtion.Module = "全部";
                    QueryCondition.Condtion.LogType = "全部";
                    QueryData();
                    BindContorls();
                    AddEnterEscPress(panelDetailInputArea, btnSave, btnCancel);


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
                listPager.CurrentPageIndex = 1;
                FillEntityWithContentValue<LogCenter>(QueryCondition.Condtion, plHeader);
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            ClearControlInput(panelDetailInputArea);
            SetFocus(txtID);
            modalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<Int64> keyList = new List<Int64>();
            foreach (GridViewRow row in gvLogCenterList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }
                CheckBox ckbSelect = row.FindControl("ckbSelect") as CheckBox;
                if (ckbSelect.Checked)
                {
                    Int64 key = StringUtil.ToType<Int64>(ckbSelect.ToolTip);
                    keyList.Add(key);
                }
            }

            if (keyList.Count == 0)
            {
                ShowMessageBox("请至少选择一条记录。");
                return;
            }

            try
            {
                ILogCenterService service = CreateService<ILogCenterService>();
                service.BatchDelete(keyList);
                QueryData();
                ShowMessageBox("删除成功");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }


        protected void gvLogCenterList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                e.Cancel = true;
                SetSortOrder<LogCenter>(QueryCondition, e.SortExpression);
                listPager.CurrentPageIndex = 0;
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }


        protected void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbSelectAll = sender as CheckBox;
            foreach (GridViewRow row in gvLogCenterList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }
                CheckBox ckbSelect = row.Cells[0].FindControl("ckbSelect") as CheckBox;
                if (ckbSelect != null)
                {
                    ckbSelect.Checked = ckbSelectAll.Checked;
                }
            }
            upList.Update();
        }

        private int? currentRowIndex;
        public int CurrentRowIndex
        {
            get
            {
                if (currentRowIndex == null)
                {
                    object rowIndex = ViewState["CURRENTROWINDEX"];
                    if (rowIndex == null)
                    {
                        currentRowIndex = 0;
                    }
                    else
                    {
                        currentRowIndex = int.Parse(rowIndex.ToString());
                    }
                }
                return currentRowIndex.Value;
            }
            set
            {
                ViewState["CURRENTROWINDEX"] = currentRowIndex = value;
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Int64 key = StringUtil.ToType<Int64>(btn.CommandArgument);
            CurrentRowIndex = int.Parse(btn.Attributes["RowIndex"]);
            ShowDetail(key);
        }

        private void ShowDetail(Int64 key)
        {
            try
            {
                ILogCenterService service = CreateService<ILogCenterService>();
                LogCenter logCenter = service.Get(key);
                FillContentValueWithEntity<LogCenter>(logCenter, panelDetailInputArea);
                modalPopupExtender.Show();
                hdID.Value = key.ToString();
                upDetail.Update();
                SetFocus(txtID);
            }
            catch (Exception ex)
            {

                ShowMessageBox(ex.Message);
            }
        }

        protected void btnPreviousItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentRowIndex > 0)
                {
                    CurrentRowIndex = CurrentRowIndex - 1;
                    LinkButton btn = gvLogCenterList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
                    Int64 key = StringUtil.ToType<Int64>(btn.CommandArgument);
                    ShowDetail(key);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        protected void btnNextItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentRowIndex < gvLogCenterList.Rows.Count - 1)
                {
                    CurrentRowIndex = CurrentRowIndex + 1;
                    LinkButton btn = gvLogCenterList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
                    Int64 key = StringUtil.ToType<Int64>(btn.CommandArgument);
                    ShowDetail(key);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        protected void listPager_PageChanged(object sender, EventArgs e)
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValid = ValidateControl(panelDetailInputArea);
                if (!isValid)
                {
                    return;
                }
                CreateOrUpdate();
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            modalPopupExtender.Hide();
        }
        public void BindItemTypeList(DropDownList drpObj, string dsname)
        {
            ILogCenterService service = CreateService<ILogCenterService>();
            DataSet ds = service.Ddlname(GetConnString(), sql(dsname));

            int s = ds.Tables[0].Rows.Count;
            if (ds.Tables.Count > 0)
            {
                drpObj.DataSource = ds;
                drpObj.DataTextField = "Text";
                drpObj.DataValueField = "Value";
                drpObj.DataBind();

            }
        }
        /// <summary>
        /// 绑定ApplicationName  Module    LogType
        /// </summary>
        private void BindContorls()
        {
            BindItemTypeList(sddlApplicationNameID, "ApplicationName");
            BindItemTypeList(sddlModuleID, "Module");
            BindItemTypeList(sddlLogTypeID, "LogType");
        }
        public string sql(string name)
        {
            return "SELECT '选择全部' Text,'全部' Value UNION ALL SELECT DISTINCT " + name + " Text, " + name + " Value FROM [Security].[dbo].[LogCenter]";
        }
        private void QueryData()
        {
            ILogCenterService service = CreateService<ILogCenterService>();
            QueryCondition.PageIndex = listPager.CurrentPageIndex;
            QueryCondition.PageSize = listPager.PageSize;
            QueryResultInfo<LogCenter> result = service.Query(QueryCondition);

            SetOrderHeaderStyle(gvLogCenterList, QueryCondition);
            gvLogCenterList.DataSource = result.RecordList;
            gvLogCenterList.DataBind();
            NoRecords<LogCenter>(gvLogCenterList);
            listPager.RecordCount = result.RecordCount;
            upList.Update();
        }


        private void CreateOrUpdate()
        {
            ILogCenterService service = CreateService<ILogCenterService>();
            LogCenter logCenter = null;

            if (string.IsNullOrEmpty(hdID.Value))
            {
                logCenter = new LogCenter();
                FillEntityWithContentValue<LogCenter>(logCenter, panelDetailInputArea);
                service.Create(logCenter);
                ShowMessageBox("创建信息成功。");
                ClearControlInput(panelDetailInputArea);
                SetFocusControl(txtID);
            }
            else
            {
                Int64 key = StringUtil.ToType<Int64>(hdID.Value);
                logCenter = service.Get(key);
                FillEntityWithContentValue<LogCenter>(logCenter, panelDetailInputArea);
                service.Update(logCenter);
                modalPopupExtender.Hide();
                ShowMessageBox("更新信息成功。");
            }


        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnPreviousItem.Enabled = CurrentRowIndex > 0;
            btnNextItem.Enabled = CurrentRowIndex < (gvLogCenterList.Rows.Count - 1);
        }



    }
}
