using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.dao;

namespace net.royal.spring.proyecto.servicio.impl
{

    public class PmNotificacionServicioImpl : GenericoServicioImpl, PmNotificacionServicio
    {

        private IServiceProvider servicioProveedor;
        private PmNotificacionDao pmNotificacionDao;
        private PmNotificacionpersonaDao pmNotificacionpersonaDao;
        private BpMaeprocesoDao bpMaeprocesoDao;

        public PmNotificacionServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmNotificacionDao = servicioProveedor.GetService<PmNotificacionDao>();
            pmNotificacionpersonaDao = servicioProveedor.GetService<PmNotificacionpersonaDao>();
            bpMaeprocesoDao = servicioProveedor.GetService<BpMaeprocesoDao>();
        }

        public void eliminarNotificacionesPorTransaccion(UsuarioActual usuarioActual, int? idTransaccion)
        {
            List<PmNotificacion> lst = pmNotificacionDao.listarPorIdTransaccion(idTransaccion);

            foreach (PmNotificacion pmNotificacion in lst)
            {
                List<PmNotificacionpersona> lst2 = pmNotificacionpersonaDao.listarPorNotificacion(pmNotificacion as PmNotificacionPk);
                foreach (PmNotificacionpersona pmNotificacionPersona in lst2)
                {
                    pmNotificacionpersonaDao.eliminar(pmNotificacionPersona);
                }
                pmNotificacionDao.eliminar(pmNotificacion);
            }
        }

        public void generarNotificacionExterna(UsuarioActual usuarioActual, DtoInterfazNotificacion dtoInterfazNotificacion)
        {
            if (dtoInterfazNotificacion.listaUsuarios == null)
                dtoInterfazNotificacion.listaUsuarios = new List<DtoTransaccionUsuario>();

            if (dtoInterfazNotificacion.listaUsuarios.Count == 0)
                return;

            PmNotificacion pmNotificacion = new PmNotificacion();
            pmNotificacion.IdNotificacion = pmNotificacionDao.generarIdNotificacion();
            pmNotificacion.Titulo = dtoInterfazNotificacion.titulo;
            pmNotificacion.Descripcion = dtoInterfazNotificacion.descripcion;
            pmNotificacion.ProcesoId = dtoInterfazNotificacion.procesoId;
            pmNotificacion.ProcesoIdTransaccion = dtoInterfazNotificacion.procesoIdTransaccion;
            pmNotificacion.ProcesoAccion = dtoInterfazNotificacion.procesoAccion;
            pmNotificacion.ProcesoParametros = dtoInterfazNotificacion.procesoParametros;
            pmNotificacion.FuenteNotificacion = dtoInterfazNotificacion.fuente;

            pmNotificacion.CreacionFecha = DateTime.Now;
            pmNotificacion.CreacionUsuario = usuarioActual.UsuarioLogin;
            pmNotificacionDao.registrar(pmNotificacion);


            foreach (DtoTransaccionUsuario bean in dtoInterfazNotificacion.listaUsuarios)
            {
                PmNotificacionpersona pmNotificacionPersona = null;
                pmNotificacionPersona = pmNotificacionpersonaDao.obtenerPorId(new PmNotificacionpersonaPk(pmNotificacion.IdNotificacion, bean.idPersona).obtenerArreglo());

                if (pmNotificacionPersona == null)
                {
                    pmNotificacionPersona = new PmNotificacionpersona();
                    pmNotificacionPersona.IdNotificacion = pmNotificacion.IdNotificacion;
                    pmNotificacionPersona.IdPersona = bean.idPersona;
                    pmNotificacionPersona.Estado = "A";
                    pmNotificacionPersona.CreacionFecha = DateTime.Now;
                    pmNotificacionPersona.CreacionUsuario = usuarioActual.UsuarioLogin;
                    pmNotificacionpersonaDao.registrar(pmNotificacionPersona);
                }
            }
        }

        public List<PmNotificacion> listarNotificacionesPorPersona(UsuarioActual usuarioActual)
        {
            var idPersona = usuarioActual.PersonaId;
            List<PmNotificacion> listaNotificaciones = new List<PmNotificacion>();
            List<PmNotificacionpersona> listaNotificacionesPersona = pmNotificacionpersonaDao.listarPorPersona(idPersona);
            foreach (PmNotificacionpersona pmNotificacionPersona in listaNotificacionesPersona)
            {
                PmNotificacionPk pk = new PmNotificacionPk();
                pk.IdNotificacion = pmNotificacionPersona.IdNotificacion;
                PmNotificacion pmNotificacion = pmNotificacionDao.obtenerPorId(pk.obtenerArreglo());
                BpMaeproceso mae = bpMaeprocesoDao.obtenerPorId(new BpMaeprocesoPk(pmNotificacion.ProcesoId).obtenerArreglo());
                pmNotificacion.IdColor = mae.IconoHojaEstilo;
                pmNotificacion.Componente = mae.Componente;
                listaNotificaciones.Add(pmNotificacion);
            }
            return listaNotificaciones;
        }
    }
}
