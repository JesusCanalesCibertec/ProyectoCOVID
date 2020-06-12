using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio.evento;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proceso.dao;
using net.royal.spring.framework.core;

namespace net.royal.spring.proyecto.servicio.evento
{
    public class PmTareaRegistrarReaprobarPlanEjecutar : BpEjecutarServicio
    {

        private IServiceProvider servicioProveedor;
        private BpStatemachineServicio bpStatemachineServicio;
        private PmProyectoDao pmProyectoDao;
        private BpProcesoconexionServicio bpProcesoconexionServicio;
        private BpTransaccionDao bpTransaccionDao;

        public PmTareaRegistrarReaprobarPlanEjecutar(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpStatemachineServicio = servicioProveedor.GetService<BpStatemachineServicio>();
            pmProyectoDao = servicioProveedor.GetService<PmProyectoDao>();
            bpProcesoconexionServicio = servicioProveedor.GetService<BpProcesoconexionServicio>();
            bpTransaccionDao = servicioProveedor.GetService<BpTransaccionDao>();
        }

        public void ejecutar(UsuarioActual usuarioActual, BpTransaccion bpTransaccion, BpProcesoconexion bpProcesoConexion)
        {
            if (UInteger.esCeroOrNulo(bpProcesoConexion.AuxInt))
            {
                throw new UException("socorrooooooooooooooooooooo!!!");
            }

            //EL ID PROYECTO NO LLEGA
            PmProyecto pmProyecto = pmProyectoDao.obtenerPorIdProyecto(new PmProyectoPk() { IdProyecto = bpProcesoConexion.AuxInt });

            BpTransaccion bpTransaccionProyecto = bpTransaccionDao.obtenerPorId(new BpTransaccionPk(pmProyecto.IdTransaccion).obtenerArreglo());

            if (bpTransaccionProyecto.ActualIdElemento == "SEGUIMIENTO")
            {
                bpProcesoConexion.AuxTransaccion = bpTransaccionProyecto.IdTransaccion;
                bpProcesoConexion.AuxComentario = "Devolución de plan automática para su aprobación de plan";
                bpProcesoConexion.SalidaIdElemento = "APROBAR";
                bpProcesoConexion.SalidaIdProceso = bpTransaccionProyecto.IdProceso;
                bpProcesoconexionServicio.registrarSeguimiento(bpProcesoConexion, usuarioActual);
            }
        }
    }
}
