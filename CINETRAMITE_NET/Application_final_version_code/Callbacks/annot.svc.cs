using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using CineProducto.Bussines;

namespace CineProducto.Callbacks
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class annot : System.Web.UI.Page
    {
        // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
        // Para crear una operación que devuelva XML,
        //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        //     e incluya la siguiente línea en el cuerpo de la operación:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebGet]
        public List<Annotation> load(string documentString = null)
        {
            int projectId = Convert.ToInt32(Session["project_id"]);
            // Agregue aquí la implementación de la operación

            return Annotation.loadAnnotationsByFileAndProject(documentString, projectId);
        }

        [OperationContract]
        [WebGet]
        public Annotation get(string id = null)
        {
            return new Annotation(id);
        }

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json
            )]
        public bool set(Annotation annotation)
        {
            if (new Annotation(annotation.annotation_id).annotation_id == null)
            {
                annotation.annotation_project = Convert.ToInt32(Session["project_id"]);
            }
            return annotation.save();
        }

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json
            )]
        public bool delete(Annotation annotation)
        {
            Annotation annot = new Annotation(annotation.annotation_id);
            if (annot.annotation_id != null)
            {
                return annotation.delete();
            }
            else
                return false;
        }

        // Agregue aquí más operaciones y márquelas con [OperationContract]
    }
}
