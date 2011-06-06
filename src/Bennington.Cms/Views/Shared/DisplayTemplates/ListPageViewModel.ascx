<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Models.ListPageViewModel>" %>

<div id="content_container" style="display: block; ">
   <div id="pageheader" class="clearfix">
      <h1>Locations</h1>
   </div>
   <div class="section">
      <ul class="tabs">
         <li>Displaying All Locations<input type="button" class="button" style="float:right;" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location"></li>
      </ul>
    </div>
    <div id="tab1" class="tabContent">
        <div class="section">
            <div class="highlight">
                <div class="content">
                    <table cellpadding="0" cellspacing="0" class="data_table" id="data_table">
                <thead>
                <tr>
                    <%
                        var metadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, Model.GetType().GetProperties().Single(x=>x.Name == "Items").PropertyType.GetGenericArguments()[0]);
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
                <%foreach(var item in Model.Items.Select(x=>(object)x))
                {%>
                <%
                    Html.RenderPartial("DisplayForObject", item);%>
                <%
                }%>
                </tbody>
                </table>
                </div>
            </div>
         </div>
         <div class="section">
             <div class="content actions"><input type="button" class="button" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location"></div>
         </div>
    </div>
</div>