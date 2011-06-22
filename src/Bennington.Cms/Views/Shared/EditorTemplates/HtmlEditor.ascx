<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.String>" %>
<%:Html.TextAreaFor(x => x, 10, 20, new { @class = "tinymce" })%>
