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
public class Role
{
    public int role_id { get; set; }
    public string role_name { get; set; }
    public string role_description { get; set; }
    public int role_deleted { get; set; }

    /* Atributos especiales para la consulta del listado de opciones paginado */
    public int role_options_record_count;
    public int role_options_page_count;
    public int role_options_page_index;

    public int assigned_users_record_count;
    public int assigned_users_page_count;
    public int assigned_users_page_index;

    /* Atributo usado para indicar un permiso relacionado con el role, en las grillas de configuracion */
    public int permission_id;


    /**
    * @abstract Este método permite obtener un dataset con el listado de las opciones de personal disponibles.
    * @param int PageSize          Cantidad de registros a presentar por página
    * @param int PageNumber        Número de página que se debe obtener
    * @param string SortColumn     Indica la columna a través de la cual se llevará a cabo el ordenamiento de la información
    * @param string SortOrder      Indica si la información de ordena de forma ascendente o descendente
    * 
    * @return DataSet retorna el dataset con el listado de opciones de roles seleccionadas.
    * */
    public DataSet getRoleList(int PageSize, int PageNumber, string SortColumn, string SortOrder)
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
            SortColumn = "role_name";
        }

        /* Validación del tipo de ordenamiento */
        if (SortOrder != "asc" && SortOrder != "desc")
        {
            SortOrder = "desc";
        }

        /* Se calcula la cantidad de registros que hacen parte del resultado */
        DataSet resultRegisterQty = db.Select("Select count(role_id) as qty FROM role WHERE role_deleted = 0");
        if (resultRegisterQty.Tables[0].Rows.Count == 1)
        {
            this.role_options_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
        }
        else
        {
            this.role_options_record_count = 0;
        }

        /* Se calcula la cantidad de páginas del resultado */
        double tempPageCount = this.role_options_record_count / (double)PageSize;
        this.role_options_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

        /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
        if (PageNumber > this.role_options_page_count)
        {
            PageNumber = this.role_options_page_count;
        }

        /* Define el indice de la página*/
        this.role_options_page_index = PageNumber - 1;

        /* Consulta el listado de registros a presentar */
        result = db.Select("SELECT * FROM"
                                + "(SELECT role_id"
                                      + ",role_name"
                                      + ",role_description"
                                      + ",role_deleted"
                                      + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                  + " FROM role"
                                  + " WHERE role_deleted = 0) AS ResultadoPaginado"
                            + " WHERE RowNumber BETWEEN " + (PageSize * this.role_options_page_index + 1)
                            + " AND " + (PageSize * (this.role_options_page_index + 1))
                            + " ORDER BY " + SortColumn + " " + SortOrder);

        /* Retorna el indicador del resultado de la operación */
        return result;
    }

    /**
    * @abstract Este método permite obtener un dataset con el listado de las opciones de personal disponibles.
    * @param int PageSize          Cantidad de registros a presentar por página
    * @param int PageNumber        Número de página que se debe obtener
    * @param string SortColumn     Indica la columna a través de la cual se llevará a cabo el ordenamiento de la información
    * @param string SortOrder      Indica si la información de ordena de forma ascendente o descendente
    * 
    * @return DataSet retorna el dataset con el listado de opciones de roles seleccionadas.
    * */
    public DataSet getAssignedUsers(int PageSize, int PageNumber, string SortColumn, string SortOrder, int role_id)
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
            SortColumn = "idusuario";
        }

        /* Validación del tipo de ordenamiento */
        if (SortOrder != "asc" && SortOrder != "desc")
        {
            SortOrder = "desc";
        }

        /* Se calcula la cantidad de registros que hacen parte del resultado */
        DataSet resultRegisterQty = db.Select("Select count(idusuario) as qty FROM role_assignment WHERE role_id = " + role_id);
        if (resultRegisterQty.Tables[0].Rows.Count == 1)
        {
            this.assigned_users_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
        }
        else
        {
            this.assigned_users_record_count = 0;
        }

        /* Se calcula la cantidad de páginas del resultado */
        double tempPageCount = this.assigned_users_record_count / (double)PageSize;
        this.assigned_users_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

        /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
        if (PageNumber > this.assigned_users_page_count)
        {
            PageNumber = this.assigned_users_page_count;
        }

        /* Define el indice de la página*/
        this.assigned_users_page_index = PageNumber - 1;

        /* Consulta el listado de registros a presentar */
        result = db.Select("SELECT * FROM"
                                + "(SELECT role_id"
                                      + ",idusuario"
                                      + ",username"
                                      + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                  + " FROM role_assignment WHERE role_id = " + role_id + ") AS ResultadoPaginado"
                            + " WHERE RowNumber BETWEEN " + (PageSize * this.assigned_users_page_index + 1)
                            + " AND " + (PageSize * (this.assigned_users_page_index + 1))
                            + " ORDER BY " + SortColumn + " " + SortOrder);

        /* Retorna el indicador del resultado de la operación */
        return result;
    }
}