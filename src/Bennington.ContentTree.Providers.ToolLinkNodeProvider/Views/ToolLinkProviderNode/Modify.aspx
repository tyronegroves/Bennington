<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models.ModifyViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% using (Html.BeginForm(Model.Action, "ToolLinkNodeNode", FormMethod.Post, new { Id = "form" })) { %>

	<div class="crudFormContainer">

		<%=Html.EditorFor(x => x.ToolLinkInputModel) %>

		<div class="commandButtonContainer">
			<input type="button" class="button" value="Cancel" onclick="window.location='<%=Url.Action("Index", "ContentTree") %>';" />
			<input type="button" class="button" value="Save" onclick="$('#ToolLinkInputModel_Action').val(this.value);$('#form').submit();" />
			<input type="button" class="button" value="Save And Exit" onclick="$('#ToolLinkInputModel_Action').val(this.value);$('#form').submit();" />
			<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", "ToolLinkNode", new { treeNodeId = Model.ToolLinkInputModel.TreeNodeId }) %>'; }" />
		</div>
	</div>

	<% } %>

</asp:Content>
