using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: admin/Order
        public ActionResult Index()
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Shop_Order> orders = db.T_Shop_Order.ToList();
            ViewBag.lst = orders;
            return View();
        }
    }
}