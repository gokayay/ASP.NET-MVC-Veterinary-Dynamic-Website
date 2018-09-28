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
    public class Index3imgController : Controller
    {
        private GunesliVeterinerEntities db = new GunesliVeterinerEntities();

        // GET: Index3img
        public ActionResult Index()
        {
            return View(db.Index3img.ToList());
        }

        // GET: Index3img/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index3img index3img = db.Index3img.Find(id);
            if (index3img == null)
            {
                return HttpNotFound();
            }
            return View(index3img);
        }

        // GET: Index3img/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Index3img/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Statement")] Index3img index3img)
        {
            if (ModelState.IsValid)
            {
                db.Index3img.Add(index3img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(index3img);
        }

        // GET: Index3img/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index3img index3img = db.Index3img.Find(id);
            if (index3img == null)
            {
                return HttpNotFound();
            }
            return View(index3img);
        }

        // POST: Index3img/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Statement,ImagePath")] Index3img index3img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(index3img).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(index3img);
        }

        // GET: Index3img/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index3img index3img = db.Index3img.Find(id);
            if (index3img == null)
            {
                return HttpNotFound();
            }
            return View(index3img);
        }

        // POST: Index3img/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Index3img index3img = db.Index3img.Find(id);
            db.Index3img.Remove(index3img);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddImage(int ID) { 
        
            Index3img ind3 = db.Index3img.Find(ID);

            return View(ind3);
        }


        [HttpPost]
        public ActionResult AddImage(App_Class.Images image, int ID)
        {
            foreach (var folder in image.Folders)
            {
                if (folder.ContentLength > 1)
                {
                    //dosya adı ve adresi
                    var folderName = Guid.NewGuid() + Path.GetExtension(folder.FileName);
                    var adress = Path.Combine(Server.MapPath("~/VeterinerImages"), folderName);
                    folder.SaveAs(adress);

                    //veritabanına kaydet->

                    Index3img r = db.Index3img.FirstOrDefault(x => x.ID == ID);
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
