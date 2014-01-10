using System.Linq;
using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess
{
    internal class UVAuditWorkFlowDA : DataBase<UVAuditWorkFlow, SecurityEntities>
    {
        internal static UVAuditWorkFlowDA DAO = new UVAuditWorkFlowDA();
        private UVAuditWorkFlowDA() { }
        protected override void AttachValue(UVAuditWorkFlow newEntity, UVAuditWorkFlow oldEntity)
        {
            oldEntity.DisplayName = newEntity.DisplayName;
            oldEntity.ResourceID = newEntity.ResourceID;
            oldEntity.TotalLevel = newEntity.TotalLevel;
            oldEntity.ResourceDisplayName = newEntity.ResourceDisplayName;
            oldEntity.ResourceName = newEntity.ResourceName;
        }
        protected override IQueryable<UVAuditWorkFlow> SetWhereClause(QueryConditionInfo<UVAuditWorkFlow> queryCondition, IQueryable<UVAuditWorkFlow> query)
        {
            if (queryCondition.Condtion.ID > 0)
            {
                query = query.Where(c => c.ID == queryCondition.Condtion.ID);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.DisplayName))
            {
                query = query.Where(c => c.DisplayName.StartsWith(queryCondition.Condtion.DisplayName));
            }
            if (queryCondition.Condtion.ResourceID > 0)
            {
                query = query.Where(c => c.ResourceID == queryCondition.Condtion.ResourceID);
            }
            if (queryCondition.Condtion.TotalLevel > 0)
            {
                query = query.Where(c => c.TotalLevel == queryCondition.Condtion.TotalLevel);
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.ResourceDisplayName))
            {
                query = query.Where(c => c.ResourceDisplayName.StartsWith(queryCondition.Condtion.ResourceDisplayName));
            }
            if (!string.IsNullOrEmpty(queryCondition.Condtion.ResourceName))
            {
                query = query.Where(c => c.ResourceName.StartsWith(queryCondition.Condtion.ResourceName));
            }
            return query;
        }
        protected override IQueryable<UVAuditWorkFlow> SetOrder(QueryConditionInfo<UVAuditWorkFlow> queryCondition, IQueryable<UVAuditWorkFlow> query)
        {
            int count = queryCondition.OrderFileds.Count;
            if (count > 0)
            {
                for (int i = count; i > 0; i--)
                {
                    OrderFiledInfo item = queryCondition.OrderFileds[i - i];
                    if (item.FieldName == "ID")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ID) : query.OrderByDescending(c => c.ID);
                    }
                    if (item.FieldName == "DisplayName")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.DisplayName) : query.OrderByDescending(c => c.DisplayName);
                    }
                    if (item.FieldName == "ResourceID")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ResourceID) : query.OrderByDescending(c => c.ResourceID);
                    }
                    if (item.FieldName == "TotalLevel")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.TotalLevel) : query.OrderByDescending(c => c.TotalLevel);
                    }
                    if (item.FieldName == "ResourceDisplayName")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ResourceDisplayName) : query.OrderByDescending(c => c.ResourceDisplayName);
                    }
                    if (item.FieldName == "ResourceName")
                    {
                        query = item.OrderDirection == OrderDirection.ASC ? query.OrderBy(c => c.ResourceName) : query.OrderByDescending(c => c.ResourceName);
                    }
                }
            }
            else
            {
                query = query.OrderByDescending(c => c.ID);
            }
            return query;
        }
    }
}
