using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Com.BaseLibrary.Common.Security;
using Com.BaseLibrary.Contract;
using Com.BaseLibrary.Utility;

using Jufine.Backend.BaseData.DataContracts;
using Jufine.Backend.BaseData.ServiceContracts;
using Jufine.Backend.IM.Business;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.WebModel;
using Jufine.Backend.WebModel.Request;

namespace Jufine.Backend.Security.WebUI
{
    public partial class MerchantSearchAuditMgmt : PageBase
    {
        public QueryParameter CurrentParameter
        {
            get
            {
                return ViewState["CurrentParameter"] as QueryParameter;
            }
            set { ViewState["CurrentParameter"] = value; }
        }

        private static ICodeValueService _codeValueService;
        private static ICodeValueService CodeValueService
        {
            get
            {
                return _codeValueService ?? (_codeValueService = CreateService<ICodeValueService>());
            }
        }
        private static IAreaService _areaService;
        private static IAreaService AreaService
        {
            get { return _areaService ?? (_areaService = CreateService<IAreaService>()); }
        }

        private static IMerchantService _service;
        private static IMerchantService Service
        {
            get
            {
                return _service ?? (_service = CreateService<IMerchantService>());
            }
        }

        private static IMerchantInfoHistoryDetailService _historyDetailService;

        private static IMerchantInfoHistoryDetailService HistoryDetailService
        {
            get
            {
                return _historyDetailService ??
                       (_historyDetailService = CreateService<IMerchantInfoHistoryDetailService>());
            }
        }
        public static List<CodeValueInfo> CooperateModeList = null;
        public static List<CodeValueInfo> InvoiceByList = null;
        public static List<CodeValueInfo> TransitCostByList;

        public override bool IsNeedMasterPage
        {
            get { return false; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCtrol();
                    //绑定传入参数
                    CurrentParameter = QueryParameter.GetQueryParameter(QueryStringManager);

                    switch (CurrentParameter.OpType)
                    {
                        case "view":
                            LoadViewMerchantInfo();
                            break;
                        case "audit":
                            if (!CurrentParameter.ValidAudit(CurrentUser))
                            {
                                LoadViewMerchantInfo();
                                ShowMessageBox("商家状态已改变.");
                            }
                            else
                            {
                                LoadAutitMerchantInfo();
                            }
                            break;
                        case "historydetail":
                            LoadHistoryMerchantInfo();
                            break;
                        default:
                            ShowMessageBox("传入参数有误");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');window.close();</script>");
            }
        }


        protected void btnPass_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Converter.ToInt32(CurrentParameter.ID, 0);
                if (id == 0)
                {
                    ShowMessageBox("传入参数有误");
                    return;
                }
                UVMerchantInfo merchantInfo = Service.GetUVMerchantInfo(id);
                if (merchantInfo == null || merchantInfo.Status != 4)
                {
                    ShowMessageBox("该条记录已经审核");
                    LoadViewMerchantInfo();
                    SetbtnVisible(false);
                    return;
                }
                int merchantID = Converter.ToInt32(CurrentParameter.MerchantID, 0);
                MerchantInfo merchant = Service.Get(merchantID);
                int pageID = Converter.ToInt32(CurrentParameter.PageID, 0);
                string crlName = CurrentParameter.CtlName;
                string auditDatestr = string.Empty;
                //调用审批方法。不是最终审核人状态还是为4.最后审批人 1
                var res = CurrentUser.GetUserAuditWorkFlowResource(crlName, merchantInfo.AuditUser, pageID, CurrentUser.GetPageControlResouceList(pageID));
                if (res.HasAuth)
                {
                    switch (merchantInfo.FromHistory)
                    {
                        case 0:
                            auditDatestr = MerchantInfoAuditStatus(!res.IsFinalStep ? 4 : 1);
                            break;
                        case 1:
                            int status = merchant.Status.ParseInt() == 0 ? 0 : 1;
                            auditDatestr = MerchantInfoAuditStatus(!res.IsFinalStep ? 4 : status);
                            break;
                    }
                    ShowMessageBox("审核成功");
                    txtReason.Text = txtReason.Text.Trim();
                    txtAuditUser.Text = CurrentUser.UserName;
                    txtAuditDate.Text = auditDatestr.ParseDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    ShowMessageBox("该用户无权限操作");
                    LoadViewMerchantInfo();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ErrorInfoBase.GetQuickError(ex.Message));
            }

            SetbtnVisible(false);
            upMerchantInfo.Update();
        }

