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
    public class IndexSlidersController : Controller
    {
        private GunesliVeterinerEntities db = new GunesliVeterinerEntities();

        // GET: IndexSliders
        public ActionResult Index()
        {
            return View(db.IndexSlider.ToList());
        }

        // GET: IndexSliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexSlider indexSlider = db.IndexSlider.Find(id);
            if (indexSlider == null)
            {
                return HttpNotFound();
            }
            return View(indexSlider);
        }

        // GET: IndexSliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndexSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Statement")] IndexSlider indexSlider)
        {
            if (ModelState.IsValid)
            {
                db.IndexSlider.Add(indexSlider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(indexSlider);
        }

        // GET: IndexSliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexSlider indexSlider = db.IndexSlider.Find(id);
            if (indexSlider == null)
            {
                return HttpNotFound();
            }
            return View(indexSlider);
        }

        // POST: IndexSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Statement,ImagePath")] IndexSlider indexSlider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indexSlider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(indexSlider);
        }

        // GET: IndexSliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexSlider indexSlider = db.IndexSlider.Find(id);
            if (indexSlider == null)
            {
                return HttpNotFound();
            }
            return View(indexSlider);
        }

        // POST: IndexSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IndexSlider indexSlider = db.IndexSlider.Find(id);
            db.IndexSlider.Remove(indexSlider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AddImage(int ID)
        {
            IndexSlider inds = db.IndexSlider.Find(ID);

            return View(inds);
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

                    IndexSlider r = db.IndexSlider.FirstOrDefault(x=>x.ID==ID);
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
