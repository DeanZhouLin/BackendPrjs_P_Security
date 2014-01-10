
using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.Business
{
	public class MerchantAdditionalCertificateBL :IMerchantAdditionalCertificateService
	{
       
		public MerchantAdditionalCertificate Get(Int32 id)
        {
            try
            {
                return MerchantAdditionalCertificateDA.DAO.Get(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public List<MerchantAdditionalCertificate> GetByMerchantID(int merchantID)
        {
            try
            {
                return MerchantAdditionalCertificateDA.DAO.GetByMerchantID(merchantID);
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
                 MerchantAdditionalCertificateDA.DAO.Delete(id);
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
                MerchantAdditionalCertificateDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<MerchantAdditionalCertificate> GetAll()
        {
            try
            {
                return MerchantAdditionalCertificateDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(MerchantAdditionalCertificate merchantAdditionalCertificate)
        {
            try
            {
                MerchantAdditionalCertificateDA.DAO.Create(merchantAdditionalCertificate);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(MerchantAdditionalCertificate merchantAdditionalCertificate)
        {
            try
            {
                MerchantAdditionalCertificateDA.DAO.Update(merchantAdditionalCertificate);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<MerchantAdditionalCertificate> Query(QueryConditionInfo<MerchantAdditionalCertificate> queryCondition)
       {
           try
            {
               return MerchantAdditionalCertificateDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }
    
       

    }
}
