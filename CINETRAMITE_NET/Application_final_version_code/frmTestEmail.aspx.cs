using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CineProducto.Bussines;

namespace CineProducto
{
    public partial class frmTestEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClave.Text == "Dia+25")
                {
                    Project project = new Project();
                    /* Envío de notificación al productor solicitante */
                    project.sendMailNotification(txtpara.Text, txtasunto.Text, txtMsg.Text, Server);
                    txtNumeroEnviados.Text = (int.Parse(txtNumeroEnviados.Text) + 1).ToString();
                }
                else
                {
                    txtMsg.Text = "Clave Invalida!";
                }
            }
            catch (Exception ex)
            {
                txtMsg.Text = txtMsg.Text + ex.Message;
            }

        }
    }
}