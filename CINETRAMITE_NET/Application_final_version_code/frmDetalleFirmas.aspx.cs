using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmDetalleFirmas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["cod"] != null)
            {
                if (!IsPostBack)
                {
                    BD.dsCineTableAdapters.firma_tramiteTableAdapter firma = new BD.dsCineTableAdapters.firma_tramiteTableAdapter();
                    BD.dsCine ds = new BD.dsCine();
                    firma.FillByCod_firma_tramite(ds.firma_tramite, int.Parse(Request.QueryString["cod"]));
                    txtCargo.Text = ds.firma_tramite[0].cargo_firma_tramite;
                    txtNombre.Text = ds.firma_tramite[0].nombre_firma_tramite;
                    chkActivo.Checked = ds.firma_tramite[0].activo;
                }
            }
        }

        protected bool validar() 
        {
            lblError.Text = "";
            if (txtNombre.Text == string.Empty)
            {
                lblError.Text = "El nombre es obligatorio";
                return false;
            }

            return true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar() == false) return;

            BD.dsCineTableAdapters.firma_tramiteTableAdapter firma = new BD.dsCineTableAdapters.firma_tramiteTableAdapter();
            if (chkActivo.Checked)
            {
                firma.UpdateActivoTodos(false);
            }
            if (Request.QueryString["cod"] != null)
            {
                firma.UpdateQuery(txtNombre.Text, chkActivo.Checked, txtCargo.Text, int.Parse(Request.QueryString["cod"]));

            }
            else {
                firma.Insert(txtNombre.Text,chkActivo.Checked,txtCargo.Text);
            }  
            Response.Redirect("frmListadoFirmas.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmListadoFirmas.aspx");
        }
    }
}