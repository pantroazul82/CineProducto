using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CineProducto.Bussines
{
    public class Resolution
    {
        public int resolutionID { get;set; }
        public string path { get;set; }

        public string path2 { get; set; }

        public int projectID { get; set; }
        public DateTime uploaded { get; set; }

        public DateTime uploaded2 { get; set; }
        public bool visible { get; set; }
        public Resolution(int id = 0)
        {
            if (id != 0)
            {
                LoadById(id);
            }
            else
            {
                this.path = null;
                this.path2 = null;
                this.projectID = 0;
                this.resolutionID = id;
            }
        }
        public void LoadById(int resID){
            DB db = new DB();
            DataSet ds = db.Select("SELECT resolution_id, "
                                 + "resolution_path, project_id,resolution_path2,resolution_uploaded2, "
                                 + "resolution_uploaded, resolution_visible "
                                 + "FROM resolution WHERE resolution_id=" + resID.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.resolutionID = (int)ds.Tables[0].Rows[0]["resolution_id"];
                this.path = ds.Tables[0].Rows[0]["resolution_path"].ToString();
                this.path2 = ds.Tables[0].Rows[0]["resolution_path2"].ToString();
                this.projectID = (int)ds.Tables[0].Rows[0]["project_id"];

                if (ds.Tables[0].Rows[0]["resolution_uploaded"] != null && ds.Tables[0].Rows[0]["resolution_uploaded"].ToString().Trim() != string.Empty)
                {
                    this.uploaded = DateTime.Parse(ds.Tables[0].Rows[0]["resolution_uploaded"].ToString());
                }

                if (ds.Tables[0].Rows[0]["resolution_uploaded2"] != null && ds.Tables[0].Rows[0]["resolution_uploaded2"].ToString().Trim() != string.Empty)
                {
                    this.uploaded2 = DateTime.Parse(ds.Tables[0].Rows[0]["resolution_uploaded2"].ToString());
                }
                this.visible = (ds.Tables[0].Rows[0]["resolution_visible"].ToString() == "0") ? false : true;
            }
        }
        public void LoadByProject (int ProjectID){

            DB db = new DB();
            DataSet ds = db.Select("SELECT resolution_id, "
                                 + "resolution_path, project_id ,resolution_path2,resolution_uploaded2,"
                                 + "resolution_uploaded, resolution_visible "
                                 + "FROM resolution WHERE project_id=" + ProjectID.ToString());
            this.projectID = ProjectID;
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.resolutionID = (int)ds.Tables[0].Rows[0]["resolution_id"];
                this.path = ds.Tables[0].Rows[0]["resolution_path"].ToString();
                this.path2 = ds.Tables[0].Rows[0]["resolution_path2"].ToString();
                this.projectID = (int)ds.Tables[0].Rows[0]["project_id"];
                if (ds.Tables[0].Rows[0]["resolution_uploaded"] != null && ds.Tables[0].Rows[0]["resolution_uploaded"].ToString().Trim() != string.Empty)
                {
                    this.uploaded = DateTime.Parse(ds.Tables[0].Rows[0]["resolution_uploaded"].ToString());
                }

                if (ds.Tables[0].Rows[0]["resolution_uploaded2"] != null && ds.Tables[0].Rows[0]["resolution_uploaded2"].ToString().Trim() != string.Empty)
                {
                    this.uploaded2 = DateTime.Parse(ds.Tables[0].Rows[0]["resolution_uploaded2"].ToString());
                }
                this.visible = (ds.Tables[0].Rows[0]["resolution_visible"].ToString() == "0") ? false : true;
            }
        }
        public bool Save()
        {

            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;
            DataSet ds = db.Select("SELECT resolution_id FROM resolution WHERE project_id=" + projectID.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.resolutionID = (int)ds.Tables[0].Rows[0]["resolution_id"];
            }

            if (this.projectID == 0 || this.path == null)
            {
                throw new Exception("InvalidData");
            }
            if (this.resolutionID == 0)
            {
                result = db.Execute(@"INSERT INTO resolution (resolution_path,resolution_path2, project_id, resolution_uploaded,resolution_uploaded2, resolution_visible) 
                values ('" + this.path + "', '" + this.path2 + "', " + this.projectID + ", GETDATE(),getdate(), 0)");
            }
            else
            {
                result = db.Execute("UPDATE resolution SET resolution_path2 = '" + this.path2 + "', resolution_path = '" + this.path + "', resolution_uploaded = GETDATE() where resolution_id=" + resolutionID);
            }
            return result;
        }

        public bool makeVisible()
        {
            DB db = new DB();
            if (this.resolutionID == 0)
            {
                throw new Exception("InvalidData");
            }
            return db.Execute("UPDATE resolution SET resolution_visible = 1 WHERE resolution_id = " + this.resolutionID);
        }

        public bool Hide()
        {
            DB db = new DB();
            if (this.resolutionID == 0)
            {
                throw new Exception("InvalidData");
            }
            return db.Execute("UPDATE resolution SET resolution_visible = 0 WHERE resolution_id = " + this.resolutionID);
        }
        public bool delete()
        {
            DB db = new DB();
            bool result = false;
            if (this.resolutionID > 0)
            {
                result = db.Execute("DELETE resolution WHERE resolution_id=" + this.resolutionID);
            }
            return result;

        }


    }
}