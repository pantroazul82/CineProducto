using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;

namespace CineProducto.Bussines
{
    [DataContract]
    public class Annotation
    {
        [DataMember]
        public string annotation_id { get; set; }
        [DataMember]
        public string annotation_text { get; set; }
        [DataMember]
        public string annotation_x { get; set; }
        [DataMember]
        public string annotation_y { get; set; }
        [DataMember]
        public int annotation_project { get; set; }
        [DataMember]
        public string annotation_width { get; set; }
        [DataMember]
        public string annotation_height { get; set; }
        [DataMember]
        public bool annotation_folded { get; set; }
        [DataMember]
        public bool annotation_readonly { get; set; }
        [DataMember]
        public string annotation_file { get; set; }
        [DataMember]
        public string annotation_type { get; set; }
        [DataMember]
        public string annotation_displayformat { get; set; }

        public Annotation(string id = "")
        {
            /* Incialización de atributos con los valores por defecto */

            this.annotation_id = null;


            /* Validación del parámetro y carga de información del objeto */
            if (id != "")
            {
                LoadAnnotation(id);
            }
        }

        static public List<Annotation> loadAnnotationsByFileAndProject(string documentString, int projectId)
        {
            List<Annotation> list = new List<Annotation>();
            DB db = new DB();
            DataSet ds = db.Select("SELECT annotation_id FROM annotation WHERE annotation_project = " + projectId + " AND annotation_file = '" + documentString + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(new Annotation(row["annotation_id"].ToString()));
                }
            }
            return list;
        }

        public void LoadAnnotation(string id)
        {

            DB db = new DB();
            DataSet ds = db.Select("SELECT * "
                                 + "FROM annotation WHERE annotation_id='" + id.ToString() + "'");
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.annotation_id = ds.Tables[0].Rows[0]["annotation_id"].ToString();
                this.annotation_text = ds.Tables[0].Rows[0]["annotation_text"].ToString() != "" ? ds.Tables[0].Rows[0]["annotation_text"].ToString() : "";
                this.annotation_file = ds.Tables[0].Rows[0]["annotation_file"].ToString() != "" ? ds.Tables[0].Rows[0]["annotation_file"].ToString() : "";
                this.annotation_type = ds.Tables[0].Rows[0]["annotation_type"].ToString() != "" ? ds.Tables[0].Rows[0]["annotation_type"].ToString() : "";
                this.annotation_displayformat = ds.Tables[0].Rows[0]["annotation_displayformat"].ToString() != "" ? ds.Tables[0].Rows[0]["annotation_displayformat"].ToString() : "";
                this.annotation_x = ds.Tables[0].Rows[0]["annotation_x"].ToString() != "" ? ds.Tables[0].Rows[0]["annotation_x"].ToString() : "0";
                this.annotation_y = ds.Tables[0].Rows[0]["annotation_y"].ToString() != "" ? ds.Tables[0].Rows[0]["annotation_y"].ToString() : "0";
                this.annotation_project = ds.Tables[0].Rows[0]["annotation_project"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["annotation_project"]) : 0;
                this.annotation_width = ds.Tables[0].Rows[0]["annotation_width"].ToString() != "" ? ds.Tables[0].Rows[0]["annotation_width"].ToString() : "0";
                this.annotation_height = ds.Tables[0].Rows[0]["annotation_height"].ToString() != "" ? ds.Tables[0].Rows[0]["annotation_height"].ToString() : "0";
                this.annotation_folded = ds.Tables[0].Rows[0]["annotation_folded"].ToString() != "" ? Convert.ToBoolean(ds.Tables[0].Rows[0]["annotation_folded"]) : false;
                this.annotation_readonly = ds.Tables[0].Rows[0]["annotation_readonly"].ToString() != "" ? Convert.ToBoolean(ds.Tables[0].Rows[0]["annotation_readonly"]) : false;
            }
        }

        public bool save()
        {

            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;

            /* Se verifica si se debe llevar a cabo una inserción o una actualización */
            if (new Annotation(this.annotation_id).annotation_id == null)
            {
                result = db.Execute("INSERT INTO annotation (annotation_text, annotation_id, annotation_x, annotation_y, "
                                        + "annotation_project,annotation_width,annotation_height,annotation_folded,annotation_readonly, " +
                                    "annotation_file, annotation_type, annotation_displayformat) VALUES ('" + this.annotation_text + "','" + this.annotation_id + "', " +
                                    this.annotation_x + ", " + this.annotation_y + ", " +
                                        this.annotation_project + ", " + this.annotation_width + ", " + this.annotation_height +
                                        ", '" + this.annotation_folded + "', '" + this.annotation_readonly + "', '" + this.annotation_file + "', '"
                                        + this.annotation_type + "', '" + this.annotation_displayformat + "')");
            }
            else
            {
                result = db.Execute("UPDATE annotation SET " +
                                       "annotation_text = '" + this.annotation_text + "', " +
                                       "annotation_x = " + this.annotation_x + ", " +
                                       "annotation_y = " + this.annotation_y + ", " +
                                       "annotation_width = " + this.annotation_width + ", " +
                                       "annotation_height = " + this.annotation_height + ", " +
                                       "annotation_folded = '" + this.annotation_folded + "', " +
                                       "annotation_readonly =  '" + this.annotation_readonly + "', " +
                                       "annotation_type = '" + this.annotation_type + "', " +
                                       "annotation_displayformat = '" + this.annotation_displayformat + "' " +
                                       "WHERE annotation_id='" + this.annotation_id + "'");
            }


            /* Retorna el indicador del resultado de la operación */
            return result;
        }

        public bool delete()
        {
            DB db = new DB();
            return db.Execute("DELETE FROM annotation WHERE annotation_id = '" + this.annotation_id + "'");
        }
    }
}