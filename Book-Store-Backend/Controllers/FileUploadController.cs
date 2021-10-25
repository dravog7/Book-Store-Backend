using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Book_Store_Backend.Controllers
{
    [RoutePrefix("api/Upload")]
    public class FileUploadController : ApiController
    {
        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var newFileName = NewFileName(postedFile.FileName);
                    var filePath = HttpContext.Current.Server.MapPath("~/Content/Images/" + newFileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add("/Content/Images/"+newFileName);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
        string NewFileName(string FileName)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(FileName);
            return RandomString(10) + file.Extension;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
