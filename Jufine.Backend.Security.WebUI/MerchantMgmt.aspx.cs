using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.BaseLibrary.Contract;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Utility;
using Jufine.Backend.IM.Business;
using Jufine.Backend.WebModel;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.BaseData.DataContracts;
using Jufine.Backend.BaseData.ServiceContracts;
using System.Data;

namespace Jufine.Backend.Security.WebUI
{
    public partial class MerchantMgmt : PageBase
    {

        private const string errorHeader = "提交失败";
        private const string ctlName = "aAudit";
        private const double timeOutValue = 0.17;//分钟
        private const string viewVisual = "/Security/MerchantSearchAuditMgmt.aspx";
        private const string historyVisual = "/Security/MerchantInfoHistoryDetailMgmt.aspx";

        private static readonly Dictionary<string, DateTime> TimeStampDic = new Dictionary<string, DateTime>();

        private static IUserMerchantService userMerchantService;
        private static IUserMerchantService UserMerchantService
        {
            get { return userMerchantService ?? (userMerchantService = CreateService<IUserMerchantService>()); }
        }

        private static ICodeValueService codeValueService;
        private static ICodeValueService CodeValueService
        {
            get { return codeValueService ?? (codeValueService = CreateService<ICodeValueService>()); }
        }

        private static IMerchantService merchantService;
        private static IMerchantService MerchantService
        {
            get { return merchantService ?? (merchantService = CreateService<IMerchantService>()); }
        }

        private static IMerchantInfoHistoryDetailService merchantInfoHistoryDetailService;
        private static IMerchantInfoHistoryDetailService MerchantInfoHistoryDetailService
        {
            get { return merchantInfoHistoryDetailService ?? (merchantInfoHistoryDetailService = CreateService<IMerchantInfoHistoryDetailService>()); }
        }

        private static List<CodeValueInfo> cooperateModeList;
        public static List<CodeValueInfo> CooperateModeList
        {
            get
            {
                DateTime dtNow = DateTime.Now;

                if (cooperateModeList == null)
                {
                    cooperateModeList = CodeValueService.GetCodeListByGroupCode("MerchantInfo_CooperationMode");
                    TimeStampDic.Add("CooperateModeList", dtNow);
                }

                if (ExecDateDiff(TimeStampDic["CooperateModeList"], dtNow) > timeOutValue)
                {
                    cooperateModeList = CodeValueService.GetCodeListByGroupCode("MerchantInfo_CooperationMode");
                    TimeStampDic["CooperateModeList"] = dtNow;
                }

                return cooperateModeList;
            }
        }

        private static List<CodeValueInfo> invoiceByList;
        public static List<CodeValueInfo> InvoiceByList
        {
            get
            {
                DateTime dtNow = DateTime.Now;

                if (invoiceByList == null)
                {
                    invoiceByList = CodeValueService.GetCodeListByGroupCode("InvoiceBy");
                    TimeStampDic.Add("InvoiceByList", dtNow);
                }

                if (ExecDateDiff(TimeStampDic["InvoiceByList"], dtNow) > timeOutValue)
                {
                    invoiceByList = CodeValueService.GetCodeListByGroupCode("InvoiceBy");
                    TimeStampDic["InvoiceByList"] = dtNow;
                }

                return invoiceByList;
            }
        }

        private static List<CodeValueInfo> transitCostByList;
        public static List<CodeValueInfo> TransitCostByList
        {
            get
            {
                DateTime dtNow = DateTime.Now;

                if (transitCostByList == null)
                {
                    transitCostByList = CodeValueService.GetCodeListByGroupCode("CostBy");
                    TimeStampDic.Add("TransitCostByList", dtNow);
                }

                if (ExecDateDiff(TimeStampDic["TransitCostByList"], dtNow) > timeOutValue)
                {
                    transitCostByList = CodeValueService.GetCodeListByGroupCode("CostBy");
                    TimeStampDic["TransitCostByList"] = dtNow;
                }

                return transitCostByList;
            }
        }

        private int? currentRowIndex;
        public int CurrentRowIndex
        {
            get
            {
                if (currentRowIndex != null) return currentRowIndex.Value;
                object rowIndex = ViewState["CURRENTROWINDEX"];
                currentRowIndex = rowIndex == null ? 0 : int.Parse(rowIndex.ToString());
                return currentRowIndex.Value;
            }
            set
            {
                ViewState["CURRENTROWINDEX"] = currentRowIndex = value;
            }
        }

        private QueryConditionInfo<UVMerchantInfo> UVQueryCondition
        {
            get
            {
                QueryConditionInfo<UVMerchantInfo> queryCondition = ViewState["UVMERCHANT_QUERYCONDITION"] as QueryConditionInfo<UVMerchantInfo>;
                if (queryCondition == null)
                {
                    queryCondition = new QueryConditionInfo<UVMerchantInfo>
                    {
                        IsAdmin = CurrentUser.IsAdmin || CurrentUser.HasPermssion("MERCHANTMGMTPMALLMERCHANT"),
                        MerchantList = UserMerchantService.GetByUserID(CurrentUser.UserId).Select(c => c.MerchantID).ToList()
                        //MerchantList = CurrentUser.PageMerchantList.Select(c => c.MerchantID).ToList()
                        //UVQueryCondition.MerchantList = UserMerchantService.GetByUserID(CurrentUser.UserId).Select(c => c.MerchantID).ToList();
                    };
                    ViewState["UVMERCHANT_QUERYCONDITION"] = queryCondition;
                }
                return queryCondition;
            }
        }

        private QueryConditionInfo<MerchantAdditionalCertificate> QueryConditionCertificate
        {
            get
            {
                QueryConditionInfo<MerchantAdditionalCertificate> queryCondition = ViewState["MERCHANT_CERTIFICATE_QUERYCONDITION"] as QueryConditionInfo<MerchantAdditionalCertificate>;
                if (queryCondition == null)
                {
                    queryCondition = new QueryConditionInfo<MerchantAdditionalCertificate>();
                    ViewState["MERCHANT_CERTIFICATE_QUERYCONDITION"] = queryCondition;
                }
                queryCondition.Condtion.MerchantID = Converter.ToInt32(hdMerchantID.Value, 0);
                return queryCondition;
            }
        }

        private UVMerchantInfo CurrentMerchant
        {
            get
            {
                return ViewState["CurrentMerchantID"] as UVMerchantInfo;
            }
            set
            {
                ViewState["CurrentMerchantID"] = value;
            }
        }

        private string CurrentUserName
        {
            get
            {
                return CurrentUser.UserName;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    InitData();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnPreviousItem.Enabled = CurrentRowIndex > 0;
            btnNextItem.Enabled = CurrentRowIndex < (gvMerchantList.Rows.Count - 1);
        }

        //查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValid = ValidateControl(plHeader);
                if (!isValid)
                {
                    return;
                }

                listPager.CurrentPageIndex = 1;

                FillEntityWithContentValue(UVQueryCondition.Condtion, plHeader);

                UVQueryCondition.Condtion.MI_CooperationMode = sddlCooperationModeList.SelectedValue;
                UVQueryCondition.Condtion.MI_ID = sddlMerchantID.SelectedValue.ParseInt();

                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        //全选
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbSelectAll = sender as CheckBox;
            foreach (GridViewRow row in gvMerchantList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }
                CheckBox ckbSelect = row.Cells[0].FindControl("ckbSelect") as CheckBox;

                if (ckbSelect != null)
                {
                    ckbSelect.Attributes.Add("onclick", "GvOnClick('" + row.ClientID + "')");
                    ckbSelect.Checked = ckbSelectAll.Checked;
                    row.CssClass = ckbSelect.Checked ? "selTr" : "";
                }
            }
            upList.Update();
        }

