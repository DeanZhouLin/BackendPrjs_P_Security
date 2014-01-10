using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IUVAuditWorkFlowService: IServiceBase
	{
		
        
        UVAuditWorkFlow Get(Int32 id);
        List<UVAuditWorkFlow> GetAll();
        void Create(UVAuditWorkFlow uVAuditWorkFlow);
        void Update(UVAuditWorkFlow uVAuditWorkFlow);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<UVAuditWorkFlow> Query(QueryConditionInfo<UVAuditWorkFlow> queryCondition);
    }
}
