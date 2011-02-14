<%@ Import Namespace="Bennington.ContentTree.Providers.ContentNodeProvider.Models" %>
<%@ Import Namespace="System.Linq" %>
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Bennington.ContentTree.Providers.ContentNodeProvider.Models.ContentItemNavigationViewModel>" %>

<% if (Model.ContentTreeNodeContentItems.Count() > 1) { %>
	<ul>
	<% foreach (var contentItem in Model.ContentTreeNodeContentItems) { %>
		<li><%=Html.ActionLink(contentItem.Name, "Modify", "ContentTreeNode", new { treeNodeId = Model.TreeNodeId, contentItemId = contentItem.Id }, null) %></li>
	<% } %>
	</ul>
<% } %>