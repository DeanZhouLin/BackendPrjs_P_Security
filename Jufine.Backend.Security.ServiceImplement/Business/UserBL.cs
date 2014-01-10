
using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;
using System.Transactions;

namespace Jufine.Backend.Security.Business
{
    public class UserBL : IUserService
    {

        public UserInfo Get(Int32 id)
        {
            try
            {
                return UserDA.DAO.Get(id);
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
                UserDA.DAO.Delete(id);
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
                UserDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<UserInfo> GetAll()
        {
            try
            {
                return UserDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(UserInfo user)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    UserDA.DAO.Create(user);
                    UserMerchant um = new UserMerchant() { 
                    UserID=user.ID,
                    MerchantID=user.MerchantID,
                    CreateDate=user.CreateDate,
                    CreateUser=user.CreateUser,
                    };
                    UserMerchantDA.DAO.Create(um);
                    ts.Complete();
                }
                
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(UserInfo user)
        {
            try
            {
                UserDA.DAO.Update(user);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public QueryResultInfo<UserInfo> Query(QueryConditionInfo<UserInfo> queryCondition)
        {
            try
            {
                return UserDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<UserInfo> GetUserListByMerchantID(Int32 merchantID)
        {
            try
            {
                return UserDA.DAO.GetUserListByMerchantID(merchantID);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public UserInfo Login(string userName, string password)
        {

            try
            {
                UserInfo user = UserDA.GetByUserName(userName);
                if (user == null)
                {
                    throw new BizException("用户名不存在。");
                }
                else if (user.Password != password)
                {
                    throw new BizException("密码错误。");
                }
                else if (user.Status == 0)
                {
                    throw new BizException("该用户被禁用，请联系管理员。");
                }
                return user;
            }
            catch (Exception ex)
            {
                //throw new BizException(ex.ToString());
                throw ExceptionFactory.BuildException(ex);
            }
        }


        public UserInfo GetByUserName(string userName)
        {
            try
            {
                return UserDA.GetByUserName(userName);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void BatchChangeStatus(List<int> idList, int status, string editUser)
        {
            try
            {
                UserDA.DAO.BatchChangeStatus(idList, status, editUser);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void BatchChangeStatusByMerchantID(List<int> idList, int status, string editUser)
        {
            try
            {
                UserDA.DAO.BatchChangeStatusByMerchantID(idList, status, editUser);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        
    }
}
