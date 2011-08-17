<%@ Import Namespace="Bennington.ContentTree.Providers.SectionNodeProvider.Models" %>

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<ContentTreeSectionNodeViewModel>" %>

<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
<%--
<script type="text/javascript" src="/Scripts/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
	tinyMCE.init({
		mode: "textareas",
		theme: "simple"
	});
</script>--%>

	<% using (Html.BeginForm(Model.Action, "ContentTreeSectionNode", FormMethod.Post, new { Id = "form" })) { %>

	<div class="contentNodeProviderForm">

		<%=Html.EditorFor(x => x.ContentTreeSectionInputModel) %>		

		<div class="commandButtonContainer">
			<input type="button" class="button" value="Cancel" onclick="window.location='<%=Url.Action("Index", "ContentTree") %>';" />
			<input type="button" class="button" value="Save" onclick="$('#ContentTreeSectionInputModel_Action').val(this.value);/*tinyMCE.triggerSave();*/$('#form').submit();" />
			<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", "ContentTreeSectionNode", new { treeNodeId = Model.ContentTreeSectionInputModel.TreeNodeId }) %>'; }" />
		</div>
	</div>
	<% } %>
</asp:Content>
