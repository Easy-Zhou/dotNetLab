using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file,String categoryName)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath("~/Content/img");
            file.SaveAs(Path.Combine(filePath,fileName));
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Category cates = new Models.T_Shop_Category();
            cates.CategoryName = categoryName;
            cates.PicUrl = fileName;
            db.T_Shop_Category.Add(cates);
            db.SaveChanges();
            return View();
        }
    }
}