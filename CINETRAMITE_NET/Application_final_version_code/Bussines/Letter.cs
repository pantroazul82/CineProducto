using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections;
using CineProducto.Bussines;
using System.Data;
using System.IO;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Configuration;

namespace CineProducto.Bussines
{
    public class Letter
    {
        public int letter_id;
        public string letter_body;
        public string letter_message;
        public string letter_prefirma;
        public string letter_greeting;
        public string letter_note;


        public Letter() {
            this.letter_body = "";
            this.letter_message = "";
            this.letter_greeting = "";
            this.letter_note = "";
            this.letter_prefirma = "";
        }
        /**
         * Load information of the letter
         * 
         * */
        public void LoadLetter() {
            BD.dsCineTableAdapters.letterTableAdapter obj = new BD.dsCineTableAdapters.letterTableAdapter();
            BD.dsCine ds = new BD.dsCine();
            obj.FillByLetter_id(ds.letter, 1);

            if (ds.letter.Rows.Count >= 1)
            {
                this.letter_id = ds.letter[0].letter_id;
                this.letter_body =    ds.letter[0].letter_body;
                this.letter_message = ds.letter[0].letter_message;
                this.letter_greeting = ds.letter[0].letter_greeting;
                this.letter_prefirma = ds.letter[0].letter_prefirma;
                this.letter_note = ds.letter[0].letter_note;
            }
        }

        /**
         * Save information of the letter
         * 
         * */
        public void save() {
 
 

            BD.dsCineTableAdapters.letterTableAdapter obj = new BD.dsCineTableAdapters.letterTableAdapter();
            BD.dsCine ds = new BD.dsCine();
            obj.FillByLetter_id(ds.letter, 1);

            if (ds.letter.Rows.Count >= 1)
            {
                obj.Update(letter_body, letter_message, letter_greeting,
                    letter_note, letter_prefirma, 1);   
            }else {
                obj.Insert(letter_body, letter_message, letter_greeting, letter_note, letter_prefirma);    
            }
            
        }
    }
}