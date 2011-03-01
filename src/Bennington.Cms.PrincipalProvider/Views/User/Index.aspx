<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ManageSite.Master" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.PrincipalProvider.Models.IndexViewModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding-top:40px;">

	<% if (Model.Users.Count() > 0) { %>

		<ul>
		<% foreach(var user in Model.Users) { %>
	
			<li><%=Html.ActionLink(user.Username, "Modify", new { Id = user.Id }, null) %></li>

		<% } %>
		</ul>

	<% } %>

	<% if (Model.Users.Count() == 0) { %>
	No items found.
	<% } %>

	<p>
		<%=Html.ActionLink("Create", "Create") %>
	</p>

</div>
</asp:Content>