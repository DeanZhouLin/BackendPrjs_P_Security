using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IRoleService: IServiceBase
	{
		
        
        RoleInfo Get(Int32 id);
        List<RoleInfo> GetAll();
        bool Create(RoleInfo role);
        bool Update(RoleInfo role);
        void Delete(Int32 id,string editUser);
        void BatchDelete(List<Int32> keyList, string editUser);
        QueryResultInfo<RoleInfo> Query(QueryConditionInfo<RoleInfo> queryCondition);

        List<RoleInfo> GetAllActive();
    }
}
