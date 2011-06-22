<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Buttons.RoutesButton>" %>
<input id="<%:Model.Id %>" type="button" class="button" value="<%:Model.Text %>" onclick="window.location='<%:Url.RouteUrl(Model.RouteValues) %>';return(false);">
