using System;
using System.Collections.Generic;
using System.Linq;
using Com.BaseLibrary.Common.Security;
using System.Web;
using Com.BaseLibrary.Web.Coockie;
using Com.BaseLibrary.Data;
using Com.BaseLibrary.Utility;
using System.Data;
using System.Data.SqlClient;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Common.Cryptography;

namespace Jufine.Backend.Security.Authentication
{
    public class UserFactory : IUserFactory
    {
        public IUser CreateUser()
        {
            return new User();
        }
    }

    public class User : IUser
    {
        private const string GETUSERRESOURCELIST = "dbo.GetUserResourceList";//获取当前用户下所有有权限的资源（存储过程名称）
        private static readonly string SecurityConnString;
        protected CookieEntryInfo UserCookie { get; set; }
        protected List<Resource> ResourceList { get; set; }
        public List<UserMerchant> PageMerchantList { get; private set; }
        //protected List<Resource> BtnResourceList { get; set; }

        public static List<Resource> AllResourceList = null;

        static User()
        {
            string connectionString = ConfigurationHelper.GetConnectionString("SecurityConn");
            if (connectionString.Contains(";"))
            {
                SecurityConnString = connectionString;
            }
            else
            {
                SecurityConnString = Encryptor.Decrypt(connectionString);
            }

            AllResourceList = GetAllResource();
        }

        public User()
        {
            UserCookie = CookieFactory.CreateCookie("user");
            if (IsLogin)
            {

                DataSet ds = SqlHelper.ExecuteDataset(SecurityConnString, GETUSERRESOURCELIST, new SqlParameter("@UserID", UserId));
                DataTable dtUser = ds.Tables[0];
                if (dtUser.Rows.Count == 0)
                {
                    Logout();
                    return;
                }

                MerchantID = Convert.ToInt32(dtUser.Rows[0]["MerchantID"]);
                IsAdmin = Convert.ToInt32(dtUser.Rows[0]["IsAdmin"]) == 1;
                DataTable dtMerchant = ds.Tables[2];
                MerchantName = dtMerchant.Rows[0]["MerchantName"].ToString();
                ResourceList = EntityBuilder.BuildEntityList<Resource>(ds.Tables[1]);
                if (ResourceList == null)
                {
                    ResourceList = new List<Resource>();
                }
                MerchantList = EntityBuilder.BuildEntityList<UserMerchant>(ds.Tables[3]);
                if (MerchantList == null)
                {
                    MerchantList = new List<UserMerchant>();
                }
                if (PageMerchantList == null)
                {
                    if (IsAdmin)
                    {
                        PageMerchantList = MerchantList;
                    }
                    else
                    {
                        string url = HttpContext.Current.Request.Path;
                        var sss = ResourceList.FindAll(c => c.ResourceAddress.ToUpper() == url.ToUpper());
                        PageMerchantList = MerchantList.FindAll(d => sss.Exists(e => e.MerchantID == d.MerchantID));
                    }
                }
            }
        }
        public string UserName
        {
            get
            {
                return UserCookie["USERNAME"];
            }
            set
            {
                UserCookie["USERNAME"] = value;
            }
        }

        public bool HasPermssion(string resourcePath)
        {
            if (IsAdmin)
            {
                return true;
            }

            var currentResourceList = GetUserResouceList();
            return currentResourceList.Exists(c => c.ResourceAddress.ToUpper() == resourcePath);
        }

        public void Logout()
        {
            UserCookie.Delete();
        }

        public int MerchantID { get; private set; }
        public int CurrentMerchantID
        {
            get
            {
                int currentMerchantID = Converter.ToInt32(UserCookie["CurrentMerchantID"], 0);
                if (currentMerchantID == 0)
                {
                    currentMerchantID = MerchantID;
                }
                return currentMerchantID;
            }
            set
            {
                UserCookie["CurrentMerchantID"] = value.ToString();
            }
        }
        public string CurrentMerchantName
        {
            get
            {
                string name = string.Empty;
                if (MerchantList != null)
                {
                    UserMerchant um = MerchantList.FirstOrDefault(c => c.MerchantID == CurrentMerchantID);
                    if (um != null)
                    {
                        name = um.MerchantName;
                    }
                }
                return name;
            }
        }
        public List<UserMerchant> MerchantList { get; private set; }
        public string MerchantName { get; private set; }
        public int UserId
        {
            get { return int.Parse(UserCookie["UID"]); }
        }
        public void Login(string userName, int userId)
        {
            UserCookie["userName"] = userName;
            UserCookie["UID"] = userId.ToString();
        }

