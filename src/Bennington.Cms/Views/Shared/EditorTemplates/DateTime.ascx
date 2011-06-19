<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime>" %>
<%=Html.TextBox("", Model.ToString("MM/dd/yyyy"), new {@class = "is_a_date"})%>
