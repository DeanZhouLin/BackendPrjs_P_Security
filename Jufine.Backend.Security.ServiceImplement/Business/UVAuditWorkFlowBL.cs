
using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.Business
{
	public class UVAuditWorkFlowBL :IUVAuditWorkFlowService
	{
       
		public UVAuditWorkFlow Get(Int32 id)
        {
            try
            {
                return UVAuditWorkFlowDA.DAO.Get(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public void Delete(Int32 id)
        {
            try
            {
                 UVAuditWorkFlowDA.DAO.Delete(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public void BatchDelete(List<Int32> keyList)
       {
           try
            {
                UVAuditWorkFlowDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<UVAuditWorkFlow> GetAll()
        {
            try
            {
                return UVAuditWorkFlowDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(UVAuditWorkFlow uVAuditWorkFlow)
        {
            try
            {
                UVAuditWorkFlowDA.DAO.Create(uVAuditWorkFlow);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(UVAuditWorkFlow uVAuditWorkFlow)
        {
            try
            {
                UVAuditWorkFlowDA.DAO.Update(uVAuditWorkFlow);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<UVAuditWorkFlow> Query(QueryConditionInfo<UVAuditWorkFlow> queryCondition)
       {
           try
            {
               return UVAuditWorkFlowDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }
    
       

    }
}
