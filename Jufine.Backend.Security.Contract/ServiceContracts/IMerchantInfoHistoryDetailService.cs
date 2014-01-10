using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
    public interface IMerchantInfoHistoryDetailService : IServiceBase
    {

        MerchantInfoHistoryDetail Get(Int32 id);
        List<MerchantInfoHistoryDetail> GetAll();
        List<int?> GetAllDistinctMerchantIDList();
        void Create(MerchantInfoHistoryDetail merchant);
        void Update(MerchantInfoHistoryDetail merchant);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<MerchantInfoHistoryDetail> Query(QueryConditionInfo<MerchantInfoHistoryDetail> queryCondition);
    }
}
