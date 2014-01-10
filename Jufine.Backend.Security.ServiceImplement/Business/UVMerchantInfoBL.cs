using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataAccess;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;

namespace Jufine.Backend.Security.Business
{
    class UVMerchantInfoBL : IUVMerchantInfoService
    {
        public UVMerchantInfo GetUVMerchantInfo(int id,string currentUserName = "")
        {
            try
            {
                var res = UVMerchantInfoDA.DAO.GetUVMerchantInfo(id);
                if (res != null && !string.IsNullOrEmpty(currentUserName))
                {
                    res.IsSelfEdit = res.EditUser == currentUserName;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }


        public QueryResultInfo<UVMerchantInfo> QueryUVMerchantInfo(QueryConditionInfo<UVMerchantInfo> queryCondition)
        {
            throw new NotImplementedException();
        }


        public string MerchantInfoChangeStatus(int id, string auditUser, string reason, int status)
        {
            try
            {
             return   UVMerchantInfoDA.DAO.MerchantInfoChangeStatus(id, auditUser, reason, status);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }
    }
}
