using Com.BaseLibrary.Utility;

namespace Jufine.Backend.Security.DataContracts
{
	
	public partial class MerchantInfoHistoryDetail
	{

        public string CooperationModeDisplay { get; set; }
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
                case 2:
                    text = "草稿";
                    break;
                case 3:
                    text = "审核不通过";
                    break;
                case 4:
                    text = "待审核";
                    break;
              
            }
            return text;
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
        public string ContractStartStr { get { return ContractStart.ToString(); } }
        public string ContractEndStr { get { return ContractEnd.ToString(); } }
        public string IDAndName { get { return ID + " - " + MerchantName; } }
	}
}