using SharePointCustomUpload.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharePointCustomUpload.Controllers
{
    public class HomeController : Controller
    {
        private void Upload()
        {
            try
            {
                string bearerToken = @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik4tbEMwbi05REFMcXdodUhZbkhRNjNHZUNYYyIsImtpZCI6Ik4tbEMwbi05REFMcXdodUhZbkhRNjNHZUNYYyJ9.eyJhdWQiOiJodHRwczovL3VvbWFwcGRldi5zaGFyZXBvaW50LmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2QwNjJiNjNlLTYyNDUtNDVhNS05Mzc0LTYwYWI4MzQ0ZWY3YS8iLCJpYXQiOjE1NTI1Nzc5NzgsIm5iZiI6MTU1MjU3Nzk3OCwiZXhwIjoxNTUyNjY0NjE4LCJhY3IiOiIxIiwiYWlvIjoiNDJKZ1lKaGh0YUpKMzJ6aFQwM2xMdFgzVVd6ZTFhRTZCYVpMQXE5K2x6QTQzZkoxaVNvQSIsImFtciI6WyJwd2QiXSwiYXBwX2Rpc3BsYXluYW1lIjoiRGV2L1Rlc3QgU2hhcmVwb2ludCBBY2Nlc3MiLCJhcHBpZCI6ImNiY2Y0YjA0LTA0ZjItNDI0MS1hNjdhLWYwZjNjN2ExZDkxYiIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiU2hhcmVwb2ludCIsImdpdmVuX25hbWUiOiJTZXJ2aWNlQWNjb3VudCIsImlwYWRkciI6IjEzMC44OC4yNDAuODIiLCJuYW1lIjoiU2VydmljZUFjY291bnRTaGFyZXBvaW50Iiwib2lkIjoiNzNiN2M3YjUtM2IyYy00YTQ5LTg5MTEtZDlhYjhhYzVkN2M4IiwicHVpZCI6IjEwMDMyMDAwM0QzMTBCREEiLCJzY3AiOiJBbGxTaXRlcy5NYW5hZ2UgQWxsU2l0ZXMuUmVhZCBBbGxTaXRlcy5Xcml0ZSBNeUZpbGVzLlJlYWQgTXlGaWxlcy5Xcml0ZSIsInNpZCI6IjRlNmU3OTI3LTM4Y2EtNDg3Yi04NWM4LWQ0ZjBlYjE5YjA0NyIsInN1YiI6IkRrck95blhOaUQzYUEwYmFab3BLeUItU0JKN1VVVUI1RXVwa1NGZk9feU0iLCJ0aWQiOiJkMDYyYjYzZS02MjQ1LTQ1YTUtOTM3NC02MGFiODM0NGVmN2EiLCJ1bmlxdWVfbmFtZSI6InNlcnZpY2VhY2NvdW50c2hhcmVwb2ludEB1b21hcHBkZXYub25taWNyb3NvZnQuY29tIiwidXBuIjoic2VydmljZWFjY291bnRzaGFyZXBvaW50QHVvbWFwcGRldi5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJzZUNjRnZnWmxFcWEyMzJZSEtzYkFBIiwidmVyIjoiMS4wIiwid2lkcyI6WyJmMjhhMWY1MC1mNmU3LTQ1NzEtODE4Yi02YTEyZjJhZjZiNmMiXX0.UBQCz09AwLQhJ2CXS3fTdeQDBnGcTTR_6_h6yk00fVNMndvK-0cy6rF0ghZcmTnk5tKWUbUbXYWpNHSVZfUhHZrP8CWbo7UEVywPAWVAD_UsMfiXLoMpNWmIivg4baFFIYCYKvNi9KKd36NgAbirCsiZV7AC1hGBfUs3rkE3rnHyDXk1bb7k5yHRmNQKTfqkGEWcEokyWarb8LYlb6K9EMgyayDIzsBITeN0i_aC47krYbiNPQrKb0v9XSfZAShtCME0vGajokW540Y2ci0DyApP_-HqETMvSr2lsCMOy-oKUN5ehIi03q7HjbdxHPcgQa-PWY-IF78sGzFeqY2w-A";
                string sharePointBaseUrl = "https://uomappdev.sharepoint.com/sites/IntegrationSpace";           
                string sharePointRootFolder = "/sites/IntegrationSpace/Shared%20Documents/DraftForms/12345678/uom_complaints";

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

                fileName = "~/App_Data/TestImage.png";
                doc = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath(fileName)));
                t = SharePointUploader.UploadDocumentAsync(sharePointBaseUrl, bearerToken, doc, sharePointRootFolder, System.IO.Path.GetFileName(fileName));
                t.Wait();

                fileName = "~/App_Data/TestExcel.xlsx";
                doc = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath(fileName)));
                t = SharePointUploader.UploadDocumentAsync(sharePointBaseUrl, bearerToken, doc, sharePointRootFolder, System.IO.Path.GetFileName(fileName));
                t.Wait();

                fileName = "~/App_Data/TestWord.docx";
                doc = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath(fileName)));
                t = SharePointUploader.UploadDocumentAsync(sharePointBaseUrl, bearerToken, doc, sharePointRootFolder, System.IO.Path.GetFileName(fileName));
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