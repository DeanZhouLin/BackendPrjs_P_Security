using System;
using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{ 
    [Serializable]
    public partial class MerchantInfo : DataContractBase
    {
        public string MerchantName { get; set; }
        public string MerchantDescription { get; set; }
        public Int32? Status { get; set; }
        public string Logo { get; set; }
        public string ServicePhone { get; set; }
        public string LegalRepresentative { get; set; }
        public string TaxNO { get; set; }
        public string TaxRegistrationCertificateURL { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string OrganizationCode { get; set; }
        public string BusinessLicenseURL { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Fax2 { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string ContactPerson1 { get; set; }
        public string Post1 { get; set; }
        public string Dept1 { get; set; }
        public string Mobile1 { get; set; }
        public string Telephone1 { get; set; }
        public string Email1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string Post2 { get; set; }
        public string Dept2 { get; set; }
        public string Mobile2 { get; set; }
        public string Telephone2 { get; set; }
        public string Email2 { get; set; }
        public string OpeningBank { get; set; }
        public string BankCardNO { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public string EditUser { get; set; }
        public DateTime? EditDate { get; set; }
        public DateTime? EditDateTo { get; set; }
        public string CooperationMode { get; set; }
        public string DisplayName { get; set; }
        public decimal? GuarantyFunds { get; set; }
        public DateTime? ContractStart { get; set; }
        public DateTime? ContractStartTo { get; set; }
        public DateTime? ContractEnd { get; set; }
        public DateTime? ContractEndTo { get; set; }
        public decimal? UsageCharges { get; set; }
        public Int32? PaymentCycle { get; set; }
        public string ContactPerson3 { get; set; }
        public string Post3 { get; set; }
        public string Dept3 { get; set; }
        public string Mobile3 { get; set; }
        public string Telephone3 { get; set; }
        public string Email3 { get; set; }
        public decimal? CommissionRatio { get; set; }
        public decimal? ReturnMerchantRatio { get; set; }
        public decimal? ReturnMallRatio { get; set; }
        public string InvoiceBy { get; set; }
        public string ProductManager { get; set; }
        public string TransitCostBy { get; set; }
        public decimal? FreeTransitAmount { get; set; }
        public Int32? BankProvinceID { get; set; }
        public string BankProvince { get; set; }
        public Int32? BankCityID { get; set; }
        public string BankCity { get; set; }
        public string AuditUser { get; set; }
        public DateTime? AuditDate { get; set; }
        public DateTime? AuditDateTo { get; set; }
        public string Reason { get; set; }
    }
}