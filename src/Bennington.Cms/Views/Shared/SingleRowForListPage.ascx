<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<object>" %>
<%
    var modelProperties = new RouteValueDictionary(Model);
    var buttons = ViewData.ModelMetadata.AdditionalValues["IndividualRowButtons"] as IEnumerable<Bennington.Cms.Buttons.Button>;
    if (buttons == null) buttons = new Bennington.Cms.Buttons.Button[] { };
    
 %>

 <tr>
 <%foreach (var key in modelProperties.Keys)
   {%>

   <td>
    <%
       Html.RenderPartial("DisplayForObject", modelProperties[key]);%>
   </td>
   
 <%
   }%>
       <td class="actions">
          <%
           Html.RenderPartial("DisplayForObject", buttons);%>
        </td>
   </tr>

