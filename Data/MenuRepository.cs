using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MenuApp.Models;
using MenuApp.Controllers;


namespace MenuApp.Data
{
    public class MenuRepository
    {
           
        public string ConnectionString = ConfigurationManager.ConnectionStrings["MenuContext"].ConnectionString;

        public MenuItem GetMenuItem(int id)
        {
            var connection = new SqlConnection(ConnectionString);
           
            var command = new SqlCommand("select id, Name, Description, CalorieCount, Price, Categoryid, active from MenuItem where id =@id", connection);
            command.Parameters.Add(new SqlParameter("@id", id));
            connection.Open();
            var reader = command.ExecuteReader();
            MenuItem menuItem = null;

            if (reader.HasRows)
            {
                reader.Read();
                
                menuItem = new MenuItem
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        CalorieCount = reader.GetInt32(3),
                        Price = reader.GetDecimal(4),
                        Category = (Categories)reader.GetInt32(5),
                        Active = reader.GetBoolean(6)
                    };
            }
            connection.Close();
            return menuItem;
        }
        public void UpdateMenuItem(MenuItem menuItem)
        {
            var context = new MenuContext();
            var existing = context.MenuItems.FirstOrDefault(x => x.Id == menuItem.Id);
            if (existing != null)
            {
                existing.Name = menuItem.Name;
                existing.Description = menuItem.Description;
                existing.CalorieCount = menuItem.CalorieCount;
                existing.Price = menuItem.Price;
                existing.Category = menuItem.Category;
                existing.Active = menuItem.Active;
            }

            context.SaveChanges();
        }

        public void CreateMenuItem(MenuItem menuItem)
        {
            var context = new MenuContext();
            context.MenuItems.Add(menuItem);
            context.SaveChanges();
        }

        public Menu GetMenu(bool onlyactive)
        {
            var context = new MenuContext();
            var menuItems = context.MenuItems.ToList();
            var menuItemModels = menuItems.Select(x => new MenuItemModel
                {
                    Id = x.Id,
                    Active = x.Active,
                    CalorieCount = x.CalorieCount,
                    Category = x.Category,
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price.ToString("C")
                }).ToList();

            
            if (onlyactive)
            {
                menuItemModels = menuItemModels.Where(x => x.Active).ToList();
            }
            var menu = new Menu();
            menu.Appetizers = menuItemModels.Where(x => x.Category == Categories.Appetizers).ToList();
            menu.Salads = menuItemModels.Where(x => x.Category == Categories.Salads).ToList();
            menu.Entrees = menuItemModels.Where(x => x.Category == Categories.Entrees).ToList();
            menu.Desserts = menuItemModels.Where(x => x.Category == Categories.Desserts).ToList();
            menu.Drinks = menuItemModels.Where(x => x.Category == Categories.Drinks).ToList();
            menu.BarDrinks = menuItemModels.Where(x => x.Category == Categories.BarDrinks).ToList();
            return menu;
        }
    }
}