        protected void btnNotPass_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Converter.ToInt32(CurrentParameter.ID, 0);
                if (id == 0)
                {
                    ShowMessageBox("传入参数有误");
                    return;
                }
                var merchantInfo = Service.GetUVMerchantInfo(id);
                if (merchantInfo == null || merchantInfo.Status != 4)
                {
                    ShowMessageBox("该条记录已经审核");
                    LoadViewMerchantInfo();
                    SetbtnVisible(false);
                    return;
                }
                int pageID = Converter.ToInt32(CurrentParameter.PageID, 0);
                string crlName = CurrentParameter.CtlName;
                //调用审批方法。不是最终审核人状态还是为4.最后审批人 1
                var res = CurrentUser.GetUserAuditWorkFlowResource(crlName, merchantInfo.AuditUser, pageID,
                    CurrentUser.GetPageControlResouceList(pageID));
                //调用审批方法。
                if (res.HasAuth)
                {
                    string auditDatestr = MerchantInfoAuditStatus(3);
                    ShowMessageBox("审核成功");
                    txtReason.Text = txtReason.Text.Trim();
                    txtAuditUser.Text = CurrentUser.UserName;
                    txtAuditDate.Text = auditDatestr.ParseDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    ShowMessageBox("该用户无权限操作");
                    LoadViewMerchantInfo();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

            SetbtnVisible(false);
            upMerchantInfo.Update();
        }



        //加载审核商家信息
        private void LoadAutitMerchantInfo()
        {
            #region fff
            //int id = Converter.ToInt32(CurrentParameter.ID, 0);
            //int merchantID = Converter.ToInt32(CurrentParameter.MerchantID, 0);
            //int pageID = Converter.ToInt32(CurrentParameter.PageID, 0);
            //string crlName = CurrentParameter.CtlName;
            //调用审批方法。不是最终审核人状态还是为4.最后审批人 1

            //UVMerchantInfo uvMerchantInfo = new UVMerchantInfo();
            //if (id == 0 || merchantID == 0)
            //{
            //    ShowMessageBox("传入参数有误");
            //    return;
            //}
            //uvMerchantInfo = Service.GetUVMerchantInfo(id, merchantID);
            //if (uvMerchantInfo == null)
            //{

            //    ShowMessageBox("当前商家状态已更改");
            //    SetbtnVisible(false);
            //    uvMerchantInfo = Service.GetUVMerchantInfoByMerchantID(merchantID);
            //    BindImage(uvMerchantInfo.TaxRegistrationCertificateURL, uvMerchantInfo.BusinessLicenseURL,
            //uvMerchantInfo.Logo);
            //    BindDdlCtrol(ddlCity, GetAreaList(Converter.ToInt32(uvMerchantInfo.BankProvinceID, 0)));
            //    FillContentValueWithEntity<UVMerchantInfo>(uvMerchantInfo, panelMerchantItem);
            //    return;
            //}
            //审核过的不可见
            //if (uvMerchantInfo.Status != 4)
            //{
            //    SetbtnVisible(false);
            //}
            //var res = CurrentUser.GetUserAuditWorkFlowResource(crlName, uvMerchantInfo.AuditUser, pageID, CurrentUser.GetPageControlResouceList(pageID));
            //if (res.HasAuth)
            //{
            //    SetbtnVisible(true);
            //}
            #endregion
            SetbtnVisible(true);
            BindImage(
                 CurrentParameter.CurrentUVMerchantInfo.TaxRegistrationCertificateURL,
                 CurrentParameter.CurrentUVMerchantInfo.BusinessLicenseURL,
                 CurrentParameter.CurrentUVMerchantInfo.Logo);
            BindDdlCtrol(ddlCity, GetAreaList(Converter.ToInt32(CurrentParameter.CurrentUVMerchantInfo.BankProvinceID, 0)));
            FillContentValueWithEntity(CurrentParameter.CurrentUVMerchantInfo, panelMerchantItem);
        }

        //加载查询商家信息
        private void LoadViewMerchantInfo()
        {
            SetbtnVisible(false);
            int merchantID = Converter.ToInt32(CurrentParameter.MerchantID, 0);
            if (merchantID == 0)
            {
                ShowMessageBox("传入参数有误");
                return;
            }
            var merchant = Service.Get(merchantID);
            if (merchant == null) throw new ArgumentNullException("merchant");
            BindImage(merchant.TaxRegistrationCertificateURL, merchant.BusinessLicenseURL, merchant.Logo);
            BindDdlCtrol(ddlCity, GetAreaList(Converter.ToInt32(merchant.BankProvinceID, 0)));
            FillContentValueWithEntity(merchant, panelMerchantItem);
        }



        //加载历史商家信息
        private void LoadHistoryMerchantInfo()
        {
            int id = Converter.ToInt32(CurrentParameter.ID, 0);
            SetbtnVisible(false);
            if (id == 0)
            {
                ShowMessageBox("传入参数有误");
                return;
            }
            var merchant = HistoryDetailService.Get(id);
            BindImage(merchant.TaxRegistrationCertificateURL, merchant.BusinessLicenseURL,
                     merchant.Logo);
            BindDdlCtrol(ddlCity, GetAreaList(Converter.ToInt32(merchant.BankProvinceID, 0)));
            FillContentValueWithEntity(merchant, panelMerchantItem);
        }

