using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CineProducto.Bussines
{
    public class DB
    {
        private string ConnectionString;
        public DB()
        {
            // obtiene la cadena de conexión a la base de datos  
            this.ConnectionString = ConfigurationManager.ConnectionStrings["cineConnectionString"].ConnectionString;
        }
        public DataSet Select(string query)  
        {
          
                SqlDataAdapter adapter = new SqlDataAdapter(query, this.ConnectionString);
                adapter.SelectCommand.CommandTimeout = 240; 
                DataSet ds = new DataSet();   
                adapter.Fill(ds, "result");
                return ds;  
        }  
        public bool Execute(string query,List<System.Data.SqlClient.SqlParameter> lista=null)  
        {
            
                string setFormat = "SET DATEFORMAT dmy; ";
                query = setFormat + query;
            
            SqlConnection dbcon;  
            using (dbcon = new SqlConnection(this.ConnectionString))  
            {  
                // create a new SQL command on thie connection  
                SqlCommand command = new SqlCommand(query, dbcon);
                command.CommandTimeout = 240;
                // open the connection  
                dbcon.Open();
                for (int k = 0; lista != null && k < lista.Count; k++)
                {
                    command.Parameters.Add(lista[k]);
                }
                // execute the query and return the number of affected rows  
                int affectedrows = command.ExecuteNonQuery();
                dbcon.Close();
                // there were no rows affected - the command failed  
                if (affectedrows == 0)  
                {
                    return false;
                // the command affected at least one row  
                } 
                else 
                {  
                    return true;  
                }  
            }  
        }
      
        public DataSet LoadProjectAttachmentCompany( string type_company)
        {
            DB db = new DB();
            DataSet ds = new DataSet();
         
            

           ds= db.Select("SELECT attachment.attachment_id, attachment.attachment_father_id, attachment.attachment_name as attachment_name, attachment.attachment_machine_name, "
                      +"attachment.attachment_description, attachment.attachment_format, attachment.attachment_quantity, attachment.attachment_order, attachment.attachment_deleted, "
                      +"attachment_validation.validation_id, attachment_validation.attachment_id AS Expr1, attachment_validation.variable, attachment_validation.validation_type, "
                      +"attachment_validation.value, attachment_validation.operator, attachment_validation.active "
                      + "FROM  dboPrd.attachment INNER JOIN "
                      + "dboPrd.attachment_validation ON attachment.attachment_id = attachment_validation.attachment_id "
                      + "WHERE    attachment_validation.value = '"+ type_company +"'");

            return ds;

        }
        public DataSet LoadProjectAttachment_ID(string type_company,int project_id){
            DB db = new DB();
            DataSet ds = new DataSet();



            ds = db.Select("SELECT     attachment.attachment_id, attachment.attachment_father_id, attachment.attachment_name, attachment.attachment_machine_name, "
                      +"attachment.attachment_description, attachment.attachment_format, attachment.attachment_quantity, attachment.attachment_order, attachment.attachment_deleted, "
                      +"attachment_validation.validation_id, attachment_validation.attachment_id AS Expr1, attachment_validation.variable, attachment_validation.validation_type, "
                      +"attachment_validation.value, attachment_validation.operator, attachment_validation.active, project_attachment.project_attachment_id, "
                      +"project_attachment.project_attachment_attachment_id, project_attachment.project_attachment_project_id, project_attachment.project_attachment_path, "
                      +"project_attachment.project_attachment_observation, project_attachment.project_attachment_date, project_attachment.project_attachment_approved "
                      + "FROM         dboPrd.attachment INNER JOIN "
                      + "dboPrd.attachment_validation ON attachment.attachment_id = attachment_validation.attachment_id FULL OUTER JOIN "
                      + "dboPrd.project_attachment ON project_attachment.project_attachment_attachment_id = attachment.attachment_id "
                      + "WHERE     (attachment_validation.value = '"+ type_company +"') AND (project_attachment.project_attachment_project_id ="+ project_id +")");

            return ds;
    
        }
        public DataSet GetSelectOptions(string tableName, string idField, string valueField, string conditionComplement = "")
        {
            if (conditionComplement != "")
            {
                conditionComplement = " AND " + conditionComplement;
            }
            DataSet result = this.Select("SELECT " + idField + ", " + valueField + " FROM dboPrd." + tableName + " WHERE " + tableName + "_deleted=0" + conditionComplement);
            return result;
        }

        public String GetTooltip(string name)
        {
            string resultado = "";
            DataSet result = new DataSet();
            if (name != null && name != "")
            {
                result = this.Select("SELECT tooltip_text FROM dboPrd.tooltip WHERE tooltip_name='" + name + "'");
            }
            if (result.Tables[0].Rows.Count == 1)
            {
                resultado = result.Tables[0].Rows[0]["tooltip_text"].ToString();
            }

            return resultado.Replace("SUPRIMIR", "");
        }
    }
}