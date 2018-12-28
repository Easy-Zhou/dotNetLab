using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.App_Start
{
    public class ManageUser
    {
        public static void IsLogin()
        {
            if(HttpContext.Current.Session["ticket"] == null)
            {
                HttpContext.Current.Response.Redirect("/admin/AdminUser/Login");
            }
                
        }
    }
}