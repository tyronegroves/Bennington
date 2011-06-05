<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SampleApp.Models.LocationViewModel>" %>


<%

    var routeValueDictionary = new RouteValueDictionary(Model);
    
 %>

 <tr>
 <%foreach (var key in routeValueDictionary.Keys)
   {%>

   <td>
    <%
       Html.RenderPartial("DisplayForObject", routeValueDictionary[key]);%>
   </td>
   
 <%
   }%>
   <td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=34');location.replace('#v/edit_locations&amp;id/34');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=34');" class="button important" value="Delete"></td>
   </tr>

