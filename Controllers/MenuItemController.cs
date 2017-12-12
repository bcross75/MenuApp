using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuApp.Models;
using System.Data.SqlClient;
using MenuApp.Data;
using System.Globalization;

namespace MenuApp.Controllers
{
    public class MenuItemController : Controller
    {   
        public ActionResult Create()
        {
            return View();
        }
        // GET: MenuItem
        [HttpPost]
        public ActionResult CreateMenuItem(MenuItemModel menuItem) 
        {
            var connectionString = @"Data Source =.\SQLExpress; Initial Catalog = Menu; Integrated Security = True";
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand("INSERT INTO[dbo].[MenuItem]([Name],[Description],[CalorieCount],[Price],[CategoryId],[Active]) VALUES(@Name, @Description, @CalorieCount, @Price, @CategoryId, @Active)", connection);
            command.Parameters.Add(new SqlParameter("@Name",menuItem.Name));
            command.Parameters.Add(new SqlParameter("@Description",menuItem.Description));
            command.Parameters.Add(new SqlParameter("@CalorieCount",menuItem.CalorieCount));
            command.Parameters.Add(new SqlParameter("@Price",menuItem.Price));
            command.Parameters.Add(new SqlParameter("@CategoryId",menuItem.Category));
            command.Parameters.Add(new SqlParameter("@Active", menuItem.Active));
            connection.Open();
            var reader = command.ExecuteNonQuery();
            var menuItems = new List<MenuItemModel>();
            return Redirect("/Menu");
        }
        public ActionResult Update(int id)
        {
            var repo = new MenuRepository();
            var menuitem = repo.GetMenuItem(id);
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