<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SampleApp.Models.LocationViewModel>" %>

                     <tr>
                     <td>
                        <%:Model.State %>
                     </td>
                     <td>
                        <%:Model.City %>
                     </td>

                     <td>
                     <%:Model.Country %>
                     </td>

                     <td>
                     <%:Model.Description%>
                     </td>
<td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=34');location.replace('#v/edit_locations&amp;id/34');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=34');" class="button important" value="Delete"></td>                     
                     </tr>