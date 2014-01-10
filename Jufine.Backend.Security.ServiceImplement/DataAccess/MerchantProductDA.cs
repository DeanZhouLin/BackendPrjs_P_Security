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
	internal class MerchantProductDA: DataBase<MerchantProduct, SecurityEntities>
	{
        internal static MerchantProductDA DAO = new MerchantProductDA();
		private MerchantProductDA(){ }
        protected override void AttachValue(MerchantProduct newEntity, MerchantProduct oldEntity)
		{
            oldEntity.MerchantID = newEntity.MerchantID;
            oldEntity.CommonNO = newEntity.CommonNO;
            oldEntity.Type = newEntity.Type;
            oldEntity.Status = newEntity.Status;
            oldEntity.Memo = newEntity.Memo;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
		}
        protected override IQueryable<MerchantProduct> SetWhereClause(QueryConditionInfo<MerchantProduct> queryCondition, IQueryable<MerchantProduct> query)
		{
                        if(queryCondition.Condtion.ID > 0)
                        {
                            query = query.Where(c => c.ID==queryCondition.Condtion.ID);
                        }
                        if(queryCondition.Condtion.MerchantID > 0)
                        {
                            query = query.Where(c => c.MerchantID==queryCondition.Condtion.MerchantID);
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.CommonNO ))
                        {
                           query = query.Where(c => c.CommonNO.StartsWith(queryCondition.Condtion.CommonNO));
                        }
                        if(queryCondition.Condtion.Type > 0)
                        {
                            query = query.Where(c => c.Type==queryCondition.Condtion.Type);
                        }
                        if(queryCondition.Condtion.Status > 0)
                        {
                            query = query.Where(c => c.Status==queryCondition.Condtion.Status);
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.Memo ))
                        {
                           query = query.Where(c => c.Memo.StartsWith(queryCondition.Condtion.Memo));
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
        protected override IQueryable<MerchantProduct> SetOrder(QueryConditionInfo<MerchantProduct> queryCondition, IQueryable<MerchantProduct> query)
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
					if (item.FieldName == "CommonNO")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.CommonNO);
						}
						else
						{
							query = query.OrderByDescending(c => c.CommonNO);
						}
					}
					if (item.FieldName == "Type")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.Type);
						}
						else
						{
							query = query.OrderByDescending(c => c.Type);
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
					if (item.FieldName == "Memo")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.Memo);
						}
						else
						{
							query = query.OrderByDescending(c => c.Memo);
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
        public void ChangeStatus(MerchantProduct entity)
		{
			using ( SecurityEntities entities = new  SecurityEntities())
			{
				ObjectSet<MerchantProduct> objectSet = entities.CreateObjectSet<MerchantProduct>();
				MerchantProduct MerchantProduct = objectSet.FirstOrDefault(c => c.ID == entity.ID);
				if (MerchantProduct != null)
				{
					MerchantProduct.EditUser = entity.EditUser;
					MerchantProduct.EditDate = DateTime.Now;
					MerchantProduct.Status = entity.Status;
					entities.SaveChanges();
				}
			}
		}
    }
}
