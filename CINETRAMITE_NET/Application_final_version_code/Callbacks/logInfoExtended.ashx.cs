using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;
using System.Text.RegularExpressions;

namespace CineProducto.Callbacks
{
    /// <summary>
    /// Summary description for logInfoExtended
    /// </summary>
    public class logInfoExtended : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string query = "SELECT project_log_content FROM project_log WHERE project_log_id = " + context.Request.QueryString["id"];
            DB db = new DB();
            DataSet ds = db.Select(query);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                context.Response.Write("<?xml version='1.0' encoding='utf-8'?>");
                context.Response.Write("<rows>");
                DataTableCollection dataTableCollection = ds.Tables;
                DataTable table = dataTableCollection[0];
                foreach (DataRow row in table.Rows) // Loop over the rows.
                {
                    foreach (var item in row.ItemArray) // Loop over the items.
                    {
                        string[] rows = Regex.Split(item.ToString(),";;;");
                        int i = 1;
                        for (int j = 0; j < (rows.Length - 1); j++ )
                        {
                            context.Response.Write("<row id=\"" + i + "\">");
                            string[] cells = Regex.Split(rows[j].ToString(), ":::");
                            bool isFieldName = true;
                            foreach (string singleCell in cells)
                            {
                                if (isFieldName)
                                {
                                    string printMe = "";
                                    switch (singleCell)
                                    {
                                        case "project_name":
                                            printMe = "T&iacute;tulo de la obra";
                                            break;
                                        case "project_domestic_producer_qty":
                                            printMe = "Cantidad de productores nacionales";
                                            break;
                                        case "project_foreign_producer_qty":
                                            printMe = "Cantidad de productores extranjeros";
                                            break;
                                        case "project_total_cost_desarrollo":
                                            printMe = "Valor etapa desarrollo";
                                            break;
                                        case "project_total_cost_preproduccion":
                                            printMe = "Valor etapa preproducci&oacute;n";
                                            break;
                                        case "project_total_cost_produccion":
                                            printMe = "Valor etapa producci&oacute;n";
                                            break;
                                        case "project_total_cost_posproduccion":
                                            printMe = "Valor etapa posproducci&oacute;n";
                                            break;
                                        case "project_total_cost_promotion":
                                            printMe = "Valor etapa promoci&oacute;n";
                                            break;
                                        case "project_synopsis":
                                            printMe = "Sinopsis";
                                            break;
                                        case "project_recording_sites":
                                            printMe = "Lugares de filmaci&oacute;n";
                                            break;
                                        case "project_duration":
                                            printMe = "Duraci&oacute;n";
                                            break;
                                        case "project_filming_start_date":
                                            printMe = "Fecha de inicio de rodaje";
                                            break;
                                        case "project_filming_end_date":
                                            printMe = "Fecha de fin de rodaje";
                                            break;
                                        case "project_filming_date_obs":
                                            printMe = "Observaciones sobre las fechas de rodaje";
                                            break;
                                        case "project_development_lab_info":
                                            printMe = "Nombre y direcci&oacute;n del laboratorio de revelado";
                                            break;
                                        case "project_preprint_store_info":
                                            printMe = "Nombre y direcci&oacute;n donde reposan los elementos de tiraje";
                                            break;
                                        case "project_legal_deposit":
                                            printMe = "Ha realizado dep&oacute;sito legal";
                                            break;
                                        case "project_has_domestic_director":
                                            printMe = "Tiene director nacional";
                                            break;
                                        case "project_clarification_request_additional_text":
                                            printMe = "Texto adicional de la solicitud de aclaraciones";
                                            break;
                                        case "project_schedule_film_view":
                                            printMe = "Fecha programada para la vista";//find out!
                                            break;
                                        case "project_result_film_view":
                                            printMe = "Resultado de la vista";//find out!!
                                            break;
                                        case "project_request_date":
                                            printMe = "Fecha de envío para revisi&oacute;n";
                                            break;
                                        case "project_clarification_request_date":
                                            printMe = "Fecha de solicitud de aclaraciones";
                                            break;
                                        case "project_clarification_response_date":
                                            printMe = "Fecha de respuesta de la solicitud de aclaraciones";
                                            break;
                                        case "project_resolution_date":
                                            printMe = "Fecha de la resoluci&oacute;n";
                                            break;
                                        case "project_notification_date":
                                            printMe = "Fecha de la notificaci&oacute;n";
                                            break;
                                    }
                                    isFieldName = false;
                                    if (printMe == "")
                                    {
                                        context.Response.Write("<cell><![CDATA[" + singleCell + "]]></cell>");

                                        //context.Response.Write("<cell><![CDATA[Otherfield]]></cell>");

//                                        break;
                                    }
                                    else
                                    {
                                        context.Response.Write("<cell><![CDATA[" + printMe + "]]></cell>");
                                    }
                                }
                                else
                                {
                                    context.Response.Write("<cell><![CDATA[" + singleCell + "]]></cell>");
                                }

                            }

                            context.Response.Write("</row>");
                            i++;
                        }
                    }
                }
                context.Response.Write("</rows>");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}