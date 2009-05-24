<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="CodeBetter.Canvas.Web.ApplicationViewPage<Credentials>" %>
<asp:Content ContentPlaceHolderID="HeadContainer" runat="server">
    <%=Html.IncludeJs("jquery.validator") %>    
</asp:Content>
<asp:Content ID="MainContainer" ContentPlaceHolderID="MainContainer" runat="server">
    <h2>Login</h2>
    <form id="login" method="post" action="<%=Html.LinkTo<HomeController>(c => c.Login()) %>"> 
        <div class="formItem">
            <label>email</label>
            <%=this.TextBox(u => u.Email)%>
        </div>    
        <div class="formItem">
            <label>password</label>
            <%=this.Password(u => u.Password).Value(string.Empty)%>
        </div>     
        <div class="formControls">
            <input type="submit" value="Login" />
        </div>        
    </form>    
</asp:Content>

<asp:Content ContentPlaceHolderID="DocumentReadyContainer" runat="server">    
    $('#login').validate({<%=Html.RulesFor<Credentials>() %>, <%=Html.GetValidationErrors()%>});    
</asp:Content>

