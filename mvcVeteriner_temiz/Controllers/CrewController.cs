using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcVeteriner_temiz.Models;
using System.IO;
using mvcVeteriner_temiz.App_Class;
using mvcVeteriner_temiz.Utils;

namespace mvcVeteriner_temiz.Controllers
{
    public class CrewController : Controller
    {
        private GunesliVeterinerEntities db = new GunesliVeterinerEntities();

        // GET: Crew
        public ActionResult Index()
        {
            return View(db.Crew.ToList());
        }

        // GET: Crew/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crew crew = db.Crew.Find(id);
            if (crew == null)
            {
                return HttpNotFound();
            }
            return View(crew);
        }

        // GET: Crew/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Crew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Statement,ImageTitle")] Crew crew)
        {
            if (ModelState.IsValid)
            {
                db.Crew.Add(crew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crew);
        }

        // GET: Crew/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crew crew = db.Crew.Find(id);
            if (crew == null)
            {
                return HttpNotFound();
            }
            return View(crew);
        }

        // POST: Crew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Statement,ImagePath,ImageTitle")] Crew crew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crew);
        }

        // GET: Crew/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crew crew = db.Crew.Find(id);
            if (crew == null)
            {
                return HttpNotFound();
            }
            return View(crew);
        }

        // POST: Crew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crew crew = db.Crew.Find(id);
            db.Crew.Remove(crew);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddImage (int id)
        {
            Crew crw = db.Crew.Find(id);
            return View(crw);
        }

        [HttpPost]
        public ActionResult AddImage(App_Class.Images image, int ID)
        {
            foreach (var folder in image.Folders)
            {
                if (folder.ContentLength > 0)
                {
                    //dosya adı ve adresi
                    var folderName = Guid.NewGuid() + Path.GetExtension(folder.FileName);
                    var adress = Path.Combine(Server.MapPath("~/VeterinerImages"), folderName);
                    folder.SaveAs(adress);

                    //veritabanına kaydet->

                    Crew r = db.Crew.FirstOrDefault(x => x.ID == ID);
                    r.ImagePath = folderName;
                    db.SaveChanges();

                }
                else
                {
                    ViewBag.Mesaj = "Yükleme işlemi yapılamadı!";
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
