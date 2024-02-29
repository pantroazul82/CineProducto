using DominioCineProducto.Data;
using DominioCineProducto.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioCineProducto
{
    public class NegocioCineProducto
    {
        cineEntities model = null;

        public NegocioCineProducto()
        {
            model = new cineEntities();
        }

        public void AdicionarEstimulo(estimulo e)
        {
            model.estimulo.Add(e);
            model.SaveChanges();
        }

        public void ActualizarFechaLimite(int project_id, DateTime fechaInicial)
        {
            int diasHabiles = 10;
            if (model.configuration.Where(x => x.configuration_name == "d_hab_lim").FirstOrDefault() != null) {
                diasHabiles = int.Parse(model.configuration.Where(x => x.configuration_name == "d_hab_lim").FirstOrDefault().configuration_value);
            }

            Data.project registro = model.project.Find(project_id);
            Calendario cal = new Calendario();
            DateTime fechaLimite = cal.SumarDiasLaborales(fechaInicial, diasHabiles);
            registro.fecha_limite = fechaLimite;
            model.SaveChanges();
        }

        public void ActualizarFechaLimiteSolicitudAclaraciones(int project_id, DateTime fechaInicial)
        {
            int diasHabiles = 15;
            Data.project registro = model.project.Find(project_id);
            Calendario cal = new Calendario();
            DateTime fechaLimite = cal.SumarDiasLaborales(fechaInicial, diasHabiles);
            registro.fecha_limite = fechaLimite;
            model.SaveChanges();
        }

        public void ActualizarFechaLimiteEnvioAclaraciones(int project_id, DateTime fechaInicial)
        {
            int diasHabiles = 10;
            if (model.configuration.Where(x => x.configuration_name == "d_hab_lim").FirstOrDefault() != null)
            {
                diasHabiles = int.Parse(model.configuration.Where(x => x.configuration_name == "d_hab_lim").FirstOrDefault().configuration_value);
            }

            Data.project registro = model.project.Find(project_id);
            Calendario cal = new Calendario();
            DateTime fechaLimite = cal.SumarDiasLaborales(fechaInicial, diasHabiles);
            registro.fecha_limite = fechaLimite;
            model.SaveChanges();
        }

        public void eliminarEstimulosProjecto(int project_id) {

            List<estimulo> ests = model.estimulo.Where(x => x.project_id == project_id).ToList();
            foreach (estimulo unEst in ests)
            {
                model.estimulo.Remove(unEst);
            }
            model.SaveChanges();

        }
        public void eliminarEstimulo(int id_estimulo)
        {
            estimulo est = model.estimulo.Find(id_estimulo);
            model.estimulo.Remove(est);
            model.SaveChanges();

        }

        public void ActualizarProyecto(project p)
        {
            Data.project registro = model.project.Find(p.project_id);
            //registro.fecha_aprobacion = p.fecha_aprobacion;
            registro.codigo_validacion = p.codigo_validacion;
            registro.fecha_notificacion_certificado = p.fecha_notificacion_certificado;
            registro.numero_certificado = p.numero_certificado;
            model.SaveChanges();
        }

        public void ActualizarFechaFinalProyecto(project p)
        {
            Data.project registro = model.project.Find(p.project_id);            
            registro.fecha_final = p.fecha_final;                        
            model.SaveChanges();
        }

        public void ActualizarRutaCertificadoProject(project p)
        {
            Data.project registro = model.project.Find(p.project_id);
            registro.ruta_certificado = p.ruta_certificado;
            model.SaveChanges();
        }

        public void ActualizarEstimuloProyecto(project p)
        {
            Data.project registro = model.project.Find(p.project_id);
            registro.tiene_estimulos = p.tiene_estimulos;
            model.SaveChanges();
        }

        public void ActualizarEstadoDeProyecto(project p)
        {
            Data.project registro = model.project.Find(p.project_id);
            registro.state_id = p.state_id;
            model.SaveChanges();
        }

        public void ActualizarRazonesRechazo(project p)
        {
            Data.project registro = model.project.Find(p.project_id);
            registro.razones_rechazo = p.razones_rechazo;
            model.SaveChanges();
        }

        public project getProject(int id)
        {
            Data.project registro = model.project.Find(id);
            return registro;
        }
        public void guardarSubsanacion(int codProyecto, DateTime FechaSubsanacion, string ObsSubsanacion)
        {
            var p = model.project.Find(codProyecto);
            if (p == null)
            {
                return;
            }
            p.SUBSANADO = true;
            p.FECHA_SUBSANACION = FechaSubsanacion;
            p.DIAS_SUBSANACION = (int)FechaSubsanacion.Subtract(DateTime.Now).TotalDays;
            p.OBSERVACIONES_SUBSANACION = ObsSubsanacion;
            p.FECHA_ENVIO_SUBSANACION = null;
            p.SUBSANACION_ENVIADA = false;
            model.SaveChanges();
        }

        public void guardarEnviarSubsanacion(int codProyecto)
        {
            var p = model.project.Find(codProyecto);
            if (p == null)
            {
                return;
            }
            p.FECHA_ENVIO_SUBSANACION = DateTime.Now;
            p.SUBSANACION_ENVIADA = true;

            model.SaveChanges();
        }

        public production_type obtenerTipoProduccion(int cod)
        {
            var p = model.production_type.Find(cod);
            return p;
        }

        public project_genre obtenerGeneroProduccion(int cod)
        {
            var p = model.project_genre.Find(cod);
            return p;
        }

        public project_type obtenerTipoObra(int cod)
        {
            var p = model.project_type.Find(cod);
            return p;
        }

        public int getMaxNumeroCertificadoProject()
        {
            Data.project registro = model.project.OrderByDescending(x => x.numero_certificado).FirstOrDefault();
            if (registro != null && registro.numero_certificado != null)
                return int.Parse(registro.numero_certificado.ToString());
            else
                return 0;
        }

        public project getProjectByCodigoValidacion(string codigo_validacion)
        {
            Data.project registro = model.project.Where(x => x.codigo_validacion == codigo_validacion).FirstOrDefault();
            return registro;
        }

        public void AsignarPrimerResponsable(int project_id, int asignado_por)
        {
            Data.project registro = model.project.Find(project_id);
            int? UltimoUsuarioAsigado = model.project.Where(x => x.responsable != null).OrderByDescending(x => x.project_request_date).First().responsable;
            Data.usuario siguienteUsuario = model.usuario.Where(c => c.es_responsable == true && c.es_asignacion_automatica == true).OrderBy(c => c.FECHA_ASIGNACION).FirstOrDefault();
            if (siguienteUsuario != null) {
                registro.responsable = siguienteUsuario.idusuario;
                siguienteUsuario.FECHA_ASIGNACION = DateTime.Now;
            }
            else {
                siguienteUsuario = model.usuario.Where(c => c.es_responsable == true && c.es_asignacion_automatica == true).OrderBy(c => c.idusuario).FirstOrDefault();
                registro.responsable = siguienteUsuario.idusuario;
            }
            model.SaveChanges();


            project_responsable pResponsable = new project_responsable();
            pResponsable.project_id = project_id;
            pResponsable.responsable = registro.responsable;
            pResponsable.asignado_por = asignado_por;
            pResponsable.fecha = DateTime.Now;
            AdicionarProjectResponsable(pResponsable);
        }

        public void AdicionarProjectResponsable(project_responsable registro) {
            model.project_responsable.Add(registro);
            model.SaveChanges();
        }

        public List<localization> getLocalizacionesByPadre(string idPadre)
        {
            var list = model.localization.Where(x => x.localization_father_id == idPadre).OrderBy(x => x.localization_name).ToList();
            return list;
        }

        public List<genero> getGeneros()
        {
            var list = model.genero.OrderBy(x => x.nombre).ToList();
            return list;
        }

        public List<etnia> getEtnias()
        {
            var list = model.etnia.OrderBy(x => x.nombre).ToList();
            return list;
        }

        public List<grupo_poblacional> getGruposPoblacionales()
        {
            var list = model.grupo_poblacional.OrderBy(x => x.id_grupo_poblacional).ToList();
            return list;
        }

        public List<identification_type> getIdentificationTypes()
        {
            var list = model.identification_type.Where(x => x.identification_type_id != 2).OrderBy(x => x.identification_type_id).ToList();
            return list;
        }
        public localization getLocalizacionById(string id)
        {
            var list = model.localization.Where(x => x.localization_id == id).FirstOrDefault();
            return list;
        }

        public List<project> getProjectByState(int id_estado)
        {
            var list = model.project.Where(x => x.state_id == id_estado).ToList();
            return list;
        }

        public producer getProducerById(int id)
        {
            producer objP = model.producer.Where(x => x.producer_id == id).FirstOrDefault();
            return objP;
        }
        public producer actualizarProducer(producer pToUpdate)
        {
            producer objP = model.producer.Find(pToUpdate.producer_id);
            objP.person_type_id = pToUpdate.person_type_id;
            if (pToUpdate.producer_localization_id == null)
            {
                objP.producer_localization_id = null;
            }
            else if (pToUpdate.producer_localization_id.Trim() == string.Empty)
            {
                objP.producer_localization_id = null;
            }
            else
            {
                objP.producer_localization_id = pToUpdate.producer_localization_id;
            }
            
            objP.PRODUCTOR_LOCALIZACION_CONTACTO_ID = pToUpdate.PRODUCTOR_LOCALIZACION_CONTACTO_ID;
            objP.producer_phone = pToUpdate.producer_phone;
            objP.producer_movil = pToUpdate.producer_movil;
            objP.producer_email = pToUpdate.producer_email;

            objP.producer_firstname = pToUpdate.producer_firstname;
            objP.producer_firstname2 = pToUpdate.producer_firstname2;
            objP.producer_lastname = pToUpdate.producer_lastname;
            objP.producer_lastname2 = pToUpdate.producer_lastname2;
            objP.producer_identification_number = pToUpdate.producer_identification_number;
            objP.identification_type_id = pToUpdate.identification_type_id;
            objP.id_genero = pToUpdate.id_genero;
            objP.fecha_nacimiento = pToUpdate.fecha_nacimiento;
            objP.id_grupo_poblacional = pToUpdate.id_grupo_poblacional;
            objP.id_etnia = pToUpdate.id_etnia;

            objP.producer_name = pToUpdate.producer_name;
            objP.producer_nit = pToUpdate.producer_nit;
            objP.producer_nit_dig_verif = pToUpdate.producer_nit_dig_verif;
            objP.producer_company_type_id = pToUpdate.producer_company_type_id;
            objP.primer_nombre_sup = pToUpdate.primer_nombre_sup;
            objP.segundo_nombre_sup = pToUpdate.segundo_nombre_sup;
            objP.primer_apellido_sup = pToUpdate.primer_apellido_sup;
            objP.segundo_apellido_sup = pToUpdate.segundo_apellido_sup;
            objP.num_id_sup = pToUpdate.num_id_sup;
            objP.identification_type_id_sup = pToUpdate.identification_type_id_sup;

            objP.producer_country = pToUpdate.producer_country;
            objP.PRODUCTOR_PAIS_CONTACTO = pToUpdate.PRODUCTOR_PAIS_CONTACTO;
            objP.producer_city = pToUpdate.producer_city;
            objP.PRODUCTOR_CIUDAD_CONTACTO = pToUpdate.PRODUCTOR_CIUDAD_CONTACTO;
            objP.abreviatura = pToUpdate.abreviatura;

            model.SaveChanges();

            return objP;
        }

        public project_staff actualizarProjectStaff(project_staff pToUpdate)
        {
            project_staff objP = model.project_staff.Find(pToUpdate.project_staff_id);

            objP.identification_type_id = pToUpdate.identification_type_id;
            objP.project_staff_identification_number = pToUpdate.project_staff_identification_number;
            objP.project_staff_firstname = pToUpdate.project_staff_firstname;
            objP.project_staff_firstname2 = pToUpdate.project_staff_firstname2;
            objP.project_staff_lastname = pToUpdate.project_staff_lastname;
            objP.project_staff_lastname2 = pToUpdate.project_staff_lastname2;
            objP.fecha_nacimiento = pToUpdate.fecha_nacimiento;
            objP.id_genero = pToUpdate.id_genero;
            objP.id_grupo_poblacional = pToUpdate.id_grupo_poblacional;
            objP.id_etnia = pToUpdate.id_etnia;
            objP.project_staff_localization_id = pToUpdate.project_staff_localization_id;
            objP.project_staff_phone = pToUpdate.project_staff_phone;
            objP.project_staff_movil = pToUpdate.project_staff_movil;
            objP.project_staff_email = pToUpdate.project_staff_email;
            objP.project_staff_position_id = pToUpdate.project_staff_position_id;
            objP.id_especialidad_cargo = pToUpdate.id_especialidad_cargo;
            
            model.SaveChanges();
            return objP;
        }
        public project_producer actualizarProjectProducer(project_producer pToUpdate)
        {
            project_producer objP = model.project_producer.Find(pToUpdate.project_producer_id);
            objP.project_producer_participation_percentage = pToUpdate.project_producer_participation_percentage;
            model.SaveChanges();

            return objP;
        }
        public project_producer getProjectProducerById(int id)
        {
            project_producer objP = model.project_producer.Where(x => x.project_producer_id == id).FirstOrDefault();
            return objP;
        }
        public localization getDepartamentobyId(string id)
        {
            localization objP = model.localization.Where(x => x.localization_id == id).FirstOrDefault();
            return objP;
        }

        public void crearProjectProducts(int id_project, int producer_type, int cantidad, int is_requester)
        {
            for (int k = 1; k <= cantidad; k++)
            {
                producer p = new producer();
                p.producer_type_id = producer_type;
                model.producer.Add(p);                
                model.SaveChanges(); 
                
                project_producer pp = new project_producer();                
                pp.project_id = id_project;
                pp.project_producer_participation_percentage = 0;
                pp.project_producer_requester = is_requester;
                pp.producer_id = p.producer_id;
                model.project_producer.Add(pp);
                model.SaveChanges();
            }            
        }

        public List<producer_company_type> getProducerCompany()
        {
            List<producer_company_type> objs = model.producer_company_type.ToList();
            return objs;
        }

        public void AdicionarProjectAttachment(project_attachment e)
        {
            model.project_attachment.Add(e);
            model.SaveChanges();
        }
        public void eliminarProjectAttachmentByProducerId(int producer_id)
        {

            List<project_attachment> ests = model.project_attachment.Where(x => x.project_attachment_producer_id == producer_id).ToList();
            foreach (project_attachment unEst in ests)
            {
                model.project_attachment.Remove(unEst);
            }
            model.SaveChanges();

        }
        public void eliminarProjectAttachmentByStaffId(int staff_id)
        {

            List<project_attachment> ests = model.project_attachment.Where(x => x.project_staff_id == staff_id).ToList();
            foreach (project_attachment unEst in ests)
            {
                model.project_attachment.Remove(unEst);
            }
            model.SaveChanges();

        }


        public producer actualizarProducerPersonTipoCompanyTipo(producer pToUpdate)
        {
            producer objP = model.producer.Find(pToUpdate.producer_id);
            objP.person_type_id = pToUpdate.person_type_id;    
            objP.producer_company_type_id = pToUpdate.producer_company_type_id; 
            model.SaveChanges();
            return objP;
        }

        public void ActualizarProjectAttachmentUrl(project_attachment e)
        {
            project_attachment pa = model.project_attachment.Find(e.project_attachment_id);
            pa.project_attachment_date = e.project_attachment_date;
            pa.project_attachment_path = e.project_attachment_path;
            pa.nombre_original = e.nombre_original;
            model.SaveChanges();
        }

        public project_status getProjectStatus(int id_project, int id_seccion)
        {
            Data.project_status registro = model.project_status.Where(x => x.project_status_section_id == id_seccion && x.project_status_project_id == id_project).FirstOrDefault();
            return registro;
        }

        public List<project_status> getProjectStatusByProject(int id_project)
        {
            List<project_status> registros = model.project_status.Where(x => x.project_status_project_id == id_project).ToList();
            return registros;
        }

        public void crearProjectStatus(project_status ps)
        {            
             model.project_status.Add(ps);
             model.SaveChanges();
        }

        public void actualizarProjectStatus(project_status ps)
        {
            Data.project_status registro = model.project_status.Where(x => x.project_status_section_id == ps.project_status_section_id && x.project_status_project_id == ps.project_status_project_id).FirstOrDefault();

            registro.project_status_observacion_inicial = ps.project_status_observacion_inicial;
            registro.project_status_observacion_inicial_date = ps.project_status_observacion_inicial_date;
            registro.project_status_solicitud_aclaraciones = ps.project_status_solicitud_aclaraciones;
            registro.project_status_solicitud_aclaraciones_date = ps.project_status_solicitud_aclaraciones_date;
            registro.project_status_aclaraciones_productor_date = ps.project_status_aclaraciones_productor_date;
            registro.project_status_modified = ps.project_status_modified;
            registro.project_status_revision_state_id = ps.project_status_revision_state_id;
            registro.project_status_tab_state_id = ps.project_status_tab_state_id ;
            model.SaveChanges();
        }
        public void actualizarProjectStatusAclaracionesProductor(project_status ps)
        {
            Data.project_status registro = model.project_status.Where(x => x.project_status_section_id == ps.project_status_section_id && x.project_status_project_id == ps.project_status_project_id).FirstOrDefault();
            registro.project_status_aclaraciones_productor = ps.project_status_aclaraciones_productor;
            registro.project_status_aclaraciones_productor_date = ps.project_status_aclaraciones_productor_date;            
            model.SaveChanges();
        }

        public void actualizarEstadoProjectStatus(project_status ps)
        {
            Data.project_status registro = model.project_status.Where(x => x.project_status_section_id == ps.project_status_section_id && x.project_status_project_id == ps.project_status_project_id).FirstOrDefault();
            registro.project_status_revision_state_id = ps.project_status_revision_state_id;
            registro.project_status_tab_state_id = ps.project_status_tab_state_id;
            registro.project_status_revision_mark = ps.project_status_revision_mark;
            model.SaveChanges();
        }

        public project_attachment getProjectAttachmentById(int id)
        {
            project_attachment objP = model.project_attachment.Where(x => x.project_attachment_id == id).FirstOrDefault();
            return objP;
        }

        public void actualizarEstadoProjectAttachment(project_attachment ps)
        {
            project_attachment registro = model.project_attachment.Find(ps.project_attachment_id);            
            registro.project_attachment_approved = ps.project_attachment_approved;
            model.SaveChanges();
        }


        public List<staff_option_detail> getStaffOptionDetailByOptionVersion(int staff_option_id, int version)
        {
            List<staff_option_detail> registros = model.staff_option_detail.Where(x => x.staff_option_id == staff_option_id && x.version == version && x.staff_option_detail_deleted==0).ToList();
            return registros;
        }

        public staff_option getStaffOptionByOptionVersion(int? project_type_id, int? production_type_id, int? project_genre_id, int? staff_option_has_domestic_director, int? staff_option_personal_option, decimal? porcentaje)
        {
            //percentage_validation = "staff_option_percentage_init <='" + percentage + "' AND staff_option_percentage_end >='" + percentage + "' AND ";

            staff_option registro = new staff_option();

            if (staff_option_personal_option != 1)
            {
                //percentage_validation = "staff_option_percentage_init <='" + percentage + "' AND staff_option_percentage_end >='" + percentage + "' AND ";
                registro = model.staff_option.Where(x => x.project_type_id == project_type_id && x.production_type_id == production_type_id
                && x.project_genre_id == project_genre_id && x.staff_option_personal_option == staff_option_personal_option
                && x.staff_option_has_domestic_director == staff_option_has_domestic_director && x.staff_option_deleted == 0
                && x.staff_option_percentage_init <= porcentaje && x.staff_option_percentage_end >= porcentaje
                ).FirstOrDefault();
            }
            else {
                registro = model.staff_option.Where(x => x.project_type_id == project_type_id && x.production_type_id == production_type_id
                && x.project_genre_id == project_genre_id && x.staff_option_personal_option == staff_option_personal_option
                && x.staff_option_has_domestic_director == staff_option_has_domestic_director && x.staff_option_deleted == 0).FirstOrDefault();
            }
            
            
            return registro;
        }


        public void eliminarProjectStaffByProject(int project_id)
        {

            List<project_staff> pss = model.project_staff.Where(x => x.project_staff_project_id == project_id).ToList();
            foreach (project_staff unPs in pss)
            {
                model.project_staff.Remove(unPs);
            }
            model.SaveChanges();

        }

        public void adicionarProjectStaff(project_staff e)
        {
            model.project_staff.Add(e);
            model.SaveChanges();
        }

        public void actualizarDomesticProject(project p)
        {
            project registro = model.project.Find(p.project_id);
            registro.project_has_domestic_director = p.project_has_domestic_director;
            model.SaveChanges();
        }


        public project_staff getProjectStaffById(int id)
        {
            project_staff objP = model.project_staff.Where(x => x.project_staff_id == id).FirstOrDefault();
            return objP;
        }

        public List<project_attachment> getProjectAttachmentByStaffId(int project_staff_id)
        {
            List<project_attachment> registros = model.project_attachment.Where(x => x.project_staff_id == project_staff_id).ToList();
            return registros;
        }

        public List<project_attachment> getProjectAttachmentByProducerId(int producer_id)
        {
            List<project_attachment> registros = model.project_attachment.Where(x => x.project_attachment_producer_id == producer_id).ToList();
            return registros;
        }


        public void actualizarDesistidoNotificacion(project p)
        {
            Data.project registro = model.project.Find(p.project_id);
            registro.notificaciones_desistidos_enviadas = 1;            
            model.SaveChanges();
        }



    }
}
