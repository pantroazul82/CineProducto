using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela los formatos con todos sus atributos */
    public class Format
    {
        public int format_id;
        public int format_type_id;
        public string format_name;
        public int format_padre;
        public string format_description;
        public string format_project_detail; //Se refiere al detall asociado a un formato para un proyecto especifico y se usa cuando el objeto hace parte del arreglo de formatos del proyecto
        public int format_deleted;
        public int format_options_record_count;
        public int format_options_page_count;
        public int format_options_page_index;
        public string format_type_name;

        /* Constructor de la clase Project */
        public Format(int format_id = 0)
        {
            if(format_id != 0)
            {
                LoadFormat(format_id); 
            }
        }

        public void LoadFormat(int format_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT format_id, format_type_id, format_name, "
                                 + "format_description, format_deleted, format_padre "
                                 + "FROM format WHERE format_id = " + format_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.format_id = (int)ds.Tables[0].Rows[0]["format_id"];
                this.format_type_id = ds.Tables[0].Rows[0]["format_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["format_type_id"] : 0;
                this.format_name = ds.Tables[0].Rows[0]["format_name"].ToString() != "" ? ds.Tables[0].Rows[0]["format_name"].ToString() : "";
                this.format_description = ds.Tables[0].Rows[0]["format_description"].ToString() != "" ? ds.Tables[0].Rows[0]["format_description"].ToString() : "";
                this.format_deleted = ds.Tables[0].Rows[0]["format_deleted"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["format_deleted"]) : 0;
                this.format_padre = ds.Tables[0].Rows[0]["format_padre"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["format_padre"] : 0;
            }
        }
        /**
        * @abstract Este método permite obtener un dataset con el listado de las opciones de adjuntos disponibles.
        * @param int PageSize          Cantidad de registros a presentar por página
        * @param int PageNumber        Número de página que se debe obtener
        * @param string SortColumn     Indica la columna a través de la cual se llevará a cabo el ordenamiento de la información
        * @param string SortOrder      Indica si la información de ordena de forma ascendente o descendente
        * 
        * @return DataSet retorna el dataset con el listado de opciones de personal seleccionadas.
        * */
        public DataSet getFormats(int PageSize, int PageNumber, string SortColumn, string SortOrder)
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
                SortColumn = "format_id";
            }

            /* Validación del tipo de ordenamiento */
            if (SortOrder != "asc" && SortOrder != "desc")
            {
                SortOrder = "desc";
            }

            /* Se calcula la cantidad de registros que hacen parte del resultado */
            DataSet resultRegisterQty = db.Select("Select count(format_id) as qty FROM format WHERE format_deleted = 0 AND format_type_id != 3");
            if (resultRegisterQty.Tables[0].Rows.Count == 1)
            {
                this.format_options_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
            }
            else
            {
                this.format_options_record_count = 0;
            }

            /* Se calcula la cantidad de páginas del resultado */
            double tempPageCount = this.format_options_record_count / (double)PageSize;
            this.format_options_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

            /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
            if (PageNumber > this.format_options_page_count)
            {
                PageNumber = this.format_options_page_count;
            }

            /* Define el indice de la página*/
            this.format_options_page_index = PageNumber - 1;

            /* Consulta el listado de registros a presentar */
            result = db.Select("SELECT * FROM"
                                + "(SELECT format_id"
                                      + ",format_type_id"
                                      + ",format_name"
                                      + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                  + " FROM format"
                                  + " WHERE format_deleted = 0 AND format_type_id != 3) AS ResultadoPaginado"
                            + " WHERE RowNumber BETWEEN " + (PageSize * this.format_options_page_index + 1)
                            + " AND " + (PageSize * (this.format_options_page_index + 1))
                            + " ORDER BY " + SortColumn + " " + SortOrder);


            /* Retorna el indicador del resultado de la operación */
            return result;
        }
        public bool Save()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;
            /* Valida que estén definidos los valores en los atributos */
            if (this.format_name != "")
            {
                /* Se verifica si se debe llevar a cabo una inserción o una actualización */
                if (this.format_id <= 0)
                {
                    result = db.Execute("INSERT INTO format (format_type_id, format_name, "
                                            + "format_description,format_deleted) " +
                                        "VALUES (" + this.format_type_id + ",'" + this.format_name + "','" +
                                            this.format_description + "'," + this.format_deleted + ")");
                }
                else
                {
                    result = db.Execute("UPDATE format SET format_name = '" + this.format_name +
                                        "', format_description = '" + this.format_description +
                                        "', format_deleted = " + this.format_deleted +
                                        " WHERE format_id=" + this.format_id);
                }
            }

            /* Retorna el indicador del resultado de la operación */
            return result;
        }
        public bool Delete()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;
            /* Valida que estén definidos los valores en los atributos */
            if (this.format_name != "")
            {
                /* Se verifica si se debe llevar a cabo una inserción o una actualización */
                if (this.format_id > 0)
                {
                    result = db.Execute("UPDATE format SET format_deleted = " + this.format_deleted +
                                        " WHERE format_id=" + this.format_id);
                }
            }

            /* Retorna el indicador del resultado de la operación */
            return result;
        }
    }
}