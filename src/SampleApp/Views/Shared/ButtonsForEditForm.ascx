<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<object>" %>
    <%
    var buttons = ViewData.ModelMetadata.AdditionalValues["ActionButtons"];
    Html.RenderPartial("DisplayForObject", buttons); %>