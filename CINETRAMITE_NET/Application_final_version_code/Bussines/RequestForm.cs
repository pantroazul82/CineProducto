using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CineProducto.Bussines
{
    public class RequestForm
    {
        public int requestID { get; set; }
        public string path { get; set; }
        public int projectID { get; set; }


        public RequestForm(int id = 0)
        {
            if (id != 0)
            {
                LoadByProject(id);
            }
            else
            {
                this.path = null;
                this.projectID = 0;
                this.requestID = id;
            }
        }
        public void LoadById(int resID)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT request_form_id, "
                                 + "request_form_path, request_form_project_id, "
                                 + "FROM dboPrd.request_form WHERE request_form_id=" + resID.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.requestID = (int)ds.Tables[0].Rows[0]["request_form_id"];
                this.path = ds.Tables[0].Rows[0]["request_form_path"].ToString();
                this.projectID = (int)ds.Tables[0].Rows[0]["request_form_project_id"];
            }
        }
        public void LoadByProject(int ProjectID)
        {

            DB db = new DB();
            DataSet ds = db.Select("SELECT request_form_id, "
                                 + "request_form_path, request_form_project_id "
                                 + "FROM dboPrd.request_form WHERE request_form_project_id=" + ProjectID.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.requestID = (int)ds.Tables[0].Rows[0]["request_form_id"];
                this.path = ds.Tables[0].Rows[0]["request_form_path"].ToString();
                this.projectID = (int)ds.Tables[0].Rows[0]["request_form_project_id"];
            }
        }

        public void DeleteByProject(int ProjectID)
        {

            DB db = new DB();
            db.Execute("delete "
                                 + " dboPrd.request_form WHERE request_form_project_id=" + ProjectID.ToString());
            
        }

        public bool Save()
        {

            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;
            if (this.projectID == 0 || this.path == null)
            {
                throw new Exception("InvalidData");
            }
            if (this.requestID == 0)
            {
                result = db.Execute("INSERT INTO dboPrd.request_form (request_form_path, request_form_project_id) values ('" + this.path + "', " + this.projectID + ")");
            }
            else
            {
                result = db.Execute("UPDATE dboPrd.request_form SET request_form_path = '" + this.path + "' where request_form_project_id="+ this.projectID );
            }
            return result;
        }
        public bool delete()
        {
            DB db = new DB();
            bool result = false;
            if (this.requestID > 0)
            {
                result = db.Execute("DELETE dboPrd.request_form WHERE request_form_id=" + this.requestID);
            }
            return result;
        }


    }
}