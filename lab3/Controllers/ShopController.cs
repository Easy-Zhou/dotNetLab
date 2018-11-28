using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab3.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            lab3.Models.ShEntities db = new Models.ShEntities();
            List<lab3.Models.T_Shop_Category> lst = db.T_Shop_Category.ToList();
            ViewBag.lst = lst;
            return View();
        }
    }
}