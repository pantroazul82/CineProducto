using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmCambiarClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void actualizarOtrosUsuarios() 
        {
            CineProducto.Bussines.User currentUser = new Bussines.User();
            //currentUser.LoadUser("", txtPasswordActual.Text);
            currentUser.updatePassword(22154, "slopez@mincultura.gov.co", "slopez");
        
        
        }

        protected void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            //actualizarOtrosUsuarios();
            //return;
            //primero validamos que el password actual sea valido
            CineProducto.Bussines.User currentUser = new Bussines.User();
            currentUser.LoadUser(Session["user_username"].ToString(), txtPasswordActual.Text);
            if (!currentUser.logged)
            {
                lblError.Text = "La contraseña actual no es valida.";
                return;
            }
            //---
            if (txtPasswordNueva.Text.Trim() == string.Empty)
            {
                lblError.Text = "La contraseña nueva es obligatoria.";
                return;
            }

            if (txtPasswordNueva.Text.Trim() != txtPasswordConfirmacion.Text )
            {
                lblError.Text = "La contraseña nueva y la confirmación no coinciden.";
                return;
            }
 
            currentUser.updatePassword(currentUser.user_id, currentUser.username, txtPasswordNueva.Text);
            lblError.Text = "Contraseña actualizada exitosamente.";
            btnCambiarPassword.Visible = false;
            linkInicio.Visible = true;
        }
    }
}