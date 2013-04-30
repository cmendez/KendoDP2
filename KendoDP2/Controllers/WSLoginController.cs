using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Controllers
{
    public class WSLoginController : Controller
    {
        //
        // GET: /WSLogin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            return Json(new DP2MembershipProvider().ValidateUser(username, password));
        }
    }
}
