using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
    [Serializable]
    public partial class MerchantInfoHistory : DataContractBase
    {
        public Int32 MerchantID { get; set; }
        public Int32 Fromstatus { get; set; }
        public Int32 Tostatus { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CreateDateTo { get; set; }

    }
}