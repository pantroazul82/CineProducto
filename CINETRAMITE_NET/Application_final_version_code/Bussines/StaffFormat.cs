using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase de apoyo para manejar la persistencia de la tabla staff_format la cual 
     * almacena la información del personal de la película que se debe relacionar
     * siempre que la película sea un largometraje.
     */
    public class StaffFormat
    {
        public int project_id;

        private string _staff_format_position;
        private string _staff_format_name;
        private string _staff_format_identification_number;
        private string _staff_format_address;
        private string _staff_format_localization_father_id;
        private string _staff_format_localization_id;
        private string _staff_format_country;
        private string _staff_format_state;
        private string _staff_format_email;
        private string _staff_format_phone;
        private string _staff_format_movil;
        private string _staff_format_fax;

        public string staff_format_position { set { _staff_format_position = value; } get { return ((_staff_format_position == null) ? "" : _staff_format_position); } }
        public string staff_format_name { set { _staff_format_name = value; } get { return ((_staff_format_name == null) ? "" : _staff_format_name); } }
        public string staff_format_identification_number { set { _staff_format_identification_number = value; } get { return ((_staff_format_identification_number == null) ? "" : _staff_format_identification_number); } }
        public string staff_format_address { set { _staff_format_address = value; } get { return ((_staff_format_address == null) ? "" : _staff_format_address); } }
        public string staff_format_localization_father_id { set { _staff_format_localization_father_id = value; } get { return ((_staff_format_localization_father_id == null) ? "" : _staff_format_localization_father_id); } }
        public string staff_format_localization_id { set { _staff_format_localization_id = value; } get { return ((_staff_format_localization_id == null) ? "" : _staff_format_localization_id); } }
        public string staff_format_country { set { _staff_format_country = value; } get { return ((_staff_format_country == null) ? "" : _staff_format_country); } }
        public string staff_format_state { set { _staff_format_state = value; } get { return ((_staff_format_state == null) ? "" : _staff_format_state); } }
        public string staff_format_email { set { _staff_format_email = value; } get { return ((_staff_format_email == null) ? "" : _staff_format_email); } }
        public string staff_format_phone { set { _staff_format_phone = value; } get { return ((_staff_format_phone == null) ? "" : _staff_format_phone); } }
        public string staff_format_movil { set { _staff_format_movil = value; } get { return ((_staff_format_movil == null) ? "" : _staff_format_movil); } }
        public string staff_format_fax { set { _staff_format_fax = value; } get { return ((_staff_format_fax == null) ? "" : _staff_format_fax); } }

        /* Constructor de la clase Staff */
        public StaffFormat(int project_id = 0, string staff_format_position = "")
        {
            if (project_id != 0 && staff_format_position != "")
            {
                this.project_id = project_id;
                this.staff_format_position = staff_format_position;
                LoadStaffFormat(); 
            }
        }

        public void LoadStaffFormat()
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_id, staff_format_position, staff_format_name, "
                                 + "staff_format_identification_number, "
                                 + "staff_format_address, staff_format_localization_id, "
                                 + "staff_format_country, staff_format_state, "
                                 + "staff_format_email, staff_format_phone, "
                                 + "staff_format_movil, staff_format_fax "
                                 + " FROM staff_format WHERE project_id=" + this.project_id 
                                 + " AND staff_format_position = '" + this.staff_format_position + "'");
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.project_id = (int)ds.Tables[0].Rows[0]["project_id"];
                this.staff_format_name = ds.Tables[0].Rows[0]["staff_format_name"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_name"].ToString() : "";
                this.staff_format_position = ds.Tables[0].Rows[0]["staff_format_position"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_position"].ToString() : "";
                this.staff_format_identification_number = ds.Tables[0].Rows[0]["staff_format_identification_number"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_identification_number"].ToString() : "";
                this.staff_format_address = ds.Tables[0].Rows[0]["staff_format_address"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_address"].ToString() : "";
                this.staff_format_localization_id = ds.Tables[0].Rows[0]["staff_format_localization_id"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_localization_id"].ToString() : "";
                this.staff_format_country = ds.Tables[0].Rows[0]["staff_format_country"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_country"].ToString() : "";
                this.staff_format_state = ds.Tables[0].Rows[0]["staff_format_state"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_state"].ToString() : "";
                this.staff_format_email = ds.Tables[0].Rows[0]["staff_format_email"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_email"].ToString() : "";
                this.staff_format_phone = ds.Tables[0].Rows[0]["staff_format_phone"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_phone"].ToString() : "";
                this.staff_format_movil = ds.Tables[0].Rows[0]["staff_format_movil"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_movil"].ToString() : "";
                this.staff_format_fax = ds.Tables[0].Rows[0]["staff_format_fax"].ToString() != "" ? ds.Tables[0].Rows[0]["staff_format_fax"].ToString() : "";
            }

            DataSet localizationFatherDS = db.Select("SELECT localization_father_id FROM localization WHERE localization_id = '" + this.staff_format_localization_id + "'");
            if (localizationFatherDS.Tables[0].Rows.Count == 1)
            {
                this.staff_format_localization_father_id = localizationFatherDS.Tables[0].Rows[0]["localization_father_id"].ToString();
            }
            else
            {
                this.staff_format_localization_father_id = "";
            }
        }

        public bool Save()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Si esta definido un project_staff_id se hace la persistencia en la base de datos */
            if (this.project_id.ToString() != "" && this.project_id > 0 && this.staff_format_position != "")
            {
                /* Crea la sentencia de almacenamiento de la ubicacion */
                string localization_update = "";
                string localization_insert = "";
                if (this.staff_format_localization_id == "0" || this.staff_format_localization_id == "" || this.staff_format_localization_id == null)
                {
                    localization_update = "null";
                    localization_insert = "null";
                }
                else
                {
                    localization_update = "'" + this.staff_format_localization_id + "'";
                    localization_insert = "'" + this.staff_format_localization_id + "'";

                    this.staff_format_country = "";
                    this.staff_format_state = "";
                }

                /* Se consulta la base de datos para verificar si el registro indicado ya existe */
                DataSet ds = db.Select("SELECT project_id "
                                     + " FROM staff_format WHERE project_id=" + this.project_id
                                     + " AND staff_format_position = '" + this.staff_format_position + "'");
                if (ds.Tables[0].Rows.Count == 1)
                {
                    /* Creación de la sentencia de actualizacion */
                    string updateProjectStaffFormat = "UPDATE staff_format SET ";

                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_name = '" + this.staff_format_name + "', ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_identification_number = '" + this.staff_format_identification_number + "', ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_address = '" + this.staff_format_address + "', ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_localization_id = " + localization_update + ", ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_country = '" + this.staff_format_country + "', ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_state = '" + this.staff_format_state + "', ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_email = '" + this.staff_format_email + "', ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_phone = '" + this.staff_format_phone + "', ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_movil = '" + this.staff_format_movil + "', ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "staff_format_fax = '" + this.staff_format_fax + "' ";
                    updateProjectStaffFormat = updateProjectStaffFormat + "WHERE project_id=" + this.project_id;
                    updateProjectStaffFormat = updateProjectStaffFormat + " AND staff_format_position = '" + this.staff_format_position + "'";

                    /* Si se actualizó correctamente la tabla del proyecto, se procede a actualizar la tabla de formatos del proyecto */
                    if (db.Execute(updateProjectStaffFormat))
                    {
                        /* Si la actualización fue exitosa se devuelve el valor true */
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    /* Creación de la sentencia de actualizacion */
                    string insertProjectStaffFormat = "INSERT INTO staff_format ("
                                         + "project_id, "
                                         + "staff_format_position, "
                                         + "staff_format_name, "
                                         + "staff_format_identification_number, "
                                         + "staff_format_address, "
                                         + "staff_format_localization_id, "
                                         + "staff_format_country, "
                                         + "staff_format_state, "
                                         + "staff_format_email, "
                                         + "staff_format_phone, "
                                         + "staff_format_movil, "
                                         + "staff_format_fax "
                            + ") VALUES ("

                            + " '" + this.project_id + "', "
                            + " '" + this.staff_format_position + "', "
                            + " '" + this.staff_format_name + "', "
                            + " '" + this.staff_format_identification_number + "', "
                            + " '" + this.staff_format_address + "', "
                            + " " + localization_insert + ", "
                            + " '" + this.staff_format_country + "', "
                            + " '" + this.staff_format_state + "', "
                            + " '" + this.staff_format_email + "', "
                            + " '" + this.staff_format_phone + "', "
                            + " '" + this.staff_format_movil + "', "
                            + " '" + this.staff_format_fax + "' )";

                    /* Si se actualizó correctamente la tabla del productor */
                    return db.Execute(insertProjectStaffFormat);
                }
            }

            return false;
        }        
    }
}