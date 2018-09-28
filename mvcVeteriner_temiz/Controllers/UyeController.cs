using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcVeteriner_temiz.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(FormCollection form)
        {
            string username = form["email"].Trim();
            string password = form["password"].Trim();

            if (username == "gokay" && password == "1234")
            {
                Session["Admin"] = "1";
                return RedirectToAction("~/AdminHome/Index");
            }


            return View("Index");
        }
    }
}