using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            return View();
        }


        public IEnumerable<string> Get()
        {
            string dirPath = Path.Combine(Server.MapPath("~/media/uploads/gallery"));
            List<string> files = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            foreach (FileInfo fInfo in dirInfo.GetFiles())
            {
                files.Add(fInfo.Name);
            }
            return files.ToArray();
        }


        public JsonResult filesinfolder( string gallery)
        {
            string salesFTPPath = Path.Combine(Server.MapPath("~/media/uploads/gallery"+ gallery));
            DirectoryInfo salesFTPDirectory = new DirectoryInfo(salesFTPPath);
            IEnumerable<string> files = salesFTPDirectory.GetFiles()
//              .Where(f => f.Extension == ".xls" || f.Extension == ".xml" || f.Extension == ".jps" || f.Extension == ".jpg" || f.Extension == ".jpeg" || f.Extension == ".png")
              .OrderBy(f => f.Name)
              .Select(f => f.Name);


            return Json(files, JsonRequestBehavior.AllowGet);
        }


    }
}
