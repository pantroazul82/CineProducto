using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela la configuración de la aplicación */
    public class AppConfiguration
    {
        public int id;
        public string name;
        public string description;
        public string value;

        /* Constructor de la clase Project */
        public AppConfiguration(string name)
        {
            this.name = name;
            if (this.name != "")
            {
                LoadByName(this.name); 
            }
        }

        public void LoadByName(string name)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT configuration_id, configuration_name, "
                                 + "configuration_description, configuration_value "
                                 + "FROM configuration WHERE configuration_name = '" + this.name +"'");
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.id = (int)ds.Tables[0].Rows[0]["configuration_id"];
                this.name = ds.Tables[0].Rows[0]["configuration_name"].ToString() != "" ? ds.Tables[0].Rows[0]["configuration_name"].ToString() : "";
                this.description = ds.Tables[0].Rows[0]["configuration_description"].ToString() != "" ? ds.Tables[0].Rows[0]["configuration_description"].ToString() : "";
                this.value = ds.Tables[0].Rows[0]["configuration_value"].ToString() != "" ? ds.Tables[0].Rows[0]["configuration_value"].ToString() : "";
            }
        }

        public bool Save()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* @todo se debe implementar la persistencia */
            return false;
        }
    }
}