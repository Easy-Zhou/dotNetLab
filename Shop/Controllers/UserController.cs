using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public JsonResult RegisterSave(string Username, string Password, string Realname, string Email)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            if(db.T_Base_User.Where(m => m.Username == Username).ToList().Count > 0)
            {
                return Json(new { code = 0, message = "用户名已存在，注册失败" });
            }
            else
            {
                Shop.Models.T_Base_User user = new Models.T_Base_User();
                user.Username = Username;
                user.Password = Password;
                user.Realname = Realname;
                user.Email = Email;
                user.type = 0;
                db.T_Base_User.Add(user);
                db.SaveChanges();
                return Json(new { code = 1, message = "注册成功"});
            }
        }

        //public ActionResult LoginCheck(String Username, String Password)
        //{
        //    Shop.Models.ShopEntities db = new Models.ShopEntities();
        //    if(db.T_Base_User.Where(m => m.Username==Username && m.Password == Password).ToList().Count > 0)
        //    {
        //        Shop.Models.T_Base_User user = new Models.T_Base_User();
        //        user.Username = Username;
        //        user.Password = Password;
        //        Session["ticket"] = user;
        //    }
        //    return RedirectToAction("index", "shop");
        //}

        public JsonResult LoginCheck(String Username, String Password)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            if (db.T_Base_User.Where(m => m.Username == Username && m.Password == Password).ToList().Count > 0)
            {

                Shop.Models.T_Base_User user = db.T_Base_User.Single(m => m.Username == Username);
                Session["ticket"] = user;
                return Json(new { code = 1, message = "登录成功" });
            }
            else
            {
                return Json(new { code = 0, message = "登录失败" });
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["ticket"] = null;
            return RedirectToAction("index", "shop");
        }

        public JsonResult CheckUsername(String Username)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            if (db.T_Base_User.Where(m => m.Username == Username).ToList().Count > 0)
            {
                return Json(new { code = 0, message = "用户名已存在" });
            }
            return Json(new { code = 1, message = "用户名可使用" });
        }

        public ActionResult Order()
        {
            Shop.Models.T_Base_User user = null;
            if(Session["ticket"]==null)
            {
                return Redirect("/user/login");
            }
            else
            {
                user = (Shop.Models.T_Base_User)Session["ticket"];
            }
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Shop_Order> order = user.T_Shop_Order.ToList();
            ViewBag.lst = order;
            return View();
        }

        public ActionResult OrderDetail(int Id)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Order order = db.T_Shop_Order.Single(m => m.Id == Id);
            ViewBag.order = order;
            return View();
        }

        public ActionResult Address(int Id)
        {
            Shop.Models.T_Base_User user = null;
            if (Session["ticket"] == null)
            {
                return Redirect("/user/login");
            }
            else
            {
                user = (Shop.Models.T_Base_User)Session["ticket"];
            }

            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Base_Address> ads = user.T_Base_Address.ToList();
            ViewBag.lst = ads;
            return View();
        }
    }
}