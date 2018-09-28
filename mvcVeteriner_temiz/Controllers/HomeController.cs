using mvcVeteriner_temiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace mvcVeteriner_temiz.Controllers
{
    public class HomeController : Controller
    {
        GunesliVeterinerEntities db = new GunesliVeterinerEntities();

        // GET: Home
        public ActionResult Index()
        {
            List<IndexSlider> listSlider = db.IndexSlider.ToList();
            IList<Index3img> listImage = db.Index3img.ToList();
            SliderViewModel viewModel = new SliderViewModel
            {
                SliderList = listSlider,
                LogoImageList = listImage
            };



            return View(viewModel);
        }

        public ActionResult AboutUs()
        {
            List<Crew> crew = db.Crew.ToList();
            return View(crew);
        }

        public ActionResult OurServices()
        {
            List<Services> services = db.Services.ToList();
            return View(services);
        }
        public ActionResult Gallery()
        {
            List<Gallery> galeri = db.Gallery.ToList();

            return View(galeri);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public PartialViewResult latestWorks()
        {
            List<LatestWorks> latestworks = db.LatestWorks.ToList();
            return PartialView(latestworks);
        }

        [HttpPost]
        public ActionResult Contact(IletisimModel model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("İsim: " + model.Name);
                body.AppendLine("Tel: " + model.Phone);
                body.AppendLine("Eposta: " + model.Email);
                body.AppendLine("Konu: " + model.Message);
                Gmail.SendMail(body.ToString());
                ViewBag.Success = true;
            }
            return View();
        }
    }
}