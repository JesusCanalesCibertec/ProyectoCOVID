using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proyecto.dominio.filtro;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.dao;
using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.sistema.dao;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proyecto.servicio.impl
{

    public class PmTareaServicioImpl : GenericoServicioImpl, PmTareaServicio
    {

        private IServiceProvider servicioProveedor;
        private PmTareaDao pmTareaDao;
        private PmProyectoDao pmProyectoDao;
        private PmTipoproyectoDao pmTipoproyectoDao;
        private BpStatemachineServicio bpStatemachineServicio;
        private BpTransaccionDao bpTransaccionDao;
        private AsAutorizacionDao asAutorizacionDao;
        private ParametrosmastDao parametrosmastDao;
        private PmPlantillaDao pmPlantillaDao;
        private PmPlantillaTareasDao pmPlantillaTareasDao;

        public PmTareaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmTareaDao = servicioProveedor.GetService<PmTareaDao>();
            pmProyectoDao = servicioProveedor.GetService<PmProyectoDao>();
            pmTipoproyectoDao = servicioProveedor.GetService<PmTipoproyectoDao>();
            bpStatemachineServicio = servicioProveedor.GetService<BpStatemachineServicio>();
            bpTransaccionDao = servicioProveedor.GetService<BpTransaccionDao>();
            asAutorizacionDao = servicioProveedor.GetService<AsAutorizacionDao>();
            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
            pmPlantillaDao = servicioProveedor.GetService<PmPlantillaDao>();
            pmPlantillaTareasDao = servicioProveedor.GetService<PmPlantillaTareasDao>();
        }

        public PmTarea coreInsertar(UsuarioActual usuarioActual, PmTarea bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return pmTareaDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public PmTarea coreActualizar(UsuarioActual usuarioActual, PmTarea bean)
        {
            return pmTareaDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PmTarea coreAnular(UsuarioActual usuarioActual, PmTareaPk id)
        {
            return pmTareaDao.coreAnular(usuarioActual, id);
        }

        public PmTarea coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea)
        {
            return pmTareaDao.coreAnular(usuarioActual, pIdProyecto, pIdTarea);
        }

        public void coreEliminar(PmTareaPk id)
        {
            pmTareaDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea)
        {
            pmTareaDao.coreEliminar(pIdProyecto, pIdTarea);
        }


        public PmTarea obtenerPorId(PmTareaPk id)
        {
            return pmTareaDao.obtenerPorId(id);
        }

        public PmTarea obtenerPorId(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea)
        {
            return pmTareaDao.obtenerPorId(pIdProyecto, pIdTarea);
        }

        public List<DtoTarea> listarTareaSegunGrupoProyecto(FiltroTarea filtro)
        {
            return pmTareaDao.listarTareaSegunGrupoProyecto(filtro);
        }

        public PmTarea actualizarTareaIper(PmTarea bean, UsuarioActual usuarioActual)
        {

            PmTarea tarea = pmTareaDao.obtenerPorId(bean.obtenerArreglo());

            /*
             campos a cambiar
             */
            tarea.IdTipo1 = bean.IdTipo1;
            tarea.Nombre = bean.Nombre;
            tarea.Descripcion = bean.Descripcion;
            tarea.IdResponsable = bean.IdResponsable;
            tarea.ProgramacionFechaInicio = bean.ProgramacionFechaInicio;
            tarea.ProgramacionFechaFin = bean.ProgramacionFechaFin;
            tarea.RealFechaFin = bean.RealFechaFin;
            tarea.flagComisionServicio = bean.flagComisionServicio == "true" ? "S" : bean.flagComisionServicio == "false" ? "N" : bean.flagComisionServicio;
            tarea.FechaUltimaActualizacion = DateTime.Now;
            tarea.ModificacionFecha = DateTime.Now;
            tarea.ModificacionUsuario = usuarioActual.UsuarioLogin;

            BpTransaccion transaccion = bpTransaccionDao.obtenerPorId(new BpTransaccionPk(tarea.IdTransaccion).obtenerArreglo());

            if (!UInteger.esCeroOrNulo(tarea.IdResponsable))
            {
                transaccion.IdPersonaReponsable = tarea.IdResponsable;
                bpTransaccionDao.actualizar(transaccion);
            }

            this.pmTareaDao.actualizar(tarea);

            return tarea;
        }

        public PmTarea guardarTareaIper(PmTarea tarea, UsuarioActual usuarioActual)
        {
            tarea.IdTarea = this.pmTareaDao.generarIdTarea(tarea.IdProyecto);

            tarea.CreacionFecha = DateTime.Now;
            tarea.FechaRegistro = DateTime.Now;
            tarea.FechaUltimaActualizacion = DateTime.Now;
            tarea.IdSolicitante = usuarioActual.PersonaId;
            tarea.CreacionUsuario = usuarioActual.UsuarioLogin;

            PmProyecto proyecto = pmProyectoDao.obtenerPorIdProyecto(new PmProyectoPk() { IdPortafolio = 1, IdPrograma = 1, IdProyecto = tarea.IdProyecto });

            if (!UValidador.esNulo(proyecto))
            {
                if (!UValidador.esNulo(proyecto.IdTipoProyecto))
                {
                    PmTipoproyecto tp = pmTipoproyectoDao.obtenerPorId(new PmTipoproyectoPk() { IdTipoProyecto = proyecto.IdTipoProyecto }.obtenerArreglo());
                    if (!UValidador.esNulo(tp))
                    {
                        if (UBoolean.validarFlag(tp.TareaFlgGeneraTransaccion))
                        {

                            int idTransaccion = bpStatemachineServicio.registrarTransaccion(usuarioActual, tp.TareaIdProceso, tarea.Nombre);

                            tarea.IdTransaccion = idTransaccion;
                            BpTransaccion transaccion = bpTransaccionDao.obtenerPorId(new BpTransaccionPk(idTransaccion).obtenerArreglo());

                            if (!UInteger.esCeroOrNulo(tarea.IdResponsable))
                            {
                                transaccion.IdPersonaReponsable = tarea.IdResponsable;
                                bpTransaccionDao.actualizar(transaccion);
                            }

                            tarea.Estado = transaccion.IdEstado;
                        }
                    }
                }
            }
            pmTareaDao.registrar(tarea);
            return tarea;
            //tareaRecursoDao.registrarTareaRecurso(tarea, listaPersonas, usuarioActual);
        }

        public PmTarea obtenerPorIdTarea(PmTareaPk pmTareaPk)
        {
            return pmTareaDao.obtenerPorId(pmTareaPk.obtenerArreglo());
        }

        public PmTarea obtenerPorIdTransaccionParaNotificacion(int idTransaccion)
        {
            return pmTareaDao.obtenerPorIdTransaccionParaNotificacion(idTransaccion);
        }

        public IList<PmTarea> listarTareasPorPersona(int? personaId)
        {
            return pmTareaDao.listarTareasPorPersona(personaId.Value);
        }

        public void asignarResponsable(UsuarioActual usuarioActual, List<DtoTarea> lista)
        {
            foreach (DtoTarea dto in lista)
            {
                PmTarea tarea = pmTareaDao.obtenerPorId(new PmTareaPk() { IdProyecto = dto.idProyecto, IdTarea = dto.idTarea }.obtenerArreglo());
                tarea.IdResponsable = dto.idResponsable;
                pmTareaDao.actualizar(tarea);
            }
        }

        public void generarPermisos(UsuarioActual usuarioActual, List<DtoTarea> lista)
        {
            foreach (DtoTarea dto in lista)
            {
                PmTarea tarea = pmTareaDao.obtenerPorId(new PmTareaPk() { IdProyecto = dto.idProyecto, IdTarea = dto.idTarea }.obtenerArreglo());
                if (UInteger.esCeroOrNulo(tarea.IdResponsable))
                {
                    continue;
                }
                var sol = new AsAutorizacion();
                sol.Empleado = tarea.IdResponsable;
                sol.Solicitadopor = usuarioActual.UsuarioLogin;
                sol.Estado = "S";
                sol.Fecha = tarea.ProgramacionFechaInicio;
                sol.Fechafin = tarea.ProgramacionFechaFin;
                sol.Desde = tarea.ProgramacionFechaInicio;
                sol.Hasta = tarea.ProgramacionFechaInicio;
                sol.Conceptoacceso = parametrosmastDao.obtenerValorExplicacion("CONCEACT", "PS", "999999");
                sol.Ultimafechamodif = DateTime.Now;
                sol.Ultimousuario = usuarioActual.UsuarioLogin;
                var ee = asAutorizacionDao.registrar(sol);
                tarea.Permiso = ee.Numeroproceso;
                pmTareaDao.actualizar(tarea);
            }
        }

        public ParametroPaginacionGenerico listarPlantillasTarea(DtoTabla filtro, ParametroPaginacionGenerico paginacion)
        {
            return pmPlantillaTareasDao.listarPlantillasTarea(filtro, paginacion);
        }

        public List<PmPlantilla> listarPlantillas()
        {
            return pmPlantillaDao.listarPlantillas();
        }

        public void agregarPlantilla(UsuarioActual usuarioActual, DtoProyecto dto)
        {
            IList<PmPlantillaTareas> tareas = pmPlantillaTareasDao.listarPlantillasTarea(new DtoTabla() { Id = dto.auxPlantilla.Value });

            List<PmPlantillaTareas> tareasOrdenado = tareas.OrderBy(o => o.Orden).ToList();

            PmProyecto proyecto = pmProyectoDao.obtenerPorIdProyecto(new PmProyectoPk() { IdPortafolio = 1, IdPrograma = 1, IdProyecto = dto.IdProyecto });

            var inicio = new DateTime(proyecto.PlanFechaInicio.Value.Year, proyecto.PlanFechaInicio.Value.Month, proyecto.PlanFechaInicio.Value.Day);

            foreach (PmPlantillaTareas item in tareasOrdenado)
            {
                var tarea = new PmTarea();
                tarea.flagComisionServicio = item.FlagComision;
                tarea.Nombre = item.Nombre;
                tarea.Descripcion = item.Descripcion;
                tarea.IdProyecto = dto.IdProyecto;

                tarea.ProgramacionFechaInicio = inicio;
                inicio = inicio.AddDays(item.Dias.HasValue ? item.Dias.Value : 1);
                tarea.ProgramacionFechaFin = inicio;

                tarea.IdSolicitante = usuarioActual.PersonaId;
                tarea.IdTipo1 = item.Tipo;

                guardarTareaIper(tarea, usuarioActual);
                inicio = inicio.AddDays(1);
                //generar tareas
            }
        }
    }
}
