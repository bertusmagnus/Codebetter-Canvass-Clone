<%@ Control Language="C#" AutoEventWireup="False" Inherits="System.Web.Mvc.ViewUserControl" %>
<% 
    var user = ((ApplicationController)ViewContext.Controller).CurrentUser; 
    if (user == null) {         
%>
    <a href="<%= Html.LinkTo<HomeController>(c => c.Login()) %>">Login</a>
<% } else  { %>
    <a href="<%= Html.LinkTo<HomeController>(c => c.Logout()) %>">Logout</a>
<% } %>
