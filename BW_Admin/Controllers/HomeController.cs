using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BW_Admin.Controllers
{
    public class HomeController : AController
    {
        // GET: Home
        public ActionResult Index()
        {
            var CountList = new List<int>();
            CountList.Add(DataContext.tbl_Category.Count());
            CountList.Add(DataContext.tbl_Subcategory.Count());
            CountList.Add(DataContext.tbl_Place.Count());
            CountList.Add(DataContext.tbl_Tour.Count());
            CountList.Add(DataContext.tbl_FeaturedPackage.Count());
            CountList.Add(DataContext.tbl_Package.Count());
            CountList.Add(DataContext.tbl_Gallery.Count());
            CountList.Add(DataContext.tbl_Exclusion.Count());
            CountList.Add(DataContext.tbl_CancelPolicy.Count());
            CountList.Add(DataContext.tbl_TermsAndCondition.Count());
            ViewBag.ListCount = CountList;

            return View();
        }
    }
}