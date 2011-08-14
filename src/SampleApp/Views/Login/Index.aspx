<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" MasterPageFile="~/Views/Login/Site.Master" %>
<%@ Import Namespace="Bennington.Cms.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%:Html.EditorForModel()%>
</asp:Content>
