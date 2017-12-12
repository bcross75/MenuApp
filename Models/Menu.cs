using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuApp.Models
{
    public class Menu
    {
        public List<MenuItem> Appetizers { get; set; }
        public List<MenuItem> Entrees { get; set; }
        public List<MenuItem> Desserts { get; set; }
        public List<MenuItem> Salads { get; set; }
        public List<MenuItem> Drinks { get; set; }
        public List<MenuItem> BarDrinks { get; set; }
    }
}