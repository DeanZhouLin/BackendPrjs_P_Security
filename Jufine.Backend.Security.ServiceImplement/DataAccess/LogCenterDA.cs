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
using System.Data;

namespace Jufine.Backend.Security.DataAccess 
{
	internal class LogCenterDA: DataBaseUseLongKey<LogCenter, SecurityEntities>
	{
        internal static LogCenterDA DAO = new LogCenterDA();
		private LogCenterDA(){ }
        protected override void AttachValue(LogCenter newEntity, LogCenter oldEntity)
		{
            oldEntity.ApplicationName = newEntity.ApplicationName;
            oldEntity.Module = newEntity.Module;
            oldEntity.LogType = newEntity.LogType;
            oldEntity.Title = newEntity.Title;
            oldEntity.Detail = newEntity.Detail;
            oldEntity.LogTime = newEntity.LogTime;
		}
        protected override IQueryable<LogCenter> SetWhereClause(QueryConditionInfo<LogCenter> queryCondition, IQueryable<LogCenter> query)
		{
                        if(queryCondition.Condtion.ID > 0)
                        {
                            query = query.Where(c => c.ID==queryCondition.Condtion.ID);
                        }
                        if (queryCondition.Condtion.ApplicationName!="全部") 
                        {
                            query = query.Where(c => c.ApplicationName.Equals(queryCondition.Condtion.ApplicationName == null ? "" : queryCondition.Condtion.ApplicationName));
                        }
                        if (queryCondition.Condtion.Module != "全部")
                        {
                            query = query.Where(c => c.Module.Equals(queryCondition.Condtion.Module == null ? "" : queryCondition.Condtion.Module));
                        }
                        if (queryCondition.Condtion.LogType != "全部")
                        {
                           query = query.Where(c => c.LogType.Equals(queryCondition.Condtion.LogType));
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.Title ))
                        {
                           query = query.Where(c => c.Title.StartsWith(queryCondition.Condtion.Title));
                        }
                        if(!string.IsNullOrEmpty( queryCondition.Condtion.Detail ))
                        {
                           query = query.Where(c => c.Detail.StartsWith(queryCondition.Condtion.Detail));
                        }
                        if(queryCondition.Condtion.LogTime!=null)
                        {
                           query = query.Where(c => c.LogTime>queryCondition.Condtion.LogTime);
                        }
                        if(queryCondition.Condtion.LogTimeTo!=null)
                        {
                           query = query.Where(c => c.LogTime <=queryCondition.Condtion.LogTimeTo);
                        }
                return query;
		}
        protected override IQueryable<LogCenter> SetOrder(QueryConditionInfo<LogCenter> queryCondition, IQueryable<LogCenter> query)
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
					if (item.FieldName == "ApplicationName")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.ApplicationName);
						}
						else
						{
							query = query.OrderByDescending(c => c.ApplicationName);
						}
					}
					if (item.FieldName == "Module")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.Module);
						}
						else
						{
							query = query.OrderByDescending(c => c.Module);
						}
					}
					if (item.FieldName == "LogType")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.LogType);
						}
						else
						{
							query = query.OrderByDescending(c => c.LogType);
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
					if (item.FieldName == "Detail")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.Detail);
						}
						else
						{
							query = query.OrderByDescending(c => c.Detail);
						}
					}
					if (item.FieldName == "LogTime")
					{
						if (item.OrderDirection == OrderDirection.ASC)
						{
							query = query.OrderBy(c => c.LogTime);
						}
						else
						{
							query = query.OrderByDescending(c => c.LogTime);
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
        internal static DataSet Ddlname(string connectionString, string sql)
        {
         return  SqlHelper.ExecuteDataset(connectionString, CommandType.Text, sql);
        }
    
    }
}
