using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;

namespace CineProducto.Bussines
{
    /* Clase que modela un adjunto y las funciones de apoyo */
    public class Attachment
    {
        public int attachment_id;
        public int attachment_father_id;
        public string attachment_name;
        public string attachment_machine_name;
        public string attachment_description;
        public string attachment_format;
        public string attachment_required;
        public int attachment_quantity;
        public int attachment_order;
        public int attachment_foreing_producer;
        public string attachment_to_foreing_producer;
        public Byte attachment_deleted;

        /* Atributos para la grilla de administración de adjuntos */
        public int attachment_options_record_count;
        public int attachment_options_page_count;
        public int attachment_options_page_index;
        public string attachment_section;  //Nombre de la sección a la cual pertenece el adjunto, la cual debe ser un adjunto con father_id=0
        public string tooltip;
        /* Constructor de la clase Attachment */
        public Attachment(int attachment_id = 0, string attachment_required = "")
        {
            /* Incialización de atributos con los valores por defecto */
            this.attachment_id = 0;

            /* Validación del parámetro y carga de información del objeto */
            if (attachment_id != 0)
            {
                LoadAttachment(attachment_id,attachment_required);
            }
        }

        public void LoadAttachment(int attachment_id, string attachment_required = "")
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT attachment_id, attachment_father_id, "
                                 + "attachment_name, attachment_machine_name, "
                                 + "attachment_description, attachment_format, "
                                 + "attachment_quantity, attachment_order, "
                                 + "attachment_deleted, "
                                 + "attachment_foreing_producer,tooltip "
                                 + "FROM attachment WHERE attachment_id=" + attachment_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.attachment_id = (int)ds.Tables[0].Rows[0]["attachment_id"];
                this.attachment_father_id = ds.Tables[0].Rows[0]["attachment_father_id"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["attachment_father_id"]) : 0;
                this.attachment_name = ds.Tables[0].Rows[0]["attachment_name"].ToString() != "" ? ds.Tables[0].Rows[0]["attachment_name"].ToString() : "";
                this.attachment_machine_name = ds.Tables[0].Rows[0]["attachment_machine_name"].ToString() != "" ? ds.Tables[0].Rows[0]["attachment_machine_name"].ToString() : "";
                this.attachment_description = ds.Tables[0].Rows[0]["attachment_description"].ToString() != "" ? ds.Tables[0].Rows[0]["attachment_description"].ToString() : "";
                this.attachment_format = ds.Tables[0].Rows[0]["attachment_format"].ToString() != "" ? ds.Tables[0].Rows[0]["attachment_format"].ToString() : "";
                this.attachment_quantity = ds.Tables[0].Rows[0]["attachment_quantity"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["attachment_quantity"]) : 0;
                this.attachment_order = ds.Tables[0].Rows[0]["attachment_order"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["attachment_order"]) : 0;
                this.attachment_deleted = ds.Tables[0].Rows[0]["attachment_deleted"].ToString() != "" ? Convert.ToByte(ds.Tables[0].Rows[0]["attachment_deleted"]) : Convert.ToByte(0);
                this.attachment_foreing_producer = ds.Tables[0].Rows[0]["attachment_foreing_producer"].ToString() != "" ? Convert.ToInt32(ds.Tables[0].Rows[0]["attachment_foreing_producer"]) : 0;
                this.tooltip = (ds.Tables[0].Rows[0]["tooltip"] == System.DBNull.Value || ds.Tables[0].Rows[0]["tooltip"] == null) ? "" : ds.Tables[0].Rows[0]["tooltip"].ToString().Trim();
                this.attachment_required = attachment_required;
            }
        }

        public bool Save()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializacion de la variable que almacena el resultado a retornar */
            bool result = false;

            /* Incialización de variables de ayuda para la inerción */
            string save_format_query_value = "'" + this.attachment_format + "'";
            string save_order_query_value = "'" + this.attachment_order + "'";
            string save_quantity_query_value = "'" + this.attachment_quantity + "'";
            string save_deleted_query_value = "'" + this.attachment_deleted + "'";

            /* Valida que estén definidos los valores en los atributos */
            if (this.attachment_name != "" && this.attachment_father_id >= 0)
            {
                /* Antes de hacer la grabación en la base de datos se incluyen valores por defecto para los atributos vacios */
                if(this.attachment_machine_name == null || this.attachment_machine_name == "") //Si no esta definido el nombre de máquina
                {
                    this.attachment_machine_name = this.attachment_name;
                    Array.ForEach(System.IO.Path.GetInvalidFileNameChars(),
                    c => this.attachment_machine_name = this.attachment_machine_name.Replace(c.ToString(), "-"));
                    this.attachment_machine_name = this.attachment_machine_name.ToLower();
                    this.attachment_machine_name = this.attachment_machine_name.Replace(" ", "-");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("(", String.Empty);
                    this.attachment_machine_name = this.attachment_machine_name.Replace(")", String.Empty);
                    this.attachment_machine_name = this.attachment_machine_name.Replace("á", "a");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("é", "e");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("í", "i");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("ó", "o");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("ú", "u");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("ñ", String.Empty);
                    this.attachment_machine_name = this.attachment_machine_name.Replace("à", "a");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("è", "e");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("ì", "i");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("ò", "o");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("ù", "u");
                    this.attachment_machine_name = this.attachment_machine_name.Replace("ç", String.Empty);
                }

                if (this.attachment_description == null || this.attachment_description == "") //Si no esta definida la descripción
                {
                    this.attachment_description = this.attachment_name;
                }

                if (this.attachment_format == null || this.attachment_format == "") //Si no esta definida el formato
                {
                    save_format_query_value = "null";
                }

                if (this.attachment_order < 0) //Si no esta definida el orden
                {
                    save_order_query_value = Convert.ToString("0");
                }

                if (this.attachment_quantity < 0) //Si no esta definida la cardinalidad
                {
                    save_quantity_query_value = Convert.ToString("1");
                }

                if (this.attachment_deleted < 0 || this.attachment_deleted > 1) //Si no esta definida la cardinalidad
                {
                    save_deleted_query_value = Convert.ToString("0");
                }

                /* Se verifica si se debe llevar a cabo una inserción o una actualización */
                if (this.attachment_id <= 0)
                {
                    result = db.Execute("INSERT INTO attachment (attachment_father_id, attachment_name, attachment_machine_name, "
                                            + "attachment_description,attachment_format,attachment_quantity,attachment_order,attachment_deleted,attachment_foreing_producer) " +
                                        "VALUES ('" + this.attachment_father_id + "','" + this.attachment_name + "','" + this.attachment_machine_name + "','" +
                                            this.attachment_description + "'," + save_format_query_value + "," + save_quantity_query_value +
                                            "," + save_order_query_value + "," + save_deleted_query_value + ","+this.attachment_foreing_producer+")");
                }
                else
                {
                    result = db.Execute("UPDATE attachment SET " +
                                           "attachment_father_id = '" + this.attachment_father_id +
                                        "', attachment_name = '" + this.attachment_name +
                                        "', attachment_machine_name = '" + this.attachment_machine_name +
                                        "', attachment_description = '" + this.attachment_description +
                                        "', attachment_format = " + save_format_query_value +
                                        ", attachment_quantity = " + save_quantity_query_value +
                                        ", attachment_order = " + save_order_query_value +
                                        ", attachment_deleted = " + save_deleted_query_value +
                                        ", attachment_foreing_producer = " + this.attachment_foreing_producer +
                                        " WHERE attachment_id=" + this.attachment_id);
                }
            }

            /* Retorna el indicador del resultado de la operación */
            return result;
        }

        /* Función que obtiene el listado de adjuntos que aplican para un proyecto en particular */
        public List<Attachment> GetAttachmentList(Project  project)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Se inicializa la lista de adjuntos */
            List<Attachment> attachment = new List<Attachment>();

            /* Si esta definido un project_id se hace la consulta de los productores */
            if (project != null)
            {
                /* Consulta las secciones de adjuntos, id padre = 0 */
                string queryAttachment = "SELECT attachment_id FROM attachment WHERE attachment_deleted = 0 AND "
                                        +" attachment_father_id=0 ORDER BY attachment_order asc";

                DataSet queryAttachmentDS = db.Select(queryAttachment);

                for (int i = 0; i < queryAttachmentDS.Tables[0].Rows.Count; i++)
                {
                    Attachment newAttachment = new Attachment();
                    newAttachment.LoadAttachment((int)queryAttachmentDS.Tables[0].Rows[i]["attachment_id"]);
                    if (newAttachment.partOfProject(project) != "")
                    {
                        attachment.Add(newAttachment);
                    }

                    /* Consulta los adjuntos que hacen parte de la sección actual */
                    string queryAttachmentChild = "SELECT attachment_id FROM attachment WHERE attachment_deleted = 0 AND "
                                        + " attachment_father_id=" + queryAttachmentDS.Tables[0].Rows[i]["attachment_id"] 
                                        + " ORDER BY attachment_order asc";

                    DataSet queryAttachmentChildDS = db.Select(queryAttachmentChild);
                
                    for (int j = 0; j < queryAttachmentChildDS.Tables[0].Rows.Count; j++)
                    {
                        Attachment newChildAttachment = new Attachment();
                        newChildAttachment.LoadAttachment((int)queryAttachmentChildDS.Tables[0].Rows[j]["attachment_id"]);
                        string resultFunction = newChildAttachment.partOfProject(project);
                        if (resultFunction != "" && resultFunction != "excluded" && resultFunction != null)
                        {
                            newChildAttachment.LoadAttachment((int)queryAttachmentChildDS.Tables[0].Rows[j]["attachment_id"],resultFunction);
                            attachment.Add(newChildAttachment);
                        }
                    }
                }
            }

            return attachment;
        }

        /* Función que obtiene el listado de adjuntos que aplican para un proyecto en particular */
        public List<Attachment> GetAttachmentListByConsult(Project project, int father_id, int producer_id = 0, bool typeProducer = true)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Se inicializa la lista de adjuntos */
            List<Attachment> attachment = new List<Attachment>();

            /* Si esta definido un project_id se hace la consulta de los productores */
            if (project != null)
            {
                /* Consulta las secciones de adjuntos, id padre = 0 */

                string conditionForeingProducer = "";
                if (!typeProducer)
                {
                    conditionForeingProducer = "AND attachment_foreing_producer = 1";
                }

                Attachment newAttachment = new Attachment();
                newAttachment.LoadAttachment((int)father_id);

                /* Consulta los adjuntos que hacen parte de la sección actual */

                string queryAttachmentChild = "SELECT attachment_id FROM attachment WHERE attachment_deleted = 0 AND "
                                    + " attachment_father_id=" + father_id
                                    + " " + conditionForeingProducer
                                    + " ORDER BY attachment_order asc";

                DataSet queryAttachmentChildDS = db.Select(queryAttachmentChild);

                for (int j = 0; j < queryAttachmentChildDS.Tables[0].Rows.Count; j++)
                {
                    Attachment newChildAttachment = new Attachment();
                    newChildAttachment.LoadAttachment((int)queryAttachmentChildDS.Tables[0].Rows[j]["attachment_id"]);
                    string resultFunction = newChildAttachment.partOfProject(project, producer_id);
                    if (resultFunction != "" && resultFunction != "excluded" && resultFunction != null)
                    {
                        newChildAttachment.LoadAttachment((int)queryAttachmentChildDS.Tables[0].Rows[j]["attachment_id"], resultFunction);
                        attachment.Add(newChildAttachment);
                    }
                }
            }

            return attachment;
        }


        public string quitarAcento(string valor) 
        {
            return valor.ToLower().Trim().Replace("persona", "").Trim().Replace("á", "a")
                        .Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").ToUpper();
        }

        public string partOfProject(Project projectCompare, int producer_id = 0)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();
            /* Variable que almacena el tipo de validación y que es retornada por el método */
            string result = "";
            /* Variables usadas para la validación */
            string validation_variable = "";
            string validation_value = "";
            string validation_operator = "";
            string validation_type = "";
            string producer_company_type = "";
            string producer_type = "";
            long total_cost = 0;

            /* Verficamos si el adjunto hace parte de alguna validación */
            DataSet ds = db.Select("SELECT attachment_id, variable, "
                                 + "validation_type, value, "
                                 + "operator "
                                 + "FROM attachment_validation "
                                 + "WHERE attachment_id=" + this.attachment_id.ToString()
                                 + " AND active = 1 order by prioridad desc");

            if (ds.Tables[0].Rows.Count >= 1)
            {
                total_cost = projectCompare.getTotalCost();
                /* Buscamos el objeto del productor que hace la solicitud */
                Producer requesterProducer = new Producer(producer_id);
                if (producer_id != 0)
                {
                    #region cargamos la informacion del tipo de productor
                    if (requesterProducer.person_type_id == 1)
                    {
                        producer_type = "NATURAL";
                    }
                    else if (requesterProducer.person_type_id == 2)
                    {
                        producer_type = "JURIDICA";
                        if (requesterProducer.producer_company_type_id == 1)
                        {
                            producer_company_type = "LTDA";
                        }
                        else if (requesterProducer.producer_company_type_id == 2)
                        {
                            producer_company_type = "SAS";
                        }
                        else if (requesterProducer.producer_company_type_id == 3)
                        {
                            producer_company_type = "SA";
                        }
                        else if (requesterProducer.producer_company_type_id == 4)
                        {
                            producer_company_type = "EU";
                        }
                        else if (requesterProducer.producer_company_type_id == 5)
                        {
                            producer_company_type = "SIN ANIMO DE LUCRO";
                        }

                        if (requesterProducer.producer_company_type_id == 2)
                        {
                            producer_company_type = "";
                        }
                    }
                    #endregion
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    validation_variable = ds.Tables[0].Rows[i]["variable"].ToString() != "" ? ds.Tables[0].Rows[i]["variable"].ToString() : "";
                    validation_value = ds.Tables[0].Rows[i]["value"].ToString() != "" ? ds.Tables[0].Rows[i]["value"].ToString() : "";
                    //en validation value viene lo que digitaron en la pagina, por ejemplo Persona Natural, Persona Juridica
                    //pero las comparaciones se hacen solo con estas opciones. natural,juridica,LTDA
                    //entonces ponemos una sustitucion para que soporte los ajustes
                    validation_value = quitarAcento(validation_value);
                    validation_operator = ds.Tables[0].Rows[i]["operator"].ToString() != "" ? ds.Tables[0].Rows[i]["operator"].ToString() : "";
                    validation_type = ds.Tables[0].Rows[i]["validation_type"].ToString() != "" ? ds.Tables[0].Rows[i]["validation_type"].ToString() : "";


                    switch (validation_variable)
                    {
                        case "producer_type":
                            #region zona de tipo de productor
                            switch (validation_operator)
                            {
                                case "=":
                                    if (quitarAcento(producer_type) == validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case "<>":
                                    if (quitarAcento(producer_type) != validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case "total_cost":
                            #region zona de costo total
                            switch (validation_operator)
                            {
                                case "=":
                                    if (total_cost == Convert.ToInt32(validation_value))
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case ">":
                                    if (total_cost > Convert.ToInt32(validation_value))
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case "<":
                                    if (total_cost < Convert.ToInt32(validation_value))
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case "project_genre":
                            #region zona de genero
                            switch (validation_operator)
                            {
                                case "=":
                                    if (quitarAcento(projectCompare.project_genre_name) == validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case "<>":
                                    if (quitarAcento(projectCompare.project_genre_name) != validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case "domestic_producers_qty":
                            #region zona de cantidad de productores
                            switch (validation_operator)
                            {
                                case "=":
                                    if (projectCompare.project_domestic_producer_qty == Convert.ToInt32(validation_value))
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case ">":
                                    if (projectCompare.project_domestic_producer_qty > Convert.ToInt32(validation_value))
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case "<":
                                    if (projectCompare.project_domestic_producer_qty < Convert.ToInt32(validation_value))
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case "type_company":
                            #region zona de tipo de compañia
                            switch (validation_operator)
                            {
                                case "=":
                                    if (quitarAcento(producer_company_type) == validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case "<>":
                                    if (quitarAcento(producer_type) != validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case "production_type":
                            #region tipo de produccion
                            switch (validation_operator)
                            {
                                case "=":
                                    if (quitarAcento(projectCompare.production_type_name) == validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case "<>":
                                    if (quitarAcento(projectCompare.production_type_name) != validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case "project_type":
                            #region validacion de operador
                            switch (validation_operator)
                            {
                                case "=":
                                    if (quitarAcento(projectCompare.project_type_name) == validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                case "<>":
                                    if (quitarAcento(projectCompare.project_type_name) != validation_value)
                                    {
                                        result = validation_type;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;

                        default:
                            break;
                    }

                }
            }
            else
            {
                result = "required";
            }

            if (result == "")
            {
                result = "required";
            }
            return result;
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
        public DataSet getAttachmentList(int PageSize, int PageNumber, string SortColumn, string SortOrder)
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
                SortColumn = "attachment_father_id";
            }

            /* Validación del tipo de ordenamiento */
            if (SortOrder != "asc" && SortOrder != "desc")
            {
                SortOrder = "desc";
            }

            /* Se calcula la cantidad de registros que hacen parte del resultado */
            DataSet resultRegisterQty = db.Select("Select count(attachment_id) as qty FROM attachment_view ");
                
                //db.Select("Select count(attachment_id) as qty FROM attachment WHERE attachment_deleted = 0");
            if (resultRegisterQty.Tables[0].Rows.Count == 1)
            {
                this.attachment_options_record_count = Convert.ToInt32(resultRegisterQty.Tables[0].Rows[0]["qty"]);
            }
            else
            {
                this.attachment_options_record_count = 0;
            }

            /* Se calcula la cantidad de páginas del resultado */
            double tempPageCount = this.attachment_options_record_count / (double)PageSize;
            this.attachment_options_page_count = Convert.ToInt32(Math.Ceiling(tempPageCount));

            /* Valida que la página solicitada no sea mayor al numero de páginas disponibles */
            if (PageNumber > this.attachment_options_page_count)
            {
                PageNumber = this.attachment_options_page_count;
            }

            /* Define el indice de la página*/
            this.attachment_options_page_index = PageNumber - 1;

            /* Consulta el listado de registros a presentar */
            result = db.Select("SELECT * FROM"
                                    + "(SELECT attachment_id"
                                          + ",attachment_name"
                                          + ",attachment_description"
                                          + ",attachment_section"
                                          + ",attachment_format"
                                          + ",attachment_order"
                                          + ",attachment_foreing_producer"
                                          + ",ROW_NUMBER() OVER(ORDER BY " + SortColumn + " " + SortOrder + ") AS RowNumber"
                                      + " FROM attachment_view) "
                                + " AS ResultadoPaginado"
                                + " WHERE RowNumber BETWEEN " + (PageSize * this.attachment_options_page_index + 1)
                                + " AND " + (PageSize * (this.attachment_options_page_index + 1))
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
            if (this.attachment_id > 0)
            {
                result = db.Execute("UPDATE attachment SET attachment_deleted = 1 WHERE attachment_id=" + this.attachment_id);
            }

            /* Retorna el indicador del resultado de la operación */
            return result;
        }
    }
}