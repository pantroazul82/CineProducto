using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CineProducto.Bussines;
using System.Data;

namespace CineProducto.Callbacks
{
    public partial class List : System.Web.UI.Page
    {
        public int countTotal = 0;
        public List<List<string>> dataSet(int page = 1, int limit = 0, string sidx = "", string sord = "", Dictionary<string, string> search = null)
        {
            List<List<string>> dataSet = new List<List<string>>();
            if (!(Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0))//el usuario no está autenticado
            {
                return dataSet;
            }
            User userObj = new User();
            userObj.user_id = Convert.ToInt32(Session["user_id"]);
            string userQ = "";
            if (!userObj.checkPermission("ver-listado-solicitudes-completo"))
            { userQ = " AND p.project_idusuario = " + userObj.user_id; }
            int start = (page - 1) * limit;
            int end = page * limit;
            string orderQ = "";
            string searchFilter = "";
            int count = 0;
            if (sidx != "" && sord != "")
            {
                orderQ = "ORDER BY " + "p."+sidx + " " + sord;
            }
            string searchQ = "";
            string declarationDates = "";
            if (search != null)
            {
                searchFilter = "WHERE ";
                foreach (string key in search.Keys)
                {
                    
                    if (System.Text.RegularExpressions.Regex.IsMatch(key, "date"))
                    {
                        string[] spl = search[key].Split('/');
                        if (spl.Length != 3)
                        {
                            continue;
                        }
                        else
                        {
                            try
                            {
                                int dd;
                                int mm;
                                int yy;
                                dd = int.Parse(spl[0]);
                                mm = int.Parse(spl[1]);
                                yy = int.Parse(spl[2]);
                                if (dd > 31 || mm > 12 || yy <= 1900 || yy > 9999)
                                {
                                    throw (new System.ArgumentException());
                                }
                                string dayStr = yy + "-" + mm + "-" + dd;
                                string dateTimeStr = dayStr + " 00:00:00.000";
                                declarationDates += " declare @start_" + key + " datetime, @end_" + key + " datetime select @start_" + key + " = '" + dateTimeStr + "', @end_" + key + " = @start_" + key + " + 1; ";
                                searchQ += " AND " + key + " >= @start_" + key + " AND " + key + " < @end_" + key + " ";
                                continue;
                            }
                            catch (System.Exception crap)
                            {
                                continue;
                            }
                        }
                    }
                    string keyFilter = "";
                    if (key == "state_name") {
                        keyFilter = "s.state_name";
                    }
                    else if (key == "resolution_path") {
                        keyFilter = "r.resolution_path";
                    }else{
                        keyFilter = "p."+key;
                    }
                    searchQ += " AND " + key + " LIKE '" + search[key] + "%' ";
                    if (count == 0)
                    {
                        searchFilter += " " + keyFilter + " LIKE '" + search[key] + "%' ";
                    }
                    else {
                        searchFilter += " AND " + keyFilter + " LIKE '" + search[key] + "%' ";
                    }

                    count++;                    
                }
            }
            if (searchFilter == "WHERE ") {
                searchFilter = "";
            }
            string query = declarationDates + "with pagedProject as (SELECT ROW_NUMBER() OVER (" + orderQ + ") AS rownumber, p.project_id, p.project_name, p.project_request_date, p.project_clarification_request_date, p.project_clarification_response_date, p.project_resolution_date, s.state_name, r.resolution_path from project p left join resolution  r on r.project_id= p.project_id left join state s on p.state_id = s.state_id " + searchFilter + " " + userQ + ") select * from pagedProject where rownumber > " + start + " and rownumber <= " + end;

            DB db = new DB();
            DataSet ds = db.Select(query);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTableCollection dataTableCollection = ds.Tables;

                DataTable table = dataTableCollection[0];
                foreach (DataRow row in table.Rows) // Loop over the rows.
                {
                    List<string> set = new List<string>();
                    int j = 0;
                    foreach (var item in row.ItemArray) // Loop over the items.
                    {
                        if (j != 0)
                            set.Add(item.ToString());
                        j++;
                        //Console.WriteLine(item); // Invokes ToString abstract method.
                    }
                    dataSet.Add(set);
                }

            }

            string Q = declarationDates + "SELECT COUNT(*) from project, state WHERE project.state_id = state.state_id " + searchQ;


            DataSet dset = db.Select(Q);
            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                DataTableCollection dataTableCollection = dset.Tables;

                DataTable table = dataTableCollection[0];


                this.countTotal = int.Parse(table.Rows[0].ItemArray[0].ToString());


            }
            return dataSet;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}