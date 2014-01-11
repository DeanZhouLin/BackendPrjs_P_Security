using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

using Com.BaseLibrary.Utility;

using System.Data;
using System.Reflection;
using System.Collections;
using System.Drawing;
using System.Text;
using Jufine.Backend.WebModel;
using Jufine.Backend.Security.ServiceContracts;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.BaseData.ServiceContracts;
using Jufine.Backend.BaseData.DataContracts;


namespace Jufine.Backend.Security.WebUI
{
    public partial class ResourceMgmt : PageBase
    {

        #region Private Variables

        //用于调度areaDA的方法操作数据库
        private IResourceService resourceService = CreateService<IResourceService>();
        //用于存储顶级结点下的所有一级节点（eg.中国下面的所有省市）
        private List<Resource> resourceList;
        //临时存储连接好的字符串（escape is ','）
        private StringBuilder sb = new StringBuilder();
        #endregion

        #region Event Handlers

        /// <summary>
        /// 页面加载事件，只加载顶级结点下的所有一级节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                resourceList = resourceService.GetAll();
                RefreshResourceTree();
                CollapseAllTreeNode();
                BindResourceType();
                ckbShowInMenu.Checked = true;
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnSave.Text = string.IsNullOrEmpty(hdfResourceId.Value) ? "保存" : "更新";
        }
        private void CollapseAllTreeNode()
        {
            foreach (TreeNode node in tvResource.Nodes)
            {
                node.CollapseAll();
            }
        }

