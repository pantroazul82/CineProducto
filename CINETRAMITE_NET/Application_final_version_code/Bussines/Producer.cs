using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;
using DominioCineProducto.utils;

namespace CineProducto.Bussines
{
    /* Clase que modela los productores con todos sus atributos */
    public class Producer
    {
        public int producer_id;
        public int person_type_id;
        public int id_etnia;
        public int id_genero;
        public int identification_type_id;
        public string producer_name;
        public string producer_nit;
        public int producer_nit_dig_verif;
        public int producer_company_type_id;
        public int producer_type_id;
        public string producer_identification_number;
        public string producer_firstname;
        public string producer_firstname2;
        public string producer_lastname;
        public string producer_lastname2;
        public string producer_localization_id;
        public string productor_localizacion_contacto_id;
        public string producer_localization_father_id;
        public string productor_localizacion_contacto_id_padre;
        public string producer_country;
        public string productor_pais_contacto;
        public string producer_city;
        public string productor_ciudad_contacto;
        public string producer_address;
        public string producer_phone;
        public string producer_fax;
        public string producer_movil;
        public string producer_email;
        public string producer_website;
        public decimal participation_percentage;
        public int requester;
        public int producer_user_id = 0;
        public int id_grupo_poblacional;
        public DateTime? fecha_nacimiento;
        public string abreviatura;
        public string primer_nombre_sup;
        public string segundo_nombre_sup;
        public string primer_apellido_sup;
        public string segundo_apellido_sup;
                        

        /* Constructor de la clase Producer */
        public Producer(int producer_id = 0)
        {
            if(producer_id != 0)
            {
                LoadProducer(producer_id); 
            }
        }

        public bool isUser(int user_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT producer_id FROM dboPrd.producer WHERE producer_user_id=" + user_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                return true;
            }
            else
                return false;
        }

