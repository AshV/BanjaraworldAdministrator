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
    public class SubcategoryController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: Subcategory
        public ActionResult Index()
        {
            var tbl_Subcategory = db.tbl_Subcategory.Include(t => t.tbl_Category);
            return View(tbl_Subcategory.ToList());
        }

        // GET: Subcategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Subcategory tbl_Subcategory = db.tbl_Subcategory.Find(id);
            if (tbl_Subcategory == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Subcategory);
        }

        // GET: Subcategory/Create
        public ActionResult Create()
        {
            ViewBag.sct_CategoryFK = new SelectList(db.tbl_Category, "cat_ID", "cat_Name");
            return View();
        }

        // POST: Subcategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "sct_ID,sct_Name,sct_CategoryFK,sct_Descripton,sct_Priority,sct_Active,sct_BannerImage,sct_SquareImage,sct_BackgroundImage1,sct_BackgroundImage2")] tbl_Subcategory tbl_Subcategory)
        {
            if (ModelState.IsValid)
            {
                tbl_Subcategory.sct_Active = true;
                tbl_Subcategory.sct_Priority = 0;
                db.tbl_Subcategory.Add(tbl_Subcategory);
                db.SaveChanges(); 
                tbl_Subcategory.sct_BannerImage = "Subcategory_" + tbl_Subcategory.sct_ID + "_BI.jpg";
                tbl_Subcategory.sct_SquareImage = "Subcategory_" + tbl_Subcategory.sct_ID + "_SI.jpg";
                tbl_Subcategory.sct_BackgroundImage1 = "Subcategory_" + tbl_Subcategory.sct_ID + "_BI1.jpg";
                tbl_Subcategory.sct_BackgroundImage2 = "Subcategory_" + tbl_Subcategory.sct_ID + "_BI2.jpg";
                db.Entry(tbl_Subcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UploadImages/" + tbl_Subcategory.sct_ID.ToString());
            }

            ViewBag.sct_CategoryFK = new SelectList(db.tbl_Category, "cat_ID", "cat_Name", tbl_Subcategory.sct_CategoryFK);
            return View(tbl_Subcategory);
        }

        // GET: Subcategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Subcategory tbl_Subcategory = db.tbl_Subcategory.Find(id);
            if (tbl_Subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.sct_CategoryFK = new SelectList(db.tbl_Category, "cat_ID", "cat_Name", tbl_Subcategory.sct_CategoryFK);
            return View(tbl_Subcategory);
        }

        // POST: Subcategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "sct_ID,sct_Name,sct_CategoryFK,sct_Descripton,sct_Priority,sct_Active,sct_BannerImage,sct_SquareImage,sct_BackgroundImage1,sct_BackgroundImage2")] tbl_Subcategory tbl_Subcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Subcategory).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = "Updated Successfully" };
            }
            ViewBag.sct_CategoryFK = new SelectList(db.tbl_Category, "cat_ID", "cat_Name", tbl_Subcategory.sct_CategoryFK);
            return new JsonResult { Data = "Error Occurred" };
        }

        // GET: Subcategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Subcategory tbl_Subcategory = db.tbl_Subcategory.Find(id);
            if (tbl_Subcategory == null)
            {
                return HttpNotFound();
            }
            List<tbl_Package> tbl_Package = db.tbl_Package.Where(w => w.pkg_SubcategoryFK == tbl_Subcategory.sct_ID).ToList();
            if (tbl_Package.Count > 0)
                ViewBag.Packs = tbl_Package;
            ViewBag.Deletable = tbl_Package.Count > 0 ? "No" : "Yes";

            return View(tbl_Subcategory);
        }

        // POST: Subcategory/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Subcategory tbl_Subcategory = db.tbl_Subcategory.Find(id);
            db.tbl_Subcategory.Remove(tbl_Subcategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadImages(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Subcategory tbl_Subcategory = db.tbl_Subcategory.Find(id);
            if (tbl_Subcategory == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Subcategory);
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
