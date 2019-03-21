using SharePointCustomUpload.Helper;
using System;
using System.IO;
using System.Web.Mvc;

namespace SharePointCustomUpload.Controllers
{
    public class HomeController : Controller
    {
        private void Upload()
        {
            try
            {
                string bearerToken = @"REDACTED";
                string sharePointBaseUrl = "https://<some-base-url>/sites/IntegrationSpace";           
                string sharePointRootFolder = "/sites/<some folder>";

                string fileName = "~/App_Data/Julius.txt";
                using (FileStream source = System.IO.File.Open(Server.MapPath(fileName), FileMode.Open))
                {

                    //var doc = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath(fileName)));
                    var t1 = SharePointUploader.UploadDocumentAsync(sharePointBaseUrl, bearerToken, source, sharePointRootFolder, System.IO.Path.GetFileName(fileName));
                    t1.Wait();
                }

                fileName = "~/App_Data/Test.pdf";
                var doc = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath(fileName)));
                var t = SharePointUploader.UploadDocumentAsync(sharePointBaseUrl, bearerToken, doc, sharePointRootFolder, System.IO.Path.GetFileName(fileName));
                t.Wait();
            }
            catch (Exception ex)
            {

            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            Upload();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }


}