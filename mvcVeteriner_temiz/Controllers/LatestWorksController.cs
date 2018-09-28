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
using System.Data.Entity.Migrations;
using mvcVeteriner_temiz.Utils;

namespace mvcVeteriner_temiz.Controllers
{
    public class LatestWorksController : Controller
    {
        private GunesliVeterinerEntities db = new GunesliVeterinerEntities();

        // GET: LatestWorks
        public ActionResult Index()
        {
            return View(db.LatestWorks.ToList());
        }

        // GET: LatestWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LatestWorks latestWorks = db.LatestWorks.Find(id);
            if (latestWorks == null)
            {
                return HttpNotFound();
            }
            return View(latestWorks);
        }

        // GET: LatestWorks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LatestWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ImageTitle")] LatestWorks latestWorks)
        {
            if (ModelState.IsValid)
            {
                db.LatestWorks.Add(latestWorks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(latestWorks);
        }

        // GET: LatestWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LatestWorks latestWorks = db.LatestWorks.Find(id);
            if (latestWorks == null)
            {
                return HttpNotFound();
            }
            return View(latestWorks);
        }

        // POST: LatestWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ImagePath,ImageTitle")] LatestWorks latestWorks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(latestWorks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(latestWorks);
        }

        // GET: LatestWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LatestWorks latestWorks = db.LatestWorks.Find(id);
            if (latestWorks == null)
            {
                return HttpNotFound();
            }
            return View(latestWorks);
        }

        // POST: LatestWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LatestWorks latestWorks = db.LatestWorks.Find(id);
            db.LatestWorks.Remove(latestWorks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddImage(int ID)
        {
            LatestWorks ltw = db.LatestWorks.Find(ID);

            return View(ltw);
        }
        [HttpPost]
        public ActionResult AddImage(App_Class.Images image, int ID)
        {
            foreach (var folder in image.Folders)
            {
                if (folder.ContentLength > 2)
                {
                    //dosya adı ve adresi
                    var folderName = Guid.NewGuid() + Path.GetExtension(folder.FileName);
                    var adress = Path.Combine(Server.MapPath("~/VeterinerImages"), folderName);
                    folder.SaveAs(adress);

                    //veritabanına kaydet->

                    LatestWorks r = db.LatestWorks.FirstOrDefault(x => x.ID == ID);
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
