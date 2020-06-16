using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Mail;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.core.dao;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.minedu.dao;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System.Transactions;


namespace net.royal.spring.minedu.servicio.impl
{
    public class MpProyectoServicioImpl : GenericoServicioImpl, MpProyectoServicio
    {
        private IServiceProvider servicioProveedor;
        private MpProyectoDao mpProyectoDao;
        private MpProyectorecursoDao mpProyectorecursoDao;
        private MpProyectorecursoperiodoDao mpProyectorecursoperiodoDao;
        private MpPersonaDao mpPersonaDao;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;
        private MpAreamineduDao mpAreamineduDao;
        private MpContratacionDao mpContratacionDao;
        private MpSistemaDao mpSistemaDao;
        private List<MpProyectorecurso> nuevaLista = new List<MpProyectorecurso>();

        public MpProyectoServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            mpProyectoDao = servicioProveedor.GetService<MpProyectoDao>();
            mpProyectorecursoDao = servicioProveedor.GetService<MpProyectorecursoDao>();
            mpProyectorecursoperiodoDao = servicioProveedor.GetService<MpProyectorecursoperiodoDao>();
            mpPersonaDao = servicioProveedor.GetService<MpPersonaDao>();
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
            mpAreamineduDao = servicioProveedor.GetService<MpAreamineduDao>();
            mpContratacionDao = servicioProveedor.GetService<MpContratacionDao>();
            mpSistemaDao = servicioProveedor.GetService<MpSistemaDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return mpProyectoDao.listarPaginacion(paginacion, filtro);
        }

        public MpProyecto registrar(UsuarioActual usuarioActual, MpProyecto bean)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    nuevaLista = new List<MpProyectorecurso>();

                    bean.IdProyecto = mpProyectoDao.generarCodigo();
                    bean.Nombre = UString.Mayusculas(bean.Nombre);
                    bean.Nombrecorto = UString.Mayusculas(bean.Nombrecorto);
                    bean.Expediente = UString.Mayusculas(bean.Expediente);
                    bean.Asunto = UString.Mayusculas(bean.Asunto);
                    bean.Proceso = UString.Mayusculas(bean.Proceso);
                    bean.Observacion = UString.Mayusculas(bean.Observacion);

