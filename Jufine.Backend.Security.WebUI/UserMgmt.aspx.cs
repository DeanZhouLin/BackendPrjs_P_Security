using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Utility;

using Jufine.Backend.WebModel;
using Jufine.Backend.Security.DataContracts;
using Jufine.Backend.Security.ServiceContracts;
using Com.BaseLibrary.Common.Cryptography;

namespace Jufine.Backend.Security.WebUI
{
	public partial class UserMgmt : PageBase
	{
		static IUserService userService = CreateService<IUserService>();
		static IMerchantService merchantService = CreateService<IMerchantService>();
		private List<Resource> m_PageList;
		private QueryConditionInfo<UserInfo> QueryCondition
		{
			get
			{
				QueryConditionInfo<UserInfo> queryCondition
					= ViewState["USER_QUERYCONDITION"] as QueryConditionInfo<UserInfo>;
				if (queryCondition == null)
				{
					queryCondition = new QueryConditionInfo<UserInfo>();
					queryCondition.IsAdmin = CurrentUser.IsAdmin;
					queryCondition.MerchantList = CurrentUser.PageMerchantList.Select(c => c.MerchantID).ToList();
					ViewState["USER_QUERYCONDITION"] = queryCondition;
				}
				return queryCondition;
			}
		}
		public int MerchantID
		{
			get
			{
				if (!CurrentUser.IsAdmin)
					return CurrentUser.MerchantID;
				return Converter.ToInt32(Request.QueryString["merchantid"], 0);
			}
		}
		public static List<MerchantInfo> merchantList { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsPostBack)
				{
					merchantList = merchantService.GetAll();
					if (MerchantID > 0)
					{
						MerchantInfo merchant = merchantService.Get(MerchantID);
						btnCreate.Enabled = merchant.Status == 1;
					}
					else
					{
						btnCreate.Enabled = false;
					}
					BindMerchantList();
					QueryCondition.Condtion.Sex = -1;
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
		private void BindMerchantList()
		{
			// SearchableListBox ss = ucSearchableListBox as SearchableListBox;

			sddlMerchantID.DataTextField = "MechantIDAndName";
			sddlMerchantID.DataValueField = "MerchantID";
			//var filter = CurrentUser.MerchantList;
			//if (!string.IsNullOrEmpty(ss.InputText))
			//{
			//	filter = CurrentUser.PageMerchantList.FindAll(c => c.MechantIDAndName.Contains(ss.InputText));
			//}
			var  merchentList = CurrentUser.PageMerchantList.FindAll(c => true);
			merchentList.Insert(0, new Com.BaseLibrary.Common.Security.UserMerchant { MerchantID = 0, MerchantName = "全部商家" });
			sddlMerchantID.DataSource = merchentList;
			sddlMerchantID.DataBind();
		}

		protected void SearchableListBox_DoSearch(object sender, EventArgs e)
		{
			BindMerchantList();
		}
		
		protected void SearchableListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//BindMerchantList();

		}
		protected void btnActive_Click(object sender, EventArgs e)
		{
			List<int> idList = GetSelectedUserIdList();

			if (idList.Count == 0)
			{
				ShowMessageBox("请至少选择一条记录。");
			}
			else
			{
				try
				{
					List<int> failIDList = new List<int>();
					foreach (int id in idList)
					{
						IMerchantService merchantService = CreateService<IMerchantService>();
						UserInfo user = userService.Get(id);
						MerchantInfo merchant = merchantService.Get(user.MerchantID);
						if (merchant == null || merchant.Status != 1)
						{
							failIDList.Add(id);
						}
					}
					idList.RemoveAll(c => failIDList.Exists(d => d == c));

					userService.BatchChangeStatus(idList, 1, CurrentUser.UserName);

					QueryData();

					SetCheckboxStatus(idList);

					upList.Update();
					if (failIDList.Count > 0)
					{
						string message = "用户名为";
						foreach (int id in failIDList)
						{
							message += userService.Get(id).UserName + ",";
						}
						message = message.TrimEnd(',');
						message += "的用户激活失败，请检查商家信息。";
						ShowMessageBox(message);
					}
					else
					{
						ShowMessageBox("激活用户信息成功。");
					}
				}
				catch (Exception ex)
				{

					ShowMessageBox(ex.Message);
				}

			}
		}

