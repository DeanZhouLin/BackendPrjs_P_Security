using System.Data.Objects;
using System.Data.EntityClient;
using Com.BaseLibrary.Common.Logging;
using Com.BaseLibrary.Utility;
using Com.BaseLibrary.Common.Cryptography;

namespace Jufine.Backend.Security.DataAccess
{
    public partial class SecurityEntities : ObjectContext
    {
        private static string connectionstring;
        static SecurityEntities()
        {
            connectionstring = ConfigurationHelper.GetConnectionString("SecurityEntities");
            LogHelper.CustomInfo(connectionstring);
            if (!connectionstring.StartsWith("metadata="))
            {
                connectionstring = Encryptor.Decrypt(connectionstring);
            }
        }

        /// <summary>
        /// 请使用应用程序配置文件的“SecurityEntities”部分中的连接字符串初始化新 SecurityEntities 对象。
        /// </summary>
        public SecurityEntities()
            : base(new EntityConnection(connectionstring), "SecurityEntities")
        {

        }
    }
}
