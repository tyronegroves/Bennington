<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.String>" %>
<%@ Import Namespace="Bennington.Login.Helpers" %>

<p class="field-wrap">
    <%=Html.RenderLabelForModel()%>
    <span class="text-box-wrap"><span class="text-box-inner"><%=Html.PasswordFor(x => x)%></span></span>
</p>
