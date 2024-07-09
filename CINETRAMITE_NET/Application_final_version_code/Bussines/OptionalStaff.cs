using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela un empleado y las funciones de apoyo */
    public class OptionalStaff
    {
        public int project_optional_staff_id;
        public int project_optional_staff_project_id;
        public string project_optional_staff_firstname;
        public string project_optional_staff_lastname;
        public string project_optional_staff_identification_type;
        public string project_optional_staff_identification_number;
        public string project_optional_staff_city;
        public string project_optional_staff_state;
        public string project_optional_staff_address;
        public string project_optional_staff_phone;
        public string project_optional_staff_movil;
        public string project_optional_staff_email;
        public int project_optional_staff_position_id;

        /* Constructor de la clase Staff */
        public OptionalStaff(int project_optional_staff_id = 0)
        {
            if (project_optional_staff_id != 0)
            {
                LoadOptionalStaff(project_optional_staff_id);
            }
        }

        public void LoadOptionalStaff(int project_optional_staff_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_optional_staff_id, project_optional_staff_project_id, project_optional_staff_firstname, "
                                 + "project_optional_staff_lastname, project_optional_staff_identification_type, "
                                 + "project_optional_staff_identification_number, project_optional_staff_city, project_optional_staff_address, "
                                 + "project_optional_staff_state, project_optional_staff_phone, project_optional_staff_movil, project_optional_staff_email, "
                                 + "project_optional_staff_identification_attachment, project_optional_staff_cv_attachment, "
                                 + "project_optional_staff_contract_attachment, project_optional_staff_position_id, "
                                 + "project_optional_staff_identification_approved, project_optional_staff_cv_approved, "
                                 + "project_optional_staff_contract_approved "
                                 + "FROM dboPrd.project_optional_staff WHERE project_optional_staff_id=" + project_optional_staff_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.project_optional_staff_id = (int)ds.Tables[0].Rows[0]["project_optional_staff_id"];
                this.project_optional_staff_project_id = (int)ds.Tables[0].Rows[0]["project_optional_staff_project_id"];
                this.project_optional_staff_firstname = ds.Tables[0].Rows[0]["project_optional_staff_firstname"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_firstname"].ToString() : "";
                this.project_optional_staff_lastname = ds.Tables[0].Rows[0]["project_optional_staff_lastname"].ToString() != "" ? ds.Tables[0].Rows[0]["projec_optionalt_staff_lastname"].ToString() : "";
                this.project_optional_staff_identification_type = ds.Tables[0].Rows[0]["project_optional_staff_identification_type"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_identification_type"].ToString() : "";
                this.project_optional_staff_identification_number = ds.Tables[0].Rows[0]["project_optional_staff_identification_number"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_identification_number"].ToString() : "";
                this.project_optional_staff_city = ds.Tables[0].Rows[0]["project_optional_staff_city"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_city"].ToString() : "";
                this.project_optional_staff_state = ds.Tables[0].Rows[0]["project_optional_staff_state"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_state"].ToString() : "";
                this.project_optional_staff_address = ds.Tables[0].Rows[0]["project_optional_staff_address"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_address"].ToString() : "";
                this.project_optional_staff_phone = ds.Tables[0].Rows[0]["project_optional_staff_phone"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_phone"].ToString() : "";
                this.project_optional_staff_movil = ds.Tables[0].Rows[0]["project_optional_staff_movil"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_movil"].ToString() : "";
                this.project_optional_staff_email = ds.Tables[0].Rows[0]["project_optional_staff_email"].ToString() != "" ? ds.Tables[0].Rows[0]["project_optional_staff_email"].ToString() : "";
                this.project_optional_staff_position_id = ds.Tables[0].Rows[0]["project_optional_staff_position_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_optional_staff_position_id"] : 0;
            }
        }

        public bool Save()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Si esta definido un project_staff_id se hace una actualización, de lo contrario se hace una inserción */
            if (this.project_optional_staff_id.ToString() != "" && this.project_optional_staff_id > 0)
            {
                /* Creación de la sentencia de actualizacion */
                string updateProjectOptionalStaff = "UPDATE dboPrd.project_optional_staff SET ";

                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_project_id = '" + this.project_optional_staff_project_id + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_firstname = '" + this.project_optional_staff_firstname + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_lastname = '" + this.project_optional_staff_lastname + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_identification_type = '" + this.project_optional_staff_identification_type + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_identification_number = '" + this.project_optional_staff_identification_number + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_city = '" + this.project_optional_staff_city + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_state = '" + this.project_optional_staff_state + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_address = '" + this.project_optional_staff_address + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_phone = '" + this.project_optional_staff_phone + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_movil = '" + this.project_optional_staff_movil + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_email = '" + this.project_optional_staff_email + "', ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "project_optional_staff_position_id = '" + this.project_optional_staff_position_id + "' ";
                updateProjectOptionalStaff = updateProjectOptionalStaff + "WHERE project_staff_id = " + this.project_optional_staff_id.ToString();

                /* Si se actualizó correctamente la tabla del proyecto, se procede a actualizar la tabla de formatos del proyecto */
                if (db.Execute(updateProjectOptionalStaff))
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
                string insertProjectOptionalStaff = "INSERT INTO dboPrd.project_optional_staff ("
                                     + "project_optional_staff_project_id, "
                                     + "project_optional_staff_firstname, "
                                     + "project_optional_staff_lastname, "
                                     + "project_optional_staff_identification_type, "
                                     + "project_optional_staff_identification_number, "
                                     + "project_optional_staff_city, "
                                     + "project_optional_staff_state, "
                                     + "project_optional_staff_address, "
                                     + "project_optional_staff_phone, "
                                     + "project_optional_staff_movil, "
                                     + "project_optional_staff_email, "
                                     + "project_optional_staff_position_id "
                        + ") VALUES ("

                        + " '" + this.project_optional_staff_project_id + "', "
                        + " '" + this.project_optional_staff_firstname + "', "
                        + " '" + this.project_optional_staff_lastname + "', "
                        + " '" + this.project_optional_staff_identification_type + "', "
                        + " '" + this.project_optional_staff_identification_number + "', "
                        + " '" + this.project_optional_staff_city + "', "
                        + " '" + this.project_optional_staff_state + "', "
                        + " '" + this.project_optional_staff_address + "', "
                        + " '" + this.project_optional_staff_phone + "', "
                        + " '" + this.project_optional_staff_movil + "', "
                        + " '" + this.project_optional_staff_email + "', "
                        + " '" + this.project_optional_staff_position_id + "' )";

                /* Si se actualizó correctamente la tabla del productor */
                if (db.Execute(insertProjectOptionalStaff))
                {
                    string queryProjectStaffId = "SELECT MAX(project_optional_staff_id) as project_optional_staff_id FROM dboPrd.project_optional_staff";
                    DataSet newStaffDS = db.Select(queryProjectStaffId);
                    if (newStaffDS.Tables[0].Rows.Count == 1)
                    {
                        this.project_optional_staff_id = Convert.ToInt32(newStaffDS.Tables[0].Rows[0]["project_optional_staff_id"]);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /* Esta función obtiene el cargo de primer nivel de la persona cargada en el objeto
         * Esto es útil debido a que los cargos son jerárquicos y en la presentación de cargos
           se cargan usando los cargos de primer nivel */
        public int getTopPosition() 
        {
            int topPosition = -1;
            if (this.project_optional_staff_id > 0) 
            {
                /* Hace disponible la conexión a la base de datos */
                DB db = new DB();
                DataSet result = db.Select("SELECT DISTINCT position_father_id "
                                         + "FROM position, project_optional_staff "
                                         + "WHERE position.position_id = project_optional_staff.project_optional_staff_position_id AND "
                                         + "position_deleted='0' AND position.position_id = " + this.project_optional_staff_position_id);
                if (result.Tables[0].Rows.Count == 1)
                {
                    topPosition = Convert.ToInt32(result.Tables[0].Rows[0]["position_father_id"]);
                    /* Si el cargo padre es 0, entonces se mantiene el mismo identificador del cargo */
                    if(topPosition == 0)
                    {
                        topPosition = this.project_optional_staff_position_id;
                    }
                }
            }

            return topPosition;
        }

        /* Este método elimina el registro de personal cargado actualmente */
        public bool Delete()
        {
            bool result = false;

            /* Si existe un proyecto cargado en el objeto se hace la eliminación del registro */
            if (this.project_optional_staff_id > 0)
            {
                /* Hace disponible la conexión a la base de datos */
                DB db = new DB();
                result = db.Execute("DELETE FROM project_optional_staff WHERE project_optional_staff_position_id = " + this.project_optional_staff_position_id);
            }

            return result;
        }
    }
}