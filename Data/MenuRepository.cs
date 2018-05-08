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
    public interface IMenuRepository
    {
        MenuItem GetMenuItem(int id);
        void UpdateMenuItem(MenuItem menuItem);
        void CreateMenuItem(MenuItem menuItem);
        Menu GetMenu(bool onlyactive);
        void DeleteMenuItem(int id);
    }

    public class MenuRepository : IMenuRepository
    {
           

        public MenuItem GetMenuItem(int id)
        {
            return new MenuContext().MenuItems.FirstOrDefault(x => x.Id == id);
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

        public void DeleteMenuItem(int id)
        {
            var context = new MenuContext();
            var existing = context.MenuItems.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                context.MenuItems.Remove(existing);
                context.SaveChanges();

            }
        }
    }
}