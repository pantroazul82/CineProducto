using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CineProducto.Bussines
{
    public class HojaTransferencia
    {
        public int requestID { get; set; }
        public string path { get; set; }
        public int projectID { get; set; }

        public HojaTransferencia(int id = 0)
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

        public void LoadByProject(int ProjectID)
        {

            DB db = new DB();
            DataSet ds = db.Select("SELECT HOJA_TRANSFERENCIA, "
                                 + "project_id "
                                 + "FROM dboPrd.project WHERE project_id=" + ProjectID.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                //this.requestID = (int)ds.Tables[0].Rows[0]["request_form_id"];
                this.path = ds.Tables[0].Rows[0]["HOJA_TRANSFERENCIA"].ToString();
                this.projectID = (int)ds.Tables[0].Rows[0]["project_id"];
            }
        }

        //public void DeleteByProject(int ProjectID)
        //{

        //    DB db = new DB();
        //    db.Execute("delete "
        //                         + " request_form WHERE request_form_project_id=" + ProjectID.ToString());

        //}

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
                result = db.Execute("UPDATE dboPrd.project SET HOJA_TRANSFERENCIA = '" + this.path + "' where project_id=" + this.projectID);

            }
            else
            {
                result = db.Execute("UPDATE dboPrd.project SET HOJA_TRANSFERENCIA = '" + this.path + "' where project_id=" + this.projectID);
            }
            return result;
        }


        //public bool delete()
        //{
        //    DB db = new DB();
        //    bool result = false;
        //    if (this.requestID > 0)
        //    {
        //        result = db.Execute("DELETE request_form WHERE request_form_id=" + this.requestID);
        //    }
        //    return result;
        //}


    }
}