        public void LoadProducerByUserClon(int user_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT producer_id, person_type_id, id_genero, id_etnia, identification_type_id, "
                                 + "producer_name, producer_nit, producer_nit_dig_verif, producer_company_type_id, "
                                 + "producer_type_id, producer_identification_number, "
                                 + "producer_firstname, producer_firstname2, producer_lastname, producer_lastname2, producer_localization_id, "
                                 + "id_grupo_poblacional, fecha_nacimiento,abreviatura, primer_nombre_sup,segundo_nombre_sup,primer_apellido_sup,segundo_apellido_sup,"
                                 + "producer_country, producer_city, producer_address, producer_phone, productor_pais_contacto, productor_ciudad_contacto, productor_localizacion_contacto_id,"
                                 + "producer_fax, producer_movil, producer_email, producer_website, producer_user_id "
                                 + "FROM dboPrd.producer WHERE producer_user_id=" + user_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.producer_id = 0;// (int)ds.Tables[0].Rows[0]["producer_id"];
                this.person_type_id = ds.Tables[0].Rows[0]["person_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["person_type_id"] : 0;
                this.id_genero = ds.Tables[0].Rows[0]["id_genero"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_genero"] : 0;
                
                this.id_etnia = ds.Tables[0].Rows[0]["id_etnia"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_etnia"] : 0;
                this.identification_type_id = ds.Tables[0].Rows[0]["identification_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["identification_type_id"] : 0;
                this.producer_name = ds.Tables[0].Rows[0]["producer_name"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_name"].ToString() : "";
                this.producer_nit = ds.Tables[0].Rows[0]["producer_nit"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_nit"].ToString() : "";
                this.producer_nit_dig_verif = ds.Tables[0].Rows[0]["producer_nit_dig_verif"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_nit_dig_verif"] : 0;                
                this.producer_company_type_id = ds.Tables[0].Rows[0]["producer_company_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_company_type_id"] : 0;
                this.producer_type_id = ds.Tables[0].Rows[0]["producer_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_type_id"] : 0;
                this.producer_identification_number = ds.Tables[0].Rows[0]["producer_identification_number"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_identification_number"].ToString() : "";
                this.producer_firstname = ds.Tables[0].Rows[0]["producer_firstname"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_firstname"].ToString() : "";
                this.producer_firstname2 = ds.Tables[0].Rows[0]["producer_firstname2"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_firstname2"].ToString() : "";
                this.producer_lastname = ds.Tables[0].Rows[0]["producer_lastname"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_lastname"].ToString() : "";
                this.producer_lastname2 = ds.Tables[0].Rows[0]["producer_lastname2"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_lastname2"].ToString() : "";

                this.id_grupo_poblacional = ds.Tables[0].Rows[0]["id_grupo_poblacional"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_grupo_poblacional"] : 0;
                this.abreviatura = ds.Tables[0].Rows[0]["abreviatura"].ToString() != "" ? ds.Tables[0].Rows[0]["abreviatura"].ToString() : "";
                this.primer_nombre_sup = ds.Tables[0].Rows[0]["primer_nombre_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["primer_nombre_sup"].ToString() : "";
                this.segundo_nombre_sup = ds.Tables[0].Rows[0]["segundo_nombre_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["segundo_nombre_sup"].ToString() : "";
                this.primer_apellido_sup = ds.Tables[0].Rows[0]["primer_apellido_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["primer_apellido_sup"].ToString() : "";
                this.segundo_apellido_sup = ds.Tables[0].Rows[0]["segundo_apellido_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["segundo_apellido_sup"].ToString() : "";

                if (ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "")
                    this.fecha_nacimiento = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString());
                else
                    this.fecha_nacimiento = null;


                this.producer_localization_id = ds.Tables[0].Rows[0]["producer_localization_id"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_localization_id"].ToString() : "";
                this.productor_localizacion_contacto_id = ds.Tables[0].Rows[0]["productor_localizacion_contacto_id"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_localizacion_contacto_id"].ToString() : "";
                this.producer_country = ds.Tables[0].Rows[0]["producer_country"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_country"].ToString() : "";
                this.productor_pais_contacto = ds.Tables[0].Rows[0]["productor_pais_contacto"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_pais_contacto"].ToString() : "";
                this.producer_city = ds.Tables[0].Rows[0]["producer_city"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_city"].ToString() : "";
                this.productor_ciudad_contacto = ds.Tables[0].Rows[0]["productor_ciudad_contacto"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_ciudad_contacto"].ToString() : "";
                this.producer_address = ds.Tables[0].Rows[0]["producer_address"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_address"].ToString() : "";
                this.producer_phone = ds.Tables[0].Rows[0]["producer_phone"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_phone"].ToString() : "";
                this.producer_fax = ds.Tables[0].Rows[0]["producer_fax"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_fax"].ToString() : "";
                this.producer_movil = ds.Tables[0].Rows[0]["producer_movil"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_movil"].ToString() : "";
                this.producer_email = ds.Tables[0].Rows[0]["producer_email"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_email"].ToString() : "";
                this.producer_website = ds.Tables[0].Rows[0]["producer_website"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_website"].ToString() : "";
                this.producer_user_id = ds.Tables[0].Rows[0]["producer_user_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_user_id"] : 0;

                DataSet localizationFatherDS = db.Select("SELECT localization_father_id FROM dboPrd.localization WHERE localization_id = '" + this.producer_localization_id + "'");
                if (localizationFatherDS.Tables[0].Rows.Count == 1)
                {
                    this.producer_localization_father_id = localizationFatherDS.Tables[0].Rows[0]["localization_father_id"].ToString();
                }
                else
                {
                    this.producer_localization_father_id = "";
                }
                DataSet localizationFatherDSContact = db.Select("SELECT localization_father_id FROM dboPrd.localization WHERE localization_id = '" + this.productor_localizacion_contacto_id + "'");
                if (localizationFatherDSContact.Tables[0].Rows.Count == 1)
                {
                    this.productor_localizacion_contacto_id_padre = localizationFatherDSContact.Tables[0].Rows[0]["localization_father_id"].ToString();
                }
                else
                {
                    this.productor_localizacion_contacto_id_padre = "";
                }
            }
        }


        public void LoadProducerByUser(int user_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT producer_id, person_type_id, id_genero, id_etnia, identification_type_id, "
                                 + "producer_name, producer_nit, producer_nit_dig_verif, producer_company_type_id, "
                                 + "producer_type_id, producer_identification_number, "
                                 + "producer_firstname, producer_firstname2, producer_lastname, producer_lastname2, producer_localization_id, "
                                 + "id_grupo_poblacional, fecha_nacimiento,abreviatura, primer_nombre_sup,segundo_nombre_sup,primer_apellido_sup,segundo_apellido_sup,"
                                 + "producer_country, producer_city, producer_address, producer_phone, productor_pais_contacto, productor_ciudad_contacto, productor_localizacion_contacto_id,"
                                 + "producer_fax, producer_movil, producer_email, producer_website, producer_user_id "
                                 + "FROM dboPrd.producer WHERE producer_user_id=" + user_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.producer_id = (int)ds.Tables[0].Rows[0]["producer_id"];
                this.person_type_id = ds.Tables[0].Rows[0]["person_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["person_type_id"] : 0;
                this.id_genero = ds.Tables[0].Rows[0]["id_genero"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_genero"] : 0;
                this.id_etnia = ds.Tables[0].Rows[0]["id_etnia"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_etnia"] : 0;
                this.identification_type_id = ds.Tables[0].Rows[0]["identification_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["identification_type_id"] : 0;
                this.producer_name = ds.Tables[0].Rows[0]["producer_name"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_name"].ToString() : "";
                this.producer_nit = ds.Tables[0].Rows[0]["producer_nit"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_nit"].ToString() : "";                
                this.producer_nit_dig_verif = ds.Tables[0].Rows[0]["producer_nit_dig_verif"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_nit_dig_verif"] : 0;
                this.producer_company_type_id = ds.Tables[0].Rows[0]["producer_company_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_company_type_id"] : 0;
                this.producer_type_id = ds.Tables[0].Rows[0]["producer_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_type_id"] : 0;
                this.producer_identification_number = ds.Tables[0].Rows[0]["producer_identification_number"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_identification_number"].ToString() : "";
                this.producer_firstname = ds.Tables[0].Rows[0]["producer_firstname"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_firstname"].ToString() : "";
                this.producer_firstname2 = ds.Tables[0].Rows[0]["producer_firstname2"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_firstname2"].ToString() : "";
                this.producer_lastname = ds.Tables[0].Rows[0]["producer_lastname"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_lastname"].ToString() : "";
                this.producer_lastname2 = ds.Tables[0].Rows[0]["producer_lastname2"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_lastname2"].ToString() : "";
                
                this.producer_localization_id = ds.Tables[0].Rows[0]["producer_localization_id"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_localization_id"].ToString() : "";
                this.producer_country = ds.Tables[0].Rows[0]["producer_country"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_country"].ToString() : "";
                this.producer_city = ds.Tables[0].Rows[0]["producer_city"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_city"].ToString() : "";

                this.productor_localizacion_contacto_id = ds.Tables[0].Rows[0]["productor_localizacion_contacto_id"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_localizacion_contacto_id"].ToString() : "";
                this.productor_pais_contacto = ds.Tables[0].Rows[0]["productor_pais_contacto"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_pais_contacto"].ToString() : "";
                this.productor_ciudad_contacto = ds.Tables[0].Rows[0]["productor_ciudad_contacto"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_ciudad_contacto"].ToString() : "";

                this.producer_address = ds.Tables[0].Rows[0]["producer_address"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_address"].ToString() : "";
                this.producer_phone = ds.Tables[0].Rows[0]["producer_phone"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_phone"].ToString() : "";
                this.producer_fax = ds.Tables[0].Rows[0]["producer_fax"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_fax"].ToString() : "";
                this.producer_movil = ds.Tables[0].Rows[0]["producer_movil"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_movil"].ToString() : "";
                this.producer_email = ds.Tables[0].Rows[0]["producer_email"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_email"].ToString() : "";
                this.producer_website = ds.Tables[0].Rows[0]["producer_website"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_website"].ToString() : "";
                this.producer_user_id = ds.Tables[0].Rows[0]["producer_user_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_user_id"] : 0;

                this.id_grupo_poblacional = ds.Tables[0].Rows[0]["id_grupo_poblacional"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_grupo_poblacional"] : 0;
                this.abreviatura = ds.Tables[0].Rows[0]["abreviatura"].ToString() != "" ? ds.Tables[0].Rows[0]["abreviatura"].ToString() : "";
                this.primer_nombre_sup = ds.Tables[0].Rows[0]["primer_nombre_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["primer_nombre_sup"].ToString() : "";
                this.segundo_nombre_sup = ds.Tables[0].Rows[0]["segundo_nombre_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["segundo_nombre_sup"].ToString() : "";
                this.primer_apellido_sup = ds.Tables[0].Rows[0]["primer_apellido_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["primer_apellido_sup"].ToString() : "";
                this.segundo_apellido_sup = ds.Tables[0].Rows[0]["segundo_apellido_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["segundo_apellido_sup"].ToString() : "";

                if (ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "")
                    this.fecha_nacimiento = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString());
                else
                    this.fecha_nacimiento = null;



                DataSet localizationFatherDS = db.Select("SELECT localization_father_id FROM dboPrd.localization WHERE localization_id = '" + this.producer_localization_id + "'");
                if (localizationFatherDS.Tables[0].Rows.Count == 1)
                {
                    this.producer_localization_father_id = localizationFatherDS.Tables[0].Rows[0]["localization_father_id"].ToString();
                }
                else
                {
                    this.producer_localization_father_id = "";
                }
                DataSet localizationFatherDSContact = db.Select("SELECT localization_father_id FROM dboPrd.localization WHERE localization_id = '" + this.productor_localizacion_contacto_id + "'");
                if (localizationFatherDSContact.Tables[0].Rows.Count == 1)
                {
                    this.productor_localizacion_contacto_id_padre = localizationFatherDSContact.Tables[0].Rows[0]["localization_father_id"].ToString();
                }
                else
                {
                    this.productor_localizacion_contacto_id_padre = "";
                }
            }
        }


        public void LoadProducer(int producer_id)
        {
            DB db = new DB();
            DataSet ds = db.Select("SELECT producer_id, person_type_id, id_genero, id_etnia, identification_type_id, "
                                 + "producer_name, producer_nit, producer_nit_dig_verif, producer_company_type_id, "
                                 + "producer_type_id, producer_identification_number, "
                                 + "producer_firstname, producer_firstname2, producer_lastname, producer_lastname2, producer_localization_id, "
                                 + "id_grupo_poblacional, fecha_nacimiento,abreviatura, primer_nombre_sup,segundo_nombre_sup,primer_apellido_sup,segundo_apellido_sup,"
                                 + "producer_country, producer_city, producer_address, producer_phone, productor_pais_contacto, productor_ciudad_contacto, productor_localizacion_contacto_id, "
                                 + "producer_fax, producer_movil, producer_email, producer_website, producer_user_id "
                                 + "FROM dboPrd.producer WHERE producer_id=" + producer_id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.producer_id = (int)ds.Tables[0].Rows[0]["producer_id"];
                this.person_type_id = ds.Tables[0].Rows[0]["person_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["person_type_id"] : 0;
                this.id_genero = ds.Tables[0].Rows[0]["id_genero"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_genero"] : 0;
                this.id_etnia = ds.Tables[0].Rows[0]["id_etnia"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_etnia"] : 0;

                this.identification_type_id = ds.Tables[0].Rows[0]["identification_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["identification_type_id"] : 0;
                this.producer_name = ds.Tables[0].Rows[0]["producer_name"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_name"].ToString() : "";
                this.producer_nit = ds.Tables[0].Rows[0]["producer_nit"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_nit"].ToString() : "";
                
                this.producer_nit_dig_verif = ds.Tables[0].Rows[0]["producer_nit_dig_verif"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_nit_dig_verif"] : 0;
                this.producer_company_type_id = ds.Tables[0].Rows[0]["producer_company_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_company_type_id"] : 0;
                this.producer_type_id = ds.Tables[0].Rows[0]["producer_type_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_type_id"] : 0;
                this.producer_identification_number = ds.Tables[0].Rows[0]["producer_identification_number"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_identification_number"].ToString() : "";
                this.producer_firstname = ds.Tables[0].Rows[0]["producer_firstname"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_firstname"].ToString() : "";
                this.producer_firstname2 = ds.Tables[0].Rows[0]["producer_firstname2"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_firstname2"].ToString() : "";
                this.producer_lastname = ds.Tables[0].Rows[0]["producer_lastname"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_lastname"].ToString() : "";
                this.producer_lastname2 = ds.Tables[0].Rows[0]["producer_lastname2"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_lastname2"].ToString() : "";
               
                
                this.productor_localizacion_contacto_id = ds.Tables[0].Rows[0]["productor_localizacion_contacto_id"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_localizacion_contacto_id"].ToString() : "";
                this.productor_pais_contacto = ds.Tables[0].Rows[0]["productor_pais_contacto"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_pais_contacto"].ToString() : "";
                this.productor_ciudad_contacto = ds.Tables[0].Rows[0]["productor_ciudad_contacto"].ToString() != "" ? ds.Tables[0].Rows[0]["productor_ciudad_contacto"].ToString() : "";

                this.producer_localization_id = ds.Tables[0].Rows[0]["producer_localization_id"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_localization_id"].ToString() : "";
                this.producer_country = ds.Tables[0].Rows[0]["producer_country"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_country"].ToString() : "";
                this.producer_city = ds.Tables[0].Rows[0]["producer_city"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_city"].ToString() : "";


                this.producer_address = ds.Tables[0].Rows[0]["producer_address"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_address"].ToString() : "";
                this.producer_phone = ds.Tables[0].Rows[0]["producer_phone"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_phone"].ToString() : "";
                this.producer_fax = ds.Tables[0].Rows[0]["producer_fax"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_fax"].ToString() : "";
                this.producer_movil = ds.Tables[0].Rows[0]["producer_movil"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_movil"].ToString() : "";
                this.producer_email = ds.Tables[0].Rows[0]["producer_email"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_email"].ToString() : "";
                this.producer_website = ds.Tables[0].Rows[0]["producer_website"].ToString() != "" ? ds.Tables[0].Rows[0]["producer_website"].ToString() : "";
                this.producer_user_id = ds.Tables[0].Rows[0]["producer_user_id"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["producer_user_id"] : 0;

                this.id_grupo_poblacional = ds.Tables[0].Rows[0]["id_grupo_poblacional"].ToString() != "" ? (int)ds.Tables[0].Rows[0]["id_grupo_poblacional"] : 0;
                this.abreviatura = ds.Tables[0].Rows[0]["abreviatura"].ToString() != "" ? ds.Tables[0].Rows[0]["abreviatura"].ToString() : "";
                this.primer_nombre_sup = ds.Tables[0].Rows[0]["primer_nombre_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["primer_nombre_sup"].ToString() : "";
                this.segundo_nombre_sup = ds.Tables[0].Rows[0]["segundo_nombre_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["segundo_nombre_sup"].ToString() : "";
                this.primer_apellido_sup = ds.Tables[0].Rows[0]["primer_apellido_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["primer_apellido_sup"].ToString() : "";
                this.segundo_apellido_sup = ds.Tables[0].Rows[0]["segundo_apellido_sup"].ToString() != "" ? ds.Tables[0].Rows[0]["segundo_apellido_sup"].ToString() : "";

                if (ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "")
                    this.fecha_nacimiento = DateTime.Parse(ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString());
                else
                    this.fecha_nacimiento = null;



                DataSet localizationFatherDS = db.Select("SELECT localization_father_id FROM dboPrd.localization WHERE localization_id = '" + this.producer_localization_id + "'");
                if (localizationFatherDS.Tables[0].Rows.Count == 1)
                {
                    this.producer_localization_father_id = localizationFatherDS.Tables[0].Rows[0]["localization_father_id"].ToString();
                }
                else 
                {
                    this.producer_localization_father_id = "";
                }
                DataSet localizationFatherDSContact = db.Select("SELECT localization_father_id FROM dboPrd.localization WHERE localization_id = '" + this.productor_localizacion_contacto_id + "'");
                if (localizationFatherDSContact.Tables[0].Rows.Count == 1)
                {
                    this.productor_localizacion_contacto_id_padre = localizationFatherDSContact.Tables[0].Rows[0]["localization_father_id"].ToString();
                }
                else 
                {
                    this.productor_localizacion_contacto_id_padre = "";
                }

            }
        }

        public bool Save(int project_id = 0)
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Define la cadena apropiada para la sentencia de base de dato si no se ha seleccionado el tipo de persona. */
            string person_type_update = "";
            string person_type_insert = "";
            if (this.person_type_id == 0)
            {
                person_type_update = "null ,";
                person_type_insert = "null";
            }
            else
            {
                person_type_update = "'" + this.person_type_id + "' ,";
                person_type_insert = "'" + this.person_type_id + "' ";
            }

            /* Define la cadena apropiada para la sentencia de base de dato si no se ha seleccionado genero. */
            string id_genero_update = "";
            string id_genero_insert = "";
            if (this.id_genero == 0)
            {
                id_genero_update = "null ,";
                id_genero_insert = "null";
            }
            else
            {
                id_genero_update = "'" + this.id_genero + "' ,";
                id_genero_insert = "'" + this.id_genero + "' ";
            }

            /* Define la cadena apropiada para la sentencia de base de dato si no se ha seleccionado etnia. */
            string id_etnia_update = "";
            string id_etnia_insert = "";
            if (this.id_etnia == 0)
            {
                id_etnia_update = "null ,";
                id_etnia_insert = "null";
            }
            else
            {
                id_etnia_update = "'" + this.id_etnia + "' ,";
                id_etnia_insert = "'" + this.id_etnia + "' ";
            }

            /* Define la cadena apropiada para la sentencia de base de dato si no se ha seleccionado etnia. */
            string id_grupo_poblacional_update = "";
            string id_grupo_poblacional_insert = "";
            if (this.id_etnia == 0)
            {
                id_grupo_poblacional_update = "null ,";
                id_grupo_poblacional_insert = "null";
            }
            else
            {
                id_grupo_poblacional_update = "'" + this.id_etnia + "' ,";
                id_grupo_poblacional_insert = "'" + this.id_etnia + "' ";
            }

            /* Crea la sentencia de almacenamiento de la ubicacion */
            string localization_update = "";
            string localization_insert = "";
            if (this.producer_localization_id == "0" || this.producer_localization_id == "" || this.producer_localization_id == null)
            {
                localization_update = "null ,";
                localization_insert = "null";
            }
            else
            {
                localization_update = "'" + this.producer_localization_id + "' ,";
                localization_insert = "'" + this.producer_localization_id + "' ";
            }
            /* Crea la sentencia de almacenamiento de la ubicacion contacto*/
            string localization_update_contact = "";
            string localization_insert_contact = "";
            if (this.productor_localizacion_contacto_id == "0" || this.productor_localizacion_contacto_id == "" || this.productor_localizacion_contacto_id == null)
            {
                localization_update_contact = "null ,";
                localization_insert_contact = "null";
            }
            else
            {
                localization_update_contact = "'" + this.productor_localizacion_contacto_id + "' ,";
                localization_insert_contact = "'" + this.productor_localizacion_contacto_id + "' ";
            }

            ProjectAttachment updateAttach = new ProjectAttachment(project_id);

            /* Define una opción por defecto para el tipo de identificación si esta vacio o es 0 */
            string identification_type_update = (this.identification_type_id > 0) ? this.identification_type_id.ToString() : "1";

            /* Define una opción por defecto para el tipo de empresa si esta vacio o es 0 */
            string identification_company_type_update = (this.producer_company_type_id > 0) ? this.producer_company_type_id.ToString() : "1";


            string producer_nit_dig_verif_update = (this.producer_nit_dig_verif > 0) ? this.producer_nit_dig_verif.ToString() : "0";


            string fecha_nacimiento_update_query = (this.fecha_nacimiento.HasValue == false || this.fecha_nacimiento.Value.Year == 1) ? "null" : "'" + this.fecha_nacimiento.Value.ToString("dd-MM-yy HH:mm:ss") + "'";

            /* Si esta definido un project_id se hace una actualización, de lo contrario se hace una inserción */
            if (this.producer_id.ToString() != "" && this.producer_id > 0)
            {
                string updateProducer = "UPDATE dboPrd.producer SET ";

                updateProducer = updateProducer + "person_type_id = " + person_type_update + " ";
                updateProducer = updateProducer + "id_genero = " + id_genero_update + " ";
                updateProducer = updateProducer + "id_grupo_poblacional = " + id_grupo_poblacional_update + " ";
                updateProducer = updateProducer + "id_etnia = " + id_etnia_update + " ";
                updateProducer = updateProducer + "identification_type_id = '" + identification_type_update + "', ";
                updateProducer = updateProducer + "producer_name = '" + StringExtensors.ToNombrePropio(this.producer_name).ToUpper() + "', ";
                updateProducer = updateProducer + "producer_nit = '" + this.producer_nit + "', ";
                updateProducer = updateProducer + "producer_nit_dig_verif = '" + producer_nit_dig_verif_update + "', ";
                updateProducer = updateProducer + "producer_company_type_id = '" + identification_company_type_update + "', ";
                updateProducer = updateProducer + "producer_type_id = '" + this.producer_type_id + "', ";
                updateProducer = updateProducer + "producer_identification_number = '" + this.producer_identification_number + "', ";
                updateProducer = updateProducer + "producer_firstname = '" + StringExtensors.ToNombrePropio(this.producer_firstname).ToUpper() + "', ";
                updateProducer = updateProducer + "producer_firstname2 = '" + StringExtensors.ToNombrePropio(this.producer_firstname2).ToUpper() + "', ";
                updateProducer = updateProducer + "producer_lastname = '" + StringExtensors.ToNombrePropio(this.producer_lastname).ToUpper() + "', ";
                updateProducer = updateProducer + "producer_lastname2 = '" + StringExtensors.ToNombrePropio(this.producer_lastname2).ToUpper() + "', ";

                updateProducer = updateProducer + "abreviatura = '" + this.abreviatura + "', ";
                updateProducer = updateProducer + "primer_nombre_sup = '" + StringExtensors.ToNombrePropio(this.primer_nombre_sup).ToUpper() + "', ";
                updateProducer = updateProducer + "segundo_nombre_sup = '" + StringExtensors.ToNombrePropio(this.segundo_nombre_sup).ToUpper() + "', ";
                updateProducer = updateProducer + "primer_apellido_sup = '" + StringExtensors.ToNombrePropio(this.primer_apellido_sup).ToUpper() + "', ";
                updateProducer = updateProducer + "segundo_apellido_sup = '" + StringExtensors.ToNombrePropio(this.segundo_apellido_sup).ToUpper() + "', ";
                updateProducer = updateProducer + "fecha_nacimiento = " + fecha_nacimiento_update_query + ", ";

                updateProducer = updateProducer + "producer_localization_id = " + localization_update;
                updateProducer = updateProducer + "productor_localizacion_contacto_id = " + localization_update_contact;
                updateProducer = updateProducer + "producer_country = '" + this.producer_country + "', ";
                updateProducer = updateProducer + "productor_pais_contacto = '" + this.productor_pais_contacto + "', ";
                updateProducer = updateProducer + "producer_city = '" + this.producer_city + "', ";
                updateProducer = updateProducer + "productor_ciudad_contacto = '" + this.productor_ciudad_contacto + "', ";
                updateProducer = updateProducer + "producer_address = '" + this.producer_address + "', ";
                updateProducer = updateProducer + "producer_phone = '" + this.producer_phone + "', ";
                updateProducer = updateProducer + "producer_fax = '" + this.producer_fax + "', ";
                updateProducer = updateProducer + "producer_movil = '" + this.producer_movil + "', ";
                updateProducer = updateProducer + "producer_email = '" + this.producer_email + "', ";
                updateProducer = updateProducer + "producer_website = '" + this.producer_website + "' ";
                updateProducer = updateProducer + "WHERE producer_id = " + this.producer_id.ToString();

                /* Si se actualizó correctamente la tabla del productor */
                updateAttach.LoadAttachmetAndUpdate(project_id,this.producer_id);
                return db.Execute(updateProducer);
                
            }
            else 
            {
                string insertProducer = "INSERT INTO dboPrd.producer (person_type_id, id_genero, id_etnia, identification_type_id, ";
                insertProducer = insertProducer + "producer_name, producer_nit, producer_nit_dig_verif,";                
                insertProducer = insertProducer + "producer_company_type_id, producer_type_id, ";
                insertProducer = insertProducer + "producer_identification_number, producer_firstname, producer_firstname2, ";                
                insertProducer = insertProducer + "producer_lastname, producer_lastname2, producer_localization_id, ";
                insertProducer = insertProducer + "id_grupo_poblacional, fecha_nacimiento,abreviatura, primer_nombre_sup,segundo_nombre_sup,primer_apellido_sup,segundo_apellido_sup,";
                insertProducer = insertProducer + "producer_country, producer_city, ";
                insertProducer = insertProducer + "productor_localizacion_contacto_id,productor_pais_contacto, productor_ciudad_contacto, ";
                insertProducer = insertProducer + "producer_address, producer_phone, ";
                insertProducer = insertProducer + "producer_fax, producer_movil, ";
                insertProducer = insertProducer + "producer_email, producer_website, producer_user_id) ";
                insertProducer = insertProducer + "VALUES (";
                insertProducer = insertProducer + person_type_insert + ", ";
                insertProducer = insertProducer + id_genero_insert + ", ";
                insertProducer = insertProducer + id_etnia_insert + ", ";
                insertProducer = insertProducer + " '" + identification_type_update + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.producer_name) + "', ";
                insertProducer = insertProducer + " '" + this.producer_nit + "', ";
                insertProducer = insertProducer + " '" + producer_nit_dig_verif_update + "', ";
                insertProducer = insertProducer + " '" + identification_company_type_update + "', ";
                insertProducer = insertProducer + " '" + this.producer_type_id + "', ";
                insertProducer = insertProducer + " '" + this.producer_identification_number + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.producer_firstname).ToUpper() + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.producer_firstname2).ToUpper() + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.producer_lastname).ToUpper() + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.producer_lastname2).ToUpper() + "', ";
                insertProducer = insertProducer + localization_insert + ", ";
                insertProducer = insertProducer + id_grupo_poblacional_insert + ", ";
                insertProducer = insertProducer + fecha_nacimiento_update_query + ", ";
                insertProducer = insertProducer + " '" + this.abreviatura + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.primer_nombre_sup).ToUpper() + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.segundo_nombre_sup).ToUpper() + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.primer_apellido_sup).ToUpper() + "', ";
                insertProducer = insertProducer + " '" + StringExtensors.ToNombrePropio(this.segundo_apellido_sup).ToUpper() + "', ";

                insertProducer = insertProducer + " '" + this.producer_country + "', ";
                insertProducer = insertProducer + " '" + this.producer_city + "', ";
                insertProducer = insertProducer + " '" + this.productor_localizacion_contacto_id + "', ";
                insertProducer = insertProducer + " '" + this.productor_pais_contacto + "', ";
                insertProducer = insertProducer + " '" + this.productor_ciudad_contacto + "', ";
                insertProducer = insertProducer + " '" + this.producer_address + "', ";
                insertProducer = insertProducer + " '" + this.producer_phone + "', ";
                insertProducer = insertProducer + " '" + this.producer_fax + "', ";
                insertProducer = insertProducer + " '" + this.producer_movil + "', ";
                insertProducer = insertProducer + " '" + this.producer_email + "', ";
                insertProducer = insertProducer + " '" + this.producer_website + "', ";
                string useridInsert = (this.producer_user_id == 0) ? "NULL" : this.producer_user_id.ToString();
                insertProducer = insertProducer + " " + useridInsert + ") ";

                /* Si se actualizó correctamente la tabla del productor */
                if (db.Execute(insertProducer))
                {
                    string queryProducerId = "SELECT MAX(producer_id) as producer_id FROM dboPrd.producer";
                    DataSet newProducerDS = db.Select(queryProducerId);
                    if (newProducerDS.Tables[0].Rows.Count == 1)
                    {
                        this.producer_id = Convert.ToInt32(newProducerDS.Tables[0].Rows[0]["producer_id"]);
                        updateAttach.LoadAttachmetAndUpdate(project_id,this.producer_id);
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

        public string GetLocationDescription()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializa una cadena para armar la respuesta */
            string result = "";

            /* Si el campo de pais y ciudad esta vacio se presenta el de departamento y municipio */
            if(this.producer_country != "")
            {
                result = this.producer_city + ", " + this.producer_country;
            }
            /* Si esta definido un localization_id se hace la consulta de los nombres */
            if (this.producer_localization_id.ToString() != "")
            {
                /* Obtiene el nombre del municipio */
                string queryLocalizationName = "SELECT localization_name ";
                queryLocalizationName = queryLocalizationName + "FROM dboPrd.localization WHERE localization_id= '" + this.producer_localization_id + "'";
                DataSet queryLocalizationDS = db.Select(queryLocalizationName);
                if (queryLocalizationDS.Tables[0].Rows.Count == 1)
                {
                    result = queryLocalizationDS.Tables[0].Rows[0]["localization_name"].ToString();
                }

                /* Obtiene el nombre del departamento */
                string queryLocalizationFatherName = "SELECT localization_name ";
                queryLocalizationFatherName = queryLocalizationFatherName + "FROM dboPrd.localization WHERE localization_id= '" + this.producer_localization_father_id+"'";
                DataSet queryLocalizationFatherDS = db.Select(queryLocalizationFatherName);
                if (queryLocalizationFatherDS.Tables[0].Rows.Count == 1)
                {
                    result = result + ", " + queryLocalizationFatherDS.Tables[0].Rows[0]["localization_name"].ToString();
                }
            }

            return result;
        }

        public string GetLocationDescriptionContact()
        {
            /* Hace disponible la conexión a la base de datos */
            DB db = new DB();

            /* Inicializa una cadena para armar la respuesta */
            string result = "";

            /* Si el campo de pais y ciudad esta vacio se presenta el de departamento y municipio */
            if (this.producer_country != "")
            {
                result = this.producer_city + ", " + this.producer_country;
            }
            /* Si esta definido un localization_id se hace la consulta de los nombres */
            if (this.productor_localizacion_contacto_id.ToString() != "")
            {
                /* Obtiene el nombre del municipio */
                string queryLocalizationName = "SELECT localization_name ";
                queryLocalizationName = queryLocalizationName + "FROM dboPrd.localization WHERE localization_id= '" + this.productor_localizacion_contacto_id + "'";
                DataSet queryLocalizationDS = db.Select(queryLocalizationName);
                if (queryLocalizationDS.Tables[0].Rows.Count == 1)
                {
                    result = queryLocalizationDS.Tables[0].Rows[0]["localization_name"].ToString();
                }

                /* Obtiene el nombre del departamento */
                string queryLocalizationFatherName = "SELECT localization_name ";
                queryLocalizationFatherName = queryLocalizationFatherName + "FROM dboPrd.localization WHERE localization_id= '" + this.productor_localizacion_contacto_id_padre + "'";
                DataSet queryLocalizationFatherDS = db.Select(queryLocalizationFatherName);
                if (queryLocalizationFatherDS.Tables[0].Rows.Count == 1)
                {
                    result = result + ", " + queryLocalizationFatherDS.Tables[0].Rows[0]["localization_name"].ToString();
                }
            }

            return result;
        }
    }
}