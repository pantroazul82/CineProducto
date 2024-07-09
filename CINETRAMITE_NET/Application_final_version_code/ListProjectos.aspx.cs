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
    public partial class ListProjectos : System.Web.UI.Page
    {
        public bool showAdvancedForm = false;

        protected string verEstado(object codEstado, object Estado, object fechaNotificacion)
        {
            int rol = (int)Session["user_role_id"];
            if (rol > 1)
            {
                return Estado.ToString();
            }

            if ((codEstado.ToString().Trim() == "10" || codEstado.ToString() == "9"))
            {
                if (fechaNotificacion == null || fechaNotificacion == System.DBNull.Value || fechaNotificacion.ToString().Trim() == string.Empty)
                    return "Por Notificar";

                DateTime f = (DateTime)fechaNotificacion;
                if (DateTime.Now < f)
                    return "Por Notificar";
            }
            else if ((codEstado.ToString().Trim() == "6" || codEstado.ToString() == "7" || codEstado.ToString() == "8"))
            {
                return "Aclaraciones enviadas";
            }
            else if ((codEstado.ToString().Trim() == "2" || codEstado.ToString() == "3" || codEstado.ToString() == "4"))
            {
                return "Enviada";
            }

            return Estado.ToString();
        }

        protected bool verVisibleResolucion(object fechaNotificacion, object ruta, object version)
        {
            if (fechaNotificacion == null || fechaNotificacion == System.DBNull.Value || fechaNotificacion.ToString().Trim() == string.Empty)
                return false;
            if (ruta == null || ruta == System.DBNull.Value || ruta.ToString().Trim() == string.Empty)
                return false;
            if (version.ToString() == "2")
                return false;

            int rol = (int)Session["user_role_id"];
            if (rol > 1)
            {
                return true;
            }

            if (DateTime.Now < (DateTime)fechaNotificacion)
            {
                return false;
            }

            return true;
        }

        protected bool verVisibleCertificado(object fechaNotificacion, object estado, object version)
        {
            if (fechaNotificacion == null || fechaNotificacion == System.DBNull.Value || fechaNotificacion.ToString().Trim() == string.Empty)
                return false;
            if (version.ToString() == "1")
                return false;
            if (estado.ToString() != "9")
                return false;

            if (DateTime.Now < (DateTime)fechaNotificacion)
            {
                return false;
            }

            return true;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarEstadosDos();
            chkOprimioBoton.Checked = true;
            cargarDAtos();
        }

        private void cargarEstados()
        {

            int rol = (int)Session["user_role_id"];
            DB db = new DB();
            string codigos = "1,2,3,4,5,6,7,8,9,10,11,12,13,14";

            switch (rol)
            {
                case 0: { codigos = "1,2,5,6,9,10,12,13,14"; break; }
                case 1: { codigos = "1,2,5,6,9,10,12,13,14"; break; }
                case 2: { codigos = "2,3,4,5,6,7,8,9,10,12,13,14"; break; }
                case 3: { codigos = "2,3,4,5,6,7,8,9,10,12,13,14"; break; }
                case 4: { codigos = "2,3,4,5,6,7,8,9,10,12,13,14"; break; }
                case 5: { codigos = "1,2,3,4,5,6,7,8,9,10,12,13,14"; break; }
                case 6: { codigos = "2,3,4,5,6,7,8,9,10,12,13,14"; break; }
                case 7: { codigos = "2,3,4,5,6,7,8,9,10,12,13,14"; break; }
            }
            //if (Session["superadmin"] != null)
            //{
            //    codigos = "1,2,3,4,5,6,7,8,9,10,12,13,14";
            //}
            if (!IsPostBack)
            {
                cmbEstado.DataSource = db.Select("select state_id,state_name from dboPrd.state where state_deleted=0 and state_id in (" + codigos + ")").Tables[0];
                cmbEstado.AppendDataBoundItems = true;
                cmbEstado.DataValueField = "state_id";
                cmbEstado.DataTextField = "state_name";
                cmbEstado.DataBind();

                cmbEstado2.DataSource = db.Select("select state_id,state_name from dboPrd.state where state_deleted=0 and state_id in (" + codigos + ")").Tables[0];
                cmbEstado2.AppendDataBoundItems = true;
                cmbEstado2.DataValueField = "state_id";
                cmbEstado2.DataTextField = "state_name";
                cmbEstado2.DataBind();

                if (rol < 1)
                {
                    cmbEstado.Items.Insert(5, new System.Web.UI.WebControls.ListItem("Por Notificar", "15"));
                    cmbEstado2.Items.Insert(5, new System.Web.UI.WebControls.ListItem("Por Notificar", "15"));
                }
                else
                {
                    cmbEstado.Items.Insert(1, new System.Web.UI.WebControls.ListItem("En estudio (Integrado)", "16"));
                    cmbEstado2.Items.Insert(1, new System.Web.UI.WebControls.ListItem("En estudio (Integrado)", "16"));
                }
            }
            switch (rol)
            {
                case 0: { lblEstados.Text = "'1','2','3','4','5','6','7','8','9','10','12','13', '14'"; break; }
                case 1: { lblEstados.Text = "'1','2','3','4','5','6','7','8','9','10','12','13','14'"; break; }
                case 2: { lblEstados.Text = "'2','6'"; break; }//case 2: { lblEstados.Text = "'2','3','6','7'"; break; }
                case 3: { lblEstados.Text = "'3','7'"; break; }//case 3: { lblEstados.Text = "'3','4','7','8'"; break; }
                case 4: { lblEstados.Text = "'4','8'"; break; }// case 4: { lblEstados.Text = "'4','5','7','8'"; break; }
                case 5: { lblEstados.Text = "'2','3','4','5','6','7','8','9','10','12','13','14'"; break; }
                case 6: { lblEstados.Text = "'2','3','4','5','6','7','8','9','10','12','13','14'"; break; }
                case 7: { lblEstados.Text = "'2','3','4','5','6','7','8','9','10','12','13','14'"; break; }
            }
            if (Session["superadmin"] != null)
            {
                // lblEstados.Text = "'2','3','4','5','6','7','8','9','10','12'";
                if (rol == 2)
                {
                    lblEstados.Text = "'2','6'";
                }
                else if (rol == 3)
                {
                    lblEstados.Text = "'3','7'";
                }
                else if (rol == 4)
                {
                    lblEstados.Text = "'4','8'";
                }
                else
                {
                    lblEstados.Text = "'2','3','4','5','6','7','8','9','10','12','13','14'";
                }

            }
        }


        public System.Data.DataTable ejecutarConsulta()
        {
            lblError.Text = "";
            string filtroFechaAvanzado = "";
            string filtroFechaResolucion = "";
            if (chkEstado.Checked)
            {
                if (!CalendarExtenderDesde.SelectedDate.HasValue)
                {
                    lblError.Text = "Ingrese el campo Desde";
                    return null;
                }

                if (!CalendarExtenderHasta.SelectedDate.HasValue)
                {
                    lblError.Text = "Ingrese el campo Hasta";
                    return null;
                }


                string fia = "";
                fia = CalendarExtenderDesde.SelectedDate.Value.Day + "/" + CalendarExtenderDesde.SelectedDate.Value.Month + "/" + CalendarExtenderDesde.SelectedDate.Value.Year;
                string ffa = "";
                ffa = CalendarExtenderHasta.SelectedDate.Value.Day + "/" + CalendarExtenderHasta.SelectedDate.Value.Month + "/" + CalendarExtenderHasta.SelectedDate.Value.Year;

                if (cmbEstado2.SelectedValue == "2")//enviada
                {
                    filtroFechaAvanzado = " and p.project_request_date between '" + fia + @"' and '" + ffa + @" 23:59'";
                }
                else if (cmbEstado2.SelectedValue == "3")//revizada
                {
                    filtroFechaAvanzado = " and p.fecha_revisor_editor between '" + fia + @"' and '" + ffa + @" 23:59'";
                }
                else if (cmbEstado2.SelectedValue == "4")//ediatada
                {
                    filtroFechaAvanzado = " and p.fecha_editor_director between '" + fia + @"' and '" + ffa + @" 23:59'";
                }
                else if (cmbEstado2.SelectedValue == "5")//solicitud de aclaraciones
                {
                    filtroFechaAvanzado = " and p.project_clarification_request_date between '" + fia + @"' and '" + ffa + @" 23:59'";
                }
                else if (cmbEstado2.SelectedValue == "6")//envio de aclaraciones
                {
                    filtroFechaAvanzado = " and p.project_clarification_response_date between '" + fia + @"' and '" + ffa + @" 23:59'";
                }
                else if (cmbEstado2.SelectedValue == "7")
                {
                    filtroFechaAvanzado = " and p.fecha_revisor_editor2 between '" + fia + @"' and '" + ffa + @"' 23:59";
                }
                else if (cmbEstado2.SelectedValue == "8")
                {
                    filtroFechaAvanzado = " and p.fecha_editor_director2 between '" + fia + @"' and '" + ffa + @" 23:59' ";
                }
                else if (cmbEstado2.SelectedValue == "9")//aprobada
                {
                    filtroFechaAvanzado = " and (p.project_resolution_date between '" + fia + @"' and '" + ffa + @" 23:59' and p.state_id =9)";
                }
                else if (cmbEstado2.SelectedValue == "10")//rechazada
                {
                    filtroFechaAvanzado = " and (p.project_resolution_date between '" + fia + @"' and '" + ffa + @" 23:59' and p.state_id =10)";
                }
                else if (cmbEstado2.SelectedValue == "11")//sin definir nunca deberia estar aca
                {
                    filtroFechaAvanzado = " and p.project_resolution_date between '" + fia + @"' and '" + ffa + @" 23:59' ";
                }
                else if (cmbEstado2.SelectedValue == "12")
                {
                    filtroFechaAvanzado = " and p.fecha_cancelacion between '" + fia + @"' and '" + ffa + @" 23:59' ";
                }

            }
            if (chkResolucion.Checked)
            {
                if (!txtResolucionDesdeCalendarExtender1.SelectedDate.HasValue)
                {
                    lblError.Text = "Ingrese el campo Desde";
                    return null;
                }

                if (!txtResolucionHastaCalendarExtender2.SelectedDate.HasValue)
                {
                    lblError.Text = "Ingrese el campo Hasta";
                    return null;
                }


                string fia = "";
                fia = txtResolucionDesdeCalendarExtender1.SelectedDate.Value.Day + "/" + txtResolucionDesdeCalendarExtender1.SelectedDate.Value.Month + "/" + txtResolucionDesdeCalendarExtender1.SelectedDate.Value.Year;
                string ffa = "";
                ffa = txtResolucionHastaCalendarExtender2.SelectedDate.Value.Day + "/" + txtResolucionHastaCalendarExtender2.SelectedDate.Value.Month + "/" + txtResolucionHastaCalendarExtender2.SelectedDate.Value.Year;

                filtroFechaResolucion = " and p.project_resolution_date between '" + fia + @"' and '" + ffa + @"' ";
            }

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

            int rol = (int)Session["user_role_id"];
            string filtroRol = "";
            string filtroFecha = " ";
            if (chkCreadasNoEnviadas.Checked)
            {
                filtroFecha = " p.project_request_date is null ";

            }


            if (fi != string.Empty && ff != string.Empty)
            {
                if (chkCreadasNoEnviadas.Checked)
                {
                    filtroFecha += " OR p.project_request_date between '" + fi + @"' and '" + ff + @" 23:59'";
                }
                else
                {
                    filtroFecha += " p.project_request_date between '" + fi + @"' and '" + ff + @" 23:59'";
                }

            }
            else if (fi != string.Empty)
            {
                if (chkCreadasNoEnviadas.Checked)
                {
                    filtroFecha += " OR p.project_request_date >= '" + fi + "'";
                }
                else
                {
                    filtroFecha += "  p.project_request_date >= '" + fi + "'";
                }
            }
            else if (ff != string.Empty)
            {
                if (chkCreadasNoEnviadas.Checked)
                {
                    filtroFecha += " or p.project_request_date <= '" + ff + @"'";
                }
                else
                {
                    filtroFecha += "  p.project_request_date <= '" + ff + @"'";
                }
            }
            else
            {
                if (chkCreadasNoEnviadas.Checked)
                {
                    filtroFecha += " OR p.project_request_date is NOT null ";
                }
                else
                {
                    filtroFecha += " OR p.project_request_date is NOT null ";
                }
            }

            string filtroEstadoAclaracionesSolicitadas = "";
            string filtroResponsable = "";
            if (rol <= 1)
            {
                filtroRol = "(p.project_idusuario = '" + Session["user_id"] + "') and ";
                grdDevDatos.Columns[1].Visible = false;
                grdDevDatos.Columns[2].Visible = false;
                grdDevDatos.Columns[12].Visible = false;//responsable
                grdDevDatos.Columns[13].Visible = false;//fecha limite

            }
            else
            {
                if (Session["user_id"].ToString().Trim() == "22159")
                {
                    grdDevDatos.Columns[1].Visible = true;
                }
                else
                {
                    grdDevDatos.Columns[1].Visible = false;

                    if (chkOprimioBoton.Checked == false && bool.Parse(Session["ES_REVISOR"].ToString())) //falta saber a q perfiles le aplica este filtro
                    {
                        filtroResponsable = " and p.responsable = " + Session["user_id"].ToString().Trim();
                    }

                    //filtroEstadoAclaracionesSolicitadas = " and p.state_id != 5 ";

                }
            }
            string filtro = "";
            if (cmbTipoProduccion.SelectedValue != "-1")
            {
                filtro = " AND p.production_type_id ='" + cmbTipoProduccion.SelectedValue + "'";
            }

            if (cmbTipoObra.SelectedValue != "-1")
            {
                filtro = filtro + " AND p.project_genre_id ='" + cmbTipoObra.SelectedValue + "'";
            }

            if (cmbDuracionObra.SelectedValue != "-1")
            {
                filtro = filtro + " AND p.project_type_id ='" + cmbDuracionObra.SelectedValue + "'";
            }

            if (cmbProdcutorPrincipal.SelectedValue != "-1")
            {
                filtro = filtro + " AND producer.person_type_id ='" + cmbProdcutorPrincipal.SelectedValue + "'";
            }

            string filtroProdcutor = "";
            if (txtProductor.Text.Trim() != string.Empty)
            {
                filtroProdcutor = " and (producer.producer_name is not null and  (producer.producer_name like '%" + txtProductor.Text.Trim().Replace("'", "%") + @"%' or producer.producer_firstname  like '%" + txtProductor.Text.Trim().Replace("'", "%") + @"%' 
                    or producer.producer_lastname  like '%" + txtProductor.Text.Trim().Replace("'", "%") + @"%'))";
            }

            string sql = @"
set dateformat dmy
select 
p.state_id,p.project_id, s.state_name  Estado, 
case when person_type_id =2 then producer.producer_name else producer.producer_firstname +' '+producer.producer_lastname end 'Productor',
usuario.username 'login' ,p.project_name Obra, 
p.project_request_date Fecha_y_Hora_de_Solicitud,cast(p.project_request_date as date) as Fecha_de_Solicitud,cast(p.project_request_date as time) Hora_de_solicitud,
p.project_clarification_request_date   fecha_solicitud_aclaraciones,cast(p.project_clarification_request_date as time) hora_solicitud_aclaraciones,
p.project_clarification_response_date   fecha_envio_aclaraciones,cast(p.project_clarification_response_date as time) hora_envio_aclaraciones,
p.project_resolution_date 'fecha_resolucion', 
p.project_notification_date  fecha_notificacion,
project_notification2_date as fecha_notificacion_2,
r.resolution_path,
resolution_path2,
(select nombres +' ' + apellidos from dboPrd.usuario where idusuario = p.responsable) as responsable,
fecha_limite as Fecha_Limite,
version

 from dboPrd.project p 
 left join dboPrd.resolution  r on r.project_id= p.project_id 
 left join dboPrd.state s on p.state_id = s.state_id 
 join dboPrd.usuario on usuario.idusuario = p.project_idusuario
left join dboPrd.project_producer on project_producer.project_id = p.project_id and project_producer.project_producer_requester=1
left join dboPrd.producer on producer.producer_id = project_producer.producer_id
where 
(" + filtroRol + @" ( " + filtroFecha + @"))
" + filtroFechaAvanzado + filtroFechaResolucion + filtroResponsable + filtroEstadoAclaracionesSolicitadas + @" 
and p.titulo_provisional like '%" + txtTituloAnterior.Text.Trim().Replace("'", "%") + @"%'
and p.project_name like '%" + txtTitulo.Text.Trim().Replace("'", "%") + @"%' " + filtroProdcutor + @"
     
     and s.state_id in (" + (chkCreadasNoEnviadas.Checked ? lblEstados.Text : lblEstados.Text.Replace("'1',", "")) + ") ";

            if (cmbEstado.SelectedValue == "15")
            {
                sql = sql + " and (project_notification_date > getdate() OR (project_notification_date is null and p.project_resolution_date is not null))";
            }
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

        public void cargarEstadosDos()
        {

            int rol = (int)Session["user_role_id"];
            if (cmbEstado.SelectedValue == "-1")
            {
                if (rol > 1)
                {
                    lblEstados.Text = "'1','2','3','4','5','6','7','8','9','10','12','13','14'";
                }
                else
                {
                    lblEstados.Text = "'1','2','3','4','5','6','7','8','9','10','12','13','14'";
                }
                if (Session["superadmin"] != null)
                {
                    lblEstados.Text = "'1','2','3','4','5','6','7','8','9','10','12','13','14'";
                }
            }
            else
            {
                if (rol > 1)
                {
                    lblEstados.Text = cmbEstado.SelectedValue;
                    if (cmbEstado.SelectedValue.Trim() == "16")
                    {
                        lblEstados.Text = "2,3,4,6,7,8";
                    }
                }
                else
                {
                    if (cmbEstado.SelectedValue.Trim() == "2")
                    {
                        lblEstados.Text = "2,3,4";
                    }
                    else if (cmbEstado.SelectedValue.Trim() == "6")
                    {
                        lblEstados.Text = "6,7,8";
                    }
                    else if (cmbEstado.SelectedValue.Trim() == "15")
                    {
                        lblEstados.Text = "9,10";
                    }
                    else
                    {
                        lblEstados.Text = cmbEstado.SelectedValue;
                    }
                }
            }
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
                        txtInicio_CalendarExtender.SelectedDate = new DateTime(2014, 01, 01);
                        txtFin_CalendarExtender.SelectedDate = DateTime.Now.AddDays(1);

                        CalendarExtenderDesde.SelectedDate = DateTime.Now.AddMonths(-3);
                        CalendarExtenderHasta.SelectedDate = DateTime.Now.AddDays(1);


                        txtResolucionDesdeCalendarExtender1.SelectedDate = DateTime.Now.AddMonths(-3);
                        txtResolucionHastaCalendarExtender2.SelectedDate = DateTime.Now.AddDays(1);

                        if (userObj.checkPermission("ver-formulario-busqueda-en-listado-solicitudes"))
                        {
                            chkCreadasNoEnviadas.Visible = true;
                            chkCreadasNoEnviadas.Checked = false;
                        }
                        else
                        {
                            chkCreadasNoEnviadas.Visible = false;
                            chkCreadasNoEnviadas.Checked = true;
                        }
                    }
                    cargarEstados();
                    /*
    1	Creada          2	Enviada         3	Revisada        4	Editada
5	Aclaraciones solicitadas            6	Aclaraciones enviadas               7	Aclaraciones revisadas
8	Aclaraciones Editadas               9	Aprobada        10	Rechazada       11	Sin Definir                     12	Cancelada
    */
                    //1 solicitnte 2 revisor 3 editor 4 director
                }
                else
                {
                    cargarEstadosDos();

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

        protected void txtEstadoDesde_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtEstadoDesde.Text.Trim() == string.Empty) return;
                int dia = int.Parse(txtEstadoDesde.Text.Substring(0, 2));
                int mes = int.Parse(txtEstadoDesde.Text.Substring(3, 2));
                int ano = int.Parse(txtEstadoDesde.Text.Substring(6, 4));
                CalendarExtenderDesde.SelectedDate = new DateTime(ano, mes, dia);
            }
            catch (Exception) { }
        }

        protected void txtEstadoHasta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtEstadoHasta.Text.Trim() == string.Empty) return;
                int dia = int.Parse(txtEstadoHasta.Text.Substring(0, 2));
                int mes = int.Parse(txtEstadoHasta.Text.Substring(3, 2));
                int ano = int.Parse(txtEstadoHasta.Text.Substring(6, 4));
                CalendarExtenderHasta.SelectedDate = new DateTime(ano, mes, dia);
            }
            catch (Exception) { }
        }


        protected void txtResolucionDesde_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtResolucionDesde.Text.Trim() == string.Empty) return;
                int dia = int.Parse(txtResolucionDesde.Text.Substring(0, 2));
                int mes = int.Parse(txtResolucionDesde.Text.Substring(3, 2));
                int ano = int.Parse(txtResolucionDesde.Text.Substring(6, 4));
                txtResolucionDesdeCalendarExtender1.SelectedDate = new DateTime(ano, mes, dia);
            }
            catch (Exception) { }
        }

        protected void txtResolucionHasta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtResolucionHasta.Text.Trim() == string.Empty) return;
                int dia = int.Parse(txtResolucionHasta.Text.Substring(0, 2));
                int mes = int.Parse(txtResolucionHasta.Text.Substring(3, 2));
                int ano = int.Parse(txtResolucionHasta.Text.Substring(6, 4));
                txtResolucionHastaCalendarExtender2.SelectedDate = new DateTime(ano, mes, dia);
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

       

        
        protected void grdDevDatos_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "project_id")
            {
                int project_id = int.Parse(e.CommandArgs.CommandArgument.ToString());
                //VerCertificado(project_id);
            }

        }
        
    }
}