        public bool IsLogin
        {
            get { return UserCookie["userName"] != null; }
        }

        public List<Resource> GetUserResouceList()
        {
            if (ResourceList == null)
            {
                return new List<Resource>();
            }
            return IsAdmin ? ResourceList : ResourceList.FindAll(c => c.MerchantID == CurrentMerchantID);
        }

        public List<Resource> GetAllSubResouceList(int resourceId)
        {
            if (IsAdmin)
            {
                return AllResourceList.FindAll(c => c.ParentID == resourceId);
            }
            return AllResourceList.FindAll(c => c.ParentID == resourceId);
        }

        public List<Resource> GetPageControlResouceList(int pageResourceId)
        {
            if (IsAdmin)
            {
                return AllResourceList.FindAll(c => c.ParentID == pageResourceId && c.ResourceType == 2);
            }
            return ResourceList.FindAll(c => c.ParentID == pageResourceId && c.ResourceType == 2 && c.MerchantID == CurrentMerchantID);
        }

        static List<Resource> GetAllResource()
        {
            const string sql = "select * from Resource where Status=1 ";
            DataSet dataset = SqlHelper.ExecuteDataset(SecurityConnString, CommandType.Text, sql);
            return EntityBuilder.BuildEntityList<Resource>(dataset.Tables[0]);
        }

        public bool IsAdmin { get; set; }

        #region AddByDean  For 多级审批

        private const int timeOutValue = 1;
        private static DateTime timeStamp = DateTime.Now;

        private static List<UserRole> _allUserRoleList;
        /// <summary>
        /// 所有的权限
        /// </summary>
        static List<UserRole> AllUserRoleList
        {
            get
            {
                if (_allUserRoleList == null || ExecDateDiff(timeStamp, DateTime.Now) > timeOutValue)
                {
                    InitAuditWorkFlowResource();
                    timeStamp = DateTime.Now;
                }
                return _allUserRoleList;
            }
        }

        private static List<AuditWorkFlowResource> _allAuditWorkFlowResourceList;
        /// <summary>
        /// 所有审批流程资源
        /// </summary>
        static IEnumerable<AuditWorkFlowResource> AllAuditWorkFlowResourceList
        {
            get
            {
                if (_allAuditWorkFlowResourceList == null || ExecDateDiff(timeStamp, DateTime.Now) > timeOutValue)
                {
                    InitAuditWorkFlowResource();
                    timeStamp = DateTime.Now;
                }
                return _allAuditWorkFlowResourceList;
            }
        }

        private static List<UserInfo> _allUserInfoList;
        /// <summary>
        /// 所有用户信息
        /// </summary>
        static List<UserInfo> AllUserInfoList
        {
            get
            {
                if (_allUserInfoList == null || ExecDateDiff(timeStamp, DateTime.Now) > timeOutValue)
                {
                    InitAuditWorkFlowResource();
                    timeStamp = DateTime.Now;
                }
                return _allUserInfoList;
            }
        }

