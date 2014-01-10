
using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.Business
{
    public class MerchantInfoHistoryDetailBL : IMerchantInfoHistoryDetailService
    {

        public MerchantInfoHistoryDetail Get(Int32 id)
        {
            try
            {
                return MerchantInfoHistoryDetailDA.DAO.Get(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public void Delete(Int32 id)
        {
            try
            {
                MerchantInfoHistoryDetailDA.DAO.Delete(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public void BatchDelete(List<Int32> keyList)
        {
            try
            {
                MerchantInfoHistoryDetailDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<MerchantInfoHistoryDetail> GetAll()
        {
            try
            {
                return MerchantInfoHistoryDetailDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<int?> GetAllDistinctMerchantIDList()
        {
            try
            {
                return MerchantInfoHistoryDetailDA.DAO.GetAllDistinctMerchantIDList() ?? new List<int?>();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(MerchantInfoHistoryDetail merchant)
        {
            try
            {
                MerchantInfoHistoryDetailDA.DAO.Create(merchant);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(MerchantInfoHistoryDetail merchant)
        {
            try
            {
                MerchantInfoHistoryDetailDA.DAO.Update(merchant);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public QueryResultInfo<MerchantInfoHistoryDetail> Query(QueryConditionInfo<MerchantInfoHistoryDetail> queryCondition)
        {
            try
            {
                return MerchantInfoHistoryDetailDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }



    }
}
