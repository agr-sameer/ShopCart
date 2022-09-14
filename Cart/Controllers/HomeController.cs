using Cart.Data;
using Cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cart.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        List<ShopCart> cartList = new List<ShopCart>();
        public HomeController()
        {
            db = new DataContext();
            cartList = new List<ShopCart>();
        }

        public ActionResult Index()
        {
            if (TempData["ShopCart"] != null)
            {
                float x = 0;
                List<ShopCart> list = TempData["ShopCart"] as List<ShopCart>;
                foreach (var item in list)
                {
                    x += item.CartTotal;

                }

                TempData["total"] = x;
            }
            TempData.Keep();
            return View(db.Products.OrderByDescending(x => x.ProductId).ToList());
        }

        [HttpPost]
        public ActionResult Index(int ItemId)
        {
            Product p = db.Products.Where(x => x.ProductId == ItemId).SingleOrDefault();
            ShopCart c = new ShopCart();

            if (TempData["ShopCart"] != null)
            {
                cartList = TempData["ShopCart"] as List<ShopCart>;
            }
            if (cartList.Any(model => model.ProductId == ItemId))
            {
                c = cartList.Single(model => model.ProductId == ItemId);
                c.ProductQty = c.ProductQty + 1;
                c.CartTotal = c.ProductQty * c.ProductCost;
            }
            else
            {
                c.ProductId = p.ProductId;
                c.ProductCost = p.ProductCost;
                c.ProductQty = 1;
                c.ProductImage = p.ProductImage;
                c.CartTotal = c.ProductCost * c.ProductQty;
                c.ProductName = p.ProductName;
                cartList.Add(c);
            }


            TempData["ShopCart"] = cartList;


            TempData.Keep();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        

        [HttpPost]
        public ActionResult AddToCart(Product product )
        {
            Product p = db.Products.Where(x => x.ProductId == product.ProductId).SingleOrDefault();
            ShopCart c = new ShopCart();
            c.ProductId = p.ProductId;
            c.ProductCost = p.ProductCost;
            c.ProductQty = 1;
            c.ProductImage = p.ProductImage;
            c.CartTotal = c.ProductCost * c.ProductQty;
            c.ProductName = p.ProductName;
            if (TempData["ShopCart"] == null)
            {
                cartList.Add(c);
                TempData["ShopCart"] = cartList;

            }
            else
            {
                List<ShopCart> cartListTemp = TempData["ShopCart"] as List<ShopCart>;
                cartListTemp.Add(c);
                TempData["ShopCart"] = cartListTemp;
            }

            TempData.Keep();




            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            TempData.Keep();


            return View();
        }

        [HttpPost]
        public ActionResult AddItem(int ItemId)
        {
            ShopCart c = new ShopCart();

            if (TempData["ShopCart"] != null)
            {
                cartList = TempData["ShopCart"] as List<ShopCart>;
            }
            if (cartList.Any(model => model.ProductId == ItemId))
            {
                c = cartList.Single(model => model.ProductId == ItemId);
                c.ProductQty = c.ProductQty + 1;
                c.CartTotal = c.ProductQty * c.ProductCost;
            }
            
            TempData["ShopCart"] = cartList;
            TempData.Keep();
            return RedirectToAction("Checkout");
        }

        [HttpPost]
        public ActionResult RemoveItem(int ItemId)
        {
            ShopCart c = new ShopCart();

            if (TempData["ShopCart"] != null)
            {
                cartList = TempData["ShopCart"] as List<ShopCart>;
            }
            if (cartList.Any(model => model.ProductId == ItemId))
            {
                c = cartList.Single(model => model.ProductId == ItemId);
                if (c.ProductQty > 1)
                {
                    c.ProductQty = c.ProductQty - 1;
                    c.CartTotal = c.ProductQty * c.ProductCost;
                }
                else
                {
                    ShopCart s = cartList.Find(m => m.ProductId == ItemId);
                    cartList.Remove(s);
                }
            }

            TempData["ShopCart"] = cartList;


            TempData.Keep();
            return RedirectToAction("Checkout");
        }
    }
}