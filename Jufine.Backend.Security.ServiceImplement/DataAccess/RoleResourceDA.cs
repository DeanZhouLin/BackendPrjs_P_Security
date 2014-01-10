using System;
using System.Linq;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;
using System.Transactions;

namespace Jufine.Backend.Security.DataAccess
{
    internal class RoleResourceDA : DataBase<RoleResource, SecurityEntities>
    {
        internal static RoleResourceDA DAO = new RoleResourceDA();
        private RoleResourceDA() { }
        protected override void AttachValue(RoleResource newEntity, RoleResource oldEntity)
        {
            oldEntity.RoleID = newEntity.RoleID;
            oldEntity.ResourceID = newEntity.ResourceID;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
        }
        protected override IQueryable<RoleResource> SetWhereClause(QueryConditionInfo<RoleResource> queryCondition, IQueryable<RoleResource> query)
        {
            if (queryCondition.Condtion.ID > 0)
            {
                query = query.Where(c => c.ID == queryCondition.Condtion.ID);
            }
            if (queryCondition.Condtion.RoleID > 0)
            {
                query = query.Where(c => c.RoleID == queryCondition.Condtion.RoleID);
            }
            if (queryCondition.Condtion.ResourceID > 0)
            {
                query = query.Where(c => c.ResourceID == queryCondition.Condtion.ResourceID);
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
            return query;
        }
        protected override IQueryable<RoleResource> SetOrder(QueryConditionInfo<RoleResource> queryCondition, IQueryable<RoleResource> query)
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
                }
            }
            else
            {
                query = query.OrderByDescending(c => c.ID);
            }
            return query;
        }

        internal void DeleteByRole(int roleID)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<RoleResource> objectSet = entities.CreateObjectSet<RoleResource>();
                objectSet.Delete(c => c.ID == roleID);
            }
        }

        internal List<RoleResource> GetByRoleID(int id)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<RoleResource> objectSet = entities.CreateObjectSet<RoleResource>();
                return objectSet.Where(c => c.RoleID == id).ToList();
            }
        }

        internal List<int> GetResourceIDByRoleList(List<int> roleIDList)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<RoleResource> objectSet = entities.CreateObjectSet<RoleResource>();
                return objectSet.Where(c => roleIDList.Contains(c.RoleID)).ToList().Select(c => c.ResourceID).Distinct().ToList();
            }
        }

        internal void UpdateAuthorityByRole(List<int> list, int roleID, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    ObjectSet<RoleResource> objectSet = entities.CreateObjectSet<RoleResource>();
                    objectSet.Delete(c => c.RoleID == roleID && !list.Contains(c.ResourceID));
                    List<int> existList = objectSet.Where(c => c.RoleID == roleID).Select(c => c.ResourceID).ToList();
                    list.RemoveAll(c => existList.Contains(c));
                    RoleResource ur = null;
                    DateTime dt = DateTime.Now;
                    foreach (int rid in list)
                    {
                        ur = new RoleResource()
                        {
                            RoleID = roleID,
                            ResourceID = rid,
                            CreateDate = dt,
                            CreateUser = editUser,
                        };
                        objectSet.AddObject(ur);
                    }
                    entities.SaveChanges();
                    ts.Complete();
                }
            }
        }
    }
}
