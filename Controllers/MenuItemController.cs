﻿using System.Web.Mvc;
using MenuApp.Models;
using MenuApp.Data;
using System.Globalization;
using System.Net;

namespace MenuApp.Controllers
{
    public class MenuItemController : Controller
    {   
        public ActionResult Create()
        {
            return View(new MenuItemModel());
        }
        // GET: MenuItem
        [HttpPost]
        public ActionResult CreateMenuItem(MenuItemModel menuItem)
        {
            decimal price;
            var valid = decimal.TryParse(menuItem.Price, out price);
            if (!valid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Price.");
            }
        
            var repo = new MenuRepository();
            repo.CreateMenuItem(Map(menuItem));
            return Redirect("/Menu");
        }
        [HttpPost]
        public ActionResult DeleteMenuItem(int? id)
        {
            if(!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Id Required.");
            var repo = new MenuRepository();
            repo.DeleteMenuItem(id.Value);
            return Redirect("/Inventory");
        }

        public ActionResult Update(int? id)
        {
            if (!id.HasValue)
                return Redirect("/Menu");

            var repo = new MenuRepository();
            var menuitem = repo.GetMenuItem(id.Value);
            var model = Map(menuitem);
            return View(model);
        }
        // GET: MenuItem
        [HttpPost]
        public ActionResult UpdateMenuItem(MenuItemModel menuItemmodel)
        {
            var repo = new MenuRepository();
            var menuitem = Map(menuItemmodel);
            repo.UpdateMenuItem(menuitem);
            return Redirect("/Menu");
        }

        public MenuItemModel Map(MenuItem menuitem)
        {
            return new MenuItemModel
            {
                Id = menuitem.Id,
                Name = menuitem.Name,
                Description = menuitem.Description,
                CalorieCount = menuitem.CalorieCount,
                Price = menuitem.Price.ToString("C"),
                Category = menuitem.Category,
                Active = menuitem.Active
            };
        }

        public MenuItem Map(MenuItemModel menuitem)
        {
            return new MenuItem
            {
                Id = menuitem.Id,
                Name = menuitem.Name,
                Description = menuitem.Description,
                CalorieCount = menuitem.CalorieCount,
                Price = decimal.Parse(menuitem.Price, NumberStyles.Currency),
                Category = menuitem.Category,
                Active = menuitem.Active
            };
        }
    }
}