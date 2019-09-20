using KataWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace KataWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "KataWeb webshop.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "https://github.com/Josipovic";

            return View();
        }
        public ActionResult GetCategories()
        {

            KataWebContext db = new KataWebContext();
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }

        public ActionResult GetProducts(int categoryId, int? page, string search, string orderBy)
        {
            KataWebContext db = new KataWebContext();
            var products = db.Products.Where(x => x.CategoryId == categoryId).AsQueryable();
            if (string.IsNullOrEmpty(search) == false)
            {

                products = products.Where(p => p.Name.ToUpper().StartsWith(search.ToUpper()));
            }
            switch (orderBy)
            {

                case "NameDesc":
                    products = products.OrderByDescending(y => y.Name);
                    break;
                case "Price":
                    products = products.OrderBy(y => y.Price);
                    break;
                case "PriceDesc":
                    products = products.OrderByDescending(y => y.Price);
                    break;
                default:
                    products = products.OrderBy(y => y.Name);
                    break;
            }
            return View(products.ToList().ToPagedList(page ?? 1, 3));
        }
        public ActionResult GetRandomDiscountedProduct()
        {
            KataWebContext db = new KataWebContext();
            var products = db.Products.Where(x => x.Discount == true).ToList();
            int selectedIndex = (int)(DateTime.Now.Ticks % products.Count);
            var product = products[selectedIndex];
            return PartialView(product);
        }
        public ActionResult Details(int Id)
        {
            KataWebContext db = new KataWebContext();
            var product = db.Products.Find(Id);
            return View(product);
        }
        public ActionResult GetAdminMenu()
        {

            return PartialView();
        }
        public ActionResult GetCartCount()
        {


            return PartialView();
        }
    }
}