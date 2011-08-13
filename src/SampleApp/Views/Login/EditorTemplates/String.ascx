<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.String>" %>

<%@ Import Namespace="DegDarwin.Helpers" %>
<p class="field-wrap">
    <%=Html.RenderLabelForModel()%>
    <span class="text-box-wrap"><span class="text-box-inner"><%=Html.TextBoxFor(x => x)%></span></span>
</p>
