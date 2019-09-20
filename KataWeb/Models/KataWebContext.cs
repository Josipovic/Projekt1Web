using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KataWeb.Models
{
    public class KataWebContext:DbContext
    {
        public System.Data.Entity.DbSet<KataWeb.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<KataWeb.Models.Category> Categories { get; set; }
    }
}