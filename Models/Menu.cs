using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuApp.Models
{
    public class Menu
    {
        public List<MenuItemModel> Appetizers { get; set; }
        public List<MenuItemModel> Entrees { get; set; }
        public List<MenuItemModel> Desserts { get; set; }
        public List<MenuItemModel> Salads { get; set; }
        public List<MenuItemModel> Drinks { get; set; }
        public List<MenuItemModel> BarDrinks { get; set; }
    }
}