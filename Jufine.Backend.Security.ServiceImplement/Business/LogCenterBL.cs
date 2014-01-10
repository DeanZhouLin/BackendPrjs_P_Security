
using System;
using System.Collections.Generic;

using Com.BaseLibrary.Utility;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;
using System.Data;

namespace Jufine.Backend.Security.Business 
{
	public class LogCenterBL :ILogCenterService
	{
       
		public LogCenter Get(Int64 id)
        {
            try
            {
                return LogCenterDA.DAO.Get(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public void Delete(Int64 id)
        {
            try
            {
                 LogCenterDA.DAO.Delete(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public void BatchDelete(List<Int64> keyList)
       {
           try
            {
                LogCenterDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<LogCenter> GetAll()
        {
            try
            {
                return LogCenterDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(LogCenter logCenter)
        {
            try
            {
                LogCenterDA.DAO.Create(logCenter);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(LogCenter logCenter)
        {
            try
            {
                LogCenterDA.DAO.Update(logCenter);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<LogCenter> Query(QueryConditionInfo<LogCenter> queryCondition)
       {
           try
            {
               return LogCenterDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }

       public DataSet Ddlname(string connectionString, string sql)
       {
           return LogCenterDA.Ddlname(connectionString, sql);
       }

    }
}
