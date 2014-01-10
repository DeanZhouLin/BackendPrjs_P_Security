using System;
using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
	[Serializable]
	public partial class QueryLog: DataContractBase
	{	
		public string UserID{get;set;}
		public string SqlText{get;set;}
		public string CreateUser{get;set;}
		public DateTime ? CreateDate{get;set;}
        public DateTime? CreateDateTo{get;set;}
		public string EditUser{get;set;}
		public DateTime ? EditDate{get;set;}
        public DateTime? EditDateTo{get;set;}
		
	}
}