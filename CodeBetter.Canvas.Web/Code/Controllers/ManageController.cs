namespace CodeBetter.Canvas.Web
{
    using System.Web.Mvc;

    public class ManageController : AuthenticatedController
    {
        private readonly IUserRepository _repository;
        public ManageController(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ViewResult Index(int? id)
        {
            var pager = new Pager(id ?? 1, 3);
            return View(_repository.GetUserList(pager));            
        }
        public ViewResult View(int id)
        {            
            return View(_repository.Find<User>(id));
        }
    }
}