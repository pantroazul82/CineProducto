using CineProducto.Bussines;
using DominioCineProducto.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmGeoReferencial : System.Web.UI.Page
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

        protected bool verVisibleResolucion(object fechaNotificacion)
        {
            if (fechaNotificacion == null || fechaNotificacion == System.DBNull.Value || fechaNotificacion.ToString().Trim() == string.Empty)
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

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarDAtos();
            pnlFiltros.Visible = false;
            pnlGrilla.Visible = true;
        }
        public void cargarDAtos()
        {
            System.Data.DataTable tb = ejecutarConsulta();
            grdDatosdev.DataSource = tb;
            lblregistros.Text = "Total Registos:" + tb.Rows.Count;
            grdDatosdev.DataBind();
        }

        public System.Data.DataTable ejecutarConsulta()
        {
            lblError.Text = "";

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
            string filtro = "";
            string filtroPresupuesto = "";
            string filtroFecha = "";
            int temp = 0;
            if (txtDesde.Text.Trim() != string.Empty && int.TryParse(txtDesde.Text, out temp))
            {
                filtroPresupuesto = " and ([project_total_cost_preproduccion]+[project_total_cost_produccion]+[project_total_cost_posproduccion]+[project_total_cost_promotion]>=" + temp + ")";
            }

            if (txtHasta.Text.Trim() != string.Empty && int.TryParse(txtHasta.Text, out temp))
            {
                if (filtroPresupuesto != string.Empty)
                {
                    filtroPresupuesto += " and (" + filtroPresupuesto.Replace("and", "") + " and ([project_total_cost_preproduccion]+[project_total_cost_produccion]+[project_total_cost_posproduccion]+[project_total_cost_promotion]<=" + temp + "))";
                }
                else
                {
                    filtroPresupuesto = " and ([project_total_cost_preproduccion]+[project_total_cost_produccion]+[project_total_cost_posproduccion]+[project_total_cost_promotion]<=" + temp + ")";
                }
            }

            if (fi != string.Empty && ff != string.Empty)
            {
                filtroFecha = " OR p.project_request_date between '" + fi + @"' and '" + ff + @"'";
            }
            else if (fi != string.Empty)
            {
                filtroFecha = " OR p.project_request_date >= '" + fi + "'";
            }
            else if (ff != string.Empty)
            {
                filtroFecha = " OR p.project_request_date <= '" + ff + @"'";
            }

            if (rol <= 1)
            {
                //filtroRol = "(p.project_idusuario = '" + Session["user_id"] + "') and ";
                //grdDAtos.Columns[1].Visible = false;
                //grdDAtos.Columns[2].Visible = false;
                Response.Redirect("~/default.aspx");
            }




            string sql = @"
set dateformat dmy
select p.project_id, p.project_name Titulo,[Titulo_provisional],
production_type.production_type_name 'Tipo_Produccion',
project_genre.project_genre_name 'Tipo_Obra',
project_type.project_type_name 'Clasificacion_Duracion',
[project_total_cost_desarrollo] Costo_desarrollo,
idioma.nombre_idioma Idioma,case when producer.person_type_id =2 then 
producer.producer_name else producer.producer_firstname +' '+producer.producer_lastname end
'Productor', 
person_type.person_type_name 'Tipo_persona_productor',producer_type.producer_type_name 'Tipo_Productor',
[project_total_cost_preproduccion] Costo_Total_Preproduccion,[project_total_cost_produccion] Costo_Produccion,
[project_total_cost_posproduccion] Costo_Posproduccion,[project_total_cost_promotion] Costo_Promocion,
[project_filming_start_date] Inicio_Filmacion,[project_filming_end_date] Fin_Filmacion,
[project_legal_deposit] Deposito_Legal,[project_percentage] Porcentaje_Colombiano,
s.state_name Estado, p.project_request_date Fecha_Solicitud, 
p.project_clarification_request_date Fecha_Solicitud_Aclaraciones, 
p.project_clarification_response_date Fecha_Envio_Aclaraciones, 
p.project_resolution_date Fecha_Resolucion,project_notification_date Fecha_Notificacion,
 p.state_id,localization.localization_name,l2.localization_name depto
from project p 
left join resolution  r on r.project_id= p.project_id 
left join state s on p.state_id = s.state_id 
join usuario on usuario.idusuario = p.project_idusuario
left join project_producer on project_producer.project_id = p.project_id and project_producer.project_producer_requester=1
left join producer on producer.producer_id = project_producer.producer_id
left join producer_type on producer_type.producer_type_id = producer.producer_type_id
left join person_type on person_type.person_type_id = producer.person_type_id
left join idioma on idioma.cod_idioma = p.cod_idioma
left join project_genre on project_genre.project_genre_id = p.project_genre_id
left join production_type on production_type.production_type_id = p.production_type_id
left join project_type on project_type.project_type_id = p.project_type_id
 join localization on localization.localization_id = producer.producer_localization_id
 join localization l2 on l2.localization_id = localization.localization_father_id
";

            string filtroSQL = @" where producer_localization_id is not null AND
(" + filtro + @" (p.project_request_date is null " + filtroFecha + @")
     and p.project_name like '%" + txtTitulo.Text.Trim().Replace("'", "%") + @"%' 
     and s.state_id >1 and s.state_id in (" + lblEstados.Text + @") 
     and (p.production_type_id = '" + cmbTipoProduccion.SelectedValue + "' or  " + cmbTipoProduccion.SelectedValue + @"='-1') 
     and (p.project_genre_id = '" + cmbTipoObra.SelectedValue + "' or  " + cmbTipoObra.SelectedValue + @"='-1') 
     and (producer.producer_name like '%" + txtProductor.Text + "%' or  producer.producer_firstname like '%" + txtProductor.Text + "%' or producer.producer_lastname like '%" + txtProductor.Text + @"%' ) 
     and (producer.person_type_id = '" + cmbTipoProductor.SelectedValue + "' or  " + cmbTipoProductor.SelectedValue + @"='-1') 
     and (p.project_type_id = '" + cmbDuracion.SelectedValue + "' or  " + cmbDuracion.SelectedValue + @"='-1') 
     " + filtroPresupuesto + ")";

            if (cmbEstado.SelectedValue == "15")
            {
                filtroSQL = filtroSQL + " and (project_notification_date > getdate() OR (project_notification_date is null and p.project_resolution_date is not null))";
            }

string filtroMapa = @"set dateformat dmy
select
l2.localization_name,
SUM(1) TOTAL,
sum(case when project_genre.project_genre_ID = 3 then 1 else 0 end) CNT_ANIMACION,
sum(case when project_genre.project_genre_ID = 2 then 1 else 0 end) CNT_DOCUMENTAL,
sum(case when project_genre.project_genre_ID = 1 then 1 else 0 end) CNT_FICCION,
sum(case when production_type.production_type_id = 1 then 1 else 0 end) CNT_PRODUCCION,
sum(case when production_type.production_type_id = 2 then 1 else 0 end) CNT_COPRODUCCION,
sum(case when project_type.project_type_id != 3 then 1 else 0 end) CNT_LARGO,
sum(case when project_type.project_type_id = 3 then 1 else 0 end) CNT_CORTO,
max(l2.localization_x_coord) localization_x_coord,
max(l2.localization_y_coord) localization_y_coord
from project p 
left join resolution  r on r.project_id= p.project_id 
left join state s on p.state_id = s.state_id 
join usuario on usuario.idusuario = p.project_idusuario
left join project_producer on project_producer.project_id = p.project_id and project_producer.project_producer_requester=1
left join producer on producer.producer_id = project_producer.producer_id
left join producer_type on producer_type.producer_type_id = producer.producer_type_id
left join person_type on person_type.person_type_id = producer.person_type_id
left join idioma on idioma.cod_idioma = p.cod_idioma
join project_genre on project_genre.project_genre_id = p.project_genre_id
join production_type on production_type.production_type_id = p.production_type_id
join project_type on project_type.project_type_id = p.project_type_id
 join localization on localization.localization_id = producer.producer_localization_id
 join localization l2 on l2.localization_id = localization.localization_father_id
 " + filtroSQL+@" 
group by l2.localization_name";


            lblregistros.Text = "";
            sql = sql +filtroSQL +" order by " + lblSort.Text + " " + lblSortDirection.Text;
            DB db = new DB();
            System.Data.DataTable tb = db.Select(sql).Tables[0];

            hdValores.Value="[";
            sql = filtroMapa;
            System.Data.DataTable tbmAPA = db.Select(sql).Tables[0];
            for(int k=0;k<tbmAPA.Rows.Count;k++)
            {
            string obj="{"+
                            "\"cnt_cortos\":" + tbmAPA.Rows[k]["CNT_CORTO"] + "," +
                            "\"cnt_largos\":" + tbmAPA.Rows[k]["CNT_LARGO"] + "," +
                            "\"cnt_ficcion\":" + tbmAPA.Rows[k]["CNT_FICCION"] + "," +
                            "\"cnt_documental\":" + tbmAPA.Rows[k]["CNT_DOCUMENTAL"] + "," +
                            "\"cnt_animacion\":" + tbmAPA.Rows[k]["CNT_ANIMACION"] + "," +
                            "\"cnt_produccion\":" + tbmAPA.Rows[k]["CNT_PRODUCCION"] + "," +
                            "\"cnt_coproduccion\":" + tbmAPA.Rows[k]["CNT_COPRODUCCION"] + "," +
                            "\"cnt_total\":" + tbmAPA.Rows[k]["TOTAL"] + "," +
                            "\"nombre_ciudad\":\"" + tbmAPA.Rows[k]["localization_name"] + "\"," +
                            "\"LATITUD\":" + tbmAPA.Rows[k]["localization_x_coord"].ToString().Replace(",",".") + "," +
                            "\"LONGITUD\":" + tbmAPA.Rows[k]["localization_y_coord"].ToString().Replace(",", ".") + "" +            
                "},";

                hdValores.Value = hdValores.Value+obj;
            }
            hdGenerarMapa.Value = "1";
            hdValores.Value= hdValores.Value.Substring(0,hdValores.Value.Length-1)+"]";

            return tb;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role_id"] == null || int.Parse(Session["user_role_id"].ToString()) <= 1)
            {
                Response.Redirect("~/default.aspx");
            }
            /* Verificamos si existe un usuario autenticado o de lo contrario lo redirigimos a la página inicial */
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                DB db = new DB();


                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(Session["user_id"]);
                int rol = (int)Session["user_role_id"];
                if (!IsPostBack)
                {
                    int user_role = userObj.GetUserRole(userObj.user_id);
                    txtInicio_CalendarExtender.SelectedDate = DateTime.Now.AddMonths(-18);
                    txtFin_CalendarExtender.SelectedDate = DateTime.Now.AddDays(1);
                    /*
    1	Creada          2	Enviada         3	Revisada        4	Editada
5	Aclaraciones solicitadas            6	Aclaraciones enviadas               7	Aclaraciones revisadas
8	Aclaraciones Editadas               9	Aprobada        10	Rechazada       11	Sin Definir                     12	Cancelada
    */
                    //1 solicitnte 2 revisor 3 editor 4 director

                    string codigos = "1,2,3,4,5,6,7,8,9,10,11,12";

                    switch (rol)
                    {
                        case 0: { codigos = "1,2,5,6,9,10,12"; break; }
                        case 1: { codigos = "1,2,5,6,9,10,12"; break; }
                        case 2: { codigos = "2,3,4,5,6,7,8,9,10,12"; break; }
                        case 3: { codigos = "2,3,4,5,6,7,8,9,10,12"; break; }
                        case 4: { codigos = "2,3,4,5,6,7,8,9,10,12"; break; }
                    }

                    if (Session["superadmin"] != null)
                    {
                        codigos = "1,2,3,4,5,6,7,8,9,10,12";
                    }

                    cmbEstado.DataSource = db.Select("select state_id,state_name from state where state_deleted=0 and state_id in (" + codigos + ")").Tables[0];
                    cmbEstado.AppendDataBoundItems = true;
                    cmbEstado.DataValueField = "state_id";
                    cmbEstado.DataTextField = "state_name";
                    cmbEstado.DataBind();

                    if (rol < 1)
                    {
                        cmbEstado.Items.Insert(5, new ListItem("Por Notificar", "15"));
                    }
                    else
                    {
                        cmbEstado.Items.Insert(1, new ListItem("En estudio (Integrado)", "16"));
                        cmbEstado.Items.Insert(1, new ListItem("Finalizadas", "17"));
                    }

                    lblEstados.Text = "'2','3','4','5','6','7','8','9','10','12'";
                    if (Session["superadmin"] != null)
                    {
                        lblEstados.Text = "'1','2','3','4','5','6','7','8','9','10','12'";
                    }
                }
                else
                {
                    if (cmbEstado.SelectedValue == "-1")
                    {
                        if (rol > 1)
                        {
                            lblEstados.Text = "'2','3','4','5','6','7','8','9','10','12'";
                        }
                        else
                        {
                            lblEstados.Text = "'1','2','3','4','5','6','7','8','9','10','12'";
                        }
                        if (Session["superadmin"] != null)
                        {
                            lblEstados.Text = "'1','2','3','4','5','6','7','8','9','10','12'";
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
                            if (cmbEstado.SelectedValue.Trim() == "17")
                            {
                                lblEstados.Text = "9,10";
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

                    cargarDAtos();
                }
            }
            else
            {
                Response.Redirect("Default.aspx", true);
            }
        }



        protected void txtInicio_TextChanged(object sender, EventArgs e)
        {
            if (txtInicio.Text.Trim() == string.Empty) return;
            int dia = int.Parse(txtInicio.Text.Substring(0, 2));
            int mes = int.Parse(txtInicio.Text.Substring(3, 2));
            int ano = int.Parse(txtInicio.Text.Substring(6, 4));
            txtInicio_CalendarExtender.SelectedDate = new DateTime(ano, mes, dia);

        }

        protected void txtFin_TextChanged(object sender, EventArgs e)
        {
            if (txtFin.Text.Trim() == string.Empty) return;
            int dia = int.Parse(txtFin.Text.Substring(0, 2));
            int mes = int.Parse(txtFin.Text.Substring(3, 2));
            int ano = int.Parse(txtFin.Text.Substring(6, 4));
            txtFin_CalendarExtender.SelectedDate = new DateTime(ano, mes, dia);
        }

        protected void grdDAtos_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression != lblSort.Text)
            {
                lblSort.Text = e.SortExpression;
                lblSortDirection.Text = "asc";

            }
            else
            {
                if (lblSortDirection.Text == "asc")
                {
                    lblSortDirection.Text = "desc";
                }
                else
                {
                    lblSortDirection.Text = "asc";
                }
            }


            cargarDAtos();
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
            xls.generarExcel(Server.MapPath("~/temp/"), "Reporte Maestro",
                ejecutarConsulta(), Response);
            // Server.MapPath 
        }

        protected void btnCAmbiarFiltros_Click(object sender, EventArgs e)
        {
            pnlFiltros.Visible = true;
            pnlGrilla.Visible = false;
        }




    }
}