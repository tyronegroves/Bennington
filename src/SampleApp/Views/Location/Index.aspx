<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SampleApp.Models.LocationViewModel>>" MasterPageFile="~/Views/Shared/ManageSite.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">



<div id="content_container" style="display: block; ">
   <div id="pageheader" class="clearfix">
      <h1>Locations</h1>
   </div>
   <div class="section">
      <ul class="tabs">
         <li>Displaying All Locations<input type="button" class="button" style="float:right;" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location"></li>
         </ul></div>
         <div id="tab1" class="tabContent">
            <div class="section">
               <div class="highlight">
                  <div class="content">
                     <table cellpadding="0" cellspacing="0" class="data_table" id="data_table">
                     <thead>
                     <tr><th class="header">State</th><th class="header">City</th><th class="header">Country</th><th class="header">Description</th><th></th></tr>
                     </thead>
                     <tbody>
                     <%
                         foreach (var item in Model)
                         {%>
                     <tr>
                     <td>
                        <%:item.State%>
                     </td>
                     <td>
                        <%:item.City%>
                     </td>

                     <td>
                     <%:item.Country%>
                     </td>

                     <td>
                     <%:item.Description%>
                     </td>
<td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=34');location.replace('#v/edit_locations&amp;id/34');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=34');" class="button important" value="Delete"></td>                     
                     </tr>
                     <%
                         }%>
                     </tbody></table></div></div></div><div class="section"><div class="content actions"><input type="button" class="button" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location"></div></div>
   </div>
</div>



</asp:Content>
