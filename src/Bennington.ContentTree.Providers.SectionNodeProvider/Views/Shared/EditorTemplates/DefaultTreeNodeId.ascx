<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Bennington.ContentTree.Contexts" %>
<%@ Import Namespace="MvcTurbine.ComponentModel" %>
<%@ Import Namespace="Paragon.ContentTree.Contexts" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%= Html.DropDownListFor(x => x, 
	(new SelectListItem[] {
			new SelectListItem()
                    {
                        Value = string.Empty,
						Text = "- please select -",
					},
	}).Union(
					ServiceLocatorManager.Current.Resolve<ITreeNodeSummaryContext>().GetChildren(HttpContext.Current.Request.QueryString["TreeNodeId"])
						.Select(a => new SelectListItem() { Selected = ((string)ViewData.ModelMetadata.Model == a.Id), Text = a.Name, Value = a.Id }))
	)%>
