using Com.BaseLibrary.Contract;
using Com.BaseLibrary.Entity;
using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
    public interface  IUVMerchantInfoService : IServiceBase
    {
        UVMerchantInfo GetUVMerchantInfo(int id,string currentUserName = "");
        string MerchantInfoChangeStatus(int id, string auditUser, string reason, int status);
        QueryResultInfo<UVMerchantInfo> QueryUVMerchantInfo(QueryConditionInfo<UVMerchantInfo> queryCondition);
    }
}
