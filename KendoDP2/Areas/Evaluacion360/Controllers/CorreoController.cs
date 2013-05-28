using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class CorreoController : Controller
    {
        //
        // GET: /Evaluacion360/Correo/

        public ActionResult Index()
        {
            //Usuario: pruebas.rhpp@gmail.com
            //Contraseña: desarrollo

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Credentials = new System.Net.NetworkCredential("pruebas.rhpp@gmail.com", "desarrollo");
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;

            mail.From = new MailAddress("pruebas.rhpp@gmail.com");
            mail.To.Add("pruebas.rhpp@gmail.com");

            mail.Subject = "[RH++] Su evaluación ya inició";
            mail.Body = "El sistema RH++ le indica que el proceso de evaluación en el cual usted es partícipe ya inicio. <br/>" +
                " Puede rendir la evaluación haciendo click en el siguiente enlace: " + "http://dp2kendo.apphb.com/" + ".<br/>" +
                "Proceso: Evaluación trimestral Enero-Marzo 2013 - Fecha de inicio: 01/01/2013 - Fecha de fin: 01/04/2013" +
                "Sírvase no responder este correo.";

            SmtpServer.Send(mail);

            return View();
        }

    }
}
