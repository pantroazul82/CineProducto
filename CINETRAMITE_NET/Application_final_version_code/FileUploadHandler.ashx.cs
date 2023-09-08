using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CineProducto
{
    /// <summary>
    /// Descripción breve de FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler
    {

    
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.QueryString["upload"] != null)
                {
                    string pathrefer = context.Request.UrlReferrer.ToString();
                   
                    var postedFile = context.Request.Files[0];
                    string tempPath = context.Request.QueryString["folder"];
                    string attachment_id = context.Request.QueryString["attachment_id"];
                    string savepath = context.Server.MapPath(tempPath);

                    string filename;

                    //For IE to get file name
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
                    {
                        string[] files = postedFile.FileName.Split(new char[] { '\\' });
                        filename = files[files.Length - 1];
                    }
                    else
                    {
                        filename = postedFile.FileName;
                    }
                    //quitamos caracteres especiales 
                    filename = filename.Replace("+","").Replace("&","").Replace("'","");

                    if (!Directory.Exists(savepath))
                        Directory.CreateDirectory(savepath);
                    string extension = System.IO.Path.GetExtension(filename);
                    if (attachment_id != "50" )
                    {
                        if(extension.ToLower() != ".pdf"){
                            throw new Exception("Error: Solo es valido subir archivos en formato pdf.");
                        }
                    }
                    else {
                        if ( extension.ToLower().IndexOf(".xls") < 0)
                        {
                            throw new Exception("Error: Solo es valido subir archivos en formato xls para el costo discriminado.");
                        }
                    }

                   postedFile.SaveAs(savepath + @"\" + filename);
                    context.Response.AddHeader("Vary", "Accept");
                    try
                    {
                        if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
                            context.Response.ContentType = "application/json";
                        else
                            context.Response.ContentType = "text/plain";
                    }
                    catch
                    {
                        context.Response.ContentType = "text/plain";
                    }

                    context.Response.Write(filename);
                    context.Response.StatusCode = 200;
                }
            }
            catch (Exception exp)
            {
                context.Response.Write(exp.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}