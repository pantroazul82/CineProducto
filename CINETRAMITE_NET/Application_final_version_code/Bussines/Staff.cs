using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;
using DominioCineProducto.utils;

namespace CineProducto.Bussines
{
    /* Clase que modela un empleado y las funciones de apoyo */
    public class Staff
    {
        public int project_staff_id;
        public int project_staff_project_id;
        public int id_genero;
        public int id_etnia;
        public int identification_type_id;
        public int id_grupo_poblacional;
        public DateTime? fecha_nacimiento;
        public string project_staff_firstname;
        public string project_staff_firstname2;
        public string project_staff_lastname;
        public string project_staff_lastname2;
        public string project_staff_genero;
        public string project_staff_localization_id;
        public string project_staff_identification_type;
        public string project_staff_identification_number;
        public string project_staff_city;
        public string project_staff_state;
        public string project_staff_address;
        public string project_staff_phone;
        public string project_staff_movil;
        public string project_staff_email;
        public string project_staff_identification_attachment;
        public string project_staff_cv_attachment;
        public string project_staff_contract_attachment;
        public int project_staff_position_id;
        public Byte project_staff_identification_approved;
        public Byte project_staff_cv_approved;
        public Byte project_staff_contract_approved;

        /* Constructor de la clase Staff */
        public Staff(int project_staff_id = 0)
        {
            if (project_staff_id != 0)
            {
                LoadStaff(project_staff_id); 
            }
        }