		protected void btnLock_Click(object sender, EventArgs e)
		{
			List<int> idList = GetSelectedUserIdList();

			if (idList.Count == 0)
			{
				ShowMessageBox("请至少选择一条记录。");
			}
			else
			{
				try
				{
					userService.BatchChangeStatus(idList, 0, CurrentUser.UserName);

					QueryData();

					SetCheckboxStatus(idList);

					upList.Update();
					ShowMessageBox("禁用用户信息成功。");
				}
				catch (Exception ex)
				{
					ShowMessageBox(ex.Message);
				}
			}
		}
		private List<int> GetSelectedUserIdList()
		{
			List<int> idList = new List<int>();
			foreach (GridViewRow row in gvUserList.Rows)
			{
				if (row.RowType != DataControlRowType.DataRow)
				{
					continue;
				}
				CheckBox ckbSelect = row.FindControl("ckbSelect") as CheckBox;
				if (ckbSelect.Checked)
				{
					int id = int.Parse(ckbSelect.ToolTip);
					idList.Add(id);
				}
			}
			return idList;
		}
		private void SetCheckboxStatus(List<int> idList)
		{
			foreach (GridViewRow row in gvUserList.Rows)
			{
				if (row.RowType != DataControlRowType.DataRow)
				{
					continue;
				}
				CheckBox ckbSelect = row.FindControl("ckbSelect") as CheckBox;
				ckbSelect.Checked = idList.Exists(c => c.ToString() == ckbSelect.ToolTip);
			}
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				bool isValid = ValidateControl(plHeader);
				if (!isValid)
				{
					return;
				}
				listPager.CurrentPageIndex = 1;
				FillEntityWithContentValue<UserInfo>(QueryCondition.Condtion, plHeader);
				// SearchableListBox ss = ucSearchableListBox as SearchableListBox;
				QueryCondition.Condtion.MerchantID = Converter.ToInt32(sddlMerchantID.SelectedValue, 0);


				int status = rbQueryLock.Checked ? 0 : (rbQueryActive.Checked ? 1 : -1);
				QueryCondition.Condtion.Status = status;
				int sex = rbQueryMale.Checked ? 0 : (rbQuerySexAll.Checked ? -1 : 1);
				QueryCondition.Condtion.Sex = sex;
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
			ckbStatus.Checked = true;
			rbMale.Checked = true;
			SetFocus(txtUserName);
			btnNextItem.Visible = false;
			btnPreviousItem.Visible = false;
			txtMerchantID.Text = MerchantID.ToString();
			txtMerchantName.Text = merchantList != null ? merchantList.Find(c => c.ID == MerchantID).MerchantName : "";
			txtUserName.Enabled = true;
			modalPopupExtender.Show();
		}
		protected void btnDelete_Click(object sender, EventArgs e)
		{
			List<Int32> keyList = new List<Int32>();
			foreach (GridViewRow row in gvUserList.Rows)
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
				//userService.BatchDelete(keyList);
				userService.BatchChangeStatus(keyList, -1, CurrentUser.UserName);
				QueryData();
				ShowMessageBox("删除成功");
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}

		}
		protected void gvUserList_Sorting(object sender, GridViewSortEventArgs e)
		{
			try
			{
				e.Cancel = true;
				SetSortOrder<UserInfo>(QueryCondition, e.SortExpression);
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
			foreach (GridViewRow row in gvUserList.Rows)
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

		#region 分配权限
		protected void lnkAuthority_Click(object sender, EventArgs e)
		{
			LinkButton btn = sender as LinkButton;
			Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
			hdUserID.Value = key.ToString();
			lblAuthorityUserName.Text = btn.ToolTip;
			lblAuthorityMerchantName.Text = StringUtil.GetShortString(btn.Attributes["MerchantName"], 15);
			int merchantID = StringUtil.ToType<Int32>(btn.Attributes["MerchantID"]);
			ShowAuthority(key, merchantID);
		}
		private void ShowAuthority(int key, int merchantID)
		{
			try
			{
				IUVUserMerchantService uvService = CreateService<IUVUserMerchantService>();
				List<UVUserMerchant> uvMerchantList = uvService.GetByUserID(key);
				ddlMerchantForAuthority.DataSource = uvMerchantList;
				ddlMerchantForAuthority.DataBind();
				if (uvMerchantList.Select(c => c.MerchantID).Contains(merchantID))
				{
					ddlMerchantForAuthority.SelectedValue = merchantID.ToString();
				}

				IUserRoleService userRoleService = CreateService<IUserRoleService>();
				List<int> roleResourceIDList = userRoleService.GetRoleResourceID(key, merchantID);

				IUserResourceService userResourceService = CreateService<IUserResourceService>();
				List<UserResource> list = userResourceService.GetByUserID(key, merchantID);

				modalPopupExtenderAuth.Show();
				upAuthority.Update();
				this.treeAuth.Nodes.Clear();
				GetResourceList();//设置m_PageList的值
				BindTree(0, null);
				//this.treeAuth.CollapseAll();

				//调用该方法设置treeAuth中checkbox的默认值
				SetCheckedNode(treeAuth.Nodes, list, roleResourceIDList);
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}
		protected void ddlMerchantForAuthority_SelectedIndexChanged(object sender, EventArgs e)
		{
			int userID = StringUtil.ToType<Int32>(hdUserID.Value);
			int merchantID = StringUtil.ToType<Int32>(ddlMerchantForAuthority.SelectedValue);
			UpdateAuthorityTreeView(userID, merchantID);
		}

		private void UpdateAuthorityTreeView(int userID, int merchantID)
		{
			try
			{
				IUserResourceService userResourceService = CreateService<IUserResourceService>();
				List<UserResource> list = userResourceService.GetByUserID(userID, merchantID);
				IUserRoleService userRoleService = CreateService<IUserRoleService>();
				List<int> roleResourceIDList = userRoleService.GetRoleResourceID(userID, merchantID);
				modalPopupExtenderAuth.Show();
				upAuthorityTreeView.Update();
				this.treeAuth.Nodes.Clear();
				GetResourceList();//设置m_PageList的值
				BindTree(0, null);
				//this.treeAuth.CollapseAll();

				//调用该方法设置treeAuth中checkbox的默认值
				SetCheckedNode(treeAuth.Nodes, list, roleResourceIDList);
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}
		protected void btnSelectAllAuthority_Click(object sender, EventArgs e)
		{
			SetSelectNode(treeAuth.Nodes, true);
			upAuthority.Update();

		}
		protected void btnSelectInverseAuthority_Click(object sender, EventArgs e)
		{
			SetSelectInverseNode(treeAuth.Nodes);
			upAuthority.Update();
		}
		protected void btnClearAuthority_Click(object sender, EventArgs e)
		{
			SetSelectNode(treeAuth.Nodes, false);
			upAuthority.Update();
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
		private void UpdateAuthority()
		{
			List<int> list = new List<int>();
			int userID = Converter.ToInt32(hdUserID.Value, 0);
			int merchantID = Converter.ToInt32(ddlMerchantForAuthority.SelectedValue, 0);
			foreach (TreeNode node in treeAuth.CheckedNodes)
			{
				if (node.ShowCheckBox == null || node.ShowCheckBox.Value)
				{
					list.Add(Converter.ToInt32(node.Value, 0));
				}
			}
			IUserResourceService userService = CreateService<IUserResourceService>();
			userService.UpdateAuthorityByUser(list, userID, merchantID, CurrentUser.UserName);
			ShowMessageBox("权限更新成功。");
		}
		protected void btnCancelAuth_OnClick(object sender, EventArgs e)
		{
			modalPopupExtenderAuth.Hide();
		}
		#endregion

		#region 分配商家
		protected void lnkMerchant_Click(object sender, EventArgs e)
		{
			LinkButton btn = sender as LinkButton;
			Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
			hdMerchantUserId.Value = key.ToString();
			lblMerchantUserName.Text = btn.ToolTip;
			lblMerchantMerchantName.Text = StringUtil.GetShortString(btn.Attributes["MerchantName"], 15);
			int merchantID = StringUtil.ToType<Int32>(btn.Attributes["MerchantID"]);
			ShowMerchant(key, merchantID);
		}
		private void ShowMerchant(int key, int merchantID)
		{
			try
			{
				IMerchantService merchantService = CreateService<IMerchantService>();
				List<MerchantInfo> list = merchantService.GetActiveMerchant();
				list.ForEach(c => c.MerchantName = Server.HtmlEncode(c.MerchantName));
				MerchantInfo mi = list.FirstOrDefault(c => c.ID == merchantID);
				if (mi != null)
				{
					list.RemoveAll(c => c.ID == merchantID);
					list.Insert(0, mi);
				}
				ckbMerchantList.DataSource = list;
				ckbMerchantList.DataBind();
				IUserMerchantService userMerchantService = CreateService<IUserMerchantService>();
				List<UserMerchant> umList = userMerchantService.GetByUserID(key);
				List<string> mList = umList.Select(c => c.MerchantID.ToString()).ToList();
				foreach (ListItem item in ckbMerchantList.Items)
				{
					if (item.Value == merchantID.ToString())
					{
						item.Enabled = false;
						item.Selected = true;
					}
					else if (mList.Contains(item.Value))
					{
						item.Selected = true;
					}
				}
				upMerchant.Update();
				modalPopupExtenderMerchant.Show();
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}
		protected void btnSelectAllMerchant_Click(object sender, EventArgs e)
		{
			SetCheckBox(ckbMerchantList, true);
			upMerchant.Update();
		}
		protected void btnSelectInverseMerchant_Click(object sender, EventArgs e)
		{
			SetInverseCheckBox(ckbMerchantList);
			upMerchant.Update();
		}
		protected void btnClearMerchant_Click(object sender, EventArgs e)
		{
			SetCheckBox(ckbMerchantList, false);
			upMerchant.Update();
		}
		private void SetCheckBox(CheckBoxList cbl, bool isChecked)
		{
			foreach (ListItem item in cbl.Items)
			{
				if (item.Enabled)
				{
					item.Selected = isChecked;
				}
			}
		}
		private void SetInverseCheckBox(CheckBoxList cbl)
		{
			foreach (ListItem item in cbl.Items)
			{
				if (item.Enabled)
				{
					item.Selected = !item.Selected;
				}
			}
		}
		protected void btnSaveMerchant_Click(object sender, EventArgs e)
		{
			try
			{
				UpdateMerchant();
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}
		private void UpdateMerchant()
		{
			List<int> list = new List<int>();
			int userID = Converter.ToInt32(hdMerchantUserId.Value, 0);
			foreach (ListItem item in ckbMerchantList.Items)
			{
				if (item.Selected)
				{
					list.Add(Converter.ToInt32(item.Value, 0));
				}
			}
			IUserMerchantService userMerchantService = CreateService<IUserMerchantService>();
			userMerchantService.UpdateMerchantByUser(list, userID, CurrentUser.UserName);
			ShowMessageBox("更新成功。");
		}
		protected void btnCancelMerchant_OnClick(object sender, EventArgs e)
		{
			modalPopupExtenderMerchant.Hide();
		}
		#endregion

		#region 分配角色
		protected void lnkRole_Click(object sender, EventArgs e)
		{
			LinkButton btn = sender as LinkButton;
			Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
			hdUserIDForRole.Value = key.ToString();
			lblUserNameForRole.Text = btn.ToolTip;
			lblMerchantNameForRole.Text = StringUtil.GetShortString(btn.Attributes["MerchantName"], 15);
			int merchantID = StringUtil.ToType<Int32>(btn.Attributes["MerchantID"]);
			ShowRole(key, merchantID);
		}
		private void ShowRole(int key, int merchantID)
		{
			try
			{
				IUVUserMerchantService uvService = CreateService<IUVUserMerchantService>();
				List<UVUserMerchant> uvMerchantList = uvService.GetByUserID(key);
				ddlMerchantListForRole.DataSource = uvMerchantList;
				ddlMerchantListForRole.DataBind();
				if (uvMerchantList.Select(c => c.MerchantID).Contains(merchantID))
				{
					ddlMerchantListForRole.SelectedValue = merchantID.ToString();
				}

				IRoleService roleService = CreateService<IRoleService>();
				List<RoleInfo> roleList = roleService.GetAllActive();
				ckbRoleList.DataSource = roleList;
				ckbRoleList.DataBind();

				IUserRoleService userRoleService = CreateService<IUserRoleService>();
				List<UserRole> list = userRoleService.GetByUserID(key, merchantID);
				List<string> rList = list.Select(c => c.RoleID.ToString()).ToList();
				foreach (ListItem item in ckbRoleList.Items)
				{
					if (rList.Contains(item.Value))
					{
						item.Selected = true;
					}
				}
				modalPopupExtenderRole.Show();
				upRole.Update();
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}
		protected void ddlMerchantForRole_SelectedIndexChanged(object sender, EventArgs e)
		{
			int userID = StringUtil.ToType<Int32>(hdUserIDForRole.Value);
			int merchantID = StringUtil.ToType<Int32>(ddlMerchantListForRole.SelectedValue);
			UpdateRoleList(userID, merchantID);
		}

		private void UpdateRoleList(int userID, int merchantID)
		{
			try
			{
				IRoleService roleService = CreateService<IRoleService>();
				List<RoleInfo> roleList = roleService.GetAllActive();
				ckbRoleList.DataSource = roleList;
				ckbRoleList.DataBind();

				IUserRoleService userRoleService = CreateService<IUserRoleService>();
				List<UserRole> list = userRoleService.GetByUserID(userID, merchantID);

				List<string> rList = list.Select(c => c.RoleID.ToString()).ToList();
				foreach (ListItem item in ckbRoleList.Items)
				{
					if (rList.Contains(item.Value))
					{
						item.Selected = true;
					}
				}
				modalPopupExtenderRole.Show();
				upRoleList.Update();
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}
		protected void btnSelectAllRole_Click(object sender, EventArgs e)
		{
			SetCheckBox(ckbRoleList, true);
			upRoleList.Update();
		}
		protected void btnSelectInverseRole_Click(object sender, EventArgs e)
		{
			SetInverseCheckBox(ckbRoleList);
			upRoleList.Update();
		}
		protected void btnClearRole_Click(object sender, EventArgs e)
		{
			SetCheckBox(ckbRoleList, false);
			upRoleList.Update();
		}
		protected void btnSaveRole_Click(object sender, EventArgs e)
		{
			try
			{
				UpdateRole();
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}
		private void UpdateRole()
		{
			List<int> list = new List<int>();
			int userID = Converter.ToInt32(hdUserIDForRole.Value, 0);
			int merchantID = Converter.ToInt32(ddlMerchantListForRole.SelectedValue, 0);
			foreach (ListItem item in ckbRoleList.Items)
			{
				if (item.Selected)
				{
					list.Add(Converter.ToInt32(item.Value, 0));
				}
			}
			IUserRoleService userRoleService = CreateService<IUserRoleService>();
			userRoleService.UpdateRoleByUser(list, userID, merchantID, CurrentUser.UserName);
			ShowMessageBox("角色更新成功。");
		}
		protected void btnCancelRole_OnClick(object sender, EventArgs e)
		{
			modalPopupExtenderRole.Hide();
		}
		#endregion

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
			txtUserName.Enabled = false;
			LinkButton btn = sender as LinkButton;
			//Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
			btnNextItem.Visible = true;
			btnPreviousItem.Visible = true;
			CurrentRowIndex = int.Parse(btn.Attributes["RowIndex"]);

			ShowDetail(btn);
		}
		private void ShowDetail(LinkButton linkButton)
		{
			try
			{
				Int32 key = StringUtil.ToType<Int32>(linkButton.CommandArgument);
				Int32 curMerchantID = StringUtil.ToType<Int32>(linkButton.Attributes["MerchantID"]);
				UserInfo user = userService.Get(key);
				FillContentValueWithEntity<UserInfo>(user, panelDetailInputArea);
				//ckbIsAdmin.Checked = user.IsAdmin == 1;
				ckbStatus.Checked = user.Status == 1;
				rbMale.Checked = user.Sex == 0;
				rbFemale.Checked = !rbMale.Checked;
				modalPopupExtender.Show();
				hdID.Value = key.ToString();
				txtMerchantName.Text = merchantList != null ? merchantList.Find(c => c.ID == curMerchantID).MerchantName : "";
				upDetail.Update();
				SetFocus(txtUserName);
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		private void SetCheckedNode(TreeNodeCollection tnc, List<UserResource> list, List<int> roleResourceIDList)
		{

			if (roleResourceIDList.Count > 0)
			{
				foreach (TreeNode node in tnc)
				{
					foreach (int rid in roleResourceIDList)
					{
						if (!node.Checked && node.Value == rid.ToString())
						{
							node.ShowCheckBox = false;
							node.Checked = true;
						}
					}
					SetCheckedNode(node.ChildNodes, list, roleResourceIDList);
				}
			}

			foreach (TreeNode node in tnc)
			{
				foreach (UserResource item in list)
				{
					//ShowMessageBox(str);
					if (!node.Checked && node.Value == item.ResourceID.ToString())
					{
						node.Checked = true;
					}
				}
				SetCheckedNode(node.ChildNodes, list, roleResourceIDList);
			}


		}
		private void GetResourceList()
		{
			IResourceService userService = CreateService<IResourceService>();
			List<Resource> list = userService.GetAll();
			if (ddlMerchantForAuthority.SelectedValue != "10000")
			{
				list.RemoveAll(c => c.ResourceType == 4);
			}
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
		protected void btnPreviousItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (CurrentRowIndex > 0)
				{
					CurrentRowIndex = CurrentRowIndex - 1;
					LinkButton btn = gvUserList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
					//Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
					ShowDetail(btn);
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
				if (CurrentRowIndex < gvUserList.Rows.Count - 1)
				{
					CurrentRowIndex = CurrentRowIndex + 1;
					LinkButton btn = gvUserList.Rows[CurrentRowIndex].FindControl("lnkEdit") as LinkButton;
					// Int32 key = StringUtil.ToType<Int32>(btn.CommandArgument);
					ShowDetail(btn);
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


		protected void btnExport_Click(object sender, EventArgs e)
		{
		    QueryCondition.ReturnAllData = true;
		    if (MerchantID > 0)
		    {
		        QueryCondition.Condtion.MerchantID = MerchantID;
		    }
		    QueryResultInfo<UserInfo> result = userService.Query(QueryCondition);
		    QueryCondition.ReturnAllData = false;
		    ExcelUtil.SaveToExcel<UserInfo>(this, result.RecordList, null, "用户数据", "UserData");
		}

		protected void btnCancel_OnClick(object sender, EventArgs e)
		{
			modalPopupExtender.Hide();
		}

		private void QueryData()
		{
			QueryResultInfo<UserInfo> result = ExecuteQuery();
			List<UserInfo> userList = result.RecordList;
			foreach (UserInfo user in userList)
			{
				MerchantInfo m = merchantList.FirstOrDefault(c => c.ID == user.MerchantID);
				user.MerchantName = m == null ? string.Empty : m.MerchantName;
			}
			SetOrderHeaderStyle(gvUserList, QueryCondition);
			gvUserList.DataSource = userList;
			gvUserList.DataBind();
			NoRecords<UserInfo>(gvUserList);
			listPager.RecordCount = result.RecordCount;
			listPager.CurrentPageIndex = result.CurrentPageIndex;
			upList.Update();
		}

		private QueryResultInfo<UserInfo> ExecuteQuery()
		{
			QueryCondition.PageIndex = listPager.CurrentPageIndex;
			QueryCondition.PageSize = listPager.PageSize;
			QueryResultInfo<UserInfo> result = userService.Query(QueryCondition);
			return result;
		}
		private bool CreateOrUpdate()
		{
			bool isCreate;
			string message = "";
			isCreate = StringUtil.IsNullOrEmpty(hdID.Value);

			//txtPassword.Text = txtPassword.Text.Trim();

			//if ((isCreate || txtPassword.Text.Length > 0) && !ValidateControl(txtUserName, txtPassword, txtFullName, txtIDCard, txtBirthday, txtEmail))
			//{
			//    return false;
			//}
			//else if (!ValidateControl(txtUserName, txtPassword, txtFullName, txtIDCard, txtBirthday, txtEmail))
			//{
			//    return false;
			//}

			UserInfo user = userService.GetByUserName(txtUserName.Text.Trim());
			IMerchantService merchantService = CreateService<IMerchantService>();
			MerchantInfo merchant = null;

			if (isCreate)
			{
				merchant = merchantService.Get(MerchantID);
				if (user != null)
				{
					ShowMessageBox("创建失败：", "用户名已存在。");
					return false;
				}
				if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
				{
					ShowMessageBox("创建失败：", "密码没有填写。");
					return false;
				}
				user = new UserInfo();
				user.CreateUser = CurrentUser.UserName;
				user.CreateDate = DateTime.Now;
			}
			else
			{
				//    merchant = merchantService.Get(user.MerchantID);
				merchant = merchantService.Get(Converter.ToInt32(txtMerchantID.Text, 0));
			}
			user.Password = string.IsNullOrEmpty(txtPassword.Text.Trim()) ? user.Password : Encryptor.Encrypt(txtPassword.Text.Trim());
			user.IDCard = txtIDCard.Text.Trim();
			user.FullName = txtFullName.Text.Trim();
			user.Email = txtEmail.Text.Trim();
			user.Status = ckbStatus.Checked ? 1 : 0;
			user.Sex = rbMale.Checked ? 0 : 1;
			//user.IsAdmin = ckbIsAdmin.Checked ? 1 : 0;
			user.EditUser = CurrentUser.UserName;
			user.EditDate = DateTime.Now;

			if (user.Status == 1 && (merchant == null || merchant.Status != 1))
			{
				user.Status = 0;
				message += "所属商家已被删除或禁用，该用户也被禁用。";
				Com.BaseLibrary.Logging.Logger.CurrentLogger.DoWrite("backend", "Security", "Error", "所属商家已被删除或禁用，该用户也被禁用。"
					, "CurrentUser:" + JsonUtil.ConvertToJson(CurrentUser) + "被操作User:" + JsonUtil.ConvertToJson(user));
			}

			if (isCreate)
			{
				user.MerchantID = MerchantID;
				user.UserName = txtUserName.Text.Trim();
				userService.Create(user);
				ShowMessageBox("创建用户信息成功。" + message);
				//ckbStatus.Checked = true;
				SetFocus(txtUserName);
				ClearControlInput(panelDetailInputArea);
				rbMale.Checked = true;
			}
			else
			{
				userService.Update(user);
				ShowMessageBox("更新用户信息成功。" + message);
				SetFocus(txtPassword);
			}
			return true;

		}
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			btnPreviousItem.Enabled = CurrentRowIndex > 0;
			btnNextItem.Enabled = CurrentRowIndex < (gvUserList.Rows.Count - 1);
		}
	}
}
