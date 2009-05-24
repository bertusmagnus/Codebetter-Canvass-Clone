<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="CodeBetter.Canvas.Web.ApplicationViewPage<User>" %>
<asp:Content ContentPlaceHolderID="HeadContainer" runat="server">
    <%=Html.IncludeJs("jquery.validator") %>    
</asp:Content>
<asp:Content ID="MainContainer" ContentPlaceHolderID="MainContainer" runat="server">
    <h2>Register Now</h2>
    <form id="register" method="post" action="<%=Html.LinkTo<HomeController>(c => c.Register()) %>">
        <div class="formItem">
            <label>name</label>
            <%=this.TextBox(u => u.Name)%>
        </div>    
        <div class="formItem">
            <label>email</label>
            <%=this.TextBox(u => u.Credentials.Email)%>
        </div>    
        <div class="formItem">
            <label>password</label>
            <%=this.Password(u => u.Credentials.Password).Value(string.Empty)%>
        </div>     
        <div class="formControls">
            <input type="submit" value="Register" />
        </div>        
    </form>    
</asp:Content>

<asp:Content ContentPlaceHolderID="DocumentReadyContainer" runat="server">
    var options = {<%=Html.RulesFor<User>() %>, <%=Html.GetValidationErrors()%>};
    //you can easily customize rules on a per-page basis
    options.rules.Credentials_Password.tip += ' (between 4-50 characters)';
    $('#register').validate(options);    
</asp:Content>