        public void LoadStaff(int project_staff_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT project_staff_id, project_staff_project_id, id_genero, id_etnia, identification_type_id,id_grupo_poblacional, fecha_nacimiento, project_staff_firstname, project_staff_firstname2, "
                                 + "project_staff_lastname, project_staff_lastname2, project_staff_genero, project_staff_localization_id, project_staff_identification_type, "
                                 + "project_staff_identification_number, project_staff_city, project_staff_address, "
                                 + "project_staff_state, project_staff_phone, project_staff_movil, project_staff_email, "
                                 + "project_staff_identification_attachment, project_staff_cv_attachment, "
                                 + "project_staff_contract_attachment, project_staff_position_id, "
                                 + "project_staff_identification_approved, project_staff_cv_approved, "
                                 + "project_staff_contract_approved "
                                 + "FROM dboPrd.project_staff WHERE project_staff_id=" + project_staff_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.project_staff_id = (int)ds.Tables[0].Rows[0]["project_staff_id"];
                this.project_staff_project_id = (int)ds.Tables[0].Rows[0]["project_staff_project_id"];

                this.id_genero = ds.Tables[0].Rows[0]["id_genero"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_genero"] : 0;
                this.id_etnia = ds.Tables[0].Rows[0]["id_etnia"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_etnia"] : 0;
                this.identification_type_id = ds.Tables[0].Rows[0]["identification_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["identification_type_id"] : 0;
                this.id_grupo_poblacional = ds.Tables[0].Rows[0]["id_grupo_poblacional"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_grupo_poblacional"] : 0;

                if (ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "") { 
                   this.fecha_nacimiento =  DateTime.Parse(ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString()); 
                }

                this.project_staff_firstname = ds.Tables[0].Rows[0]["project_staff_firstname"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_firstname"].ToString() : "";
                this.project_staff_firstname2 = ds.Tables[0].Rows[0]["project_staff_firstname2"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_firstname2"].ToString() : "";
                this.project_staff_lastname = ds.Tables[0].Rows[0]["project_staff_lastname"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_lastname"].ToString() : "";
                this.project_staff_lastname2 = ds.Tables[0].Rows[0]["project_staff_lastname2"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_lastname2"].ToString() : "";
                this.project_staff_genero = ds.Tables[0].Rows[0]["project_staff_genero"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_genero"].ToString() : "";
                this.project_staff_localization_id = ds.Tables[0].Rows[0]["project_staff_localization_id"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_localization_id"].ToString() : "";
                this.project_staff_identification_type = ds.Tables[0].Rows[0]["project_staff_identification_type"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_identification_type"].ToString() : "";
                this.project_staff_identification_number = ds.Tables[0].Rows[0]["project_staff_identification_number"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_identification_number"].ToString() : "";
                this.project_staff_city = ds.Tables[0].Rows[0]["project_staff_city"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_city"].ToString() : "";
                this.project_staff_state = ds.Tables[0].Rows[0]["project_staff_state"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_state"].ToString() : "";
                this.project_staff_address = ds.Tables[0].Rows[0]["project_staff_address"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_address"].ToString() : "";
                this.project_staff_phone = ds.Tables[0].Rows[0]["project_staff_phone"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_phone"].ToString() : "";
                this.project_staff_movil = ds.Tables[0].Rows[0]["project_staff_movil"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_movil"].ToString() : "";
                this.project_staff_email = ds.Tables[0].Rows[0]["project_staff_email"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_email"].ToString() : "";
                this.project_staff_identification_attachment = ds.Tables[0].Rows[0]["project_staff_identification_attachment"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_identification_attachment"].ToString() : "";
                this.project_staff_cv_attachment = ds.Tables[0].Rows[0]["project_staff_cv_attachment"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_cv_attachment"].ToString() : "";
                this.project_staff_contract_attachment = ds.Tables[0].Rows[0]["project_staff_contract_attachment"].ToString() != "" ? ds.Tables[0].Rows[0]["project_staff_contract_attachment"].ToString() : "";
                this.project_staff_position_id = ds.Tables[0].Rows[0]["project_staff_position_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_staff_position_id"] : 0;
                this.project_staff_identification_approved = ds.Tables[0].Rows[0]["project_staff_identification_approved"].ToString() != "" ? Convert.ToByte(ds.Tables[0].Rows[0]["project_staff_identification_approved"]) : Convert.ToByte(0);
                this.project_staff_cv_approved = ds.Tables[0].Rows[0]["project_staff_cv_approved"].ToString() != "" ? Convert.ToByte(ds.Tables[0].Rows[0]["project_staff_cv_approved"]) : Convert.ToByte(0);
                this.project_staff_contract_approved = ds.Tables[0].Rows[0]["project_staff_contract_approved"].ToString() != "" ? Convert.ToByte(ds.Tables[0].Rows[0]["project_staff_contract_approved"]) : Convert.ToByte(0);
            }
        }

        public bool Save()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Si esta definido un project_staff_id se hace una actualización, de lo contrario se hace una inserción */
            if (this.project_staff_id.ToString() != "" && this.project_staff_id > 0)
            {
                /* Creación de la sentencia de actualizacion */
                string updateProjectStaff = "UPDATE dboPrd.project_staff SET ";

                updateProjectStaff = updateProjectStaff + "project_staff_project_id = '" + this.project_staff_project_id + "', ";
                updateProjectStaff = updateProjectStaff + "id_genero = '" + this.id_genero + "', ";
                updateProjectStaff = updateProjectStaff + "id_etnia = '" + this.id_etnia + "', ";
                updateProjectStaff = updateProjectStaff + "identification_type_id = '" + this.identification_type_id + "', ";
                updateProjectStaff = updateProjectStaff + "id_grupo_poblacional = '" + this.id_grupo_poblacional + "', ";

                if (this.fecha_nacimiento != null)
                {
                    string year = DateTime.Parse(this.fecha_nacimiento.ToString()).Year.ToString();
                    string mes = DateTime.Parse(this.fecha_nacimiento.ToString()).Month.ToString();
                    string dia = DateTime.Parse(this.fecha_nacimiento.ToString()).Day.ToString();
                    updateProjectStaff = updateProjectStaff + "fecha_nacimiento = '" + year +"-"+mes+"-"+dia +"', ";
                }

                updateProjectStaff = updateProjectStaff + "project_staff_firstname = '" + StringExtensors.ToNombrePropio(this.project_staff_firstname) + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_firstname2 = '" + StringExtensors.ToNombrePropio(this.project_staff_firstname2) + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_lastname = '" + StringExtensors.ToNombrePropio(this.project_staff_lastname) + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_lastname2 = '" + StringExtensors.ToNombrePropio(this.project_staff_lastname2) + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_genero = '" + this.project_staff_genero + "', ";
                
                if(this.project_staff_localization_id != null && this.project_staff_localization_id != "" && this.project_staff_localization_id != "0")
                  updateProjectStaff = updateProjectStaff + "project_staff_localization_id = '" + this.project_staff_localization_id + "', ";

                updateProjectStaff = updateProjectStaff + "project_staff_identification_type = '" + this.project_staff_identification_type + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_identification_number = '" + this.project_staff_identification_number + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_city = '" + this.project_staff_city + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_state = '" + this.project_staff_state + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_address = '" + this.project_staff_address + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_phone = '" + this.project_staff_phone + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_movil = '" + this.project_staff_movil + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_email = '" + this.project_staff_email + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_identification_attachment = '" + this.project_staff_identification_attachment + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_cv_attachment = '" + this.project_staff_cv_attachment + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_contract_attachment = '" + this.project_staff_contract_attachment + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_position_id = '" + this.project_staff_position_id + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_identification_approved = '" + this.project_staff_identification_approved + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_cv_approved = '" + this.project_staff_cv_approved + "', ";
                updateProjectStaff = updateProjectStaff + "project_staff_contract_approved = " + project_staff_contract_approved + " ";
                updateProjectStaff = updateProjectStaff + "WHERE project_staff_id = " + this.project_staff_id.ToString();

                /* Si se actualizó correctamente la tabla del proyecto, se procede a actualizar la tabla de formatos del proyecto */
                if (db.Execute(updateProjectStaff))
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

                string fecha_Nacimiento_string = "null ";
                if (this.fecha_nacimiento != null)
                {
                    string year = DateTime.Parse(this.fecha_nacimiento.ToString()).Year.ToString();
                    string mes = DateTime.Parse(this.fecha_nacimiento.ToString()).Month.ToString();
                    string dia = DateTime.Parse(this.fecha_nacimiento.ToString()).Day.ToString();
                    fecha_Nacimiento_string="'" + year + "-" + mes + "-" + dia + "' ";
                }

                /* Creación de la sentencia de actualizacion */
                string insertProjectStaff = "INSERT INTO dboPrd.project_staff ("
                                     + "project_staff_project_id, "
                                     + "id_genero, "
                                     + "id_etnia, "
                                     + "identification_type_id, "
                                     + "id_grupo_poblacional, "
                                     + "fecha_nacimiento, "
                                     + "project_staff_firstname, "
                                     + "project_staff_firstname2, "
                                     + "project_staff_lastname, "
                                     + "project_staff_lastname2, "
                                     + "project_staff_genero, "                                     
                                     + "project_staff_identification_type, "
                                     + "project_staff_identification_number, "
                                     + "project_staff_city, "
                                     + "project_staff_state, "
                                     + "project_staff_address, "
                                     + "project_staff_phone, "
                                     + "project_staff_movil, "
                                     + "project_staff_email, "
                                     + "project_staff_identification_attachment, "
                                     + "project_staff_cv_attachment, "
                                     + "project_staff_contract_attachment, "
                                     + "project_staff_position_id, "
                                     + "project_staff_identification_approved, "
                                     + "project_staff_cv_approved, "
                                     + "project_staff_contract_approved "
                        + ") VALUES ("

                        + " '" + this.project_staff_project_id + "', "
                        + " '" + this.id_genero + "', "
                        + " '" + this.id_etnia + "', "
                        + " '" + this.identification_type_id + "', "
                        + " '" + this.id_grupo_poblacional + "', "
                        + " " + fecha_Nacimiento_string + ", "
                        + " '" + StringExtensors.ToNombrePropio(this.project_staff_firstname) + "', "
                        + " '" + StringExtensors.ToNombrePropio(this.project_staff_firstname2) + "', "
                        + " '" + StringExtensors.ToNombrePropio(this.project_staff_lastname) + "', "
                        + " '" + StringExtensors.ToNombrePropio(this.project_staff_lastname2) + "', "
                        + " '" + this.project_staff_genero + "', "                        
                        + " '" + this.project_staff_identification_type + "', "
                        + " '" + this.project_staff_identification_number + "', "
                        + " '" + this.project_staff_city + "', "
                        + " '" + this.project_staff_state + "', "
                        + " '" + this.project_staff_address + "', "
                        + " '" + this.project_staff_phone + "', "
                        + " '" + this.project_staff_movil + "', "
                        + " '" + this.project_staff_email + "', "
                        + " '" + this.project_staff_identification_attachment + "', "
                        + " '" + this.project_staff_cv_attachment + "', "
                        + " '" + this.project_staff_contract_attachment + "', "
                        + " '" + this.project_staff_position_id + "', "
                        + " '" + this.project_staff_identification_approved + "', "
                        + " '" + this.project_staff_cv_approved + "', "
                        + " '" + this.project_staff_contract_approved + "' )";

                /* Si se actualizó correctamente la tabla del productor */
                if (db.Execute(insertProjectStaff))
                {
                    string queryProjectStaffId = "SELECT MAX(project_staff_id) as project_staff_id FROM dboPrd.project_staff";
                    DataSet newStaffDS = db.Select(queryProjectStaffId);
                    if (newStaffDS.Tables[0].Rows.Count == 1)
                    {
                        this.project_staff_id = Convert.ToInt32(newStaffDS.Tables[0].Rows[0]["project_staff_id"]);
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

        /**
         * Esta función permite obtener las opciones de personal disponibles para un tipo de proyecto, tipo de producción,
         * subtipo de proyecto y nacionalidad del director, determinados.
         */
        public DataSet getStaffOptions(int project_type_id, int production_type_id, int project_genre_id, int has_domestic_director,int percentage,int personal_type) 
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            int _personal_type = personal_type;
            if (personal_type == 0)
            {
                _personal_type = 2;
            }

            if (project_type_id > 0 && production_type_id > 0 && project_genre_id > 0 && has_domestic_director >= 0 && percentage > 0 && percentage <= 100 && _personal_type > 0)
            {
                string percentage_validation = "";
                if (personal_type != 1)
                {
                    percentage_validation = "staff_option_percentage_init <='" + percentage + "' AND staff_option_percentage_end >='" + percentage + "' AND ";
                }
                result = db.Select("SELECT staff_option_id, staff_option_description "
                                    + "FROM dboPrd.staff_option "
                                    + "WHERE project_type_id='" + project_type_id + "' AND "
                                    + "production_type_id='" + production_type_id + "' AND "
                                    + "project_genre_id='" + project_genre_id + "' AND "
                                    + percentage_validation
                                    + "staff_option_personal_option ='" + _personal_type + "' AND "
                                    + "staff_option_has_domestic_director='" + has_domestic_director + "' and staff_option_deleted=0 ");
            }
            else {
                result = db.Select("SELECT top 0 staff_option_id, staff_option_description "
                                      + "FROM dboPrd.staff_option ");
            }

            /* Retorna el indicador del resultado de la operación */
            return result;   
        }

        /* Esta función obtiene el detalle (los cargos y la cantidad de cargos de cada opcion)
         * de una opción de personal particular */
        public DataSet getStaffOptionDetail(int staff_option_id, int version)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            if (staff_option_id > 0)
            {
                string consulta = "SELECT staff_option_detail_id, position.position_id, position_name, "
                                    + "staff_option_detail_quantity, staff_option_detail_optional_quantity "
                                    + "FROM dboPrd.staff_option_detail, dboPrd.position "
                                    + "WHERE staff_option_id = '" + staff_option_id.ToString() + "' AND "
                                    + "staff_option_detail_deleted='0' AND position.position_id = staff_option_detail.position_id"
                                    + " AND staff_option_detail.version = " + version.ToString();
                result = db.Select(consulta );
            }

            /* Retorna el indicador del resultado de la operación */
            return result;   
        }

        /* Esta función obtiene los cargos de segundo nivel (hijos) de un cargo de primer nivel indicado */
        public DataSet getChildPositions(int position_id) 
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            if (position_id > 0)
            {
                result = db.Select("SELECT position_id, position_name, position_description "
                                    + "FROM dboPrd.position "
                                    + "WHERE position_father_id = '" + position_id + "' AND "
                                    + "position_deleted='0'");
            }

            /* Retorna el indicador del resultado de la operación */
            return result;   
        }

        /* Esta función obtiene los cargos de primer nivel */
        public DataSet getTopPositions()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            result = db.Select("SELECT position_id, position_name, position_description "
                                + "FROM dboPrd.position "
                                + "WHERE position_father_id = '0' AND "
                                + "position_deleted='0'");

            /* Retorna el indicador del resultado de la operación */
            return result;
        }

        /* Esta función obtiene el cargo de primer nivel de la persona cargada en el objeto
         * Esto es útil debido a que los cargos son jerárquicos y en la presentación de cargos
           se cargan usando los cargos de primer nivel */
        public int getTopPosition() 
        {
            int topPosition = -1;
            if (this.project_staff_id > 0) 
            {
                /* Hace disponible la conexión a la base de datos */
                DB db = new DB();
                DataSet result = db.Select("SELECT DISTINCT position_father_id "
                                         + "FROM dboPrd.position, dboPrd.project_staff "
                                         + "WHERE position.position_id = project_staff.project_staff_position_id  "
                                         //+  " AND position_deleted='0' "+
                                         +" AND position.position_id = " + this.project_staff_position_id);
                if (result.Tables[0].Rows.Count == 1)
                {
                    topPosition = Convert.ToInt32(result.Tables[0].Rows[0]["position_father_id"]);
                    /* Si el cargo padre es 0, entonces se mantiene el mismo identificador del cargo */
                    if(topPosition == 0)
                    {
                        topPosition = this.project_staff_position_id;
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
            if (this.project_staff_id > 0)
            {
                /* Hace disponible la conexión a la base de datos */
                DB db = new DB();
                /*
                 select * from project_attachment where project_staff_id in(
                    select project_staff_id FROM project_staff WHERE project_staff_position_id=34
                 )
                 */

                //eliminamos los aduntos de ese cargo
                string sql = @"delete dboPrd.project_attachment from project_attachment where 
                project_attachment_project_id =" + project_staff_project_id+ @" and project_staff_id
                in(
select project_staff_id FROM dboPrd.project_staff WHERE project_staff_position_id= " + this.project_staff_position_id +" and project_staff_project_id =" + project_staff_project_id + @"
                )";
                db.Execute(sql);
                //eliminamos el cargo de ese proyecto
                result = db.Execute("DELETE FROM dboPrd.project_staff WHERE project_staff_position_id = " + this.project_staff_position_id +
                    " and project_staff_project_id=" + project_staff_project_id);
            }

            return result;
        }
    }
}