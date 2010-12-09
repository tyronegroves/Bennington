<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContentTreeContactUsNodeProvider.Models.ContentTreeContactUsNodeInputModel>" %>

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

	<% using (Html.BeginForm(Model.Action, "ContentTreeContactUsNode", FormMethod.Post, new { Id = "form" })) { %>

	<div class="crudFormContainer">

		<%=Html.EditorForModel()%>		

		<div class="commandButtonContainer">
			<input type="button" class="button" value="Cancel" onclick="window.location='<%=Url.Action("Index", "ContentTree") %>';" />
			<input type="button" class="button" value="Save" onclick="$('#ContentTreeContactUsNodeInputModel_Action').val(this.value);/*tinyMCE.triggerSave();*/$('#form').submit();" />
			<input type="button" class="button" value="Save And Exit" onclick="$('#ContentTreeContactUsNodeInputModel_Action').val(this.value);/*tinyMCE.triggerSave();*/$('#form').submit();" />
			<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", "ContentTreeContactUsNode", new { treeNodeId = Model.TreeNodeId }) %>'; }" />
		</div>
	</div>
	<% } %>
</asp:Content>
