using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Data;
using CineProducto.Bussines;
using System.Web.SessionState;


namespace CineProducto.Callbacks
{
    /// <summary>
    /// Summary description for logInfo
    /// </summary>
    public class logInfo : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string query = "SELECT project_log_id, project_log_date, project_name FROM project_log, project WHERE project_log_project_id = project_id AND project_id = " + context.Session["project_id"];
            DB db = new DB();
            DataSet ds = db.Select(query);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                context.Response.Write("<rows>");
                DataTableCollection dataTableCollection = ds.Tables;
                DataTable table = dataTableCollection[0];
                foreach (DataRow row in table.Rows) // Loop over the rows.
                {
                    bool first = true;
                    foreach (var item in row.ItemArray) // Loop over the items.
                    {
                        if (first == true)
                        {
                            context.Response.Write("<row id=\""+item.ToString()+"\">");
                            first = false;
                            continue;
                        }
                        context.Response.Write("<cell>" + item.ToString() + "</cell>");
                    }
                    context.Response.Write("</row>");
                }
                context.Response.Write("</rows>");
            }
            //context.Response.Write("<rows></rows>");
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