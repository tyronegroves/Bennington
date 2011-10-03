<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Routes.aspx.cs" %>
<%@ Import Namespace="System.Web.Routing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"></head>
<body>
	<%--<h1><%=Server.MapPath("/") %></h1>--%>

    <form id="form1" runat="server">
    <div>
		<table border="1" width="10000px">
        <%foreach (Route route in RouteTable.Routes)%>
        <%{%>
		<tr>
            <td><%=route.Url %></td>
		</tr>
        <%}%>
		</table>
    </div>
    </form>
</body>
</html>
