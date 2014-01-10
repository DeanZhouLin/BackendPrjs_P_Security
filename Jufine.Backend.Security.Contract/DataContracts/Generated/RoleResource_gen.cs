using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
	[Serializable]
	public partial class RoleResource: DataContractBase
	{	
		public Int32 RoleID{get;set;}
		public Int32 ResourceID{get;set;}
		public string CreateUser{get;set;}
		public DateTime ? CreateDate{get;set;}
        public DateTime? CreateDateTo{get;set;}
		public string EditUser{get;set;}
		public DateTime ? EditDate{get;set;}
        public DateTime? EditDateTo{get;set;}
		
	}
}