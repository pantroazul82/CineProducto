using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioCineProducto.utils
{
    public class excel
    {
        public void generarExcel(string rutaTemporal, string nombreReporte,System.Data.DataTable tabla,System.Web.HttpResponse response) 
        {
        //        private void generarArchivo(string archivo,string nombreArchivo,int filaSubTotal,int filaInicioPromocion){
             
            string ruta = rutaTemporal;
            string nombreDestino = nombreReporte + DateTime.Now.Ticks.ToString().Substring(7) + ".xlsx";
            string archivoDestino = rutaTemporal + "/" + nombreDestino;
            

            FileInfo newFile = new FileInfo(archivoDestino);
            ExcelPackage pck = new ExcelPackage(newFile);            

            var ws = pck.Workbook.Worksheets.Add(nombreReporte);
            ws.DefaultColWidth = 32;
            //agregamos los titulos
            for(int j=0;j< tabla.Columns.Count;j++)
            {
                ws.Cells[1, j+1].Value = tabla.Columns[j].ColumnName.Replace("_"," ");
                ws.Cells[1, j + 1].Style.WrapText = false;
                ws.Cells[1, j + 1].AutoFitColumns(10, 20);
                ws.Cells[1, j + 1].Style.WrapText = true;
            }

            for(int k=0;k<tabla.Rows.Count;k++)
            {
                for(int j=0;j< tabla.Columns.Count;j++)
                {
                    Type t = tabla.Rows[k][j].GetType();
                    if (t.Name.ToLower().Contains("datetime"))
                    {
                         ws.Cells[k + 2, j + 1].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        if (tabla.Rows[k][j] != null && tabla.Rows[k][j] != System.DBNull.Value)
                            ws.Cells[k + 2, j + 1].Value = (DateTime)tabla.Rows[k][j];
                    } else {
                        ws.Cells[k + 2, j + 1].Value = tabla.Rows[k][j].ToString();
                    }
                }
            }           
                
            pck.Save();
            //    Response.Redirect("~/" + pathArchivosTemp + "/" + nombreDestino);
            response.Redirect("~/temp/" + nombreDestino);

        }

    }
}
