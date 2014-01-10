using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IUVUserMerchantService: IServiceBase
	{


        UVUserMerchant Get(Int32 id);
        List<UVUserMerchant> GetAll();
        void Create(UVUserMerchant uvUserMerchant);
        void Update(UVUserMerchant uvUserMerchant);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<UVUserMerchant> Query(QueryConditionInfo<UVUserMerchant> queryCondition);

        List<UVUserMerchant> GetByUserID(int key);

    }
}
