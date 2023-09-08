using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela un adjunto de un proyecto particulas y las funciones de apoyo */
    public class ProjectType
    {
        public int id;
        public string name;
        public string description;
        public bool deleted;

        /* Constructor de la clase ProjectAttachment */
        public ProjectType(int project_type_id = 0)
        {
            /* Incialización de variables */
            this.id = 0;
            this.name = "";
            this.description = "";
            this.deleted = false;

            if (project_type_id != 0)
            {
                LoadProjectType(project_type_id); 
            }
        }

        /* Carga la inforamación del adjunto a partír del identificador único del tipo del proyecto */
        public void LoadProjectType(int project_type_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_type_id, project_type_name, "
                                 + "project_type_description, project_type_deleted "
                                 + "FROM project_type WHERE project_type_id=" + project_type_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.id = (int)ds.Tables[0].Rows[0]["project_type_id"];
                this.name = ds.Tables[0].Rows[0]["project_type_name"].ToString();
                this.description = ds.Tables[0].Rows[0]["project_type_description"].ToString();
                this.deleted = ds.Tables[0].Rows[0]["project_type_deleted"].ToString() == "0" ? false : true;
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
            //string SaveQuery = "";

            return result;
        }

        /* Esta función obtiene el listado de tipos de proyecto activos */
        public DataSet getProjectTypes()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            result = db.Select("SELECT project_type_id, project_type_name, project_type_description "
                                + "FROM project_type "
                                + "WHERE project_type_deleted = '0'");

            /* Retorna el indicador del resultado de la operación */
            return result;
        }
    }
}