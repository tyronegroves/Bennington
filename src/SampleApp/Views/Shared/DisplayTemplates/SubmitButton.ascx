﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bennington.Cms.Buttons.SubmitButton>" %>
 <input type="submit" class="button" value="<%:Model.Text %>" <%=string.IsNullOrEmpty(Model.Id) ? "" : string.Format("id=\"{0}\"", Model.Id) %>/>