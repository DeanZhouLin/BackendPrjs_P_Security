
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
	public class RoleBL :IRoleService
	{
       
		public RoleInfo Get(Int32 id)
        {
            try
            {
                return RoleDA.DAO.Get(id);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public void Delete(Int32 id,string editUser)
        {
            try
            {
                RoleDA.DAO.LogicDelete(id, editUser);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
        public void BatchDelete(List<Int32> keyList, string editUser)
       {
           try
            {
                RoleDA.DAO.BatchLogicDelete(keyList,editUser);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
       }
    
        public List<RoleInfo> GetAll()
        {
            try
            {
                return RoleDA.DAO.GetAll();
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public bool Create(RoleInfo role)
        {
            try
            {
                RoleInfo ri = RoleDA.DAO.GetByRoleName(role.RoleName,-1);
                if (ri!=null)
                {
                    return false;
                }
                RoleDA.DAO.Create(role);
                return true;
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public bool Update(RoleInfo role)
        {
            try
            {
                RoleInfo ri = RoleDA.DAO.GetByRoleName(role.RoleName,role.ID);
                if (ri != null)
                {
                    return false;
                }
                RoleDA.DAO.Update(role);
                return true;
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }
       public QueryResultInfo<RoleInfo> Query(QueryConditionInfo<RoleInfo> queryCondition)
       {
           try
            {
               return RoleDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            } 
       }

       public List<RoleInfo> GetAllActive()
       {
           try
           {
               return RoleDA.DAO.GetAllActive();
           }
           catch (Exception ex)
           {

               throw ExceptionFactory.BuildException(ex);
           }
       }

    }
}