                    foreach (MpProyectorecurso recurso in bean.listaRecursos)
                    {
                        foreach (MpProyectorecursoperiodo periodo in recurso.listaPeriodos)
                        {
                            if (bean.Fechainicio <= periodo.Fechainicio && bean.Fechafin >= periodo.Fechafin) { }
                            else
                            {
                                string cadena = "Los periodos de los seleccionados requieren estar en el rango del periodo del proyecto";
                                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, cadena));
                                throw new UException(lista);
                            }
                        }
                        foreach (MpProyectorecursoperiodo periodo in recurso.listaPeriodos)
                        {
                            periodo.IdProyecto = bean.IdProyecto;
                            if (UString.estaVacio(recurso.Estado)) recurso.Estado = ConstanteEstadoGenerico.ACTIVO;
                            mpProyectorecursoperiodoDao.registrar(usuarioActual, periodo);
                        }
                        recurso.IdProyecto = bean.IdProyecto;
                        recurso.Nombre = UString.Mayusculas(recurso.Nombre);
                        recurso.Cargo = UString.Mayusculas(recurso.Cargo);
                        if (UString.estaVacio(recurso.Estado)) recurso.Estado = ConstanteEstadoGenerico.ACTIVO;
                        mpProyectorecursoDao.registrar(usuarioActual, recurso);
                    }

                    //if(bean.Gestor != null)
                    //{
                    //    MpProyectorecurso gestor = new MpProyectorecurso();
                    //    gestor.IdProyecto = bean.IdProyecto;
                    //    gestor.IdRecurso = mpProyectorecursoDao.generarCodigo(bean.IdProyecto);
                    //    gestor.IdPersona = bean.Gestor;
                    //    gestor.Area = gestor.IdPersona != null ? 989 : gestor.Area;
                    //    gestor.Rol = "4";

                    //    MpContratacion gestorcontratacion = mpContratacionDao.obtenerUltimoXPersona(gestor.IdPersona.Value);
                    //    gestor.Cargo = maMiscelaneosdetalleDao.obtenerDescripcion("MP", "CARGOMIN", gestorcontratacion.Cargo);

                    //    mpProyectorecursoDao.registrar(usuarioActual, gestor);
                    //    nuevaLista.Add(gestor);
                    //}


                    //foreach (MpProyectorecurso equipo in bean.listaRecursos2)
                    //{
                    //    equipo.IdProyecto = bean.IdProyecto;
                    //    equipo.IdRecurso = bean.listaRecursos.Count > 0 ? equipo.IdRecurso + bean.listaRecursos.Count : equipo.IdRecurso;
                    //    equipo.Area = 989;
                    //    if (UString.estaVacio(equipo.Estado)) equipo.Estado = ConstanteEstadoGenerico.ACTIVO;
                    //    mpProyectorecursoDao.registrar(usuarioActual, equipo);
                    //    nuevaLista.Add(equipo);
                    //}

                    //if (bean.Estadoatencion == "16")
                    //{
                    //    foreach (var item in nuevaLista)
                    //    {
                    //        if (item.IdPersona != null && item.Area == 989)
                    //        {
                    //            MpPersona persona = mpPersonaDao.obtenerPorId(new MpPersonaPk() { IdPersona = item.IdPersona }.obtenerArreglo());
                    //            this.enviarCorreoParticipante(bean, persona);
                    //        }
                    //    }

                    //}
                    //this.enviarCorreo(bean, usuarioActual);
                    bean = mpProyectoDao.registrar(usuarioActual, bean);
                    ts.Complete();
                    return bean;
                }
            }
            catch (Exception)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "Error en el proceso de registro"));
                throw new UException(lista);
            }


        }

        public MpProyecto obtenerPorId(int pIdProyecto)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    MpProyecto bean = mpProyectoDao.obtenerPorId(new MpProyecto() { IdProyecto = pIdProyecto }.obtenerArreglo());
                    bean.listaRecursos = mpProyectorecursoDao.listarRecursos(bean.IdProyecto.Value);

                    foreach (var recurso in bean.listaRecursos)
                    {
                        if (recurso.IdPersona != null)
                        {
                            recurso.listaPeriodos = mpProyectorecursoperiodoDao.listarPeriodos(bean.IdProyecto.Value, recurso.IdPersona.Value);
                        }
                    }
                    ts.Complete();
                    return bean;
                }
            }
            catch (Exception)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "Error en el proceso de consulta"));
                throw new UException(lista);
            }
        }

        public MpProyecto actualizar(UsuarioActual usuarioActual, MpProyecto bean)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    nuevaLista = new List<MpProyectorecurso>();
                    bean.Nombre = UString.Mayusculas(bean.Nombre);
                    bean.Nombrecorto = UString.Mayusculas(bean.Nombrecorto);
                    bean.Expediente = UString.Mayusculas(bean.Expediente);
                    bean.Asunto = UString.Mayusculas(bean.Asunto);
                    bean.Proceso = UString.Mayusculas(bean.Proceso);
                    bean.Observacion = UString.Mayusculas(bean.Observacion);

                    mpProyectoDao.eliminarListarecursos(bean.IdProyecto);

                    foreach (MpProyectorecurso recurso in bean.listaRecursos)
                    {
                        if (recurso.IdPersona != null)
                        {
                            MpContratacion auxcontrato = mpContratacionDao.obtenerUltimoXPersona(recurso.IdPersona.Value); ;
                            mpProyectorecursoDao.eliminarPeriodos(bean.IdProyecto.Value, recurso.IdPersona.Value, auxcontrato.IdContratacion.Value);

                            foreach (MpProyectorecursoperiodo periodo in recurso.listaPeriodos)
                            {
                                if (periodo.IdContratacion == auxcontrato.IdContratacion)
                                {
                                    periodo.IdProyecto = bean.IdProyecto;
                                    if (UString.estaVacio(recurso.Estado)) recurso.Estado = ConstanteEstadoGenerico.ACTIVO;
                                    mpProyectorecursoperiodoDao.registrar(usuarioActual, periodo);
                                }

                            }
                        }
                        recurso.IdProyecto = bean.IdProyecto;
                        recurso.Nombre = UString.Mayusculas(recurso.Nombre);
                        recurso.Cargo = UString.Mayusculas(recurso.Cargo);
                        if (UString.estaVacio(recurso.Estado)) recurso.Estado = ConstanteEstadoGenerico.ACTIVO;
                        mpProyectorecursoDao.registrar(usuarioActual, recurso);
                    }

                    //if (bean.Gestor != null)
                    //{
                    //    MpProyectorecurso gestor = new MpProyectorecurso();
                    //    gestor.IdProyecto = bean.IdProyecto;
                    //    gestor.IdRecurso = mpProyectorecursoDao.generarCodigo(bean.IdProyecto);
                    //    gestor.IdPersona = bean.Gestor;
                    //    gestor.Area = gestor.IdPersona != null ? 989 : gestor.Area;
                    //    gestor.Rol = "4";

                    //    MpContratacion gestorcontratacion = mpContratacionDao.obtenerUltimoXPersona(gestor.IdPersona.Value);
                    //    gestor.Cargo = maMiscelaneosdetalleDao.obtenerDescripcion("MP", "CARGOMIN", gestorcontratacion.Cargo);

                    //    mpProyectorecursoDao.registrar(usuarioActual, gestor);
                    //    nuevaLista.Add(gestor);
                    //}

                    //foreach (MpProyectorecurso equipo in bean.listaRecursos2)
                    //{

                    //    equipo.IdProyecto = bean.IdProyecto;
                    //    equipo.Area = 989;
                    //    if (UString.estaVacio(equipo.Estado)) equipo.Estado = ConstanteEstadoGenerico.ACTIVO;
                    //    mpProyectorecursoDao.registrar(usuarioActual, equipo);
                    //    nuevaLista.Add(equipo);

                    //}

                    //if (bean.Estadoatencion == "16")
                    //{
                    //    foreach (var item in nuevaLista)
                    //    {
                    //        if (item.IdPersona != null && item.Area == 989)
                    //        {
                    //            MpPersona persona = mpPersonaDao.obtenerPorId(new MpPersonaPk() { IdPersona = item.IdPersona }.obtenerArreglo());
                    //            this.enviarCorreoParticipante(bean, persona);
                    //        }
                    //    }

                    //    this.enviarCorreo(bean, usuarioActual);
                    //}
                    bean = mpProyectoDao.actualizar(usuarioActual, bean);
                    ts.Complete();
                    return bean;
                }
            }
            catch (Exception)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "Error en el proceso de actualización"));
                throw new UException(lista);
            }

        }

        public List<MpProyecto> ListarNombres()
        {
            return mpProyectoDao.ListarNombres();

        }

        public MpProyectoPk cambiarestado(MpProyectoPk pk)
        {
            throw new NotImplementedException();
        }

        public List<DtoProyecto> Listardetalle(string pTipo, int? pAnio, string pEstado)
        {
            List<DtoProyecto> lista = new List<DtoProyecto>();

            lista = mpProyectoDao.Listardetalle(pTipo, pAnio, pEstado);

            foreach (var item in lista)
            {
                MpSistema Beansistema = mpSistemaDao.obtenerPorId(new MpSistemaPk() { IdSistema = item.idSIS }.obtenerArreglo());
                if (Beansistema != null)
                {
                    item.NombreSIS = Beansistema.Nombre;
                    item.NomcortoSIS = Beansistema.Siglas;
                }
            }
            return lista;
        }

        public void enviarCorreo(MpProyecto bean, UsuarioActual usuario)
        {

            MailMessage mm = new MailMessage();

            MpPersona auxusuario = mpPersonaDao.ObtenerPersonaxUsuario(usuario.UsuarioLogin);

            mm.To.Add(new MailAddress(auxusuario.Correo != null ? auxusuario.Correo : auxusuario.Correoalterno));
            mm.From = new MailAddress("each1corporativo@gmail.com");
            mm.Subject = "Nuevo proyecto: " + bean.Nombre + (bean.Nombrecorto == null ? null : " ( " + bean.Nombrecorto + " )");
            mm.Body = CreateBody(bean, auxusuario, "REGISTRADOR");
            mm.IsBodyHtml = true;

            NetworkCredential NetworkCred = new NetworkCredential("each1corporativo@gmail.com", "*each1corporativo*");
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);

        }

        public void enviarCorreoParticipante(MpProyecto bean, MpPersona persona)
        {
            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(persona.Correo != null ? persona.Correo : persona.Correoalterno));
            mm.From = new MailAddress("each1corporativo@gmail.com");
            mm.Subject = "Nuevo proyecto: " + bean.Nombre + "( " + bean.Nombrecorto + " )";
            mm.Body = CreateBody(bean, persona, null);
            mm.IsBodyHtml = true;

            NetworkCredential NetworkCred = new NetworkCredential("each1corporativo@gmail.com", "*each1corporativo*");
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }

        private string CreateBody(MpProyecto bean, MpPersona persona, string tipo)
        {
            //MISCELANEOS
            string estadoproyecto = maMiscelaneosdetalleDao.obtenerDescripcion("MP", "ESTPROYECT", bean.Estadoatencion);
            string tipoproyecto = maMiscelaneosdetalleDao.obtenerDescripcion("MP", "TIPOPROYEC", bean.Tipoproyecto);
            string coordinacion = maMiscelaneosdetalleDao.obtenerDescripcion("MP", "COORDINAMP", bean.Coordinacion);

            StringBuilder sb = new StringBuilder();
            sb.Append("<head>");
            if (bean.Estadoatencion == "15")
            {
                sb.Append("<h3>" + persona.Nombre + " " + persona.Apellido + " has registrado un Proyecto con estado '" + estadoproyecto + "'</h3>");
                sb.Append("<h4><i> Nota: Se notificará por correo a las personas agregadas al proyecto y pertenecientes a la Unidad de Sistemas de Información (USI), una vez el proyecto inicie.</i></h4>");
            }
            else
            {
                if (tipo == "REGISTRADOR")
                {
                    sb.Append("<h3>" + persona.Nombre + " " + persona.Apellido + " has iniciado un Proyecto con estado '" + estadoproyecto + "'</h3>");
                }
                else
                {
                    sb.Append("<h3>" + persona.Nombre + " " + persona.Apellido + " bienvenido al equipo. </h3>");
                }

            }
            sb.Append("<h3>ESTE CORREO ES UNA PRUEBA, OMITIR LA INFORMACIÓN RECIBIDA</h3>");
            sb.Append("</head>");

            sb.Append("<body>");
            sb.Append("<h2><u>DATOS GENERALES DEL PROYECTO</u></h2><br>");

            sb.Append("<table>");

            sb.Append("<tr>");
            sb.Append("<td><b> Código: </b></td> ");
            sb.Append("<td> {codigo}</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td><b> Área: </b></td>");
            sb.Append("<td> {area}</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td><b> Tipo: </b></td>");
            sb.Append("<td> {tipo}</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td><b> Coordinación: </b ></td>");
            sb.Append("<td> {coordinacion}</td>");
            sb.Append("</tr>");
            sb.Append("</table><br>"); ;

            if (bean.Estadoatencion == "16")
            {
                sb.Append("<h2><u>PARTICIPANTES</u></h2><br>");
                sb.Append("<div  style='overflow - x: scroll; width: 100 % '>");
                sb.Append("<table border='1' cellspacing='0' cellpadding='2' bordercolor='666633'>");

                //style = 'font-size: bold;' bgcolor = '#FFFFFF'

                //CABECERA
                sb.Append("<tr>");
                sb.Append("<th style='width: 50px; text-align: center' > N° </th>");
                sb.Append("<th style='width: 300px; text-align: center'> Área </th>");
                sb.Append("<th style='width: 250px; text-align: center'> Nombre </th>");
                sb.Append("<th style='width: 150px; text-align: center'> Cargo </th>");
                sb.Append("<th style='width: 150px; text-align: center'> Rol </th>");
                sb.Append("</tr>");

                //DETALLE


                foreach (var item in nuevaLista)
                {
                    string rol = maMiscelaneosdetalleDao.obtenerDescripcion("MP", "ROLPROYECT", item.Rol);

                    //MpPersona auxPersona = mpPersonaDao.obtenerPorId(new MpPersonaPk(){IdPersona = item.IdPersona}.obtenerArreglo());
                    MpAreaminedu auxArea = mpAreamineduDao.obtenerPorId(new MpAreamineduPk() { Sedeid = item.Area }.obtenerArreglo());

                    sb.Append("<tr>");
                    //sb.Append("<td style='width: 50px; text-align: center'> " + item.IdRecurso + "</td>");
                    sb.Append("<td style='width: 300px'> " + auxArea.Descripcion + "( " + auxArea.DescripcionCorta + " )</td>");
                    if (item.Area == 989)
                    {
                        //sb.Append("<td style='width: 250px'> " + auxPersona.Nombre+" "+auxPersona.Apellido +"</td>");
                    }
                    else
                    {
                        sb.Append("<td style='width: 250px'> " + item.Nombre + "</td>");
                    }
                    sb.Append("<td style='width: 150px'> " + item.Cargo + "</td>");
                    sb.Append("<td style='width: 150px'> " + rol + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("</table></div>");
            }

            MpAreaminedu AreaProyect = mpAreamineduDao.obtenerPorId(new MpAreamineduPk() { Sedeid = bean.Area }.obtenerArreglo());
            sb.Append("</body>");
            sb.Replace("{codigo}", bean.Codigoproyecto);
            sb.Replace("{tipo}", tipoproyecto);
            sb.Replace("{coordinacion}", coordinacion);
            sb.Replace("{area}", AreaProyect.Descripcion);


            return sb.ToString();
        }

        public List<DtoTabla> ListarBarraEstados()
        {
            return mpProyectoDao.ListarBarraEstados();
        }

        public List<DtoTabla> ListarBarraTipos()
        {
            return mpProyectoDao.ListarBarraTipos();
        }

        public List<MpProyectorecurso> ListarEquipotecnico(int pIdProyecto)
        {
            List<MpProyectorecurso> lista = new List<MpProyectorecurso>();
            List<MpProyectorecurso> lst = new List<MpProyectorecurso>();

            lista = mpProyectorecursoDao.listarRecursos(pIdProyecto);

            if (lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    if (Int32.Parse(item.Rol) > 4)
                    {
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }

}
