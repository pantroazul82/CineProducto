using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xceed.Words.NET;

namespace DominioCineProducto.utils
{
    public class parametroRemplazo
    {
        parametroRemplazo() { }

        public parametroRemplazo(string parametro, string remplazo)
        {
            this.parametro = parametro;
            this.remplazo = remplazo;
        }

        /// <summary>
        /// parametro o plantilla que esta en el word
        /// </summary>
        public string parametro { set; get; }

        //remplazo valor que viene de la base de datos
        public string remplazo { set; get; }

    }
    public class clsGeneracionPDF
    {
        const string rutaPlantillas = "~/modelos_cartas";



        public void generarFormularioProducto(HttpServerUtility s, System.Web.UI.Page pagina, List<parametroRemplazo> lista)
        {
            generarPdf(s, pagina, "Formulario_Producto_2023_V2.docx", lista);
        }


        public void generarPdf(HttpServerUtility s, System.Web.UI.Page pagina, string plantilla, List<parametroRemplazo> listaRemplazar)
        {
            System.Configuration.AppSettingsReader ar = new System.Configuration.AppSettingsReader();
            string ruta = s.MapPath(rutaPlantillas + "/" + plantilla);
            Xceed.Document.NET.Document d = DocX.Load(ruta);

            Xceed.Document.NET.Formatting fBold = new Xceed.Document.NET.Formatting();
            fBold.Bold = true;
            fBold.Size = 10;
            Xceed.Document.NET.Formatting fNormal = new Xceed.Document.NET.Formatting();
            fNormal.Bold = false;
            fNormal.Size = 8;
            string pathArchivosTemp = ar.GetValue("pathArchivosTempFormulario", typeof(string)).ToString();
            string pathArchivosTempRelativo = ar.GetValue("pathArchivosTemp", typeof(string)).ToString(); ;
            if (listaRemplazar != null)
            {
                foreach (var item in listaRemplazar)
                {
                    d.ReplaceText(item.parametro, item.remplazo, false);
                }
            }

            //d.Save();
            string nombreFinal = DateTime.Now.Ticks.ToString().Substring(10) + "cer_Inversion.docx";
            string rutaFinal = pathArchivosTemp + @"\" + nombreFinal;
            d.SaveAs(rutaFinal);
            //el save as de arriba deberia ir a una ruta temporal
            RichEditDocumentServer server = new RichEditDocumentServer();
            server.LoadDocument(rutaFinal);
            //
            PdfExportOptions options = new PdfExportOptions();
            options.DocumentOptions.Author = "Ministerio de Cultura";
            options.Compressed = true;
            options.ImageQuality = PdfJpegImageQuality.Highest;
            //Export the document to the stream: 
            using (FileStream pdfFileStream = new FileStream(rutaFinal.Replace(".docx", ".pdf"), FileMode.Create))
            {
                server.ExportToPdf(pdfFileStream, options);
            }

            pagina.ClientScript.RegisterStartupScript(
                 this.GetType(), "OpenWindow",
                 "window.open('" + "../../" + pathArchivosTempRelativo.Replace("~/", "") + "/" + nombreFinal.Replace(".docx", ".pdf") + "','_newtab" + DateTime.Now.Ticks + "');", true);

        }
    }
}
