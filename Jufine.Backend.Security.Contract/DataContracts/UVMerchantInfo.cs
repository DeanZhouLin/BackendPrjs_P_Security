using System;
using Com.BaseLibrary.Utility;

namespace Jufine.Backend.Security.DataContracts
{
    public partial class UVMerchantInfo
    {
        public bool HasAuth { get; set; }
        public bool IsFinalStep { get; set; }
        public int CurrentStep { get; set; }
        public int WaitAuditStep { get; set; }
        public int TotalAuditStepCount { get; set; }
        public string TotalAuditWorkFlowStepsStr { get; set; }

        public string CurrentMerchantID
        {
            get { return ID + "_" + MerchantID; }
        }

        public string CooperationModeDisplay { get; set; }
        public string MI_CooperationModeDisplay { get; set; }

        public string MerchantShortName
        {
            get
            {
                return StringUtil.GetShortString(MerchantName, 10);
            }
        }

        public string Strstatus { get { return GetStatus(); } }
        public string GetStatus()
        {
            string text = "";
            switch (Status)
            {
                case 0:
                    text = "禁用";
                    break;
                case 1:
                    text = "激活";
                    break;
                case 3:
                    text = "审核不通过";
                    break;
                case 4:
                    text = "待审核";
                    break;
                case 2:
                    text = "草稿";
                    break;
            }
            return text;
        }

        public string MI_Strstatus
        {
            get { return GetMI_Status(); }
        }
        public string GetMI_Status()
        {
            string text = "";
            switch (MI_Status)
            {
                case 0:
                    text = "禁用";
                    break;
                case 1:
                    text = "激活";
                    break;
                case 3:
                    text = "审核不通过";
                    break;
                case 4:
                    text = "待审核";
                    break;
                case 2:
                    text = "草稿";
                    break;
            }
            return text;
        }

        public string MerchantStrstatus
        {
            get { return IsSelfEdit ? GetStatus() : GetMI_Status(); }
        }

        public string InvoiceBys { get { return GetInvoiceBy(); } }
        public string GetInvoiceBy()
        {
            string text = "";
            switch (InvoiceBy)
            {
                case "1":
                    text = "商家开票";
                    break;
                case "2":
                    text = "聚好商城开票";
                    break;
                case "3":
                    text = "不提供发票";
                    break;
            }
            return text;
        }

        public string MI_InvoiceBys { get { return GetInvoiceBy(); } }
        public string MI_GetInvoiceBy()
        {
            string text = "";
            switch (MI_InvoiceBy)
            {
                case "1":
                    text = "商家开票";
                    break;
                case "2":
                    text = "聚好商城开票";
                    break;
                case "3":
                    text = "不提供发票";
                    break;
            }
            return text;
        }

        public string CooperationModes { get { return GetCooperationMode(); } }
        public string GetCooperationMode()
        {
            string text = "";
            switch (CooperationMode)
            {
                case "1":
                    text = "平台服务";
                    break;
                case "2":
                    text = "代运营";
                    break;
                case "3":
                    text = "经销";
                    break;
                case "4":
                    text = "代销";
                    break;
            }
            return text;
        }

        public string MI_CooperationModes { get { return GetCooperationMode(); } }
        public string MI_GetCooperationMode()
        {
            string text = "";
            switch (MI_CooperationMode)
            {
                case "1":
                    text = "平台服务";
                    break;
                case "2":
                    text = "代运营";
                    break;
                case "3":
                    text = "经销";
                    break;
                case "4":
                    text = "代销";
                    break;
            }
            return text;
        }

        public string ContractStartStr { get { return ContractStart.ToString(); } }
        public string MI_ContractStartStr { get { return MI_ContractStart.ToString(); } }

        public string ContractEndStr { get { return ContractEnd.ToString(); } }
        public string MI_ContractEndStr { get { return MI_ContractEnd.ToString(); } }
        public string IDAndName { get { return ID + " - " + MerchantName; } }

        public bool IsSelfEdit { get; set; }

        public int? S_MerchantID { get; set; }
        public string S_MerchantName { get; set; }
        public string S_CooperationMode { get; set; }
        public string S_CooperationModeDisplay { get; set; }
        public string S_ServicePhone { get; set; }
        public string S_OrganizationCode { get; set; }
        public string S_Telephone { get; set; }
        public string S_ContactPerson1 { get; set; }
        public string S_EditUser { get; set; }
        public DateTime? S_EditDate { get; set; }
        public string S_StrEditDate { get; set; }

        public string S_Strstatus { get; set; }
        public int? S_Status { get; set; }
        public string S_StatusToolTipText { get; set; }

        public DateTime? S_CreateDate { get; set; }
        public DateTime? S_CreateDateTo { get; set; }

        public bool S_AuditVisual { get; set; }
        public string S_AuditToolTip { get; set; }
        public string S_AuditURL { get; set; }

        public bool S_HistoryVisual { get; set; }
        public string S_HistoryURL { get; set; }

        public bool S_ShowUser { get; set; }

        public string S_ViewURL { get; set; }

        public bool S_ViewVisual { get; set; }

        public static string GetMerchantInfoParts(MerchantInfoParts merchantInfoParts)
        {

            switch (merchantInfoParts)
            {
                case MerchantInfoParts.Contactperson:
                    return "Contactperson";
                case MerchantInfoParts.Cooperation:
                    return "Cooperation";
                case MerchantInfoParts.License:
                    return "License";
                case MerchantInfoParts.MerchantPreview:
                    return "MerchantPreview";
            }

            return string.Empty;

        }
        public static MerchantInfoParts? GetMerchantInfoParts(object obj)
        {

            if (obj == null)
            {
                return null;
            }

            switch (obj.ToString().Trim().ToLower())
            {
                case "contactperson":
                    return MerchantInfoParts.Contactperson;
                case "cooperation":
                    return MerchantInfoParts.Cooperation;
                case "license":
                    return MerchantInfoParts.License;
                case "merchantpreview":
                    return MerchantInfoParts.MerchantPreview;
                default:
                    return MerchantInfoParts.All;
            }

        }

    }
}
