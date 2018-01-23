using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.SqlClient;
using MenuApp.Data;
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
            var command = new SqlCommand("select m.Id, m.Name, m.Description, m.CalorieCount, m.Price, c.id as Categoryid, m.Active from MenuItem m join Category c on m.CategoryId = c.Id where m.Active =1", connection);
            connection.Open();
            var reader = command.ExecuteReader();
            var menuItems = new List<MenuItemModel>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    menuItems.Add(new MenuItemModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        CalorieCount = reader.GetInt32(3),
                        Price = reader.GetDecimal(4).ToString("C"),
                        Category = (Categories)reader.GetInt32(5),
                        Active = reader.GetBoolean(6)
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