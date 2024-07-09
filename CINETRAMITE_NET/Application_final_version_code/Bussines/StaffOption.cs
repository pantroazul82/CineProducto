using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CineProducto.Bussines;

/// <summary>
/// Descripción breve de Opcion de personal
/// </summary>
public class StaffOption
{
    public int id { get; set; }
    public int project_type_id { get; set; }
    public string project_type_name { get; set; }
    public int production_type_id { get; set; }
    public string production_type_name { get; set; }
    public string project_genre_name { get; set; }
    public int project_genre_id { get; set; }
    public string has_domestic_director_description { get; set; }
    public int has_domestic_director { get; set; }
    public string description { get; set; }
    public string staff_option_personal_option { get; set; }
    public int staff_option_percentage_init { get; set; }
    public int staff_option_percentage_end { get; set; }

    /* Atributos especiales para la consulta del listado de opciones paginado */
    public int staff_options_record_count;
    public int staff_options_page_count;
    public int staff_options_page_index;


    /**
    * @abstract Este método permite obtener un dataset con el listado de las opciones de personal disponibles.
    * @param int PageSize          Cantidad de registros a presentar por página
    * @param int PageNumber        Número de página que se debe obtener
    * @param string SortColumn     Indica la columna a través de la cual se llevará a cabo el ordenamiento de la información
    * @param string SortOrder      Indica si la información de ordena de forma ascendente o descendente
    * 
    * @return DataSet retorna el dataset con el listado de opciones de personal seleccionadas.
    * */
    public DataSet getStaffOptionList(int PageSize, int PageNumber, string SortColumn, string SortOrder)
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
            SortColumn = "project_type_name";
        }

        /* Validación del tipo de ordenamiento */
        if (SortOrder != "asc" && SortOrder != "desc")
        {
            SortOrder = "desc";
        }

        /* Se calcula la cantidad de registros que hacen parte del resultado */
        DataSet resultRegisterQty = db.Select("Select count(staff_option_id) as qty FROM dboPrd.staff_option WHERE staff_option_deleted = 0");
        if (resultRegisterQty.Tables[0].Rows.Count == 1)
        {
            this.staff_options_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
        }
        else
        {
            this.staff_options_record_count = 0;
        }

        /* Se calcula la cantidad de páginas del resultado */
        double tempPageCount = (double)this.staff_options_record_count/PageSize;
        this.staff_options_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

        /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
        if (PageNumber > this.staff_options_page_count)
        {
            PageNumber = this.staff_options_page_count;
        }

        /* Define el indice de la página*/
        this.staff_options_page_index = PageNumber - 1;

        /* Consulta el listado de registros a presentar */
        result = db.Select("SELECT * FROM"
                                + "(SELECT staff_option_id"
                                      + ",project_type.project_type_name"
                                      + ",production_type.production_type_name"
                                      + ",project_genre.project_genre_name"
                                      + ",staff_option_has_domestic_director"
                                      + ",staff_option_description"
                                      + ",staff_option_personal_option"
                                      + ",staff_option_percentage_init"
                                      + ",staff_option_percentage_end"
                                      + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                  + " FROM dboPrd.staff_option, dboPrd.project_type, dboPrd.production_type, dboPrd.project_genre"
                                  + " WHERE staff_option.project_type_id = project_type.project_type_id AND"
                                        + " staff_option.production_type_id = production_type.production_type_id AND"
                                        + " staff_option.project_genre_id = project_genre.project_genre_id AND"
                                        + " staff_option.staff_option_deleted = 0) AS ResultadoPaginado"
                            + " WHERE RowNumber BETWEEN " + (PageSize * this.staff_options_page_index + 1)
                            + " AND " + (PageSize * (this.staff_options_page_index + 1))
                            + " ORDER BY " + SortColumn + " " + SortOrder);

        /* Retorna el indicador del resultado de la operación */
        return result;
    }

    /**
    * @abstract Este método permite obtener un dataset con el listado de los cargos y cantidades de una opción de personal particular.
    * @param int PageSize          Cantidad de registros a presentar por página
    * @param int PageNumber        Número de página que se debe obtener
    * @param string SortColumn     Indica la columna a través de la cual se llevará a cabo el ordenamiento de la información
    * @param string SortOrder      Indica si la información de ordena de forma ascendente o descendente
    * @param int StaffOptionId     Identificador de la opción de personal de la cual se consultarán los cargos y cantidades asociados
    * 
    * @return DataSet retorna el dataset con el listado de los cargos y cantidades de una opción de personal indicada.
    * */
    public DataSet getStaffOptionDetail(int PageSize, int PageNumber, string SortColumn, string SortOrder, int StaffOptionId,string version )
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
            SortColumn = "position_name";
        }

        /* Validación del tipo de ordenamiento */
        if (SortOrder != "asc" && SortOrder != "desc")
        {
            SortOrder = "desc";
        }

        /* Se calcula la cantidad de registros que hacen parte del resultado */
        DataSet resultRegisterQty = db.Select("Select count(position_id) as qty FROM dboPrd.staff_option_detail WHERE staff_option_detail_deleted = 0 AND version=" + version +" AND staff_option_id = " + StaffOptionId);
        if (resultRegisterQty.Tables[0].Rows.Count == 1)
        {
            this.staff_options_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
        }
        else
        {
            this.staff_options_record_count = 0;
        }

        /* Se calcula la cantidad de páginas del resultado */
        double tempPageCount = (double)this.staff_options_record_count / PageSize;
        this.staff_options_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

        /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
        if (PageNumber > this.staff_options_page_count)
        {
            PageNumber = this.staff_options_page_count;
        }

        /* Define el indice de la página*/
        this.staff_options_page_index = PageNumber - 1;

        /* Consulta el listado de registros a presentar */
        result = db.Select("SELECT * FROM "
                                + "(SELECT staff_option_detail_id, staff_option_id, position_name"
                                      + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                + " FROM dboPrd.staff_option_detail, dboPrd.position "
                                + " WHERE staff_option_detail_deleted = 0 AND staff_option_detail.position_id = position.position_id "
                                + " AND version = " + version
                                + " AND staff_option_id = " + StaffOptionId + ") AS ResultadoPaginado"
                            + " WHERE RowNumber BETWEEN " + (PageSize * this.staff_options_page_index + 1)
                            + " AND " + (PageSize * (this.staff_options_page_index + 1))                            
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
        if (this.project_type_id > 0 && this.production_type_id > 0 && this.project_genre_id > 0 && this.has_domestic_director >= 0 && this.description != "")
        {
            /* Se verifica si se debe llevar a cabo una inserción o una actualización */
            if (this.id <= 0)
            {
                result = db.Execute("INSERT INTO dboPrd.staff_option (project_type_id, production_type_id, project_genre_id, staff_option_has_domestic_director, staff_option_description, staff_option_deleted,staff_option_personal_option,staff_option_percentage_init,staff_option_percentage_end) " +
                                    "VALUES ('" + this.project_type_id + "','" + this.production_type_id + "','" + this.project_genre_id + "','" + this.has_domestic_director + "','" + this.description + "', 0 ,'" + this.staff_option_personal_option + "','" + this.staff_option_percentage_init + "','" + this.staff_option_percentage_end + "' )");
            }
            else
            {
                result = db.Execute("UPDATE dboPrd.staff_option SET project_type_id = '" + this.project_type_id +
                                    "', production_type_id = '" + this.production_type_id +
                                    "', project_genre_id = '" + this.project_genre_id +
                                    "', staff_option_has_domestic_director = '" + this.has_domestic_director +
                                    "', staff_option_description = '" + this.description +
                                    "', staff_option_personal_option = '" + this.staff_option_personal_option +
                                    "', staff_option_percentage_init = '" + this.staff_option_percentage_init+
                                    "', staff_option_percentage_end = '" + this.staff_option_percentage_end +
                                    "' WHERE staff_option_id=" + this.id);
            }
        }

        /* Retorna el indicador del resultado de la operación */
        return result;
    }

    /**
   * @abstract Este método elimina el registro (se marca como eliminado).
   * 
   * @return bool verdadero si la grabación se realizó con éxito o falso en caso contrario
   * */
    public bool Delete()
    {
        /* Hace disponible la conexión a la base de datos */
        DB db = new DB();

        /* Inicializacion de la variable que almacena el resultado a retornar */
        bool result = false;

        /* Se verifica si se debe llevar a cabo una inserción o una actualización */
        if (this.id > 0)
        {
            result = db.Execute("UPDATE dboPrd.staff_option SET staff_option_deleted = 1 WHERE staff_option_id=" + this.id);
        }

        /* Retorna el indicador del resultado de la operación */
        return result;
    }
}
