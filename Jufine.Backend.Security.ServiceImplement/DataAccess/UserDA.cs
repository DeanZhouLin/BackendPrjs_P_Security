using System;
using System.Linq;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess
{
	internal class UserDA : DataBase<UserInfo, SecurityEntities>
	{
		internal static UserDA DAO = new UserDA();
		private UserDA() { }
		protected override void AttachValue(UserInfo newEntity, UserInfo oldEntity)
		{
			oldEntity.MerchantID = newEntity.MerchantID;
			oldEntity.UserName = newEntity.UserName;
			oldEntity.Password = newEntity.Password;
			oldEntity.FullName = newEntity.FullName;
			oldEntity.Sex = newEntity.Sex;
			oldEntity.IDCard = newEntity.IDCard;
			oldEntity.Status = newEntity.Status;
			oldEntity.Email = newEntity.Email;
			oldEntity.IsAdmin = newEntity.IsAdmin;
			oldEntity.CreateUser = newEntity.CreateUser;
			oldEntity.CreateDate = newEntity.CreateDate;
			oldEntity.EditUser = newEntity.EditUser;
			oldEntity.EditDate = newEntity.EditDate;
		}

		protected override IQueryable<UserInfo> GetSelectLinq(SecurityEntities entities, QueryConditionInfo<UserInfo> queryCondition)
		{
			ObjectSet<UserInfo> user = entities.CreateObjectSet<UserInfo>();
			ObjectSet<MerchantInfo> merchant = entities.CreateObjectSet<MerchantInfo>();
			if (string.IsNullOrEmpty(queryCondition.Condtion.MerchantName))
			{
				return from u in user
					   select u;
			}
			else
			{
				var sss = from u in user
						  join m in merchant on u.MerchantID equals m.ID
                          where m.MerchantName.Contains(queryCondition.Condtion.MerchantName)
						  select u;
				return sss;
			}
		}

		public override QueryResultInfo<UserInfo> Query(QueryConditionInfo<UserInfo> queryCondition)
		{
			return base.Query(queryCondition);
		}
		protected override IQueryable<UserInfo> SetWhereClause(QueryConditionInfo<UserInfo> queryCondition, IQueryable<UserInfo> query)
		{
			if (queryCondition.Condtion.ID > 0)
			{
				query = query.Where(c => c.ID == queryCondition.Condtion.ID);
			}
			if (queryCondition.Condtion.MerchantID > 0)
			{
                query = query.Where(c => c.MerchantID == queryCondition.Condtion.MerchantID);
			}
			if (!string.IsNullOrEmpty(queryCondition.Condtion.UserName))
			{
				query = query.Where(c => c.UserName.Contains(queryCondition.Condtion.UserName));
			}
			if (!string.IsNullOrEmpty(queryCondition.Condtion.Password))
			{
				query = query.Where(c => c.Password.StartsWith(queryCondition.Condtion.Password));
			}
			if (!string.IsNullOrEmpty(queryCondition.Condtion.FullName))
			{
                query = query.Where(c => c.FullName.Contains(queryCondition.Condtion.FullName));
			}
			if (queryCondition.Condtion.Sex >= 0)
			{
				query = query.Where(c => c.Sex == queryCondition.Condtion.Sex);
			}
			if (!string.IsNullOrEmpty(queryCondition.Condtion.IDCard))
			{
				query = query.Where(c => c.IDCard.StartsWith(queryCondition.Condtion.IDCard));
			}
			if (queryCondition.Condtion.Status >= 0)
			{
				query = query.Where(c => c.Status == queryCondition.Condtion.Status && c.Status != -1);
			}
			else
			{
				query = query.Where(c => c.Status != -1);
			}
			if (!string.IsNullOrEmpty(queryCondition.Condtion.Email))
			{
				query = query.Where(c => c.Email.StartsWith(queryCondition.Condtion.Email));
			}
			if (queryCondition.Condtion.IsAdmin > 0)
			{
				query = query.Where(c => c.IsAdmin == queryCondition.Condtion.IsAdmin);
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
                    query = query.Where(c => queryCondition.MerchantList.Contains(c.MerchantID));
                }
            }
			return query;
		}
		protected override IQueryable<UserInfo> SetOrder(QueryConditionInfo<UserInfo> queryCondition, IQueryable<UserInfo> query)
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
                            query = query.OrderBy(c => c.MerchantID);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.MerchantID);
                        }
                    }
					if (item.FieldName == "UserName")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.UserName);
						}
						else
						{
							query = query.OrderByDescending(c => c.UserName);
						}
					}
					if (item.FieldName == "Password")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.Password);
						}
						else
						{
							query = query.OrderByDescending(c => c.Password);
						}
					}
					if (item.FieldName == "FullName")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.FullName);
						}
						else
						{
							query = query.OrderByDescending(c => c.FullName);
						}
					}
					if (item.FieldName == "Sex")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.Sex);
						}
						else
						{
							query = query.OrderByDescending(c => c.Sex);
						}
					}
					if (item.FieldName == "IDCard")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.IDCard);
						}
						else
						{
							query = query.OrderByDescending(c => c.IDCard);
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
					if (item.FieldName == "IsAdmin")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.IsAdmin);
						}
						else
						{
							query = query.OrderByDescending(c => c.IsAdmin);
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
		public void ChangeStatus(UserInfo entity)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserInfo> objectSet = entities.CreateObjectSet<UserInfo>();
				//UserInfo UserInfo = objectSet.FirstOrDefault(c => c.ID == entity.ID);
				//if (UserInfo != null)
				//{
				//    UserInfo.EditUser = entity.EditUser;
				//    UserInfo.EditDate = DateTime.Now;
				//    UserInfo.Status = entity.Status;
				//    entities.SaveChanges();
				//}
				objectSet.Update(
					c => c.ID == entity.ID,
					c => new UserInfo
					{
						EditUser = entity.EditUser,
						EditDate = DateTime.Now,
						Status = entity.Status
					});
				entities.SaveChanges();
			}
		}
		internal List<UserInfo> GetUserListByMerchantID(int merchantID)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserInfo> objectSet = entities.CreateObjectSet<UserInfo>();
				return objectSet.Where(c => c.MerchantID == merchantID).ToList();
			}
		}


		internal static UserInfo GetByUserName(string userName)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserInfo> objectSet = entities.CreateObjectSet<UserInfo>();
				return objectSet.FirstOrDefault(c => c.UserName == userName && c.Status != -1);
			}
		}

		internal void DeleteByMerchantID(int id, string editUser)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserInfo> objectSet = entities.CreateObjectSet<UserInfo>();
				UserInfo UserInfo = objectSet.FirstOrDefault(c => c.MerchantID == id);
				if (UserInfo != null)
				{
					UserInfo.EditUser = editUser;
					UserInfo.EditDate = DateTime.Now;
					UserInfo.Status = -1;
					entities.SaveChanges();
				}
			}
		}

		internal void BatchDeleteByMerchantID(List<int> keyList, string editUser)
		{
			BatchChangeStatusByMerchantID(keyList, -1, editUser);
		}

		internal void BatchChangeStatus(List<int> idList, int status, string editUser)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserInfo> objectSet = entities.CreateObjectSet<UserInfo>();
				List<UserInfo> entityList = objectSet.Where(c => idList.Contains(c.ID)).ToList();
				foreach (var item in entityList)
				{
					item.EditUser = editUser;
					item.EditDate = DateTime.Now;
					item.Status = status;
				}
				entities.SaveChanges();
			}
		}

		internal void BatchChangeStatusByMerchantID(List<int> idList, int status, string editUser)
		{
			using (SecurityEntities entities = new SecurityEntities())
			{
				ObjectSet<UserInfo> objectSet = entities.CreateObjectSet<UserInfo>();
				List<UserInfo> entityList = objectSet.Where(c => idList.Contains(c.MerchantID)).ToList();
				foreach (var item in entityList)
				{
					item.EditUser = editUser;
					item.EditDate = DateTime.Now;
					item.Status = status;
				}
				entities.SaveChanges();
			}
		}
	}
}
