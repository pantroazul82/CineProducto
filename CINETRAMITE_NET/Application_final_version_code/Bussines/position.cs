using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CineProducto.Bussines
{
    public class position
    {
        public int position_id;
        public int position_father_id;
        public string position_name;
        public string position_description;
        public int page_count;
        public int page_index;
        public int record_count;
        public int position_deleted;

        public position(){
            
        }

        public void loadTopPositions(int page_size = 10, int page_number = 1, string SortColumn = "position_id", string SortOrder = "desc")
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            result = db.Select("select * from (  SELECT position_id, position_name, position_description "
                + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                + " FROM position "
                                + " WHERE position_father_id = '0' AND "
                                + " position_deleted='0' "
                               
                                + ") st "
                                );
            double tempPageCount = (double)Convert.ToInt32(result.Tables[0].Rows.Count) / page_size;
            this.record_count = Convert.ToInt32(result.Tables[0].Rows.Count);
            this.page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));
            if (page_number > this.page_count)
            {
                page_number = this.page_count;
            }
            this.page_index = page_number - 1;
            /* Retorna el indicador del resultado de la operación */
        }

        public DataSet getChildPositions(int position_id, int page_size = 10, int page_number = 1, string SortColumn = "position_id", string SortOrder = "desc")
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            if (position_id > 0)
            {
                result = db.Select(
                    "select * from ("+
                    "SELECT position_id, position_father_id, position_name, position_description "
                    + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                    + " FROM position "
                                    + " WHERE position_father_id = '" + position_id + "' AND "
                                    + " position_deleted='0'"
                                    + ") st where  "
                                +" RowNumber BETWEEN " + (page_size * this.page_index + 1)
                                + " AND " + (page_size * (this.page_index + 1))
                             );
                double tempPageCount = (double)Convert.ToInt32(result.Tables[0].Rows.Count) / page_size;
                this.record_count = Convert.ToInt32(result.Tables[0].Rows.Count);
                this.page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));
                if (page_number > this.page_count)
                {
                    page_number = this.page_count;
                }
                this.page_index = page_number - 1;
            }
            /* page_count definition */

            

            /* Retorna el indicador del resultado de la operación */
            return result;
        }

        /* Esta función obtiene los cargos de primer nivel */
        public DataSet getTopPositions(int page_size = 10, int page_number = 1, string SortColumn = "position_id", string SortOrder = "desc")
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            DataSet result = new DataSet();
            result = db.Select(
                   "select * from (" +
                "SELECT position_id, position_father_id, position_name, position_description "
                   + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                 
                                + " FROM position "
                                + " WHERE position_father_id = '0' AND "
                                + " position_deleted='0'"
                              

                                +") st where  "
                                + " RowNumber BETWEEN " + (page_size * this.page_index + 1)
                                + " AND " + (page_size * (this.page_index + 1)));
            double tempPageCount = (double)Convert.ToInt32(result.Tables[0].Rows.Count) / page_size;
           // this.record_count = Convert.ToInt32(result.Tables[0].Rows.Count);
        //    this.page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));
            if (page_number > this.page_count)
            {
                page_number = this.page_count;
            }
           // this.page_index = page_number - 1;
            /* Retorna el indicador del resultado de la operación */
            return result;
        }

        public void save() {
            DB db = new DB();
            DataSet ds = db.Select("SELECT position_id "
                                 + "FROM position WHERE position_id="+this.position_id);
            string query = "";
            if (ds.Tables[0].Rows.Count == 1)
            {
                query = "UPDATE position SET position_name = '" + this.position_name + "' , position_description = '" + this.position_description + "', position_father_id = '" + this.position_father_id + "' , position_deleted = "+this.position_deleted+" WHERE position_id ="+this.position_id;
            }
            else
            {
                query = "INSERT INTO position (position_name, position_description, position_father_id, position_deleted) VALUES ('" + this.position_name + "','" + this.position_description + "', " + this.position_father_id + ", " + this.position_deleted + ")";
            }
            db.Execute(query);
        }
        public bool Delete()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;

            /* Se verifica si se debe llevar a cabo una inserción o una actualización */
            if (this.position_id > 0)
            {
                result = db.Execute("UPDATE position SET position_deleted = 1 WHERE position_id=" + this.position_id);
            }

            /* Retorna el indicador del resultado de la operación */
            return result;
        }
    }
}