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
    public class PlaceController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: Place
        public ActionResult Index()
        {
            return View(db.tbl_Place.ToList());
        }

        // GET: Place/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Place tbl_Place = db.tbl_Place.Find(id);
            if (tbl_Place == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Place);
        }

        // GET: Place/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Place/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "plc_ID,plc_Description,plc_TimeToVisit,plc_Location,plc_Priority,plc_Active,plc_BannerImage,plc_SquareImage,plc_BackgroundImage1,plc_BackgroundImage2,plc_Name")] tbl_Place tbl_Place)
        {
            if (ModelState.IsValid)
            {
                tbl_Place.plc_Active = true;
                tbl_Place.plc_Priority = 0;
                db.tbl_Place.Add(tbl_Place);
                db.SaveChanges();
                tbl_Place.plc_BannerImage ="Place_"+tbl_Place.plc_ID+ "_BI.jpg";
                tbl_Place.plc_SquareImage = "Place_" + tbl_Place.plc_ID + "_SI.jpg";
                tbl_Place.plc_BackgroundImage1 = "Place_" + tbl_Place.plc_ID + "_BI1.jpg";
                tbl_Place.plc_BackgroundImage2 = "Place_" + tbl_Place.plc_ID + "_BI2.jpg";
                db.Entry(tbl_Place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UploadImages/" + tbl_Place.plc_ID);
            }

            return View(tbl_Place);
        }

        // GET: Place/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Place tbl_Place = db.tbl_Place.Find(id);
            if (tbl_Place == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Place);
        }

        // POST: Place/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "plc_ID,plc_Description,plc_TimeToVisit,plc_Location,plc_Priority,plc_Active,plc_BannerImage,plc_SquareImage,plc_BackgroundImage1,plc_BackgroundImage2,plc_Name")] tbl_Place tbl_Place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Place).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = "Updated Successfully" };
            }
            return new JsonResult { Data = "Error Occurred" };
        }

        // GET: Place/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Place tbl_Place = db.tbl_Place.Find(id);
            if (tbl_Place == null)
            {
                return HttpNotFound();
            }
            List<tbl_Package> tbl_Package = db.tbl_Package.SqlQuery("Select * from tbl_Package where pkg_ID in(Select distinct plg_PackageID from tbl_PlacePackage where plg_PlaceID=" + id + ")").ToList();
            if (tbl_Package.Count > 0)
                ViewBag.Packs = tbl_Package;
            ViewBag.Deletable = tbl_Package.Count > 0 ? "No" : "Yes";

            return View(tbl_Place);
        }

        // POST: Place/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Place tbl_Place = db.tbl_Place.Find(id);
            db.tbl_Place.Remove(tbl_Place);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadImages(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Place tbl_Place = db.tbl_Place.Find(id);
            if (tbl_Place == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Place);
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
