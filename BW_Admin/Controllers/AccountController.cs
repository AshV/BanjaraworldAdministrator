using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW_Admin;
using System.Web.Security;

namespace BW_Admin.Controllers
{
    public class AccountController : AController
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        // GET: Account
        public ActionResult Index()
        {
            return View(db.tbl_Account.ToList());
        }

        // GET: Account/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Account tbl_Account = db.tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Account);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "acc_UserID,acc_Password,acc_Active")] tbl_Account tbl_Account)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Account.Add(tbl_Account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Account);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Account tbl_Account = db.tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "acc_UserID,acc_Password,acc_Active")] tbl_Account tbl_Account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Account);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Account tbl_Account = db.tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_Account tbl_Account = db.tbl_Account.Find(id);
            db.tbl_Account.Remove(tbl_Account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword()
        {
            return View();
         //   return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePassword()
        {
            string Op = "Blank";
            try
            {
                string CPass = Request.Form["CPassword"];
                string NPass = Request.Form["NPassword"];
                string CPassDB = db.tbl_Account.Where(w => w.acc_UserID == "Administrator").FirstOrDefault().acc_Password;
                if (CPass == CPassDB)
                {
                    tbl_Account tbl_Acc = db.tbl_Account.Find("Administrator");
                    tbl_Acc.acc_Password = NPass;
                    db.Entry(tbl_Acc).State = EntityState.Modified;
                    db.SaveChanges();
                    Op = "Password Updated Successfully";
                }
                else
                {
                    Op = "Your Current Password is Wrong, Try Again.";
                }
            }
            catch (Exception ex)
            {
                Op = ex.Message;
            }
            return new JsonResult { Data = Op };
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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
