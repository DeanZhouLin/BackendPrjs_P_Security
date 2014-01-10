using System;
using System.Linq;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess 
{
	internal class MerchantAdditionalCertificateDA: DataBase<MerchantAdditionalCertificate, SecurityEntities>
	{
        internal static MerchantAdditionalCertificateDA DAO = new MerchantAdditionalCertificateDA();
		private MerchantAdditionalCertificateDA(){ }
        protected override void AttachValue(MerchantAdditionalCertificate newEntity, MerchantAdditionalCertificate oldEntity)
		{
            oldEntity.MerchantID = newEntity.MerchantID;
            oldEntity.ImangeName = newEntity.ImangeName;
            oldEntity.Title = newEntity.Title;
            oldEntity.DisplayOrder = newEntity.DisplayOrder;
            oldEntity.Status = newEntity.Status;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
		}
        protected override IQueryable<MerchantAdditionalCertificate> SetWhereClause(QueryConditionInfo<MerchantAdditionalCertificate> queryCondition, IQueryable<MerchantAdditionalCertificate> query)
		{
                        if(queryCondition.Condtion.ID > 0)
                        {
                            query = query.Where(c => c.ID==queryCondition.Condtion.ID);
                        }
                        if(queryCondition.Condtion.MerchantID > 0)
                        {
                            query = query.Where(c => c.MerchantID==queryCondition.Condtion.MerchantID);
                        }
                        if (!string.IsNullOrEmpty(queryCondition.Condtion.ImangeName))
                        {
                            query = query.Where(c => c.Title.StartsWith(queryCondition.Condtion.ImangeName));
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.Title ))
                        {
                           query = query.Where(c => c.Title.StartsWith(queryCondition.Condtion.Title));
                        }
                        if(queryCondition.Condtion.DisplayOrder > 0)
                        {
                            query = query.Where(c => c.DisplayOrder==queryCondition.Condtion.DisplayOrder);
                        }
                        if(queryCondition.Condtion.Status > 0)
                        {
                            query = query.Where(c => c.Status==queryCondition.Condtion.Status);
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
        protected override IQueryable<MerchantAdditionalCertificate> SetOrder(QueryConditionInfo<MerchantAdditionalCertificate> queryCondition, IQueryable<MerchantAdditionalCertificate> query)
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
					if (item.FieldName == "ImangeName")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.ImangeName);
						}
						else
						{
							query = query.OrderByDescending(c => c.ImangeName);
						}
					}
					if (item.FieldName == "Title")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.Title);
						}
						else
						{
							query = query.OrderByDescending(c => c.Title);
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
        public void ChangeStatus(MerchantAdditionalCertificate entity)
		{
			using ( SecurityEntities entities = new  SecurityEntities())
			{
				ObjectSet<MerchantAdditionalCertificate> objectSet = entities.CreateObjectSet<MerchantAdditionalCertificate>();
				MerchantAdditionalCertificate MerchantAdditionalCertificate = objectSet.FirstOrDefault(c => c.ID == entity.ID);
				if (MerchantAdditionalCertificate != null)
				{
					MerchantAdditionalCertificate.EditUser = entity.EditUser;
					MerchantAdditionalCertificate.EditDate = DateTime.Now;
					MerchantAdditionalCertificate.Status = entity.Status;
					entities.SaveChanges();
				}
			}
		}

        internal List<MerchantAdditionalCertificate> GetByMerchantID(int merchantID)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantAdditionalCertificate> objectSet = entities.CreateObjectSet<MerchantAdditionalCertificate>();
                return objectSet.Where(c => c.MerchantID == merchantID).ToList();
            }
        }

        internal void Delete(int merchantID, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantAdditionalCertificate> objectSet = entities.CreateObjectSet<MerchantAdditionalCertificate>();
                MerchantAdditionalCertificate info = objectSet.FirstOrDefault(c => c.MerchantID == merchantID);
                if (info != null)
                {
                    info.EditUser = editUser;
                    info.EditDate = DateTime.Now;
                    info.Status = -1;
                    entities.SaveChanges();
                }
            }
        }

        internal void BatchDelete(List<int> merchantIDList, string editUser)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<MerchantAdditionalCertificate> objectSet = entities.CreateObjectSet<MerchantAdditionalCertificate>();
                List<MerchantAdditionalCertificate> entityList = objectSet.Where(c => merchantIDList.Contains(c.MerchantID)).ToList();
                foreach (var item in entityList)
                {
                    item.EditUser = editUser;
                    item.EditDate = DateTime.Now;
                    item.Status = -1;
                }
                entities.SaveChanges();
            }
        }
    }
}
