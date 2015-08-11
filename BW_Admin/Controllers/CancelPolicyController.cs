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
    public class CancelPolicyController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: CancelPolicy
        public ActionResult Index()
        {
            return View(db.tbl_CancelPolicy.ToList());
        }

        // GET: CancelPolicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_CancelPolicy tbl_CancelPolicy = db.tbl_CancelPolicy.Find(id);
            if (tbl_CancelPolicy == null)
            {
                return HttpNotFound();
            }
            return View(tbl_CancelPolicy);
        }

        // GET: CancelPolicy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CancelPolicy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "cnp_ID,cnp_Name,cnp_Description")] tbl_CancelPolicy tbl_CancelPolicy)
        {
            if (ModelState.IsValid)
            {
                db.tbl_CancelPolicy.Add(tbl_CancelPolicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_CancelPolicy);
        }

        // GET: CancelPolicy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_CancelPolicy tbl_CancelPolicy = db.tbl_CancelPolicy.Find(id);
            if (tbl_CancelPolicy == null)
            {
                return HttpNotFound();
            }
            return View(tbl_CancelPolicy);
        }

        // POST: CancelPolicy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "cnp_ID,cnp_Name,cnp_Description")] tbl_CancelPolicy tbl_CancelPolicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_CancelPolicy).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = "Updated Successfully" };
            }
            return new JsonResult { Data = "Error Occurred" };
        }

        // GET: CancelPolicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_CancelPolicy tbl_CancelPolicy = db.tbl_CancelPolicy.Find(id);
            if (tbl_CancelPolicy == null)
            {
                return HttpNotFound();
            }
            List<tbl_Package> tbl_Package = db.tbl_Package.Where(w => w.pkg_CancelPolicyFK == tbl_CancelPolicy.cnp_ID).ToList();
            if (tbl_Package.Count > 0)
                ViewBag.Packs = tbl_Package;
            ViewBag.Deletable = tbl_Package.Count > 0 ? "No" : "Yes";

            return View(tbl_CancelPolicy);
        }

        // POST: CancelPolicy/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_CancelPolicy tbl_CancelPolicy = db.tbl_CancelPolicy.Find(id);
            db.tbl_CancelPolicy.Remove(tbl_CancelPolicy);
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
