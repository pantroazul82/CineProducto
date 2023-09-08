using CineProducto.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CineProducto
{
    public partial class frmEditLetter : System.Web.UI.Page
    {
        public string currentForm = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Verificamos si existe un usuario autenticado o de lo contrario lo redirigimos a la página inicial */
            if (Session["user_id"] != null && Convert.ToInt32(Session["user_id"]) > 0)
            {
                /* Si el usuario está autenticado verificamos el rol y el permiso asignado para la visualización del listado */
                User userObj = new User();
                userObj.user_id = Convert.ToInt32(Session["user_id"]);

                if (!userObj.checkPermission("administrar-sistema"))
                {
                    Response.Redirect("Default.aspx", true);
                }
            }
            else
            {
                Response.Redirect("Default.aspx", true);
            }
            if (IsPostBack  == false)
            {
                Letter a = new Letter();
                a.LoadLetter();
                letter_greeting.Text = a.letter_greeting;
                letter_body.Text = a.letter_body;
                letter_footer_message.Text = a.letter_message;
                letter_note.Text = a.letter_note;
                letter_prefirma.Text = a.letter_prefirma;
            }

           
        }

       

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            Letter a = new Letter();
            
             a.letter_greeting=letter_greeting.Text ;
             a.letter_body=letter_body.Text ;
             a.letter_message = letter_footer_message.Text;
             a.letter_note = letter_note.Text;
             a.letter_prefirma = letter_prefirma.Text;
            a.save();
            lblError.Text = "Cambios Guardados satisfactoriamente";
        }
    }
}