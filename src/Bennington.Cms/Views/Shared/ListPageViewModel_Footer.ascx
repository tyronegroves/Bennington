﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="PagedList" %>
<%@ Import Namespace="PagedList.Mvc" %>

<%
    object model = Model;

    var bottomLeftButtons = ViewData.ModelMetadata.AdditionalValues["BottomLeftButtons"] as IEnumerable<Bennington.Cms.Buttons.Button>;
    if (bottomLeftButtons == null) bottomLeftButtons = new Bennington.Cms.Buttons.Button[] { };
    
        %>

         <div class="section">

             <div class="content pagination">
                 <%: Html.PagedListPager(Model.PagedItems as IPagedList, page => Url.Action("Index", new {page}))%>
             </div>

             <div class="content actions">
                <%--<input type="button" class="button" onclick="getFile('core/process.php?v=add_locations');location.replace('#v/add_locations');" value="Add A New Location">--%>
                <% Html.RenderPartial("DisplayForObject", bottomLeftButtons); %>
             </div>

         </div>
