using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.BaseLibrary.Common.Security;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IMerchantService: IServiceBase
	{
		
        
        MerchantInfo Get(Int32 id);
        UVMerchantInfo GetUVMerchantInfo(int id, string currentUserName = "");
        UVMerchantInfo GetUVMerchantInfo(int id, int merchantId, string currentUserName = "");
	    UVMerchantInfo GetUVMerchantInfoByMerchantID(int merchantId, string currentUserName = "");
	    QueryResultInfo<UVMerchantInfo> QueryUVMerchantInfo(QueryConditionInfo<UVMerchantInfo> queryCondition,IUser currentUser, string ctlName, int currentPageID, List<Com.BaseLibrary.Common.Security.Resource> pageControlResources);

        MerchantInfo GetByName(string merchantName);

        List<MerchantInfo> GetAll();
        bool Create(MerchantInfo merchant,int userID);

        void CreateHistoryDetail(MerchantInfoHistoryDetail merchantInfoHistoryDetail);
	    void UpdateHistoryDetail(MerchantInfoHistoryDetail merchantInfoHistoryDetail);
        MerchantInfoHistoryDetail CopyToMerchantInfoHistoryDetail(MerchantInfo source, MerchantInfoHistoryDetail target);
	    bool Update(MerchantInfo merchant);
        void Delete(Int32 id,string editUser);
        void BatchDelete(List<Int32> keyList,string editUser);
        QueryResultInfo<MerchantInfo> Query(QueryConditionInfo<MerchantInfo> queryCondition);
        void BatchChangeStatus(List<int> idList, int status, string editUser);
        //更改状态并且插入历史记录表
        void BatchChangeStatusMerchant(List<int> idList, int status, string editUser,int fromHistory=0);
	    void ChangeStatusMerchant(int id, int status, string editUser, int fromHistory = 0);
        //删除商家
        void BatchDeleteMerchant(List<int> keyList, string editUser);
        List<MerchantInfo> GetActiveMerchant();
	    List<MerchantInfo> GetAllMerchant();
	}
}
