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
            String fileName = "";
            if (file == null || String.IsNullOrEmpty(file.FileName) || file.ContentLength == 0)
            {

            }
            else
            {
                fileName = file.FileName;
                String filePath = Server.MapPath("~/Content/img");
                file.SaveAs(Path.Combine(filePath, fileName));
            }
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Category cates = new Models.T_Shop_Category();
            cates.CategoryName = categoryName;
            cates.PicUrl = fileName;
            db.T_Shop_Category.Add(cates);
            db.SaveChanges();
            return Redirect("/admin/category/index");
        }

        public JsonResult Delete(int id)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Category cate = db.T_Shop_Category.Find(id);
            db.T_Shop_Category.Remove(cate);
            int result = 0;
            try
            {
                result = db.SaveChanges();
            }
            catch
            {
                return Json(new {code=500,message="删除失败！" });
            }
            if(result > 0)
            {
                return Json(new { code = 200, message = "删除成功" });
            }
            else
            {
                return Json(new { code = 500, message = "删除失败" });
            }
        }

        public ActionResult Update(int Id)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Category cate = db.T_Shop_Category.Single(m => m.Id == Id);
            ViewBag.category = cate;
            return View();
        }

        public ActionResult UpdateSave(String categoryName, HttpPostedFileBase file,int id)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Category cates = db.T_Shop_Category.Single(m => m.Id == id);
            cates.CategoryName = categoryName;
            if (file == null || String.IsNullOrEmpty(file.FileName) || file.ContentLength == 0)
            {
                
            }
            else
            {
                String fileName = file.FileName;
                String filePath = Server.MapPath("~/Content/img");
                file.SaveAs(Path.Combine(filePath, fileName));
                cates.PicUrl = fileName;
            }
            db.SaveChanges();
            return Redirect("/admin/category/index");
        }
    }
}