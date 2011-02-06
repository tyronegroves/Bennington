<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Homepage.Master" Inherits="System.Web.Mvc.ViewPage<Paragon.ContentTree.Engines.Homepage.Models.HomepageIndexViewModel>" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h1><%=Model.Header %></h1>

   <div><%=Model.Body %></div>

</asp:Content>
