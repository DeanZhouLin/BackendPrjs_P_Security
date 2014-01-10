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
	internal class UserResourceDA : DataBase<UserResource, SecurityEntities>
	{
		internal static UserResourceDA DAO = new UserResourceDA();
		private UserResourceDA() { }
		protected override void AttachValue(UserResource newEntity, UserResource oldEntity)
		{
			oldEntity.UserID = newEntity.UserID;
			oldEntity.ResourceID = newEntity.ResourceID;
            oldEntity.MerchantID = newEntity.MerchantID;
			oldEntity.CreateUser = newEntity.CreateUser;
			oldEntity.CreateDate = newEntity.CreateDate;
			oldEntity.EditUser = newEntity.EditUser;
			oldEntity.EditDate = newEntity.EditDate;
		}
		protected override IQueryable<UserResource> SetWhereClause(QueryConditionInfo<UserResource> queryCondition, IQueryable<UserResource> query)
		{
			if (queryCondition.Condtion.ID > 0)
			{
				query = query.Where(c => c.ID == queryCondition.Condtion.ID);
			}
			if (queryCondition.Condtion.UserID > 0)
			{
				query = query.Where(c => c.UserID == queryCondition.Condtion.UserID);
			}
			if (queryCondition.Condtion.ResourceID > 0)
			{
				query = query.Where(c => c.ResourceID == queryCondition.Condtion.ResourceID);
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
		protected override IQueryable<UserResource> SetOrder(QueryConditionInfo<UserResource> queryCondition, IQueryable<UserResource> query)
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

		internal List<UserResource> GetByUserID(int id,int merchantID)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserResource> objectSet = entities.CreateObjectSet<UserResource>();
                return objectSet.Where(c => c.UserID == id && c.MerchantID == merchantID).ToList();
			}
		}

		internal void DeleteByUserID(int userID)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserResource> objectSet = entities.CreateObjectSet<UserResource>();
				objectSet.Delete(c => c.UserID == userID);
			}
		}

		internal void DeleteByResourceID(int id)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserResource> objectSet = entities.CreateObjectSet<UserResource>();
				objectSet.Delete(c => c.ResourceID == id);
			}
		}

		internal void DeleteByResourceList(List<int> idList)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserResource> objectSet = entities.CreateObjectSet<UserResource>();
				objectSet.Delete(c => idList.Contains(c.ID));
			}
		}

        internal void UpdateAuthorityByUser(List<int> list, int userID, int merchantID, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    ObjectSet<UserResource> objectSet = entities.CreateObjectSet<UserResource>();
                    objectSet.Delete(c => c.UserID == userID && c.MerchantID == merchantID && !list.Contains(c.ResourceID));
                    List<int> existList = objectSet.Where(c => c.UserID == userID && c.MerchantID == merchantID).Select(c => c.ResourceID).Distinct().ToList();
                    list.RemoveAll(c => existList.Contains(c));
                    UserResource ur = null;
                    DateTime dt = DateTime.Now;
                    foreach (int rid in list)
                    {
                        ur = new UserResource()
                        {
                            UserID = userID,
                            MerchantID = merchantID,
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
