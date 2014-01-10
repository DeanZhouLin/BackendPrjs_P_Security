using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.BaseLibrary.Contract;
using Com.BaseLibrary.Entity;
using Jufine.Backend.IM.Business;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.WebModel;
using Jufine.Backend.Security.ServiceContracts;
using Resource = Jufine.Backend.Security.DataContracts.Resource;

namespace Jufine.Backend.Security.WebUI
{
    public partial class AuditWorkFlowResourceMgmt : PageBase
    {

        private QueryConditionInfo<UVAuditWorkFlow> UVQueryCondition
        {
            get
            {
                QueryConditionInfo<UVAuditWorkFlow> queryCondition = ViewState["UVAUDITWORKFLOWRESOURCE_QUERYCONDITION"] as QueryConditionInfo<UVAuditWorkFlow>;
                if (queryCondition != null) return queryCondition;
                queryCondition = new QueryConditionInfo<UVAuditWorkFlow>();
                ViewState["UVAUDITWORKFLOWRESOURCE_QUERYCONDITION"] = queryCondition;
                return queryCondition;
            }
        }

        private int? currentRowIndex;
        public int CurrentRowIndex
        {
            get
            {
                if (currentRowIndex != null) return currentRowIndex.Value;
                object rowIndex = ViewState["CURRENTROWINDEX"];
                currentRowIndex = rowIndex == null ? 0 : int.Parse(rowIndex.ToString());
                return currentRowIndex.Value;
            }
            set
            {
                ViewState["CURRENTROWINDEX"] = currentRowIndex = value;
            }
        }

        private static IUVAuditWorkFlowService uVAuditWorkFlowService;
        private static IUVAuditWorkFlowService UVAuditWorkFlowService
        {
            get { return uVAuditWorkFlowService ?? (uVAuditWorkFlowService = CreateService<IUVAuditWorkFlowService>()); }
        }

        private static IAuditWorkFlowResourceService auditWorkFlowResourceService;
        private static IAuditWorkFlowResourceService AuditWorkFlowResourceService
        {
            get { return auditWorkFlowResourceService ?? (auditWorkFlowResourceService = CreateService<IAuditWorkFlowResourceService>()); }
        }

        private static IUserService userService;
        private static IUserService UserService
        {
            get { return userService ?? (userService = CreateService<IUserService>()); }
        }

        private static IResourceService resourceService;
        private static IResourceService ResourceService
        {
            get { return resourceService ?? (resourceService = CreateService<IResourceService>()); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CoreExecAction(QueryData);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnPreviousItem.Enabled = CurrentRowIndex > 0;
            btnNextItem.Enabled = CurrentRowIndex < (gvAuditWorkFlowResourceList.Rows.Count - 1);
        }

        //查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            listPager.CurrentPageIndex = 1;
            FillEntityWithContentValue(UVQueryCondition.Condtion, plHeader);

            CoreExecAction(QueryData);
        }

