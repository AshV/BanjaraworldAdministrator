using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BW_Admin.Controllers
{
    public class AController : Controller
    {
        private Chandan_Banjara_WorldEntities db = new Chandan_Banjara_WorldEntities();

        public Chandan_Banjara_WorldEntities DataContext
        {
            get { return db; }
        }

        public AController()
        {
          
        }

        [HttpPost]
        public ActionResult UploadImage(IEnumerable<HttpPostedFileBase> files, string fileName)
        {
            string Op = "Blank";
            try
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var path = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                            file.SaveAs(path);
                        }
                    }
                }
                Op = "Successfully Uploaded";
            }
            catch(Exception ex)
            {
                Op = ex.Message;
            }
            return new JsonResult { Data = Op };
        }

    }
}