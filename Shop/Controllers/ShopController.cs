using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Shop_Category> lst = db.T_Shop_Category.ToList();
            ViewBag.lst = lst;
            return View();
        }

        public ActionResult List(int CategoryId=1)
        {

            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Category category = db.T_Shop_Category.Single(m => m.Id == CategoryId);
            List<Shop.Models.T_Shop_Product> lst = category.T_Shop_Product.ToList();
            ViewBag.lst = lst; 
            return View();
        }

        public ActionResult Detail(int Id=1)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Product product = db.T_Shop_Product.Single(m => m.Id == Id);
            ViewBag.item = product;
            return View();
        }

    }
}