using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW_Admin;
using System.IO;

namespace BW_Admin.Controllers
{
    public class TourController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: Tour
        public ActionResult Index()
        {
            return View(db.tbl_Tour.ToList());
        }

        // GET: Tour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tour tbl_Tour = db.tbl_Tour.Find(id);
            if (tbl_Tour == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Tour);
        }

        // GET: Tour/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tour/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "tor_ID,tor_Overview,tor_Description,tor_Priority,tor_Active,tor_BannerImage,tor_SquareImage,tor_BackgroundImage1,tor_BackgroundImage2,tor_Name")] tbl_Tour tbl_Tour)
        {
            if (ModelState.IsValid)
            {
                tbl_Tour.tor_Active = true;
                tbl_Tour.tor_Priority = 0;
                db.tbl_Tour.Add(tbl_Tour);
                db.SaveChanges();
                tbl_Tour.tor_BannerImage = "Tour_" + tbl_Tour.tor_ID + "_BI.jpg";
                tbl_Tour.tor_SquareImage = "Tour_" + tbl_Tour.tor_ID + "_SI.jpg";
                tbl_Tour.tor_BackgroundImage1 = "Tour_" + tbl_Tour.tor_ID + "_BI1.jpg";
                tbl_Tour.tor_BackgroundImage2 = "Tour_" + tbl_Tour.tor_ID + "_BI2.jpg";
                db.Entry(tbl_Tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UploadImages/" + tbl_Tour.tor_ID.ToString());
            }
            return View(tbl_Tour);
        }

        // GET: Tour/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tour tbl_Tour = db.tbl_Tour.Find(id);
            if (tbl_Tour == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Tour);
        }

        // POST: Tour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "tor_ID,tor_Overview,tor_Description,tor_Priority,tor_Active,tor_BannerImage,tor_SquareImage,tor_BackgroundImage1,tor_BackgroundImage2,tor_Name")] tbl_Tour tbl_Tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Tour).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = "Updated Successfully" };
            } 
            return new JsonResult { Data = "Error Occurred" };
        }

        // GET: Tour/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tour tbl_Tour = db.tbl_Tour.Find(id);
            if (tbl_Tour == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Tour);
        }

        // POST: Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Tour tbl_Tour = db.tbl_Tour.Find(id);
            db.tbl_Tour.Remove(tbl_Tour);
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
