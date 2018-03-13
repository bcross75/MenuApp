using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MenuApp.Data
{
    public class MenuContext: DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }

        public MenuContext()
            : base("MenuContext")
        {
            Database.SetInitializer<MenuContext>(new NullDatabaseInitializer<MenuContext>());
        }
    }

}