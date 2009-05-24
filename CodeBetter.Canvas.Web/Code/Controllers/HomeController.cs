
namespace CodeBetter.Canvas.Web
{
    using System.Web.Mvc;
    
    public class HomeController : ApplicationController
    {
        private readonly IUserRepository _repository;

        public HomeController(IUserRepository repository) : base()
        {
            _repository = repository;            
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
    }
}
