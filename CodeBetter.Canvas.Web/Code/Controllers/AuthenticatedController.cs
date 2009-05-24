namespace CodeBetter.Canvas.Web
{
    using System.Web.Mvc;

    public class AuthenticatedController : ApplicationController
    {
        private User _user;
        public override User CurrentUser
        {
            get { return _user; }
        }

        protected IRepository Repository { get; private set; }

        public AuthenticatedController(IRepository repository)
        {
            Repository = repository;
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {            
            var userId = GetIdentity();
            if (userId == null)
            {
                context.Result = RedirectToLogin();
                return;
            }
            var user = Repository.Find<User>(userId.Value);
            if (user == null)
            {
                context.Result = RedirectToLogin();
                return;
            }
            ViewData["CurrentUser"] = user;
            _user = user;
            base.OnActionExecuting(context);
        }

        private int? GetIdentity()
        {
            if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return null;
            }
            int userId;
            return (!int.TryParse(User.Identity.Name, out userId)) ? null : (int?)userId;            
        }
    }
}