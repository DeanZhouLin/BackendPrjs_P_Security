using System.Linq;
using Com.BaseLibrary.Service;
using Com.BaseLibrary.Entity;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess 
{
	internal class QueryLogDA: DataBase<QueryLog, SecurityEntities>
	{
        internal static QueryLogDA DAO = new QueryLogDA();
		private QueryLogDA(){ }
        protected override void AttachValue(QueryLog newEntity, QueryLog oldEntity)
		{
            oldEntity.UserID = newEntity.UserID;
            oldEntity.SqlText = newEntity.SqlText;
            oldEntity.CreateUser = newEntity.CreateUser;
            oldEntity.CreateDate = newEntity.CreateDate;
            oldEntity.EditUser = newEntity.EditUser;
            oldEntity.EditDate = newEntity.EditDate;
		}
        protected override IQueryable<QueryLog> SetWhereClause(QueryConditionInfo<QueryLog> queryCondition, IQueryable<QueryLog> query)
		{
                        if(queryCondition.Condtion.ID > 0)
                        {
                            query = query.Where(c => c.ID==queryCondition.Condtion.ID);
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.UserID ))
                        {
                           query = query.Where(c => c.UserID.StartsWith(queryCondition.Condtion.UserID));
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.SqlText ))
                        {
                           query = query.Where(c => c.SqlText.StartsWith(queryCondition.Condtion.SqlText));
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
        protected override IQueryable<QueryLog> SetOrder(QueryConditionInfo<QueryLog> queryCondition, IQueryable<QueryLog> query)
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
					if (item.FieldName == "SqlText")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.SqlText);
						}
						else
						{
							query = query.OrderByDescending(c => c.SqlText);
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
    }
}
