using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IUserService: IServiceBase
	{
        List<UserInfo> GetUserListByMerchantID(Int32 merchantID);
        void BatchChangeStatusByMerchantID(List<int> idList, int status, string editUser);
        
        UserInfo Get(Int32 id);
        List<UserInfo> GetAll();
        void Create(UserInfo user);
        void Update(UserInfo user);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<UserInfo> Query(QueryConditionInfo<UserInfo> queryCondition);

        UserInfo Login(string userName, string password);

        UserInfo GetByUserName(string userName);

        void BatchChangeStatus(List<int> idList,int status,string editUser);
    }
}
