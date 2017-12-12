using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuApp.Models;
using System.Data.SqlClient;

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
        public ActionResult CreateMenuItem(MenuItem menuItem) 
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
            var menuItems = new List<MenuItem>();
            return Redirect("/Menu");
        }
    }
}