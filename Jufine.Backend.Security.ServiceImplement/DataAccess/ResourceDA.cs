using System;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Data;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.DataAccess 
{
	internal class ResourceDA: DataBase<Resource, SecurityEntities>
	{
        internal static ResourceDA DAO = new ResourceDA();
		private ResourceDA(){ }
        protected override void AttachValue(Resource newEntity, Resource oldEntity)
		{
            oldEntity.ParentID = newEntity.ParentID;
            oldEntity.ResourceName = newEntity.ResourceName;
            oldEntity.DisplayName = newEntity.DisplayName;
            oldEntity.DisplayOrder = newEntity.DisplayOrder;
            oldEntity.ResourceAddress = newEntity.ResourceAddress;
            oldEntity.ShowInMenu = newEntity.ShowInMenu;
            oldEntity.ResourceType = newEntity.ResourceType;
            oldEntity.Path = newEntity.Path;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
            oldEntity.Status = newEntity.Status;
		}
        protected override IQueryable<Resource> SetWhereClause(QueryConditionInfo<Resource> queryCondition, IQueryable<Resource> query)
		{
                        if(queryCondition.Condtion.ID > 0)
                        {
                            query = query.Where(c => c.ID==queryCondition.Condtion.ID);
                        }
                        if(queryCondition.Condtion.ParentID > 0)
                        {
                            query = query.Where(c => c.ParentID==queryCondition.Condtion.ParentID);
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.ResourceName ))
                        {
                           query = query.Where(c => c.ResourceName.StartsWith(queryCondition.Condtion.ResourceName));
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.DisplayName ))
                        {
                           query = query.Where(c => c.DisplayName.StartsWith(queryCondition.Condtion.DisplayName));
                        }
                        if(queryCondition.Condtion.DisplayOrder > 0)
                        {
                            query = query.Where(c => c.DisplayOrder==queryCondition.Condtion.DisplayOrder);
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.ResourceAddress ))
                        {
                           query = query.Where(c => c.ResourceAddress.StartsWith(queryCondition.Condtion.ResourceAddress));
                        }
                        if(queryCondition.Condtion.ShowInMenu > 0)
                        {
                            query = query.Where(c => c.ShowInMenu==queryCondition.Condtion.ShowInMenu);
                        }
                        if(queryCondition.Condtion.ResourceType > 0)
                        {
                            query = query.Where(c => c.ResourceType==queryCondition.Condtion.ResourceType);
                        }
                        if (!string.IsNullOrEmpty(queryCondition.Condtion.Path))
                        {
                            query = query.Where(c => c.Path.StartsWith(queryCondition.Condtion.Path));
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
                        if(queryCondition.Condtion.Status > 0)
                        {
                            query = query.Where(c => c.Status==queryCondition.Condtion.Status);
                        }
                return query;
		}
        protected override IQueryable<Resource> SetOrder(QueryConditionInfo<Resource> queryCondition, IQueryable<Resource> query)
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
					if (item.FieldName == "ParentID")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.ParentID);
						}
						else
						{
							query = query.OrderByDescending(c => c.ParentID);
						}
					}
					if (item.FieldName == "ResourceName")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.ResourceName);
						}
						else
						{
							query = query.OrderByDescending(c => c.ResourceName);
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
					if (item.FieldName == "ResourceAddress")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.ResourceAddress);
						}
						else
						{
							query = query.OrderByDescending(c => c.ResourceAddress);
						}
					}
					if (item.FieldName == "ShowInMenu")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.ShowInMenu);
						}
						else
						{
							query = query.OrderByDescending(c => c.ShowInMenu);
						}
					}
					if (item.FieldName == "ResourceType")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.ResourceType);
						}
						else
						{
							query = query.OrderByDescending(c => c.ResourceType);
						}
					}
                    if (item.FieldName == "Path")
                    {
                        if (item.OrderDirection == OrderDirection.ASC)
                        {
                            query = query.OrderBy(c => c.Path);
                        }
                        else
                        {
                            query = query.OrderByDescending(c => c.Path);
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
				}
			}
			else
			{
				query = query.OrderByDescending(c => c.ID);
			}
            return query;
		}
        public void ChangeStatus(Resource entity)
		{
			using ( SecurityEntities entities = new  SecurityEntities())
			{
				ObjectSet<Resource> objectSet = entities.CreateObjectSet<Resource>();
				Resource Resource = objectSet.FirstOrDefault(c => c.ID == entity.ID);
				if (Resource != null)
				{
					Resource.EditUser = entity.EditUser;
					Resource.EditDate = DateTime.Now;
					Resource.Status = entity.Status;
					entities.SaveChanges();
				}
			}
		}

        internal bool IsResourceNameExist(Resource resource)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<Resource> objectSet = entities.CreateObjectSet<Resource>();
                return objectSet.FirstOrDefault(c => c.ID!=resource.ID && c.ResourceName == resource.ResourceName) != null;
                
            }
        }

        internal int GetByResourceName(string resourceName)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<Resource> objectSet = entities.CreateObjectSet<Resource>();
                Resource resource=objectSet.FirstOrDefault(c => c.ResourceName == resourceName);
                return resource.ID;

            }
        }

        internal void ChangeStatus(int id ,int status)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<Resource> objectSet = entities.CreateObjectSet<Resource>();
                Resource resource = objectSet.FirstOrDefault(c => c.ID == id);
                if (resource != null)
                {
                    resource.Status = status;
                    entities.SaveChanges();
                }
            }
        }
    }
}
