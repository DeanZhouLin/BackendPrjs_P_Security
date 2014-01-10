
using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;

namespace Jufine.Backend.Security.Business
{
	public class RoleResourceBL :IRoleResourceService
	{
       
		public RoleResource Get(Int32 id)
        {
            try
            {
                return RoleResourceDA.DAO.Get(id);
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
                 RoleResourceDA.DAO.Delete(id);
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
                RoleResourceDA.DAO.BatchDelete(keyList);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<RoleResource> GetAll()
        {
            try
            {
                return RoleResourceDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Create(RoleResource roleResource)
        {
            try
            {
                RoleResourceDA.DAO.Create(roleResource);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void Update(RoleResource roleResource)
        {
            try
            {
                RoleResourceDA.DAO.Update(roleResource);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<RoleResource> Query(QueryConditionInfo<RoleResource> queryCondition)
       {
           try
            {
               return RoleResourceDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }

       public void UpdateAuthorityByRole(List<int> list, int roleID,string editUser)
       {
           try
           {
               RoleResourceDA.DAO.UpdateAuthorityByRole(list, roleID, editUser);
           }
           catch (Exception ex)
           {

               throw ExceptionFactory.BuildException(ex);
           }
       }
       public List<RoleResource> GetByRoleID(int id)
       {
           try
           {
               return RoleResourceDA.DAO.GetByRoleID(id);
           }
           catch (Exception ex)
           {

               throw ExceptionFactory.BuildException(ex);
           }
       }
    }
}
