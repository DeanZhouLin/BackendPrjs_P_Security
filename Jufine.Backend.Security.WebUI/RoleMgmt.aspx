<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True" CodeBehind="RoleMgmt.aspx.cs" Inherits="Jufine.Backend.Security.WebUI.RoleMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript">

		// 点击复选框时触发事件
		function postBackByObject() {
			var o = window.event.srcElement;
			if (o.tagName == "INPUT" && o.type == "checkbox") {
				//这里的第一个参数是UpdatePanel ID，因为我使用了MS的ASPAJAX来实现局部刷新
				//如果没有使用MS的ASPAJAX，这里的两个参数都可以为空
				__doPostBack("<%=treeAuth.ClientID %>", "");
			}
		}

	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:Panel ID="plHeader" runat="server" DefaultButton="btnSearch" CssClass="tools_bar">
		<div class="toolbg toolbgline toolheight nowrap" style="">
			<div class="right">
			</div>
			<div class="nowrap left">
				<asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click" Text="查询" />
				<asp:Button ID="btnCreate" runat="server" CssClass="btn" OnClick="btnCreate_Click" Text="新增" />
				<asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="删除" OnClientClick="return DeleteConfirm('ckbSelect');" OnClick="btnDelete_Click" />
			</div>
			<div class="clr">
				&nbsp;
			</div>
		</div>
		<div class="edit_bar">
			<table style="width: 100%;" class="search_table">
				<tr>
					<td style="width: 80px;">角色名称：
					</td>
					<td>
						<asp:TextBox ID="stxtRoleName" PropertyName="RoleName" MaxLength="100" Width="300" runat="server"></asp:TextBox>
					</td>
					<td style="width: 80px;">角色说明：
					</td>
					<td>
						<asp:TextBox ID="stxtDisplayName" PropertyName="DisplayName" MaxLength="500" Width="300" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td style="width: 80px;">状态：
					</td>
					<td>
						<asp:RadioButton ID="rbQueryAll" runat="server" Checked="true" GroupName="StatusQuery" Text="全部" />
						<asp:RadioButton ID="rbQueryActive" runat="server" GroupName="StatusQuery" Text="激活" />
						<asp:RadioButton ID="rbQueryLock" runat="server" GroupName="StatusQuery" Text="禁用" />
					</td>
				</tr>
			</table>
		</div>
	</asp:Panel>
	<asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
		<ContentTemplate>
			<asp:GridView ID="gvRoleList"
				runat="server"
				OnSorting="gvRoleList_Sorting"
				AutoGenerateColumns="False"
				AllowSorting="true"
				CssClass="business_list">
				<Columns>
					<asp:TemplateField ItemStyle-Width="30">
						<HeaderTemplate>
							<asp:CheckBox ID="ckbSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelectAll_CheckedChanged" />
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox ID="ckbSelect" runat="server" ToolTip='<%# Eval("ID") %>' />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="60" HeaderText="编辑">
						<ItemTemplate>
							<asp:LinkButton ID="lnkEdit" runat="server" RowIndex='<%#Container.DataItemIndex %>' CommandArgument='<%# Eval("ID") %>' OnClick="lnkEdit_Click">编辑</asp:LinkButton>
							<asp:LinkButton ID="lnkAuthority" runat="server" ToolTip='<%# Eval("RoleName") %>' Description='<%# Eval("DisplayName") %>' CommandArgument='<%# Eval("ID") %>' OnClick="lnkAuthority_Click">权限</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField DataField="ID" HeaderText="编号" SortExpression="ID" ItemStyle-Width="60" />
					<asp:BoundField DataField="RoleName" HeaderText="角色名称" SortExpression="RoleName" ItemStyle-Width="60" />
					<asp:BoundField DataField="DisplayName" HeaderText="角色说明" SortExpression="DisplayName" ItemStyle-Width="60" />
					<asp:BoundField DataField="DisplayOrder" HeaderText="显示顺序" SortExpression="DisplayOrder" ItemStyle-Width="60" />
					<asp:TemplateField HeaderText="状态" SortExpression="Status" ItemStyle-Width="60">
						<ItemTemplate>
							<asp:Label ID="LabelStatus" runat="server" Text='<%# (Eval("Status") != null && Eval("Status").ToString().Trim() == "1") ?"激活":"禁用" %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<%--<asp:BoundField DataField="CreateUser" HeaderText="CreateUser" SortExpression="CreateUser" ItemStyle-Width="60" />
                            <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" ItemStyle-Width="60" />--%>
					<asp:BoundField DataField="EditUser" HeaderText="编辑人" SortExpression="EditUser" ItemStyle-Width="60" />
					<asp:BoundField DataField="EditDate" HeaderText="编辑时间" SortExpression="EditDate" ItemStyle-Width="60" />
				</Columns>
			</asp:GridView>
			<div class="pagination">
				<asp:ListPager Width="100%" ID="listPager" runat="server" FirstPageText="首页" LastPageText="尾页"
					NextPageText="下一页" OnPageChanged="listPager_PageChanged" PageSize="15" PrevPageText="上一页"
					ShowPageIndexBox="Always" PageIndexBoxType="TextBox" ShowNavigationToolTip="True"
					CustomInfoTextAlign="Left" ShowCustomInfoSection="Left" CustomInfoHTML="&nbsp;&nbsp;第 %CurrentPageIndex% 页，共 %PageCount% 页"
					SubmitButtonClass="pages_butt" TextBeforePageIndexBox="到第" TextAfterPageIndexBox="页  "
					CustomInfoStyle="padding-top:3px!important;padding-top:6px;height:20px;" CustomInfoSectionWidth="20%">
				</asp:ListPager>
			</div>
		</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="listPager" />
			<asp:AsyncPostBackTrigger ControlID="btnSearch" />
			<asp:AsyncPostBackTrigger ControlID="btnDelete" />
		</Triggers>
	</asp:UpdatePanel>
	<asp:Panel CssClass="miniWindow" ID="plDetail" runat="server" Style="display: none; width: 480px;">
		<div class="container">
			<asp:Panel ID="plTitle" Style="cursor: move;" runat="server">
				<div class="" id="miniWindow_close">
				</div>
				<div class="t" id="miniWindow_handle">
					<div class="l">
					</div>
					<div class="title">
						新增/编辑角色
					</div>
					<div class="r">
					</div>
				</div>
			</asp:Panel>
			<div class="c">
				<asp:UpdatePanel ID="upDetail" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="panelDetailInputArea" runat="server" DefaultButton="btnSave">
							<asp:HiddenField ID="hdID" runat="server" />
							<div class="c1">
								<table style="width: 100%;">
									<tr>
										<td style="width: 80px;">角色名称：
										</td>
										<td>
											<asp:TextBox ID="txtRoleName" PropertyName="RoleName" MaxLength="100" Width="200" runat="server" Rel="req|len:1~100" Title="角色名称"></asp:TextBox><span runat="server"
												id="Span2" style="color: Red">*</span>
										</td>

									</tr>
									<tr>
										<td style="width: 80px;">角色说明：
										</td>
										<td>
											<asp:TextBox ID="txtDisplayName" PropertyName="DisplayName" MaxLength="500" Width="200" runat="server" Rel="len:1~500" Title="角色说明"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td style="width: 80px;">显示顺序：
										</td>
										<td>
											<asp:TextBox ID="txtDisplayOrder" PropertyName="DisplayOrder" MaxLength="4" Width="200" runat="server" Rel="req|pinum" Title="显示顺序"></asp:TextBox><span runat="server"
												id="Span1" style="color: Red">*</span>
										</td>

									</tr>
									<tr>
										<td style="width: 80px;">状态：
										</td>
										<td>
											<asp:CheckBox ID="ckbStatus" Checked="true" runat="server" Text="激活" />
										</td>
									</tr>
								</table>
							</div>
						</asp:Panel>
						<div class="prenext">
							<asp:Button ID="btnPreviousItem" runat="server" Text="<上一条" OnClick="btnPreviousItem_Click" />
							<asp:Button ID="btnNextItem" runat="server" Text="下一条>" OnClick="btnNextItem_Click" />
						</div>
					</ContentTemplate>
					<Triggers>
						<asp:AsyncPostBackTrigger ControlID="btnPreviousItem" />
						<asp:AsyncPostBackTrigger ControlID="btnNextItem" />
						<asp:AsyncPostBackTrigger ControlID="btnCreate" />
						<asp:AsyncPostBackTrigger ControlID="btnSave" />
						<asp:AsyncPostBackTrigger ControlID="btnCancel" />
					</Triggers>
				</asp:UpdatePanel>
				<div class="saveexit">
					<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
					<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="取消" />
				</div>
				<div class="clear">
				</div>
			</div>
			<div class="b">
			</div>
		</div>
	</asp:Panel>
	<asp:ModalPopupExtender ID="modalPopupExtender" runat="server" TargetControlID="OkButton" PopupControlID="plDetail" BackgroundCssClass="modalBackground" Drag="true"
		PopupDragHandleControlID="plTitle">
	</asp:ModalPopupExtender>
	<asp:LinkButton ID="OkButton" runat="server" Text="" />
	<asp:Panel CssClass="miniWindow" ID="plAuthority" runat="server" Style="display: none; width: 410px;">
		<div class="container">
			<asp:Panel ID="plAuthTitle" Style="cursor: move;" runat="server">
				<div class="" id="Div1">
				</div>
				<div class="t" id="Div2">
					<div class="l">
					</div>
					<div class="title">
						角色权限分配
					</div>
					<div class="r">
					</div>
				</div>
			</asp:Panel>
			<div class="c">
				<asp:UpdatePanel ID="upAuthority" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
					<ContentTemplate>
						<asp:Panel ID="panelAuthorityInputArea" runat="server" DefaultButton="btnSaveAuth">
							<asp:HiddenField ID="hdRoleID" runat="server" />
							<div class="c1">
								<table style="width: 100%;">
									<tr>
										<td colspan="2">
											<asp:Label ID="label3" runat="server" Text="角色:"></asp:Label>
											<asp:Label ID="lblAuthorityRoleName" runat="server" Text="角色名"></asp:Label>
											(<asp:Label ID="lblRoleDescription" runat="server" Text="角色说明"></asp:Label>)
										</td>
									</tr>
									<tr>
										<td style="text-align: left; vertical-align: top; padding: 10px;">
											<asp:Panel ID="TreeViewPanel" ScrollBars="Auto" BorderWidth="1" BorderStyle="Inset" Width="240px" Height="300px" runat="server">
												<span style="padding: 5px;"><strong>角色权限分配：</strong></span>
												<asp:UpdatePanel ID="upAuthorityPanel" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<asp:TreeView
															runat="server"
															NodeStyle-BorderStyle="None"
															ShowLines="True"
															BorderWidth="0px"
															OnTreeNodeCheckChanged="treeAuth_TreeNodeCheckChanged"
															Font-Size="13px"
															ForeColor="#333333"
															SelectedNodeStyle-ForeColor="#ff6600"
															ParentNodeStyle-BorderStyle="Dotted"
															ShowCheckBoxes="All"
															ID="treeAuth">
															<ParentNodeStyle BorderStyle="None" />
															<NodeStyle BorderStyle="None" />
														</asp:TreeView>
													</ContentTemplate>
												</asp:UpdatePanel>

											</asp:Panel>
										</td>
										<td style="width: 120px; text-align: left; vertical-align: bottom;">
											<div style="padding: 5px;">
												<asp:Button ID="btnSelectAllAuthority" runat="server" OnClick="btnSelectAllAuthority_Click" Text="全选" />
											</div>
											<div style="padding: 5px;">
												<asp:Button ID="btnSelectInverseAuthority" runat="server" OnClick="btnSelectInverseAuthority_Click" Text="反选" />
											</div>
											<div style="padding: 5px;">
												<asp:Button ID="btnClearAuthority" runat="server" OnClick="btnClearAuthority_Click" Text="清空" />
											</div>
										</td>
									</tr>
								</table>
							</div>
						</asp:Panel>
						<div class="prenext">
						</div>
					</ContentTemplate>
					<Triggers>
						<asp:AsyncPostBackTrigger ControlID="btnSaveAuth" />
						<asp:AsyncPostBackTrigger ControlID="btnCancelAuth" />
					</Triggers>
				</asp:UpdatePanel>
				<div class="saveexit">
					<asp:Button ID="btnSaveAuth" runat="server" OnClick="btnSaveAuth_Click" Text="保存" />
					<asp:Button ID="btnCancelAuth" runat="server" OnClick="btnCancelAuth_OnClick" Text="退出" />
				</div>
				<div class="clear">
				</div>
			</div>
			<div class="b">
			</div>
		</div>
	</asp:Panel>
	<asp:ModalPopupExtender ID="modalPopupExtenderAuth" runat="server" TargetControlID="LinkButton1" PopupControlID="plAuthority" BackgroundCssClass="modalBackground"
		Drag="true" PopupDragHandleControlID="plAuthTitle">
	</asp:ModalPopupExtender>
	<asp:LinkButton ID="LinkButton1" runat="server" Text="" />
</asp:Content>
