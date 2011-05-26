<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/ManageSite.Master" %>

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
                     <tr><td></td><td>Iwakuni</td><td>JP</td><td>MCAS</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=41');location.replace('#v/edit_locations&amp;id/41');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=41');" class="button important" value="Delete"></td></tr>
                     <tr><td></td><td>Okinawa</td><td>JP</td><td>MCB Camp Butler</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=42');location.replace('#v/edit_locations&amp;id/42');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=42');" class="button important" value="Delete"></td></tr>
                     <tr><td>AZ</td><td>Yuma</td><td>USA</td><td>Marine Corps Air Station</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=30');location.replace('#v/edit_locations&amp;id/30');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=30');" class="button important" value="Delete"></td></tr>
                     <tr><td>CA</td><td>Barstow</td><td>USA</td><td>MCX - Marine Corps Logistics Base</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=25');location.replace('#v/edit_locations&amp;id/25');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=25');" class="button important" value="Delete"></td></tr>
                     <tr><td>CA</td><td>Camp Pendleton</td><td>USA</td><td>MCX - Marine Corps Base</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=26');location.replace('#v/edit_locations&amp;id/26');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=26');" class="button important" value="Delete"></td></tr>
                     <tr><td>CA</td><td>San Diego</td><td>USA</td><td>MCRD/WRR</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=28');location.replace('#v/edit_locations&amp;id/28');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=28');" class="button important" value="Delete"></td></tr>
                     <tr><td>CA</td><td>San Diego</td><td>USA</td><td>MCAS Miramar</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=27');location.replace('#v/edit_locations&amp;id/27');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=27');" class="button important" value="Delete"></td></tr>
                     <tr><td>CA</td><td>Twentynine Palms</td><td>USA</td><td>MCX - MCAGCC</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=29');location.replace('#v/edit_locations&amp;id/29');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=29');" class="button important" value="Delete"></td></tr>
                     <tr><td>GA</td><td>Albany</td><td>USA</td><td>Marine Corps Logistics Base</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=39');location.replace('#v/edit_locations&amp;id/39');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=39');" class="button important" value="Delete"></td></tr>
                     <tr><td>HI</td><td>Kaneohe Bay</td><td>USA</td><td>Marine Corps Base Hawaii</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=40');location.replace('#v/edit_locations&amp;id/40');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=40');" class="button important" value="Delete"></td></tr>
                     <tr><td>MO</td><td>Kansas City</td><td>USA</td><td>MCCS</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=31');location.replace('#v/edit_locations&amp;id/31');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=31');" class="button important" value="Delete"></td></tr>
                     <tr><td>NC</td><td>Camp Lejeune</td><td>USA</td><td>Marine Corps Base</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=36');location.replace('#v/edit_locations&amp;id/36');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=36');" class="button important" value="Delete"></td></tr>
                     <tr><td>NC</td><td>Cherry Point</td><td>USA</td><td>Marine Corps Air Station</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=35');location.replace('#v/edit_locations&amp;id/35');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=35');" class="button important" value="Delete"></td></tr>
                     <tr><td>SC</td><td>Beaufort</td><td>USA</td><td>Marine Corps Air Station</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=37');location.replace('#v/edit_locations&amp;id/37');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=37');" class="button important" value="Delete"></td></tr>
                     <tr><td>SC</td><td>Parris Island</td><td>USA</td><td>Marine Corps Recruit Depot/ERR</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=38');location.replace('#v/edit_locations&amp;id/38');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=38');" class="button important" value="Delete"></td></tr>
                     <tr><td>VA</td><td>Arlington</td><td>USA</td><td>Henderson Hall</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=32');location.replace('#v/edit_locations&amp;id/32');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=32');" class="button important" value="Delete"></td></tr>
                     <tr><td>VA</td><td>Norfolk</td><td>USA</td><td>Camp Allen</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=34');location.replace('#v/edit_locations&amp;id/34');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=34');" class="button important" value="Delete"></td></tr>
                     <tr><td>VA</td><td>Quantico</td><td>USA</td><td>Marine Corps Base Quantico</td><td class="actions"><input type="button" onclick="getFile('core/process.php?v=edit_locations&amp;id=33');location.replace('#v/edit_locations&amp;id/33');" class="button" value="Edit"> <input type="button" onclick="getFile('core/process.php?v=delete_locations&amp;id=33');" class="button important" value="Delete"></td></tr></tbody></table></div></div></div><div class="section"><div class="content actions"><input type="button" class="button" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location"></div></div>
   </div>
</div>



</asp:Content>
