
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
	public class ResourceBL : IResourceService
	{

		public Resource Get(Int32 id)
		{
			try
			{
				return ResourceDA.DAO.Get(id);
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
				using (TransactionScope ts = new TransactionScope())
				{
					UserResourceDA.DAO.DeleteByResourceID(id);
					ResourceDA.DAO.Delete(id);
					ts.Complete();
				}
				
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
				UserResourceDA.DAO.DeleteByResourceList(keyList);
				ResourceDA.DAO.BatchDelete(keyList);
			}
			catch (Exception ex)
			{

				throw ExceptionFactory.BuildException(ex);
			}
		}

		public List<Resource> GetAll()
		{
			try
			{
				return ResourceDA.DAO.GetAll();
			}
			catch (Exception ex)
			{

				throw ExceptionFactory.BuildException(ex);
			}
		}

       
		public int Create(Resource resource)
		{
			try
			{
				bool isResourceNameExist = ResourceDA.DAO.IsResourceNameExist(resource);
				if (isResourceNameExist)
				{
					return -1;
				}
				else
				{
					ResourceDA.DAO.Create(resource);
					return ResourceDA.DAO.GetByResourceName(resource.ResourceName);
				}

			}
			catch (Exception ex)
			{

				throw ExceptionFactory.BuildException(ex);
			}
		}

		public bool Update(Resource resource)
		{
			try
			{
				bool isResourceNameExist = ResourceDA.DAO.IsResourceNameExist(resource);
				if (isResourceNameExist)
				{
					return false;
				}
				else
				{
					ResourceDA.DAO.Update(resource);
					return true;
				}
			}
			catch (Exception ex)
			{

				throw ExceptionFactory.BuildException(ex);
			}
		}
		public QueryResultInfo<Resource> Query(QueryConditionInfo<Resource> queryCondition)
		{
			try
			{
				return ResourceDA.DAO.Query(queryCondition);
			}
			catch (Exception ex)
			{

				throw ExceptionFactory.BuildException(ex);
			}
		}

		public Resource GetParentResource(int id)
		{
			try
			{
				Resource resource = ResourceDA.DAO.Get(id);
				return ResourceDA.DAO.Get(resource.ParentID);
			}
			catch (Exception ex)
			{

				throw ExceptionFactory.BuildException(ex);
			}
		}

		public void BatchChangeStatus(List<string> idList, int status)
		{
			try
			{
				foreach (string id in idList)
				{
					ResourceDA.DAO.ChangeStatus(Converter.ToInt32(id, 0), status);
				}
			}
			catch (Exception ex)
			{

				throw ExceptionFactory.BuildException(ex);
			}
		}

	}
}
