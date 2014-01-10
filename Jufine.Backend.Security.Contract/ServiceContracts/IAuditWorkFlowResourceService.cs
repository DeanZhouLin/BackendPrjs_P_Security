using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
    public interface IAuditWorkFlowResourceService : IServiceBase
    {
        AuditWorkFlowResource Get(Int32 id);
        List<AuditWorkFlowResource> GetAll();
        void Create(AuditWorkFlowResource auditWorkFlowResource);
        void Update(AuditWorkFlowResource auditWorkFlowResource);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<AuditWorkFlowResource> Query(QueryConditionInfo<AuditWorkFlowResource> queryCondition);
        List<TAuditWorkFlowResource> GetTAuditWorkFlowResources(int resourceID);
        void AddAuditUserForAuditWorkFlow(int resourceID, string userName, int auditLevel, string createUser, out string msg, out int msgNumber);
        void DeleteAuditUserForAuditWorkFlow(int resourceID, string userName, string editUser, out string msg, out int msgNumber);
    }
}
