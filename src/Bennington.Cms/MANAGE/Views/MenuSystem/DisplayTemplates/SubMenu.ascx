<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Models.SubMenu>" %>


<%--                <div class="clearfix" id="submenucontainer">
<%--                    <ul class="menu clearfix file_subnav">
                       <li id="active_files_sub" onclick="selected_sub(this);selected_sub(this);"><a href="#v/active_files">Active Catalogs</a></li>
                       <li id="inactive_files_sub" onclick="selected_sub(this);selected_sub(this);"><a href="#v/inactive_files">Inactive Catalogs</a></li>
                       <li id="pending_files_sub" onclick="selected_sub(this);selected_sub(this);"><a href="#v/pending_files">Pending Catalogs</a></li>
                       </ul>           
                </div>--%>



<div class="clearfix" id="submenucontainer">
    <ul class="menu clearfix file_subnav">
        <%:Html.DisplayFor(x => x.Items)%>
    </ul>
</div>
