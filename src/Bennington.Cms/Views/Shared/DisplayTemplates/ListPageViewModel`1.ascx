<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%

    var model = (object)Model;
    var metadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, model.GetType().GetProperties().Single(x => x.Name == "Items").PropertyType.GetGenericArguments()[0]);

    var additionalValues = new RouteValueDictionary(metadata.AdditionalValues);

    var sectionHeader = additionalValues["SectionHeader"] as string;
    var gridHeader = additionalValues["GridHeader"] as string;

    var topRightButtons = additionalValues["TopRightButtons"] as IEnumerable<Button> ?? new Button[] {};
    
        %>

<div id="content_container" style="display: block; ">
   <div id="pageheader" class="clearfix">
      <h1><%:sectionHeader %></h1>
   </div>
   <div class="section">
      <ul class="tabs">
         <li><%:gridHeader %> 
<%Html.RenderPartial("DisplayForObject", topRightButtons); %>
<%--             <input type="button" class="button" style="float:right;" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location">
             <input type="button" class="button" style="float:right;" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location">
--%>         </li>
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
                  {
                      Html.RenderPartial("SingleRowForListPage", item as object);
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