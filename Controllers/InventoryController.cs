using System.Web.Mvc;
using MenuApp.Data;

namespace MenuApp.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            var repository = new MenuRepository();
            var menu = repository.GetMenu(false);
            return View(menu);
        }
    }
}