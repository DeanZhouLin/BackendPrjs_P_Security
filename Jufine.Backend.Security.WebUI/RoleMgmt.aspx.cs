using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Utility;

using Jufine.Backend.WebModel;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;

namespace Jufine.Backend.Security.WebUI
{
    public partial class RoleMgmt : PageBase
    {
        private QueryConditionInfo<RoleInfo> QueryCondition
        {
            get
            {
                QueryConditionInfo<RoleInfo> queryCondition
                    = ViewState["ROLE_QUERYCONDITION"] as QueryConditionInfo<RoleInfo>;
                if (queryCondition == null)
                {
                    queryCondition = new QueryConditionInfo<RoleInfo>();
                    ViewState["ROLE_QUERYCONDITION"] = queryCondition;
                }
                return queryCondition;
            }
        }
        private List<Resource> m_PageList;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    QueryCondition.Condtion.Status = -1;
                    QueryData();
                    AddEnterEscPress(panelDetailInputArea, btnSave, btnCancel);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                listPager.CurrentPageIndex = 1;
                FillEntityWithContentValue<RoleInfo>(QueryCondition.Condtion,plHeader);
                QueryCondition.Condtion.Status = rbQueryActive.Checked ? 1 : (rbQueryLock.Checked ? 0 : -1);
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            ClearControlInput(panelDetailInputArea);
            SetFocus(txtRoleName);
            btnPreviousItem.Visible = btnNextItem.Visible = false;
            ckbStatus.Checked = true;
            modalPopupExtender.Show();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<Int32> keyList = new List<Int32>();
            foreach (GridViewRow row in gvRoleList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }
                CheckBox ckbSelect = row.FindControl("ckbSelect") as CheckBox;
                if (ckbSelect.Checked)
                {
                    Int32 key = StringUtil.ToType<Int32>(ckbSelect.ToolTip);
                    keyList.Add(key);
                }
            }

            if (keyList.Count == 0)
            {
                ShowMessageBox("请至少选择一条记录。");
                return;
            }

            try
            {
                IRoleService service = CreateService<IRoleService>();
                service.BatchDelete(keyList,CurrentUser.UserName);
                QueryData();
                ShowMessageBox("删除成功");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }


