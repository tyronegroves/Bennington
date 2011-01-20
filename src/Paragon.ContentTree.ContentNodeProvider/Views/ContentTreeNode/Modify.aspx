<%@ Import Namespace="Paragon.ContentTree.ContentNodeProvider.Models" %>

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Paragon.ContentTree.ContentNodeProvider.Models.ContentTreeNodeViewModel>" %>

<%@ Import Namespace="System.Web.Mvc" %>

<%@ Import Namespace="System.Security.Policy" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
<%--
<script type="text/javascript" src="/Scripts/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
	tinyMCE.init({
		mode: "textareas",
		theme: "simple"
	});
</script>--%>

<% Html.RenderAction("ContentItemNavigation", "ContentTreeNode", new { TreeNodeId = Model.ContentTreeNodeInputModel.TreeNodeId }); %>

	<% using (Html.BeginForm(Model.Action, "ContentTreeNode", FormMethod.Post, new { Id = "form" })) { %>

	<div class="contentNodeProviderForm">

		<%=Html.EditorFor(x => x.ContentTreeNodeInputModel)%>		

		<div class="commandButtonContainer">
			<input type="button" class="button" value="Cancel" onclick="window.location='<%=Url.Action("Index", "ContentTree") %>';" />
			<input type="button" class="button" value="Save" onclick="$('#ContentTreeNodeInputModel_FormAction').val(this.value);/*tinyMCE.triggerSave();*/$('#form').submit();" />
			<input type="button" class="button" value="Save And Exit" onclick="$('#ContentTreeNodeInputModel_FormAction').val(this.value);/*tinyMCE.triggerSave();*/$('#form').submit();" />
			<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", "ContentTreeNode", new { treeNodeId = Model.ContentTreeNodeInputModel.TreeNodeId }) %>'; }" />
		</div>
	</div>
	<% } %>
</asp:Content>
