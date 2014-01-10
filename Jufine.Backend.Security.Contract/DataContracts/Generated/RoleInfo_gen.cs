using System;
using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
	[Serializable]
	public partial class RoleInfo: DataContractBase
	{	
		public string RoleName{get;set;}
		public string DisplayName{get;set;}
		public Int32 ? DisplayOrder{get;set;}
		public Int32 Status{get;set;}
		public string CreateUser{get;set;}
		public DateTime ? CreateDate{get;set;}
        public DateTime? CreateDateTo{get;set;}
		public string EditUser{get;set;}
		public DateTime ? EditDate{get;set;}
        public DateTime? EditDateTo{get;set;}
		
	}
}