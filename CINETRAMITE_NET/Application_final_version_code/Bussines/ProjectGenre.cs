using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela un adjunto de un proyecto particulas y las funciones de apoyo */
    public class ProjectGenre
    {
        public int id;
        public string name;
        public string description;
        public bool deleted;

        /* Constructor de la clase ProjectAttachment */
        public ProjectGenre(int project_genre_id = 0)
        {
            /* Incialización de variables */
            this.id = 0;
            this.name = "";
            this.description = "";
            this.deleted = false;

            if (project_genre_id != 0)
            {
                LoadProjectGenre(project_genre_id); 
            }
        }

        /* Carga la inforamación del adjunto a partír del identificador único del tipo del proyecto */
        public void LoadProjectGenre(int project_genre_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_genre_id, project_genre_name, "
                                 + "project_genre_description, project_genre_deleted "
                                 + "FROM project_genre WHERE project_genre_id=" + project_genre_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.id = (int)ds.Tables[0].Rows[0]["project_genre_id"];
                this.name = ds.Tables[0].Rows[0]["project_genre_name"].ToString();
                this.description = ds.Tables[0].Rows[0]["project_genre_description"].ToString();
                this.deleted = ds.Tables[0].Rows[0]["project_genre_deleted"].ToString() == "0" ? false : true;
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
        public DataSet getProjectGenres()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            result = db.Select("SELECT project_genre_id, project_genre_name, project_genre_description "
                                + "FROM project_genre "
                                + "WHERE project_genre_deleted = '0'");

            /* Retorna el indicador del resultado de la operación */
            return result;
        }
    }
}