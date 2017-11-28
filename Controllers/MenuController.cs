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
            var command = new SqlCommand("select m.Name, m.Description, m.CalorieCount, m.Price, c.Name as Category from MenuItem m join Category c on m.CategoryId = c.Id", connection);
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
                        Price = reader.GetDecimal(3),
                        Category = reader.GetString(4)

                    });

                }
            }
            connection.Close();
            return View(menuItems);
        }
    }
}