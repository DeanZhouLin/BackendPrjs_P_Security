using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Com.BaseLibrary.Contract;
using Com.BaseLibrary.Entity;

namespace Jufine.Backend.Security.DataContracts
{
	[Serializable]
	public partial class Resource: DataContractBase
	{
         [DataMapping]
		public Int32 ParentID{get;set;}
         [DataMapping]
		public string ResourceName{get;set;}
         [DataMapping]
		public string DisplayName{get;set;}
         [DataMapping]
		public Int32 DisplayOrder{get;set;}
         [DataMapping]
		public string ResourceAddress{get;set;}
		public Int32 ShowInMenu{get;set;}
		public Int32 ResourceType{get;set;}
		public string Path{get;set;}
		public Int32 Status{get;set;}
		public string CreateUser{get;set;}
		public DateTime ? CreateDate{get;set;}
        public DateTime? CreateDateTo{get;set;}
		public string EditUser{get;set;}
		public DateTime ? EditDate{get;set;}
        public DateTime? EditDateTo{get;set;}
		
	}
}