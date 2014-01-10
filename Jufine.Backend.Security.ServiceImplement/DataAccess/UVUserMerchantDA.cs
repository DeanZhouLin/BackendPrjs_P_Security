using System.Linq;
using System.Data.Objects;
using System.Collections.Generic;

using Com.BaseLibrary.Service;
using Jufine.Backend.Security.DataContracts;

namespace Jufine.Backend.Security.DataAccess
{
    internal class UVUserMerchantDA : DataBase<UVUserMerchant, SecurityEntities>
    {
        internal static UVUserMerchantDA DAO = new UVUserMerchantDA();
        private UVUserMerchantDA() { }
        
        internal List<UVUserMerchant> GetByUserID(int id)
        {
            using (SecurityEntities entities = new SecurityEntities())
            {
                ObjectSet<UVUserMerchant> objectSet = entities.CreateObjectSet<UVUserMerchant>();
                return objectSet.Where(c => c.Status == 1 && c.UserID == id).ToList();
            }
        }
    }
}
