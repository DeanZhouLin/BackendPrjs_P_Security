using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IUserMerchantService: IServiceBase
	{
		
        
        UserMerchant Get(Int32 id);
        List<UserMerchant> GetAll();
        void Create(UserMerchant userMerchant);
        void Update(UserMerchant userMerchant);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<UserMerchant> Query(QueryConditionInfo<UserMerchant> queryCondition);

        List<UserMerchant> GetByUserID(int key);

        void UpdateMerchantByUser(List<int> list, int userID,string editUser);
    }
}
