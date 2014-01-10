using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess
{
    internal class MerchantInfoHistoryDetailDA : DataBase<MerchantInfoHistoryDetail, SecurityEntities>
    {
        internal static MerchantInfoHistoryDetailDA DAO = new MerchantInfoHistoryDetailDA();
        private MerchantInfoHistoryDetailDA() { }
        protected override void AttachValue(MerchantInfoHistoryDetail newEntity, MerchantInfoHistoryDetail oldEntity)
        {
            oldEntity.MerchantID = newEntity.MerchantID;
            oldEntity.MerchantName = newEntity.MerchantName;
            oldEntity.MerchantDescription = newEntity.MerchantDescription;
            oldEntity.Status = newEntity.Status;
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
            oldEntity.OpeningBank = newEntity.OpeningBank;
            oldEntity.BankCardNO = newEntity.BankCardNO;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
            oldEntity.CooperationMode = newEntity.CooperationMode;
            oldEntity.DisplayName = newEntity.DisplayName;
            oldEntity.GuarantyFunds = newEntity.GuarantyFunds;
            oldEntity.ContractStart = newEntity.ContractStart;
            oldEntity.ContractEnd = newEntity.ContractEnd;
            oldEntity.UsageCharges = newEntity.UsageCharges;
            oldEntity.PaymentCycle = newEntity.PaymentCycle;
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
        protected override IQueryable<MerchantInfoHistoryDetail> SetWhereClause(QueryConditionInfo<MerchantInfoHistoryDetail> queryCondition, IQueryable<MerchantInfoHistoryDetail> query)
        {
            query = query.Where(c => c.Status == 0 || c.Status == 1 || c.Status == 3);
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
            if (queryCondition.Condtion.MerchantID > 0)
            {
                query = query.Where(c => c.MerchantID == queryCondition.Condtion.MerchantID);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.MerchantName))
            {
                query = query.Where(c => c.MerchantName.StartsWith(queryCondition.Condtion.MerchantName));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.MerchantDescription))
            {
                query = query.Where(c => c.MerchantDescription.StartsWith(queryCondition.Condtion.MerchantDescription));
            }
            if (queryCondition.Condtion.Status > 0)
            {
                query = query.Where(c => c.Status == queryCondition.Condtion.Status);
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
                query = query.Where(c => c.Telephone.StartsWith(queryCondition.Condtion.Telephone));
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
                query = query.Where(c => c.ContactPerson1.StartsWith(queryCondition.Condtion.ContactPerson1));
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
            if (!string.IsNullOrEmpty(queryCondition.Condtion.CooperationMode))
            {
                query = query.Where(c => c.CooperationMode.StartsWith(queryCondition.Condtion.CooperationMode));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.DisplayName))
            {
                query = query.Where(c => c.DisplayName.StartsWith(queryCondition.Condtion.DisplayName));
            }
            if (queryCondition.Condtion.GuarantyFunds > 0)
            {
                query = query.Where(c => c.GuarantyFunds == queryCondition.Condtion.GuarantyFunds);
            }
            if (queryCondition.Condtion.ContractStart != null)
            {
                query = query.Where(c => c.ContractStart > queryCondition.Condtion.ContractStart);
            }
            if (queryCondition.Condtion.ContractStartTo != null)
            {
                query = query.Where(c => c.ContractStart <= queryCondition.Condtion.ContractStartTo);
            }
            if (queryCondition.Condtion.ContractEnd != null)
            {
                query = query.Where(c => c.ContractEnd > queryCondition.Condtion.ContractEnd);
            }
            if (queryCondition.Condtion.ContractEndTo != null)
            {
                query = query.Where(c => c.ContractEnd <= queryCondition.Condtion.ContractEndTo);
            }
            if (queryCondition.Condtion.UsageCharges > 0)
            {
                query = query.Where(c => c.UsageCharges == queryCondition.Condtion.UsageCharges);
            }
            if (queryCondition.Condtion.PaymentCycle > 0)
            {
                query = query.Where(c => c.PaymentCycle == queryCondition.Condtion.PaymentCycle);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.ContactPerson3))
            {
                query = query.Where(c => c.ContactPerson3.StartsWith(queryCondition.Condtion.ContactPerson3));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Post3))
            {
                query = query.Where(c => c.Post3.StartsWith(queryCondition.Condtion.Post3));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Dept3))
            {
                query = query.Where(c => c.Dept3.StartsWith(queryCondition.Condtion.Dept3));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Mobile3))
            {
                query = query.Where(c => c.Mobile3.StartsWith(queryCondition.Condtion.Mobile3));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Telephone3))
            {
                query = query.Where(c => c.Telephone3.StartsWith(queryCondition.Condtion.Telephone3));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.Email3))
            {
                query = query.Where(c => c.Email3.StartsWith(queryCondition.Condtion.Email3));
            }
            if (queryCondition.Condtion.CommissionRatio > 0)
            {
                query = query.Where(c => c.CommissionRatio == queryCondition.Condtion.CommissionRatio);
            }
            if (queryCondition.Condtion.ReturnMerchantRatio > 0)
            {
                query = query.Where(c => c.ReturnMerchantRatio == queryCondition.Condtion.ReturnMerchantRatio);
            }
            if (queryCondition.Condtion.ReturnMallRatio > 0)
            {
                query = query.Where(c => c.ReturnMallRatio == queryCondition.Condtion.ReturnMallRatio);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.InvoiceBy))
            {
                query = query.Where(c => c.InvoiceBy.StartsWith(queryCondition.Condtion.InvoiceBy));
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
            return query;
        }
        protected override IQueryable<MerchantInfoHistoryDetail> SetOrder(QueryConditionInfo<MerchantInfoHistoryDetail> queryCondition, IQueryable<MerchantInfoHistoryDetail> query)
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
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ID);
                        }
                    }
                    if (item.FieldName == "MerchantID")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.MerchantID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.MerchantID);
                        }
                    }
                    if (item.FieldName == "MerchantName")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.MerchantName);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.MerchantName);
                        }
                    }
                    if (item.FieldName == "MerchantDescription")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.MerchantDescription);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.MerchantDescription);
                        }
                    }
                    if (item.FieldName == "Status")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Status);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Status);
                        }
                    }
                    if (item.FieldName == "Logo")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Logo);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Logo);
                        }
                    }
                    if (item.FieldName == "ServicePhone")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ServicePhone);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ServicePhone);
                        }
                    }
                    if (item.FieldName == "LegalRepresentative")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.LegalRepresentative);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.LegalRepresentative);
                        }
                    }
                    if (item.FieldName == "TaxNO")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.TaxNO);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.TaxNO);
                        }
                    }
                    if (item.FieldName == "TaxRegistrationCertificateURL")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.TaxRegistrationCertificateURL);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.TaxRegistrationCertificateURL);
                        }
                    }
                    if (item.FieldName == "Address")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Address);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Address);
                        }
                    }
                    if (item.FieldName == "PostalCode")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.PostalCode);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.PostalCode);
                        }
                    }
                    if (item.FieldName == "OrganizationCode")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.OrganizationCode);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.OrganizationCode);
                        }
                    }
                    if (item.FieldName == "BusinessLicenseURL")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.BusinessLicenseURL);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.BusinessLicenseURL);
                        }
                    }
                    if (item.FieldName == "Telephone")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Telephone);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Telephone);
                        }
                    }
                    if (item.FieldName == "Fax")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Fax);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Fax);
                        }
                    }
                    if (item.FieldName == "Fax2")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Fax2);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Fax2);
                        }
                    }
                    if (item.FieldName == "Website")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Website);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Website);
                        }
                    }
                    if (item.FieldName == "Email")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Email);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Email);
                        }
                    }
                    if (item.FieldName == "ContactPerson1")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ContactPerson1);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ContactPerson1);
                        }
                    }
                    if (item.FieldName == "Post1")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Post1);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Post1);
                        }
                    }
                    if (item.FieldName == "Dept1")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Dept1);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Dept1);
                        }
                    }
                    if (item.FieldName == "Mobile1")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Mobile1);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Mobile1);
                        }
                    }
                    if (item.FieldName == "Telephone1")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Telephone1);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Telephone1);
                        }
                    }
                    if (item.FieldName == "Email1")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Email1);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Email1);
                        }
                    }
                    if (item.FieldName == "ContactPerson2")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ContactPerson2);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ContactPerson2);
                        }
                    }
                    if (item.FieldName == "Post2")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Post2);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Post2);
                        }
                    }
                    if (item.FieldName == "Dept2")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Dept2);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Dept2);
                        }
                    }
                    if (item.FieldName == "Mobile2")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Mobile2);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Mobile2);
                        }
                    }
                    if (item.FieldName == "Telephone2")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Telephone2);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Telephone2);
                        }
                    }
                    if (item.FieldName == "Email2")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Email2);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Email2);
                        }
                    }
                    if (item.FieldName == "OpeningBank")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.OpeningBank);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.OpeningBank);
                        }
                    }
                    if (item.FieldName == "BankCardNO")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.BankCardNO);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.BankCardNO);
                        }
                    }
                    if (item.FieldName == "CreateUser")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.CreateUser);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.CreateUser);
                        }
                    }
                    if (item.FieldName == "CreateDate")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.CreateDate);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.CreateDate);
                        }
                    }
                    if (item.FieldName == "EditUser")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.EditUser);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.EditUser);
                        }
                    }
                    if (item.FieldName == "EditDate")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.EditDate);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.EditDate);
                        }
                    }
                    if (item.FieldName == "CooperationMode")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.CooperationMode);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.CooperationMode);
                        }
                    }
                    if (item.FieldName == "DisplayName")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.DisplayName);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.DisplayName);
                        }
                    }
                    if (item.FieldName == "GuarantyFunds")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.GuarantyFunds);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.GuarantyFunds);
                        }
                    }
                    if (item.FieldName == "ContractStart")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ContractStart);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ContractStart);
                        }
                    }
                    if (item.FieldName == "ContractEnd")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ContractEnd);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ContractEnd);
                        }
                    }
                    if (item.FieldName == "UsageCharges")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.UsageCharges);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.UsageCharges);
                        }
                    }
                    if (item.FieldName == "PaymentCycle")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.PaymentCycle);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.PaymentCycle);
                        }
                    }
                    if (item.FieldName == "ContactPerson3")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ContactPerson3);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ContactPerson3);
                        }
                    }
                    if (item.FieldName == "Post3")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Post3);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Post3);
                        }
                    }
                    if (item.FieldName == "Dept3")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Dept3);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Dept3);
                        }
                    }
                    if (item.FieldName == "Mobile3")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Mobile3);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Mobile3);
                        }
                    }
                    if (item.FieldName == "Telephone3")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Telephone3);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Telephone3);
                        }
                    }
                    if (item.FieldName == "Email3")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Email3);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Email3);
                        }
                    }
                    if (item.FieldName == "CommissionRatio")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.CommissionRatio);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.CommissionRatio);
                        }
                    }
                    if (item.FieldName == "ReturnMerchantRatio")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ReturnMerchantRatio);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ReturnMerchantRatio);
                        }
                    }
                    if (item.FieldName == "ReturnMallRatio")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ReturnMallRatio);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ReturnMallRatio);
                        }
                    }
                    if (item.FieldName == "InvoiceBy")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.InvoiceBy);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.InvoiceBy);
                        }
                    }
                    if (item.FieldName == "ProductManager")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ProductManager);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ProductManager);
                        }
                    }
                    if (item.FieldName == "TransitCostBy")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.TransitCostBy);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.TransitCostBy);
                        }
                    }
                    if (item.FieldName == "FreeTransitAmount")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.FreeTransitAmount);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.FreeTransitAmount);
                        }
                    }
                    if (item.FieldName == "BankProvinceID")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.BankProvinceID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.BankProvinceID);
                        }
                    }
                    if (item.FieldName == "BankProvince")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.BankProvince);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.BankProvince);
                        }
                    }
                    if (item.FieldName == "BankCityID")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.BankCityID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.BankCityID);
                        }
                    }
                    if (item.FieldName == "BankCity")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.BankCity);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.BankCity);
                        }
                    }
                }
            }
            else
            {
                query = query.OrderByDescending(c => c.ID);
            }
            return query;
        }
        public void ChangeStatus(MerchantInfoHistoryDetail entity)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfoHistoryDetail> objectSet = entities.CreateObjectSet<MerchantInfoHistoryDetail>();
                MerchantInfoHistoryDetail MerchantInfoHistoryDetail = objectSet.FirstOrDefault(c => c.ID == entity.ID);
                if (MerchantInfoHistoryDetail != null)
                {
                    MerchantInfoHistoryDetail.EditUser = entity.EditUser;
                    MerchantInfoHistoryDetail.EditDate = DateTime.Now;
                    MerchantInfoHistoryDetail.Status = entity.Status;
                    entities.SaveChanges();
                }
            }
        }

        internal List<int?> GetAllDistinctMerchantIDList()
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantInfoHistoryDetail> objectSet = entities.CreateObjectSet<MerchantInfoHistoryDetail>();
                return objectSet.Where(c => c.Status == 0 || c.Status == 3 || c.Status == 1).Select(c => c.MerchantID).Distinct().ToList();
            }
        }
    }
}
