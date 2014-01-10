using System;
using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
	[Serializable]
	public partial class UserInfo: DataContractBase
	{	
		public Int32 MerchantID{get;set;}
		public string UserName{get;set;}
		public string Password{get;set;}
		public string FullName{get;set;}
		public Int32 Sex{get;set;}
		public string IDCard{get;set;}
		public Int32 Status{get;set;}
        public DateTime? BirthdayTo{get;set;}
		public string Email{get;set;}
		public Int32 IsAdmin{get;set;}
		public string CreateUser{get;set;}
		public DateTime ? CreateDate{get;set;}
        public DateTime? CreateDateTo{get;set;}
		public string EditUser{get;set;}
		public DateTime ? EditDate{get;set;}
        public DateTime? EditDateTo{get;set;}
		
	}
}