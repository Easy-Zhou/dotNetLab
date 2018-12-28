using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: admin/Product
        public ActionResult Index()
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Shop_Product> products = db.T_Shop_Product.ToList();
            ViewBag.lstProduct = products;
            //List<Shop.Models.T_Shop_Category> cates = db.T_Shop_Category.ToList();
            //ViewBag.lst = cates;
            //if (cates.Count > 0)
            //{
            //    List<Shop.Models.T_Shop_Product> products = cates[0].T_Shop_Product.ToList();
            //    ViewBag.lstProduct = products;
            //}
            //else
            //{
            //    ViewBag.lstProduct = null;
            //}
            return View();
        }

        public JsonResult GetProduct(int Id)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Category cate = db.T_Shop_Category.Single(m => m.Id == Id);
            List<Shop.Models.T_Shop_Product> products = cate.T_Shop_Product.ToList();
            ViewBag.lstProduct = products;
            return Json(new { code = 200, message = "获取成功" });
        }

        public ActionResult Add()
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            List<Shop.Models.T_Shop_Category> cates = db.T_Shop_Category.ToList();
            ViewBag.category = cates;
            return View();
        }

        [HttpPost]
        public ActionResult Add(String productName, int categoryId, Decimal productPrice, int productStock, HttpPostedFileBase file)
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
            Shop.Models.T_Shop_Product product = new Models.T_Shop_Product();
            product.ProductName = productName;
            product.CategoryId = categoryId;
            product.price = productPrice;
            product.StockAmount = productStock;
            product.PicUrl = fileName;
            db.T_Shop_Product.Add(product);
            db.SaveChanges();
            return Redirect("/admin/product/index");
        }

        public JsonResult Delete(int id)
        {
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Product product = db.T_Shop_Product.Find(id);
            db.T_Shop_Product.Remove(product);
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
            Shop.Models.T_Shop_Product product = db.T_Shop_Product.Single(m => m.Id == Id);
            List<Shop.Models.T_Shop_Category> cates = db.T_Shop_Category.ToList();
            ViewBag.product = product;
            ViewBag.category = cates;
            return View();
        }

        public ActionResult UpdateSave(String productName, int categoryId, Decimal productPrice, int productStock, HttpPostedFileBase file, int id)
        {
            Shop.App_Start.ManageUser.IsLogin();
            Shop.Models.ShopEntities db = new Models.ShopEntities();
            Shop.Models.T_Shop_Product product = db.T_Shop_Product.Single(m => m.Id == id);
            product.ProductName = productName;
            product.CategoryId = categoryId;
            product.price = productPrice;
            product.StockAmount = productStock;
            if (file == null || String.IsNullOrEmpty(file.FileName) || file.ContentLength == 0)
            {

            }
            else
            {
                String fileName = file.FileName;
                String filePath = Server.MapPath("~/Content/img");
                file.SaveAs(Path.Combine(filePath, fileName));
                product.PicUrl = fileName;
            }
            db.SaveChanges();
            return Redirect("/admin/product/index");
        }

    }
}