        /// <summary>
        /// 获取控件是否可见
        /// </summary>
        /// <param name="ctlName"></param>
        /// <param name="previewAuditUserName"></param>
        /// <param name="currentPageId"></param>
        /// <param name="pageControlResouceList"></param>
        /// <returns></returns>
        public UserAuditWorkFlowResource GetUserAuditWorkFlowResource(string ctlName, string previewAuditUserName, int currentPageId, List<Resource> pageControlResouceList)
        {
            UserAuditWorkFlowResource result = new UserAuditWorkFlowResource();

            //获取前审批用户的UserID
            int preAuditUserId;
            #region 【若传入参数值为空，则设置为-1；否则，提示“找不到指定的用户信息”的错误信息】

            if (string.IsNullOrEmpty(previewAuditUserName) || string.IsNullOrEmpty(previewAuditUserName.Trim()))
            {
                preAuditUserId = -1;
            }
            else
            {
                preAuditUserId = GetUserIdByUserName(previewAuditUserName);
                if (preAuditUserId == -1)
                {
                    result.AddError("找不到指定的用户信息-" + previewAuditUserName);
                    return result;
                }
            }

            #endregion

            //获取控制的资源【若找不到对应的资源，提示“找不到指定的控件资源”的错误信息】(admin 能找出所有的资源，仅查询“按钮”控件)
            var ctlResource = pageControlResouceList.FindAll(c => c.ControlIDList.Contains(ctlName));
            if (ctlResource.Count == 0)
            {
                result.AddError("找不到指定的控件资源-" + ctlName);
                return result;
            }

            List<int> ctlResourceIds = new List<int>();
            ctlResourceIds.AddRange(from resource in ctlResource.ToList() select resource.ID);

            //当前用户的审批资源
            var currentUserAduitWorkFlowResourceDic = GetAllAuthedAuditWorkFlowResource(currentPageId);

            //判断当前用户是否有该控件的控制权限
            int ctlResourceId = -1;
            foreach (var resourceId in ctlResourceIds)
            {
                if (currentUserAduitWorkFlowResourceDic.ContainsKey(resourceId))
                {
                    ctlResourceId = resourceId;
                    break;
                }
            }

            //当前用户无任何审批流程资源
            if (ctlResourceId == -1)
            {
                return result;
            }

            //当前用户对当前控件的审批资源
            var currentUserAduitWorkFlowResource = currentUserAduitWorkFlowResourceDic[ctlResourceId];

            var previewUserAduitWorkFlowResourceDic = preAuditUserId != -1 ? GetAllAuthedAuditWorkFlowResource(currentPageId, preAuditUserId) : null;

            if (preAuditUserId != -1 && !previewUserAduitWorkFlowResourceDic.ContainsKey(ctlResourceId))
            {
                //result.AddError("前审核用户无任何审批流程信息-" + previewAuditUserName);
                //return result;
                preAuditUserId = -1;
                previewUserAduitWorkFlowResourceDic = null;
            }

            var previewUserAuditWorkFlowResource = preAuditUserId != -1 ? previewUserAduitWorkFlowResourceDic[ctlResourceId] : null;

            var currentWorkFlowResource = currentUserAduitWorkFlowResource.CurrentWorkFlowResource;
            var previewWorkFlowResource = preAuditUserId != -1 ? previewUserAuditWorkFlowResource.CurrentWorkFlowResource : null;

            if (currentWorkFlowResource == null)
            {
                result.AddError("当前用户无具体审批流程信息-" + UserName);
                return result;
            }

            if (previewWorkFlowResource == null && preAuditUserId != -1)
            {
                result.AddError("前审核用户无具体审批流程信息-" + previewAuditUserName);
                return result;
            }

            int nextStep = preAuditUserId != -1 ? previewWorkFlowResource.ParentGroupID : -1;

            foreach (var aawfr in currentUserAduitWorkFlowResource.AuthedAuditWorkFlowResources.Values)
            {
                if (aawfr.GroupID == nextStep || (preAuditUserId == -1 && currentUserAduitWorkFlowResource.ContainsStep(1)))
                {
                    currentUserAduitWorkFlowResource.HasAuth = true;
                    currentUserAduitWorkFlowResource.WaitAuditStep = preAuditUserId != -1 ? previewUserAuditWorkFlowResource.CurrentAuditStep + 1 : 1;
                    return currentUserAduitWorkFlowResource;
                }
            }

            currentUserAduitWorkFlowResource.WaitAuditStep = preAuditUserId != -1 ? previewUserAuditWorkFlowResource.CurrentAuditStep + 1 : 1;
            currentUserAduitWorkFlowResource.HasAuth = false;
            return currentUserAduitWorkFlowResource;
        }

