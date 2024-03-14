using CineProducto.Bussines;
using DominioCineProducto;
using DominioCineProducto.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmVerCoproductor : System.Web.UI.Page
    {
        public int project_state = 0;
        public int user_role = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblViewProductorSeleccionado.Text = Request.QueryString["project_producer_id"];
            
            NegocioCineProducto neg = new NegocioCineProducto();            
            project_producer pp = neg.getProjectProducerById(int.Parse(lblViewProductorSeleccionado.Text));
            lblIdProducer.Text = pp.producer_id.ToString();            
            User userObj = new User();
            userObj.user_id = Convert.ToInt32(Session["user_id"]);
            this.user_role = userObj.GetUserRole(userObj.user_id);
        }

        protected void checkAdjunto_Click(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            string datosEnviados = b.CommandArgument;

            NegocioCineProducto neg = new NegocioCineProducto();
            project_attachment objPa = neg.getProjectAttachmentById(int.Parse(datosEnviados));

            objPa.project_attachment_approved = 1;
            neg.actualizarEstadoProjectAttachment(objPa);
            ASPxGridView1.DataBind();
        }

        protected void checkAdjuntoRechazar_Click(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            string datosEnviados = b.CommandArgument;

            NegocioCineProducto neg = new NegocioCineProducto();
            project_attachment objPa = neg.getProjectAttachmentById(int.Parse(datosEnviados));

            objPa.project_attachment_approved = 0;
            neg.actualizarEstadoProjectAttachment(objPa);
            ASPxGridView1.DataBind();
        }
    }
}