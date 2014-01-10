
using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.Business
{
	public class MerchantProductBL :IMerchantProductService
	{
       
		public MerchantProduct Get(Int32 id)
        {
            try
            {
                return MerchantProductDA.DAO.Get(id);
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
                 MerchantProductDA.DAO.Delete(id);
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
                MerchantProductDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<MerchantProduct> GetAll()
        {
            try
            {
                return MerchantProductDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(MerchantProduct merchantProduct)
        {
            try
            {
                MerchantProductDA.DAO.Create(merchantProduct);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(MerchantProduct merchantProduct)
        {
            try
            {
                MerchantProductDA.DAO.Update(merchantProduct);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<MerchantProduct> Query(QueryConditionInfo<MerchantProduct> queryCondition)
       {
           try
            {
               return MerchantProductDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }
    
       

    }
}
