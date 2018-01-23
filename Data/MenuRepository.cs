using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MenuApp.Models;
using MenuApp.Controllers;


namespace MenuApp.Data
{
    public class MenuRepository
    {
           
        public const string ConnectionString = @"Data Source =.\SQLExpress; Initial Catalog = Menu; Integrated Security = True";

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
            var connectionString = @"Data Source =.\SQLExpress; Initial Catalog = Menu; Integrated Security = True";
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand("INSERT INTO[dbo].[MenuItem]([Name],[Description],[CalorieCount],[Price],[CategoryId],[Active]) VALUES(@Name, @Description, @CalorieCount, @Price, @CategoryId, @Active)", connection);
            command.Parameters.Add(new SqlParameter("@Name", menuItem.Name));
            command.Parameters.Add(new SqlParameter("@Description", menuItem.Description));
            command.Parameters.Add(new SqlParameter("@CalorieCount", menuItem.CalorieCount));
            command.Parameters.Add(new SqlParameter("@Price", menuItem.Price));
            command.Parameters.Add(new SqlParameter("@CategoryId", menuItem.Category));
            command.Parameters.Add(new SqlParameter("@Active", menuItem.Active));
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}