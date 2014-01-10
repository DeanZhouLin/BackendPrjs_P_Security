using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess
{
    internal class AuditWorkFlowResourceDA : DataBase<AuditWorkFlowResource, SecurityEntities>
    {
        internal static AuditWorkFlowResourceDA DAO = new AuditWorkFlowResourceDA();
        private AuditWorkFlowResourceDA() { }
        protected override void AttachValue(AuditWorkFlowResource newEntity, AuditWorkFlowResource oldEntity)
        {
            oldEntity.DisplayName = newEntity.DisplayName;
            oldEntity.ResourceID = newEntity.ResourceID;
            oldEntity.RoleID = newEntity.RoleID;
            oldEntity.UserID = newEntity.UserID;
            oldEntity.Status = newEntity.Status;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
            oldEntity.GroupID = newEntity.GroupID;
            oldEntity.ParentGroupID = newEntity.ParentGroupID;
        }
        protected override IQueryable<AuditWorkFlowResource> SetWhereClause(QueryConditionInfo<AuditWorkFlowResource> queryCondition, IQueryable<AuditWorkFlowResource> query)
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
            if (queryCondition.Condtion.RoleID > 0)
            {
                query = query.Where(c => c.RoleID == queryCondition.Condtion.RoleID);
            }
            if (queryCondition.Condtion.UserID > 0)
            {
                query = query.Where(c => c.UserID == queryCondition.Condtion.UserID);
            }
            if (queryCondition.Condtion.Status > 0)
            {
                query = query.Where(c => c.Status == queryCondition.Condtion.Status);
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
            if (queryCondition.Condtion.GroupID > 0)
            {
                query = query.Where(c => c.GroupID == queryCondition.Condtion.GroupID);
            }
            if (queryCondition.Condtion.ParentGroupID > 0)
            {
                query = query.Where(c => c.ParentGroupID == queryCondition.Condtion.ParentGroupID);
            }
            return query;
        }
        protected override IQueryable<AuditWorkFlowResource> SetOrder(QueryConditionInfo<AuditWorkFlowResource> queryCondition, IQueryable<AuditWorkFlowResource> query)
        {
            int count = queryCondition.OrderFileds.Count;
            if (count > 0)
            {
                for (int i = count; i > 0; i--)
                {
                    OrderFiledInfo item = queryCondition.OrderFileds[i - i];
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
                    if (item.FieldName == "ResourceID")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ResourceID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ResourceID);
                        }
                    }
                    if (item.FieldName == "RoleID")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.RoleID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.RoleID);
                        }
                    }
                    if (item.FieldName == "UserID")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.UserID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.UserID);
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
                    if (item.FieldName == "GroupID")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.GroupID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.GroupID);
                        }
                    }
                    if (item.FieldName == "ParentGroupID")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.ParentGroupID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.ParentGroupID);
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
        public void ChangeStatus(AuditWorkFlowResource entity)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<AuditWorkFlowResource> objectSet = entities.CreateObjectSet<AuditWorkFlowResource>();
                AuditWorkFlowResource AuditWorkFlowResource = objectSet.FirstOrDefault(c => c.ID == entity.ID);
                if (AuditWorkFlowResource != null)
                {
                    AuditWorkFlowResource.EditUser = entity.EditUser;
                    AuditWorkFlowResource.EditDate = DateTime.Now;
                    AuditWorkFlowResource.Status = entity.Status;
                    entities.SaveChanges();
                }
            }
        }

        public List<TAuditWorkFlowResource> GetTAuditWorkFlowResources(int resourceID)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                var temp = entities.ExecuteFunction<TAuditWorkFlowResource>(
                    "UP_GetAuditWorkFlowResourceByResourceID",
                     MergeOption.NoTracking,
                     BuildParameter("ResourceID", resourceID)
                );
                return temp.ToList();
            }
        }

        public void AddAuditUserForAuditWorkFlow(int resourceID, string userName, int auditLevel, string createUser, out string msg, out int msgNumber)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {

                ObjectParameter pMsg = BuildParameter("Msg", string.Empty);
                ObjectParameter pMsgNumber = BuildParameter("MsgNumber", 0);
                entities.ExecuteFunction(
                    "UP_AddAuditUserForAuditWorkFlow",
                     BuildParameter("ResourceName", null),
                     BuildParameter("ResourceID", resourceID),
                     BuildParameter("UserName", userName),
                     BuildParameter("AuditLevel", auditLevel),
                     BuildParameter("CreateUser", createUser),
                     pMsg,
                     pMsgNumber
                );
                int.TryParse(pMsgNumber.Value.ToString(), out msgNumber);
                msg = pMsg.Value.ToString();
            }
        }
        public void DeleteAuditUserForAuditWorkFlow(int resourceID, string userName, string editUser, out string msg, out int msgNumber)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {

                ObjectParameter pMsg = BuildParameter("Msg", string.Empty);
                ObjectParameter pMsgNumber = BuildParameter("MsgNumber", 0);
                entities.ExecuteFunction(
                    "UP_DeleteAuditUserForAuditWorkFlow",
                     BuildParameter("ResourceName", null),
                     BuildParameter("ResourceID", resourceID),
                     BuildParameter("UserName", userName),
                     BuildParameter("EditUser", editUser),
                     pMsg,
                     pMsgNumber
                );
                int.TryParse(pMsgNumber.Value.ToString(), out msgNumber);
                msg = pMsg.Value.ToString();
            }
        }

        public override int BatchDelete(List<int> resourceIDList)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<AuditWorkFlowResource> objectSet = entities.CreateObjectSet<AuditWorkFlowResource>();
                //使用批量删除技术
                int result = objectSet.Delete(c => resourceIDList.Contains(c.ResourceID));
                return result;
            }
        }

        public override int Delete(int resourceID)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<AuditWorkFlowResource> objectSet = entities.CreateObjectSet<AuditWorkFlowResource>();
                //使用批量删除技术
                int result = objectSet.Delete(c => c.ResourceID == resourceID);
                return result;
            }
        }

    }
}
