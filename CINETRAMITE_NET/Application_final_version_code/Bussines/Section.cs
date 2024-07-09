using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela un empleado y las funciones de apoyo */
    public class Section
    {
        public int section_id;
        public int project_id;
        public string section_name = "";
        public string solicitud_aclaraciones="";
        public DateTime solicitud_aclaraciones_date;
        public string observacion_inicial="";
        public DateTime observacion_inicial_date;
        public string aclaraciones_productor="";
        public DateTime aclaraciones_productor_date;
        public string observacion_final="";
        public DateTime observacion_final_date;
    
        public int revision_state_id;
        public string revision_state_name="";

        public int tab_state_id;
        public string tab_state_name="";

        public string revision_mark = "";

        public DateTime modified;
        public DateTime viewed;

        /* Constructor de la clase Section */
        public Section(int section_id = 0, int project_id = 0)
        {
            if (section_id > 0 && project_id > 0)
            {
                this.project_id = project_id;
                this.section_id = section_id;

                LoadSection(); 
            }
        }

        public bool LoadSection()
        {
            /* Se valida si están definidos los valores de las llaves del registro ya que son valores obligatorios */
            if (this.section_id > 0 && this.project_id > 0)
            {
                DB db = new DB();
                DataSet ds = db.Select("SELECT project_status_project_id, project_status_section_id, section.section_name, "
                                     + "project_status_revision_state_id, state_revision.state_name as revision_state_name, "
                                     + "project_status_tab_state_id, state_tab.state_name as tab_state_name, project_status_revision_mark, "
                                     + "project_status_solicitud_aclaraciones, project_status_solicitud_aclaraciones_date, "
                                     + "project_status_observacion_inicial, project_status_observacion_inicial_date, "
                                     + "project_status_aclaraciones_productor, project_status_aclaraciones_productor_date, "
                                     + "project_status_observacion_final, project_status_observacion_final_date, "
                                     + "project_status_modified, project_status_viewed "
                                     + "FROM dboPrd.project_status, dboPrd.section, dboPrd.state state_revision, dboPrd.state state_tab "
                                     + "WHERE project_status.project_status_section_id = section.section_id AND "
                                     + "state_revision.state_id = project_status.project_status_revision_state_id AND "
                                     + "state_tab.state_id = project_status.project_status_tab_state_id AND "
                                     + "project_status_project_id=" + this.project_id.ToString() + " AND "
                                     + "project_status_section_id=" + this.section_id);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    this.section_name = ds.Tables[0].Rows[0]["section_name"].ToString() != "" ? ds.Tables[0].Rows[0]["section_name"].ToString() : "";

                    this.solicitud_aclaraciones = ds.Tables[0].Rows[0]["project_status_solicitud_aclaraciones"].ToString() != "" ? ds.Tables[0].Rows[0]["project_status_solicitud_aclaraciones"].ToString() : "";

                    if (ds.Tables[0].Rows[0]["project_status_solicitud_aclaraciones_date"].ToString() != "")
                    {
                        this.solicitud_aclaraciones_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_status_solicitud_aclaraciones_date"].ToString());
                    }

                    this.observacion_inicial = ds.Tables[0].Rows[0]["project_status_observacion_inicial"].ToString() != "" ? ds.Tables[0].Rows[0]["project_status_observacion_inicial"].ToString() : "";

                    if (ds.Tables[0].Rows[0]["project_status_observacion_inicial_date"].ToString() != "")
                    {
                        this.aclaraciones_productor_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_status_observacion_inicial_date"].ToString());
                    }

                    this.aclaraciones_productor = ds.Tables[0].Rows[0]["project_status_aclaraciones_productor"].ToString() != "" ? ds.Tables[0].Rows[0]["project_status_aclaraciones_productor"].ToString() : "";

                    if (ds.Tables[0].Rows[0]["project_status_aclaraciones_productor_date"].ToString() != "")
                    {
                        this.observacion_inicial_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_status_aclaraciones_productor_date"].ToString());
                    }

                    this.observacion_final = ds.Tables[0].Rows[0]["project_status_observacion_final"].ToString() != "" ? ds.Tables[0].Rows[0]["project_status_observacion_final"].ToString() : "";

                    if (ds.Tables[0].Rows[0]["project_status_observacion_final_date"].ToString() != "")
                    {
                        this.observacion_final_date = DateTime.Parse(ds.Tables[0].Rows[0]["project_status_observacion_final_date"].ToString());
                    }

                    this.revision_state_id = ds.Tables[0].Rows[0]["project_status_revision_state_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_status_revision_state_id"] : 0;
                    this.revision_state_name = ds.Tables[0].Rows[0]["revision_state_name"].ToString() != "" ? ds.Tables[0].Rows[0]["revision_state_name"].ToString() : "";
                    this.tab_state_id = ds.Tables[0].Rows[0]["project_status_tab_state_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["project_status_tab_state_id"] : 0;
                    this.tab_state_name = ds.Tables[0].Rows[0]["tab_state_name"].ToString() != "" ? ds.Tables[0].Rows[0]["tab_state_name"].ToString() : "";
                    this.revision_mark = ds.Tables[0].Rows[0]["project_status_revision_mark"].ToString() != "" ? ds.Tables[0].Rows[0]["project_status_revision_mark"].ToString() : "";

                    if (ds.Tables[0].Rows[0]["project_status_modified"].ToString() != "")
                    {
                        this.modified = DateTime.Parse(ds.Tables[0].Rows[0]["project_status_modified"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["project_status_viewed"].ToString() != "")
                    {
                        this.viewed = DateTime.Parse(ds.Tables[0].Rows[0]["project_status_viewed"].ToString());
                    }

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

        public bool Save()
        {
            /* Se valida si están definidos los valores de las llaves del registro ya que son valores obligatorios */
            if (this.section_id > 0 && this.project_id > 0)
            {
                /* Hace disponible la conexión a la base de datos */
                DB db = new DB();

                /* Ajuste de la información de fechas, donde se valida si vienen fechas por defecto se
                 ajustan en null para la actualización correcta en la base de datos */
                string solicitud_aclaraciones_date_update_query = (this.solicitud_aclaraciones_date.Year == 1) ? "null" : "'" + this.solicitud_aclaraciones_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string observacion_inicial_date_update_query = (this.observacion_inicial_date.Year == 1) ? "null" : "'" + this.observacion_inicial_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string aclaraciones_productor_date_update_query = (this.aclaraciones_productor_date.Year == 1) ? "null" : "'" + this.aclaraciones_productor_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string observacion_final_date_update_query = (this.observacion_final_date.Year == 1) ? "null" : "'" + this.observacion_final_date.ToString("dd-MM-yy HH:mm:ss") + "'";
                string modified_update_query = (this.modified.Year == 1) ? "null" : "'" + this.modified.ToString("dd-MM-yy HH:mm:ss") + "'";
                string viewed_update_query = (this.viewed.Year == 1) ? "null" : "'" + this.viewed.ToString("dd-MM-yy HH:mm:ss") + "'";

                /* Ajusta el valor del estado de la revisión y la pestaña en caso de que sea 0 
                 * Lo cual se hace ya que estos dos valores no pueden ser cero para que la consulta
                 * que carga la información en el objeto no quede en blanco por problema en la relacion
                 * de estos valores con sus respectivas tablas.
                 */
                int revision_state_id_tmp = (this.revision_state_id == 0) ? 11 : this.revision_state_id;
                int tab_state_id_tmp = (this.tab_state_id == 0) ? 11 : this.tab_state_id;

                /* Si existe el registor en la base de datos se hace una actualización, de lo contrario se hace una inserción */
                DataSet ds = db.Select("SELECT project_status_project_id, project_status_section_id "
                                     + "FROM dboPrd.project_status "
                                     + "WHERE project_status_project_id =" + this.project_id.ToString() + " AND "
                                     + "project_status_section_id=" + this.section_id);
                if (ds.Tables[0].Rows.Count == 1)
                {

                    if (this.solicitud_aclaraciones == null) 
                    {
                        this.solicitud_aclaraciones = "";
                    }
                    if (this.observacion_inicial == null)
                    {
                        this.observacion_inicial = "";
                    }
                    if (this.aclaraciones_productor == null)
                    {
                        this.aclaraciones_productor = "";
                    }
                    if (this.observacion_final == null)
                    {
                        this.observacion_final = "";
                    }

                    /* Creación de la sentencia de actualizacion */
                    string updateProjectSection = "UPDATE dboPrd.project_status SET ";
                    List<System.Data.SqlClient.SqlParameter> listaParametros = new List<System.Data.SqlClient.SqlParameter>();

                    updateProjectSection = updateProjectSection + "project_status_revision_state_id = '" + revision_state_id_tmp + "', ";
                    updateProjectSection = updateProjectSection + "project_status_tab_state_id = '" + tab_state_id_tmp + "', ";
                    updateProjectSection = updateProjectSection + "project_status_revision_mark = '" + this.revision_mark + "', ";
                    updateProjectSection = updateProjectSection + "project_status_solicitud_aclaraciones = '" + this.solicitud_aclaraciones.Replace("'", "´") + "', ";
                    updateProjectSection = updateProjectSection + "project_status_solicitud_aclaraciones_date = " + solicitud_aclaraciones_date_update_query + ", ";
                    updateProjectSection = updateProjectSection + "project_status_observacion_inicial = @project_status_observacion_inicial , ";
                    System.Data.SqlClient.SqlParameter parametrproject_status_observacion_inicial = new System.Data.SqlClient.SqlParameter();
                    parametrproject_status_observacion_inicial.Value = this.observacion_inicial.Replace("'", "´");
                    parametrproject_status_observacion_inicial.ParameterName = "@project_status_observacion_inicial";
                    parametrproject_status_observacion_inicial.Direction = ParameterDirection.Input;
                    parametrproject_status_observacion_inicial.SqlDbType = SqlDbType.VarChar;
                    listaParametros.Add(parametrproject_status_observacion_inicial);



                    updateProjectSection = updateProjectSection + "project_status_observacion_inicial_date = " + observacion_inicial_date_update_query + ", ";
                    updateProjectSection = updateProjectSection + "project_status_aclaraciones_productor = '" + this.aclaraciones_productor.Replace("'", "´") + "', ";
                    updateProjectSection = updateProjectSection + "project_status_aclaraciones_productor_date = " + aclaraciones_productor_date_update_query + ", ";
                    updateProjectSection = updateProjectSection + "project_status_observacion_final = @project_status_observacion_final , ";
                    System.Data.SqlClient.SqlParameter parametrproject_status_observacion_final = new System.Data.SqlClient.SqlParameter();
                    parametrproject_status_observacion_final.Value = this.observacion_final.Replace("'", "´");
                    parametrproject_status_observacion_final.ParameterName = "@project_status_observacion_final";
                    parametrproject_status_observacion_final.Direction = ParameterDirection.Input;
                    parametrproject_status_observacion_final.SqlDbType = SqlDbType.VarChar;
                    listaParametros.Add(parametrproject_status_observacion_final);


                    updateProjectSection = updateProjectSection + "project_status_observacion_final_date = " + observacion_final_date_update_query + ", ";
                    updateProjectSection = updateProjectSection + "project_status_modified = " + modified_update_query + ", ";
                    updateProjectSection = updateProjectSection + "project_status_viewed = " + viewed_update_query + " ";
                    updateProjectSection = updateProjectSection + "WHERE project_status_project_id = '" + this.project_id.ToString() + "' ";
                    updateProjectSection = updateProjectSection + "AND project_status_section_id = '" + this.section_id + "' ";


                    /* Se retorna el resultado de la actualización */
                    if (db.Execute(updateProjectSection, listaParametros))
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
                    string insertProjectSection = "INSERT INTO dboPrd.project_status ("
                                         + "project_status_project_id, "
                                         + "project_status_section_id, "
                                         + "project_status_revision_state_id, "
                                         + "project_status_tab_state_id, "
                                         + "project_status_revision_mark, "
                                         + "project_status_solicitud_aclaraciones, "
                                         + "project_status_solicitud_aclaraciones_date, "
                                         + "project_status_observacion_inicial, "
                                         + "project_status_observacion_inicial_date, "
                                         + "project_status_aclaraciones_productor, "
                                         + "project_status_aclaraciones_productor_date, "
                                         + "project_status_observacion_final, "
                                         + "project_status_observacion_final_date, "
                                         + "project_status_modified, "
                                         + "project_status_viewed "
                            + ") VALUES ("

                            + " '" + this.project_id + "', "
                            + " '" + this.section_id + "', "
                            + " '" + this.revision_state_id + "', "
                            + " '" + this.tab_state_id + "', "
                            + " '" + this.revision_mark + "', "
                            + " '" + this.solicitud_aclaraciones + "', "
                            + " " + solicitud_aclaraciones_date_update_query + ", "
                            + " '" + this.observacion_inicial + "', "
                            + " " + observacion_inicial_date_update_query + ", "
                            + " '" + this.aclaraciones_productor + "', "
                            + " " + aclaraciones_productor_date_update_query + ", "
                            + " '" + this.observacion_final + "', "
                            + " " + observacion_final_date_update_query + ", "
                            + " " + modified_update_query + ", "
                            + " " + viewed_update_query + " )";

                    /* Si se actualizó correctamente la tabla del productor */
                    if (db.Execute(insertProjectSection))
                    {
                        string queryProjectSection = "SELECT project_status_project_id, project_status_section_id "
                                                        + "FROM dboPrd.project_status "
                                                        + "WHERE project_status_project_id = '" + this.project_id + "' AND "
                                                        + "project_status_section_id = '" + this.section_id + "'";
                        DataSet newSectionDS = db.Select(queryProjectSection);
                        if (newSectionDS.Tables[0].Rows.Count == 1)
                        {
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
            else
            {
                return false;
            }
        }
    }
}