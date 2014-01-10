using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IResourceService: IServiceBase
	{
		
        
        Resource Get(Int32 id);
        List<Resource> GetAll();
        int Create(Resource resource);
        bool Update(Resource resource);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<Resource> Query(QueryConditionInfo<Resource> queryCondition);
        void BatchChangeStatus(List<string> idList, int status);
        Resource GetParentResource(int id);
    }
}
