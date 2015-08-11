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
    public class PackageController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: Package
        public ActionResult Index()
        {
            var tbl_Package = db.tbl_Package.Include(t => t.tbl_CancelPolicy).Include(t => t.tbl_Exclusion).Include(t => t.tbl_Subcategory).Include(t => t.tbl_TermsAndCondition);
            return View(tbl_Package.ToList());
        }

        // GET: Package/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Package tbl_Package = db.tbl_Package.Find(id);
            if (tbl_Package == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Package);
        }

        // GET: Package/Create
        public ActionResult Create()
        {
            ViewBag.pkg_CancelPolicyFK = new SelectList(db.tbl_CancelPolicy, "cnp_ID", "cnp_Name");
            ViewBag.pkg_ExclusionFK = new SelectList(db.tbl_Exclusion, "exc_ID", "exc_Name");
            ViewBag.pkg_SubcategoryFK = new SelectList(db.tbl_Subcategory, "sct_ID", "sct_Name");
            ViewBag.pkg_TermsAndConditionFK = new SelectList(db.tbl_TermsAndCondition, "tnc_ID", "tnc_Name");
            return View();
        }

        // POST: Package/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "pkg_ID,pkf_Name,pkg_Subtitle,pkg_Overview,pkg_Description,pkg_Inclusion,pkg_DefaultSeason,pkg_DefaultRoom,pkg_DefaultDay,pkg_DayHeading1,pkg_DayItinerary1,pkg_DayHeading2,pkg_DayItinerary2,pkg_DayHeading3,pkg_DayItinerary3,pkg_Roomtype1,pkg_Roomtype2,pkg_Roomtype3,pkg_Priority,pkg_Active,pkg_BannerImage,pkg_SquareImage,pkg_BackgroundImage1,pkg_BackgroundImage2,pkg_ExclusionFK,pkg_TermsAndConditionFK,pkg_CancelPolicyFK,pkg_SubcategoryFK")] tbl_Package tbl_Package)
        {
            if (ModelState.IsValid)
            {
                tbl_Package.pkg_DefaultSeason = true;
                tbl_Package.pkg_Roomtype1 = "Standard";
                tbl_Package.pkg_Roomtype2 = "Deluxe";
                tbl_Package.pkg_Roomtype3 = "Super Deluxe";
                tbl_Package.pkg_DefaultRoom = "1";
                tbl_Package.pkg_Priority = 0;
                tbl_Package.pkg_Active = true;
                db.tbl_Package.Add(tbl_Package);
                List<tbl_Price> prices = new List<tbl_Price>();
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "111", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "112", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "121", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "122", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "131", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "132", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "211", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "212", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "221", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "222", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "231", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "232", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "311", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "312", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "321", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "322", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "331", prc_Price = 0 });
                prices.Add(new tbl_Price { prc_PackageId = tbl_Package.pkg_ID, prc_Coordinate = "332", prc_Price = 0 });
                foreach (tbl_Price tp in prices)
                    db.tbl_Price.Add(tp);
                db.SaveChanges();
                tbl_Package.pkg_BannerImage = "Package_" + tbl_Package.pkg_ID + "_BI.jpg";
                tbl_Package.pkg_SquareImage = "Package_" + tbl_Package.pkg_ID + "_SI.jpg";
                tbl_Package.pkg_BackgroundImage1 = "Package_" + tbl_Package.pkg_ID + "_BI1.jpg";
                tbl_Package.pkg_BackgroundImage2 = "Package_" + tbl_Package.pkg_ID + "_BI2.jpg";
                db.Entry(tbl_Package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Next/" + tbl_Package.pkg_ID.ToString());
            }

            ViewBag.pkg_CancelPolicyFK = new SelectList(db.tbl_CancelPolicy, "cnp_ID", "cnp_Name", tbl_Package.pkg_CancelPolicyFK);
            ViewBag.pkg_ExclusionFK = new SelectList(db.tbl_Exclusion, "exc_ID", "exc_Name", tbl_Package.pkg_ExclusionFK);
            ViewBag.pkg_SubcategoryFK = new SelectList(db.tbl_Subcategory, "sct_ID", "sct_Name", tbl_Package.pkg_SubcategoryFK);
            ViewBag.pkg_TermsAndConditionFK = new SelectList(db.tbl_TermsAndCondition, "tnc_ID", "tnc_Name", tbl_Package.pkg_TermsAndConditionFK);
            return View(tbl_Package);
        }

        // GET: Package/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Package tbl_Package = db.tbl_Package.Find(id);
            if (tbl_Package == null)
            {
                return HttpNotFound();
            }

            List<tbl_Price> tbl_Price = db.tbl_Price.Where(w => w.prc_PackageId == id).ToList();
            List<string> Coords = new List<string>();
            List<string> Prices = new List<string>();
            foreach (tbl_Price p in tbl_Price)
            {
                Coords.Add(p.prc_Coordinate);
                Prices.Add(p.prc_Price.ToString());
            }
            ViewBag.Coord = Coords;
            ViewBag.Price = Prices;
            //string PricesJSON = "<script> var prices={";
            //foreach (tbl_Price p in tbl_Price)
            //    PricesJSON += '"' + p.prc_Coordinate + '"' + " : " + '"' + p.prc_Price + '"' + ',';
            //PricesJSON += "} </script>";
            //ViewBag.PriceList = PricesJSON;
            ViewBag.pkg_CancelPolicyFK = new SelectList(db.tbl_CancelPolicy, "cnp_ID", "cnp_Name", tbl_Package.pkg_CancelPolicyFK);
            ViewBag.pkg_ExclusionFK = new SelectList(db.tbl_Exclusion, "exc_ID", "exc_Name", tbl_Package.pkg_ExclusionFK);
            ViewBag.pkg_SubcategoryFK = new SelectList(db.tbl_Subcategory, "sct_ID", "sct_Name", tbl_Package.pkg_SubcategoryFK);
            ViewBag.pkg_TermsAndConditionFK = new SelectList(db.tbl_TermsAndCondition, "tnc_ID", "tnc_Name", tbl_Package.pkg_TermsAndConditionFK);
            return View(tbl_Package);
        }

        // POST: Package/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "pkg_ID,pkf_Name,pkg_Subtitle,pkg_Overview,pkg_Description,pkg_Inclusion,pkg_DefaultSeason,pkg_DefaultRoom,pkg_DefaultDay,pkg_DayHeading1,pkg_DayItinerary1,pkg_DayHeading2,pkg_DayItinerary2,pkg_DayHeading3,pkg_DayItinerary3,pkg_Roomtype1,pkg_Roomtype2,pkg_Roomtype3,pkg_Priority,pkg_Active,pkg_BannerImage,pkg_SquareImage,pkg_BackgroundImage1,pkg_BackgroundImage2,pkg_ExclusionFK,pkg_TermsAndConditionFK,pkg_CancelPolicyFK,pkg_SubcategoryFK")] tbl_Package tbl_Package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Package).State = EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = "Updated Successfully" };
            }
            ViewBag.pkg_CancelPolicyFK = new SelectList(db.tbl_CancelPolicy, "cnp_ID", "cnp_Name", tbl_Package.pkg_CancelPolicyFK);
            ViewBag.pkg_ExclusionFK = new SelectList(db.tbl_Exclusion, "exc_ID", "exc_Name", tbl_Package.pkg_ExclusionFK);
            ViewBag.pkg_SubcategoryFK = new SelectList(db.tbl_Subcategory, "sct_ID", "sct_Name", tbl_Package.pkg_SubcategoryFK);
            ViewBag.pkg_TermsAndConditionFK = new SelectList(db.tbl_TermsAndCondition, "tnc_ID", "tnc_Name", tbl_Package.pkg_TermsAndConditionFK);
            return new JsonResult { Data = "Error Occurred" };
        }

        // GET: Package/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Package tbl_Package = db.tbl_Package.Find(id);
            if (tbl_Package == null)
            {
                return HttpNotFound();
            }

            List<tbl_Place> tbl_Place = db.tbl_Place.SqlQuery("Select * from tbl_Place where plc_ID in(Select distinct plg_PlaceID from tbl_PlacePackage where plg_PackageID=" + id + ")").ToList();
            ViewBag.Places = tbl_Place;
            tbl_FeaturedPackage Feat = db.tbl_FeaturedPackage.Where(w => w.ftp_PlaceFK == id).FirstOrDefault();
            ViewBag.Featured = Feat == null ? "No" : "Yes";
            ViewBag.Deletable = tbl_Place.Count > 0 || Feat != null ? "No" : "Yes";

            return View(tbl_Package);
        }

        // POST: Package/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
           List<tbl_Price> Prices = db.tbl_Price.Where(w=>w.prc_PackageId==id).ToList();
           foreach (tbl_Price p in Prices)
           {
               db.tbl_Price.Remove(p);
           }
            
            tbl_Package tbl_Package = db.tbl_Package.Find(id);
            db.tbl_Package.Remove(tbl_Package);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Next(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Package tbl_Package = db.tbl_Package.Find(id);
            if (tbl_Package == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaceList = new SelectList(db.tbl_Place, "plc_ID", "plc_Name");
            ViewBag.packageId = id;
            return View(tbl_Package);
        }

        [HttpPost]
        public ActionResult UpdatePrices()
        {
            string Op = "Blank";
            try
            {
                int id = Convert.ToInt32(Request.Form.Get(0));
                for (int i = 1; i < Request.Form.Count; i++)
                {
                    string key = Request.Form.GetKey(i).ToString();
                    tbl_Price prices = db.tbl_Price.Where(qry => qry.prc_PackageId == id && qry.prc_Coordinate == key).FirstOrDefault();
                    prices.prc_Price = Convert.ToInt32(Request.Form.Get(i));
                    db.SaveChanges();
                }
                Op="Price Updated";
            }
            catch(Exception ex)
            {
                Op = ex.Message;
            }
            return new JsonResult { Data = Op };
        }

        [HttpPost]
        public ActionResult AddPlace()
        {
            int id = Convert.ToInt32(Request.Form.Get(0));
            int placeId = Convert.ToInt32(Request.Form.Get(1));
            tbl_Place place = db.tbl_Place.Where(qry => qry.plc_ID == placeId).FirstOrDefault();
            tbl_PlacePackage placePackage = new tbl_PlacePackage();
            placePackage.plg_PlaceID = placeId;
            placePackage.plg_PackageID = id;
            db.tbl_PlacePackage.Add(placePackage);
            db.SaveChanges();
            return new JsonResult { Data = place.plc_Name };
        }

        //public ActionResult RedirectToUploadImage()
        //{
        //    int id = Convert.ToInt32(Request.Form.Get(0));
        //    return RedirectToAction("UploadImages/" + id);
        //}

        //public ActionResult UploadImages(int id)
        //{
        //    ViewBag.packageId = id;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> files, string imgType, string PackageID)
        //{
        //    if (files != null)
        //    {
        //        foreach (var file in files)
        //        {
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                var path = Path.Combine(Server.MapPath("~/Pictures"), "Package_" + PackageID + "_" + imgType + ".jpg");
        //                file.SaveAs(path);
        //            }
        //        }
        //    }
        //    return new JsonResult { Data = "Successfully Uploaded " + imgType };
        //}

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
