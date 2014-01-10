using System;
using System.Linq;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess
{
    internal class MerchantDA : DataBase<MerchantInfo, SecurityEntities>
    {
        internal static MerchantDA DAO = new MerchantDA();
        private MerchantDA() { }
        protected override void AttachValue(MerchantInfo newEntity, MerchantInfo oldEntity)
        {
            oldEntity.MerchantName = newEntity.MerchantName;
            oldEntity.MerchantDescription = newEntity.MerchantDescription;
            //oldEntity.Status = newEntity.Status;
            oldEntity.Logo = newEntity.Logo;
            oldEntity.ServicePhone = newEntity.ServicePhone;
            oldEntity.LegalRepresentative = newEntity.LegalRepresentative;
            oldEntity.TaxNO = newEntity.TaxNO;
            oldEntity.TaxRegistrationCertificateURL = newEntity.TaxRegistrationCertificateURL;
            oldEntity.Address = newEntity.Address;
            oldEntity.PostalCode = newEntity.PostalCode;
            oldEntity.OrganizationCode = newEntity.OrganizationCode;
            oldEntity.BusinessLicenseURL = newEntity.BusinessLicenseURL;
            oldEntity.Telephone = newEntity.Telephone;
            oldEntity.Fax = newEntity.Fax;
            oldEntity.Fax2 = newEntity.Fax2;
            oldEntity.Website = newEntity.Website;
            oldEntity.Email = newEntity.Email;
            oldEntity.ContactPerson1 = newEntity.ContactPerson1;
            oldEntity.Post1 = newEntity.Post1;
            oldEntity.Dept1 = newEntity.Dept1;
            oldEntity.Mobile1 = newEntity.Mobile1;
            oldEntity.Telephone1 = newEntity.Telephone1;
            oldEntity.Email1 = newEntity.Email1;
            oldEntity.ContactPerson2 = newEntity.ContactPerson2;
            oldEntity.Post2 = newEntity.Post2;
            oldEntity.Dept2 = newEntity.Dept2;
            oldEntity.Mobile2 = newEntity.Mobile2;
            oldEntity.Telephone2 = newEntity.Telephone2;
            oldEntity.Email2 = newEntity.Email2;
            oldEntity.ContactPerson3 = newEntity.ContactPerson3;
            oldEntity.Post3 = newEntity.Post3;
            oldEntity.Dept3 = newEntity.Dept3;
            oldEntity.Mobile3 = newEntity.Mobile3;
            oldEntity.Telephone3 = newEntity.Telephone3;
            oldEntity.Email3 = newEntity.Email3;
            oldEntity.CommissionRatio = newEntity.CommissionRatio;
            oldEntity.ReturnMerchantRatio = newEntity.ReturnMerchantRatio;
            oldEntity.ReturnMallRatio = newEntity.ReturnMallRatio;
            oldEntity.InvoiceBy = newEntity.InvoiceBy;
            oldEntity.OpeningBank = newEntity.OpeningBank;
            oldEntity.BankCardNO = newEntity.BankCardNO;
            oldEntity.CooperationMode = newEntity.CooperationMode;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
            oldEntity.DisplayName = newEntity.DisplayName;
            oldEntity.GuarantyFunds = newEntity.GuarantyFunds;
            oldEntity.ContractStart = newEntity.ContractStart;
            oldEntity.ContractEnd = newEntity.ContractEnd;
            oldEntity.UsageCharges = newEntity.UsageCharges;
            oldEntity.PaymentCycle = newEntity.PaymentCycle;
            oldEntity.ProductManager = newEntity.ProductManager;
            oldEntity.TransitCostBy = newEntity.TransitCostBy;
            oldEntity.FreeTransitAmount = newEntity.FreeTransitAmount;
            oldEntity.BankProvinceID = newEntity.BankProvinceID;
            oldEntity.BankProvince = newEntity.BankProvince;
            oldEntity.BankCityID = newEntity.BankCityID;
            oldEntity.BankCity = newEntity.BankCity;


            oldEntity.AuditUser = newEntity.AuditUser;
            oldEntity.AuditDate = newEntity.AuditDate;
            oldEntity.Reason = newEntity.Reason;
        }

        public MerchantInfoHistoryDetail CopyToMerchantInfoHistoryDetail(MerchantInfo newEntity, MerchantInfoHistoryDetail oldEntity)
        {
            if (oldEntity == null)
            {
                oldEntity = new MerchantInfoHistoryDetail();
            }
            oldEntity.MerchantID = newEntity.ID;
            oldEntity.MerchantName = newEntity.MerchantName;
            oldEntity.MerchantDescription = newEntity.MerchantDescription;
            //oldEntity.Status = newEntity.Status;
            oldEntity.Logo = newEntity.Logo;
            oldEntity.ServicePhone = newEntity.ServicePhone;
            oldEntity.LegalRepresentative = newEntity.LegalRepresentative;
            oldEntity.TaxNO = newEntity.TaxNO;
            oldEntity.TaxRegistrationCertificateURL = newEntity.TaxRegistrationCertificateURL;
            oldEntity.Address = newEntity.Address;
            oldEntity.PostalCode = newEntity.PostalCode;
            oldEntity.OrganizationCode = newEntity.OrganizationCode;
            oldEntity.BusinessLicenseURL = newEntity.BusinessLicenseURL;
            oldEntity.Telephone = newEntity.Telephone;
            oldEntity.Fax = newEntity.Fax;
            oldEntity.Fax2 = newEntity.Fax2;
            oldEntity.Website = newEntity.Website;
            oldEntity.Email = newEntity.Email;
            oldEntity.ContactPerson1 = newEntity.ContactPerson1;
            oldEntity.Post1 = newEntity.Post1;
            oldEntity.Dept1 = newEntity.Dept1;
            oldEntity.Mobile1 = newEntity.Mobile1;
            oldEntity.Telephone1 = newEntity.Telephone1;
            oldEntity.Email1 = newEntity.Email1;
            oldEntity.ContactPerson2 = newEntity.ContactPerson2;
            oldEntity.Post2 = newEntity.Post2;
            oldEntity.Dept2 = newEntity.Dept2;
            oldEntity.Mobile2 = newEntity.Mobile2;
            oldEntity.Telephone2 = newEntity.Telephone2;
            oldEntity.Email2 = newEntity.Email2;
            oldEntity.ContactPerson3 = newEntity.ContactPerson3;
            oldEntity.Post3 = newEntity.Post3;
            oldEntity.Dept3 = newEntity.Dept3;
            oldEntity.Mobile3 = newEntity.Mobile3;
            oldEntity.Telephone3 = newEntity.Telephone3;
            oldEntity.Email3 = newEntity.Email3;
            oldEntity.CommissionRatio = newEntity.CommissionRatio;
            oldEntity.ReturnMerchantRatio = newEntity.ReturnMerchantRatio;
            oldEntity.ReturnMallRatio = newEntity.ReturnMallRatio;
            oldEntity.InvoiceBy = newEntity.InvoiceBy;
            oldEntity.OpeningBank = newEntity.OpeningBank;
            oldEntity.BankCardNO = newEntity.BankCardNO;
            oldEntity.CooperationMode = newEntity.CooperationMode;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
            oldEntity.DisplayName = newEntity.DisplayName;
            oldEntity.GuarantyFunds = newEntity.GuarantyFunds;
            oldEntity.ContractStart = newEntity.ContractStart;
            oldEntity.ContractEnd = newEntity.ContractEnd;
            oldEntity.UsageCharges = newEntity.UsageCharges;
            oldEntity.PaymentCycle = newEntity.PaymentCycle;
            oldEntity.ProductManager = newEntity.ProductManager;
            oldEntity.TransitCostBy = newEntity.TransitCostBy;
            oldEntity.FreeTransitAmount = newEntity.FreeTransitAmount;
            oldEntity.BankProvinceID = newEntity.BankProvinceID;
            oldEntity.BankProvince = newEntity.BankProvince;
            oldEntity.BankCityID = newEntity.BankCityID;
            oldEntity.BankCity = newEntity.BankCity;

            oldEntity.AuditUser = newEntity.AuditUser;
            oldEntity.AuditDate = newEntity.AuditDate;

            return oldEntity;
        }

        protected override IQueryable<MerchantInfo> SetWhereClause(QueryConditionInfo<MerchantInfo> queryCondition, IQueryable<MerchantInfo> query)
        {

            if (!string.IsNullOrEmpty(queryCondition.Condtion.AuditUser))
            {
                query = query.Where(c => c.AuditUser.StartsWith(queryCondition.Condtion.AuditUser));
            }
            if (queryCondition.Condtion.AuditDate != null)
            {
                query = query.Where(c => c.AuditDate > queryCondition.Condtion.AuditDate);
            }
            if (queryCondition.Condtion.AuditDateTo != null)
            {
                query = query.Where(c => c.AuditDate <= queryCondition.Condtion.AuditDateTo);
            }

            if (queryCondition.Condtion.ID > 0)
            {
                query = query.Where(c => c.ID == queryCondition.Condtion.ID);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.MerchantName))
            {
                query = query.Where(c => c.MerchantName.StartsWith(queryCondition.Condtion.MerchantName));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.DisplayName))
            {
                query = query.Where(c => c.DisplayName.Contains(queryCondition.Condtion.DisplayName));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.MerchantDescription))
            {
                query = query.Where(c => c.MerchantDescription.StartsWith(queryCondition.Condtion.MerchantDescription));
            }
            if (queryCondition.Condtion.Status >= 0)
            {
                query = query.Where(c => c.Status == queryCondition.Condtion.Status && c.Status != -1);
            }
            else
            {
                query = query.Where(c => c.Status != -1);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Logo))
            {
                query = query.Where(c => c.Logo.StartsWith(queryCondition.Condtion.Logo));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.ServicePhone))
            {
                query = query.Where(c => c.ServicePhone.StartsWith(queryCondition.Condtion.ServicePhone));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.LegalRepresentative))
            {
                query = query.Where(c => c.LegalRepresentative.StartsWith(queryCondition.Condtion.LegalRepresentative));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.TaxNO))
            {
                query = query.Where(c => c.TaxNO.StartsWith(queryCondition.Condtion.TaxNO));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.TaxRegistrationCertificateURL))
            {
                query = query.Where(c => c.TaxRegistrationCertificateURL.StartsWith(queryCondition.Condtion.TaxRegistrationCertificateURL));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Address))
            {
                query = query.Where(c => c.Address.StartsWith(queryCondition.Condtion.Address));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.PostalCode))
            {
                query = query.Where(c => c.PostalCode.StartsWith(queryCondition.Condtion.PostalCode));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.OrganizationCode))
            {
                query = query.Where(c => c.OrganizationCode.StartsWith(queryCondition.Condtion.OrganizationCode));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.BusinessLicenseURL))
            {
                query = query.Where(c => c.BusinessLicenseURL.StartsWith(queryCondition.Condtion.BusinessLicenseURL));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Telephone))
            {
                query = query.Where(c => c.Telephone.StartsWith(queryCondition.Condtion.Telephone) || c.Telephone1.StartsWith(queryCondition.Condtion.Telephone) || c.Telephone2.StartsWith(queryCondition.Condtion.Telephone));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Fax))
            {
                query = query.Where(c => c.Fax.StartsWith(queryCondition.Condtion.Fax));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Fax2))
            {
                query = query.Where(c => c.Fax2.StartsWith(queryCondition.Condtion.Fax2));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Website))
            {
                query = query.Where(c => c.Website.StartsWith(queryCondition.Condtion.Website));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Email))
            {
                query = query.Where(c => c.Email.StartsWith(queryCondition.Condtion.Email));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.ContactPerson1))
            {
                query = query.Where(c => c.ContactPerson1.StartsWith(queryCondition.Condtion.ContactPerson1) || c.ContactPerson2.StartsWith(queryCondition.Condtion.ContactPerson1));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Post1))
            {
                query = query.Where(c => c.Post1.StartsWith(queryCondition.Condtion.Post1));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Dept1))
            {
                query = query.Where(c => c.Dept1.StartsWith(queryCondition.Condtion.Dept1));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Mobile1))
            {
                query = query.Where(c => c.Mobile1.StartsWith(queryCondition.Condtion.Mobile1));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Telephone1))
            {
                query = query.Where(c => c.Telephone1.StartsWith(queryCondition.Condtion.Telephone1));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Email1))
            {
                query = query.Where(c => c.Email1.StartsWith(queryCondition.Condtion.Email1));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.ContactPerson2))
            {
                query = query.Where(c => c.ContactPerson2.StartsWith(queryCondition.Condtion.ContactPerson2));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Post2))
            {
                query = query.Where(c => c.Post2.StartsWith(queryCondition.Condtion.Post2));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Dept2))
            {
                query = query.Where(c => c.Dept2.StartsWith(queryCondition.Condtion.Dept2));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Mobile2))
            {
                query = query.Where(c => c.Mobile2.StartsWith(queryCondition.Condtion.Mobile2));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Telephone2))
            {
                query = query.Where(c => c.Telephone2.StartsWith(queryCondition.Condtion.Telephone2));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Email2))
            {
                query = query.Where(c => c.Email2.StartsWith(queryCondition.Condtion.Email2));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.OpeningBank))
            {
                query = query.Where(c => c.OpeningBank.StartsWith(queryCondition.Condtion.OpeningBank));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.BankCardNO))
            {
                query = query.Where(c => c.BankCardNO.StartsWith(queryCondition.Condtion.BankCardNO));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.CooperationMode))
            {
                query = query.Where(c => c.CooperationMode == queryCondition.Condtion.CooperationMode);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.CreateUser))
            {
                query = query.Where(c => c.CreateUser.StartsWith(queryCondition.Condtion.CreateUser));
            }
            if (queryCondition.Condtion.CreateDate != null)
            {
                query = query.Where(c => c.CreateDate > queryCondition.Condtion.CreateDate);
            }
            if (queryCondition.Condtion.CreateDateTo != null)
            {
                query = query.Where(c => c.CreateDate <= queryCondition.Condtion.CreateDateTo);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.EditUser))
            {
                query = query.Where(c => c.EditUser.StartsWith(queryCondition.Condtion.EditUser));
            }
            if (queryCondition.Condtion.EditDate != null)
            {
                query = query.Where(c => c.EditDate > queryCondition.Condtion.EditDate);
            }
            if (queryCondition.Condtion.EditDateTo != null)
            {
                query = query.Where(c => c.EditDate <= queryCondition.Condtion.EditDateTo);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.ProductManager))
            {
                query = query.Where(c => c.ProductManager.StartsWith(queryCondition.Condtion.ProductManager));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.TransitCostBy))
            {
                query = query.Where(c => c.TransitCostBy.StartsWith(queryCondition.Condtion.TransitCostBy));
            }
            if (queryCondition.Condtion.FreeTransitAmount > 0)
            {
                query = query.Where(c => c.FreeTransitAmount == queryCondition.Condtion.FreeTransitAmount);
            }
            if (queryCondition.Condtion.BankProvinceID > 0)
            {
                query = query.Where(c => c.BankProvinceID == queryCondition.Condtion.BankProvinceID);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.BankProvince))
            {
                query = query.Where(c => c.BankProvince.StartsWith(queryCondition.Condtion.BankProvince));
            }
            if (queryCondition.Condtion.BankCityID > 0)
            {
                query = query.Where(c => c.BankCityID == queryCondition.Condtion.BankCityID);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.BankCity))
            {
                query = query.Where(c => c.BankCity.StartsWith(queryCondition.Condtion.BankCity));
            }

            if (!queryCondition.IsAdmin)
            {
                if (queryCondition.MerchantList == null || queryCondition.MerchantList.Count == 0)
                {
                    query = query.Where(c => c.ID == -1);
                }
                else if (queryCondition.MerchantList.Count == 1)
                {
                    int merchantID = queryCondition.MerchantList[0];
                    query = query.Where(c => c.ID == merchantID);
                }
                else
                {
                    query = query.Where(c => queryCondition.MerchantList.Contains(c.ID));
                }
            }
            return query;
        }
        protected override IQueryable<MerchantInfo> SetOrder(QueryConditionInfo<MerchantInfo> queryCondition, IQueryable<MerchantInfo> query)
        {
            int count = queryCondition.OrderFileds.Count;
            if (count > 0)
            {
                for (int i = count; i > 0; i--)
                {
                    OrderFiledInfo item = queryCondition.OrderFileds[i - i];

                    if (item.FieldName == "AuditUser")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.AuditUser) : query.OrderByDescending(c => c.AuditUser);
                    }
                    if (item.FieldName == "AuditDate")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.AuditDate) : query.OrderByDescending(c => c.AuditDate);
                    }

                    if (item.FieldName == "ID")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ID) : query.OrderByDescending(c => c.ID);
                    }
                    if (item.FieldName == "MerchantName")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.MerchantName) : query.OrderByDescending(c => c.MerchantName);
                    }

                    if (item.FieldName == "DisplayName")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.DisplayName) : query.OrderByDescending(c => c.DisplayName);
                    }

                    if (item.FieldName == "MerchantDescription")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.MerchantDescription) : query.OrderByDescending(c => c.MerchantDescription);
                    }
                    if (item.FieldName == "Status")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Status) : query.OrderByDescending(c => c.Status);
                    }
                    if (item.FieldName == "Logo")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Logo) : query.OrderByDescending(c => c.Logo);
                    }
                    if (item.FieldName == "ServicePhone")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ServicePhone) : query.OrderByDescending(c => c.ServicePhone);
                    }
                    if (item.FieldName == "LegalRepresentative")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.LegalRepresentative) : query.OrderByDescending(c => c.LegalRepresentative);
                    }
                    if (item.FieldName == "TaxNO")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.TaxNO) : query.OrderByDescending(c => c.TaxNO);
                    }
                    if (item.FieldName == "TaxRegistrationCertificateURL")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.TaxRegistrationCertificateURL) : query.OrderByDescending(c => c.TaxRegistrationCertificateURL);
                    }
                    if (item.FieldName == "Address")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Address) : query.OrderByDescending(c => c.Address);
                    }
                    if (item.FieldName == "PostalCode")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.PostalCode) : query.OrderByDescending(c => c.PostalCode);
                    }
                    if (item.FieldName == "OrganizationCode")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.OrganizationCode) : query.OrderByDescending(c => c.OrganizationCode);
                    }
                    if (item.FieldName == "BusinessLicenseURL")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.BusinessLicenseURL) : query.OrderByDescending(c => c.BusinessLicenseURL);
                    }
                    if (item.FieldName == "Telephone")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Telephone) : query.OrderByDescending(c => c.Telephone);
                    }
                    if (item.FieldName == "Fax")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Fax) : query.OrderByDescending(c => c.Fax);
                    }
                    if (item.FieldName == "Fax2")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Fax2) : query.OrderByDescending(c => c.Fax2);
                    }
                    if (item.FieldName == "Website")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Website) : query.OrderByDescending(c => c.Website);
                    }
                    if (item.FieldName == "Email")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Email) : query.OrderByDescending(c => c.Email);
                    }
                    if (item.FieldName == "ContactPerson1")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ContactPerson1) : query.OrderByDescending(c => c.ContactPerson1);
                    }
                    if (item.FieldName == "Post1")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Post1) : query.OrderByDescending(c => c.Post1);
                    }
                    if (item.FieldName == "Dept1")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Dept1) : query.OrderByDescending(c => c.Dept1);
                    }
                    if (item.FieldName == "Mobile1")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Mobile1) : query.OrderByDescending(c => c.Mobile1);
                    }
                    if (item.FieldName == "Telephone1")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Telephone1) : query.OrderByDescending(c => c.Telephone1);
                    }
                    if (item.FieldName == "Email1")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Email1) : query.OrderByDescending(c => c.Email1);
                    }
                    if (item.FieldName == "ContactPerson2")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ContactPerson2) : query.OrderByDescending(c => c.ContactPerson2);
                    }
                    if (item.FieldName == "Post2")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Post2) : query.OrderByDescending(c => c.Post2);
                    }
                    if (item.FieldName == "Dept2")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Dept2) : query.OrderByDescending(c => c.Dept2);
                    }
                    if (item.FieldName == "Mobile2")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Mobile2) : query.OrderByDescending(c => c.Mobile2);
                    }
                    if (item.FieldName == "Telephone2")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Telephone2) : query.OrderByDescending(c => c.Telephone2);
                    }
                    if (item.FieldName == "Email2")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.Email2) : query.OrderByDescending(c => c.Email2);
                    }
                    if (item.FieldName == "OpeningBank")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.OpeningBank) : query.OrderByDescending(c => c.OpeningBank);
                    }
                    if (item.FieldName == "BankCardNO")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.BankCardNO) : query.OrderByDescending(c => c.BankCardNO);
                    }
                    if (item.FieldName == "CooperationMode")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.CooperationMode) : query.OrderByDescending(c => c.CooperationMode);
                    }
                    if (item.FieldName == "CreateUser")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.CreateUser) : query.OrderByDescending(c => c.CreateUser);
                    }
                    if (item.FieldName == "CreateDate")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.CreateDate) : query.OrderByDescending(c => c.CreateDate);
                    }
                    if (item.FieldName == "EditUser")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.EditUser) : query.OrderByDescending(c => c.EditUser);
                    }
                    if (item.FieldName == "EditDate")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.EditDate) : query.OrderByDescending(c => c.EditDate);
                    }

                    if (item.FieldName == "ProductManager")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ProductManager) : query.OrderByDescending(c => c.ProductManager);
                    }
                    if (item.FieldName == "TransitCostBy")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.TransitCostBy) : query.OrderByDescending(c => c.TransitCostBy);
                    }
                    if (item.FieldName == "FreeTransitAmount")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.FreeTransitAmount) : query.OrderByDescending(c => c.FreeTransitAmount);
                    }
                    if (item.FieldName == "BankProvinceID")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.BankProvinceID) : query.OrderByDescending(c => c.BankProvinceID);
                    }
                    if (item.FieldName == "BankProvince")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.BankProvince) : query.OrderByDescending(c => c.BankProvince);
                    }
                    if (item.FieldName == "BankCityID")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.BankCityID) : query.OrderByDescending(c => c.BankCityID);
                    }
                    if (item.FieldName == "BankCity")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.BankCity) : query.OrderByDescending(c => c.BankCity);
                    }
                }
            }
            else
            {
                query = query.OrderByDescending(c => c.ID);
            }
            return query;
        }

        public void ChangeStatus(MerchantInfo entity)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfo> objectSet = entities.CreateObjectSet<MerchantInfo>();
                MerchantInfo MerchantInfo = objectSet.FirstOrDefault(c => c.ID == entity.ID);
                if (MerchantInfo != null)
                {
                    MerchantInfo.EditUser = entity.EditUser;
                    MerchantInfo.EditDate = DateTime.Now;
                    MerchantInfo.Status = entity.Status;
                    entities.SaveChanges();
                }
            }
        }

        public void Delete(int merchantID, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfo> objectSet = entities.CreateObjectSet<MerchantInfo>();
                MerchantInfo MerchantInfo = objectSet.FirstOrDefault(c => c.ID == merchantID);
                if (MerchantInfo != null)
                {
                    MerchantInfo.EditUser = editUser;
                    MerchantInfo.EditDate = DateTime.Now;
                    MerchantInfo.Status = -1;
                    entities.SaveChanges();
                }
            }
        }

        internal bool IsExsit(MerchantInfo merchant)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfo> objectSet = entities.CreateObjectSet<MerchantInfo>();
                return objectSet.FirstOrDefault(c => c.Status == 1 && c.ID != merchant.ID && (c.MerchantName == merchant.MerchantName || c.OrganizationCode == merchant.OrganizationCode || c.TaxNO == merchant.TaxNO)) == null;
            }
        }

        internal void BatchChangeStatus(List<int> idList, int status, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfo> objectSet = entities.CreateObjectSet<MerchantInfo>();
                List<MerchantInfo> entityList = objectSet.Where(c => idList.Contains(c.ID)).ToList();
                foreach (var item in entityList)
                {
                    item.EditUser = editUser;
                    item.EditDate = DateTime.Now;
                    item.Status = status;
                }
                entities.SaveChanges();
            }
        }

        internal void BatchChangeStatusMerchant(List<int> idList, int status, string editUser, int fromHistory = 0)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                foreach (int id in idList)
                {
                    entities.ExecuteFunction("UP_UpdateMerchantInfoStatus",
                    BuildParameter("ID", id),
                    BuildParameter("ToStatus", status),
                    BuildParameter("EditUser", editUser),
                    BuildParameter("FromHistory", fromHistory));
                }
            }
        }

        internal void ChangeStatusMerchant(int id, int status, string editUser, int fromHistory = 0)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                entities.ExecuteFunction("UP_UpdateMerchantInfoStatus",
                BuildParameter("ID", id),
                BuildParameter("ToStatus", status),
                BuildParameter("EditUser", editUser),
                BuildParameter("FromHistory", fromHistory));
            }
        }
        internal void BatchDeleteMerchant(List<int> keyList, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                foreach (int Merchantid in keyList)
                {
                    entities.ExecuteFunction("UP_DeleteMerchantInfo",
                       BuildParameter("MerchantId", Merchantid),
                       BuildParameter("EditUser", editUser)
                       );
                }
            }
        }
        internal MerchantInfo GetByName(string merchantName)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfo> objectSet = entities.CreateObjectSet<MerchantInfo>();
                return objectSet.FirstOrDefault(c => c.MerchantName == merchantName && c.Status != -1);
            }
        }

        internal List<MerchantInfo> GetActiveMerchant()
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfo> objectSet = entities.CreateObjectSet<MerchantInfo>();
                return objectSet.Where(c => c.Status == 1).ToList();
            }
        }

        internal List<MerchantInfo> GetAllMerchant()
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfo> objectSet = entities.CreateObjectSet<MerchantInfo>();
                return objectSet.Where(c => c.Status != -1).ToList();
            }
        }



    }
}
