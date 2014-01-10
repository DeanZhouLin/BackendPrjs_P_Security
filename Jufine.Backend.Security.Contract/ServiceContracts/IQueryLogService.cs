using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IQueryLogService: IServiceBase
	{
		
        
        QueryLog Get(Int32 id);
        List<QueryLog> GetAll();
        void Create(QueryLog queryLog);
        void Update(QueryLog queryLog);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<QueryLog> Query(QueryConditionInfo<QueryLog> queryCondition);
    }
}
