using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using KataWeb.Models;
using System.Text;

namespace KataWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View("Cart");
        }

        public ActionResult AddToCart(int Id)
        {
            KataWebContext db = new KataWebContext();
            List<Item> cart = (List<Item>)Session["cart"];
            if (cart == null)
            {
                //ako je null nista ne postoji u sessionu
                cart = new List<Item>();
                var product = db.Products.Find(Id);
                Item item = new Item();
                item.Product = product;
                item.Count = 1;
                cart.Add(item);
                Session["cart"] = cart;
            }
            else
            {
                //lista postoji u sessionu
                //pretrazujem da li proizvod postoji u kosari
                int location = cart.FindIndex(x => x.Product.Id == Id);
                //nalazi index od elementa koji zadovoljava ovaj kriterij
                if (location == -1) //to znaci da proizvod nije pronadjen u listi
                {
                    var product = db.Products.Find(Id);
                    Item item = new Item();
                    item.Product = product;
                    item.Count = 1;
                    cart.Add(item);
                    Session["cart"] = cart;
                }
                else
                { //proizvod nije pronadjen na lokaciji u varijabli location
                    cart[location].Count++;
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        public ActionResult IncreaseCount(int Id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id == Id);
            cart[location].Count++;
            Session["cart"] = cart;

            return RedirectToAction("Index");

        }
        public ActionResult DecreaseCount(int Id)
        {

            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id == Id);
            if (cart[location].Count == 1)
            {
                cart.RemoveAt(location);
            }
            else
            {
                cart[location].Count--;
            }

            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int Id)
        {

            List<Item> cart = (List<Item>)Session["cart"];
            int location = cart.FindIndex(x => x.Product.Id == Id);
            cart.RemoveAt(location);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Checkout(AdressInformation info)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 20000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("bestmailever5@gmail.com", "kompjuter123");

            string mailMessage = "Nova narudžba je zaprimljena!" + Environment.NewLine + " " + "Kupac:" + info.FirstName + " " + info.LastName +
                Environment.NewLine + "Adresa:" + Environment.NewLine + info.City + Environment.NewLine +
                info.Adress + Environment.NewLine + info.ZipCode + Environment.NewLine + "Kontakt broj:" +
                Environment.NewLine + info.PhoneNumber + Environment.NewLine ;

            var products = (List<Item>)Session["cart"];
            decimal total = 0;
            foreach (var product in products)
            {
                if (product.Product.Discount == true)
                {

                    total += (product.Count * product.Product.DiscountPrice);
                }
                else
                {
                    total += (product.Count * product.Product.Price);
                }
                mailMessage += "Ukupni iznos narudžbe:" + " " + total + " " + "Kn" ;

            }
            MailMessage message = new MailMessage("bestmailever5@gmail.com", "katarina.josipovic@hotmail.com",
                "Narudžba", mailMessage);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(message);
            return View("OrderCompleted");
        }
    }
}