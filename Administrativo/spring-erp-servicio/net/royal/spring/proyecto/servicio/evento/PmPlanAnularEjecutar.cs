using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.servicio.evento;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.dominio;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proceso.dao;

namespace net.royal.spring.proyecto.servicio.evento
{
    public class PmPlanAnularEjecutar : BpEjecutarServicio
    {
        private IServiceProvider servicioProveedor;
        private BpStatemachineServicio bpStatemachineServicio;
        private PmProyectoDao pmProyectoDao;
        private PmTareaDao pmTareaDao;
        private BpProcesoconexionServicio bpProcesoconexionServicio;
        private BpTransaccionDao bpTransaccionDao;

        public PmPlanAnularEjecutar(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpStatemachineServicio = servicioProveedor.GetService<BpStatemachineServicio>();
            pmProyectoDao = servicioProveedor.GetService<PmProyectoDao>();
            pmTareaDao = servicioProveedor.GetService<PmTareaDao>();
            bpProcesoconexionServicio = servicioProveedor.GetService<BpProcesoconexionServicio>();
            bpTransaccionDao = servicioProveedor.GetService<BpTransaccionDao>();
        }

        public void ejecutar(UsuarioActual usuarioActual, BpTransaccion bpTransaccion, BpProcesoconexion bpProcesoConexion)
        {
            PmProyecto pmProyecto = pmProyectoDao.obtenerPorIdTransaccion(bpTransaccion.IdTransaccion.Value);

            if (pmProyecto == null)
            {
                return;
            }

            List<DtoTarea> lstTareas = pmTareaDao.listarTareasNoFinalizadas(usuarioActual, pmProyecto);
            foreach (DtoTarea dtoTarea in lstTareas)
            {
                BpTransaccion bpTranTarea = bpTransaccionDao.obtenerPorId(new BpTransaccionPk() { IdTransaccion = dtoTarea.idTransaccion }.obtenerArreglo());
                if (bpTranTarea != null)
                {

                    bpProcesoConexion.AuxTransaccion = dtoTarea.idTransaccion;
                    bpProcesoConexion.AuxComentario = "Anulación automatica por anulación del plan";
                    bpProcesoConexion.SalidaIdElemento = "FINANULADO";
                    bpProcesoConexion.SalidaIdProceso = bpTranTarea.IdProceso;
                    bpProcesoconexionServicio.registrarSeguimiento(bpProcesoConexion, usuarioActual);

                }
            }
        }
    }
}
