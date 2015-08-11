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
    public class TermsAndConditionController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: TermsAndCondition
        public ActionResult Index()
        {
            return View(db.tbl_TermsAndCondition.ToList());
        }

        // GET: TermsAndCondition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_TermsAndCondition tbl_TermsAndCondition = db.tbl_TermsAndCondition.Find(id);
            if (tbl_TermsAndCondition == null)
            {
                return HttpNotFound();
            }
            return View(tbl_TermsAndCondition);
        }

        // GET: TermsAndCondition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TermsAndCondition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "tnc_ID,tnc_Name,tnc_Description")] tbl_TermsAndCondition tbl_TermsAndCondition)
        {
            if (ModelState.IsValid)
            {
                db.tbl_TermsAndCondition.Add(tbl_TermsAndCondition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_TermsAndCondition);
        }

        // GET: TermsAndCondition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_TermsAndCondition tbl_TermsAndCondition = db.tbl_TermsAndCondition.Find(id);
            if (tbl_TermsAndCondition == null)
            {
                return HttpNotFound();
            }
            return View(tbl_TermsAndCondition);
        }

        // POST: TermsAndCondition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "tnc_ID,tnc_Name,tnc_Description")] tbl_TermsAndCondition tbl_TermsAndCondition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_TermsAndCondition).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = "Updated Successfully" };
            }
            return new JsonResult { Data = "Error Occurred" };
        }

        // GET: TermsAndCondition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_TermsAndCondition tbl_TermsAndCondition = db.tbl_TermsAndCondition.Find(id);
            if (tbl_TermsAndCondition == null)
            {
                return HttpNotFound();
            }
            List<tbl_Package> tbl_Package = db.tbl_Package.Where(w => w.pkg_TermsAndConditionFK == tbl_TermsAndCondition.tnc_ID).ToList();
            if (tbl_Package.Count > 0)
                ViewBag.Packs = tbl_Package;
            ViewBag.Deletable = tbl_Package.Count > 0 ? "No" : "Yes";

            return View(tbl_TermsAndCondition);
        }

        // POST: TermsAndCondition/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_TermsAndCondition tbl_TermsAndCondition = db.tbl_TermsAndCondition.Find(id);
            db.tbl_TermsAndCondition.Remove(tbl_TermsAndCondition);
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
