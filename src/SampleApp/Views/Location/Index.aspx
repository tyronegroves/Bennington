<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<SampleApp.Models.ListPageViewModel<SampleApp.Models.LocationViewModel>>" MasterPageFile="~/Views/Shared/ManageSite.Master" %>

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

                     <tr>
                       <%
                           var metadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, Model.GetType().GetGenericArguments()[0]);
                           foreach(var property in metadata.Properties)
                           {
                               %>
                               <th><%:property.DisplayName ?? property.PropertyName %></th>
                               <%
                           }
                       %>
                       <th></th>
                                          </tr>
                     </thead>
                     <tbody>
                     <%foreach(var item in Model.Items)
                       {%>
                       <%
                           Html.RenderPartial("DisplayForObject", item);%>
                     <%
                       }%>
                     </tbody></table></div></div></div><div class="section"><div class="content actions"><input type="button" class="button" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location"></div></div>
   </div>
</div>



</asp:Content>