        //排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvMerchantList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                e.Cancel = true;
                SetSortOrder(UVQueryCondition, e.SortExpression);
                listPager.CurrentPageIndex = 0;
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }

        //分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        //列表数据绑定
        /// <summary>
        /// 列表数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvMerchantList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow row = e.Row;
                    CheckBox ckb = row.Cells[0].FindControl("ckbSelect") as CheckBox;
                    row.Attributes.Add("onmouseover", "GvOnMousemove('" + row.ClientID + "')");
                    ckb.Attributes.Add("onclick", "GvOnClick('" + row.ClientID + "')");
                }
            }
            catch (Exception)
            {
            }
        }

        //新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                //清空输入
                ClearControlInput(panelDetailInputArea);
                //绑定合作模式
                BindDDLControl(ddlCooperationModeList, CooperateModeList, true);
                //设置按钮显示/隐藏
                SetBtnInPropVisual(1000);
                //显示表头文字信息
                UpdateTitle();
                //初始化Tab页显示
                SetTabBoxFocus(0, txtMerchantName);
                //设置当前的商家为null
                CurrentMerchant = null;
                //显示弹出框
                modalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.ToString()));
            }
        }

        //省选择1
        /// <summary>
        /// 省选择1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBankProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<AreaInfo> cityList =
                    ddlBankProvince.SelectedValue == "0" ?
                    new List<AreaInfo>() :
                    GetAreaList(int.Parse(ddlBankProvince.SelectedValue));
                BindArea(ddlBankCity, cityList, true);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
        }

        //删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> idList = GetSelectedMerchantIdList();
            try
            {
                var keyList = idList.Select(c => c.Split('_')[1].ParseInt()).ToList();
                if (keyList.Count == 0)
                {
                    ShowMessageBox("请至少选择一条记录。");
                    return;
                }
                int chkCount = keyList.Count(key => MerchantService.GetUVMerchantInfo(key) != null);

                //if (chkCount != keyList.Count)
                //{
                //    ShowMessageBox("存在已经被删除的商家");
                //}
                //else
                //{
                //    MerchantService.BatchDeleteMerchant(keyList, CurrentUser.UserName);
                //    ShowMessageBox("批量删除成功");
                //}

                MerchantService.BatchDeleteMerchant(keyList, CurrentUser.UserName);
                ShowMessageBox(chkCount != keyList.Count ? "批量删除成功，包括已经被删除的商家" : "批量删除成功");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
            finally
            {
                QueryData();
                if (idList != null)
                {
                    SetCheckboxStatus(idList);
                }
            }
        }

        //激活
        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActive_Click(object sender, EventArgs e)
        {
            //获取所有选择项目
            List<string> idList = null;
            try
            {
                idList = GetSelectedMerchantIdList();
                string error = ActiveOrLockMerchant(idList, 1);
                ShowMessageBox(string.IsNullOrEmpty(error) ? "批量激活成功" : ErrorInfoBase.GetQuickError(error));
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
            finally
            {
                QueryData();
                if (idList != null)
                {
                    SetCheckboxStatus(idList);
                }
            }
        }

        //禁用
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLock_Click(object sender, EventArgs e)
        {
            //获取所有选择项目
            List<string> idList = null;
            try
            {
                idList = GetSelectedMerchantIdList();
                string error = ActiveOrLockMerchant(idList, 0);
                ShowMessageBox(string.IsNullOrEmpty(error) ? "批量禁用成功" : ErrorInfoBase.GetQuickError(error));
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
            finally
            {
                QueryData();
                if (idList != null)
                {
                    SetCheckboxStatus(idList);
                }
            }
        }

        //导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ReportMerchant();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
        }

        //上一条
        /// <summary>
        /// 上一条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPreviousItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentRowIndex > 0)
                {
                    CurrentRowIndex = CurrentRowIndex - 1;
                    LinkButton btn = gvMerchantList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;

                    if (btn == null)
                    {
                        ShowMessageBox(ErrorInfoBase.GetQuickError("未能获取到按钮数据，请刷新页面"));
                        modalPopupExtender.Hide();
                        return;
                    }

                    string key = btn.CommandArgument;
                    var res = key.Split('_').ToList();

                    if (res.Count == 2)
                    {
                        ShowDetail(res[1].ParseInt());
                        if (CurrentMerchant.EditUser != CurrentUserName && CurrentMerchant.Status == 2)
                        {
                            ShowMessageBox("注意：当前记录正在被" + CurrentMerchant.EditUser + "编辑");
                        }
                    }
                    else
                    {
                        ShowMessageBox(ErrorInfoBase.GetQuickError("参数错误，需要两个参数，请刷新页面"));
                        modalPopupExtender.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
        }

        //下一条
        /// <summary>
        /// 下一条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNextItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentRowIndex < gvMerchantList.Rows.Count - 1)
                {
                    CurrentRowIndex = CurrentRowIndex + 1;
                    LinkButton btn = gvMerchantList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
                    if (btn == null)
                    {
                        ShowMessageBox(ErrorInfoBase.GetQuickError("未能获取到按钮数据，请刷新页面"));
                        modalPopupExtender.Hide();
                        return;
                    }

                    string key = btn.CommandArgument;
                    var res = key.Split('_').ToList();

                    if (res.Count == 2)
                    {
                        ShowDetail(res[1].ParseInt());
                        if (CurrentMerchant.EditUser != CurrentUserName && CurrentMerchant.Status == 2)
                        {
                            ShowMessageBox("注意：当前记录正在被" + CurrentMerchant.EditUser + "编辑");
                        }
                    }
                    else
                    {
                        ShowMessageBox(ErrorInfoBase.GetQuickError("参数错误，需要两个参数，请刷新页面"));
                        modalPopupExtender.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
        }

        //编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {

                //显示上_下页
                btnPreviousItem.Visible = true;
                btnNextItem.Visible = true;

                BindDDLControl(ddlCooperationModeList, CooperateModeList, true);
                SetTabBoxFocus(0, txtMerchantName, false);

                LinkButton btn = sender as LinkButton;

                if (btn == null)
                {
                    ShowMessageBox(ErrorInfoBase.GetQuickError("未能获取到按钮数据，请刷新页面"));
                    return;
                }

                string key = btn.CommandArgument;
                var res = key.Split('_').ToList();

                if (res.Count == 2)
                {
                    CurrentRowIndex = Int32.Parse(btn.Attributes["RowIndex"]);
                    ShowDetail(res[1].ParseInt());
                    if (CurrentMerchant.EditUser != CurrentUser.UserName && CurrentMerchant.Status == 2)
                    {
                        ShowMessageBox("注意：当前记录正在被" + CurrentMerchant.EditUser + "编辑");
                        modalPopupExtender.Hide();
                    }
                }
                else
                {
                    ShowMessageBox(ErrorInfoBase.GetQuickError("参数错误，需要两个参数，请刷新页面"));
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }
        }

        //审核（未使用，前台隐藏）
        /// <summary>
        /// 审核（未使用，前台隐藏）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAudit_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            string itemID = btn.Attributes["ItemID"];
            string merchantID = btn.Attributes["MerchantID"];

            string url = Request.Url.AbsoluteUri.Replace("MerchantMgmt.aspx", "") + btn.CommandArgument;
            if (CurrentUser.HasPermssion("/Security/MerchantSearchAuditMgmt.aspx".ToUpper()))
            {
                var currentUVMerchantInfo = MerchantService.GetUVMerchantInfo(itemID.ParseInt(), merchantID.ParseInt());

                if (currentUVMerchantInfo == null)
                {
                    ShowMessageBox("记录状态已改变，将刷新页面。");
                    QueryData();
                    return;
                }

                var currentUserAuditWorkFlowResource = CurrentUser.GetUserAuditWorkFlowResource(ctlName, currentUVMerchantInfo.AuditUser, CurrentResourcePage.ID, AllSubResouceList);
                if (!currentUserAuditWorkFlowResource.HasAuth)
                {
                    ShowMessageBox("记录状态已改变，将刷新页面。");
                    QueryData();
                    return;
                }
                string str = "window.open('" + url + "','_blank','width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no, menu=no')";
                ExecuteJavascript(str);
            }
            else
            {
                ShowMessageBox("当前登录用户没有配置审核页面的权限。");
            }
        }

        //保存商家信息
        /// <summary>
        /// 保存商家信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            UVMerchantInfo uvMerchant = null;
            try
            {
                var btn = sender as Button;
                string res = SavePartialMerchant(MerchantInfo.GetMerchantInfoParts(btn.CommandArgument), out uvMerchant);
                if (!string.IsNullOrEmpty(res))
                {
                    ShowMessageBox(res);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
                if (!ex.Message.Contains("商家名称或组织机构代码或税务登记号"))
                {
                    modalPopupExtender.Hide();
                }
                else
                {
                    modalPopupExtender.Show();
                }
            }
            finally
            {
                if (uvMerchant != null)
                {
                    QueryData();
                    ShowDetail(uvMerchant.MerchantID.ParseInt());
                }
                else
                {
                    QueryData();
                }
            }
        }

        //提交审核
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitAuditing_Click(object sender, EventArgs e)
        {
            try
            {
                UVMerchantInfo uvMerchant;
                string res = SavePartialMerchant(MerchantInfoParts.All, out uvMerchant);
                if (!string.IsNullOrEmpty(res))
                {

                    int id = uvMerchant.ID;
                    int merchantID = uvMerchant.MerchantID.ParseInt();
                    int fromHistory = uvMerchant.FromHistory.ParseInt();

                    MerchantService.ChangeStatusMerchant(id, 4, CurrentUserName, fromHistory);
                    ShowDetail(merchantID);

                    ShowMessageBox("提交审核成功");
                }
                else
                {
                    if (uvMerchant == null)
                    {
                        //modalPopupExtender.Hide();
                    }
                    else
                    {
                        ShowDetail(uvMerchant.MerchantID.ParseInt());
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
                //if (!ex.Message.Contains("商家名称或组织机构代码或税务登记号"))
                //{
                //    modalPopupExtender.Hide();
                //}
                //else
                //{
                //    modalPopupExtender.Show();
                //}
            }
            finally
            {
                QueryData();
            }
        }

        //撤消
        /// <summary>
        /// chexiao
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnDo_Click(object sender, EventArgs e)
        {
            try
            {
                string error = BackAndUnDo(-1);
                ShowMessageBox(string.IsNullOrEmpty(error) ? "撤消成功" : error);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
                modalPopupExtender.Hide();
            }
            finally
            {
                QueryData();
            }
        }

        //撤回
        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                string error = BackAndUnDo(2);
                ShowMessageBox(string.IsNullOrEmpty(error) ? "撤回成功" : error);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
                modalPopupExtender.Hide();
            }
            finally
            {
                QueryData();
            }
        }

        //退出
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            modalPopupExtender.Hide();
        }

        //Tab页
        /// <summary>
        /// Tab页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TabBox_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabBox.ActiveTabIndex == 1)
            {
                //if (hdIsAdd.Value == "1")
                //    GetProductImageListByNO();
                //else
                //GetItemImageListByNO();
                ////btnDetail.Text = "保存图片";
                //btnDetail.Visible = false;
                //upDetail.Update();
            }
        }

        //上传证件
        /// <summary>
        /// 上传证件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkImage_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
            string merchantName = btn.Attributes["MerchantName"];
            ShowUploadImage(key, merchantName);
        }

        //排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvMerchantAdditionalCertificateList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                e.Cancel = true;
                SetSortOrder(QueryConditionCertificate, e.SortExpression);
                listPager.CurrentPageIndex = 0;
                QueryCertificateData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }

        //分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void listPagerCertificate_PageChanged(object sender, EventArgs e)
        {
            try
            {
                QueryCertificateData();
            }
            catch (Exception ex)
            {

                ShowMessageBox(ex.Message);
            }

        }

        //编辑证件
        /// <summary>
        /// 编辑证件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEditCertificate_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
            hdCertificateID.Value = key.ToString();
            ShowCertificate(key);
        }

        //删除证件
        /// <summary>
        /// 删除证件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDeleteCertificate_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
            IMerchantAdditionalCertificateService service = CreateService<IMerchantAdditionalCertificateService>();
            service.Delete(key);
            otxtTitle.Text = "";
            otxtDisplayOrder.Text = "";
            (merchantOtherImage as UCUploadFile).ImageName = "";
            QueryCertificateData();
            upCertificateList.Update();
            upUploadImage.Update();
            ShowMessageBox("删除成功");
        }

        //保存证件
        /// <summary>
        /// 保存证件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveOtherImage_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValid = ValidateControl(panelUploadImageInputArea);
                if (!isValid)
                {
                    QueryCertificateData();
                    return;
                }
                if (StringUtil.IsNullOrEmpty((merchantOtherImage as UCUploadFile).ImageName))
                {
                    ShowMessageBox("证件图片没有上传。");
                    return;
                }
                CreateOrUpdateOtherImage();
                QueryCertificateData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }

        //退出证件
        /// <summary>
        /// 退出证件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelOtherImage_Click(object sender, EventArgs e)
        {
            modalPopupExtenderUploadImage.Hide();
        }

        //用户
        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCreateUser_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
            if (CurrentUser.IsAdmin || CurrentUser.PageMerchantList.Select(c => c.MerchantID).Contains(key))
            {
                Response.Redirect(WebsiteUrl + "UserMgmt.aspx?merchantid=" + key);
            }
            else
            {
                ShowMessageBox("当前登录用户没有管理该商家用户的权限。");
            }
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            BindDDLControl(sddlCooperationModeList, CooperateModeList, true, "", "全部");
            BindDDLControl(ddlInvoiceBy, InvoiceByList);
            BindDDLControl(ddlTransitCostBy, TransitCostByList);
            BindDDLControl(sddlMerchantID, MerchantService.GetAllMerchant(), true);

            //省
            BindArea(ddlBankProvince, GetAreaList(0), true);

            UVQueryCondition.Condtion.MI_ID = 0;

            QueryData();
            AddEnterEscPress(panelDetailInputArea, btnSave, btnCancel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryData()
        {

            UVQueryCondition.PageIndex = listPager.CurrentPageIndex;
            UVQueryCondition.PageSize = listPager.PageSize;

            QueryResultInfo<UVMerchantInfo> result = MerchantService.QueryUVMerchantInfo(UVQueryCondition, CurrentUser, ctlName, CurrentResourcePage.ID, AllSubResouceList);

            while (result.RecordList.Count == 0 && listPager.CurrentPageIndex > 1)
            {
                UVQueryCondition.PageIndex = --listPager.CurrentPageIndex;
                result = MerchantService.QueryUVMerchantInfo(UVQueryCondition, CurrentUser, ctlName, CurrentResourcePage.ID, AllSubResouceList);
            }

            if (CooperateModeList != null)
            {
                foreach (var item in result.RecordList)
                {

                    item.S_ViewVisual = CurrentUser.HasPermssion(viewVisual.ToUpper());
                    item.S_HistoryVisual &= CurrentUser.HasPermssion(historyVisual.ToUpper());

                    CodeValueInfo value = CooperateModeList.FirstOrDefault(c => c.CodeValue == item.CooperationMode);
                    item.CooperationModeDisplay = value == null ? string.Empty : value.CodeText;

                    value = CooperateModeList.FirstOrDefault(c => c.CodeValue == item.MI_CooperationMode);
                    item.MI_CooperationModeDisplay = value == null ? string.Empty : value.CodeText;

                    value = CooperateModeList.FirstOrDefault(c => c.CodeValue == item.S_CooperationMode);
                    item.S_CooperationModeDisplay = value == null ? string.Empty : value.CodeText;

                }
            }

            SetOrderHeaderStyle(gvMerchantList, UVQueryCondition);
            gvMerchantList.DataSource = result.RecordList;
            gvMerchantList.DataBind();

            NoRecords<UVMerchantInfo>(gvMerchantList);
            listPager.RecordCount = result.RecordCount;

            upList.Update();

        }

        #region 控制按钮的显示隐藏

        private void SetBtnVisualInSaveExit(bool visiable = false)
        {
            btnSubmitAuditing.Visible = visiable;//提交审批
            btnSave.Visible = visiable;//保存全部
            btnBack.Visible = visiable;//撤回
            btnUnDo.Visible = visiable;
        }

        private void SetBtnVisualInTabPanel(bool visiable = false)
        {
            btnSaveContactperson.Visible = visiable;
            btnSaveCooperation.Visible = visiable;
            btnSaveLicense.Visible = visiable;
            btnSaveMerchantPreview.Visible = visiable;
        }

        private void SetTabEnable(bool enable = false)
        {
            panelDetailInputAreaMerchantPreview.Enabled = enable;
            panelDetailInputAreaCooperation.Enabled = enable;
            panelDetailInputAreaContactperson.Enabled = enable;
            panelDetailInputAreaLicense.Enabled = enable;
            btnMinFreeTA.Visible = enable;
            btnMaxFreeTA.Visible = enable;
            //merchantImageTax.Visible = enable;
            //merchantImageBusiness.Visible = enable;
            //merchantImage.Visible = enable;
        }

        /// <summary>
        /// 控制弹出框中的按钮的显示隐藏
        /// -1 删除 | 0 禁用 | 1 激活 | 2 草稿 | 3 审核不通过 | 4 待审核
        /// TabPanel中有4个【保存】button
        /// SaveExit中有【保存全部】、【提交审核】、【撤回】、【退出】4个button
        /// </summary>
        /// <param name="status"></param>
        /// <param name="editUserName"></param>
        private void SetBtnInPropVisual(int status, string editUserName = "")
        {

            //初始化 全部隐藏
            SetBtnVisualInSaveExit();
            SetBtnVisualInTabPanel();
            SetTabEnable();

            switch (status)
            {
                case 0: //禁用
                    SetBtnVisualInTabPanel(true);//显示所有的在TabPanel中的【保存按钮】
                    btnSubmitAuditing.Visible = false;//提交审批
                    btnSave.Visible = true;//保存全部
                    btnBack.Visible = false;//撤回
                    btnUnDo.Visible = false;//撤销
                    SetTabEnable(true);
                    break;
                case 1://激活
                    SetBtnVisualInTabPanel(true);//显示所有的在TabPanel中的【保存按钮】
                    btnSubmitAuditing.Visible = false;//提交审批
                    btnSave.Visible = true;//保存全部
                    btnBack.Visible = false;//撤回
                    btnUnDo.Visible = false;//撤销
                    SetTabEnable(true);
                    break;
                case 2: //草稿
                    btnBack.Visible = false;//撤回
                    if (CurrentUser.UserName == editUserName)
                    {
                        SetBtnVisualInTabPanel(true);//显示所有的在TabPanel中的【保存按钮】
                        btnSave.Visible = true;//保存全部
                        btnSubmitAuditing.Visible = true;//提交审批
                        btnUnDo.Visible = true;//撤销
                        SetTabEnable(true);
                    }
                    break;
                case 3://审核不通过
                    SetBtnVisualInTabPanel(true);//显示所有的在TabPanel中的【保存按钮】
                    btnSubmitAuditing.Visible = false;//提交审批
                    btnSave.Visible = true;//保存全部
                    btnBack.Visible = false;//撤回
                    btnUnDo.Visible = false;//撤销
                    SetTabEnable(true);
                    break;
                case 4://待审核
                    if (CurrentUser.UserName == editUserName)
                    {
                        btnBack.Visible = true; //撤回
                    }
                    btnUnDo.Visible = false;//撤销
                    break;
                default://新增
                    SetBtnVisualInTabPanel(true);//显示所有的在TabPanel中的【保存按钮】
                    btnSubmitAuditing.Visible = false;//提交审批
                    btnSave.Visible = true;//保存全部
                    btnBack.Visible = false;//撤回
                    btnNextItem.Visible = false;//上一条
                    btnPreviousItem.Visible = false;//下一条
                    btnUnDo.Visible = false;//撤销
                    SetTabEnable(true);
                    ddlCooperationModeList.SelectedIndex = 0;
                    ddlCooperationModeList.Enabled = true;
                    break;
            }

            upBtnChangeStatus.Update();
            upOpContactperson.Update();
            upOpCooperation.Update();
            upOpLicense.Update();
            upOpMerchantPreview.Update();

        }

        #endregion

        /// <summary>
        /// 显示详情
        /// </summary>
        /// <param name="merchantID"></param>
        private void ShowDetail(int merchantID)
        {

            UVMerchantInfo merchant = MerchantService.GetUVMerchantInfoByMerchantID(merchantID, CurrentUserName);

            if (merchant == null)
            {
                throw new Exception("无商家信息");
            }

            var authEntity = CurrentUser.GetUserAuditWorkFlowResource(ctlName, merchant.AuditUser, CurrentResourcePage.ID, AllSubResouceList);


            //当前操作实体赋值
            CurrentMerchant = merchant;

            //int status = merchant.IsSelfEdit ? merchant.Status.ParseInt() : merchant.MI_Status.ParseInt();
            int status = merchant.Status.ParseInt();

            #region 填充数据实体

            FillContentValueWithEntity(merchant, panelDetailInputArea);

            if (merchant.BankProvinceID != null && merchant.BankProvinceID.ParseInt() > 0)
            {
                BindArea(ddlBankCity, GetAreaList(merchant.BankProvinceID.ParseInt()), true);
                var cityID = merchant.BankCityID;
                foreach (ListItem item in ddlBankCity.Items)
                {
                    if (item.Value == cityID.ParseString())
                    {
                        ddlBankCity.SelectedValue = item.Value;
                        break;
                    }
                }
            }

            ckEditorMerchantDescription.Text = merchant.MerchantDescription;

            if (merchant.ContractStart != null)
                txtContractStart.Text = StringUtil.IsNullOrEmpty(merchant.ContractStart)
                    ? string.Empty
                    : merchant.ContractStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (merchant.ContractEnd != null)
                txtContractEnd.Text = StringUtil.IsNullOrEmpty(merchant.ContractEnd)
                    ? string.Empty
                    : merchant.ContractEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");

            (merchantImage as UCUploadFile).ImageName = merchant.Logo;
            (merchantImageTax as UCUploadFile).ImageName = merchant.TaxRegistrationCertificateURL;
            (merchantImageBusiness as UCUploadFile).ImageName = merchant.BusinessLicenseURL;


            #endregion

            if (ddlCooperationModeList.SelectedIndex != 0 && (merchant.FromHistory == 1 || (merchant.FromHistory == 0 && (merchant.Status == 1 || merchant.Status == 0))))
            {
                ddlCooperationModeList.Enabled = false;
            }
            else
            {
                ddlCooperationModeList.Enabled = true;
            }

            if (status == 2)//"草稿"
            {
                merchant.S_Status = merchant.IsSelfEdit ? merchant.Status : merchant.MI_Status;
                merchant.S_Strstatus = merchant.IsSelfEdit ? merchant.Strstatus : merchant.MI_Strstatus;
            }
            else if (status == 4 && (merchant.IsSelfEdit || authEntity.HasAuth))//"待审核"
            {
                merchant.S_Status = merchant.Status;
                merchant.S_Strstatus = merchant.Strstatus;
            }
            else
            {
                merchant.S_Status = merchant.MI_Status;
                merchant.S_Strstatus = merchant.MI_Strstatus;
            }

            StringBuilder sb = new StringBuilder("编辑商家-");
            sb.Append("【商家编号：").Append(merchant.MerchantID).Append("】");
            sb.Append("【商家状态：").Append(merchant.S_Strstatus).Append("】");
            sb.Append("【来源于：").Append((merchant.FromHistory == 1 ? "商家历史详情表" : "商家详情表")).Append("】");
            sb.Append("【最新编辑人：").Append(merchant.EditUser).Append("】");
            UpdateTitle(sb.ToString());

            SetBtnInPropVisual(status, CurrentMerchant.EditUser);
            modalPopupExtender.Show();

        }

        #region 保存前检查

        private bool CheckMerchantPreview()
        {
            bool isValid = ValidateControl(panelDetailInputAreaMerchantPreview);

            if (!isValid)
            {
                SetTabBoxFocus(0);
                return false;
            }


            if (ddlBankProvince.SelectedValue == "0")
            {
                SetTabBoxFocus(0, ddlBankProvince);
                ShowMessageBox(ErrorInfoBase.GetQuickError("请选择开户行省", errorHeader));
                return false;
            }

            if (ddlBankCity.SelectedValue == "0")
            {
                SetTabBoxFocus(0, ddlBankCity);
                ShowMessageBox(ErrorInfoBase.GetQuickError("请选择开户行市", errorHeader));
                return false;
            }

            return true;
        }

        private bool CheckCooperation()
        {

            bool isValid = ValidateControl(panelDetailInputAreaCooperation);

            if (!isValid)
            {
                SetTabBoxFocus(1);
                return false;
            }


            if (ddlCooperationModeList.SelectedValue == "0")
            {
                SetTabBoxFocus(1, ddlCooperationModeList);
                ShowMessageBox(ErrorInfoBase.GetQuickError("请选择合作模式", errorHeader));
                return false;
            }


            if (!string.IsNullOrEmpty(txtContractStart.Text) && !string.IsNullOrEmpty(txtContractEnd.Text))
            {
                if (DateTime.Parse(txtContractStart.Text) > DateTime.Parse(txtContractEnd.Text))
                {
                    SetTabBoxFocus(1, txtContractStart);
                    ShowMessageBox(ErrorInfoBase.GetQuickError("合同开始时间应小于结束时间", errorHeader));
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtContractEnd.Text))
            {
                if (DateTime.Now > DateTime.Parse(txtContractEnd.Text))
                {
                    SetTabBoxFocus(1, txtContractEnd);
                    ShowMessageBox(ErrorInfoBase.GetQuickError("合同结束时间应大于当前时间", errorHeader));
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtGuarantyFunds.Text))
            {
                if (Convert.ToDecimal(txtGuarantyFunds.Text) < 0)
                {
                    SetTabBoxFocus(1, txtGuarantyFunds);
                    ShowMessageBox(ErrorInfoBase.GetQuickError("保证金不能为负数", errorHeader));
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtUsageCharges.Text))
            {
                if (Convert.ToDecimal(txtUsageCharges.Text) < 0)
                {
                    SetTabBoxFocus(1, txtUsageCharges);
                    ShowMessageBox(ErrorInfoBase.GetQuickError("使用费用不能为负数", errorHeader));
                    return false;
                }
            }

            if (ddlCooperationModeList.SelectedValue == "1")
            {
                if (string.IsNullOrEmpty(txtCommissionRatio.Text.Trim()))
                {
                    SetTabBoxFocus(1, txtCommissionRatio);
                    ShowMessageBox(ErrorInfoBase.GetQuickError("平台服务的商家佣金比率必填", errorHeader));
                    return false;
                }
            }

            return true;

        }

        private bool CheckContactperson()
        {
            bool isValid = ValidateControl(panelDetailInputAreaContactperson);
            if (!isValid)
            {
                SetTabBoxFocus(2);
                return false;
            }
            return true;
        }

        private bool CheckLicense()
        {

            bool isValid = ValidateControl(panelDetailInputAreaLicense);
            if (!isValid)
            {
                SetTabBoxFocus(3);
                return false;
            }

            if (StringUtil.IsNullOrEmpty((merchantImage as UCUploadFile).ImageName) || StringUtil.IsNullOrEmpty((merchantImageBusiness as UCUploadFile).ImageName) || StringUtil.IsNullOrEmpty((merchantImageTax as UCUploadFile).ImageName))
            {
                if (StringUtil.IsNullOrEmpty((merchantImageTax as UCUploadFile).ImageName))
                {
                    ShowMessageBox(ErrorInfoBase.GetQuickError("税务登记证图片没有上传", errorHeader));
                    SetTabBoxFocus(3);
                    return false;
                }
                if (StringUtil.IsNullOrEmpty((merchantImageBusiness as UCUploadFile).ImageName))
                {
                    ShowMessageBox(ErrorInfoBase.GetQuickError("营业执照图片没有上传", errorHeader));
                    SetTabBoxFocus(3);
                    return false;
                }
                if (StringUtil.IsNullOrEmpty((merchantImage as UCUploadFile).ImageName))
                {
                    ShowMessageBox(ErrorInfoBase.GetQuickError("商家Logo图片没有上传", errorHeader));
                    SetTabBoxFocus(3);
                    return false;
                }
            }
            return true;
        }

        private bool CheckValid(MerchantInfoParts merchantInfoParts)
        {
            switch (merchantInfoParts)
            {
                case MerchantInfoParts.MerchantPreview:
                    return CheckMerchantPreview();
                case MerchantInfoParts.License:
                    return CheckLicense();
                case MerchantInfoParts.Cooperation:
                    return CheckCooperation();
                case MerchantInfoParts.Contactperson:
                    return CheckContactperson();
                case MerchantInfoParts.All:
                    return CheckMerchantPreview() && CheckCooperation() && CheckContactperson() && CheckLicense();
            }
            return false;
        }

        #endregion

        /// <summary>
        /// 保存商家信息，带回最新的UV实体
        /// </summary>
        /// <param name="merchantInfoParts"></param>
        /// <param name="uvMerchant"></param>
        /// <returns></returns>
        private string SavePartialMerchant(MerchantInfoParts merchantInfoParts, out UVMerchantInfo uvMerchant)
        {

            uvMerchant = null;

            DateTime dtNow = DateTime.Now;

            //参数检查
            var isValid = CheckValid(merchantInfoParts);

            if (!isValid)
            {
                return "";
            }

            //检查操作是否有效
            CheckResult chkRes = CheckPreOpration(out uvMerchant, true);

            if (chkRes.HasError)
            {
                ShowMessageBox(chkRes.GetError());
                return "";
            }

            if (chkRes.OPType != OPType.NewData && uvMerchant != null && CurrentUserName != uvMerchant.EditUser && uvMerchant.Status == 2)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError("商家信息正在被" + uvMerchant.EditUser + "编辑，需要" + uvMerchant.EditUser + "撤销编辑后才能进行保存"));
                return "";
            }

            #region 初始化需要保存的实体

            MerchantInfo merchant = new MerchantInfo
            {
                CreateUser = CurrentUserName,
                CreateDate = dtNow
            };

            MerchantInfoHistoryDetail merchantHistoryDetail = new MerchantInfoHistoryDetail
            {
                CreateUser = CurrentUserName,
                CreateDate = dtNow
            };

            switch (chkRes.OPType)
            {

                case OPType.InMerchantInfo:
                    int mID = chkRes.StoredUvMerchantInfo.MerchantID.ParseInt();
                    merchant = MerchantService.Get(mID);
                    merchantHistoryDetail = MerchantService.CopyToMerchantInfoHistoryDetail(merchant, merchantHistoryDetail);
                    break;

                case OPType.FromMerchantInfoHistoryDetail:
                    merchantHistoryDetail = MerchantInfoHistoryDetailService.Get(chkRes.ID);
                    break;

            }

            #endregion

            #region 使用对应的Panel填充实体

            Control panel = null;

            switch (merchantInfoParts)
            {
                case MerchantInfoParts.MerchantPreview:
                    panel = panelDetailInputAreaMerchantPreview;
                    break;
                case MerchantInfoParts.License:
                    panel = panelDetailInputAreaLicense;
                    break;
                case MerchantInfoParts.Cooperation:
                    panel = panelDetailInputAreaCooperation;
                    break;
                case MerchantInfoParts.Contactperson:
                    panel = panelDetailInputAreaContactperson;
                    break;
                case MerchantInfoParts.All:
                    panel = panelDetailInputArea;
                    break;
            }

            FillEntityWithContentValue(merchant, panel);
            FillEntityWithContentValue(merchantHistoryDetail, panel);

            #endregion

            #region 其它信息填充进入实体

            merchantHistoryDetail.EditUser = merchant.EditUser = CurrentUserName;
            merchantHistoryDetail.EditDate = merchant.EditDate = dtNow;

            switch (merchantInfoParts)
            {
                case MerchantInfoParts.MerchantPreview:
                    merchantHistoryDetail.MerchantDescription = merchant.MerchantDescription = ckEditorMerchantDescription.Text; //商家描述
                    merchantHistoryDetail.DisplayName = merchant.DisplayName = txtDisplayName.Text; //商家显示名称
                    merchantHistoryDetail.BankProvince = merchant.BankProvince = ddlBankProvince.SelectedItem.Text;
                    merchantHistoryDetail.BankProvinceID = merchant.BankProvinceID = ddlBankProvince.SelectedItem.Value.ParseInt();
                    merchantHistoryDetail.BankCity = merchant.BankCity = ddlBankCity.SelectedItem.Text;
                    merchantHistoryDetail.BankCityID = merchant.BankCityID = ddlBankCity.SelectedItem.Value.ParseInt();
                    break;
                case MerchantInfoParts.License:
                    merchantHistoryDetail.Logo = merchant.Logo = (merchantImage as UCUploadFile).ImageName; //Logo
                    merchantHistoryDetail.BusinessLicenseURL = merchant.BusinessLicenseURL = (merchantImageBusiness as UCUploadFile).ImageName; //营业执照
                    merchantHistoryDetail.TaxRegistrationCertificateURL = merchant.TaxRegistrationCertificateURL = (merchantImageTax as UCUploadFile).ImageName; //税务登记证
                    break;
                case MerchantInfoParts.Cooperation:
                    break;
                case MerchantInfoParts.Contactperson:
                    break;
                case MerchantInfoParts.All:

                    #region

                    merchantHistoryDetail.MerchantDescription = merchant.MerchantDescription = ckEditorMerchantDescription.Text; //商家描述
                    merchantHistoryDetail.DisplayName = merchant.DisplayName = txtDisplayName.Text; //商家显示名称
                    merchantHistoryDetail.BankProvince = merchant.BankProvince = ddlBankProvince.SelectedItem.Text;
                    merchantHistoryDetail.BankProvinceID = merchant.BankProvinceID = ddlBankProvince.SelectedItem.Value.ParseInt();
                    merchantHistoryDetail.BankCity = merchant.BankCity = ddlBankCity.SelectedItem.Text;
                    merchantHistoryDetail.BankCityID = merchant.BankCityID = ddlBankCity.SelectedItem.Value.ParseInt();
                    merchantHistoryDetail.Logo = merchant.Logo = (merchantImage as UCUploadFile).ImageName; //Logo
                    merchantHistoryDetail.BusinessLicenseURL = merchant.BusinessLicenseURL = (merchantImageBusiness as UCUploadFile).ImageName; //营业执照
                    merchantHistoryDetail.TaxRegistrationCertificateURL = merchant.TaxRegistrationCertificateURL = (merchantImageTax as UCUploadFile).ImageName; //税务登记证

                    #endregion

                    break;

            }

            if (string.IsNullOrEmpty(merchant.MerchantName))
            {
                merchant.MerchantName = string.Empty;
            }

            #endregion

            string res = string.Empty;

            int merchantID = 0;

            switch (chkRes.OPType)
            {
                case OPType.NewData:
                    try
                    {
                        merchant.Status = 2;
                        merchant.CreateDate = dtNow;
                        merchant.CreateUser = CurrentUserName;
                        merchant.EditDate = dtNow;
                        merchant.EditUser = CurrentUserName;

                        MerchantService.Create(merchant, CurrentUser.IsAdmin ? -1 : CurrentUser.UserId);
                        merchantID = merchant.ID;
                        MerchantService.BatchChangeStatus(new List<int> { merchantID }, 2, CurrentUserName);

                        res = merchantInfoParts == MerchantInfoParts.All ? "创建信息成功" : "保存信息成功";
                    }
                    catch (Exception ex)
                    {
                        uvMerchant = null;
                        throw new Exception(ex.Message);
                    }
                    break;

                case OPType.InMerchantInfo:

                    uvMerchant = MerchantService.GetUVMerchantInfoByMerchantID(merchant.ID, CurrentUser.UserName);
                    if (uvMerchant == null)
                    {
                        throw new Exception("商家数据被删除");
                    }

                    //如果商家数据表中的状态是：删除，禁用，启用，审核不通过；则插入一条历史详情表的数据
                    if (new List<int> { -1, 0, 1, 3 }.Contains(merchant.Status.ParseInt()))
                    {
                        merchantHistoryDetail.ID = uvMerchant.ID;
                        merchantHistoryDetail.MerchantID = uvMerchant.MerchantID;
                        merchantHistoryDetail.Status = 2;

                        merchantHistoryDetail.AuditDate = null;
                        merchantHistoryDetail.AuditUser = null;
                        merchantHistoryDetail.Reason = null;

                        merchantHistoryDetail.CreateDate = dtNow;
                        merchantHistoryDetail.CreateUser = CurrentUserName;

                        merchant.EditDate = dtNow;
                        merchant.EditUser = CurrentUserName;

                        MerchantService.CreateHistoryDetail(merchantHistoryDetail);

                        merchantID = merchantHistoryDetail.MerchantID.ParseInt();
                    }
                    else
                    {
                        merchant.ID = uvMerchant.ID;
                        merchant.EditDate = dtNow;
                        merchant.EditUser = CurrentUserName;
                        merchant.Status = 2;
                        try
                        {
                            MerchantService.Update(merchant);
                            merchantID = merchant.ID;
                        }
                        catch (Exception ex)
                        {
                            uvMerchant = null;
                            throw new Exception(ex.Message);
                        }
                    }
                    res = "更新信息成功";
                    break;

                case OPType.FromMerchantInfoHistoryDetail:

                    uvMerchant = MerchantService.GetUVMerchantInfoByMerchantID(merchantHistoryDetail.MerchantID.ParseInt(), CurrentUser.UserName);
                    if (uvMerchant == null)
                    {
                        throw new Exception("商家数据被删除");
                    }

                    merchantHistoryDetail.ID = uvMerchant.ID;
                    merchantHistoryDetail.MerchantID = uvMerchant.MerchantID;
                    merchantHistoryDetail.Status = 2;

                    merchantHistoryDetail.AuditDate = null;
                    merchantHistoryDetail.AuditUser = null;
                    merchantHistoryDetail.Reason = null;

                    merchantHistoryDetail.EditDate = dtNow;
                    merchantHistoryDetail.EditUser = CurrentUserName;

                    MerchantService.UpdateHistoryDetail(merchantHistoryDetail);
                    merchantID = uvMerchant.MerchantID.ParseInt();
                    res = "更新信息成功";
                    break;

            }

            uvMerchant = MerchantService.GetUVMerchantInfoByMerchantID(merchantID, CurrentUser.UserName);
            if (uvMerchant == null)
            {
                throw new Exception("商家数据被删除");
            }
            return res;
        }

        /// <summary>
        /// 操作前检查数据的存在性，数据是否被改变过，带回最新的UV实体
        /// </summary>
        /// <param name="uvMerchantInfo"></param>
        /// <param name="newDataExists">是否包含新增操作</param>
        /// <returns>操作的类型：数据来源【新增，Info表中，HistoryDetail表中】，当前操作对象，商家</returns>
        private CheckResult CheckPreOpration(out UVMerchantInfo uvMerchantInfo, bool newDataExists = false)
        {

            uvMerchantInfo = null;
            CheckResult chkRes = new CheckResult();

            if (CurrentMerchant == null)
            {
                if (!newDataExists)
                {
                    chkRes.AddError("当前商家信息为空，请刷新页面后重试");
                    modalPopupExtender.Hide();
                    return chkRes;
                }
                chkRes.OPType = OPType.NewData;
            }
            else
            {

                chkRes.StoredUvMerchantInfo = CurrentMerchant;  //当前存储的数据
                chkRes.ID = chkRes.StoredUvMerchantInfo.ID;

                var merchantID = chkRes.StoredUvMerchantInfo.MerchantID.ParseInt();

                uvMerchantInfo = MerchantService.GetUVMerchantInfoByMerchantID(merchantID, CurrentUser.UserName);

                if (uvMerchantInfo == null)
                {
                    chkRes.AddError("商家数据被删除");
                    modalPopupExtender.Hide();
                    return chkRes;
                }

                if (uvMerchantInfo.EditDate != chkRes.StoredUvMerchantInfo.EditDate)
                {
                    chkRes.AddError("商家数据已被更改，请重新检查后，再行操作");
                    return chkRes;
                }

                switch (uvMerchantInfo.FromHistory)
                {
                    case 0:
                        chkRes.OPType = OPType.InMerchantInfo;
                        break;
                    case 1:
                        chkRes.OPType = OPType.FromMerchantInfoHistoryDetail;
                        break;
                }
            }

            if (chkRes.OPType == 0)
            {
                chkRes.AddError("未能获取正确的操作类型");
                modalPopupExtender.Hide();
                return chkRes;
            }

            return chkRes;

        }

        /// <summary>
        /// 激活/禁用
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private string ActiveOrLockMerchant(ICollection<string> idList, int status)
        {

            if (idList.Count == 0)
            {
                return "请至少选择一条记录";
            }

            List<int> idListAll = new List<int>();
            List<int> idListInMerchantInfo = new List<int>();
            List<int> idListInHistoryDetail = new List<int>();
            List<int> merchantIdListInHistoryDetail = new List<int>();

            foreach (var item in idList)
            {
                int merchantId = item.Split('_')[1].ParseInt();

                UVMerchantInfo merchantInfo = MerchantService.GetUVMerchantInfoByMerchantID(merchantId, CurrentUser.UserName);
                if (merchantInfo == null)
                {
                    continue;
                }
                if (merchantInfo.Status == 0 || merchantInfo.Status == 1 || merchantInfo.MI_Status == 0 || merchantInfo.MI_Status == 1)
                {

                    int id = merchantInfo.ID;
                    merchantId = merchantInfo.MerchantID.ParseInt();

                    idListAll.Add(merchantId);

                    if (merchantInfo.FromHistory == 0)
                    {
                        idListInMerchantInfo.Add(id);
                    }
                    else
                    {
                        idListInHistoryDetail.Add(id);
                        merchantIdListInHistoryDetail.Add(merchantId);
                    }
                }
            }

            if (idList.Count != idListInMerchantInfo.Count + idListInHistoryDetail.Count)
            {
                return "存在不处于启用/禁用状态或已经被删除的商家";
            }

            if (idListAll.Count > 0)
            {
                MerchantService.BatchChangeStatusMerchant(idListAll, status, CurrentUser.UserName);
            }

            //if (idListInHistoryDetail.Count > 0)
            //{
            //    MerchantService.BatchChangeStatusMerchant(idListInHistoryDetail, status, CurrentUser.UserName, 1);
            //    MerchantService.BatchChangeStatusMerchant(merchantIdListInHistoryDetail, status, CurrentUser.UserName);
            //}

            return "";

        }

        /// <summary>
        /// 撤回/撤销
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string BackAndUnDo(int status)
        {
            UVMerchantInfo uvMerchantInfo;
            int merchantID = -1;

            CheckResult chkResult = CheckPreOpration(out uvMerchantInfo);
            if (uvMerchantInfo != null)
            {
                merchantID = uvMerchantInfo.MerchantID.ParseInt();
            }
            if (chkResult.HasError)
            {
                if (uvMerchantInfo != null)
                {
                    ShowDetail(merchantID);
                }
                return chkResult.GetError();
            }

            int id = uvMerchantInfo.ID;
            int fromHistory = uvMerchantInfo.FromHistory.ParseInt();

            MerchantService.ChangeStatusMerchant(id, status, CurrentUserName, fromHistory);

            if (fromHistory == 0 && status == 2)
            {
                var temp = MerchantService.Get(id);
                temp.AuditUser = null;
                temp.AuditDate = null;
                temp.Reason = null;
                MerchantService.Update(temp);
            }
            else if (fromHistory == 1 && status == 2)
            {
                var temp = MerchantInfoHistoryDetailService.Get(id);
                temp.AuditUser = null;
                temp.AuditDate = null;
                temp.Reason = null;
                MerchantInfoHistoryDetailService.Update(temp);
            }

            if (status == -1 && fromHistory == 0)
            {
                modalPopupExtender.Hide();
            }
            else
            {
                ShowDetail(merchantID);
            }
            return "";
        }

        /// <summary>
        /// 导出
        /// </summary>
        private void ReportMerchant()
        {

            UVQueryCondition.PageIndex = listPager.CurrentPageIndex;
            UVQueryCondition.PageSize = listPager.PageSize;
            UVQueryCondition.ReturnAllData = true;

            QueryResultInfo<UVMerchantInfo> result = MerchantService.QueryUVMerchantInfo(UVQueryCondition, CurrentUser, ctlName, CurrentResourcePage.ID, AllSubResouceList);

            List<UVMerchantInfo> merchantInfoList;
            if (result.RecordList.Count != 0)
            {
                merchantInfoList = result.RecordList;
            }
            else
            {
                ShowMessageBox("查询条件不存在记录");
                QueryData();
                return;
            }
            DataTable dt = ListUtil.ConvertToTable(merchantInfoList);
            string[] columnNames =
            { 
                "MI_ID", 
                "MI_MerchantName", 
                "MI_DisplayName", 
                "MI_LegalRepresentative",
                "MI_TaxNO",
                "MI_OrganizationCode",
                "MI_OpeningBank",
                "MI_BankCardNO",
                "MI_Telephone",
                "MI_Fax",
                "MI_Fax2",
                "MI_Address",
                "MI_PostalCode",
                "MI_Email",
                "MI_Website",
                "MI_ServicePhone",
                "MI_Strstatus",
                "MI_CooperationModes",
                "MI_ContractStartStr",
                "MI_ContractEndStr", 
                "MI_GuarantyFunds",
                "MI_UsageCharges", 
                "MI_PaymentCycle",
                "MI_CommissionRatio",
                "MI_ReturnMerchantRatio",
                "MI_ReturnMallRatio",
                "MI_InvoiceBys"
            };

            DateTime date = DateTime.Now;
            string excelName = date.ToString("yyyy-MM-d") + "-" + date.ToString("yyyy-MM-d") + "-商家数据";
            DataTable excelDT = dt.DefaultView.ToTable(false, columnNames);
            excelDT.Columns["MI_ID"].ColumnName = "商家编号";
            excelDT.Columns["MI_MerchantName"].ColumnName = "商家名称";
            excelDT.Columns["MI_DisplayName"].ColumnName = "显示名称";
            excelDT.Columns["MI_LegalRepresentative"].ColumnName = "法人";
            excelDT.Columns["MI_TaxNO"].ColumnName = "税务登记号";
            excelDT.Columns["MI_OrganizationCode"].ColumnName = "组织机构代码";
            excelDT.Columns["MI_OpeningBank"].ColumnName = "开户行";
            excelDT.Columns["MI_BankCardNO"].ColumnName = "银行账号";
            excelDT.Columns["MI_Telephone"].ColumnName = "联系电话";
            excelDT.Columns["MI_Fax"].ColumnName = "传真";
            excelDT.Columns["MI_Fax2"].ColumnName = "传真2";
            excelDT.Columns["MI_Address"].ColumnName = "地址";
            excelDT.Columns["MI_PostalCode"].ColumnName = "邮编";
            excelDT.Columns["MI_Email"].ColumnName = "邮箱";
            excelDT.Columns["MI_Website"].ColumnName = "官网";
            excelDT.Columns["MI_ServicePhone"].ColumnName = "服务电话";
            excelDT.Columns["MI_Strstatus"].ColumnName = "状态";
            excelDT.Columns["MI_CooperationModes"].ColumnName = "合作模式";
            excelDT.Columns["MI_ContractStartStr"].ColumnName = "合同开始";
            excelDT.Columns["MI_ContractEndStr"].ColumnName = "合同结束";
            excelDT.Columns["MI_GuarantyFunds"].ColumnName = "保证金";
            excelDT.Columns["MI_UsageCharges"].ColumnName = "使用费用";
            excelDT.Columns["MI_PaymentCycle"].ColumnName = "账期";
            excelDT.Columns["MI_CommissionRatio"].ColumnName = "佣金比率";
            excelDT.Columns["MI_ReturnMerchantRatio"].ColumnName = "返商家比率";
            excelDT.Columns["MI_ReturnMallRatio"].ColumnName = "返商城比率";
            excelDT.Columns["MI_InvoiceBys"].ColumnName = "开票方";
            ExcelUtil.SaveToExcel(this, excelDT, "", excelName);

        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryCertificateData()
        {
            IMerchantAdditionalCertificateService service = CreateService<IMerchantAdditionalCertificateService>();
            QueryConditionCertificate.PageIndex = listPagerCertificate.CurrentPageIndex;
            QueryConditionCertificate.PageSize = listPagerCertificate.PageSize;
            QueryResultInfo<MerchantAdditionalCertificate> result = service.Query(QueryConditionCertificate);

            SetOrderHeaderStyle(gvMerchantAdditionalCertificateList, QueryConditionCertificate);
            gvMerchantAdditionalCertificateList.DataSource = result.RecordList;
            gvMerchantAdditionalCertificateList.DataBind();
            NoRecords<MerchantAdditionalCertificate>(gvMerchantAdditionalCertificateList);
            listPagerCertificate.RecordCount = result.RecordCount;
            upCertificateList.Update();
        }

        /// <summary>
        /// 显示证件详情
        /// </summary>
        /// <param name="key"></param>
        private void ShowCertificate(int key)
        {
            try
            {
                IMerchantAdditionalCertificateService service = CreateService<IMerchantAdditionalCertificateService>();
                MerchantAdditionalCertificate certificate = service.Get(key);
                hdMerchantID.Value = certificate.MerchantID.ToString();
                FillContentValueWithEntity(certificate, upUploadImage);
                (merchantOtherImage as UCUploadFile).ImageName = certificate.ImangeName;
                upUploadImage.Update();
                SetFocus(otxtTitle);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        /// <summary>
        /// 显示证件图片
        /// </summary>
        /// <param name="key"></param>
        /// <param name="merchantName"></param>
        private void ShowUploadImage(Int32 key, string merchantName)
        {
            try
            {
                modalPopupExtenderUploadImage.Show();
                hdMerchantID.Value = key.ToString();
                lblMerchantName.Text = merchantName;
                ckbImageStatus.Checked = true;
                QueryCertificateData();
                upCertificateList.Update();
                upUploadImage.Update();
                SetFocus(otxtTitle);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        /// <summary>
        /// 创建/更新其它证件信息
        /// </summary>
        private void CreateOrUpdateOtherImage()
        {

            IMerchantAdditionalCertificateService service = CreateService<IMerchantAdditionalCertificateService>();
            MerchantAdditionalCertificate certificate;

            if (string.IsNullOrEmpty(hdCertificateID.Value))
            {
                certificate = new MerchantAdditionalCertificate
                {
                    CreateUser = CurrentUser.UserName,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                Int32 key = StringUtil.ToType<Int32>(hdCertificateID.Value);
                certificate = service.Get(key);
            }

            FillEntityWithContentValue(certificate, panelUploadImageInputArea);
            certificate.EditUser = CurrentUser.UserName;
            certificate.EditDate = DateTime.Now;
            certificate.Status = ckbImageStatus.Checked ? 1 : 0;
            certificate.ImangeName = (merchantOtherImage as UCUploadFile).ImageName;
            certificate.MerchantID = Converter.ToInt32(hdMerchantID.Value, 0);

            if (string.IsNullOrEmpty(hdCertificateID.Value))
            {
                service.Create(certificate);
                ShowMessageBox("创建信息成功。");
                ClearControlInput(panelUploadImageInputArea);
                ckbImageStatus.Checked = true;
                hdMerchantID.Value = certificate.MerchantID.ToString();
                SetFocusControl(otxtTitle);
            }
            else
            {
                service.Update(certificate);
                ShowMessageBox("更新信息成功。");
            }

        }


        private void UpdateTitle(string title = "新增商家")
        {
            lblTiltle.Text = title;
            upTitle.Update();
        }

        private void SetTabBoxFocus(int tabIndex, Control ctl = null, bool isUpdateUP = true)
        {
            TabBox.ActiveTabIndex = tabIndex;
            if (ctl != null)
            {
                ctl.Focus();
            }
            if (isUpdateUP)
            {
                upDetail.Update();
            }
        }

        private List<string> GetSelectedMerchantIdList()
        {
            List<string> idList = new List<string>();
            foreach (GridViewRow row in gvMerchantList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }
                CheckBox ckbSelect = row.FindControl("ckbSelect") as CheckBox;
                if (ckbSelect.Checked)
                {
                    idList.Add(ckbSelect.ToolTip);
                }
            }
            return idList;
        }

        private static double ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            //你想转的格式
            return ts3.TotalMinutes;
        }

        private static void BindDDLControl<T>(ListControl ddl, List<T> list, bool flag = false, string defualtValue = "0", string defaultText = "请选择")
        {
            ddl.DataSource = list;
            ddl.DataBind();
            if (flag)
            {
                ListItem item = new ListItem { Value = defualtValue, Text = defaultText };
                ddl.Items.Insert(0, item);
            }
        }

        private void SetCheckboxStatus(List<string> idList)
        {
            foreach (GridViewRow row in gvMerchantList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }
                CheckBox ckbSelect = row.FindControl("ckbSelect") as CheckBox;
                if (ckbSelect != null)
                {
                    ckbSelect.Checked = idList.Exists(c => c.ToString() == ckbSelect.ToolTip);
                    if (ckbSelect.Checked)
                    {
                        row.CssClass = "selTr";
                    }
                }
            }
        }

        private static void BindArea(ListControl ddl, List<AreaInfo> list, bool flag)
        {
            try
            {
                if (list == null)
                {
                    list = new List<AreaInfo>();
                }
                ddl.Items.Clear();
                ddl.DataSource = list;
                if (flag)
                {
                    list.Insert(0, new AreaInfo { AreaCode = "-1", AreaName = "请选择" });
                }
                ddl.DataBind();
            }
            catch (Exception)
            {
            }
        }

        private static List<AreaInfo> GetAreaList(int parentId)
        {
            IAreaService areaService = CreateService<IAreaService>();
            return areaService.GetAreaByParentID(parentId);
        }


        private class CheckResult : ErrorInfoBase
        {
            public OPType OPType { get; set; }
            public UVMerchantInfo StoredUvMerchantInfo { get; set; }
        }

        private enum OPType
        {
            NewData = 1, InMerchantInfo = 2, FromMerchantInfoHistoryDetail = 4
        }

    }
}