        /// <summary>
        /// 用于刷新当前树节点（用于显示更新数据数据后的效果）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshResourceTree();
        }

        /// <summary>
        /// 用于删除当前节点以及所有的子孙
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (tvResource.SelectedNode == null)
            {
                ShowMessageBox("请选择节点进行删除。");
                return;
            }
            if (tvResource.SelectedNode.ChildNodes.Count > 0)
            {
                ShowMessageBox("存在子节点，不能删除。");
                return;
            }




            try
            {
                int resrouceId = Converter.ToInt32(tvResource.SelectedNode.Value, -1);
                resourceService.Delete(resrouceId);
                if (tvResource.SelectedNode.Parent == null)
                {
                    tvResource.Nodes.Remove(tvResource.SelectedNode);
                }
                else
                {
                    tvResource.SelectedNode.Parent.ChildNodes.Remove(tvResource.SelectedNode);
                }
                ShowMessageBox("删除成功。");
                ResetContent();
            }

            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }


        /// <summary>
        /// 用于修改当前节点的相关信息，以及所有的子孙的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateControl(panel1))
            {
                return;
            }
            else if (string.IsNullOrEmpty(txtParentResourceName.Text))
            {
                ShowMessageBox("当前未选中节点，无法新建结点。");
                return;
            }
            else
            {
                try
                {
                    int result = 0;
                    if (string.IsNullOrEmpty(hdfResourceId.Value))
                    {
                        Resource resource = new Resource();
                        FillEntityWithContentValue(resource, panel1);
                        resource.ParentID = Converter.ToInt32(hdfParentResourceId.Value, 0);
                        resource.CreateUser = resource.EditUser = CurrentUser.UserName;
                        resource.CreateDate = resource.EditDate = DateTime.Now;
                        resource.Status = ckbStatus.Checked ? 1 : 0;
                        resource.ShowInMenu = ckbShowInMenu.Checked ? 1 : 0;
                        resource.ResourceType = Converter.ToInt32(rblResourceType.SelectedValue, 1);
                        GetResourcePath(hdfParentResourceId.Value);
                        resource.Path = path;
                        result = resourceService.Create(resource);
                        if (result > 0)
                        {
                            ShowMessageBox("新建资源成功。");
                        }
                        else
                        {
                            ShowMessageBox("资源名称不能重复。");
                        }
                    }
                    else
                    {
                        int resoruceId = Converter.ToInt32(tvResource.SelectedNode.Value, -1);
                        Resource resource = resourceService.Get(resoruceId);
                        FillEntityWithContentValue<Resource>(resource, panel1);
                        resource.Status = ckbStatus.Checked ? 1 : 0;
                        resource.ShowInMenu = ckbShowInMenu.Checked ? 1 : 0;
                        resource.ResourceType = Converter.ToInt32(rblResourceType.SelectedValue, 1);
                        GetResourcePath(resource.ParentID.ToString());
                        resource.Path = path;
                        if (resource.ParentID == resource.ID)
                        {
                            ShowMessageBox("更新失败，自己不能为自己的子节点。");
                        }
                        else
                        {
                            resource.EditUser = CurrentUser.UserName;
                            resource.EditDate = DateTime.Now;
                            if (resourceService.Update(resource))
                            {
                                ShowMessageBox("更新资源修改成功。");
                                TreeNode node = FindTreeNodeByNodeValue(tvResource.Nodes, resoruceId.ToString());
                                if (resource.Status == 1 && !string.IsNullOrEmpty(resource.Path))
                                {
                                    List<string> idList = resource.Path.Split('/').ToList();
                                    resourceService.BatchChangeStatus(idList, 1);
                                }
                                else
                                {
                                    if (resource.Status != 1)
                                    {
                                        GetAllChildNodesByNode(node);
                                        if (childIdList != null && childIdList.Count > 0)
                                        {
                                            resourceService.BatchChangeStatus(childIdList, 0);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ShowMessageBox("资源名称不能重复。");
                            }
                        }
                    }
                    RefreshResourceTree();
                    if (result > 0)
                    {
                        SelectTreeNodeByNodeValue(tvResource.Nodes, result.ToString());
                    }
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message);
                }
            }
        }
        private string path = string.Empty;
        private void GetResourcePath(string id)
        {
            if (id != "0")
            {
                path = id + "/" + path;
                TreeNode node = FindTreeNodeByNodeValue(tvResource.Nodes, id);
                if (node.Parent == null)
                {
                    return;
                }
                GetResourcePath(node.Parent.Value);
            }

        }

        private List<string> childIdList = new List<string>();
        private void GetAllChildNodesByNode(TreeNode node)
        {
            if (node.ChildNodes != null)
            {
                foreach (TreeNode n in node.ChildNodes)
                {
                    childIdList.Add(n.Value);
                    GetAllChildNodesByNode(n);
                }
            }
        }

        private TreeNode SelectTreeNodeByNodeValue(TreeNodeCollection tnc, string id)
        {
            foreach (TreeNode node in tnc)
            {
                if (node.Value == id)
                {
                    node.Select();
                    ExpandToNode(node);
                    return node;
                }
                TreeNode findNode = SelectTreeNodeByNodeValue(node.ChildNodes, id);
                if (findNode != null)
                {
                    return findNode;
                }
            }
            return null;
        }

        private TreeNode FindTreeNodeByNodeValue(TreeNodeCollection tnc, string id)
        {
            foreach (TreeNode node in tnc)
            {
                if (node.Value == id)
                {
                    return node;
                }
                TreeNode findNode = FindTreeNodeByNodeValue(node.ChildNodes, id);
                if (findNode != null)
                {
                    return findNode;
                }
            }
            return null;
        }

        private void BindResourceType()
        {
            ICodeValueService service = CreateService<ICodeValueService>();
            List<CodeValueInfo> codeValueList = new List<CodeValueInfo>();
            codeValueList = service.GetCodeListByGroupCode("Resource_ResourceType");
            rblResourceType.DataSource = codeValueList;
            rblResourceType.DataBind();
            //rblResourceType.SelectedIndex = 0;
        }


        /// <summary>
        /// 用于处理大数据量的树状结构的动态加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvResource_SelectedNodeChanged(object sender, EventArgs e)
        {
            hdfParentResourceId.Value = hdfResourceId.Value = tvResource.SelectedNode.Value;
            int resourceId = int.Parse(tvResource.SelectedNode.Value);
            Resource resource = resourceService.Get(resourceId);
            FillContentValueWithEntity(resource, panel1);
            rblResourceType.SelectedValue = resource.ResourceType.ToString();
            ckbShowInMenu.Checked = resource.ShowInMenu == 1;
            ckbStatus.Checked = resource.Status == 1;
            Resource resouceTemp = GetParentResourceNameByID(resource.ID);
            txtParentResourceName.Text = resouceTemp == null ? "根节点" : resouceTemp.DisplayName;
        }

        #endregion

        #region Private Methods



        /// <summary>
        /// 构建树结构
        /// </summary>
        /// <param name="parentTreeNode">父节点</param>
        /// <param name="childResourceList">子节点列表</param>
        private void AddTreeNodes(object parentTreeNode, List<Resource> allResourceList)
        {
            int resourceId = 0;
            TreeNodeCollection childNodes = null;
            if (parentTreeNode is TreeView)
            {
                childNodes = (parentTreeNode as TreeView).Nodes;
            }
            else
            {
                resourceId = int.Parse((parentTreeNode as TreeNode).Value);
                childNodes = (parentTreeNode as TreeNode).ChildNodes;
            }

            foreach (Resource area in allResourceList)
            {
                if (area.ParentID == resourceId)
                {
                    TreeNode node = new TreeNode();
                    node.Value = area.ID.ToString();
                    node.ToolTip = area.DisplayOrder.ToString();
                    node.Text = string.Format(node.ToolTip + "." + area.DisplayName + (area.Status == 0 ? "（禁用）" : ""), area.DisplayName);
                    node.SelectAction = TreeNodeSelectAction.SelectExpand;
                    AddTreeNodes(node, allResourceList);
                    childNodes.Add(node);
                }

            }
        }

        /// <summary>
        /// 刷新整个ResourceTree（用于显示更新数据数据后的效果）
        /// </summary>
        private void RefreshResourceTree()
        {
            int currentResourceID = 0;
            if (tvResource.SelectedNode != null)
            {
                currentResourceID = int.Parse(tvResource.SelectedNode.Value);
            }

            tvResource.Nodes.Clear();
            resourceList = resourceService.GetAll();
            resourceList = resourceList.OrderBy(c => c.DisplayOrder).ToList();
            AddTreeNodes(tvResource, resourceList);
            //BindParentCategory();
            TreeNode node = FindNodeByResourceId(tvResource, currentResourceID);
            if (node != null)
            {
                node.Select();
                ExpandToNode(node);
            }


        }

        private void ExpandToNode(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Expand();
                ExpandToNode(node.Parent);
            }
        }

        //protected void btnNew_Click(object sender, EventArgs e)
        //{
        //    hdfResourceId.Value = string.Empty;
        //    ResetContent();
        //}
        protected void btnNewRootNode_Click(object sender, EventArgs e)
        {
            CreateNode(1);
        }
        protected void btnNewAdjacentNode_Click(object sender, EventArgs e)
        {
            CreateNode(2);
        }
        protected void btnNewChildNode_Click(object sender, EventArgs e)
        {
            CreateNode(3);
        }

        private void CreateNode(int createType)
        {
            hdfResourceId.Value = string.Empty;
            ResetContent();

            if (createType == 1)
            {
                hdfParentResourceId.Value = "0";
                txtParentResourceName.Text = "根节点";
            }
            else
            {
                TreeNode node = tvResource.SelectedNode;
                if (node == null)
                {
                    ShowMessageBox("请选择一个节点。");
                    return;
                }
                Resource resource = null;
                if (createType == 2)
                {
                    resource = GetParentResourceNameByID(Converter.ToInt32(node.Value, 0));
                }
                else
                {
                    resource = resourceService.Get(Converter.ToInt32(node.Value, 0));
                }

                hdfParentResourceId.Value = resource == null ? "0" : resource.ID.ToString();
                txtParentResourceName.Text = resource == null ? "根节点" : resource.DisplayName;
            }
        }

        //private void BindParentCategory()
        //{
        //    List<Resource> rootResourceList = resourceList.FindAll(c => c.ParentID == 0);
        //    rootResourceList.Insert(0, new Resource { ID = 0, DisplayName = "根资源" });
        //    ddlParentResourceID.DataSource = rootResourceList;
        //    ddlParentResourceID.DataBind();
        //}

        private TreeNode FindNodeByResourceId(Object parentTreeNode, int currentResourceID)
        {

            TreeNodeCollection childNodes = null;
            if (parentTreeNode is TreeView)
            {
                childNodes = (parentTreeNode as TreeView).Nodes;
            }
            else
            {
                childNodes = (parentTreeNode as TreeNode).ChildNodes;
            }
            foreach (TreeNode treeNode in childNodes)
            {
                if (currentResourceID == int.Parse(treeNode.Value))
                {
                    return treeNode;
                }
                else
                {
                    TreeNode findTreeNode = FindNodeByResourceId(treeNode, currentResourceID);
                    if (findTreeNode != null)
                    {
                        return findTreeNode;
                    }
                }
            }
            return null;
        }

        private Resource GetParentResourceNameByID(int id)
        {
            return resourceService.GetParentResource(id);
        }

        /// <summary>
        ///  重置相关组件的值
        /// </summary>
        private void ResetContent()
        {
            txtResourceAddress.Text = txtDisplayName.Text = txtResourceName.Text = txtDisplayOrder.Text = txtParentResourceName.Text = string.Empty;
            ckbShowInMenu.Checked = ckbStatus.Checked = true;
        }


        /// <summary>
        /// 用于删除当前节点以及所有的子孙
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIFunctionResource_Click(object sender, EventArgs e)
        {
            try
            {
                //List<Resource> list =  ExcelUtil.GetEntityListFromFile<Resource>(@"D:\work\resource.xls");
                List<Resource> list = ExcelUtil.GetEntityListFromFile<Resource>(GetServerUploadFile("resource.xls", "ResourceUploadPath"));
                //ShowMessageBox(list.Count.ToString());
                //return;
                if (list != null && list.Count > 0)
                {
                    IResourceService service = CreateService<IResourceService>();
                    Resource resource = null;
                    int i = 0;
                    foreach (Resource res in list)
                    {
                        resource = new Resource();
                        resource.ParentID = res.ParentID;
                        resource.ResourceName = res.ResourceAddress.Substring(3) + res.ResourceName;
                        resource.DisplayName = res.DisplayName;
                        resource.DisplayOrder = res.DisplayOrder;
                        resource.ResourceAddress = res.ResourceAddress;
                        resource.ShowInMenu = 0;
                        resource.ResourceType = 2;
                        resource.Path = "hz导入";
                        resource.Status = 1;
                        resource.CreateUser = CurrentUser.UserName;
                        resource.CreateDate = DateTime.Now;
                        service.Create(resource);
                        i++;
                    }
                    ShowMessageBox("导入成功" + i + "条");
                }
                else
                {
                    ShowMessageBox("excel没数据。");
                }
            }
            catch (Exception ex)
            {

                ShowMessageBox(ex.ToString());
            }
        }

        #endregion

    }
}