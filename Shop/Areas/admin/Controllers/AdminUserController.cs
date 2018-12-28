using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.admin.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: admin/AdminUser
        public ActionResult Index()
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Base_User> users = db.T_Base_User.Where(m => m.type == 1).ToList();
            ViewBag.lst = users;
            return View();
        }

        public ActionResult Add()
        {
            Shop.App_Start.ManageUser.IsLogin();
            return View();
        }

        public ActionResult AddSave(String username, String password, String realName, String email)
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Base_User user = new Models.T_Base_User();
            user.Username = username;
            user.Password = password;
            user.Realname = realName;
            user.Email = email;
            user.type = 1;
            db.T_Base_User.Add(user);
            db.SaveChanges();
            return Redirect("/admin/AdminUser/index");
        }

        public JsonResult Delete(int id)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Base_User user = db.T_Base_User.Find(id);
            db.T_Base_User.Remove(user);
            int result = 0;
            try
            {
                result = db.SaveChanges();
            }
            catch
            {
                return Json(new { code = 500, message = "删除失败！" });
            }
            if (result > 0)
            {
                return Json(new { code = 200, message = "删除成功" });
            }
            else
            {
                return Json(new { code = 500, message = "删除失败" });
            }
        }

        public ActionResult Update(int Id=1)
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Base_User user = db.T_Base_User.Single(m => m.Id == Id);
            ViewBag.user = user;
            return View();
        }

        public ActionResult UpdateSave(String username, String password, String realName, String email, int id)
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Base_User user = db.T_Base_User.Single(m => m.Id == id);
            user.Username = username;
            user.Password = password;
            user.Realname = realName;
            user.Email = email;
            db.SaveChanges();
            return Redirect("/admin/AdminUser/index");
        }

        public JsonResult LoginCheck(String Username, String Password)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            if (db.T_Base_User.Where(m => m.Username == Username && m.Password == Password && m.type == 1).ToList().Count > 0)
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
            Shop.App_Start.ManageUser.IsLogin();
            Session["ticket"] = null;
            return Redirect("/admin/AdminUser/Login");
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

    }
}