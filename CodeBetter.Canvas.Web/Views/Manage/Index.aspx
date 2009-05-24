<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="CodeBetter.Canvas.Web.ApplicationViewPage<PagedList<User>>" %>
<asp:Content ContentPlaceHolderID="HeadContainer" runat="server">
    <%= Html.IncludeJs("jquery.list")%>   
</asp:Content>
<asp:Content ID="MainContainer" ContentPlaceHolderID="MainContainer" runat="server">
<% if (!Model.HasRecords) { %>
    <strong>There are currently no users (humm...how did you log-in then?!)</strong>
<% } else { %>
    <table class="list" rel="<%=Html.LinkTo<ManageController>(c => c.View(0)) %>">
        <thead>
            <tr>
                <th>Name</th>
                <th>Email</th>                
            </tr>
        </thead>
        <tbody>
        <% Model.Data.Each(d => { %>
            <tr rel="<%=d.Id %>">
                <td><%=Html.Encode(d.Name) %></td>
                <td><%= Html.Encode(d.Credentials.Email)%></td>
            </tr>
        <% }); %>
        </tbody>
    </table>  
    <%Html.RenderPartial("pager", Model.PagedInfo);%>      
<% } %>
</asp:Content>


