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
    public partial class ValidarCertificadoProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text.ToLower() != Session["CaptchaVerify"].ToString())
            {
                lblCaptchaMessage.Text = "El texto no es válido!";
                lblCaptchaMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            NegocioCineProducto neg = new NegocioCineProducto();
            
            project myProject =   neg.getProjectByCodigoValidacion(txtCodigoValidacion.Text);
            if (myProject != null ) 
            {
                lblResultado.Text = "La " + myProject.production_type.production_type_name+ " con codigo de validación "+ txtCodigoValidacion.Text + " se encuentra registrada!!" ;
                txtCodigoValidacion.Text = "";
                txtVerificationCode.Text = "";
                lblCaptchaMessage.Text = "";

                lblSinopsis.Text = myProject.project_synopsis;
                lblTipoObra.Text = myProject.project_type.project_type_name;
                lblTipoProduccion.Text = myProject.production_type.production_type_name;
                lblGenero.Text = myProject.project_genre.project_genre_name;
                
                if (myProject.project_producer.Count() > 0)
                {
                    foreach (project_producer unPp in myProject.project_producer) {
                        if (unPp.producer.producer_type_id == 1)
                        {
                            if (unPp.producer.person_type_id == 1)
                            {
                                lblProductor.Text += unPp.producer.producer_firstname + " " + unPp.producer.producer_lastname+"<br />";
                            }
                            else
                            {
                                lblProductor.Text += unPp.producer.producer_name + "<br />";
                            }
                                
                        }
                        else {
                            if (unPp.producer.person_type_id == 1)
                            {
                                lblCoproductor.Text += unPp.producer.producer_firstname + " " + unPp.producer.producer_lastname + "<br />";
                            }
                            else
                            {
                                lblCoproductor.Text += unPp.producer.producer_name + "<br />";
                            }                            
                        }
                        
                    }
                    
                }
                  
                lblTitulo.Text = myProject.project_name;
                lblFechaInicioRodaje.Text = DateTime.Parse(myProject.project_filming_start_date.ToString()).ToShortDateString();
                lblFechaFinRodaje.Text = DateTime.Parse(myProject.project_filming_end_date.ToString()).ToShortDateString();
                lblDuracion.Text = (myProject.project_duration / 60).ToString()+" minuto(s) y "+ (myProject.project_duration % 60).ToString() + " segundo(s)";
                lblCosto.Text = (myProject.project_total_cost_desarrollo + myProject.project_total_cost_preproduccion + myProject.project_total_cost_produccion + myProject.project_total_cost_posproduccion + myProject.project_total_cost_promotion).ToString();
                

                panelResultado.Visible = true;
                panelConsultar.Visible = false;
            }
            else 
            {
                panelResultado.Visible = true;
                panelConsultar.Visible = false;
                lblResultado.Text = "El código no fue encontrado!!";
                txtCodigoValidacion.Text = "";
            }

        }
    }
}