using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.admin.Controllers
{
    public class UserController : Controller
    {
        // GET: admin/User
        public ActionResult Index()
        {
            return View();
        }
    }
}