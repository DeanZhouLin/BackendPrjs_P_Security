using System;
using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
	[Serializable]
	public partial class UVUserMerchant: DataContractBase
	{	
		public Int32 UserID{get;set;}
		public Int32 MerchantID{get;set;}
        public string MerchantName { get; set; }
        public Int32 Status { get; set; }
		
	}
}