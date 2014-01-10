
using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.Business
{
    public class AuditWorkFlowResourceBL : IAuditWorkFlowResourceService
    {

        public AuditWorkFlowResource Get(Int32 id)
        {
            try
            {
                return AuditWorkFlowResourceDA.DAO.Get(id);
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
                AuditWorkFlowResourceDA.DAO.Delete(id);
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
                AuditWorkFlowResourceDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<AuditWorkFlowResource> GetAll()
        {
            try
            {
                return AuditWorkFlowResourceDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(AuditWorkFlowResource auditWorkFlowResource)
        {
            try
            {
                AuditWorkFlowResourceDA.DAO.Create(auditWorkFlowResource);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(AuditWorkFlowResource auditWorkFlowResource)
        {
            try
            {
                AuditWorkFlowResourceDA.DAO.Update(auditWorkFlowResource);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public QueryResultInfo<AuditWorkFlowResource> Query(QueryConditionInfo<AuditWorkFlowResource> queryCondition)
        {
            try
            {
                return AuditWorkFlowResourceDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<TAuditWorkFlowResource> GetTAuditWorkFlowResources(int resourceID)
        {
            try
            {
                return AuditWorkFlowResourceDA.DAO.GetTAuditWorkFlowResources(resourceID);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void AddAuditUserForAuditWorkFlow(int resourceID, string userName, int auditLevel, string createUser, out string msg, out int msgNumber)
        {
            try
            {
                AuditWorkFlowResourceDA.DAO.AddAuditUserForAuditWorkFlow(resourceID, userName, auditLevel, createUser, out msg, out msgNumber);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void DeleteAuditUserForAuditWorkFlow(int resourceID, string userName, string editUser, out string msg, out int msgNumber)
        {
            try
            {
                AuditWorkFlowResourceDA.DAO.DeleteAuditUserForAuditWorkFlow(resourceID, userName, editUser, out msg, out msgNumber);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }


    }
}
