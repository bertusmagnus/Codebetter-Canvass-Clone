<%@ Control Language="C#" AutoEventWireup="False" Inherits="CodeBetter.Canvas.Web.ApplicationViewUserControl<string[]>" %>
<ul id="pageErrors">
    <% Model.Each(e =>
       { %>
        <li><%=e %></li>
    <% }); %>
</ul>
