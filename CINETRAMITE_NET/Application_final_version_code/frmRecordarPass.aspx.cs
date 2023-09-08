using CineProducto.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmRecordarPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecordar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() == string.Empty)
            {
                lblError.Text = "Ingrese un nombre de usuario";
            }
            CineProducto.Bussines.User currentUser = new Bussines.User();
            currentUser.LoadUser(txtUsuario.Text);
            if (currentUser.user_id == 0)
            {
                lblError.Text = "No se encuentra registrado el usuario";
                return;
            }
            //armamos el email
            string subject = "Recordatorio de contraseña dirección de cinematografia";
            string passNuevo= System.IO.Path.GetRandomFileName();
            passNuevo= passNuevo.Substring(0,passNuevo.Length/2)+ DateTime.Now.Ticks.ToString().Substring(5);
            string body = "La información para ingresar al sistema es <br>"+
                "usuario: "+currentUser.username+"<br>"+
                "paswword: "+passNuevo+"<br>";

            Project project = new Project();
            /* Envío de notificación al productor solicitante */
            project.sendMailNotification(currentUser.mail, subject, body,Server);
            txtUsuario.Text="";
            currentUser.updatePassword(currentUser.user_id, currentUser.username, passNuevo);
            //currentUser.updatePassword(22159, "cgarzon@mincultura.gov.co", "cgarzon");
         

            lblError.Text = "Se asigno una nueva contraseña y se enviaron los detalles al correo " + currentUser.mail;
        }
    }
}