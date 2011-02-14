<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Bennington.ContentTree.Contexts" %>
<%@ Import Namespace="MvcTurbine.ComponentModel" %>
<%@ Import Namespace="Paragon.ContentTree.Contexts" %>
<%= Html.DropDownListFor(x => x, 
	(new SelectListItem[] {
			new SelectListItem()
                    {
                        Value = string.Empty,
						Text = "- please select -",
					},
	}).Union(
			ServiceLocatorManager.Current.Resolve<ITreeNodeProviderContext>().GetAllTreeNodeProviders()
				.Select(a => new SelectListItem() { Text = a.Name, Value = Url.Action("Create", a.ControllerToUseForCreation, new { ProviderType = a.GetType().AssemblyQualifiedName }) })
	)) %>
