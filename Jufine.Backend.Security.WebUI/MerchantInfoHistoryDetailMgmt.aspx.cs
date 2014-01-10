using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Utility;
using Jufine.Backend.BaseData.DataContracts;
using Jufine.Backend.BaseData.ServiceContracts;
using Jufine.Backend.WebModel;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;

namespace Jufine.Backend.Security.WebUI
{
    public partial class MerchantInfoHistoryDetailMgmt : PageBase
    {
        static readonly ICodeValueService CodeValueService = CreateService<ICodeValueService>();
        private static List<CodeValueInfo> _cooperateModeList;
        public static List<CodeValueInfo> CooperateModeList
        {
            get {
                return _cooperateModeList ??
                       (_cooperateModeList = CodeValueService.GetCodeListByGroupCode("MerchantInfo_CooperationMode"));
            }
        }

        private QueryConditionInfo<MerchantInfoHistoryDetail> QueryCondition
        {
            get
            {
                QueryConditionInfo<MerchantInfoHistoryDetail> queryCondition
                    = ViewState["MERCHANT_QUERYCONDITION"] as QueryConditionInfo<MerchantInfoHistoryDetail>;
                if (queryCondition == null)
                {
                    queryCondition = new QueryConditionInfo<MerchantInfoHistoryDetail>();
                    ViewState["MERCHANT_QUERYCONDITION"] = queryCondition;
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
                    var merchentList = CreateService<IMerchantService>().GetAllMerchant();
                    BindDDLControl(sddlMerchantID, merchentList, true);
                    QueryString();
                    InitData();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void QueryString()
        {
            var merchentList = CreateService<IMerchantService>().GetAllMerchant();
            var ss = merchentList.Select(c => c.MerchantID).ToList();
            int merchantID = Converter.ToInt32(QueryStringManager.GetValue("MerchantID"), 0);
            if (merchantID != 0 && ss.Contains(merchantID))
            {
                sddlMerchantID.SelectedValue = QueryStringManager.GetValue("MerchantID");
            }
            else
            {
                sddlMerchantID.SelectedValue = "0";
            }
        }
        private void InitData()
        {
            BindDDLControl(sddlCooperationModeList, CooperateModeList, true, "", "全部");
            QueryCondition.Condtion.Status = -1;
            if (sddlMerchantID.SelectedValue != "0")
            {
                QueryCondition.Condtion.MerchantID = Converter.ToInt32(sddlMerchantID.SelectedValue, 0);
            }
            QueryData();
        }
        private static void BindDDLControl<T>(ListControl ddl, List<T> list, bool flag = false, string defualtValue = "0", string defaultText = "请选择")
        {
            ddl.Items.Clear();
            ddl.DataSource = list;
            ddl.DataBind();
            if (flag)
            {
                ListItem item = new ListItem { Value = defualtValue, Text = defaultText };
                ddl.Items.Insert(0, item);
            }
        }
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
                FillEntityWithContentValue(QueryCondition.Condtion, plHeader);
                QueryCondition.Condtion.Status = Converter.ToInt32(ddlQueryList.SelectedValue, -1);
                QueryCondition.Condtion.MerchantID = Converter.ToInt32(sddlMerchantID.SelectedValue, 0);
                QueryCondition.Condtion.CooperationMode = sddlCooperationModeList.SelectedValue;
                FillEntityWithContentValue<MerchantInfoHistoryDetail>(QueryCondition.Condtion, plHeader);
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }
        protected void gvMerchantList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                e.Cancel = true;
                SetSortOrder<MerchantInfoHistoryDetail>(QueryCondition, e.SortExpression);
                listPager.CurrentPageIndex = 0;
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

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

        protected string BuildUrl(object id, object opType)
        {
            return "MerchantSearchAuditMgmt.aspx?ID=" + id + "&OPType=" + opType;
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
        private void QueryData()
        {
            IMerchantInfoHistoryDetailService service = CreateService<IMerchantInfoHistoryDetailService>();
            QueryCondition.PageIndex = listPager.CurrentPageIndex;
            QueryCondition.PageSize = listPager.PageSize;
            QueryResultInfo<MerchantInfoHistoryDetail> result = service.Query(QueryCondition);

            SetOrderHeaderStyle(gvMerchantList, QueryCondition);
            gvMerchantList.DataSource = result.RecordList;
            gvMerchantList.DataBind();
            NoRecords<MerchantInfoHistoryDetail>(gvMerchantList);
            listPager.RecordCount = result.RecordCount;
            upList.Update();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
    }
}
