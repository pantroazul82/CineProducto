using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela un adjunto y las funciones de apoyo */
    public class ValidationAttachment
    {
        public int validation_id;
        public int attachment_id;
        public string attachment_name;
        public string validation_variable;
        public string validation_type;
        public string validation_value;
        public string validation_operator;
        public int active;

        /* Atributos para la grilla de administración de adjuntos */
        public int validation_attachment_options_record_count;
        public int validation_attachment_options_page_count;
        public int validation_attachment_options_page_index;

        /* Constructor de la clase Attachment */
        public ValidationAttachment(int validation_id = 0)
        {
            /* Incialización de atributos con los valores por defecto */
            this.validation_id = 0;

            /* Validación del parámetro y carga de información del objeto */
            if (validation_id != 0)
            {
                LoadValidationAttachment(validation_id);
            }
        }

        public void LoadValidationAttachment(int validation_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT av.validation_id, av.attachment_id, "
                                 + "a.attachment_name, av.variable, "
                                 + "av.validation_type, av.value, "
                                 + "av.operator, av.active "
                                 + "FROM attachment_validation av, attachment a" 
                                 + "WHERE av.attachment_id = a.attachment_id AND "
                                 + "attachment_id=" + attachment_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.validation_id = (int)ds.Tables[0].Rows[0]["validation_id"];
                this.attachment_id = ds.Tables[0].Rows[0]["attachment_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["attachment_id"]) : 0;
                this.attachment_name = ds.Tables[0].Rows[0]["attachment_name"].ToString() != "" ? ds.Tables[0].Rows[0]["attachment_name"].ToString() : "";
                this.validation_variable = ds.Tables[0].Rows[0]["variable"].ToString() != "" ? ds.Tables[0].Rows[0]["variable"].ToString() : "";
                this.validation_type = ds.Tables[0].Rows[0]["validation_type"].ToString() != "" ? ds.Tables[0].Rows[0]["validation_type"].ToString() : "";
                this.validation_value = ds.Tables[0].Rows[0]["value"].ToString() != "" ? ds.Tables[0].Rows[0]["value"].ToString() : "";
                this.validation_operator = ds.Tables[0].Rows[0]["operator"].ToString() != "" ? ds.Tables[0].Rows[0]["operator"].ToString() : "";
                this.active = ds.Tables[0].Rows[0]["active"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["active"]) : 0;
            }
        }

        public bool Save()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;

            /* Inicializacion de variables para la grabación */
            string save_query_active_field = (this.active == 1) ? "1" : "0";

            /* Valida que estén definidos los valores en los atributos */
            if (this.attachment_id > 0 && 
                this.validation_variable != "" && 
                this.validation_type != "" && 
                this.validation_value != "" && 
                this.validation_operator != "")
            {
                /* Se verifica si se debe llevar a cabo una inserción o una actualización */
                if (this.validation_id <= 0)
                {
                    result = db.Execute("INSERT INTO attachment_validation (attachment_id, variable, validation_type, "
                                            +"value, operator, active) " +
                                        "VALUES ('" + this.attachment_id + "','" + this.validation_variable + "','" + this.validation_type + "','" +
                                            this.validation_value + "','" + this.validation_operator + "','" + save_query_active_field + "')");
                }
                else
                {
                    result = db.Execute("UPDATE attachment_validation SET " +
                                           "attachment_id = '" + this.attachment_id +
                                        "', variable = '" + this.validation_variable +
                                        "', validation_type = '" + this.validation_type +
                                        "', value = '" + this.validation_value +
                                        "', operator = '" + this.validation_operator +
                                        "', active = " + save_query_active_field +
                                        " WHERE validation_id=" + this.validation_id);
                }
            }

            /* Retorna el indicador del resultado de la operación */
            return result;
        }

        /**
        * @abstract Este método permite obtener un dataset con el listado de las validaciones de adjuntos disponibles.
        * @param int PageSize          Cantidad de registros a presentar por página
        * @param int PageNumber        Número de página que se debe obtener
        * @param string SortColumn     Indica la columna a través de la cual se llevará a cabo el ordenamiento de la información
        * @param string SortOrder      Indica si la información de ordena de forma ascendente o descendente
        * 
        * @return DataSet retorna el dataset con el listado de opciones de personal seleccionadas.
        * */
        public DataSet getValidationAttachmentList(int PageSize, int PageNumber, string SortColumn, string SortOrder)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Instancia el objeto que almacenará el listado de opciones */
            DataSet result = new DataSet();

            /* Validación de la cantidad de registros por página */
            if (PageSize <= 0)
            {
                PageSize = 10;
            }
            /* Validación del número de página */
            if (PageNumber <= 0)
            {
                PageNumber = 1;
            }

            /* Validación de la columna por la cual se ordenará */
            if (SortColumn == "")
            {
                SortColumn = "attachment_name";
            }

            /* Validación del tipo de ordenamiento */
            if (SortOrder != "asc" && SortOrder != "desc")
            {
                SortOrder = "desc";
            }

            /* Se calcula la cantidad de registros que hacen parte del resultado */
            DataSet resultRegisterQty = db.Select("Select count(validation_id) as qty FROM attachment_validation");
            if (resultRegisterQty.Tables[0].Rows.Count == 1)
            {
                this.validation_attachment_options_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
            }
            else
            {
                this.validation_attachment_options_record_count = 0;
            }

            /* Se calcula la cantidad de páginas del resultado */
            double tempPageCount = this.validation_attachment_options_record_count / (double)PageSize;
            this.validation_attachment_options_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

            /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
            if (PageNumber > this.validation_attachment_options_page_count)
            {
                PageNumber = this.validation_attachment_options_page_count;
            }

            /* Define el indice de la página*/
            this.validation_attachment_options_page_index = PageNumber - 1;

            /* Consulta el listado de registros a presentar */
            result = db.Select("SELECT * FROM"
                                    + "(SELECT av.validation_id"
                                          + ",av.attachment_id"
                                          + ",a.attachment_name"
                                          + ",av.variable"
                                          + ",av.validation_type"
                                          + ",av.value"
                                          + ",av.operator"
                                          + ",av.active"
                                          + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                      + " FROM attachment_validation av, attachment a WHERE av.attachment_id = a.attachment_id) "
                                + " AS ResultadoPaginado"
                                + " WHERE RowNumber BETWEEN " + (PageSize * this.validation_attachment_options_page_index + 1)
                                + " AND " + (PageSize * (this.validation_attachment_options_page_index + 1))
                                + " ORDER BY " + SortColumn + " " + SortOrder);

            /* Retorna el indicador del resultado de la operación */
            return result;
        }

        /**
         * @abstract Este método elimina el registro (se marca como eliminado).
         * 
         * @return bool verdadero si la grabación se realizó con éxito o falso en caso contrario
         **/
        public bool Delete()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;

            /* Se verifica si se debe llevar a cabo una inserción o una actualización */
            if (this.validation_id > 0)
            {
                result = db.Execute("DELETE FROM attachment_validation WHERE validation_id=" + this.validation_id);
            }

            /* Retorna el indicador del resultado de la operación */
            return result;
        }
    }
}