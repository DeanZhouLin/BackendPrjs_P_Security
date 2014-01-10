using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IUserResourceService: IServiceBase
	{
		
        
        UserResource Get(Int32 id);
        List<UserResource> GetAll();
        void Create(UserResource userResource);
        void Update(UserResource userResource);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<UserResource> Query(QueryConditionInfo<UserResource> queryCondition);

        List<UserResource> GetByUserID(int id,int merchantID);


        void UpdateAuthorityByUser(List<int> list, int userID,int merchantID,string editUser);
    }
}
