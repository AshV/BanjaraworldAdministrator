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
    public class ExclusionController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: Exclusion
        public ActionResult Index()
        {
            return View(db.tbl_Exclusion.ToList());
        }

        // GET: Exclusion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Exclusion tbl_Exclusion = db.tbl_Exclusion.Find(id);
            if (tbl_Exclusion == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Exclusion);
        }

        // GET: Exclusion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exclusion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "exc_ID,exc_Name,exc_Description")] tbl_Exclusion tbl_Exclusion)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Exclusion.Add(tbl_Exclusion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Exclusion);
        }

        // GET: Exclusion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Exclusion tbl_Exclusion = db.tbl_Exclusion.Find(id);
            if (tbl_Exclusion == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Exclusion);
        }

        // POST: Exclusion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "exc_ID,exc_Name,exc_Description")] tbl_Exclusion tbl_Exclusion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Exclusion).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = "Updated Successfully" };
            }
            return new JsonResult { Data = "Error Occurred" };
        }

        // GET: Exclusion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Exclusion tbl_Exclusion = db.tbl_Exclusion.Find(id);
            if (tbl_Exclusion == null)
            {
                return HttpNotFound();
            }
            List<tbl_Package> tbl_Package = db.tbl_Package.Where(w => w.pkg_ExclusionFK == tbl_Exclusion.exc_ID).ToList();
            if (tbl_Package.Count > 0)
                ViewBag.Packs = tbl_Package;
            ViewBag.Deletable = tbl_Package.Count > 0 ? "No" : "Yes";

            return View(tbl_Exclusion);
        }

        // POST: Exclusion/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Exclusion tbl_Exclusion = db.tbl_Exclusion.Find(id);
            db.tbl_Exclusion.Remove(tbl_Exclusion);
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
