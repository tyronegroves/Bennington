<%@ Import Namespace="Paragon.ContentTree.ContentNodeProvider.Models" %>
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Paragon.ContentTree.ContentNodeProvider.Models.ContentItemNavigationViewModel>" %>

<ul>
<% foreach (var contentItem in Model.ContentTreeNodeContentItems) { %>
	<li><%=Html.ActionLink(contentItem.Name, "Modify", "ContentTreeNode", new { treeNodeId = Model.TreeNodeId, contentItemId = contentItem.Id }, null) %></li>
<% } %>
</ul>
