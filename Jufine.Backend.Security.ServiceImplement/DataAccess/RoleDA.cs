using System;
using System.Linq;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess 
{
	internal class RoleDA: DataBase<RoleInfo, SecurityEntities>
	{
        internal static RoleDA DAO = new RoleDA();
		private RoleDA(){ }
        protected override void AttachValue(RoleInfo newEntity, RoleInfo oldEntity)
		{
            oldEntity.RoleName = newEntity.RoleName;
            oldEntity.DisplayName = newEntity.DisplayName;
            oldEntity.DisplayOrder = newEntity.DisplayOrder;
            oldEntity.Status = newEntity.Status;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
		}
        protected override IQueryable<RoleInfo> SetWhereClause(QueryConditionInfo<RoleInfo> queryCondition, IQueryable<RoleInfo> query)
		{
                        if(queryCondition.Condtion.ID > 0)
                        {
                            query = query.Where(c => c.ID==queryCondition.Condtion.ID);
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.RoleName ))
                        {
                           query = query.Where(c => c.RoleName.StartsWith(queryCondition.Condtion.RoleName));
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.DisplayName ))
                        {
                           query = query.Where(c => c.DisplayName.StartsWith(queryCondition.Condtion.DisplayName));
                        }
                        if(queryCondition.Condtion.DisplayOrder > 0)
                        {
                            query = query.Where(c => c.DisplayOrder==queryCondition.Condtion.DisplayOrder);
                        }
                        if (queryCondition.Condtion.Status >= 0)
                        {
                            query = query.Where(c => c.Status == queryCondition.Condtion.Status);
                        }
                        else
                        {
                            query = query.Where(c => c.Status !=-1);
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.CreateUser ))
                        {
                           query = query.Where(c => c.CreateUser.StartsWith(queryCondition.Condtion.CreateUser));
                        }
                        if(queryCondition.Condtion.CreateDate!=null)
                        {
                           query = query.Where(c => c.CreateDate>queryCondition.Condtion.CreateDate);
                        }
                        if(queryCondition.Condtion.CreateDateTo!=null)
                        {
                           query = query.Where(c => c.CreateDate <=queryCondition.Condtion.CreateDateTo);
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.EditUser ))
                        {
                           query = query.Where(c => c.EditUser.StartsWith(queryCondition.Condtion.EditUser));
                        }
                        if(queryCondition.Condtion.EditDate!=null)
                        {
                           query = query.Where(c => c.EditDate>queryCondition.Condtion.EditDate);
                        }
                        if(queryCondition.Condtion.EditDateTo!=null)
                        {
                           query = query.Where(c => c.EditDate <=queryCondition.Condtion.EditDateTo);
                        }
                return query;
		}
        protected override IQueryable<RoleInfo> SetOrder(QueryConditionInfo<RoleInfo> queryCondition, IQueryable<RoleInfo> query)
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
					if (item.FieldName == "RoleName")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.RoleName);
						}
						else
						{
							query = query.OrderByDescending(c => c.RoleName);
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
					if (item.FieldName == "DisplayOrder")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.DisplayOrder);
						}
						else
						{
							query = query.OrderByDescending(c => c.DisplayOrder);
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
				}
			}
			else
			{
				query = query.OrderByDescending(c => c.ID);
			}
            return query;
		}
        public void ChangeStatus(RoleInfo entity)
		{
			using ( SecurityEntities entities = new  SecurityEntities())
			{
				ObjectSet<RoleInfo> objectSet = entities.CreateObjectSet<RoleInfo>();
				RoleInfo RoleInfo = objectSet.FirstOrDefault(c => c.ID == entity.ID);
				if (RoleInfo != null)
				{
					RoleInfo.EditUser = entity.EditUser;
					RoleInfo.EditDate = DateTime.Now;
					RoleInfo.Status = entity.Status;
					entities.SaveChanges();
				}
			}
		}

        internal RoleInfo GetByRoleName(string roleName,int roleID)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<RoleInfo> objectSet = entities.CreateObjectSet<RoleInfo>();
                return objectSet.FirstOrDefault(c => c.RoleName == roleName&&c.Status!=-1&&c.ID!=roleID);
            }
        }

        internal void LogicDelete(int id,string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<RoleInfo> objectSet = entities.CreateObjectSet<RoleInfo>();
                RoleInfo RoleInfo = objectSet.FirstOrDefault(c => c.ID == id);
                if (RoleInfo != null)
                {
                    RoleInfo.EditUser = editUser;
                    RoleInfo.EditDate = DateTime.Now;
                    RoleInfo.Status = -1;
                    entities.SaveChanges();
                }
            }
        }

        internal void BatchLogicDelete(List<int> keyList, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<RoleInfo> objectSet = entities.CreateObjectSet<RoleInfo>();
                List<RoleInfo> roleList= objectSet.Where(c =>keyList.Contains(c.ID)).ToList();
                foreach (RoleInfo role in roleList)
                {
                    role.EditUser = editUser;
                    role.EditDate = DateTime.Now;
                    role.Status = -1;
                }
                entities.SaveChanges();
            }
        }

        internal List<RoleInfo> GetAllActive()
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<RoleInfo> objectSet = entities.CreateObjectSet<RoleInfo>();
                return objectSet.Where(c => c.Status == 1).ToList();
            }
        }
    }
}
