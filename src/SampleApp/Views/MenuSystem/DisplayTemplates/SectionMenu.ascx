<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Models.SectionMenu>" %>
<div id="menucontainer" class="clearfix">

    <ul class="menu clearfix">
        <%if (Model.Items.Any())
          {%>
        <%:Html.DisplayFor(x => x.Items)%>
        <%
          }
          else
          {%>
          <li><a href="#">&nbsp;</a></li>
          <%
          }%>
    </ul>
</div>
