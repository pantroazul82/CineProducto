using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela un adjunto de un proyecto particular y las funciones de apoyo */
    public class ProjectAttachment
    {
        public int project_attachment_id;
        public Attachment attachment;
        public string attachment_name;
        public int project_id;
        public string project_attachment_path;
        public string project_attachment_observation;
        public DateTime project_attachment_date;
        public Byte project_attachment_approved;
        public int project_attachement_producer_id;
        public int project_attachment_project_staff_order;
        public string nombre_original;
        public int project_staff_id;

        /* Constructor de la clase ProjectAttachment */
        public ProjectAttachment(int project_attachment_id = 0)
        {
            /* Incialización de variables */
            this.project_attachment_id = 0;
            this.attachment = new Attachment();
            this.project_id = 0;
            this.project_attachment_path = "";
            this.project_attachment_observation = "";
            this.nombre_original = "";
            this.project_attachment_date = DateTime.Now;
            this.project_attachment_approved = 0;
            this.project_staff_id = 0;
            this.nombre_original = "";
            if (project_attachment_id != 0)
            {
                LoadProjectAttachment(project_attachment_id); 
            }
        }

        /* Carga la inforamación del adjunto a partír del identificador único del adjunto del proyecto */
        public void LoadProjectAttachment(int project_attachment_id, string attachmet_required = "")
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_attachment_id, project_attachment_attachment_id, "
                                 + "project_attachment_project_id, project_attachment_path, "
                                 + "project_attachment_observation, project_attachment_date, "
                                 + "project_attachment_approved ,"
                                 + "project_attachment_producer_id ,"
                                 + "project_attachment_project_staff_order,nombre_original,project_staff_id "
                                 + " FROM dboPrd.project_attachment WHERE project_attachment_id=" + project_attachment_id.ToString()
                                 );
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.project_attachment_id = (int)ds.Tables[0].Rows[0]["project_attachment_id"];
                this.attachment = new Attachment(Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_attachment_id"]), attachmet_required);
                this.project_id = ds.Tables[0].Rows[0]["project_attachment_project_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_project_id"]) : 0;
                this.project_attachment_path = ds.Tables[0].Rows[0]["project_attachment_path"].ToString() != "" ? ds.Tables[0].Rows[0]["project_attachment_path"].ToString() : "";
                this.project_attachment_observation = ds.Tables[0].Rows[0]["project_attachment_observation"].ToString() != "" ? ds.Tables[0].Rows[0]["project_attachment_observation"].ToString() : "";
                this.nombre_original = (ds.Tables[0].Rows[0]["nombre_original"] != null && ds.Tables[0].Rows[0]["nombre_original"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["nombre_original"].ToString() : "";
                this.project_attachment_approved = ds.Tables[0].Rows[0]["project_attachment_approved"].ToString() != "" ? Convert.ToByte(ds.Tables[0].Rows[0]["project_attachment_approved"]) : Convert.ToByte(0);
                this.project_attachement_producer_id = ds.Tables[0].Rows[0]["project_attachment_producer_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_producer_id"]) : 0;
                this.project_attachment_project_staff_order = ds.Tables[0].Rows[0]["project_attachment_project_staff_order"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_project_staff_order"]) : 0;
                this.project_staff_id = (
                                            ds.Tables[0].Rows[0]["project_staff_id"] != null &&
                                            ds.Tables[0].Rows[0]["project_staff_id"] != System.DBNull.Value &&
                                            ds.Tables[0].Rows[0]["project_staff_id"].ToString() != "") ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_staff_id"]) : 0;
                
                if (ds.Tables[0].Rows[0]["project_attachment_date"].ToString() != "")
                {
                    this.project_attachment_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_attachment_date"].ToString());
                }
            }
        }

        

        /* Carga la inforamación del adjunto a partír del identificador del proyecto y del identificador del adjunto */
        public void LoadProjectAttachment(int project_id, int attachment_id, string attachmet_required = "")
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_attachment_id "
                                 + " FROM dboPrd.project_attachment "
                                 + "WHERE project_attachment_project_id=" + project_id.ToString()
                                 + "AND project_attachment_attachment_id=" + attachment_id.ToString());
            if (ds.Tables[0].Rows.Count >= 1)
            {
                this.LoadProjectAttachment(Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_id"]), attachmet_required);
            }
            else 
            {
                this.attachment = new Attachment(attachment_id, attachmet_required);
            }
        }

        public void LoadPersonalProjectAttachment(int project_id, int attachment_id, int attachment_staff_order)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_attachment_id, project_attachment_attachment_id, "
                                 + "project_attachment_project_id, project_attachment_path, "
                                 + "project_attachment_observation, project_attachment_date, "
                                 + "project_attachment_approved, "
                                 + "project_attachment_producer_id, "
                                 + "project_attachment_project_staff_order,nombre_original ,project_staff_id "
                                 + " FROM dboPrd.project_attachment "
                                 + "WHERE project_attachment_project_id=" + project_id.ToString()
                                 + " AND project_attachment_project_staff_order=" + attachment_staff_order.ToString()
                                 + " AND project_attachment_attachment_id=" + attachment_id.ToString());
            if (ds.Tables[0].Rows.Count >= 1)
            {
                this.project_attachment_id = (int)ds.Tables[0].Rows[0]["project_attachment_id"];
                this.attachment = new Attachment(Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_attachment_id"]), "");
                this.project_id = ds.Tables[0].Rows[0]["project_attachment_project_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_project_id"]) : 0;
                this.project_attachment_path = ds.Tables[0].Rows[0]["project_attachment_path"].ToString() != "" ? ds.Tables[0].Rows[0]["project_attachment_path"].ToString() : "";
                this.project_attachment_observation = ds.Tables[0].Rows[0]["project_attachment_observation"].ToString() != "" ? ds.Tables[0].Rows[0]["project_attachment_observation"].ToString() : "";
                this.project_attachment_approved = ds.Tables[0].Rows[0]["project_attachment_approved"].ToString() != "" ? Convert.ToByte(ds.Tables[0].Rows[0]["project_attachment_approved"]) : Convert.ToByte(0);
                this.project_attachement_producer_id = ds.Tables[0].Rows[0]["project_attachment_producer_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_producer_id"]) : 0;
                this.project_attachment_project_staff_order = ds.Tables[0].Rows[0]["project_attachment_project_staff_order"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_project_staff_order"]) : 0;
                this.nombre_original = (ds.Tables[0].Rows[0]["nombre_original"] != null && ds.Tables[0].Rows[0]["nombre_original"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["nombre_original"].ToString() : "";

                this.project_staff_id = (
                                          ds.Tables[0].Rows[0]["project_staff_id"] != null &&
                                          ds.Tables[0].Rows[0]["project_staff_id"] != System.DBNull.Value &&
                                          ds.Tables[0].Rows[0]["project_staff_id"].ToString() != "") ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_staff_id"]) : 0;
                

                if (ds.Tables[0].Rows[0]["project_attachment_date"].ToString() != "")
                {
                    this.project_attachment_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_attachment_date"].ToString());
                }
            }
        }


        public void LoadPersonalProjectAttachmentByProject_staff(int project_id, int attachment_id, int project_staff_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_attachment_id, project_attachment_attachment_id, "
                                 + "project_attachment_project_id, project_attachment_path, "
                                 + "project_attachment_observation, project_attachment_date, "
                                 + "project_attachment_approved, "
                                 + "project_attachment_producer_id, "
                                 + "project_attachment_project_staff_order,nombre_original ,project_staff_id "
                                 + " FROM dboPrd.project_attachment "
                                 + "WHERE project_attachment_project_id=" + project_id.ToString()
                                 + " AND project_staff_id=" + project_staff_id.ToString()
                                 + " AND project_attachment_attachment_id=" + attachment_id.ToString() + " order by project_attachment_date desc ");
            if (ds.Tables[0].Rows.Count >= 1)
            {
                this.project_attachment_id = (int)ds.Tables[0].Rows[0]["project_attachment_id"];
                this.attachment = new Attachment(Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_attachment_id"]), "");
                this.project_id = ds.Tables[0].Rows[0]["project_attachment_project_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_project_id"]) : 0;
                this.project_attachment_path = ds.Tables[0].Rows[0]["project_attachment_path"].ToString() != "" ? ds.Tables[0].Rows[0]["project_attachment_path"].ToString() : "";
                this.project_attachment_observation = ds.Tables[0].Rows[0]["project_attachment_observation"].ToString() != "" ? ds.Tables[0].Rows[0]["project_attachment_observation"].ToString() : "";
                this.project_attachment_approved = ds.Tables[0].Rows[0]["project_attachment_approved"].ToString() != "" ? Convert.ToByte(ds.Tables[0].Rows[0]["project_attachment_approved"]) : Convert.ToByte(0);
                this.project_attachement_producer_id = ds.Tables[0].Rows[0]["project_attachment_producer_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_producer_id"]) : 0;
                this.project_attachment_project_staff_order = ds.Tables[0].Rows[0]["project_attachment_project_staff_order"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_project_staff_order"]) : 0;
                this.nombre_original = (ds.Tables[0].Rows[0]["nombre_original"] != null && ds.Tables[0].Rows[0]["nombre_original"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["nombre_original"].ToString() : "";

                this.project_staff_id = (
                                          ds.Tables[0].Rows[0]["project_staff_id"] != null &&
                                          ds.Tables[0].Rows[0]["project_staff_id"] != System.DBNull.Value &&
                                          ds.Tables[0].Rows[0]["project_staff_id"].ToString() != "") ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_staff_id"]) : 0;


                if (ds.Tables[0].Rows[0]["project_attachment_date"].ToString() != "")
                {
                    this.project_attachment_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_attachment_date"].ToString());
                }
            }
        }

        /* este */
        public void  loadAttachmentByFhaterAndProducerId(int projectId, int attachmnetId, int producer_id)
        {
            DB db = new DB();
            string _producer_id = "";
            string _attachmnetId = "";
            if (producer_id != 0)
            {
                _producer_id = "AND project_attachment_producer_id = '" + producer_id.ToString() + "'";
            }
            if (attachmnetId != 0)
            {
                _attachmnetId = "AND project_attachment_attachment_id = '" + attachmnetId.ToString() + "'";
            }
            DataSet ds = db.Select("SELECT project_attachment_id, project_attachment_attachment_id, "
                                 + "project_attachment_project_id, project_attachment_path, "
                                 + "project_attachment_observation, project_attachment_date, "
                                 + "project_attachment_approved ,"
                                 + "project_attachment_producer_id,nombre_original  ,project_staff_id "
                                 + "FROM dboPrd.project_attachment WHERE project_attachment_project_id=" + projectId.ToString()
                                 + " " + _producer_id
                                 + " " + _attachmnetId
                                 );
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.project_attachment_id = (int)ds.Tables[0].Rows[0]["project_attachment_id"];
                this.attachment = new Attachment(Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_attachment_id"]), "");
                this.project_id = ds.Tables[0].Rows[0]["project_attachment_project_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_project_id"]) : 0;
                this.project_attachment_path = ds.Tables[0].Rows[0]["project_attachment_path"].ToString() != "" ? ds.Tables[0].Rows[0]["project_attachment_path"].ToString() : "";
                this.project_attachment_observation = ds.Tables[0].Rows[0]["project_attachment_observation"].ToString() != "" ? ds.Tables[0].Rows[0]["project_attachment_observation"].ToString() : "";
                this.project_attachment_approved = ds.Tables[0].Rows[0]["project_attachment_approved"].ToString() != "" ? Convert.ToByte(ds.Tables[0].Rows[0]["project_attachment_approved"]) : Convert.ToByte(0);
                this.project_attachement_producer_id = ds.Tables[0].Rows[0]["project_attachment_producer_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_attachment_producer_id"]) : 0;
                this.nombre_original = (ds.Tables[0].Rows[0]["nombre_original"] != null && ds.Tables[0].Rows[0]["nombre_original"] != System.DBNull.Value) ? ds.Tables[0].Rows[0]["nombre_original"].ToString() : "";

                this.project_staff_id = (
                                     ds.Tables[0].Rows[0]["project_staff_id"] != null &&
                                     ds.Tables[0].Rows[0]["project_staff_id"] != System.DBNull.Value &&
                                     ds.Tables[0].Rows[0]["project_staff_id"].ToString() != "") ? Convert.ToInt32(ds.Tables[0].Rows[0]["project_staff_id"]) : 0;
               

                if (ds.Tables[0].Rows[0]["project_attachment_date"].ToString() != "")
                {
                    this.project_attachment_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_attachment_date"].ToString());
                }
            }
        }
        public bool Save()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Incializa la variable que se retornará */
            bool result = false;

            /* Incializa la variable que almacena la sentencia de 
             * inserción o actualización */
            string SaveQuery = "";

            /* Se valida si en el objeto hay un identificador de un proyecto
             * cargado, pues es obligatorio para la actualización o para la
             * inserción de un nuevo registro
             */
            if (this.project_id > 0 && this.attachment.attachment_id > 0 && this.project_attachment_path != "")
            {
                /* Valida si en el objeto existe un id para actualizar o si se 
                 * debe llevar a cabo una inserción
                 */
                    if (this.project_attachment_id > 0)
                    {
                        string project_attachment_date_update_query = (this.project_attachment_date.Year == 1) ? "null" : "GETDATE()";

                        SaveQuery = "UPDATE dboPrd.project_attachment SET "
                                    + "project_attachment_project_id=" + this.project_id + ","
                                    +"project_attachment_attachment_id =" + this.attachment.attachment_id + ","
                                    +"project_attachment_path='" + this.project_attachment_path + "',"
                                    + "project_attachment_observation='" + this.project_attachment_observation.Replace("'", "") + "',"
                                    + "project_attachment_date=" + project_attachment_date_update_query + ","
                                    +"project_attachment_approved=" + this.project_attachment_approved + ","
                                    + "project_attachment_producer_id=" + this.project_attachement_producer_id.ToString() + ", "
                                    + "project_attachment_project_staff_order=" + this.project_attachment_project_staff_order + ", "
                                    + "nombre_original='" + this.nombre_original.Replace("'","") + "', "
                                    + "project_staff_id=" + this.project_staff_id + " "
                                    
                                    +"WHERE project_attachment_id = " + this.project_attachment_id;
                    }
                    else
                    {
                        SaveQuery = "INSERT INTO dboPrd.project_attachment "
                                    + "(project_attachment_project_id, project_attachment_attachment_id, "
                                    +"project_attachment_path,project_attachment_observation, "
                                    + "project_attachment_date, project_attachment_approved,project_attachment_producer_id,project_attachment_project_staff_order,nombre_original,project_staff_id) "
                                    +"VALUES ('"+ this.project_id +"','"+ this.attachment.attachment_id +"',"
                                    +"'"+ this.project_attachment_path +"','"+ this.project_attachment_observation +"',"
                                    + "GETDATE(),'" + this.project_attachment_approved + "'," + this.project_attachement_producer_id.ToString() + "," + this.project_attachment_project_staff_order + ",'"+this.nombre_original+"',"+ this.project_staff_id +")";
                    }

                    /* Ejecuta la sentencia */
                    result = db.Execute(SaveQuery);
            }

            return result;
        }

        public void LoadAttachmetAndUpdate(int project_attachment_id, int producer_id)
        {
            Project project = new Project( project_attachment_id );
            DB db = new DB();
            foreach (ProjectAttachment item in project.attachment)
            {
                if (item.attachment.attachment_father_id == 1 || item.attachment.attachment_father_id == 16) //Solo los adjuntos de datos del productor y de financiación
                {
                    if (item.project_attachement_producer_id == 0) {
                        string project_attachment_date_update_query = (item.project_attachment_date.Year == 1) ? "null" : "GETDATE()";
                        string SaveQuery = "UPDATE dboPrd.project_attachment SET "
                                    + "project_attachment_project_id=" + item.project_id + ","
                                    + "project_attachment_attachment_id =" + item.attachment.attachment_id + ","
                                    + "project_attachment_path='" + item.project_attachment_path + "',"
                                    + "project_attachment_observation='" + item.project_attachment_observation + "',"
                                    + "project_attachment_date=" + project_attachment_date_update_query + ","
                                    + "project_attachment_approved=" + item.project_attachment_approved + ","
                                    + "project_attachment_producer_id='" + producer_id + "', "
                                    + "project_staff_id=" + this.project_staff_id + " "
                                    + "WHERE project_attachment_id = " + item.project_attachment_id;
                        db.Execute(SaveQuery);
                    }
                }
            }
        }
    }
      
}