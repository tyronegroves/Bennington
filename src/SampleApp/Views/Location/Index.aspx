<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Bennington.Cms.Models.ListPageViewModel<SampleApp.Models.LocationViewModel>>" MasterPageFile="~/Views/Shared/ManageSite.Master" %>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <%:Html.DisplayForModel() %>
</asp:Content>
