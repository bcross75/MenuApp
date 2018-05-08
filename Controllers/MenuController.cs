using System.Web.Mvc;
using MenuApp.Data;

namespace MenuApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuRepository _repository;

        public MenuController(IMenuRepository repository)
        {
            _repository = repository;
        }
        // GET: Menu
        public ActionResult Index()
        {
            var menu = _repository.GetMenu(true);
            return View(menu);
        }
    }
}