
using System;
using System.Collections.Generic;

using Com.BaseLibrary.Utility;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;
using System.Linq;

namespace Jufine.Backend.Security.Business
{
	public class UserRoleBL :IUserRoleService
	{
       
		public UserRole Get(Int32 id)
        {
            try
            {
                return UserRoleDA.DAO.Get(id);
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
                 UserRoleDA.DAO.Delete(id);
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
                UserRoleDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<UserRole> GetAll()
        {
            try
            {
                return UserRoleDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(UserRole userRole)
        {
            try
            {
                UserRoleDA.DAO.Create(userRole);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(UserRole userRole)
        {
            try
            {
                UserRoleDA.DAO.Update(userRole);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<UserRole> Query(QueryConditionInfo<UserRole> queryCondition)
       {
           try
            {
               return UserRoleDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }

       public List<UserRole> GetByUserID(int id, int merchantID)
       {
           try
           {
               return UserRoleDA.DAO.GetByUserID(id, merchantID);
           }
           catch (Exception ex)
           {

               throw ExceptionFactory.BuildException(ex);
           }
       }
       public void UpdateRoleByUser(List<int> list, int userID, int merchantID, string editUser)
       {
           try
           {
               UserRoleDA.DAO.UpdateRoleByUser(list, userID, merchantID, editUser);
           }
           catch (Exception ex)
           {
               throw ExceptionFactory.BuildException(ex);
           }
       }
       public List<int> GetRoleResourceID(int userID, int merchantID)
       {
           try
           {
               List<int> rList = new List<int>();
               List<UserRole> userRoleList = UserRoleDA.DAO.GetByUserID(userID, merchantID);
               if (userRoleList.Count > 0)
               {
                   List<int> roleIDList = userRoleList.Select(c => c.RoleID).ToList();
                   rList= RoleResourceDA.DAO.GetResourceIDByRoleList(roleIDList);
               }
               return rList;
           }
           catch (Exception ex)
           {

               throw ExceptionFactory.BuildException(ex);
           }
       }
    }
}
