﻿using System.Web.Mvc;
using MenuApp.Data;

namespace MenuApp.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            var repository = new MenuRepository();
            var menu = repository.GetMenu(true);
            return View(menu);
        }
    }
}