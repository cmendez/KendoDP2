﻿using KendoDP2.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KendoDP2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() : base()
        {
            ViewBag.Area = "";
        }

        [Authorize()]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            using (DP2Context context = new DP2Context())
            {
                var T = context.TablaUsuarios.One(o => o.Username.Equals(User.Identity.Name));
                var user = T != null ? T.ToDTO() : null;
                Session["enlinea"] = user;
            }
            return View();                
        }

        [Authorize()]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize()]
		public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (new DP2MembershipProvider().ValidateUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Los datos ingresados son incorrectos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize()]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
