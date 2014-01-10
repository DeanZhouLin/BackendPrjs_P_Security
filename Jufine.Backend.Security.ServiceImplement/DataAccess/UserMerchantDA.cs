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
    internal class UserMerchantDA : DataBase<UserMerchant, SecurityEntities>
    {
        internal static UserMerchantDA DAO = new UserMerchantDA();
        private UserMerchantDA() { }
        protected override void AttachValue(UserMerchant newEntity, UserMerchant oldEntity)
        {
            oldEntity.UserID = newEntity.UserID;
            oldEntity.MerchantID = newEntity.MerchantID;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
        }
        protected override IQueryable<UserMerchant> SetWhereClause(QueryConditionInfo<UserMerchant> queryCondition, IQueryable<UserMerchant> query)
        {
            if (queryCondition.Condtion.ID > 0)
            {
                query = query.Where(c => c.ID == queryCondition.Condtion.ID);
            }
            if (queryCondition.Condtion.UserID > 0)
            {
                query = query.Where(c => c.UserID == queryCondition.Condtion.UserID);
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
        protected override IQueryable<UserMerchant> SetOrder(QueryConditionInfo<UserMerchant> queryCondition, IQueryable<UserMerchant> query)
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

        internal List<UserMerchant> GetByUserID(int id)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UserMerchant> objectSet = entities.CreateObjectSet<UserMerchant>();
                return objectSet.Where(c => c.UserID == id).ToList();
            }
        }

        internal void UpdateMerchantByUser(List<int> list, int userID, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    ObjectSet<UserMerchant> objectSet = entities.CreateObjectSet<UserMerchant>();
                    objectSet.Delete(c => c.UserID == userID && !list.Contains(c.MerchantID));
                    List<int> existIDList = objectSet.Where(c => c.UserID == userID).Select(c => c.MerchantID).ToList();
                    list.RemoveAll(c => existIDList.Contains(c));
                    UserMerchant um = null;
                    DateTime dt = DateTime.Now;
                    foreach (int mid in list)
                    {
                        um = new UserMerchant()
                        {
                            UserID = userID,
                            MerchantID = mid,
                            CreateUser = editUser,
                            CreateDate = dt,
                        };
                        objectSet.AddObject(um);
                    }
                    entities.SaveChanges();
                    ts.Complete();
                }
            }
        }
    }
}
