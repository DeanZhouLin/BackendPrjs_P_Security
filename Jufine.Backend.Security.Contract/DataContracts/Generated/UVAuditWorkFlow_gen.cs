using System;
using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
	[Serializable]
	public partial class UVAuditWorkFlow: DataContractBase
	{	
		public string DisplayName{get;set;}
		public Int32 ResourceID{get;set;}
		public Int32 TotalLevel{get;set;}
		public string ResourceDisplayName{get;set;}
		public string ResourceName{get;set;}
		
	}
}