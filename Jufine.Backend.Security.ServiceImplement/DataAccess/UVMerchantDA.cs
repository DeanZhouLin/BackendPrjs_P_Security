using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using Com.BaseLibrary.Common.Security;
using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess
{
    internal class UVMerchantInfoDA : DataBase<UVMerchantInfo, SecurityEntities>
    {

        internal static UVMerchantInfoDA DAO = new UVMerchantInfoDA();

        private UVMerchantInfoDA() { }

        public IEnumerable<UVMerchantInfo> SetWhereClause(QueryConditionInfo<UVMerchantInfo> queryCondition, List<UVMerchantInfo> queryList)
        {
            IEnumerable<UVMerchantInfo> query = queryList;

            //商家名称
            if (!string.IsNullOrEmpty(queryCondition.Condtion.S_MerchantName))
            {
                query = query.Where(c => c.S_MerchantName != null && c.S_MerchantName.StartsWith(queryCondition.Condtion.S_MerchantName));
            }
            //服务电话
            if (!string.IsNullOrEmpty(queryCondition.Condtion.S_ServicePhone))
            {
                query = query.Where(c => c.S_ServicePhone != null && c.S_ServicePhone.StartsWith(queryCondition.Condtion.S_ServicePhone));
            }
            //联系电话
            if (!string.IsNullOrEmpty(queryCondition.Condtion.S_Telephone))
            {
                //query = query.Where(c => c.Telephone1.StartsWith(queryCondition.Condtion.S_Telephone) || c.Telephone2.StartsWith(queryCondition.Condtion.S_Telephone) || c.Telephone3.StartsWith(queryCondition.Condtion.S_Telephone) || c.MI_Telephone1.StartsWith(queryCondition.Condtion.S_Telephone) || c.MI_Telephone2.StartsWith(queryCondition.Condtion.S_Telephone) || c.MI_Telephone3.StartsWith(queryCondition.Condtion.S_Telephone));
                query = query.Where(c => c.S_Telephone != null && c.S_Telephone.StartsWith(queryCondition.Condtion.S_Telephone));
            }
            //联系人
            if (!string.IsNullOrEmpty(queryCondition.Condtion.S_ContactPerson1))
            {
                query = query.Where(c => c.S_ContactPerson1 != null && c.S_ContactPerson1.StartsWith(queryCondition.Condtion.S_ContactPerson1));
            }
            //商家状态
            if (queryCondition.Condtion.S_Status >= 0)
            {
                query = query.Where(c => c.S_Status == queryCondition.Condtion.S_Status);
            }
            return query;
        }

        public IOrderedEnumerable<UVMerchantInfo> SetOrder(QueryConditionInfo<UVMerchantInfo> queryCondition, List<UVMerchantInfo> queryList)
        {
            int count = queryCondition.OrderFileds.Count;

            if (count <= 0) return queryList.OrderByDescending(c => c.MI_CreateDate);

            OrderFiledInfo item = queryCondition.OrderFileds[0];

            if (item.FieldName == "S_MerchantID")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_MerchantID) : queryList.OrderByDescending(c => c.S_MerchantID);
            }
            if (item.FieldName == "S_MerchantName")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_MerchantName) : queryList.OrderByDescending(c => c.S_MerchantName);
            }
            if (item.FieldName == "S_ServicePhone")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_ServicePhone) : queryList.OrderByDescending(c => c.S_ServicePhone);
            }
            if (item.FieldName == "S_Telephone")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_Telephone) : queryList.OrderByDescending(c => c.S_Telephone);
            }
            if (item.FieldName == "S_ContactPerson1")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_ContactPerson1) : queryList.OrderByDescending(c => c.S_ContactPerson1);
            }
            if (item.FieldName == "S_EditDate")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_EditDate) : queryList.OrderByDescending(c => c.S_EditDate);
            }
            if (item.FieldName == "S_CooperationMode")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_CooperationMode) : queryList.OrderByDescending(c => c.S_CooperationMode);
            }
            if (item.FieldName == "S_OrganizationCode")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_OrganizationCode) : queryList.OrderByDescending(c => c.S_OrganizationCode);
            }
            if (item.FieldName == "S_EditUser")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_EditUser) : queryList.OrderByDescending(c => c.S_EditUser);
            }
            if (item.FieldName == "S_Status")
            {
                return item.OrderDirection == OrderDirection.ASC ? queryList.OrderBy(c => c.S_Status) : queryList.OrderByDescending(c => c.S_Status);
            }
            return queryList.OrderByDescending(c => c.MI_CreateDate);
        }

        public List<UVMerchantInfo> GetTotalUVMerchantInfo(IUser currentUser, QueryConditionInfo<UVMerchantInfo> queryCondition)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UVMerchantInfo> uvItemInfo = entities.CreateObjectSet<UVMerchantInfo>();
                var query = from a in uvItemInfo
                            select a;

                query = query.Where(c => c.MI_Status != -1 && c.Status != -1);

                if (!queryCondition.IsAdmin)
                {
                    if (queryCondition.MerchantList == null || queryCondition.MerchantList.Count == 0)
                    {
                        query = query.Where(c => c.MerchantID == -1);
                    }
                    else if (queryCondition.MerchantList.Count == 1)
                    {
                        int merchantID = queryCondition.MerchantList[0];
                        query = query.Where(c => c.MerchantID == merchantID);
                    }
                    else
                    {
                        query = query.Where(c => c.MerchantID != null && queryCondition.MerchantList.Contains((int)c.MerchantID));
                    }
                }

                //商家ID
                if (queryCondition.Condtion.MI_ID > 0)
                {
                    query = query.Where(c => c.MI_ID == queryCondition.Condtion.MI_ID);
                }

                //合作模式
                if (!string.IsNullOrEmpty(queryCondition.Condtion.MI_CooperationMode))
                {
                    query = query.Where(c => c.MI_CooperationMode == queryCondition.Condtion.MI_CooperationMode);
                }

                //创建日期
                if (queryCondition.Condtion.MI_CreateDate != null)
                {
                    query = query.Where(c => c.MI_CreateDate > queryCondition.Condtion.MI_CreateDate);
                }
                if (queryCondition.Condtion.MI_CreateDateTo != null)
                {
                    query = query.Where(c => c.MI_CreateDate <= queryCondition.Condtion.MI_CreateDateTo);
                }

                return query.ToList();
            }
        }

        [Obsolete("该方法已过期，请使用GetTotalUVMerchantInfo方法")]
        public QueryResultInfo<UVMerchantInfo> GetUVMerchantInfoDynamic(QueryConditionInfo<UVMerchantInfo> queryCondition)
        {
            QueryResultInfo<UVMerchantInfo> result = new QueryResultInfo<UVMerchantInfo>();
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UVMerchantInfo> uvItemInfo = entities.CreateObjectSet<UVMerchantInfo>();
                var query = from a in uvItemInfo
                            select a;

                query = SetWhereClause(queryCondition, query);
                result.RecordCount = query.Count();
                if (result.RecordCount == 0)
                {
                    result.RecordList = new List<UVMerchantInfo>();
                    return result;
                }

                query = SetOrder(queryCondition, query);

                if (queryCondition.ReturnAllData)
                {
                    result.RecordList = query.ToList();
                    return result;
                }

                int startRowIndex = (queryCondition.PageIndex - 1) * queryCondition.PageSize;

                if (startRowIndex > result.RecordCount)//如果起始位置大于总记录数，取最后一页
                {
                    startRowIndex = Math.Max(0, result.RecordCount - queryCondition.PageSize);
                    result.RecordList = query.Skip(startRowIndex).ToList();
                }
                else
                {
                    int pageSize = Math.Min((result.RecordCount - startRowIndex), queryCondition.PageSize);
                    result.RecordList = query.Skip(startRowIndex).Take(pageSize).ToList();
                }
                return result;

            }
        }

        public UVMerchantInfo GetUVMerchantInfo(int id)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UVMerchantInfo> objectSet = entities.CreateObjectSet<UVMerchantInfo>();
                UVMerchantInfo uvMerchantInfo = objectSet.FirstOrDefault(c => c.ID == id && c.Status != -1);
                return uvMerchantInfo;
            }
        }

        public UVMerchantInfo GetUVMerchantInfo(int id, int merchantId)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UVMerchantInfo> objectSet = entities.CreateObjectSet<UVMerchantInfo>();
                UVMerchantInfo uvMerchantInfo = objectSet.FirstOrDefault(c => c.ID == id && c.MerchantID == merchantId);
                return uvMerchantInfo;
            }
        }

        public UVMerchantInfo GetUVMerchantInfoByMerchantID(int merchantId)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UVMerchantInfo> objectSet = entities.CreateObjectSet<UVMerchantInfo>();
                UVMerchantInfo uvMerchantInfo = objectSet.FirstOrDefault(c => c.MerchantID == merchantId);
                return uvMerchantInfo;
            }
        }

        public string MerchantInfoChangeStatus(int id, string auditUser, string reason, int status)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectParameter putauditDate = BuildParameter("AuditDate", null);
                entities.ExecuteFunction("UP_UpdateMerchantInfoAuditStatus",
                 BuildParameter("ID", id),
                 BuildParameter("AuditUser", auditUser),
                 BuildParameter("Reason", reason),
                 BuildParameter("Status", status),
                 putauditDate
                 );
                return putauditDate.Value.ToString();
            }
        }

        internal bool IsExsit(MerchantInfo merchant)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UVMerchantInfo> objectSet = entities.CreateObjectSet<UVMerchantInfo>();
                return objectSet.
                    FirstOrDefault
                    (c =>
                        c.Status != -1 && c.MI_Status != -1 &&
                        c.Status != 3 && c.MI_Status != 3 &&
                        c.MerchantID != merchant.ID &&
                        !string.IsNullOrEmpty(merchant.MerchantName) &&
                         !string.IsNullOrEmpty(merchant.OrganizationCode) &&
                          !string.IsNullOrEmpty(merchant.TaxNO) &&
                        (
                            c.MerchantName == merchant.MerchantName || c.MI_MerchantName == merchant.MerchantName ||
                            c.OrganizationCode == merchant.OrganizationCode || c.MI_OrganizationCode == merchant.OrganizationCode ||
                            c.TaxNO == merchant.TaxNO || c.MI_TaxNO == merchant.TaxNO
                         )
                     ) == null;
            }
        }

        internal bool IsExsit(MerchantInfoHistoryDetail merchant)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UVMerchantInfo> objectSet = entities.CreateObjectSet<UVMerchantInfo>();
                return objectSet.
                    FirstOrDefault
                    (c =>
                        c.Status != -1 && c.MI_Status != -1 &&
                        c.Status != 3 && c.MI_Status != 3 &&
                        c.MerchantID != merchant.MerchantID &&
                         !string.IsNullOrEmpty(merchant.MerchantName) &&
                         !string.IsNullOrEmpty(merchant.OrganizationCode) &&
                          !string.IsNullOrEmpty(merchant.TaxNO) &&
                        (c.MerchantName == merchant.MerchantName || c.MI_MerchantName == merchant.MerchantName ||
                        c.OrganizationCode == merchant.OrganizationCode || c.MI_OrganizationCode == merchant.OrganizationCode ||
                        c.TaxNO == merchant.TaxNO || c.MI_TaxNO == merchant.TaxNO)) == null;
            }
        }

    }
}
