using Postal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SendEmailWithAttachment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email) {
            if (!string.IsNullOrEmpty(email)) {
                if (Request.Files.Count > 0) {

                    dynamic emailToSend = new Email("EmailWithAttachement");

                    byte[] data = new byte[0];
                    data = new byte[Request.Files[0].ContentLength];

                    var fileName = Request.Files[0].FileName;
                    var extension = Path.GetExtension(Request.Files[0].FileName);
                    Request.Files[0].InputStream.Read(data, 0, Request.Files[0].ContentLength);

                    using (var stream = new MemoryStream(data, true))
                    {
                        emailToSend.Attach(new Attachment(stream, fileName + extension));
                        emailToSend.To = email;
                        emailToSend.Send();
                    }   

                   
                }
            }

            return View(); 
        }
    }
}