        //新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                ShowUpdateTitle(lblTitle, upTitle, "新增多级审批资源");
                ShowDetail(null, false, true, false);
            });
        }

        //批量删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBatchDelete_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                List<Int32> keyList = GetGVCheckedRowList(gvAuditWorkFlowResourceList);
                if (keyList.Count == 0)
                {
                    ShowMessageBox("请至少选择一条记录。");
                    return;
                }
                AuditWorkFlowResourceService.BatchDelete(keyList);
            }, QueryData, "删除成功");
        }

        //编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                CurrentRowIndex = GetValueByPropName(sender, "RowIndex").ParseInt();
                ShowUpdateTitle(lblTitle, upTitle, "编辑多级审批资源");
                ShowDetail(GetValueByPropName(sender, "CommandArgument"), true, false, true);
            });
        }

        //删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                int resourceID = GetValueByPropName(sender, "CommandArgument").ParseInt();
                AuditWorkFlowResourceService.Delete(resourceID);
            }, QueryData, "删除成功");
        }

        //树SelectedChanged
        /// <summary>
        /// 树SelectedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void treeAuth_TreeNodeSelectedChanged(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                var selectNode = GetValueByPropName(sender, "SelectedNode") as TreeNode;
                txtResourceName.Text = selectNode != null ? selectNode.Text : "";
                hdResourceID.Value = selectNode != null ? selectNode.Value : "";
                ShowDetail(hdResourceID.Value, true, true);
            });
        }

        //树NodeCheckChanged
        /// <summary>
        /// 树NodeCheckChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void treeAuth_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }

        //分配权限
        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddAuditUserForAuditWorkFlow_Click(object sender, EventArgs e)
        {
            int resourceID = hdResourceID.Value.ParseInt();
            string userName = sddlUserInfo.SelectedItem.Text;
            string userID = sddlUserInfo.SelectedItem.Value;
            int auditLevel = sddlAuditLevel.SelectedValue.ParseInt();

            CoreExecAction(() =>
               {
                   int msgNumber;
                   string msg;
                   AuditWorkFlowResourceService.AddAuditUserForAuditWorkFlow(resourceID, userName, auditLevel, CurrentUser.UserName, out msg, out msgNumber);
                   ShowDetail(resourceID, true);
                   sddlUserInfo.SelectedValue = userID;
                   ShowMessageBox(msg);
               });
        }

        //删除权限
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDeleteAuditUserForAuditWorkFlow_Click(object sender, EventArgs e)
        {
            int resourceID = GetValueByPropName(sender, "ResourceID").ParseInt();
            string userName = GetValueByPropName(sender, "UserName").ParseString();

            CoreExecAction(() =>
            {
                int msgNumber;
                string msg;
                AuditWorkFlowResourceService.DeleteAuditUserForAuditWorkFlow(resourceID, userName, CurrentUser.UserName, out msg, out msgNumber);
                ShowDetail(resourceID, true);
                ShowMessageBox(msg);
            });
        }

        //取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            CoreExecAction(() => modalPopupExtender.Hide(), QueryData);
        }

        //上一页
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPreviousItem_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                CurrentRowIndex = PreviousShowAction(gvAuditWorkFlowResourceList, "lnkEdit", ShowDetail, CurrentRowIndex);
            });
        }

        //下一页
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNextItem_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                CurrentRowIndex = NextShowAction(gvAuditWorkFlowResourceList, "lnkEdit", ShowDetail, CurrentRowIndex);
            });
        }

        //全选
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                SelectAllChangedAction(gvAuditWorkFlowResourceList, GetValueByPropName(sender, "Checked").ParseBool());
                upList.Update();
            });
        }

        //排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAuditWorkFlowResourceList_Sorting(object sender, GridViewSortEventArgs e)
        {
            CoreExecAction(() =>
            {
                e.Cancel = true;
                SetSortOrder(UVQueryCondition, e.SortExpression);
                listPager.CurrentPageIndex = 0;
            }, QueryData);
        }

        //分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void listPager_PageChanged(object sender, EventArgs e)
        {
            CoreExecAction(QueryData);
        }


        /// <summary>
        /// 参数{key,queryData,editTree,showPN}
        /// </summary>
        /// <param name="inputObjects"></param>
        private void ShowDetail(params object[] inputObjects)
        {
            ClearControlInput(panelDetailInputArea);

            int key = GetIntValueFromParamsObjects(inputObjects, 1, -1);
            bool? queryData = GetBoolValueFromParamsObjects(inputObjects, 2);
            bool? editTree = GetBoolValueFromParamsObjects(inputObjects, 3);
            bool? showPN = GetBoolValueFromParamsObjects(inputObjects, 4);

            var selNode = TreeViewHelper<Resource>.RefreshTree(treeAuth, ResourceService.GetAll().Where(c => c.Status == 1), upAuthorityTreeView, key);
            CommonBindDDL(sddlUserInfo, UserService.GetAll().Where(c => c.Status == 1));

            var tAuditWorkFlowResourceList = queryData == null || queryData == false ? new List<TAuditWorkFlowResource>() : AuditWorkFlowResourceService.GetTAuditWorkFlowResources(key);

            var maxAuditLevel = tAuditWorkFlowResourceList.Count > 0 ? (tAuditWorkFlowResourceList.Max(c => c.AuditLevel) + 1).ParseInt() : 1;
            CommonBindDDL(sddlAuditLevel, 1, maxAuditLevel, maxAuditLevel.ParseString());

            if (tAuditWorkFlowResourceList.Count > 0)
            {
                gvAuditResourceList.DataSource = tAuditWorkFlowResourceList;
                gvAuditResourceList.DataBind();
            }
            else
            {
                gvAuditResourceList.DataSource = null;
                gvAuditResourceList.DataBind();
            }
            NoRecords<TAuditWorkFlowResource>(gvAuditResourceList);

            hdID.Value = key.ToString();
            if (editTree != null)
            {
                treeAuth.Enabled = (bool)editTree;
            }
            if (showPN != null)
            {
                btnNextItem.Visible = btnPreviousItem.Visible = (bool)showPN;
            }

            txtResourceName.Text = selNode == null ? "" : selNode.Text;
            hdResourceID.Value = key.ToString();

            upDetail.Update();
            upAuditResourceList.Update();

            modalPopupExtender.Show();

        }

        private void QueryData()
        {
            UVQueryCondition.PageIndex = listPager.CurrentPageIndex;
            UVQueryCondition.PageSize = listPager.PageSize;

            QueryResultInfo<UVAuditWorkFlow> result = UVAuditWorkFlowService.Query(UVQueryCondition);

            SetOrderHeaderStyle(gvAuditWorkFlowResourceList, UVQueryCondition);

            gvAuditWorkFlowResourceList.DataSource = result.RecordList;
            gvAuditWorkFlowResourceList.DataBind();

            NoRecords<UVAuditWorkFlow>(gvAuditWorkFlowResourceList);
            listPager.RecordCount = result.RecordCount;
            upList.Update();
        }


        private static class TreeViewHelper<T> where T : DataContractBase
        {
            /// <summary>
            /// 刷新整个ResourceTree（用于显示更新数据数据后的效果）
            /// </summary>
            public static TreeNode RefreshTree(TreeView treeView, IEnumerable<T> totalResourceList, UpdatePanel updatePanel = null, int selectResourceID = -1)
            {

                AddTreeNodes(treeView, totalResourceList);
                TreeNode node = null;
                if (selectResourceID > -1)
                {
                    node = FindNodeByResourceId(treeView, selectResourceID);

                    if (node != null)
                    {
                        node.Select();
                        ExpandToNode(node);
                    }
                }

                if (updatePanel != null)
                {
                    updatePanel.Update();
                }
                return node;
            }

            /// <summary>
            /// 构建树结构
            /// </summary>
            /// <param name="parentTreeNode">父节点</param>
            /// <param name="allResourceList"></param>
            /// <param name="showDic"></param>
            /// <param name="action"></param>
            /// <param name="nValue"></param>
            /// <param name="nParentID"></param>
            /// <param name="nDisplayOrder"></param>
            /// <param name="nDisplayName"></param>
            /// <param name="nStatus"></param>
            private static void AddTreeNodes(
                object parentTreeNode,
                IEnumerable<T> allResourceList,
                Dictionary<int, string> showDic = null,
                TreeNodeSelectAction action = TreeNodeSelectAction.SelectExpand,
                string nValue = "ID",
                string nParentID = "ParentID",
                string nDisplayOrder = "DisplayOrder",
                string nDisplayName = "DisplayName",
                string nStatus = "Status")
            {
                int resourceId = 0;
                TreeNodeCollection childNodes;

                var treeNode = parentTreeNode as TreeNode;
                if (treeNode != null)
                {
                    resourceId = treeNode.Value.ParseInt();
                    childNodes = treeNode.ChildNodes;
                }
                else
                {
                    var treeView = parentTreeNode as TreeView;
                    treeView.Nodes.Clear();
                    childNodes = treeView.Nodes;
                }

                if (showDic == null)
                {
                    showDic = new Dictionary<int, string> { { 0, "禁用" } };
                }

                foreach (var resource in allResourceList.Where(c => c.GetValue(nParentID).ParseInt() == resourceId))
                {
                    var value = resource.GetValue(nValue).ParseString();
                    var name = resource.GetValue(nDisplayName);
                    var order = resource.GetValue(nDisplayOrder).ParseString();
                    var status = resource.GetValue(nStatus).ParseInt();
                    var displayText = order + "." + name + (showDic.ContainsKey(status) ? showDic[status] : "");

                    TreeNode node = new TreeNode
                    {
                        Value = value,
                        ToolTip = order,
                        Text = displayText,
                        SelectAction = action
                    };

                    AddTreeNodes(node, allResourceList, showDic);
                    childNodes.Add(node);
                }
            }

            private static TreeNode FindNodeByResourceId(Object parentTreeNode, int currentResourceID)
            {
                var tv = parentTreeNode as TreeNode;
                TreeNodeCollection childNodes = tv != null ? tv.ChildNodes : (parentTreeNode as TreeView).Nodes;

                foreach (TreeNode treeNode in childNodes)
                {
                    if (currentResourceID == Int32.Parse(treeNode.Value))
                    {
                        return treeNode;
                    }
                    TreeNode findTreeNode = FindNodeByResourceId(treeNode, currentResourceID);
                    if (findTreeNode != null)
                    {
                        return findTreeNode;
                    }
                }
                return null;
            }

            private static void SetParentNode(TreeNode node)
            {
                while (true)
                {
                    if (node.Parent != null)
                    {
                        node.Parent.Checked = true;
                        node = node.Parent;
                        continue;
                    }
                    break;
                }
            }

            private static void SetSelectNode(TreeNodeCollection tnc, bool select)
            {
                if (tnc == null || tnc.Count <= 0) return;
                foreach (TreeNode node in tnc)
                {
                    node.Checked = @select;
                    SetSelectNode(node.ChildNodes, @select);
                }
            }

            private static void SetSelectInverseNode(TreeNodeCollection tnc)
            {
                if (tnc == null || tnc.Count <= 0) return;
                foreach (TreeNode node in tnc)
                {
                    node.Checked = !node.Checked;
                    SetSelectInverseNode(node.ChildNodes);
                }
            }

            private static void ExpandToNode(TreeNode node)
            {
                while (true)
                {
                    if (node.Parent != null)
                    {
                        node.Parent.Expand();
                        node = node.Parent;
                        continue;
                    }
                    break;
                }
            }
        }


    }

}
