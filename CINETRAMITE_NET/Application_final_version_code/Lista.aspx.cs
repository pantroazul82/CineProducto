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
    public partial class Lista : System.Web.UI.Page
    {
        public bool showAdvancedForm = false;
        public int user_role =0; 

        protected string verEstado(object codEstado, object Estado, object fechaNotificacion)
        {
            int rol = (int)Session["user_role_id"];
            this.user_role = rol;
            if (rol > 1)
            {
                return Estado.ToString();
            }

            if ((codEstado.ToString().Trim() == "10" || codEstado.ToString() == "9")  )
            {
                if (fechaNotificacion == null || fechaNotificacion == System.DBNull.Value || fechaNotificacion.ToString().Trim() == string.Empty)
                    return "Por Notificar";

                DateTime f = (DateTime)fechaNotificacion;
                if(DateTime.Now < f)
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

        protected void esProductor()
        {
            bool esProductor = (bool)Session["ES_PRODUCTOR"];
            if (esProductor)
            {
                grdDevDatos.Columns["Fecha_Limite"].Visible = false;
            }
        }
        protected bool verVisibleResolucion(object fechaNotificacion,object ruta, object version)
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

        protected bool verVisibleCertificado(object fechaNotificacion,object estado, object version)
        {
            if (fechaNotificacion == null || fechaNotificacion == System.DBNull.Value || fechaNotificacion.ToString().Trim() == string.Empty)
                return false;
            if (version.ToString() == "1")
                return false;
            if (estado.ToString() != "9")
                return false;

            if ( DateTime.Now < (DateTime)fechaNotificacion)
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
            if (Session["superadmin"] != null)
            {
                codigos = "1,2,3,4,5,6,7,8,9,10,12,13,14";
            }
            if (!IsPostBack)
            {
                cmbEstado.DataSource = db.Select("select state_id,state_name from state where state_deleted=0 and state_id in (" + codigos + ")").Tables[0];
                cmbEstado.AppendDataBoundItems = true;
                cmbEstado.DataValueField = "state_id";
                cmbEstado.DataTextField = "state_name";
                cmbEstado.DataBind();

                cmbEstado2.DataSource = db.Select("select state_id,state_name from state where state_deleted=0 and state_id in (" + codigos + ")").Tables[0];
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


        public System.Data.DataTable  ejecutarConsulta() 
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
                }else if (cmbEstado2.SelectedValue == "3")//revizada
                {
                    filtroFechaAvanzado = " and p.fecha_revisor_editor between '" + fia + @"' and '" + ffa + @" 23:59'";
                }else if (cmbEstado2.SelectedValue == "4")//ediatada
                {
                    filtroFechaAvanzado = " and p.fecha_editor_director between '" + fia + @"' and '" + ffa + @" 23:59'";
                }else if (cmbEstado2.SelectedValue == "5")//solicitud de aclaraciones
                {
                    filtroFechaAvanzado = " and p.project_clarification_request_date between '" + fia + @"' and '" + ffa + @" 23:59'";
                }else if (cmbEstado2.SelectedValue == "6")//envio de aclaraciones
                {
                    filtroFechaAvanzado = " and p.project_clarification_response_date between '" + fia + @"' and '" + ffa + @" 23:59'";
                }else if (cmbEstado2.SelectedValue == "7")
                {
                    filtroFechaAvanzado = " and p.fecha_revisor_editor2 between '" + fia + @"' and '" + ffa + @"' 23:59";
                }else if (cmbEstado2.SelectedValue == "8")
                {
                    filtroFechaAvanzado = " and p.fecha_editor_director2 between '" + fia + @"' and '" + ffa + @" 23:59' ";
                }else if (cmbEstado2.SelectedValue == "9")//aprobada
                {
                    filtroFechaAvanzado = " and (p.project_resolution_date between '" + fia + @"' and '" + ffa + @" 23:59' and p.state_id =9)";
                }else if (cmbEstado2.SelectedValue == "10")//rechazada
                {
                    filtroFechaAvanzado = " and (p.project_resolution_date between '" + fia + @"' and '" + ffa + @" 23:59' and p.state_id =10)";
                }else if (cmbEstado2.SelectedValue == "11")//sin definir nunca deberia estar aca
                {
                    filtroFechaAvanzado = " and p.project_resolution_date between '" + fia + @"' and '" + ffa + @" 23:59' ";
                }else if (cmbEstado2.SelectedValue == "12")
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
            if(chkCreadasNoEnviadas.Checked)
            {
                filtroFecha = " p.project_request_date is null ";

            }


            if (fi != string.Empty && ff != string.Empty)
            {
                if (chkCreadasNoEnviadas.Checked)
                {
                    filtroFecha += " OR p.project_request_date between '" + fi + @"' and '" + ff + @" 23:59'";
                }
                else {
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
            }else {
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
            else {
                if (Session["user_id"].ToString().Trim() == "22159")
                {
                    grdDevDatos.Columns[1].Visible = true;
                }
                else {
                    grdDevDatos.Columns[1].Visible = false;

                    if (chkOprimioBoton.Checked == false && bool.Parse(Session["ES_REVISOR"].ToString()) ) //falta saber a q perfiles le aplica este filtro
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
                //filtroProdcutor = " and (producer.producer_name is not null and  (producer.producer_name like '%" + txtProductor.Text.Trim().Replace("'", "%") + @"%' or producer.producer_firstname  like '%" + txtProductor.Text.Trim().Replace("'", "%") + @"%' 
                //    or producer.producer_lastname  like '%" + txtProductor.Text.Trim().Replace("'", "%") + @"%'))";

                filtroProdcutor = " and replace(RTRIM(producer.producer_firstname)+' '+RTRIM(isnull(producer.producer_firstname2,''))+' '+RTRIM(producer.producer_lastname)+' '+isnull(RTRIM(producer.producer_lastname2),'') +' '+ RTRIM(isnull(producer.producer_name,'')),'  ',' ')  like '%" + txtProductor.Text.Trim().Replace("'", "%") + "%'";

            }

            string sql = @"
set dateformat dmy
select 
p.state_id,p.project_id, s.state_name  Estado, 
case when person_type_id =2 then producer.producer_name else replace(RTRIM(producer.producer_firstname)+' '+RTRIM(isnull(producer.producer_firstname2,''))+' '+RTRIM(producer.producer_lastname)+' '+isnull(RTRIM(producer.producer_lastname2),''),'  ',' ')  end 'Productor',
p.project_name Obra, 
p.project_request_date as Fecha_y_Hora_de_Solicitud,
cast(p.project_request_date as date) as Fecha_de_Solicitud,
cast(p.project_request_date as time) Hora_de_solicitud,
cast(p.project_clarification_request_date as date) as fecha_solicitud_aclaraciones,
cast(p.project_clarification_request_date as time) as hora_solicitud_aclaraciones,
cast (p.project_clarification_response_date as date) as fecha_envio_aclaraciones,
cast(p.project_clarification_response_date as time) as hora_envio_aclaraciones,
p.project_resolution_date as fecha_resolucion, 
p.project_notification_date as fecha_notificacion, 
project_notification2_date as fecha_notificacion_2,
fecha_limite as Fecha_Limite,
case
when p.state_id=9 and project_notification_date is not null then cast(p.project_notification_date as date)
when p.state_id=9 and project_resolution_date is not null then cast(p.project_resolution_date as date)
when p.state_id=10 then cast(p.fecha_final as date) 
when p.state_id=12 then cast(p.fecha_cancelacion as date)

--when p.state_id=14 then cast(p.fecha_desistido as date) else null end 'fecha_tramite_fin',

when p.state_id=14 then dateadd(day,20, cast(p.project_clarification_request_date as date)) else null end 'fecha_tramite_fin',
r.resolution_path as pdf_resolucion,
resolution_path2 as pdf_resolucion_aclaratoria,
(select nombres +' ' + apellidos from usuario where idusuario = p.responsable) as responsable,
usuario.username 'login',
version

 from project p 
 left join resolution  r on r.project_id= p.project_id 
 left join state s on p.state_id = s.state_id 
 join usuario on usuario.idusuario = p.project_idusuario
left join project_producer on project_producer.project_id = p.project_id and project_producer.project_producer_requester=1
left join producer on producer.producer_id = project_producer.producer_id
where 
(" + filtroRol + @" ( "+filtroFecha+@"))
"+filtroFechaAvanzado+filtroFechaResolucion + filtroResponsable + filtroEstadoAclaracionesSolicitadas  + @" 
and p.titulo_provisional like '%" + txtTituloAnterior.Text.Trim().Replace("'", "%") + @"%'
and p.project_name like '%" + txtTitulo.Text.Trim().Replace("'","%")+@"%' "+filtroProdcutor+@"
     
     and s.state_id in (" + (chkCreadasNoEnviadas.Checked ? lblEstados.Text : lblEstados.Text.Replace("'1',","")) + ") ";

            if (cmbEstado.SelectedValue == "15")
            {
                sql = sql + " and (project_notification_date > getdate() OR (project_notification_date is null and p.project_resolution_date is not null))";
            }
            sql = sql + filtro;
           sql=sql+ "  order by " + lblSort.Text + " " + lblSortDirection.Text;
            DB db = new DB();
         return   db.Select(sql).Tables[0];        
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
            if (Session["ES_PRODUCTOR"]==null || bool.Parse(Session["ES_PRODUCTOR"].ToString())) {
                btnDesistidos.Visible = false;
                //btnDesistidosAviso.Visible = false;
                lblDesistidos.Visible = false;
            }

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
                        txtInicio_CalendarExtender.SelectedDate = new DateTime(2014,01,01);
                        txtFin_CalendarExtender.SelectedDate = DateTime.Now.AddDays(1);

                        CalendarExtenderDesde.SelectedDate = DateTime.Now.AddMonths(-3);
                        CalendarExtenderHasta.SelectedDate = DateTime.Now.AddDays(1);


                        txtResolucionDesdeCalendarExtender1.SelectedDate = DateTime.Now.AddMonths(-3);
                        txtResolucionHastaCalendarExtender2.SelectedDate = DateTime.Now.AddDays(1);

                        if (userObj.checkPermission("ver-formulario-busqueda-en-listado-solicitudes"))
                        {
                            chkCreadasNoEnviadas.Visible = true;
                            chkCreadasNoEnviadas.Checked = false;
                        }else {
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
            try { 
            if (txtInicio.Text.Trim() == string.Empty) return;
            int dia=int.Parse(txtInicio.Text.Substring(0,2));
            int mes=int.Parse(txtInicio.Text.Substring(3,2));
            int ano=int.Parse(txtInicio.Text.Substring(6,4));
            txtInicio_CalendarExtender.SelectedDate = new DateTime(ano,mes,dia);
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
            try{
            if (txtEstadoDesde.Text.Trim() == string.Empty) return;
            int dia = int.Parse(txtEstadoDesde.Text.Substring(0, 2));
            int mes = int.Parse(txtEstadoDesde.Text.Substring(3, 2));
            int ano = int.Parse(txtEstadoDesde.Text.Substring(6, 4));
            CalendarExtenderDesde.SelectedDate = new DateTime(ano, mes, dia);
            }catch(Exception){}
        }

        protected void txtEstadoHasta_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtEstadoHasta.Text.Trim() == string.Empty) return;
            int dia = int.Parse(txtEstadoHasta.Text.Substring(0, 2));
            int mes = int.Parse(txtEstadoHasta.Text.Substring(3, 2));
            int ano = int.Parse(txtEstadoHasta.Text.Substring(6, 4));
            CalendarExtenderHasta.SelectedDate = new DateTime(ano, mes, dia);
            }catch(Exception){}
        }


        protected void txtResolucionDesde_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtResolucionDesde.Text.Trim() == string.Empty) return;
            int dia = int.Parse(txtResolucionDesde.Text.Substring(0, 2));
            int mes = int.Parse(txtResolucionDesde.Text.Substring(3, 2));
            int ano = int.Parse(txtResolucionDesde.Text.Substring(6, 4));
            txtResolucionDesdeCalendarExtender1.SelectedDate = new DateTime(ano, mes, dia);
            }catch(Exception){}
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
            }catch(Exception){}
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

        protected void btnDesistidos_Click(object sender, EventArgs e)
        {

            NegocioCineProducto neg = new NegocioCineProducto();
            List<project> projecsTToAclarate = neg.getProjectByState(5);//5 es estado aclaracion soliticada

            Calendario cal = new Calendario();
            string codDesistidos = "";
            foreach (project unProject in projecsTToAclarate) 
            {
                if (unProject.project_clarification_request_date != null)
                {
                    DateTime fechaLimiteProject = cal.SumarDiasLaborales(unProject.project_clarification_request_date, 15); // 15 dias laborales
                    if (fechaLimiteProject < DateTime.Now)
                    { //vencida
                        unProject.state_id = 14; //14 es desistido
                        neg.ActualizarEstadoDeProyecto(unProject);
                        codDesistidos += unProject.project_id.ToString() + ", ";
                        //enviar alerta de desistido
                        enviarNotificacionDesistido(unProject.project_id);
                    }
                }
            }

            if (codDesistidos != "")
                lblDesistidos.Text += "Se marcaron como desistidos los proyectos con Codigo: " + codDesistidos;
            else
                lblDesistidos.Text = "No se encontraron proyectos para desistir";


            //******aviso desistidos******//

            
            projecsTToAclarate = neg.getProjectByState(5);//5 es estado aclaracion soliticada

            cal = new Calendario();
            codDesistidos = "";
            foreach (project unProject in projecsTToAclarate)
            {
                if (unProject.project_clarification_request_date != null)
                {
                    DateTime fechaLimiteProject = cal.SumarDiasLaborales(unProject.project_clarification_request_date, 10); // 15 dias laborales
                    if (fechaLimiteProject < DateTime.Now  && (unProject.notificaciones_desistidos_enviadas == null || unProject.notificaciones_desistidos_enviadas == 0) )
                    {
                        codDesistidos += unProject.project_id.ToString() + ", ";
                        //enviar alerta de desistido
                        enviarAvisoNotificacionDesistido(unProject.project_id);
                        neg.actualizarDesistidoNotificacion(unProject);
                    }
                }
            }

            if (codDesistidos != "")
                lblDesistidos.Text += ". Se notificaron para desistidos los proyectos con Codigo: " + codDesistidos;
            else
                lblDesistidos.Text += ". No se encontraron proyectos para notificar como desistidos";

        }

        public void enviarNotificacionDesistido(int project_id) {
            Project project = new Project();
            project.LoadProject(project_id);
            string mailTo = "";
            string subject = "Su solicitud de la obra " + project .project_name+ " ha sido DESISTIDA";
            int RequesterProducerTemp = project.producer.FindIndex(
                delegate (Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
            if (RequesterProducerTemp != -1)
            {
                mailTo = project.producer[RequesterProducerTemp].producer_email;
                //producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
            }

            string body = @"Estimado productor: 
                           </br></br>
                             <p style='text-align:justify;'> 
                            Con fundamento en el artículo 2.10.1.4 del Decreto 1080 de 2015 modificado por el Decreto 525 de 2021, le informamos que su <u>solicitud se consideró como DESISTIDA</u>, toda vez que no se dio respuesta al requerimiento emitido por el Ministerio de Cultura dentro del plazo máximo establecido en dicha norma. 
                            </br>
                            Tenga en cuenta que, en caso de tener interés en ello, podrá solicitar nuevamente el reconocimiento de la nacionalidad de esta obra cinematográfica, para lo cual deberá presentar una nueva solicitud y allegar la información y documentos allí requeridos en consonancia con la Ley 397 de 1997, el Decreto 1080 de 2015 y la Resolución 1021 de 2016 del Ministerio de Cultura
                            <br />
                        <br />
                        Si desea evaluar nuestro servicio lo invitamos a diligenciar una breve encuesta en el siguiente enlace <a href='https://forms.office.com/r/nnZ7UHd6kU'>Satisfacción Tramite en Línea</a>

                            </p>
                           ";            
            project.sendMailNotification(mailTo, subject, body, Server);
        }

        
        public void VerCertificado(int id_projecto_param)
        {
            Project project = new Project();
            project.LoadProject(id_projecto_param);

            NegocioCineProducto neg = new NegocioCineProducto();
            project myProject = neg.getProject(id_projecto_param);



            string fileName = "Certificado_" + myProject.numero_certificado.ToString() + ".pdf";

            if (myProject.state_id == 9 && (myProject.ruta_certificado != null && myProject.ruta_certificado != string.Empty))
            {
                System.Configuration.AppSettingsReader ar = new System.Configuration.AppSettingsReader();
                string pathArchivosPermanente = ar.GetValue("pathArchivosPermanente", typeof(string)).ToString();
                string ruta = "~/" + pathArchivosPermanente + "/";

                string rutaCompleta = Server.MapPath(ruta + myProject.ruta_certificado);
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(rutaCompleta));
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                var bytes = System.IO.File.ReadAllBytes(rutaCompleta);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
                
            }
            else
            {

            if (myProject.numero_certificado == null)
            {
                fileName = Guid.NewGuid().ToString() + ".pdf";
            }

            Document document = new Document(PageSize.LEGAL, 50, 50, 15, 25);
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    Paragraph separtor = new Paragraph("\n");
                    Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                    Font boldFontBlue = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLUE);
                    Font boldFontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                    Font boldFontTituloSub = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.BOLD | Font.UNDERLINE);
                    Font normal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    Font fontPie = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                    PdfPTable t = new PdfPTable(3);
                    t.SetWidthPercentage(new float[] { 190, 310, 100 }, PageSize.LEGAL);
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("images/mincultura.png"));
                    jpg.ScaleToFit(150f, 50f);
                    //Give space before image
                    jpg.SpacingBefore = 1f;
                    //Give some space after the image
                    jpg.SpacingAfter = 1f;
                    jpg.Alignment = Element.ALIGN_RIGHT;
                    t.AddCell(new PdfPCell(jpg) { Rowspan = 5, PaddingLeft = 1, PaddingTop = 1 });
                    t.AddCell(new PdfPCell(new Paragraph(new Chunk("\nMINISTERIO DE CULTURA DE COLOMBIA\n\n\n", boldFont))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 5 });
                    t.AddCell(new PdfPCell(new Paragraph("\n" + myProject.numero_certificado.ToString())) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 5 });
                    document.Add(t);
                    document.Add(separtor);


                    var titulo = new Paragraph(new Chunk("CERTIFICACIÓN DE PRODUCTO NACIONAL (CPN)", boldFontTitulo));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    titulo.Font = boldFontTitulo;
                    document.Add(titulo);
                    document.Add(Chunk.NEWLINE);


                    var phraseSubTitulo = new Phrase();
                    string mesRes = "";
                    string fechaRes = "";
                    if (myProject.project_resolution_date != null)
                    {
                        DateTime dRes = DateTime.Parse(myProject.project_resolution_date.ToString());
                        switch (dRes.Month)
                        {
                            case 1: mesRes = "Enero"; break;
                            case 2: mesRes = "Febrero"; break;
                            case 3: mesRes = "Marzo"; break;
                            case 4: mesRes = "Abril"; break;
                            case 5: mesRes = "Mayo"; break;
                            case 6: mesRes = "Junio"; break;
                            case 7: mesRes = "Julio"; break;
                            case 8: mesRes = "Agosto"; break;
                            case 9: mesRes = "Septiembre"; break;
                            case 10: mesRes = "Octubre"; break;
                            case 11: mesRes = "Noviembre"; break;
                            case 12: mesRes = "Diciembre"; break;
                        }
                        fechaRes = dRes.Day.ToString() + " de " + mesRes + " de " + dRes.Year.ToString();
                    }

                    phraseSubTitulo.Add("Certificado No. ");
                    string numeroCertificadoMostrar = "";
                    if (myProject.numero_certificado != null && myProject.numero_certificado > 0)
                    {
                        numeroCertificadoMostrar = (int.Parse(myProject.numero_certificado.ToString())).ToString("D5");
                    }
                    phraseSubTitulo.Add(new Chunk(numeroCertificadoMostrar, boldFontTituloSub));
                    //phraseSubTitulo.Add(" del ");
                    //phraseSubTitulo.Add(new Chunk(fechaRes, boldFontTituloSub));
                    document.Add(phraseSubTitulo);
                    document.Add(separtor);

                    var parag = new Paragraph(new Chunk("La Dirección de Audiovisuales, Cine y Medios Interactivos, del Ministerio de Cultura de la República de Colombia, de conformidad con lo previsto en el artículo 2.10.1.4. del Decreto 1080 de 2015, certifica el carácter de producto nacional a la siguiente obra cinematográfica:", boldFont));
                    parag.Alignment = Element.ALIGN_JUSTIFIED;
                    parag.Font = boldFontTitulo;
                    document.Add(parag);
                    document.Add(separtor);


                    var phraseTitulo = new Phrase();
                    phraseTitulo.Add(new Chunk("Titulo: ", boldFont));
                    document.Add(phraseTitulo);
                    document.Add(Chunk.NEWLINE);

                    var phraseTitulo2 = new Phrase();
                    phraseTitulo2.Add(myProject.project_name);
                    document.Add(phraseTitulo2);
                    document.Add(Chunk.NEWLINE);

                    int? duracion_minutos = myProject.project_duration / 60;
                    int? duracion_segundos = myProject.project_duration % 60;
                    string duracionPeli = duracion_minutos + " minutos y " + duracion_segundos + " segundos";
                    var phraseTipoPeli = new Phrase();
                    string tipoNombre = "";
                    switch (myProject.project_type_id)
                    {
                        case 1: tipoNombre = "Largometraje"; break;
                        case 2: tipoNombre = "Largometraje para televisión"; break;
                        case 3: tipoNombre = "Cortometraje"; break;
                    }
                    phraseTipoPeli.Add(tipoNombre + " de " + myProject.project_genre.project_genre_name + " cuya duración es de " + duracionPeli);
                    document.Add(phraseTipoPeli);
                    document.Add(separtor);


                    //************ nacional
                    var phrasePropductionTipo = new Phrase();
                    phrasePropductionTipo.Add(new Chunk(myProject.production_type.production_type_name.Trim() + " nacional producida por: ", boldFont));
                    document.Add(phrasePropductionTipo);
                    document.Add(Chunk.NEWLINE);

                    PdfPTable tP = new PdfPTable(11);
                    tP.WidthPercentage = 100f;
                    tP.AddCell(new PdfPCell(new Paragraph(new Chunk("Nombre", boldFont))) { Colspan = 4 });
                    tP.AddCell(new PdfPCell(new Paragraph(new Chunk("Identificación", boldFont))) { Colspan = 3 });
                    tP.AddCell(new PdfPCell(new Paragraph(new Chunk("País", boldFont))) { Colspan = 2 });
                    tP.AddCell(new PdfPCell(new Paragraph(new Chunk("Porcentaje", boldFont))) { Colspan = 2 });

                    foreach (project_producer unProjectProducer in myProject.project_producer.Where(x => x.producer.producer_type_id == 1))
                    {

                        if (unProjectProducer.producer.person_type_id == 1)
                        {
                            string segundoNombre = "";
                            if (unProjectProducer.producer.producer_firstname2 != null && unProjectProducer.producer.producer_firstname2 != "")
                                segundoNombre = " " + unProjectProducer.producer.producer_firstname2;

                            tP.AddCell(new PdfPCell(new Paragraph(StringExtensors.ToNombrePropio(unProjectProducer.producer.producer_firstname + segundoNombre + " " + unProjectProducer.producer.producer_lastname + " " + unProjectProducer.producer.producer_lastname2))) { Colspan = 4 });


                            string ccCompleto = "";
                            if (unProjectProducer.producer.producer_identification_number != null && unProjectProducer.producer.producer_identification_number != "")
                            {
                                ccCompleto = string.Format("{0:n0}", Convert.ToInt64(unProjectProducer.producer.producer_identification_number));
                            }
                            tP.AddCell(new PdfPCell(new Paragraph("C.C. " + ccCompleto)) { Colspan = 3 });
                            tP.AddCell(new PdfPCell(new Paragraph("Colombia")) { Colspan = 2 });                            

                            //var phraseProductor = new Phrase();
                            //phraseProductor.Add(unProjectProducer.producer.producer_firstname+" " +unProjectProducer.producer.producer_lastname + ", C.C. " + unProjectProducer.producer.producer_identification_number + " ("+unProjectProducer.producer.producer_type.producer_type_name+")");
                            //document.Add(phraseProductor);
                            //document.Add(Chunk.NEWLINE);

                        }
                        else
                        {
                            string nitCompleto = "";
                            if (unProjectProducer.producer.producer_nit_dig_verif != null && unProjectProducer.producer.producer_nit != null && unProjectProducer.producer.producer_nit != "")
                            {
                                nitCompleto = string.Format("{0:n0}", Convert.ToInt64(unProjectProducer.producer.producer_nit));
                                nitCompleto += "-" + unProjectProducer.producer.producer_nit_dig_verif.ToString();
                            }
                            string producerTipoEmpresaMostrar = "";
                            if (unProjectProducer.producer.producer_company_type_id != null && unProjectProducer.producer.producer_company_type_id < 5)
                            {
                                producerTipoEmpresaMostrar = " " + unProjectProducer.producer.producer_company_type.producer_company_type_name;
                            }
                            if (unProjectProducer.producer.producer_type_id == 2) producerTipoEmpresaMostrar = "";

                            tP.AddCell(new PdfPCell(new Paragraph(StringExtensors.ToNombrePropio(unProjectProducer.producer.producer_name) + producerTipoEmpresaMostrar)) { Colspan = 4 });
                            if (unProjectProducer.producer.producer_type_id == 2)
                            {
                                tP.AddCell(new PdfPCell(new Paragraph(nitCompleto)) { Colspan = 3 });
                                tP.AddCell(new PdfPCell(new Paragraph(unProjectProducer.producer.producer_country)) { Colspan = 2 });
                            }
                            else
                            {
                                tP.AddCell(new PdfPCell(new Paragraph("NIT " + nitCompleto)) { Colspan = 3 });
                                tP.AddCell(new PdfPCell(new Paragraph("Colombia")) { Colspan = 2 });
                            }


                            //var phraseProductor = new Phrase();
                            //phraseProductor.Add(unProjectProducer.producer.producer_name + ", NIT " + unProjectProducer.producer.producer_nit + " (" + unProjectProducer.producer.producer_type.producer_type_name + ")");
                            //document.Add(phraseProductor);
                            //document.Add(Chunk.NEWLINE);
                        }
                        tP.AddCell(new PdfPCell(new Paragraph(unProjectProducer.project_producer_participation_percentage.ToString() + "%")) { Colspan = 2 });

                    }
                    document.Add(tP);

                    document.Add(separtor);

                    //************ si es coproduccion
                    if (myProject.production_type_id == 2)
                    {
                        var phrasePropductionTipo2 = new Phrase();
                        phrasePropductionTipo2.Add(new Chunk("En coproducción con: ", boldFont));
                        document.Add(phrasePropductionTipo2);
                        document.Add(Chunk.NEWLINE);

                        PdfPTable tP2 = new PdfPTable(5);
                        tP2.WidthPercentage = 100f;
                        tP2.AddCell(new PdfPCell(new Paragraph(new Chunk("Nombre", boldFont))) { Colspan = 2 });
                        //tP2.AddCell(new PdfPCell(new Paragraph(new Chunk("Identificación", boldFont))));
                        tP2.AddCell(new PdfPCell(new Paragraph(new Chunk("País", boldFont))) { Colspan = 2 });
                        tP2.AddCell(new PdfPCell(new Paragraph(new Chunk("Porcentaje", boldFont))));

                        foreach (project_producer unProjectProducer in myProject.project_producer.Where(x => x.producer.producer_type_id == 2))
                        {

                            if (unProjectProducer.producer.person_type_id == 1)
                            {
                                string segundoNombre = "";
                                if (unProjectProducer.producer.producer_firstname2 != null && unProjectProducer.producer.producer_firstname2 != "")
                                    segundoNombre = " " + unProjectProducer.producer.producer_firstname2;

                                tP2.AddCell(new PdfPCell(new Paragraph(StringExtensors.ToNombrePropio(unProjectProducer.producer.producer_firstname + segundoNombre + " " + unProjectProducer.producer.producer_lastname + " " + unProjectProducer.producer.producer_lastname2))) { Colspan = 2 });

                                string ccCompleto = "";
                                if (unProjectProducer.producer.producer_identification_number != null && unProjectProducer.producer.producer_identification_number != "")
                                {
                                    ccCompleto = string.Format("{0:n0}", Convert.ToInt64(unProjectProducer.producer.producer_identification_number));
                                }
                                if (unProjectProducer.producer.producer_type_id == 2)
                                {
                                    //tP2.AddCell(new PdfPCell(new Paragraph("")));
                                }
                                else
                                {
                                    tP2.AddCell(new PdfPCell(new Paragraph("C.C. " + ccCompleto)));
                                }
                                tP2.AddCell(new PdfPCell(new Paragraph(unProjectProducer.producer.producer_country)) { Colspan = 2 });

                                //var phraseProductor = new Phrase();
                                //phraseProductor.Add(unProjectProducer.producer.producer_firstname+" " +unProjectProducer.producer.producer_lastname + ", C.C. " + unProjectProducer.producer.producer_identification_number + " ("+unProjectProducer.producer.producer_type.producer_type_name+")");
                                //document.Add(phraseProductor);
                                //document.Add(Chunk.NEWLINE);

                            }
                            else
                            {
                                string nitCompleto = "";
                                if (unProjectProducer.producer.producer_nit_dig_verif != null && unProjectProducer.producer.producer_nit != null && unProjectProducer.producer.producer_nit != "")
                                {
                                    nitCompleto = string.Format("{0:n0}", Convert.ToInt64(unProjectProducer.producer.producer_nit));
                                    nitCompleto += "-" + unProjectProducer.producer.producer_nit_dig_verif.ToString();
                                }

                                string producerTipoEmpresaMostrar = "";
                                if (unProjectProducer.producer.producer_company_type_id != null && unProjectProducer.producer.producer_company_type_id < 5)
                                {
                                    producerTipoEmpresaMostrar = " " + unProjectProducer.producer.producer_company_type.producer_company_type_name;
                                }
                                tP2.AddCell(new PdfPCell(new Paragraph(StringExtensors.ToNombrePropio(unProjectProducer.producer.producer_name) + producerTipoEmpresaMostrar)) { Colspan = 2 });
                                if (unProjectProducer.producer.producer_type_id == 2)
                                {
                                    //tP2.AddCell(new PdfPCell(new Paragraph("")));
                                    tP2.AddCell(new PdfPCell(new Paragraph(unProjectProducer.producer.producer_country)) { Colspan = 2 });
                                }
                                else
                                {
                                    tP2.AddCell(new PdfPCell(new Paragraph("NIT " + nitCompleto)));
                                    tP2.AddCell(new PdfPCell(new Paragraph("Colombia")));
                                }

                                //var phraseProductor = new Phrase();
                                //phraseProductor.Add(unProjectProducer.producer.producer_name + ", NIT " + unProjectProducer.producer.producer_nit + " (" + unProjectProducer.producer.producer_type.producer_type_name + ")");
                                //document.Add(phraseProductor);
                                //document.Add(Chunk.NEWLINE);
                            }
                            tP2.AddCell(new PdfPCell(new Paragraph(unProjectProducer.project_producer_participation_percentage.ToString() + "%")));
                        }
                        document.Add(tP2);
                        document.Add(separtor);
                    }

                    var phraseParticipacion = new Phrase();
                    phraseParticipacion.Add("Con un porcentaje de participación económica nacional del " + Convert.ToInt32(myProject.project_percentage).ToString() + "% ");
                    document.Add(phraseParticipacion);
                    document.Add(separtor);


                    var phrasePersonal = new Phrase();
                    phrasePersonal.Add(new Chunk("Personal nacional acreditado:", boldFont));
                    document.Add(phrasePersonal);
                    document.Add(Chunk.NEWLINE);


                    PdfPTable tPers = new PdfPTable(2);
                    tPers.WidthPercentage = 100f;
                    tPers.AddCell(new PdfPCell(new Paragraph(new Chunk("Nombre", boldFont))));
                    tPers.AddCell(new PdfPCell(new Paragraph(new Chunk("Cargo", boldFont))));
                    foreach (project_staff unPersonal in myProject.project_staff.OrderBy(x => x.project_staff_position_id))
                    {
                        string segundoNombre = "";
                        if (unPersonal.project_staff_firstname2 != null && unPersonal.project_staff_firstname2 != "")
                        {
                            segundoNombre = " " + unPersonal.project_staff_firstname2;
                        }

                        tPers.AddCell(new PdfPCell(new Paragraph(StringExtensors.ToNombrePropio(unPersonal.project_staff_firstname + segundoNombre + " " + unPersonal.project_staff_lastname + " " + unPersonal.project_staff_lastname2))));
                        tPers.AddCell(new PdfPCell(new Paragraph(unPersonal.position.position_name)));

                        //var phraseProductor = new Phrase();
                        //phraseProductor.Add(unPersonal.project_staff_firstname + " " + unPersonal.project_staff_firstname2 + " " + unPersonal.project_staff_lastname + " " + unPersonal.project_staff_lastname2 + ", "+ unPersonal.position.position_name);
                        //document.Add(phraseProductor);              
                        //document.Add(Chunk.NEWLINE);
                    }
                    document.Add(tPers);
                    document.Add(separtor);


                    var phraseFecha = new Phrase();
                    string mes = "";
                    switch (DateTime.Now.Month)
                    {
                        case 1: mes = "Enero"; break;
                        case 2: mes = "Febrero"; break;
                        case 3: mes = "Marzo"; break;
                        case 4: mes = "Abril"; break;
                        case 5: mes = "Mayo"; break;
                        case 6: mes = "Junio"; break;
                        case 7: mes = "Julio"; break;
                        case 8: mes = "Agosto"; break;
                        case 9: mes = "Septiembre"; break;
                        case 10: mes = "Octubre"; break;
                        case 11: mes = "Noviembre"; break;
                        case 12: mes = "Diciembre"; break;
                    }
                    phraseFecha.Add("Se expide esta certificación el " + fechaRes + ", en Bogotá D.C.");
                    document.Add(phraseFecha);
                    document.Add(separtor);

                    var phraseCod2 = new Phrase();
                    phraseCod2.Add(new Chunk("Código de validación: " + myProject.codigo_validacion, boldFont));
                    document.Add(phraseCod2);
                    document.Add(Chunk.NEWLINE);


                    var phraseSep2 = new Phrase();
                    phraseSep2.Add(new Chunk("_______________________________________________________________________________________________________________", fontPie));
                    document.Add(phraseSep2);
                    document.Add(Chunk.NEWLINE);

                    fontPie = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLUE);
                    var phraseCod = new Phrase();
                    phraseCod.Add(new Chunk("Puede validar el certificado con el código en la url https://cineproducto.mincultura.gov.co/ValidarCertificadoProducto.aspx ", fontPie));
                    document.Add(phraseCod);
                    document.Add(Chunk.NEWLINE);



                    document.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    if (myProject.state_id == 9 && (myProject.ruta_certificado == null || myProject.ruta_certificado == string.Empty))
                    {
                        System.Configuration.AppSettingsReader ar = new System.Configuration.AppSettingsReader();
                        string pathArchivosPermanente = ar.GetValue("pathArchivosPermanente", typeof(string)).ToString();
                        string ruta = Server.MapPath("~/" + pathArchivosPermanente + "/");
                        if (!Directory.Exists(ruta))
                        {
                            Directory.CreateDirectory(ruta);
                        }
                        ruta = ruta + fileName;
                        System.IO.File.WriteAllBytes(ruta, bytes);

                        myProject.ruta_certificado = fileName;

                        NegocioCineProducto neg1 = new NegocioCineProducto();
                        neg1.ActualizarRutaCertificadoProject(myProject);

                        //return ruta;

                    }

                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.ContentType = "application/pdf";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.End();
                    Response.Close();

                }

            }
        }


        protected void grdDevDatos_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "project_id")
            {
                int project_id = int.Parse(e.CommandArgs.CommandArgument.ToString());
                VerCertificado(project_id);
            }

        }

        protected void btnDesistidosAviso_Click(object sender, EventArgs e)
        {

            NegocioCineProducto neg = new NegocioCineProducto();
            List<project> projecsTToAclarate = neg.getProjectByState(5);//5 es estado aclaracion soliticada

            Calendario cal = new Calendario();
            string codDesistidos = "";
            foreach (project unProject in projecsTToAclarate)
            {
                if (unProject.project_clarification_request_date != null) {
                    DateTime fechaLimiteProject = cal.SumarDiasLaborales(unProject.project_clarification_request_date, 10); // 15 dias laborales
                    if (fechaLimiteProject < DateTime.Now)
                    { codDesistidos += unProject.project_id.ToString() + ", ";
                        //enviar alerta de desistido
                        enviarAvisoNotificacionDesistido(unProject.project_id);
                    }
                }
            }

            if (codDesistidos != "")
                lblDesistidos.Text = "Se notificaron para desistidos los proyectos con Codigo: " + codDesistidos;
            else
                lblDesistidos.Text = "No se encontraron proyectos para notificar como desistidos";

        }

        public void enviarAvisoNotificacionDesistido(int project_id)
        {
            Project project = new Project();
            project.LoadProject(project_id);
            string mailTo = "";
            string subject = "Su solicitud de la obra " + project.project_name + " sera pasada a estado DESISTIDA";
            int RequesterProducerTemp = project.producer.FindIndex(
                delegate (Producer producerObj)
                {
                    return producerObj.requester == 1;
                });
            if (RequesterProducerTemp != -1)
            {
                mailTo = project.producer[RequesterProducerTemp].producer_email;
                //producerName = project.producer[RequesterProducerTemp].producer_firstname + " " + project.producer[RequesterProducerTemp].producer_lastname;
            }

            string body = @"Estimado productor: 
                           </br></br>
                           <p style='text-align:justify;'>
                           Con fundamento en el artículo 2.10.1.4 del Decreto 1080 de 2015 modificado por el Decreto 525 de 2021, le informamos que su solicitud a la fecha tiene pendiente aclaraciones por contestar, tenga en cuenta que, en caso de no contestar las aclaraciones en el tiempo establecido su solicitud se <u>considerará DESISTIDA</u>
                            <br />
                        <br />
                        Si desea evaluar nuestro servicio lo invitamos a diligenciar una breve encuesta en el siguiente enlace <a href='https://forms.office.com/r/nnZ7UHd6kU'>Satisfacción Tramite en Línea</a>
                            </p>

                           ";
            project.sendMailNotification(mailTo, subject, body, Server);
        }
    }
}