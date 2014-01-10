using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IRoleResourceService: IServiceBase
	{
		
        
        RoleResource Get(Int32 id);
        List<RoleResource> GetAll();
        void Create(RoleResource roleResource);
        void Update(RoleResource roleResource);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<RoleResource> Query(QueryConditionInfo<RoleResource> queryCondition);

        void UpdateAuthorityByRole(List<int> list, int roleID, string editUser);

        List<RoleResource> GetByRoleID(int key);
    }
}
