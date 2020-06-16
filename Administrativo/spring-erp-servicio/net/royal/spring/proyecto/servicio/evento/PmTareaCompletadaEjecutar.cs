using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio.evento;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.dominio;

namespace net.royal.spring.proyecto.servicio.evento
{
    public class PmTareaCompletadaEjecutar : BpEjecutarServicio
    {
        private IServiceProvider servicioProveedor;
        private PmTareaDao pmTareaDao;

        public PmTareaCompletadaEjecutar(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmTareaDao = servicioProveedor.GetService<PmTareaDao>();

        }
        public void ejecutar(UsuarioActual usuarioActual, BpTransaccion bpTransaccion, BpProcesoconexion bpProcesoConexion)
        {
            /*if (bpProcesoConexion.SalidaIdElemento == "FIN")
            {
                PmTarea tareaPrincipal = pmTareaDao.obtenerPorIdTransaccion(bpTransaccion.IdTransaccion.Value);

                if (tareaPrincipal == null)
                    return;

                String idExterno = tareaPrincipal.IdProyecto.ToString() + "-" + tareaPrincipal.IdTarea.ToString();
                List<PmPretarea> lst = pmPretareaDao.listarPorExternoActivos(ConstanteExterno.PROYECTO_PMTAREA, idExterno);
                for (PmPretarea pmPretarea : lst)
                {
                    PmTarea tarea = new PmTarea();
                    tarea.getPk().setIdProyecto(tareaPrincipal.getPk().getIdProyecto());
                    tarea.setIdTareaPadre(tareaPrincipal.getPk().getIdTarea());
                    tarea.setNombre(pmPretarea.getNombre());
                    tarea.setDescripcion(pmPretarea.getComentarios());
                    tarea.setIdResponsable(pmPretarea.getIdResponsable());
                    tarea.setProgramacionFechaInicio(pmPretarea.getFechaInicio());
                    tarea.setProgramacionFechaFin(pmPretarea.getFechaFin());
                    tarea.setIdTipoTarea(PmTarea.TIPO_TAREA_NORMAL);
                    tarea = pmTareaDao.guardarTarea(tarea, usuarioActual, null);

                    pmPretarea.setTareaIdProyecto(tarea.IdProyecto);
                    pmPretarea.setTareaId(tarea.IdTarea);
                    pmPretarea.setEstado("P");
                    pmPretarea.setModificacionFecha(new Date());
                    pmPretarea.setModificacionTerminal(usuarioActual.getUsuarioIpAddress());
                    pmPretarea.setModificacionUsuario(usuarioActual.getUsuarioLogin());
                    pmPretareaDao.actualizar(pmPretarea);
                }*/
        }
    }
}
