
using System;
using System.Collections.Generic;
using System.Linq;
using Com.BaseLibrary.Common.Security;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.ExceptionHandle;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataAccess;
using System.Transactions;
using Jufine.Backend.IM.ServiceContracts;
using Com.BaseLibrary.Contract;
using Jufine.Backend.TM.ServiceContracts;
using UserMerchant = Jufine.Backend.Security.DataContracts.UserMerchant;
using System.Text;

namespace Jufine.Backend.Security.Business
{
    public class MerchantBL : IMerchantService
    {

        public MerchantInfo Get(Int32 id)
        {
            try
            {
                return MerchantDA.DAO.Get(id);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void CreateHistoryDetail(MerchantInfoHistoryDetail merchantInfoHistoryDetail)
        {
            try
            {
                bool isExist = UVMerchantInfoDA.DAO.IsExsit(merchantInfoHistoryDetail);
                if (!isExist)
                {
                    throw new BizException("商家名称或组织机构代码或税务登记号已存在");
                }
                MerchantInfoHistoryDetailDA.DAO.Create(merchantInfoHistoryDetail);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public MerchantInfoHistoryDetail CopyToMerchantInfoHistoryDetail(MerchantInfo source, MerchantInfoHistoryDetail target)
        {
            try
            {
                return MerchantDA.DAO.CopyToMerchantInfoHistoryDetail(source, target);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void UpdateHistoryDetail(MerchantInfoHistoryDetail merchantInfoHistoryDetail)
        {
            try
            {
                bool isExist = UVMerchantInfoDA.DAO.IsExsit(merchantInfoHistoryDetail);
                if (!isExist)
                {
                    throw new BizException("商家名称或组织机构代码或税务登记号已存在");
                }

                MerchantInfoHistoryDetailDA.DAO.Update(merchantInfoHistoryDetail);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public UVMerchantInfo GetUVMerchantInfo(int id, string currentUserName = "")
        {
            try
            {
                var res = UVMerchantInfoDA.DAO.GetUVMerchantInfo(id);
                if (res != null && !string.IsNullOrEmpty(currentUserName))
                {
                    res.IsSelfEdit = currentUserName == res.EditUser;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public UVMerchantInfo GetUVMerchantInfo(int id, int merchantId, string currentUserName = "")
        {
            try
            {
                var res = UVMerchantInfoDA.DAO.GetUVMerchantInfo(id, merchantId);
                if (res != null && !string.IsNullOrEmpty(currentUserName))
                {
                    res.IsSelfEdit = currentUserName == res.EditUser;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public UVMerchantInfo GetUVMerchantInfoByMerchantID(int merchantId, string currentUserName = "")
        {
            try
            {
                var res = UVMerchantInfoDA.DAO.GetUVMerchantInfoByMerchantID(merchantId);
                if (res != null && !string.IsNullOrEmpty(currentUserName))
                {
                    res.IsSelfEdit = currentUserName == res.EditUser;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public QueryResultInfo<UVMerchantInfo> QueryUVMerchantInfo(QueryConditionInfo<UVMerchantInfo> queryCondition, IUser currentUser, string ctlName, int currentPageID, List<Com.BaseLibrary.Common.Security.Resource> pageControlResources)
        {
            try
            {
                var allMerchantsInHistory = new MerchantInfoHistoryDetailBL().GetAllDistinctMerchantIDList();

                var res = UVMerchantInfoDA.DAO.GetTotalUVMerchantInfo(currentUser, queryCondition);

                if (res != null && res.Count > 0 && !string.IsNullOrEmpty(currentUser.UserName))
                {
                    foreach (var umi in res)
                    {
                        umi.IsSelfEdit = umi.EditUser == currentUser.UserName;
                        string auditUser = umi.AuditUser;
                        UserAuditWorkFlowResource currUserAuditWorkFlowResource;

                        bool hasAuth = HasAuth(auditUser, currentUser, currentPageID, ctlName, pageControlResources, out currUserAuditWorkFlowResource);
                        if (umi.Status == 2)//"草稿"
                        {
                            umi.S_Status = umi.IsSelfEdit ? umi.Status : umi.MI_Status;
                            umi.S_Strstatus = umi.IsSelfEdit ? umi.Strstatus : umi.MI_Strstatus;
                        }
                        else if (umi.Status == 4 && (umi.IsSelfEdit || hasAuth))//"待审核"
                        {
                            umi.S_Status = umi.Status;
                            umi.S_Strstatus = umi.Strstatus;
                        }
                        else
                        {
                            umi.S_Status = umi.MI_Status;
                            umi.S_Strstatus = umi.MI_Strstatus;
                        }

                        bool isGetCurrValue = (umi.S_Status == 2 && umi.IsSelfEdit) || (umi.S_Status == 4 && (hasAuth || umi.IsSelfEdit));

                        umi.S_MerchantID = !isGetCurrValue ? umi.MI_ID : umi.MerchantID;
                        umi.S_MerchantName = !isGetCurrValue ? umi.MI_MerchantName : umi.MerchantName;
                        umi.S_CooperationMode = !isGetCurrValue ? umi.MI_CooperationMode : umi.CooperationMode;
                        umi.S_ServicePhone = !isGetCurrValue ? umi.MI_ServicePhone : umi.ServicePhone;
                        umi.S_OrganizationCode = !isGetCurrValue ? umi.MI_OrganizationCode : umi.OrganizationCode;
                        umi.S_Telephone = !isGetCurrValue ? umi.MI_Telephone : umi.Telephone;
                        umi.S_ContactPerson1 = !isGetCurrValue ? umi.MI_ContactPerson1 : umi.ContactPerson1;
                        umi.S_EditUser = !isGetCurrValue ? umi.MI_EditUser : umi.EditUser;
                        umi.S_EditDate = !isGetCurrValue ? umi.MI_EditDate : umi.EditDate;
                        umi.S_CreateDate = !isGetCurrValue ? umi.MI_CreateDate : umi.CreateDate;

                        umi.S_StrEditDate = umi.S_EditDate == null ? "" : ((DateTime)umi.S_EditDate).ToString("yyyy-MM-dd HH:mm:ss");

                        umi.S_StatusToolTipText = "【当前状态：" + umi.MI_Strstatus + "】\r\n【编辑状态：" + umi.Strstatus + "】";

                        if (umi.S_Status == 4)
                        {
                            umi.HasAuth = currUserAuditWorkFlowResource.HasAuth;
                            umi.IsFinalStep = currUserAuditWorkFlowResource.IsFinalStep;
                            umi.CurrentStep = currUserAuditWorkFlowResource.CurrentAuditStep;
                            umi.WaitAuditStep = currUserAuditWorkFlowResource.WaitAuditStep;
                            umi.TotalAuditStepCount = currUserAuditWorkFlowResource.TotalAuditStepCount;
                            umi.TotalAuditWorkFlowStepsStr = currUserAuditWorkFlowResource.TotalAuditWorkFlowStepsStr;
                            umi.S_AuditVisual = umi.HasAuth;
                        }
                    }
                }

                res = UVMerchantInfoDA.DAO.SetWhereClause(queryCondition, res).ToList();
                var result = new QueryResultInfo<UVMerchantInfo> { RecordCount = res.Count };
                res = UVMerchantInfoDA.DAO.SetOrder(queryCondition, res).ToList();

                if (queryCondition.ReturnAllData)
                {
                    result.RecordList = res;
                    return result;
                }

                int startRowIndex = (queryCondition.PageIndex - 1) * queryCondition.PageSize;

                if (startRowIndex > result.RecordCount)//如果起始位置大于总记录数，取最后一页
                {
                    startRowIndex = Math.Max(0, result.RecordCount - queryCondition.PageSize);
                    result.RecordList = res.Skip(startRowIndex).ToList();
                }
                else
                {
                    int pageSize = Math.Min((result.RecordCount - startRowIndex), queryCondition.PageSize);
                    result.RecordList = res.Skip(startRowIndex).Take(pageSize).ToList();
                }

                foreach (var umi in result.RecordList)
                {

                    umi.S_AuditToolTip = "当前用户进行第【" + umi.TotalAuditWorkFlowStepsStr + "】级审批；\r\n当前记录等待第【" + umi.WaitAuditStep + "】级审批；\r\n记录总共要进行【" + umi.TotalAuditStepCount + "】级审批；";
                    StringBuilder sb = new StringBuilder("MerchantSearchAuditMgmt.aspx?");
                    sb.Append("OPType=").Append("audit").Append("&");
                    sb.Append("CtlName=").Append(ctlName).Append("&");
                    sb.Append("PageID=").Append(currentPageID).Append("&");
                    sb.Append("HasAuth=").Append(umi.HasAuth ? 1 : 0).Append("&");
                    sb.Append("ID=").Append(umi.ID).Append("&");
                    sb.Append("MerchantID=").Append(umi.MerchantID).Append("&");
                    sb.Append("FromHistory=").Append(umi.FromHistory);
                    umi.S_AuditURL = sb.ToString();

                    umi.S_HistoryVisual = allMerchantsInHistory.Contains(umi.MerchantID);
                    if (umi.S_HistoryVisual)
                    {
                        umi.S_HistoryURL = "MerchantInfoHistoryDetailMgmt.aspx?MerchantID=" + umi.MerchantID;
                    }

                    umi.S_ShowUser = umi.S_Status == 1;

                    sb = new StringBuilder("MerchantSearchAuditMgmt.aspx?");
                    sb.Append("OPType=").Append("view").Append("&");
                    sb.Append("ID=").Append(umi.ID).Append("&");
                    sb.Append("MerchantID=").Append(umi.MerchantID).Append("&");
                    sb.Append("FromHistory=").Append(umi.FromHistory);

                    umi.S_ViewURL = sb.ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        private static bool HasAuth(string auditUser, IUser currentUser, int currentPageID, string ctlName, List<Com.BaseLibrary.Common.Security.Resource> pageControlResources, out UserAuditWorkFlowResource userAuditWorkFlowResource)
        {
            userAuditWorkFlowResource = currentUser.GetUserAuditWorkFlowResource(ctlName, auditUser, currentPageID, pageControlResources);
            return userAuditWorkFlowResource.HasAuth;
        }

        public void Delete(Int32 id, string editUser)
        {
            try
            {
                MerchantDA.DAO.Delete(id, editUser);
                MerchantAdditionalCertificateDA.DAO.Delete(id, editUser);
                UserDA.DAO.DeleteByMerchantID(id, editUser);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void BatchDelete(List<Int32> keyList, string editUser)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    MerchantDA.DAO.BatchChangeStatus(keyList, -1, editUser);
                    MerchantAdditionalCertificateDA.DAO.BatchDelete(keyList, editUser);
                    UserDA.DAO.BatchDeleteByMerchantID(keyList, editUser);
                    IShippingFeeRateService shippingFeeRate = ServiceFactory.CreateService<IShippingFeeRateService>();
                    shippingFeeRate.DeleteRate(keyList);
                    IItemService itemService = ServiceFactory.CreateService<IItemService>();
                    foreach (int id in keyList)
                    {
                        itemService.BatchLogicDeleteByMerchantID(id, editUser);
                    }
                    ts.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<MerchantInfo> GetAll()
        {
            try
            {
                return MerchantDA.DAO.GetAll();
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public bool Create(MerchantInfo merchant, int userID)
        {
            try
            {
                bool isExist = UVMerchantInfoDA.DAO.IsExsit(merchant);
                if (!isExist)
                {
                    throw new BizException("商家名称或组织机构代码或税务登记号已存在");
                }
                if (userID > 0)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        MerchantDA.DAO.Create(merchant);
                        UserMerchant um = new UserMerchant
                        {
                            MerchantID = merchant.ID,
                            UserID = userID,
                            CreateUser = merchant.CreateUser,
                            EditDate = merchant.CreateDate,
                            CreateDate = merchant.CreateDate,
                            EditUser = merchant.CreateUser
                        };
                        UserMerchantBL userMerchantBL = new UserMerchantBL();
                        userMerchantBL.Create(um);
                        ts.Complete();
                    }
                }
                else
                {
                    MerchantDA.DAO.Create(merchant);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public bool Update(MerchantInfo merchant)
        {
            try
            {
                bool isExist = UVMerchantInfoDA.DAO.IsExsit(merchant);
                if (!isExist)
                {
                    throw new BizException("商家名称或组织机构代码或税务登记号已存在");
                }
                if (merchant.Status == 0)
                {
                    List<int> idList = new List<int> { merchant.ID };
                    UserDA.DAO.BatchChangeStatusByMerchantID(idList, 0, merchant.EditUser);
                }
                MerchantDA.DAO.Update(merchant);
                return true;
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public QueryResultInfo<MerchantInfo> Query(QueryConditionInfo<MerchantInfo> queryCondition)
        {
            try
            {
                return MerchantDA.DAO.Query(queryCondition);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void BatchChangeStatus(List<int> idList, int status, string editUser)
        {
            try
            {
                if (status == 0)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        UserDA.DAO.BatchChangeStatusByMerchantID(idList, 0, editUser);
                        IItemService itemService = ServiceFactory.CreateService<IItemService>();
                        foreach (int id in idList)
                        {
                            itemService.BatchLogicDeleteByMerchantID(id, editUser);
                        }
                        ts.Complete();
                    }
                }
                MerchantDA.DAO.BatchChangeStatus(idList, status, editUser);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public void BatchChangeStatusMerchant(List<int> idList, int status, string editUser, int fromHistory = 0)
        {
            try
            {
                MerchantDA.DAO.BatchChangeStatusMerchant(idList, status, editUser, fromHistory);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }

        }

        public void ChangeStatusMerchant(int id, int status, string editUser, int fromHistory = 0)
        {
            try
            {
                MerchantDA.DAO.ChangeStatusMerchant(id, status, editUser, fromHistory);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }

        }
        public void BatchDeleteMerchant(List<int> keyList, string editUser)
        {
            try
            {
                MerchantDA.DAO.BatchDeleteMerchant(keyList, editUser);
            }
            catch (Exception ex)
            {

                throw ExceptionFactory.BuildException(ex);
            }
        }

        public MerchantInfo GetByName(string merchantName)
        {
            try
            {
                return MerchantDA.DAO.GetByName(merchantName);
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<MerchantInfo> GetActiveMerchant()
        {
            try
            {
                return MerchantDA.DAO.GetActiveMerchant();
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }

        public List<MerchantInfo> GetAllMerchant()
        {
            try
            {
                return MerchantDA.DAO.GetAllMerchant();
            }
            catch (Exception ex)
            {
                throw ExceptionFactory.BuildException(ex);
            }
        }


    }
}
