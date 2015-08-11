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
    public class FeaturedPackageController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: FeaturedPackage
        public ActionResult Index()
        {
            var tbl_FeaturedPackage = db.tbl_FeaturedPackage.Include(t => t.tbl_Package);
            return View(tbl_FeaturedPackage.ToList());
        }

        // GET: FeaturedPackage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_FeaturedPackage tbl_FeaturedPackage = db.tbl_FeaturedPackage.Find(id);
            if (tbl_FeaturedPackage == null)
            {
                return HttpNotFound();
            }
            return View(tbl_FeaturedPackage);
        }

        // GET: FeaturedPackage/Create
        public ActionResult Create()
        {
            ViewBag.ftp_PlaceFK = new SelectList(db.tbl_Package, "pkg_ID", "pkf_Name");
            return View();
        }

        // POST: FeaturedPackage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ftp_ID,ftp_PlaceFK,ftp_Priority")] tbl_FeaturedPackage tbl_FeaturedPackage)
        {
            if (ModelState.IsValid)
            {
                db.tbl_FeaturedPackage.Add(tbl_FeaturedPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ftp_PlaceFK = new SelectList(db.tbl_Package, "pkg_ID", "pkf_Name", tbl_FeaturedPackage.ftp_PlaceFK);
            return View(tbl_FeaturedPackage);
        }

        // GET: FeaturedPackage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_FeaturedPackage tbl_FeaturedPackage = db.tbl_FeaturedPackage.Find(id);
            if (tbl_FeaturedPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ftp_PlaceFK = new SelectList(db.tbl_Package, "pkg_ID", "pkf_Name", tbl_FeaturedPackage.ftp_PlaceFK);
            return View(tbl_FeaturedPackage);
        }

        // POST: FeaturedPackage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ftp_ID,ftp_PlaceFK,ftp_Priority")] tbl_FeaturedPackage tbl_FeaturedPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_FeaturedPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ftp_PlaceFK = new SelectList(db.tbl_Package, "pkg_ID", "pkf_Name", tbl_FeaturedPackage.ftp_PlaceFK);
            return View(tbl_FeaturedPackage);
        }

        // GET: FeaturedPackage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_FeaturedPackage tbl_FeaturedPackage = db.tbl_FeaturedPackage.Find(id);
            if (tbl_FeaturedPackage == null)
            {
                return HttpNotFound();
            }
            return View(tbl_FeaturedPackage);
        }

        // POST: FeaturedPackage/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_FeaturedPackage tbl_FeaturedPackage = db.tbl_FeaturedPackage.Find(id);
            db.tbl_FeaturedPackage.Remove(tbl_FeaturedPackage);
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
