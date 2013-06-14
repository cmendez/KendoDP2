using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
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
        public void EnviarEmailsInicio(List<Colaborador> listaJefes, ProcesoEvaluacion proceso) 
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Credentials = new System.Net.NetworkCredential("pruebas.rhpp@gmail.com", "desarrollo");
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            String to = "";
            String from = "pruebas.rhpp@gmail.com";

            mail.From = new MailAddress(from);
            mail.Subject = "[RH++] Proceso Evaluación:" + proceso.Nombre.ToUpper() + " - Elegir evaluadores ";
            String link = KendoDP2.MvcApplication.IsDebug ? "http://localhost:2642/Evaluacion360/ProcesoEvaluacion" : "http://dp2kendo.apphb.com/Evaluacion360/ProcesoEvaluacion";
            String messageText = ", el sistema RH++ le indica que el proceso de evaluación '" +  proceso.Nombre.ToUpper() +"' en el cual miembros de su equipo son partícipes ya inició y " +
                                 "se requiere la elección de evaluadores. <br> Para seleccionar la lista d evaluadores acceder a <a href='"+link+"'> aqui.</a><br>" + 
                                 "Sírvase no responder este correo.";

            foreach (Colaborador c in listaJefes)
            {
                if (c == null) continue;
                if (c.CorreoElectronico != null)
                    to = c.CorreoElectronico;
                else
                    to = "pruebas.rhpp+RHSE_JEFES@gmail.com";
                mail.To.Add(to);
                mail.Body = "<strong> "+ c.ToDTO().NombreCompleto + "</strong> " + messageText;
                try
                {
                    SmtpServer.Send(mail);
                }
                catch (Exception e) { 
                }
            }
        }

        public void SendEmailRH(String from, String to, String subject, String message){
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Credentials = new System.Net.NetworkCredential("pruebas.rhpp@gmail.com", "desarrollo");
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            if (String.IsNullOrEmpty(to))
                to = "pruebas.rhpp+RHColaboradorSINEMAIL@gmail.com";
            mail.To.Add(to);
            mail.Body = message;
            SmtpServer.Send(mail);
        } 


        public ActionResult EnviarEmails()
        {
            
            //Usuario: pruebas.rhpp@gmail.com
            //Contraseña: desarrollo

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Credentials = new System.Net.NetworkCredential("pruebas.rhpp@gmail.com", "desarrollo");
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            String to = "";
            String from = "pruebas.rhpp@gmail.com";
            mail.From = new MailAddress(from);
            mail.To.Add(to);

            mail.Subject = "[RH++] Su evaluación ya inició";
            String link = KendoDP2.MvcApplication.IsDebug ? "http://localhost:2642/Evaluacion360/ProcesoEvaluacion" : "http://dp2kendo.apphb.com/Evaluacion360/ProcesoEvaluacion";
            mail.Body = "El sistema RH++ le indica que el proceso de evaluación en el cual usted es partícipe ya inicio. <br/>" +
                " Puede rendir la evaluación haciendo click en el siguiente enlace: " +"<a href='"+link+"'>aquí</a>" + ".<br/>" +
                "Proceso: Evaluación trimestral Enero-Marzo 2013 - Fecha de inicio: 01/01/2013 - Fecha de fin: 01/04/2013" +
                "Sírvase no responder este correo.";

            SmtpServer.Send(mail);

            return View();
        }

        public String getMensajeParaEvaluador(String nombreCompleto)
        {
            String mensaje = "";
            String link = KendoDP2.MvcApplication.IsDebug ? "http://localhost:2642/Evaluacion360/ProcesoEvaluacion" : "http://dp2kendo.apphb.com/";
            mensaje += nombreCompleto + ", el sistema RH++ le indica que el proceso de evaluación en el cual usted es partícipe ya inicio. <br/>" +
                " Puede rendir la evaluación haciendo click en el siguiente enlace: <a href='" +link + "'> aquí.</a><br/>" +
                "Proceso: Evaluación trimestral Enero-Marzo 2013 - Fecha de inicio: 01/01/2013 - Fecha de fin: 01/04/2013" +
                "Sírvase no responder este correo.";
            return mensaje;
        }

    }
}