        protected void gvRoleList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                e.Cancel = true;
                SetSortOrder<RoleInfo>(QueryCondition, e.SortExpression);
                listPager.CurrentPageIndex = 0;
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }


        protected void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbSelectAll = sender as CheckBox;
            foreach (GridViewRow row in gvRoleList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow)
                {
                    continue;
                }
                CheckBox ckbSelect = row.Cells[0].FindControl("ckbSelect") as CheckBox;
                if (ckbSelect != null)
                {
                    ckbSelect.Checked = ckbSelectAll.Checked;
                }
            }
            upList.Update();
        }
        
        private int? currentRowIndex;
        public int CurrentRowIndex
        {
            get
            {
                if (currentRowIndex == null)
                {
                    object rowIndex = ViewState["CURRENTROWINDEX"];
                    if (rowIndex == null)
                    {
                        currentRowIndex = 0;
                    }
                    else
                    {
                        currentRowIndex = int.Parse(rowIndex.ToString());
                    }
                }
                return currentRowIndex.Value;
            }
            set
            {
                ViewState["CURRENTROWINDEX"] = currentRowIndex = value;
            }
        }
        
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
            CurrentRowIndex = int.Parse(btn.Attributes["RowIndex"]);
            btnPreviousItem.Visible = btnNextItem.Visible = true;
            ckbStatus.Checked = true;
            ShowDetail(key);
        }
        
        private void ShowDetail(Int32 key)
        {
            try
            {
                IRoleService service = CreateService<IRoleService>();
                RoleInfo role = service.Get(key);
                FillContentValueWithEntity<RoleInfo>(role,panelDetailInputArea);
                ckbStatus.Checked = role.Status == 1;
                modalPopupExtender.Show();
                hdID.Value = key.ToString();
                upDetail.Update();
                SetFocus(txtRoleName);
            }
            catch (Exception ex)
            {

                ShowMessageBox(ex.Message);
            }
        }
        
        protected void btnPreviousItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentRowIndex > 0)
                {
                    CurrentRowIndex = CurrentRowIndex - 1;
                    LinkButton btn = gvRoleList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
                    Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
                    ShowDetail(key);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        protected void btnNextItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentRowIndex <gvRoleList.Rows.Count-1)
                {
                    CurrentRowIndex = CurrentRowIndex + 1;
                    LinkButton btn = gvRoleList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
                    Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
                    ShowDetail(key);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        
        protected void listPager_PageChanged(object sender, EventArgs e)
        {
            try
            {
                QueryData();
            }
            catch (Exception ex)
            {

                ShowMessageBox(ex.Message);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValid = ValidateControl(panelDetailInputArea);
                if (!isValid)
                {
                    return;
                }
                CreateOrUpdate();
                QueryData();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            modalPopupExtender.Hide();
        }

        private void QueryData()
        {
            IRoleService service = CreateService<IRoleService>();
            QueryCondition.PageIndex = listPager.CurrentPageIndex;
            QueryCondition.PageSize = listPager.PageSize;
            QueryResultInfo<RoleInfo> result = service.Query(QueryCondition);

            SetOrderHeaderStyle(gvRoleList, QueryCondition);
            gvRoleList.DataSource = result.RecordList;
            gvRoleList.DataBind();
            NoRecords<RoleInfo>(gvRoleList);
            listPager.RecordCount = result.RecordCount;
            upList.Update();
        }


        private void CreateOrUpdate()
        {
            IRoleService service = CreateService<IRoleService>();
            RoleInfo role = null;

            if (string.IsNullOrEmpty(hdID.Value))
            {
                role = new RoleInfo();
                role.CreateUser = CurrentUser.UserName;
                role.CreateDate = DateTime.Now;    
            }
            else
            {
                Int32 key = StringUtil.ToType<Int32>(hdID.Value);
                role = service.Get(key);
            }
            FillEntityWithContentValue<RoleInfo>(role, panelDetailInputArea);
            role.EditUser = CurrentUser.UserName;
            role.EditDate = DateTime.Now;
            role.Status = ckbStatus.Checked ? 1 : 0;
            if (string.IsNullOrEmpty(hdID.Value))
            {
                if (!service.Create(role))
                {
                    ShowMessageBox("角色已存在，请修改角色名。");
                    return;
                }
                ShowMessageBox("创建信息成功。");
                ClearControlInput(panelDetailInputArea);
                SetFocusControl(txtRoleName);
            }
            else
            {
                if (!service.Update(role))
                {
                    ShowMessageBox("角色已存在，请修改角色名。");
                    return;
                }
                modalPopupExtender.Hide();
                ShowMessageBox("更新信息成功。");
            }
        }
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnPreviousItem.Enabled = CurrentRowIndex > 0;
            btnNextItem.Enabled = CurrentRowIndex < (gvRoleList.Rows.Count-1);
        }

        #region auth
        protected void btnSelectAllAuthority_Click(object sender, EventArgs e)
        {
            SetSelectNode(treeAuth.Nodes, true);
            upAuthorityPanel.Update();
        }
        protected void btnSelectInverseAuthority_Click(object sender, EventArgs e)
        {
            SetSelectInverseNode(treeAuth.Nodes);
            upAuthorityPanel.Update();
        }
        protected void btnClearAuthority_Click(object sender, EventArgs e)
        {
            SetSelectNode(treeAuth.Nodes, false);
            upAuthorityPanel.Update();
        }
        private void SetSelectNode(TreeNodeCollection tnc, bool select)
        {
            if (tnc != null && tnc.Count > 0)
            {
                foreach (TreeNode node in tnc)
                {
                    node.Checked = select;
                    SetSelectNode(node.ChildNodes, select);
                }
            }
        }
        private void SetSelectInverseNode(TreeNodeCollection tnc)
        {
            if (tnc != null && tnc.Count > 0)
            {
                foreach (TreeNode node in tnc)
                {
                    node.Checked = !node.Checked;
                    SetSelectInverseNode(node.ChildNodes);
                }
            }
        }
        protected void btnSaveAuth_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateAuthority();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        protected void btnCancelAuth_OnClick(object sender, EventArgs e)
        {
            modalPopupExtenderAuth.Hide();
        }
        private void UpdateAuthority()
        {
            List<int> list = new List<int>();
            foreach (TreeNode node in treeAuth.CheckedNodes)
            {
                list.Add(Converter.ToInt32(node.Value, 0));
            }
            IRoleResourceService roleService = CreateService<IRoleResourceService>();
            roleService.UpdateAuthorityByRole(list, Converter.ToInt32(hdRoleID.Value, 0), CurrentUser.UserName);
            ShowMessageBox("权限更新成功。");
        }
        protected void treeAuth_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

            if (!e.Node.Checked)
            {
                SetSelectNode(e.Node.ChildNodes, false);
            }
            else
            {
                SetSelectNode(e.Node.ChildNodes, true);
                SetParentNode(e.Node);
            }
            //UpdatePanel1.Update();
        }
        private void SetParentNode(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Checked = true;
                SetParentNode(node.Parent);
            }
        }

        protected void lnkAuthority_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
            hdRoleID.Value = key.ToString();
            lblAuthorityRoleName.Text = Server.HtmlEncode(btn.ToolTip); 
            lblRoleDescription.Text =Server.HtmlEncode(StringUtil.GetShortString(btn.Attributes["Description"], 15));
            ShowAuthority(key);
        }
        private void ShowAuthority(int key)
        {
            try
            {

                IRoleResourceService service = CreateService<IRoleResourceService>();
                List<RoleResource> list = service.GetByRoleID(key);

                modalPopupExtenderAuth.Show();
                upAuthority.Update();
                this.treeAuth.Nodes.Clear();
                GetResourceList();//设置m_PageList的值
                BindTree(0, null);
                //this.treeAuth.CollapseAll();

                //调用该方法设置treeAuth中checkbox的默认值
                SetCheckedNode(treeAuth.Nodes, list);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        private void SetCheckedNode(TreeNodeCollection tnc, List<RoleResource> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (TreeNode node in tnc)
                {
                    foreach (RoleResource item in list)
                    {
                        //ShowMessageBox(str);
                        if (node.Value == item.ResourceID.ToString())
                        {
                            node.Checked = true;
                        }
                    }
                    SetCheckedNode(node.ChildNodes, list);
                }
            }
        }
        private void GetResourceList()
        {
            IResourceService roleService = CreateService<IResourceService>();
            List<Resource> list = roleService.GetAll();
            list.RemoveAll(c => c.ResourceType == 4);
            m_PageList = list.OrderBy(c => c.ParentID).ToList();
        }
        private void BindTree(int parentID, TreeNode temptree)
        {
            List<Resource> tempResource = null;
            tempResource = m_PageList.FindAll(c => c.ParentID == parentID);

            foreach (Resource res in tempResource)
            {

                TreeNode treeNode = new TreeNode();

                treeNode.SelectAction = TreeNodeSelectAction.None;

                string resourceID = res.ID.ToString();
                treeNode.Value = resourceID;
                //string treetext = res.ResourceName;
                string treetext = res.DisplayName;
                //string treetext = res.ID.ToString();
                treeNode.Text = treetext;

                if (temptree == null)
                {
                    treeAuth.Nodes.Add(treeNode);
                    BindTree(res.ID, treeNode);
                }
                else
                {
                    temptree.ChildNodes.Add(treeNode);
                    BindTree(res.ID, treeNode);
                }
            }

            treeAuth.Attributes.Add("onclick", "postBackByObject()");
        }
        #endregion
    }
}
