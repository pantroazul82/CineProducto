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
    public partial class frmEditarCoproductor : System.Web.UI.Page
    {
        public int project_state = 0;
        public int user_role = 0;
        protected void Page_Init(object sender, EventArgs e)
        {            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblViewProductorSeleccionado.Text = Request.QueryString["project_producer_id"];
                NegocioCineProducto neg = new NegocioCineProducto();
                project_producer pp = neg.getProjectProducerById(int.Parse(lblViewProductorSeleccionado.Text));
                lblIdProducer.Text = pp.producer_id.ToString();
                cargarForm();                  
            }


            User userObj = new User();
            userObj.user_id = Convert.ToInt32(Session["user_id"]);
            this.user_role = userObj.GetUserRole(userObj.user_id);

            ASPxGridView1.DataBind();

        }
        public void cargarForm() {
            NegocioCineProducto neg = new NegocioCineProducto();
            project_producer objP = neg.getProjectProducerById(int.Parse(lblViewProductorSeleccionado.Text));
            llenarTipoEmpresa(objP.producer.producer_type_id);

            cmbDepartamento.DataBind();
            cmbDepartamentoContacto.DataBind();
            cmbEtnia.DataBind();
            cmbGenero.DataBind();
            cmbGrupoPoblacional.DataBind();            
            cmbTipoDocumentoRep.DataBind();
            cmbTipoDocumentoRepLegalSup.DataBind();
            cmbTipoEmpresa.DataBind();
            cmbTipoProductor.DataBind();
            cmbPais.DataBind();

            txtPorcentajeParticipacion.Text = objP.project_producer_participation_percentage.ToString();
            cmbTipoProductor.SelectedValue = objP.producer.person_type_id.ToString();

            txtTelefono.Text = objP.producer.producer_phone;
            txtTelefonoAlternativo.Text = objP.producer.producer_movil;
            txtEmail.Text = objP.producer.producer_email;


            if (objP.producer.producer_type_id == 1)
            {  //1 es colombiano
                pnDatosOrigenExt.Visible = false;
                pnlPaisContacto.Visible = false;
                upDatosOrigenNal.Visible = true;
                pnPNaturalNal.Visible = true;
                if (objP.producer.producer_localization_id != string.Empty && objP.producer.producer_localization_id != null)
                  cmbDepartamento.SelectedValue = objP.producer.localization.localization_father_id;
                cmbMunicipio.DataBind();
                cmbMunicipio.SelectedValue = objP.producer.producer_localization_id;

                if (objP.producer.productor_localizacion_contacto_id != null)
                {
                    if (objP.producer.productor_localizacion_contacto_id.Trim() != string.Empty)
                    {
                        localization objL = neg.getDepartamentobyId(objP.producer.productor_localizacion_contacto_id);
                        cmbDepartamentoContacto.SelectedValue = objL.localization_father_id;

                        cmbMunicipioContacto.DataBind();
                        cmbMunicipioContacto.SelectedValue = objP.producer.producer_localization_id;
                    }
                }
                    


                txtCedulaCiudadania.ToolTip = "";
                txtNIT.ToolTip = "NIT de acuerdo con la camara de comercio";
                txtNitDigVerificacion.ToolTip = "Digito de verificacion del NIT de acuerdo con la camara de comercio"; ;
            }
            else {
                pnDatosOrigenExt.Visible = true;
                pnlPaisContacto.Visible = true;
                upDatosOrigenNal.Visible = false;
                pnPNaturalNal.Visible = false;
                lblCedulaCiudadania.Text = "ID *: ";
                lblNit.Text = "ID *: ";
                lblNumDocumentoRepLegal.Text = "ID Representante Legal *: ";
                lblNumDocumentoRepLegalSup.Text = "ID Representante Legal Suplente: ";
                //txtPais.Text= objP.producer.producer_country;
                cmbPais.SelectedValue = objP.producer.producer_country;
                cmbPaisContacto.SelectedValue = objP.producer.productor_pais_contacto;
                txtCiudad.Text = objP.producer.producer_city;
                txtCiudadContacto.Text = objP.producer.productor_ciudad_contacto;
                txtNitDigVerificacion.Visible = false;
                RequiredFieldValidatorNitDigVerif.Enabled = false;
                lblSeparadorNIt.Visible = false;
                RangeValidatorNumDocRepLegal.Enabled = false;
                rangeCedulaCiudadania.Enabled = false;
                RangeValidator3.Enabled = false; // es el nit cuando es extranjero se anula
                RangeValidator5.Enabled = false;

            }

            txtFechaNacimiento.MinDate = DateTime.Parse("1870-01-01");
            txtFechaNacimiento.MaxDate = DateTime.Now;

            if (cmbTipoProductor.SelectedValue == "1")  //cmbTipoProductor es tipo de persona
            {
                txtPrimerNombre.Text = objP.producer.producer_firstname;
                txtSegundoNombre.Text = objP.producer.producer_firstname2;
                txtPrimerApellido.Text = objP.producer.producer_lastname;
                txtSegundoApellido.Text = objP.producer.producer_lastname2;
                txtCedulaCiudadania.Text = objP.producer.producer_identification_number;
                cmbGenero.SelectedValue = objP.producer.id_genero.ToString();
                if(objP.producer.fecha_nacimiento != null)
                  txtFechaNacimiento.Date = DateTime.Parse(objP.producer.fecha_nacimiento.ToString());              

                cmbGrupoPoblacional.SelectedValue = objP.producer.id_grupo_poblacional.ToString();
                cmbEtnia.SelectedValue = objP.producer.id_etnia.ToString();
            }
            if (cmbTipoProductor.SelectedValue == "2")  //cmbTipoProductor es tipo de persona
            {
                txtPrimerNombreRep.Text= objP.producer.producer_firstname;
                txtSegundoNombreRep.Text= objP.producer.producer_firstname2;
                txtPrimerApellidoRep.Text= objP.producer.producer_lastname;
                txtSegundoApellidoRep.Text= objP.producer.producer_lastname2;
                txtNumDocumentoRepLegal.Text= objP.producer.producer_identification_number;
                cmbTipoDocumentoRep.SelectedValue= objP.producer.identification_type_id.ToString();
                txtRazonSocial.Text = objP.producer.producer_name;
                txtNIT.Text = objP.producer.producer_nit;
                txtNitDigVerificacion.Text= objP.producer.producer_nit_dig_verif.ToString();
                cmbTipoEmpresa.SelectedValue= objP.producer.producer_company_type_id.ToString();
                txtPrimerNombreRepSup.Text = objP.producer.primer_nombre_sup;
                txtSegundoNombreRepSup.Text = objP.producer.segundo_nombre_sup;
                txtPrimerApellidoRepSup.Text = objP.producer.primer_apellido_sup;
                txtSegundoApellidoRepSup.Text = objP.producer.segundo_apellido_sup;
                txtNumDocumentoRepLegalSup.Text = objP.producer.num_id_sup;
                cmbTipoDocumentoRepLegalSup.SelectedValue = objP.producer.identification_type_id_sup.ToString();
                txtAbreviatura.Text = objP.producer.abreviatura;
            }
            verificarTipoPersona();
                       
            

        }

        public void llenarTipoEmpresa(int? producer_type_id) {

            NegocioCineProducto neg = new NegocioCineProducto();
            List<producer_company_type> lista = neg.getProducerCompany();
            foreach (producer_company_type pc in lista) {
                ListItem i = new ListItem();
                i.Value = pc.producer_company_type_id.ToString();
                i.Text = pc.producer_company_type_name;
                if (producer_type_id == 1 && pc.producer_company_type_id != 7)
                {
                    cmbTipoEmpresa.Items.Add(i);
                }
                if (producer_type_id == 2 && pc.producer_company_type_id == 7) {
                    cmbTipoEmpresa.Items.Add(i);
                }

            }
            //cmbTipoEmpresa.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblErrorCoproductor.Text = "";
            lblErrorCoproductor.Visible = false;
            NegocioCineProducto neg = new NegocioCineProducto();
            project_producer objPp = neg.getProjectProducerById(int.Parse(lblViewProductorSeleccionado.Text));

            //objPp.project_producer_participation_percentage = Double.Parse(txtPorcentajeParticipacion.Text);
            objPp.project_producer_participation_percentage = Double.Parse(txtPorcentajeParticipacion.Text.Replace(",", ".").Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
            
            producer objP = new producer();
            objP.producer_id = objPp.producer_id;
            objP.person_type_id = int.Parse(cmbTipoProductor.SelectedValue); //cmbTipoProductor es tipo de persona


            if (objPp.producer.producer_type_id == 1) { //1 es colombiano
                objP.producer_localization_id = cmbMunicipio.SelectedValue;
                objP.productor_localizacion_contacto_id = cmbMunicipioContacto.SelectedValue;
            }
            else {
                objP.producer_country = cmbPais.SelectedValue;
                objP.productor_pais_contacto = cmbPaisContacto.SelectedValue;
                objP.producer_city = txtCiudad.Text;
                objP.productor_ciudad_contacto = txtCiudadContacto.Text;
            }            

            objP.producer_phone = txtTelefono.Text;
            objP.producer_movil = txtTelefonoAlternativo.Text;
            objP.producer_email =  txtEmail.Text;

            //if(objPp.producer.producer_type_id == 1)  colombiano o extranjero


            if (cmbTipoProductor.SelectedValue == "1")  //cmbTipoProductor es tipo de persona natural
            {
                objP.producer_firstname = StringExtensors.ToNombrePropio(txtPrimerNombre.Text).ToUpper();
                objP.producer_firstname2 = StringExtensors.ToNombrePropio(txtSegundoNombre.Text).ToUpper();
                objP.producer_lastname = StringExtensors.ToNombrePropio(txtPrimerApellido.Text).ToUpper();
                objP.producer_lastname2 = StringExtensors.ToNombrePropio(txtSegundoApellido.Text).ToUpper();
                objP.producer_identification_number = txtCedulaCiudadania.Text;
                if (objP.producer_identification_number.ToString() == string.Empty)
                {

                }
                objP.id_genero = int.Parse(cmbGenero.SelectedValue);
                objP.fecha_nacimiento = txtFechaNacimiento.Date;
                if(cmbGrupoPoblacional.SelectedValue != string.Empty)
                objP.id_grupo_poblacional = int.Parse(cmbGrupoPoblacional.SelectedValue);
                if (cmbEtnia.SelectedValue != string.Empty)
                    objP.id_etnia = int.Parse(cmbEtnia.SelectedValue);
               

                //coloca en vacio los otros campos
                objP.identification_type_id = null;
                objP.producer_name = string.Empty;
                objP.producer_nit = string.Empty;
                objP.producer_nit_dig_verif = null;
                objP.producer_company_type_id = null;
                objP.primer_nombre_sup = string.Empty;
                objP.segundo_nombre_sup = string.Empty;
                objP.primer_apellido_sup = string.Empty;
                objP.segundo_apellido_sup = string.Empty;
                objP.num_id_sup = string.Empty;
                objP.identification_type_id_sup = null;

            }
            if (cmbTipoProductor.SelectedValue == "2")  //cmbTipoProductor es tipo de persona
            {
                objP.producer_firstname = StringExtensors.ToNombrePropio(txtPrimerNombreRep.Text).ToUpper();
                objP.producer_firstname2 = StringExtensors.ToNombrePropio(txtSegundoNombreRep.Text).ToUpper();
                objP.producer_lastname = StringExtensors.ToNombrePropio(txtPrimerApellidoRep.Text).ToUpper();
                objP.producer_lastname2 = StringExtensors.ToNombrePropio(txtSegundoApellidoRep.Text).ToUpper();
                objP.producer_identification_number = txtNumDocumentoRepLegal.Text;
                objP.identification_type_id = int.Parse(cmbTipoDocumentoRep.SelectedValue);
                objP.producer_name = StringExtensors.ToNombrePropio(txtRazonSocial.Text).ToUpper();
                objP.producer_nit = txtNIT.Text;
                if(txtNitDigVerificacion.Text != string.Empty)
                  objP.producer_nit_dig_verif = int.Parse(txtNitDigVerificacion.Text);
                objP.producer_company_type_id = int.Parse(cmbTipoEmpresa.SelectedValue);
                objP.primer_nombre_sup = StringExtensors.ToNombrePropio(txtPrimerNombreRepSup.Text).ToUpper();
                objP.segundo_nombre_sup = StringExtensors.ToNombrePropio(txtSegundoNombreRepSup.Text).ToUpper();
                objP.primer_apellido_sup = StringExtensors.ToNombrePropio(txtPrimerApellidoRepSup.Text).ToUpper();
                objP.segundo_apellido_sup = StringExtensors.ToNombrePropio(txtSegundoApellidoRepSup.Text).ToUpper();
                if(cmbTipoDocumentoRepLegalSup.SelectedValue != string.Empty)
                objP.identification_type_id_sup = int.Parse(cmbTipoDocumentoRepLegalSup.SelectedValue);

                if (cmbTipoDocumentoRep.SelectedValue == "1")
                {
                    string texto = txtNumDocumentoRepLegal.Text;
                    Regex regex = new Regex("[a-zA-Z]");
                    bool contieneLetras = regex.IsMatch(texto);
                    if (contieneLetras)
                    {
                        lblErrorCoproductor.Text = "La cedula debe ser numerica obligatoriamente";
                        lblErrorCoproductor.Visible = true;
                        return;
                    }
                }
                objP.num_id_sup = txtNumDocumentoRepLegalSup.Text;
                objP.abreviatura= txtAbreviatura.Text;
            }

            neg.actualizarProjectProducer(objPp);
            neg.actualizarProducer(objP);

            Response.Write(@"<script language='javascript'>alert('Información guardada correctamente!');</script>");

        }

        protected void btnAdicionarEstimulo_Click(object sender, EventArgs e)
        {

        }

        protected void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbTipoProductor_SelectedIndexChanged(object sender, EventArgs e)
        {
            verificarTipoPersona();
            eliminarAdjuntos();
            if (cmbTipoProductor.SelectedValue == "1")
            {
                crearAdjuntos(int.Parse(cmbTipoProductor.SelectedValue), 0);
            }
            if (cmbTipoProductor.SelectedValue == "2")
            {
                if(cmbTipoEmpresa.SelectedValue != string.Empty)
                 crearAdjuntos(int.Parse(cmbTipoProductor.SelectedValue), int.Parse(cmbTipoEmpresa.SelectedValue));
            }

            ASPxGridView1.DataBind();
        }
        public void verificarTipoPersona() {
            NegocioCineProducto neg = new NegocioCineProducto();
            project_producer objP = neg.getProjectProducerById(int.Parse(lblViewProductorSeleccionado.Text));
            if (cmbTipoProductor.SelectedValue == "1")
            {
                if (objP.producer.producer_type_id == 1) //Colombiano
                {
                    pnPersonaJuridica.Visible = false;
                    pnPersonaNatural.Visible = true;
                    pnlCiudadContacto.Visible = true;
                    pnlDepMunNacimiento.Visible = true;
                }
                else
                {
                    pnPersonaJuridica.Visible = false;
                    pnPersonaNatural.Visible = true;
                    pnlCiudadContacto.Visible = false;
                }
                
            }
            else if (cmbTipoProductor.SelectedValue == "2")
            {
                if (objP.producer.producer_type_id == 1) //extranjero natural
                {
                    pnPersonaJuridica.Visible = true;
                    pnPersonaNatural.Visible = false;
                    pnlCiudadContacto.Visible = true;
                    pnlDepMunNacimiento.Visible = false;
                }
                else
                {
                    pnPersonaJuridica.Visible = true; //extranjero juridico
                    pnPersonaNatural.Visible = false;
                    pnlCiudadContacto.Visible = false;
                    pnlDepMunNacimiento.Visible = false;
                }
            }
            else
            {
                pnPersonaJuridica.Visible = false;
                pnPersonaNatural.Visible = false;
                pnlCiudadContacto.Visible = false;
                pnlDepMunNacimiento.Visible = false;
            }

        }

        protected void cmbTipoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //elimina datos adjuntos y los vuelve a crear            
            eliminarAdjuntos();
            crearAdjuntos(int.Parse(cmbTipoProductor.SelectedValue),int.Parse(cmbTipoEmpresa.SelectedValue));
            ASPxGridView1.DataBind();
        }
        public void eliminarAdjuntos() {
            NegocioCineProducto neg = new NegocioCineProducto();
            project_producer objPp = neg.getProjectProducerById(int.Parse(lblViewProductorSeleccionado.Text));                       
            
            if(objPp.project.state_id == 1) ///revisar bien en q estado borrar o poner aviso de confirmacion para q no eliminen los adjuntos
              neg.eliminarProjectAttachmentByProducerId(objPp.producer_id);
        }

        public void crearAdjuntos(int id_tipo_persona, int id_tipo_empresa) {

            NegocioCineProducto neg = new NegocioCineProducto();
            project_producer objPp = neg.getProjectProducerById(int.Parse(lblViewProductorSeleccionado.Text));

            //si es extranjero no crea nada
            if (objPp.producer.producer_type_id == 2) {
                return;
            }

            producer pToUpdate = new producer();
            pToUpdate.producer_id = objPp.producer_id;
            if (id_tipo_empresa > 0)
                pToUpdate.producer_company_type_id = id_tipo_empresa;
            pToUpdate.person_type_id = id_tipo_persona;
            neg.actualizarProducerPersonTipoCompanyTipo(pToUpdate);

            //si es persona natural colombiano crea un adjunto  solo crea el 60
            if (id_tipo_persona == 1) {
                project_attachment pa = new project_attachment();
                pa.project_attachment_project_id = objPp.project_id;
                pa.project_attachment_producer_id = objPp.producer_id;
                pa.project_attachment_attachment_id = 60;
                pa.project_attachment_path = "";
                pa.project_attachment_date = DateTime.Now;
                pa.project_attachment_approved = 0;
                neg.AdicionarProjectAttachment(pa);
            }
            //si es persona juridica depende del tipo de empresa
            
            if (id_tipo_persona == 2 )
            {
                var tipos_empresa = new List<int>();
               

                if (id_tipo_empresa == 1 || id_tipo_empresa == 4)
                { //EU LTDA = 47,44
                    tipos_empresa.Add(44);
                    tipos_empresa.Add(47);
                }
                else {
                    //Egubernamental ESAL SA SAS = 47,44,54,48
                    tipos_empresa.Add(44);
                    tipos_empresa.Add(47);
                    tipos_empresa.Add(48);
                    tipos_empresa.Add(54);
                }

                foreach (int unTipo in tipos_empresa) {
                    project_attachment pa = new project_attachment();
                    pa.project_attachment_project_id = objPp.project_id;
                    pa.project_attachment_producer_id = objPp.producer_id;
                    pa.project_attachment_attachment_id = unTipo;
                    pa.project_attachment_path = "";
                    pa.project_attachment_date = DateTime.Now;
                    pa.project_attachment_approved = 0;
                    neg.AdicionarProjectAttachment(pa);
                }

            }            

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
            project_producer objPp = neg.getProjectProducerById(int.Parse(lblViewProductorSeleccionado.Text));

            ASPxUploadControl b = (ASPxUploadControl)sender;
            string idProjectAttachm = b.AmazonSettings.AccountName;

            project_attachment pat= new project_attachment();
            pat.project_attachment_id = int.Parse(idProjectAttachm);

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") +"_"+ e.UploadedFile.FileName.Replace("+", "").Replace("&", "").Replace("'", "").Replace(" ", "_");

            System.Configuration.AppSettingsReader ar = new System.Configuration.AppSettingsReader();
            string FolderURL = ar.GetValue("FolderURL", typeof(string)).ToString()+"/"+ objPp .project_id.ToString()+ "/"+lblIdProducer.Text+"/";                        
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

        protected void callbackPanel_Callback(object source, CallbackEventArgsBase e)
        {
            //Label1.Text = "File Uploaded " + DateTime.Now.ToLongTimeString();
            
        }

        protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxGridView1.DataBind();
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