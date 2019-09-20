using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KataWeb.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Naziv proizvoda")]
        public string Name { get; set; }
        [Display(Name = "Opis proizvoda")]
        public string Description { get; set; }
        [Display(Name = "Kratak opis")]
        public string ShortDesc { get; set; }
        [Display(Name = "Cijena ")]
        public decimal Price { get; set; }
        [Display(Name = "Na sniženju")]
        public bool Discount { get; set; }
        [Display(Name = "Cijena na sniženju")]
        public decimal DiscountPrice { get; set; }
        [Display(Name = "Putanja do slike")]
        public string ImageUrl { get; set; }
        [Display(Name = "Jamstvo")]
        public int Warranty { get; set; }
        [Display(Name = "Dostupan")]
        public bool Available { get; set; }
        [Display(Name = "Novo")]
        public bool New { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Boja")]
        public string Colour { get; set; }
        [Display(Name = "Dostava")]
        public string Delivery { get; set; }

        [Display(Name = "Kategorija")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}