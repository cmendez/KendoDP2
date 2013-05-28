using KendoDP2.Areas.Evaluacion360.Models;
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

        public void EnviarEmailsInicio(List<ColaboradorXProcesoEvaluacion> colaboradores, ProcesoEvaluacion proceso) 
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Credentials = new System.Net.NetworkCredential("pruebas.rhpp@gmail.com", "desarrollo");
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            String to = "";
            String from = "pruebas.rhpp@gmail.com";

            mail.From = new MailAddress(from);
            mail.Subject = "[RH++] Su evaluación ya inició";
            String messageText = ", el sistema RH++ le indica que el proceso de evaluación '" +  proceso.Nombre.ToUpper() +"' en el cual usted es partícipe ya inició. " +
                " Puede rendir la evaluación haciendo click en el siguiente enlace: " + "http://dp2kendo.apphb.com/Evaluacion360/Evaluaciones" + "." +
                "Sírvase no responder este correo.";

            foreach (ColaboradorXProcesoEvaluacion c in colaboradores){
                if (c.ToDTO().ColaboradorDTO.CorreoElectronico != null)
                    to = c.ToDTO().ColaboradorDTO.CorreoElectronico;
                else
                    to = "ktucto+RHSE@gmail.com";
                mail.To.Add(to);
                mail.Body = c.ToDTO().ColaboradorDTO.NombreCompleto + messageText;
                SmtpServer.Send(mail);

                // Actualizar estado colaboradores
                /*using (DP2Context context = new DP2Context())
                {
                    EstadoColaboradorXProcesoEvaluacion iniciado = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Iniciado));
                    c.EstadoColaboradorXProcesoEvaluacion = iniciado;

                    //ColaboradorXProcesoEvaluacion col = context.TablaColaboradorXProcesoEvaluaciones.FindByID(c.ID, true);
                    //col.EstadoColaboradorXProcesoEvaluacion = iniciado;
                    //context.TablaColaboradorXProcesoEvaluaciones.ModifyElement(col);
                }*/
            }
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
            mail.Body = "El sistema RH++ le indica que el proceso de evaluación en el cual usted es partícipe ya inicio. <br/>" +
                " Puede rendir la evaluación haciendo click en el siguiente enlace: " + "http://dp2kendo.apphb.com/Evaluacion360/Evaluaciones" + ".<br/>" +
                "Sírvase no responder este correo.";

            SmtpServer.Send(mail);

            return View();
        }

    }
}
