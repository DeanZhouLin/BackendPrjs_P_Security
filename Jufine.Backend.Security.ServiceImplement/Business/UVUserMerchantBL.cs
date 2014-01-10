
using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.Business
{
	public class UVUserMerchantBL :IUVUserMerchantService
	{
       
		public UVUserMerchant Get(Int32 id)
        {
            try
            {
                return UVUserMerchantDA.DAO.Get(id);
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
                 UVUserMerchantDA.DAO.Delete(id);
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
                UVUserMerchantDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<UVUserMerchant> GetAll()
        {
            try
            {
                return UVUserMerchantDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(UVUserMerchant uvUserMerchant)
        {
            try
            {
                UVUserMerchantDA.DAO.Create(uvUserMerchant);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(UVUserMerchant uvUserMerchant)
        {
            try
            {
                UVUserMerchantDA.DAO.Update(uvUserMerchant);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<UVUserMerchant> Query(QueryConditionInfo<UVUserMerchant> queryCondition)
       {
           try
            {
               return UVUserMerchantDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }

       public List<UVUserMerchant> GetByUserID(int id)
       {
           try
           {
               return UVUserMerchantDA.DAO.GetByUserID(id);
           }
           catch (Exception ex)
           {

               throw ExceptionFactory.BuildException(ex);
           }
       }
    }
}
