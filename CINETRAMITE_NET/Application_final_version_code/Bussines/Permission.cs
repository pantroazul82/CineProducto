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
/// Clase que representa los permisos
/// </summary>
public class Permission
{
    public int permission_id { get; set; }
    public string permission_name { get; set; }
    public string permission_description { get; set; }

    /* Atributos especiales para la consulta del listado de opciones paginado */
    public int permission_options_record_count;
    public int permission_options_page_count;
    public int permission_options_page_index;

    public int assigned_roles_record_count;
    public int assigned_roles_page_count;
    public int assigned_roles_page_index;

    /**
    * @abstract Este método permite obtener un dataset con el listado de las opciones de personal disponibles.
    * @param int PageSize          Cantidad de registros a presentar por página
    * @param int PageNumber        Número de página que se debe obtener
    * @param string SortColumn     Indica la columna a través de la cual se llevará a cabo el ordenamiento de la información
    * @param string SortOrder      Indica si la información de ordena de forma ascendente o descendente
    * 
    * @return DataSet retorna el dataset con el listado de opciones de roles seleccionadas.
    * */
    public DataSet getPermissionList(int PageSize, int PageNumber, string SortColumn, string SortOrder)
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
            SortColumn = "permission_name";
        }

        /* Validación del tipo de ordenamiento */
        if (SortOrder != "asc" && SortOrder != "desc")
        {
            SortOrder = "desc";
        }

        /* Se calcula la cantidad de registros que hacen parte del resultado */
        DataSet resultRegisterQty = db.Select("Select count(permission_id) as qty FROM dboPrd.permission");
        if (resultRegisterQty.Tables[0].Rows.Count == 1)
        {
            this.permission_options_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
        }
        else
        {
            this.permission_options_record_count = 0;
        }

        /* Se calcula la cantidad de páginas del resultado */
        double tempPageCount = this.permission_options_record_count / (double)PageSize;
        this.permission_options_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

        /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
        if (PageNumber > this.permission_options_page_count)
        {
            PageNumber = this.permission_options_page_count;
        }

        /* Define el indice de la página*/
        this.permission_options_page_index = PageNumber - 1;

        /* Consulta el listado de registros a presentar */
        result = db.Select("SELECT * FROM"
                                + "(SELECT permission_id"
                                      + ",permission_name"
                                      + ",permission_description"
                                      + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                  + " FROM dboPrd.permission) AS ResultadoPaginado"
                            + " WHERE RowNumber BETWEEN " + (PageSize * this.permission_options_page_index + 1)
                            + " AND " + (PageSize * (this.permission_options_page_index + 1))
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
    public DataSet getAssignedRoles(int PageSize, int PageNumber, string SortColumn, string SortOrder, int permission_id)
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
        DataSet resultRegisterQty = db.Select("Select count(role_id) as qty FROM dboPrd.role_permission WHERE permission_id = " + permission_id);
        if (resultRegisterQty.Tables[0].Rows.Count == 1)
        {
            this.assigned_roles_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
        }
        else
        {
            this.assigned_roles_record_count = 0;
        }

        /* Se calcula la cantidad de páginas del resultado */
        double tempPageCount = this.assigned_roles_record_count / (double)PageSize;
        this.assigned_roles_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

        /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
        if (PageNumber > this.assigned_roles_page_count)
        {
            PageNumber = this.assigned_roles_page_count;
        }

        /* Define el indice de la página*/
        this.assigned_roles_page_index = PageNumber - 1;

        /* Consulta el listado de registros a presentar */
        result = db.Select("SELECT * FROM"
                                + "(SELECT role_permission.permission_id"
                                      + ",role.role_id"
                                      + ",role.role_name"
                                      + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                  + " FROM dboPrd.role_permission, role "
                                  + " WHERE role_permission.role_id = role.role_id AND permission_id = " + permission_id + ") AS ResultadoPaginado"
                            + " WHERE RowNumber BETWEEN " + (PageSize * this.assigned_roles_page_index + 1)
                            + " AND " + (PageSize * (this.assigned_roles_page_index + 1))
                            + " ORDER BY " + SortColumn + " " + SortOrder);

        /* Retorna el indicador del resultado de la operación */
        return result;
    }

    /* Esta función asigna un rol a un permiso */
    public bool assignRolePermission(int permission_id, int role_id)
    {
        bool result;
        DB db = new DB();
        result = db.Execute("INSERT INTO dboPrd.role_permission "
                    + "(role_id, permission_id)  "
                    + "VALUES (" + role_id + "," + permission_id + ")" );

        return result;
    }

    /* Esta función asigna un rol a un usuario */
    public bool deleteRolePermission(int permission_id, int role_id)
    {
        bool result;
        DB db = new DB();
        result = db.Execute("DELETE FROM dboPrd.role_permission "
                    + "WHERE role_id = " + role_id + " AND permission_id = " + permission_id);

        return result;
    }
}