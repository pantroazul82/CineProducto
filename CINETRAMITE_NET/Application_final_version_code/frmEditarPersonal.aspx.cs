using CineProducto.Bussines;
using DevExpress.Web;
using DominioCineProducto;
using DominioCineProducto.Data;
using DominioCineProducto.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


namespace CineProducto
{
    public partial class frmEditarPersonal : System.Web.UI.Page
    {
        public int project_state = 0;
        public int user_role = 0; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblIdProjectStaff.Text = Request.QueryString["project_staff_id"];
                NegocioCineProducto neg = new NegocioCineProducto();
                cargarForm();
            }

            User userObj = new User();
            userObj.user_id = Convert.ToInt32(Session["user_id"]);                
            this.user_role = userObj.GetUserRole(userObj.user_id);

            
            ASPxGridView1.DataBind();
        }
        public void cargarForm()
        {
            NegocioCineProducto neg = new NegocioCineProducto();
            project p = neg.getProject(Convert.ToInt32(Session["project_id"]));
            this.project_state = Convert.ToInt32(p.state_id);

            project_staff objP = neg.getProjectStaffById(int.Parse(lblIdProjectStaff.Text));

            lblProjectId.Text = objP.project_staff_project_id.ToString();

            lblStaffOptionId.Text = objP.project_staff_position_id.ToString();
            if (objP.position.position_father_id != 0)  {
                lblStaffOptionId.Text = objP.position.position_father_id.ToString();
            }

            cmbCargo.DataBind();

            if (cmbCargo.Items.Count == 1) {
                pnCargo.Visible = false;
            }

           
            cmbDepartamento.DataBind();
            cmbEtnia.DataBind();
            cmbGenero.DataBind();
            cmbGrupoPoblacional.DataBind();
            cmbTipoDocumentoRep.DataBind();

            //txtPorcentajeParticipacion.Text = objP.project_producer_participation_percentage.ToString();

            cmbTipoDocumentoRep.SelectedValue = objP.identification_type_id.ToString();
            txtCedulaCiudadania.Text = objP.project_staff_identification_number;
            txtPrimerNombre.Text = objP.project_staff_firstname;
            txtSegundoNombre.Text = objP.project_staff_firstname2 ;
            txtPrimerApellido.Text = objP.project_staff_lastname;
            txtSegundoApellido.Text = objP.project_staff_lastname2;

            if(objP.fecha_nacimiento != null)
              txtFechaNacimiento.Date = DateTime.Parse(objP.fecha_nacimiento.ToString());

            txtFechaNacimiento.MinDate = DateTime.Parse("1870-01-01");
            txtFechaNacimiento.MaxDate = DateTime.Now;

            cmbGenero.SelectedValue = objP.id_genero.ToString();
            cmbGrupoPoblacional.SelectedValue = objP.id_grupo_poblacional.ToString();
            cmbEtnia.SelectedValue = objP.id_etnia.ToString();            
            txtTelefono.Text = objP.project_staff_phone;
            txtTelefonoAlternativo.Text = objP.project_staff_movil;
            txtEmail.Text = objP.project_staff_email;

            if (objP.project_staff_localization_id != string.Empty && objP.project_staff_localization_id != null)
                cmbDepartamento.SelectedValue = objP.localization.localization_father_id;
            cmbMunicipio.DataBind();
            cmbMunicipio.SelectedValue = objP.project_staff_localization_id;

            if (objP.position.position_father_id != 0)
            {
                cmbCargo.SelectedValue = objP.project_staff_position_id.ToString();                 
            }
            cmbEspecialidad.DataBind();
            if (objP.id_especialidad_cargo != 0)
            {
                cmbEspecialidad.SelectedValue = objP.id_especialidad_cargo.ToString();
            }

            if (neg.getProjectAttachmentByStaffId(objP.project_staff_id).Count == 0) {
                crearAdjuntos();
            }
            else if (neg.getProjectAttachmentByStaffId(objP.project_staff_id).Count == 1)
            {
                crearAdjuntoAdicional(neg.getProjectAttachmentByStaffId(objP.project_staff_id)[0].project_attachment_attachment_id);
            }
            else if (neg.getProjectAttachmentByStaffId(objP.project_staff_id).Count > 2 && p.state_id == 1)
            {
                eliminarAdjuntos();
                crearAdjuntos();
            }

        }
        public void eliminarAdjuntos()
        {
            NegocioCineProducto neg = new NegocioCineProducto();
            neg.eliminarProjectAttachmentByStaffId(int.Parse(lblIdProjectStaff.Text));
        }
        public void crearAdjuntoAdicional(int paai) {
            NegocioCineProducto neg = new NegocioCineProducto();
            project_staff objP = neg.getProjectStaffById(int.Parse(lblIdProjectStaff.Text));

            var tipos_adjunto = new List<int>();
            tipos_adjunto.Add(64);
            tipos_adjunto.Add(65);

            foreach (int unTipo in tipos_adjunto)
            {
                if (paai != unTipo) { 
                    project_attachment pa = new project_attachment();
                    pa.project_attachment_project_id = Convert.ToInt32(Session["project_id"]);
                    pa.project_staff_id = int.Parse(lblIdProjectStaff.Text);
                    pa.project_attachment_attachment_id = unTipo;
                    pa.project_attachment_path = "";
                    pa.project_attachment_date = DateTime.Now;
                    pa.project_attachment_approved = 0;
                    neg.AdicionarProjectAttachment(pa);
                }
            }
        }

        public void crearAdjuntos()
        {

            NegocioCineProducto neg = new NegocioCineProducto();
            project_staff objP = neg.getProjectStaffById(int.Parse(lblIdProjectStaff.Text));

            var tipos_adjunto = new List<int>();
            tipos_adjunto.Add(64);
            tipos_adjunto.Add(65);

            foreach (int unTipo in tipos_adjunto)
            {
                project_attachment pa = new project_attachment();
                pa.project_attachment_project_id = Convert.ToInt32(Session["project_id"]);
                pa.project_staff_id = int.Parse(lblIdProjectStaff.Text);
                pa.project_attachment_attachment_id = unTipo;
                pa.project_attachment_path = "";
                pa.project_attachment_date = DateTime.Now;
                pa.project_attachment_approved = 0;
                neg.AdicionarProjectAttachment(pa);
            }

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            NegocioCineProducto neg = new NegocioCineProducto();
            project_staff objP = neg.getProjectStaffById(int.Parse(lblIdProjectStaff.Text));

            
            objP.identification_type_id = int.Parse(cmbTipoDocumentoRep.SelectedValue);
            objP.project_staff_identification_number = txtCedulaCiudadania.Text;
            objP.project_staff_firstname = StringExtensors.ToNombrePropio(txtPrimerNombre.Text).ToUpper();
            objP.project_staff_firstname2 = StringExtensors.ToNombrePropio(txtSegundoNombre.Text).ToUpper();
            objP.project_staff_lastname = StringExtensors.ToNombrePropio(txtPrimerApellido.Text).ToUpper();
            objP.project_staff_lastname2 = StringExtensors.ToNombrePropio(txtSegundoApellido.Text).ToUpper();
            objP.fecha_nacimiento = txtFechaNacimiento.Date;
            objP.id_genero = int.Parse(cmbGenero.SelectedValue);

            if(cmbEspecialidad.SelectedValue  != null && cmbEspecialidad.SelectedValue != string.Empty)
                objP.id_especialidad_cargo = int.Parse(cmbEspecialidad.SelectedValue);

            if (cmbGrupoPoblacional.SelectedValue != string.Empty)
                objP.id_grupo_poblacional = int.Parse(cmbGrupoPoblacional.SelectedValue);

            if (cmbEtnia.SelectedValue != string.Empty)
                objP.id_etnia = int.Parse(cmbEtnia.SelectedValue);

            objP.project_staff_localization_id = cmbMunicipio.SelectedValue;
            objP.project_staff_phone = txtTelefono.Text;
            objP.project_staff_movil = txtTelefonoAlternativo.Text;
            objP.project_staff_email = txtEmail.Text;
            if(cmbCargo.SelectedValue != string.Empty)
            objP.project_staff_position_id = int.Parse(cmbCargo.SelectedValue);

            neg.actualizarProjectStaff(objP);

            Response.Write(@"<script language='javascript'>alert('Información guardada correctamente!');</script>");
            
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

        protected void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void upload_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string fileName1 = e.UploadedFile.FileName;
            // Realiza la validación de caracteres especiales en el nombre del archivo
            if (!Regex.IsMatch(fileName1, "^[a-zA-Z0-9\\s\\.,\\-_]+$"))
            {
                e.IsValid = false;
                e.ErrorText = "¡El nombre del archivo no debe contener caracteres especiales: #@+(){}°~“´´%&!";
                return;
            }
            NegocioCineProducto neg = new NegocioCineProducto();
            project_staff objP = neg.getProjectStaffById(int.Parse(lblIdProjectStaff.Text));

            ASPxUploadControl b = (ASPxUploadControl)sender;
            string idProjectAttachm = b.AmazonSettings.AccountName;

            project_attachment pat = new project_attachment();
            pat.project_attachment_id = int.Parse(idProjectAttachm);

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + e.UploadedFile.FileName.Replace("+", "").Replace("&", "").Replace("'", "").Replace(" ", "_");

            System.Configuration.AppSettingsReader ar = new System.Configuration.AppSettingsReader();
            string FolderURL = ar.GetValue("FolderURL", typeof(string)).ToString() + "/" + objP.project_staff_project_id.ToString() + "/" + lblIdProjectStaff.Text + "/";
            string ruta = Server.MapPath("~/" + FolderURL + "/");
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
            ruta = ruta + fileName;
            e.UploadedFile.SaveAs(ruta); //Server.MapPath(ruta)

            pat.project_attachment_path = FolderURL + fileName;
            pat.project_attachment_date = DateTime.Now;
            pat.nombre_original = e.UploadedFile.FileName;


            neg.ActualizarProjectAttachmentUrl(pat);

        }

        protected void cmbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}