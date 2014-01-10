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