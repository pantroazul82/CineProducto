using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CineProducto.Callbacks;
using System.ServiceModel.Activation;
using System.Web.Routing;
using System.Globalization;
using System.Threading;

namespace CineProducto
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            //sRouteTable.Routes.Add(new ServiceRoute("", new WebServiceHostFactory(), typeof(annotationsService)));
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception objError = Server.GetLastError().GetBaseException();

            string err = "Error en la aplicacion" + "@%@" +
                          "Error en la pagina: " + Request.Url.ToString() +
                          "@%@" +
                          "Mensage: " + objError.Message.ToString() +
                          "@%@" +
                          "Tecnico:" + objError.StackTrace.ToString();

            Server.ClearError();
            err = err.Replace("\r\n", "@%@");
            err = err.Replace("\r", "@%@");
            err = err.Replace("\n", "@%@");
            string er = DateTime.Now.Ticks.ToString().Substring(8) + "cineproducto";
            string ruta = Server.MapPath("~/logs/");
            try
            {
                System.IO.File.WriteAllText(ruta + "/" + er + ".log", err);
                Response.Redirect("~/frmErrorManager.aspx?er=" + er);
            }
            catch (Exception) { }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.LongDatePattern = "dddd, dd' de 'MMMM' de 'yyyy";
            newCulture.DateTimeFormat.ShortTimePattern = "hh:mm tt";
            newCulture.DateTimeFormat.LongTimePattern = "hh:mm:ss tt";
            newCulture.DateTimeFormat.DateSeparator = "/";
            newCulture.NumberFormat.NumberDecimalSeparator = ".";
            newCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            newCulture.NumberFormat.PercentDecimalSeparator = ".";
            newCulture.NumberFormat.NumberGroupSeparator = ",";
            newCulture.NumberFormat.CurrencyGroupSeparator = ",";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }

    }
}
