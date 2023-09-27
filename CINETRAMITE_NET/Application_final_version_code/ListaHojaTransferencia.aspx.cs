using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CineProducto.Bussines;
using System.Data;
using DominioCineProducto.utils;
using DominioCineProducto;
using DominioCineProducto.Data;


using System.IO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.Globalization;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Text;
using System.Security.Cryptography;
using System.Drawing.Imaging;


namespace CineProducto
{
    public partial class ListaHojaTransferencia : System.Web.UI.Page
    {
        public bool showAdvancedForm = false;
        public int user_role = 0;

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            chkOprimioBoton.Checked = true;
            cargarDAtos();
        }

        public System.Data.DataTable ejecutarConsulta()
        {
            lblError.Text = "";
            string filtroFechaAvanzado = "";


            string fi = "";
            if (txtInicio_CalendarExtender.SelectedDate.HasValue)
            {
                fi = txtInicio_CalendarExtender.SelectedDate.Value.Day + "/" + txtInicio_CalendarExtender.SelectedDate.Value.Month + "/" + txtInicio_CalendarExtender.SelectedDate.Value.Year;
            }

            string ff = "";
            if (txtFin_CalendarExtender.SelectedDate.HasValue)
            {
                ff = txtFin_CalendarExtender.SelectedDate.Value.Day + "/" + txtFin_CalendarExtender.SelectedDate.Value.Month + "/" + txtFin_CalendarExtender.SelectedDate.Value.Year;
            }

            string filtroFecha = " ";

            if (fi != string.Empty && ff != string.Empty)
            {

                    filtroFecha += " p.project_request_date between '" + fi + @"' and '" + ff + @" 23:59'";

            }
            else if (fi != string.Empty)
            {

                    filtroFecha += "  p.project_request_date >= '" + fi + "'";
                
            }
            else if (ff != string.Empty)
            {

                    filtroFecha += "  p.project_request_date <= '" + ff + @"'";
                
            }
            else
            {

                    filtroFecha += " OR p.project_request_date is NOT null ";
                
     
                    filtroFecha += " OR p.project_request_date is NOT null ";
                
            }

            string filtro = "";
           

            string filtroProdcutor = "";
            if (txtProductor.Text.Trim() != string.Empty)
            {

                filtroProdcutor = " and replace(RTRIM(producer.producer_firstname)+' '+RTRIM(isnull(producer.producer_firstname2,''))+' '+RTRIM(producer.producer_lastname)+' '+isnull(RTRIM(producer.producer_lastname2),'') +' '+ RTRIM(isnull(producer.producer_name,'')),'  ',' ')  like '%" + txtProductor.Text.Trim().Replace("'", "%") + "%'";

            }

            string sql = @"
set dateformat dmy
select 
p.project_id,
p.HOJA_TRANSFERENCIA,
case when person_type_id =2 then producer.producer_name else replace(RTRIM(producer.producer_firstname)+' '+RTRIM(isnull(producer.producer_firstname2,''))+' '+RTRIM(producer.producer_lastname)+' '+isnull(RTRIM(producer.producer_lastname2),''),'  ',' ')  end 'Productor',
p.project_name Obra, 
p.project_request_date as Fecha_y_Hora_de_Solicitud,
cast(p.project_request_date as date) as Fecha_de_Solicitud,
cast(p.project_request_date as time) Hora_de_solicitud,
version
 from project p 
 join usuario on usuario.idusuario = p.project_idusuario
left join project_producer on project_producer.project_id = p.project_id and project_producer.project_producer_requester=1
left join producer on producer.producer_id = project_producer.producer_id

where 
 ( " + filtroFecha + @")
" + filtroFechaAvanzado  +  @" 
and p.project_name like '%" + txtTitulo.Text.Trim().Replace("'", "%") + @"%' " + filtroProdcutor + @"
     
 ";
            sql = sql + filtro;
            sql = sql + "  order by " + lblSort.Text + " " + lblSortDirection.Text;
            DB db = new DB();
            return db.Select(sql).Tables[0];
        }

        public void cargarDAtos()
        {
            grdDevDatos.DataSource = ejecutarConsulta();
            grdDevDatos.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            /* Verificamos si existe un usuario autenticado o de lo contrario lo redirigimos a la página inicial */
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                DB db = new DB();


                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(Session["user_id"]);
                int rol = (int)Session["user_role_id"];

                if (!IsPostBack || chkOprimioBoton.Checked == false)
                {
                    if (!IsPostBack)
                    {
                        int user_role = userObj.GetUserRole(userObj.user_id);
                        this.user_role = user_role;
                        txtInicio_CalendarExtender.SelectedDate = new DateTime(2014, 01, 01);
                        txtFin_CalendarExtender.SelectedDate = DateTime.Now.AddDays(1);


                    }
                }

                if (userObj.checkPermission("ver-formulario-busqueda-en-listado-solicitudes"))
                {
                    showAdvancedForm = true;

                }

            }
            else
            {
                Response.Redirect("Default.aspx", true);
            }
            cargarDAtos();
        }

        protected void txtInicio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtInicio.Text.Trim() == string.Empty) return;
                int dia = int.Parse(txtInicio.Text.Substring(0, 2));
                int mes = int.Parse(txtInicio.Text.Substring(3, 2));
                int ano = int.Parse(txtInicio.Text.Substring(6, 4));
                txtInicio_CalendarExtender.SelectedDate = new DateTime(ano, mes, dia);
            }
            catch (Exception) { }

        }

        protected void txtFin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFin.Text.Trim() == string.Empty) return;
                int dia = int.Parse(txtFin.Text.Substring(0, 2));
                int mes = int.Parse(txtFin.Text.Substring(3, 2));
                int ano = int.Parse(txtFin.Text.Substring(6, 4));
                txtFin_CalendarExtender.SelectedDate = new DateTime(ano, mes, dia);
            }
            catch (Exception) { }
        }

        protected void btnLimpiarDesde_Click(object sender, EventArgs e)
        {
            txtInicio_CalendarExtender.SelectedDate = null;
            txtInicio.Text = "";
        }

        protected void btnLimpiarHasta_Click(object sender, EventArgs e)
        {
            txtFin_CalendarExtender.SelectedDate = null;
            txtFin.Text = "";
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            excel xls = new excel();
            xls.generarExcel(Server.MapPath("~/temp/"), "Tramite producto",
                ejecutarConsulta(), Response);
        }

    }
}