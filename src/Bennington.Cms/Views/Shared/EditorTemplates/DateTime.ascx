<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>

<%:Html.TextBox("", Model.HasValue ? Model.Value.ToString("M/d/yyyy") : "", new {@class = "is_a_date"})%>
