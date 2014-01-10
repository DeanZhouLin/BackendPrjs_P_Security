using System;
using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{
    [Serializable]
    public class TAuditWorkFlowResource : DataContractBase
    {
        public Int32 UserID { get; set; }
        public Int32 ResourceID { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string ResourceName { get; set; }
        public string ResourceDisplayName { get; set; }
        public Int64 AuditLevel { get; set; }
    }
}