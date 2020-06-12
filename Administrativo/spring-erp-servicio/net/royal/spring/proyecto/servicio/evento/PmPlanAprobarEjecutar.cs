using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio.evento;
using net.royal.spring.proyecto.dominio;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proceso.dao;

namespace net.royal.spring.proyecto.servicio.evento
{
    public class PmPlanAprobarEjecutar : BpEjecutarServicio
    {
        private IServiceProvider servicioProveedor;
        private BpStatemachineServicio bpStatemachineServicio;
        private PmProyectoDao pmProyectoDao;
        private PmTareaDao pmTareaDao;
        private BpProcesoconexionServicio bpProcesoconexionServicio;
        private BpTransaccionDao bpTransaccionDao;

        public PmPlanAprobarEjecutar(IServiceProvider _servicioProveedor)
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

            List<PmTarea> lstTareas = pmTareaDao.listarPorProyecto(pmProyecto.IdProyecto);
            foreach (PmTarea pmTarea in lstTareas)
            {
                BpTransaccion bpTranTarea = bpTransaccionDao.obtenerPorId(new BpTransaccionPk(pmTarea.IdTransaccion).obtenerArreglo());
                if (bpTranTarea != null)
                {
                    if (bpTranTarea.ActualIdElemento == "APROBAR" || bpTranTarea.ActualIdElemento == "PREPARAR")
                    {
                        bpProcesoConexion.AuxTransaccion = pmTarea.IdTransaccion;
                        bpProcesoConexion.AuxComentario = "Aprobación automatica por aprobación de plan";
                        bpProcesoConexion.SalidaIdElemento = "SEGUIMIENTO";
                        bpProcesoConexion.SalidaIdProceso = bpTranTarea.IdProceso;
                        bpProcesoconexionServicio.registrarSeguimiento(bpProcesoConexion, usuarioActual);
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
        }
    }
}
