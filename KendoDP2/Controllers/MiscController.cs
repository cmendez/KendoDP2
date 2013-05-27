﻿using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Controllers
{
    public class MiscController : Controller
    {
        //
        // GET: /Misc/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetImagen(int archivoID)
        {
            using (DP2Context context = new DP2Context())
            {
                var archivo = context.TablaArchivos.FindByID(archivoID);
                    if (archivo.Data != null)
                        return File(archivo.Data, archivo.Mime);
                var file = Server.MapPath("~/Images/unknown-person.jpg");
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        return File(memoryStream.ToArray(), "image/jpg");
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult UploadPDF(IEnumerable<HttpPostedFileBase> Archivo2)
        {
            return UploadSingleFile(Archivo2);
        }
        [HttpPost]
        public ActionResult UploadSingleFile(IEnumerable<HttpPostedFileBase> Archivo)
        {
            // The Name of the Upload component is "files"
            if (Archivo != null)
            {
                foreach (var file in Archivo)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        file.InputStream.CopyTo(memoryStream);
                        using (DP2Context context = new DP2Context())
                        {
                            Archivo a = new Archivo { Data = memoryStream.ToArray(), Nombre = file.FileName, Mime = file.ContentType };
                            int ID = context.TablaArchivos.AddElement(a);
                            return Json(new { ID = ID });
                        }
                    }
                }
            }

            // Return an empty string to signify success
            return Json(new { ID = 0 });
        }
    }
}