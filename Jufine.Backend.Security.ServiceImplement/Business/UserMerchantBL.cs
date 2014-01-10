
using System;
using System.Collections.Generic;

using Com.BaseLibrary.Utility;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;
using System.Transactions;

namespace Jufine.Backend.Security.Business
{
    public class UserMerchantBL : IUserMerchantService
    {

        public UserMerchant Get(Int32 id)
        {
            try
            {
                return UserMerchantDA.DAO.Get(id);
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
                UserMerchantDA.DAO.Delete(id);
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
                UserMerchantDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<UserMerchant> GetAll()
        {
            try
            {
                return UserMerchantDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(UserMerchant userMerchant)
        {
            try
            {
                UserMerchantDA.DAO.Create(userMerchant);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(UserMerchant userMerchant)
        {
            try
            {
                UserMerchantDA.DAO.Update(userMerchant);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public QueryResultInfo<UserMerchant> Query(QueryConditionInfo<UserMerchant> queryCondition)
        {
            try
            {
                return UserMerchantDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<UserMerchant> GetByUserID(int id)
        {
            try
            {
                return UserMerchantDA.DAO.GetByUserID(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public void UpdateMerchantByUser(List<int> list, int userID, string editUser)
        {
            try
            {
                UserMerchantDA.DAO.UpdateMerchantByUser(list, userID, editUser);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }
    }
}
