using DominioCineProducto.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmDatosAdjuntos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //formulario creado para poder hacer soporte por CG, ya que no se tiene acceso al servidor
        }

        protected void btnCargarARchivo_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = Server.MapPath("~/uploads/");
                FileUpload1.SaveAs(ruta + txtRuta.Text + "/" + txtNombreARchivo.Text);
                lblError.Text = "Archivo cargado satisfactoriamente.";
            }catch(Exception ex){
                lblError.Text = "Error al cagar "+ex.Message;
            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            string ruta = Server.MapPath("~/uploads/");
            if (System.IO.File.Exists(ruta + txtDescargar.Text))
            {
                Response.Redirect("~/uploads/" + txtDescargar.Text);
            }
            else {
                lblError.Text = "no existe el archivo en el servidor";
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Electra69")
            {
                pnlDOs.Visible = true;
                pnlUno.Visible = false;
            }
        }

       public class adjuntoTmp {
            public string nombre { set; get; }
            public string url { set; get; }
        }

        protected void btnVerificarADjuntos_Click(object sender, EventArgs e)
        {
            string ruta=Server.MapPath(txtVerificarAdjuntos.Text);

            
            string[] arr= System.IO.Directory.GetFiles(ruta);

            List<adjuntoTmp> lista = new List<adjuntoTmp>();
            for (int k = 0; k < arr.Length; k++)
            {
                adjuntoTmp a = new adjuntoTmp();
                a.nombre = arr[k];
                a.url = "~/"+txtVerificarAdjuntos.Text+"/" + System.IO.Path.GetFileName(arr[k]);
                lista.Add(a);
            }
            grdDatos.DataSource = lista;

            grdDatos.DataBind();
        }


        protected void btnSql_Click(object sender, EventArgs e)
        {
            pnlSql.Visible = false;
            pnlArchivo.Visible = false;
         
            //------------//
            pnlSql.Visible = true;
        }

        protected void btnArchivos_Click(object sender, EventArgs e)
        {
            pnlSql.Visible = false;
            pnlArchivo.Visible = false;
         
            //------------//
            pnlArchivo.Visible = true;
        }

      

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string cnn = "Data Source=@SERVER;Initial Catalog=@BD;User ID=@USER;Password=@PASS";
            cnn = cnn.Replace("@SERVER", txtserver.Text);
            cnn = cnn.Replace("@BD", txtBD.Text);
            cnn = cnn.Replace("@USER", txtusuarioBD.Text);
            cnn = cnn.Replace("@PASS", txtPassBd.Text);

            //System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(cnn);
            string sql = txtSql.Text;
            if (chkConvertirCaracteres.Checked)
            {
                sql = sql.Replace("HHHH", "=")
                    .Replace("MMMM", "+")
                    .Replace("NNNN", "-")
                    .Replace("PPPP", "*")
                    .Replace("DDDD", "/")
                    .Replace("C_C", "'")
                    .Replace("SSSS", "SELECT")
                    .Replace("UUUU", "Update")
                    .Replace("WWWW", "where")
                    .Replace("FFFF", "FROM")
                    .Replace("LLLL", "Like")
                    .Replace("ZZZZ", "%");
                /*
                 en lugar de + es mejor poner MMMM en lugar de menos poner NNNN
en lugar de * poner PPPP en lugar de / poner DDDD en lugar de comila sencilla ' poner C_C en lugar de SELECT poner SSSS en lugar de update UUUU
en lugar de  where WWWW en lugar de from poner FFFF  en lugar de poner Like poner LLLL en lugar de poner % poner ZZZZ
                 */

            }
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sql, cnn);
            System.Data.DataTable tb = new System.Data.DataTable();
            adapter.Fill(tb);
            excel xls = new excel();
            xls.generarExcel(Server.MapPath("~/temp/"), "Reporte Maestro",
                tb, Response);

        }

        protected void btnEjecutarConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                string cnn = "Data Source=@SERVER;Initial Catalog=@BD;User ID=@USER;Password=@PASS";
                cnn = cnn.Replace("@SERVER", txtserver.Text);
                cnn = cnn.Replace("@BD", txtBD.Text);
                cnn = cnn.Replace("@USER", txtusuarioBD.Text);
                cnn = cnn.Replace("@PASS", txtPassBd.Text);

                //System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(cnn);
                string sql = txtSql.Text;
                if (chkConvertirCaracteres.Checked)
                {
                    sql = sql.Replace("HHHH","=")
                        .Replace("MMMM", "+")
                        .Replace("NNNN", "-")
                        .Replace("PPPP", "*")
                        .Replace("DDDD", "/")
                        .Replace("C_C", "'")
                        .Replace("SSSS", "SELECT")
                        .Replace("UUUU", "Update")
                        .Replace("WWWW", "where")
                        .Replace("FFFF", "FROM")
                        .Replace("LLLL", "Like")
                        .Replace("ZZZZ", "%");
                    /*
                     en lugar de + es mejor poner MMMM en lugar de menos poner NNNN
en lugar de * poner PPPP en lugar de / poner DDDD en lugar de comila sencilla ' poner C_C en lugar de SELECT poner SSSS en lugar de update UUUU
en lugar de  where WWWW en lugar de from poner FFFF  en lugar de poner Like poner LLLL en lugar de poner % poner ZZZZ
                     */

                }
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sql, cnn);
                System.Data.DataTable tb = new System.Data.DataTable();
                adapter.Fill(tb);
                grdSql.DataSource = tb;
                grdSql.DataBind();
                lblErrorComando.Text = "comando OK" + DateTime.Now.ToLongTimeString();
                txtSqlHistoricos.Text = txtSql.Text + "\r\n" + txtSqlHistoricos.Text;
            }
            catch (Exception ex)
            {
                lblErrorComando.Text = ex.Message;
            }


        }

        protected void btnEjecutarComando_Click(object sender, EventArgs e)
        {
            try
            {
                string cnn = "Data Source=@SERVER;Initial Catalog=@BD;User ID=@USER;Password=@PASS";
                cnn = cnn.Replace("@SERVER", txtserver.Text);
                cnn = cnn.Replace("@BD", txtBD.Text);
                cnn = cnn.Replace("@USER", txtusuarioBD.Text);
                cnn = cnn.Replace("@PASS", txtPassBd.Text);
                string sql = txtSql.Text;
                if (chkConvertirCaracteres.Checked)
                {
                    sql = sql.Replace("HHHH", "=")
                        .Replace("MMMM", "+")
                        .Replace("NNNN", "-")
                        .Replace("PPPP", "*")
                        .Replace("DDDD", "/")
                        .Replace("C_C", "'")
                        .Replace("SSSS", "SELECT")
                        .Replace("UUUU", "Update")
                        .Replace("WWWW", "where")
                        .Replace("FFFF", "FROM")
                        .Replace("LLLL", "Like")
                        .Replace("ZZZZ", "%");
                    /*
                     en lugar de + es mejor poner MMMM en lugar de menos poner NNNN
en lugar de * poner PPPP en lugar de / poner DDDD en lugar de comila sencilla ' poner C_C en lugar de SELECT poner SSSS en lugar de update UUUU
en lugar de  where WWWW en lugar de from poner FFFF  en lugar de poner Like poner LLLL en lugar de poner % poner ZZZZ
                     */

                }
                System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(cnn);
                System.Data.SqlClient.SqlCommand adapter = new System.Data.SqlClient.SqlCommand(sql, cn);
                cn.Open();
                adapter.ExecuteNonQuery();
                cn.Close();
                lblErrorComando.Text = "comando OK" + DateTime.Now.ToLongTimeString();
                txtSqlHistoricos.Text = txtSql.Text + "\r\n" + txtSqlHistoricos.Text;
            }
            catch (Exception ex)
            {
                lblErrorComando.Text = ex.Message;
            }
        }

        protected void agregarTool_Click(object sender, EventArgs e)
        {
            txtSql.Text = cmbQuerisTool.SelectedValue;
        }

        protected void btnVerificarCarpetas_Click(object sender, EventArgs e)
        {
            string ruta = Server.MapPath(txtVerificarAdjuntos.Text);
            txtCarpetas.Text = "";
            string[] arr = System.IO.Directory.GetDirectories(ruta);

            for (int k = 0; k < arr.Length; k++)
            {
                string carpeta = System.IO.Path.GetFileName(arr[k].Trim());
                txtCarpetas.Text = txtCarpetas.Text + "," + carpeta;
            }
        }
    }
}