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

        public JsonResult AddCart(int productId)
        {
            Shop.Models.T_Base_User user = null;
            if (Session["ticket"] == null)
            {
                return Json(new { code = 2, message = "用户未登录" });
            }
            else { user = (Shop.Models.T_Base_User)Session["ticket"]; }
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Cart cart = null;
            cart = db.T_Shop_Cart.SingleOrDefault(m => m.UserId == user.Id && m.ProductId == productId);
            if (cart != null)
            {
                cart.Count += 1;
            }
            else
            {
                cart = new Models.T_Shop_Cart();
                cart.ProductId = productId;
                cart.UserId = user.Id;
                cart.Count = 1;
                db.T_Shop_Cart.Add(cart);
            }
            if (db.SaveChanges() > 0)
            {
                return Json(new { code = 1, message = "添加成功" });
            }
            return Json(new { code = 0, message = "添加失败" }); ;
        }

        

        public ActionResult Cart()
        {
            Shop.Models.T_Base_User user = null;
            if (Session["ticket"] == null)
            {
                return Redirect("/user/login");
            }
            else { user = (Shop.Models.T_Base_User)Session["ticket"]; }
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            ViewBag.lst = user.T_Shop_Cart.ToList();
            return View();
        }

        public JsonResult Settle_accounts(List<int> signArray)
        {
            List<Shop.Models.T_Shop_Cart> carts = null;
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            if (signArray.Count > 0)
            {
                for (int i = 0; i < signArray.Count; i++)
                {
                    if(signArray[i] > 0) {
                        Shop.Models.T_Shop_Cart cart =  db.T_Shop_Cart.Single(m => m.Id == signArray[i]);
                        carts.Add(cart);
                        
                    }
                }

                return Json(new { code = 1, message = signArray[2] });
            }
            return Json(new { code = 0, message = "失败" });
        }

        public JsonResult UpdateCartCount(int count,int cartId)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Cart cart = db.T_Shop_Cart.Single(m => m.Id == cartId);
            cart.Count = count;
            int result = db.SaveChanges();
            if (result > 0)
                return Json(new { code = 1, message = "success" });
            return Json(new { code = 0, message = "failed" });
        }

        public ActionResult Order()
        {
            return View();
        }
    }
}