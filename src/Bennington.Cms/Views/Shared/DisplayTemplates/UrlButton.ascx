<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Buttons.UrlButton>" %>
<input id="<%:Model.Id %>" type="button" class="button" value="<%:Model.Text %>" onclick="window.location='<%:Model.Url %>';return(false);">
