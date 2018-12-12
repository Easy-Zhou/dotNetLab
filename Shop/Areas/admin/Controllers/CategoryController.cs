using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: admin/Category
        public ActionResult Index()
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Shop_Category> cates = db.T_Shop_Category.ToList();
            ViewBag.lst = cates;
            return View();
        }
    }
}