using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;
using System.Data;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface ILogCenterService: IServiceBase
	{
		
        
        LogCenter Get(Int64 id);
        List<LogCenter> GetAll();
        DataSet Ddlname(string connectionString,string sql);
        void Create(LogCenter logCenter);
        void Update(LogCenter logCenter);
        void Delete(Int64 id);
        void BatchDelete(List<Int64> keyList);
        QueryResultInfo<LogCenter> Query(QueryConditionInfo<LogCenter> queryCondition);
    }
}
