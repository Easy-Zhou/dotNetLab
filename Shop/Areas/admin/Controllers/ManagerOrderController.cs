using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.admin.Controllers
{
    public class ManagerOrderController : Controller
    {
        // GET: admin/ManagerOrder
        public ActionResult Index()
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Shop_Order> orders = db.T_Shop_Order.ToList();
            ViewBag.lst = orders;
            return View();
        }

        public JsonResult Delete(int id)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Order order = db.T_Shop_Order.Find(id);
            db.T_Shop_Order.Remove(order);
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

        public ActionResult Update(int Id = 1)
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Order order = db.T_Shop_Order.Single(m => m.Id == Id);
            ViewBag.order = order;
            return View();
        }

        public ActionResult UpdateSave(String username, String phoneNumber, String Address, Decimal price, int id)
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Order order = db.T_Shop_Order.Single(m => m.Id == id);
            order.Price = price;
            order.T_Base_Address.PhoneNumber = phoneNumber;
            order.T_Base_Address.Name = username;
            order.T_Base_Address.Address = Address;
            db.SaveChanges();
            return Redirect("/admin/ManagerOrder/index");
        }

    }
}