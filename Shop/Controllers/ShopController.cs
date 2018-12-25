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

        public JsonResult AddCart(int userId,int productId)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Cart cart = null;
            cart = db.T_Shop_Cart.SingleOrDefault(m => m.UserId == userId && m.ProductId == productId);
            if (cart != null)
            {
                cart.Count += 1;
            }
            else
            {
                cart = new Models.T_Shop_Cart();
                cart.ProductId = productId;
                cart.UserId = userId;
                cart.Count = 1;
                db.T_Shop_Cart.Add(cart);
            }
            if (db.SaveChanges() > 0)
            {
                return Json(new { code = 1, message = "添加成功" });
            }
            return Json(new { code = 0, message = "添加失败" }); ;
        }

    }
}