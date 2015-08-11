using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW_Admin;

namespace BW_Admin.Controllers
{
    public class GalleryController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: Gallery
        public ActionResult Index()
        {
            var tbl_Gallery = db.tbl_Gallery.Include(t => t.tbl_Package).Include(t => t.tbl_Place);
            return View(tbl_Gallery.ToList());
        }

        // GET: Gallery/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Gallery tbl_Gallery = db.tbl_Gallery.Find(id);
            if (tbl_Gallery == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Gallery);
        }

        // GET: Gallery/Create
        public ActionResult Create()
        {
            ViewBag.glr_PackageID = new SelectList(db.tbl_Package, "pkg_ID", "pkf_Name");
            ViewBag.glr_PlaceID = new SelectList(db.tbl_Place, "plc_ID", "plc_Description");
            return View();
        }

        // POST: Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "glr_ID,glr_Image,glr_Description,glr_Type,glr_PlaceID,glr_PackageID")] tbl_Gallery tbl_Gallery)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Gallery.Add(tbl_Gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.glr_PackageID = new SelectList(db.tbl_Package, "pkg_ID", "pkf_Name", tbl_Gallery.glr_PackageID);
            ViewBag.glr_PlaceID = new SelectList(db.tbl_Place, "plc_ID", "plc_Description", tbl_Gallery.glr_PlaceID);
            return View(tbl_Gallery);
        }

        // GET: Gallery/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Gallery tbl_Gallery = db.tbl_Gallery.Find(id);
            if (tbl_Gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.glr_PackageID = new SelectList(db.tbl_Package, "pkg_ID", "pkf_Name", tbl_Gallery.glr_PackageID);
            ViewBag.glr_PlaceID = new SelectList(db.tbl_Place, "plc_ID", "plc_Description", tbl_Gallery.glr_PlaceID);
            return View(tbl_Gallery);
        }

        // POST: Gallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "glr_ID,glr_Image,glr_Description,glr_Type,glr_PlaceID,glr_PackageID")] tbl_Gallery tbl_Gallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.glr_PackageID = new SelectList(db.tbl_Package, "pkg_ID", "pkf_Name", tbl_Gallery.glr_PackageID);
            ViewBag.glr_PlaceID = new SelectList(db.tbl_Place, "plc_ID", "plc_Description", tbl_Gallery.glr_PlaceID);
            return View(tbl_Gallery);
        }

        // GET: Gallery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Gallery tbl_Gallery = db.tbl_Gallery.Find(id);
            if (tbl_Gallery == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Gallery);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Gallery tbl_Gallery = db.tbl_Gallery.Find(id);
            db.tbl_Gallery.Remove(tbl_Gallery);
            db.SaveChanges();
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
