﻿using System;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Data;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.DataAccess;
using System.Transactions;

namespace Jufine.Backend.Security.DataAccess
{
    internal class UserRoleDA : DataBase<UserRole, SecurityEntities>
    {
        internal static UserRoleDA DAO = new UserRoleDA();
        private UserRoleDA() { }
        protected override void AttachValue(UserRole newEntity, UserRole oldEntity)
        {
            oldEntity.UserID = newEntity.UserID;
            oldEntity.RoleID = newEntity.RoleID;
            oldEntity.MerchantID = newEntity.MerchantID;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
        }
        protected override IQueryable<UserRole> SetWhereClause(QueryConditionInfo<UserRole> queryCondition, IQueryable<UserRole> query)
        {
            if (queryCondition.Condtion.ID > 0)
            {
                query = query.Where(c => c.ID == queryCondition.Condtion.ID);
            }
            if (queryCondition.Condtion.UserID > 0)
            {
                query = query.Where(c => c.UserID == queryCondition.Condtion.UserID);
            }
            if (queryCondition.Condtion.RoleID > 0)
            {
                query = query.Where(c => c.RoleID == queryCondition.Condtion.RoleID);
            }
            if (queryCondition.Condtion.MerchantID > 0)
            {
                query = query.Where(c => c.MerchantID == queryCondition.Condtion.MerchantID);
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
        protected override IQueryable<UserRole> SetOrder(QueryConditionInfo<UserRole> queryCondition, IQueryable<UserRole> query)
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

        internal List<UserRole> GetByUserID(int id, int merchantID)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UserRole> objectSet = entities.CreateObjectSet<UserRole>();
                return objectSet.Where(c => c.UserID == id && c.MerchantID == merchantID).ToList();
            }
        }

        internal void UpdateRoleByUser(List<int> list, int userID, int merchantID, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    ObjectSet<UserRole> objectSet = entities.CreateObjectSet<UserRole>();
                    objectSet.Delete(c => c.UserID == userID && c.MerchantID == merchantID && !list.Contains(c.RoleID));
                    List<int> existList = objectSet.Where(c => c.UserID == userID && c.MerchantID == merchantID).Select(c => c.RoleID).ToList();
                    list.RemoveAll(c => existList.Contains(c));
                    UserRole ur = null;
                    DateTime dt = DateTime.Now;
                    foreach (int rid in list)
                    {
                        ur = new UserRole()
                        {
                            UserID = userID,
                            MerchantID = merchantID,
                            RoleID = rid,
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
