<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models.ModifyViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <%--<% using (Html.BeginForm(Model.ToolLinkInputModel.Action, "ToolLinkProviderNode", FormMethod.Post, new { Id = "form" })) { %>--%>
   <form Id="form" action="/Manage/ToolLinkProviderNode/<%=Model.ToolLinkInputModel.Action %>" method="post">

	<div class="crudFormContainer">

		<%=Html.EditorFor(x => x.ToolLinkInputModel) %>

		<div class="commandButtonContainer">
			<input type="button" class="button" value="Cancel" onclick="window.location='<%=Url.Action("Index", "ContentTree") %>';" />
			<input type="button" class="button" value="Save" onclick="$('#ToolLinkInputModel_FormAction').val(this.value);$('#form').submit();" />
			<input type="button" class="button" value="Save And Exit" onclick="$('#ToolLinkInputModel_FormAction').val(this.value);$('#form').submit();" />
			<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", "ToolLinkProviderNode", new { treeNodeId = Model.ToolLinkInputModel.TreeNodeId }) %>'; }" />
		</div>
	</div>

	<%--<% } %>--%>
    </form>

</asp:Content>
