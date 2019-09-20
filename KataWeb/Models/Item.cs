using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KataWeb.Models
{
    public class Item
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}