        /// <summary>
        /// 获取当前用户在页面下所有的审批流程的权限
        /// key - 控制的资源ID
        /// value - 审批资源
        /// admin拥有所有审批流程的资源
        /// </summary>
        /// <param name="currentPageID">当前页面ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>用户在页面下所有的审批流程资源【key - 控制的资源ID， value - 审批资源】</returns>
        private Dictionary<int, UserAuditWorkFlowResource> GetAllAuthedAuditWorkFlowResource(int currentPageID, int userID = -1)
        {

            #region 初始化数据：初始化UserID，IsAdmin，RoleList，页面下所有的审批流程，页面下用户有权限的审批流程

            if (userID == -1)
            {
                userID = UserId;
            }

            var isAdmin = AllUserInfoList.Find(c => c.ID == userID).IsAdmin;

            var authedRoleList = AllUserRoleList.FindAll(c => c.UserID == userID).Select(c => c.RoleID).ToList();

            //当前用户在当前页面所拥有的流程
            List<AuditWorkFlowResource> authedAuditWorkFlowResourceList = new List<AuditWorkFlowResource>();
            List<AuditWorkFlowResource> allAuditWorkFlowResourceList = new List<AuditWorkFlowResource>();

            #endregion

            #region 遍历所有的审批资源，填充【页面下所有的审批流程，页面下用户有权限的审批流程】

            foreach (var auditWorkFlowResource in AllAuditWorkFlowResourceList)
            {
                //获取当前的控制资源
                //var resource = ResourceList.FirstOrDefault(c => c.ID == auditWorkFlowResource.ResourceID);
                AuditWorkFlowResource tempResource = auditWorkFlowResource;
                //var resourceList = ResourceList.Where(c => tempResource.ResourceIDList.Contains(c.ID));
                var resourceList = ResourceList.Where(c => c.ID == tempResource.ResourceID);
                //判断控制资源的父节点是否是当前页面
                foreach (var resource in resourceList)
                {

                    if (resource == null || resource.ParentID != currentPageID) continue;
                    ////！设置当前页面的ResourceID的值！
                    //auditWorkFlowResource.ResourceID = resource.ID;

                    allAuditWorkFlowResourceList.Add(auditWorkFlowResource);
                    if (isAdmin == 1 || auditWorkFlowResource.UserID == userID ||
                        authedRoleList.Contains(auditWorkFlowResource.RoleID))
                    {
                        authedAuditWorkFlowResourceList.Add(auditWorkFlowResource);
                    }

                    break;
                }
            }

            #endregion

            //组织返回值：【key 为当前页面下被控制的控件的Resource的ID】
            //【value 封装了该控制资源下，当前用户所拥有的所有的审批流程&其它信息（详情请见具体定义）】
            Dictionary<int, UserAuditWorkFlowResource> resultDic = new Dictionary<int, UserAuditWorkFlowResource>();

            foreach (var auditWorkFlowResource in authedAuditWorkFlowResourceList)
            {
                //获取受空资源的ResourceID
                int key = auditWorkFlowResource.ResourceID;
                auditWorkFlowResource.IsFinalStep = auditWorkFlowResource.ParentGroupID == 0;
                UserAuditWorkFlowResource currUserAuditWorkFlowResouce;

                if (resultDic.ContainsKey(key))//返回值中已经包含该资源类型的流程
                {
                    //获取控制封装对象 设置IsFinalStep的值
                    currUserAuditWorkFlowResouce = resultDic[key];
                    if (!currUserAuditWorkFlowResouce.IsFinalStep && auditWorkFlowResource.IsFinalStep)
                    {
                        currUserAuditWorkFlowResouce.IsFinalStep = true;
                    }

                    currUserAuditWorkFlowResouce.AuthedAuditWorkFlowResources.Add(auditWorkFlowResource.ID, auditWorkFlowResource);//添加审批流程对象
                }
                else
                {
                    //根据审批流程，生成封装对象
                    currUserAuditWorkFlowResouce = new UserAuditWorkFlowResource
                    {
                        ControlResouce = AllResourceList.Find(c => c.ID == key),
                        IsFinalStep = auditWorkFlowResource.ParentGroupID == 0
                    };

                    currUserAuditWorkFlowResouce.AuthedAuditWorkFlowResources.Add(auditWorkFlowResource.ID, auditWorkFlowResource);//添加审批流程对象
                    currUserAuditWorkFlowResouce.TotalAuditWorkFlowResources.AddRange(allAuditWorkFlowResourceList);

                    //加入字典
                    resultDic.Add(key, currUserAuditWorkFlowResouce);
                }
            }

            return resultDic;
        }

        /// <summary>
        /// 根据用户名获取用户ID
        /// 找不到返回-1
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private static int GetUserIdByUserName(string userName)
        {
            var temp = AllUserInfoList.FirstOrDefault(c => c.UserName == userName);
            if (temp == null)
            {
                return -1;
            }
            return temp.ID;
        }

        /// <summary>
        /// 初始化审批流程信息
        /// </summary>
        private static void InitAuditWorkFlowResource()
        {
            const string sql = "UP_GetAuditWorkFlowResourceList";
            var dataset = SqlHelper.ExecuteDataset(SecurityConnString, CommandType.StoredProcedure, sql);
            //获取所有权限
            _allUserRoleList = EntityBuilder.BuildEntityList<UserRole>(dataset.Tables[0]);
            //获取所有审批流程信息
            _allAuditWorkFlowResourceList = EntityBuilder.BuildEntityList<AuditWorkFlowResource>(dataset.Tables[1]);
            //获取所有人员信息
            _allUserInfoList = EntityBuilder.BuildEntityList<UserInfo>(dataset.Tables[2]);
        }

        private static double ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            //你想转的格式
            return ts3.TotalMinutes;
        }

        #endregion

    }
}
