using System;
using System.Text;
using System.Collections.Generic;

using Com.BaseLibrary.Contract;

namespace Jufine.Backend.Security.DataContracts
{

    public partial class RoleInfo
    {
        public string RoleNameDisplay
        {
            get
            {
                return RoleName + "(" + DisplayName + ")";
            }
        }

    }
}