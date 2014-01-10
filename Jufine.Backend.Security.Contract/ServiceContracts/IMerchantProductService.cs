using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IMerchantProductService: IServiceBase
	{
		
        
        MerchantProduct Get(Int32 id);
        List<MerchantProduct> GetAll();
        void Create(MerchantProduct merchantProduct);
        void Update(MerchantProduct merchantProduct);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<MerchantProduct> Query(QueryConditionInfo<MerchantProduct> queryCondition);
    }
}