        //审批方法
        private string MerchantInfoAuditStatus(int status)
        {
            int id = Converter.ToInt32(CurrentParameter.ID, 0);
            IUVMerchantInfoService uvMerchantInfoService = CreateService<IUVMerchantInfoService>();
            return uvMerchantInfoService.MerchantInfoChangeStatus(id, CurrentUser.UserName, txtReason.Text.Trim(), status);
        }

        public void SetbtnVisible(bool flag)
        {
            btnPass.Visible = btnNotPass.Visible = txtReason.Enabled = flag;
            traudit.Visible = !flag;
            upMerchantInfo.Update();
        }

        //加载证件图片方法
        private void BindImage(string taxRegistrationCertificateUrl, string businessLicenseUrl, string logo)
        {
            merchantImageTax.ImageUrl = BuildMerchantImageUrl(taxRegistrationCertificateUrl);
            linkMerchantImageTax.HRef = BuildMerchantImageUrl(taxRegistrationCertificateUrl);
            merchantImageBusiness.ImageUrl = BuildMerchantImageUrl(businessLicenseUrl);
            linkMerchantImageBusiness.HRef = BuildMerchantImageUrl(businessLicenseUrl);
            merchantImage.ImageUrl = BuildMerchantImageUrl(logo);
            linkMerchantImage.HRef = BuildMerchantImageUrl(logo);
        }


        //加载合作模式，开票方，运费承担方，省
        private void BindCtrol()
        {
            CooperateModeList = CodeValueService.GetCodeListByGroupCode("MerchantInfo_CooperationMode");
            InvoiceByList = CodeValueService.GetCodeListByGroupCode("InvoiceBy");
            TransitCostByList = CodeValueService.GetCodeListByGroupCode("CostBy");
            BindDdlCtrol(ddlProvince, GetAreaList(0));
            BindDdlCtrol(ddlCooperationModeList, CooperateModeList);
            BindDdlCtrol(ddlInvoiceBy, InvoiceByList);
            BindDdlCtrol(ddlTransitCostBy, TransitCostByList);
        }

        //绑定DDL公共方法
        private static void BindDdlCtrol(ListControl dropDownList, object list)
        {
            dropDownList.Items.Clear();
            dropDownList.DataSource = list;
            dropDownList.DataBind();
            ListItem item = new ListItem { Value = "", Text = "全部" };
            dropDownList.Items.Insert(0, item);
        }

        //绑定省市区方法
        private static List<AreaInfo> GetAreaList(int parentID)
        {
            return AreaService.GetAreaByParentID(parentID);
        }


        [Serializable]
        public class QueryParameter
        {
            public string ID { get; set; }
            public string MerchantID { get; set; }
            public string FromHistory { get; set; }
            public string OpType { get; set; }
            public string HasAuth { get; set; }
            public string PageID { get; set; }
            public string CtlName { get; set; }

            public UserAuditWorkFlowResource CurrentUserAuditWorkFlowResource { get; private set; }
            public UVMerchantInfo CurrentUVMerchantInfo { get; private set; }

            private QueryParameter()
            {
            }

            public static QueryParameter GetQueryParameter(QueryStringManager queryStringManager)
            {
                return new QueryParameter
                {
                    ID = queryStringManager.GetValue("ID"),
                    MerchantID = queryStringManager.GetValue("MerchantID"),
                    FromHistory = queryStringManager.GetValue("FromHistory"),
                    OpType = queryStringManager.GetValue("OPType"),
                    HasAuth = queryStringManager.GetValue("HasAuth"),
                    PageID = queryStringManager.GetValue("PageID"),
                    CtlName = queryStringManager.GetValue("CtlName")
                };
            }

            public bool IsValid(bool isAudit)
            {
                return !isAudit || (!string.IsNullOrEmpty(PageID) && !string.IsNullOrEmpty(CtlName));
            }

            public bool ValidAudit(IUser currentUser)
            {
                var temp = !string.IsNullOrEmpty(PageID) && !string.IsNullOrEmpty(CtlName);
                if (!temp)
                {
                    return false;
                }

                CurrentUVMerchantInfo = Service.GetUVMerchantInfo(ID.ParseInt(), MerchantID.ParseInt());

                if (CurrentUVMerchantInfo == null)
                {
                    return false;
                }

                CurrentUserAuditWorkFlowResource = currentUser.GetUserAuditWorkFlowResource(CtlName, CurrentUVMerchantInfo.AuditUser, PageID.ParseInt(), currentUser.GetPageControlResouceList(PageID.ParseInt()));

                return CurrentUserAuditWorkFlowResource.HasAuth;
            }

        }

    }
}