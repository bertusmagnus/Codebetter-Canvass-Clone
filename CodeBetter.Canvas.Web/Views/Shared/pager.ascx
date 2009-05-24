<%@ Control Language="C#" Inherits="CodeBetter.Canvas.Web.ApplicationViewUserControl<PagedInfo>" %>
<%
    var pagesToShow = 10;
    var pages = Model.TotalPages > pagesToShow ? pagesToShow : Model.TotalPages;
    var startPage = Model.CurrentPage > pagesToShow / 2 && Model.TotalPages > pagesToShow ? Model.CurrentPage - pagesToShow / 2 : 1;
    if (startPage + pages > Model.TotalPages)
    {
        pages = Model.TotalPages - startPage + 1;
    }
%> 
<%  if (pages > 1) { %>
    <div class="pager">
        <% if (startPage > 1) { %> <a class="page">1</a> <div class="page">..</div> <% } %>
        <% pages.Times(i => { %> <a class="<%= Model.CurrentPage == i ? " current" : "page"  %>"><%= startPage + i - 1%></a> <% }); %>
        <% if (startPage + pages < Model.TotalPages) { %> <div class="page">..</div> <a class="page"><%= Model.TotalPages%></a> <% } %>
        <div class="clear"></div>
    </div>    
<% } %>


