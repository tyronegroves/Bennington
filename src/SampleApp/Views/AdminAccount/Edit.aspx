<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" MasterPageFile="~/Views/Shared/ManageSite.Master" %>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<%:Html.EditorForModel() %>
<script type="text/javascript">
    $("#SaveAndExit").click(function () {
        $(this).parent('form').append('<input type="hidden" name="SaveAndExit" value="True" />');
    });
</script>
</asp:Content>
