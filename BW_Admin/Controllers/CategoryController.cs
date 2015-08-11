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
    public class CategoryController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.tbl_Category.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "cat_ID,cat_Name,cat_Descripton,cat_Priority,cat_Active,cat_BannerImage,cat_SquareImage,cat_BackgroundImage1,cat_BackgroundImage2")] tbl_Category tbl_Category)
        {
            if (ModelState.IsValid)
            {
                tbl_Category.cat_Active = true;
                tbl_Category.cat_Priority = 0;
                db.tbl_Category.Add(tbl_Category);
                db.SaveChanges();
                tbl_Category.cat_BannerImage = "Category_" + tbl_Category.cat_ID + "_BI.jpg";
                tbl_Category.cat_SquareImage = "Category_" + tbl_Category.cat_ID + "_SI.jpg";
                tbl_Category.cat_BackgroundImage1 = "Category_" + tbl_Category.cat_ID + "_BI1.jpg";
                tbl_Category.cat_BackgroundImage2 = "Category_" + tbl_Category.cat_ID + "_BI2.jpg";
                db.Entry(tbl_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UploadImages/" + tbl_Category.cat_ID);
            }

            return View(tbl_Category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "cat_ID,cat_Name,cat_Descripton,cat_Priority,cat_Active,cat_BannerImage,cat_SquareImage,cat_BackgroundImage1,cat_BackgroundImage2")] tbl_Category tbl_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Category).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = "Updated Successfully" };
                    //RedirectToAction("Details", new { id = tbl_Category.cat_ID });
            }
            return new JsonResult { Data = "Error Occurred" };
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            List<tbl_Subcategory> tbl_Subcategory = db.tbl_Subcategory.Where(w => w.sct_CategoryFK == tbl_Category.cat_ID).ToList();
            if (tbl_Subcategory.Count > 0)
                ViewBag.SubCats = tbl_Subcategory;
            ViewBag.Deletable = tbl_Subcategory.Count > 0 ? "No" : "Yes";
            return View(tbl_Category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            db.tbl_Category.Remove(tbl_Category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadImages(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
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
