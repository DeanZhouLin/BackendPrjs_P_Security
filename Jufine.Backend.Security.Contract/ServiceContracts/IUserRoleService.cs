using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IUserRoleService: IServiceBase
	{
		
        
        UserRole Get(Int32 id);
        List<UserRole> GetAll();
        void Create(UserRole userRole);
        void Update(UserRole userRole);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<UserRole> Query(QueryConditionInfo<UserRole> queryCondition);

        List<UserRole> GetByUserID(int key, int merchantID);

        void UpdateRoleByUser(List<int> list, int userID, int merchantID, string editUser);

        List<int> GetRoleResourceID(int key, int merchantID);
    }
}
