
using System;
using System.Collections.Generic;

using Com.BaseLibrary.Utility;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.Business
{
	public class UserResourceBL :IUserResourceService
	{
       
		public UserResource Get(Int32 id)
        {
            try
            {
                return UserResourceDA.DAO.Get(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public List<UserResource> GetByUserID(int id,int merchantID)
        {
            try
            {
                return UserResourceDA.DAO.GetByUserID(id,merchantID);
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
                 UserResourceDA.DAO.Delete(id);
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
                UserResourceDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<UserResource> GetAll()
        {
            try
            {
                return UserResourceDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(UserResource userResource)
        {
            try
            {
                UserResourceDA.DAO.Create(userResource);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(UserResource userResource)
        {
            try
            {
                UserResourceDA.DAO.Update(userResource);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<UserResource> Query(QueryConditionInfo<UserResource> queryCondition)
       {
           try
            {
               return UserResourceDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }
       public void UpdateAuthorityByUser(List<int> list, int userID, int merchantID, string editUser)
       {
           try
           {
               UserResourceDA.DAO.UpdateAuthorityByUser(list,userID,merchantID,editUser);
           }
           catch (Exception ex)
           {
               throw ExceptionFactory.BuildException(ex);
           }
       }
       

    }
}
