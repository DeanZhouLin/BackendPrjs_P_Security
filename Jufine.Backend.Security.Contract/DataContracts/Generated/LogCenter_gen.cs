using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
	[Serializable]
	public partial class LogCenter: DataContractBaseLongKey
	{	
		public string ApplicationName{get;set;}
		public string Module{get;set;}
		public string LogType{get;set;}
		public string Title{get;set;}
		public string Detail{get;set;}
		public DateTime ? LogTime{get;set;}
        public DateTime? LogTimeTo{get;set;}
		
	}
}