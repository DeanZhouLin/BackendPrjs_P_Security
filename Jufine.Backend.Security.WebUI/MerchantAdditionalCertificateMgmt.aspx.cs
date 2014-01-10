using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Utility;

using Jufine.Backend.WebModel;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;

namespace Jufine.Backend.Security.WebUI
{
    public partial class MerchantAdditionalCertificateMgmt : PageBase
    {
        private QueryConditionInfo<MerchantAdditionalCertificate> QueryCondition
        {
            get
            {
                QueryConditionInfo<MerchantAdditionalCertificate> queryCondition
                    = ViewState["MERCHANTADDITIONALCERTIFICATE_QUERYCONDITION"] as QueryConditionInfo<MerchantAdditionalCertificate>;
                if (queryCondition == null)
                {
                    queryCondition = new QueryConditionInfo<MerchantAdditionalCertificate>();
                    ViewState["MERCHANTADDITIONALCERTIFICATE_QUERYCONDITION"] = queryCondition;
                }
                return queryCondition;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    QueryData();
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
                FillEntityWithContentValue<MerchantAdditionalCertificate>(QueryCondition.Condtion,plHeader);
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
            btnPreviousItem.Visible = false;
            btnNextItem.Visible = false;
            modalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<Int32> keyList = new List<Int32>();
            foreach (GridViewRow row in gvMerchantAdditionalCertificateList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }
                CheckBox ckbSelect = row.FindControl("ckbSelect") as CheckBox;
                if (ckbSelect.Checked)
                {
                    Int32 key = StringUtil.ToType<Int32>(ckbSelect.ToolTip);
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
                IMerchantAdditionalCertificateService service = CreateService<IMerchantAdditionalCertificateService>();
                service.BatchDelete(keyList);
                QueryData();
                ShowMessageBox("删除成功");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }


        protected void gvMerchantAdditionalCertificateList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                e.Cancel = true;
                SetSortOrder<MerchantAdditionalCertificate>(QueryCondition, e.SortExpression);
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
            foreach (GridViewRow row in gvMerchantAdditionalCertificateList.Rows)
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
            Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
            btnPreviousItem.Visible = true;
            btnNextItem.Visible = true;
            CurrentRowIndex = int.Parse(btn.Attributes["RowIndex"]);
            ShowDetail(key);
        }
        
        private void ShowDetail(Int32 key)
        {
            try
            {
                IMerchantAdditionalCertificateService service = CreateService<IMerchantAdditionalCertificateService>();
                MerchantAdditionalCertificate merchantAdditionalCertificate = service.Get(key);
                FillContentValueWithEntity<MerchantAdditionalCertificate>(merchantAdditionalCertificate,panelDetailInputArea);
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
                    LinkButton btn = gvMerchantAdditionalCertificateList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
                    Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
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
                if (CurrentRowIndex <gvMerchantAdditionalCertificateList.Rows.Count-1)
                {
                    CurrentRowIndex = CurrentRowIndex + 1;
                    LinkButton btn = gvMerchantAdditionalCertificateList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
                    Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
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

        private void QueryData()
        {
            IMerchantAdditionalCertificateService service = CreateService<IMerchantAdditionalCertificateService>();
            QueryCondition.PageIndex = listPager.CurrentPageIndex;
            QueryCondition.PageSize = listPager.PageSize;
            QueryResultInfo<MerchantAdditionalCertificate> result = service.Query(QueryCondition);

            SetOrderHeaderStyle(gvMerchantAdditionalCertificateList, QueryCondition);
            gvMerchantAdditionalCertificateList.DataSource = result.RecordList;
            gvMerchantAdditionalCertificateList.DataBind();
            NoRecords<MerchantAdditionalCertificate>(gvMerchantAdditionalCertificateList);
            listPager.RecordCount = result.RecordCount;
            upList.Update();
        }


        private void CreateOrUpdate()
        {
            IMerchantAdditionalCertificateService service = CreateService<IMerchantAdditionalCertificateService>();
            MerchantAdditionalCertificate merchantAdditionalCertificate = null;

            if (string.IsNullOrEmpty(hdID.Value))
            {
                merchantAdditionalCertificate = new MerchantAdditionalCertificate();
                FillEntityWithContentValue<MerchantAdditionalCertificate>(merchantAdditionalCertificate,panelDetailInputArea);
                merchantAdditionalCertificate.CreateUser = CurrentUser.UserName;
				merchantAdditionalCertificate.CreateDate = DateTime.Now;
                merchantAdditionalCertificate.EditUser = CurrentUser.UserName;
				merchantAdditionalCertificate.EditDate = DateTime.Now;
                service.Create(merchantAdditionalCertificate);
                ShowMessageBox("创建信息成功。");
                ClearControlInput(panelDetailInputArea);
                SetFocusControl(txtID);
            }
            else
            {
                Int32 key = StringUtil.ToType<Int32>(hdID.Value);
                merchantAdditionalCertificate=service.Get(key);
                FillEntityWithContentValue<MerchantAdditionalCertificate>(merchantAdditionalCertificate,panelDetailInputArea);
                merchantAdditionalCertificate.EditUser = CurrentUser.UserName;
				merchantAdditionalCertificate.EditDate = DateTime.Now;
                service.Update(merchantAdditionalCertificate);
                modalPopupExtender.Hide();
                ShowMessageBox("更新信息成功。");
            }


        }
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnPreviousItem.Enabled = CurrentRowIndex > 0;
            btnNextItem.Enabled = CurrentRowIndex < (gvMerchantAdditionalCertificateList.Rows.Count-1);
        }
    }
}
