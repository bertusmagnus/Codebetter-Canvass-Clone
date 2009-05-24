
namespace CodeBetter.Canvas.Web
{
    using System.Web.Mvc;
    using Validation;

    public class HomeController : ApplicationController
    {
        private readonly IUserRepository _repository;
        private readonly IAuthenticationManager _authentication;

        public HomeController(IAuthenticationManager authentication, IUserRepository repository)
        {
            _repository = repository;
            _authentication = authentication;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Register()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Register([Bind]User user)
        {
            if (!ModelState.IsValid) { return View(user); }
            _repository.Save(user);
            return View("RegistrationSuccessful");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Login()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login([Bind]Credentials credentials)
        {
            if (!ModelState.IsValid) { return View(credentials); }

            var user = _repository.FindByCredentials(credentials);
            if (user != null)
            {
                _authentication.SetAuthenticationToken(user.Id);
                return RedirectTo<ManageController>(c => c.Index(0));
            }
            AddError(new ValidationError(null, "Invalid username/password, please try again"));            
            return View(credentials);
        }        
        public ViewResult Logout()
        {
            _authentication.Signout();
            return View("index");
        }
    }
}
