using System;
using System.Collections.Generic;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Contract;

using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.ServiceContracts
{
	public interface IMerchantAdditionalCertificateService: IServiceBase
	{
		
        
        MerchantAdditionalCertificate Get(Int32 id);
        List<MerchantAdditionalCertificate> GetAll();
        void Create(MerchantAdditionalCertificate merchantAdditionalCertificate);
        void Update(MerchantAdditionalCertificate merchantAdditionalCertificate);
        void Delete(Int32 id);
        void BatchDelete(List<Int32> keyList);
        QueryResultInfo<MerchantAdditionalCertificate> Query(QueryConditionInfo<MerchantAdditionalCertificate> queryCondition);

        List<MerchantAdditionalCertificate> GetByMerchantID(int merchantID);
    }
}
