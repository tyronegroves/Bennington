<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime>" %>
<%if (Model == null) return; %>
<%:Model.ToString("MM/dd/yyyy") %>