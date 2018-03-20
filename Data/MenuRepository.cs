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
            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("Update dbo.MenuItem Set Name = @Name,Description = @Description,CalorieCount = @CalorieCount,Price = @Price,CategoryId = @CategoryId,Active = @Active Where id = @id", connection);
            command.Parameters.Add(new SqlParameter("@Id", menuItem.Id));
            command.Parameters.Add(new SqlParameter("@Name", menuItem.Name));
            command.Parameters.Add(new SqlParameter("@Description", menuItem.Description));
            command.Parameters.Add(new SqlParameter("@CalorieCount", menuItem.CalorieCount));
            command.Parameters.Add(new SqlParameter("@Price", menuItem.Price));
            command.Parameters.Add(new SqlParameter("@CategoryId", menuItem.Category));
            command.Parameters.Add(new SqlParameter("@Active", menuItem.Active));
            connection.Open();
            var reader = command.ExecuteNonQuery();
        }
        public void CreateMenuItem(MenuItemModel menuItem)
        {
            var context = new MenuContext();
            var price = decimal.Parse(menuItem.Price);
            context.MenuItems.Add(new MenuItem
            {
                Name = menuItem.Name,
                Description = menuItem.Description,
                CalorieCount = menuItem.CalorieCount,
                Price = price,
                Category = menuItem.Category,
                Active = menuItem.Active
            });
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