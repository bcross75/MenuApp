using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using MenuApp.Models;

namespace MenuApp.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            var connectionString = @"Data Source =.\SQLExpress; Initial Catalog = Menu; Integrated Security = True";
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand("select m.Name, m.Description, m.CalorieCount, m.Price, c.id as Categoryid from MenuItem m join Category c on m.CategoryId = c.Id", connection);
            connection.Open();
            var reader = command.ExecuteReader();
            var menuItems = new List<MenuItem>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    menuItems.Add(new MenuItem
                    {
                        Name = reader.GetString(0),
                        Description = reader.GetString(1),
                        CalorieCount = reader.GetInt32(2),
                        Price = reader.GetDecimal(3).ToString("C"),
                        Category = (Categories)reader.GetInt32(4)

                    });

                }
            }
            connection.Close();
            var menu = new Menu();
            menu.Appetizers = menuItems.Where(x => x.Category == Categories.Appetizers).ToList();
            menu.Salads = menuItems.Where(x => x.Category == Categories.Salads).ToList();
            menu.Entrees = menuItems.Where(x => x.Category == Categories.Entrees).ToList();
            menu.Desserts = menuItems.Where(x => x.Category == Categories.Desserts).ToList();
            menu.Drinks = menuItems.Where(x => x.Category == Categories.Drinks).ToList();
            menu.BarDrinks = menuItems.Where(x => x.Category == Categories.BarDrinks).ToList();
            return View(menu);
        }
    }
}