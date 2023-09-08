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
public class StaffOptionDetail
{
    public int id { get; set; }
    public int staff_option_id { get; set; }
    public string position_name { get; set; }
    public int position_id { get; set; }
    public int position_qty { get; set; }
    public int position_optional_qty { get; set; }
    public bool deleted { get; set; }

    /**
    * @abstract Este método carga la informacion de una opcion definida por su id.
    * 
    * @return bool verdadero si la carga se realizó con éxito o falso en caso contrario
    * */
    public bool LoadData()
    {
        /* Hace disponible la conexión a la base de datos */
        DB db = new DB();

        /* Inicializacion de la variable que almacena el resultado a retornar */
        bool result = false;

        /* Instancia el objeto que almacenará el listado de opciones */
        DataSet staffOptionDetailQuery = new DataSet();

        /* Consulta el listado de registros a presentar */
        staffOptionDetailQuery = db.Select("SELECT staff_option_detail_id " +
                                               ",staff_option_id " +
                                               ",staff_option_detail.position_id " +
                                               ",position.position_name " +
                                               ",staff_option_detail_quantity" +
                                               ",staff_option_detail_optional_quantity" +
                                               ",staff_option_detail_deleted " +
                                            "FROM staff_option_detail, position " +
                                            "WHERE position.position_id = staff_option_detail.position_id AND " +
                                               " staff_option_detail_id = " + this.id);

        if (staffOptionDetailQuery.Tables[0].Rows.Count == 1)
        {
            result = true;
            this.staff_option_id = Convert.ToInt32(staffOptionDetailQuery.Tables[0].Rows[0]["staff_option_id"]);
            this.position_name = staffOptionDetailQuery.Tables[0].Rows[0]["position_name"].ToString();
            this.position_id = Convert.ToInt32(staffOptionDetailQuery.Tables[0].Rows[0]["position_id"]);
            this.position_qty = Convert.ToInt32(staffOptionDetailQuery.Tables[0].Rows[0]["staff_option_detail_quantity"]);
            this.position_optional_qty = Convert.ToInt32(staffOptionDetailQuery.Tables[0].Rows[0]["staff_option_detail_optional_quantity"]);

            int deletedTemp = Convert.ToInt32(staffOptionDetailQuery.Tables[0].Rows[0]["staff_option_detail_deleted"]);
            if (deletedTemp == 0)
            {
                this.deleted = false;
            }
            else
            {
                this.deleted = true;
            }
            
        }

        /* Retorna el indicador del resultado de la operación */
        return result;
    }

    /**
    * @abstract Este método guarda la informacion del objeto cargado.
    * 
    * @return bool verdadero si la grabación se realizó con éxito o falso en caso contrario
    * */
    public bool Save()
    {
        /* Hace disponible la conexión a la base de datos */
        DB db = new DB();

        /* Inicializacion de la variable que almacena el resultado a retornar */
        bool result = false;

        /* Valida que estén definidos los valores en los atributos */
        if (this.position_id > 0 && this.position_qty > 0 && this.staff_option_id > 0)
        {
            /* Se verifica si se debe llevar a cabo una inserción o una actualización */
            if (this.id <= 0)
            {
                result = db.Execute("INSERT INTO staff_option_detail (staff_option_id, position_id, staff_option_detail_quantity, staff_option_detail_optional_quantity, staff_option_detail_deleted) " +
                                    "VALUES (" + this.staff_option_id + "," + this.position_id + "," + this.position_qty + "," + this.position_optional_qty + ", 0)");
            }
            else
            {
                result = db.Execute("UPDATE staff_option_detail SET position_id = " + this.position_id + ", staff_option_detail_optional_quantity = " + this.position_optional_qty + " WHERE staff_option_detail_id=" + this.id);
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
            result = db.Execute("UPDATE staff_option_detail SET staff_option_detail_deleted = 1 WHERE staff_option_detail_id=" + this.id);
        }

        /* Retorna el indicador del resultado de la operación */
        return result;
    }
}