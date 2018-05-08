using System.Web.Mvc;
using MenuApp.Data;

namespace MenuApp.Controllers
{
    public class InventoryController : Controller
    {

        private readonly IMenuRepository _repository;
        public InventoryController(IMenuRepository repository)
        {
            _repository = repository;
        }
        // GET: Inventory
        public ActionResult Index()
        {
            var menu = _repository.GetMenu(false);
            return View(menu);
        }
    }
}