using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.framework.web.controller;
using net.royal.spring.proceso.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso
{
    [Route("api/spring/proceso/[controller]")]
    public class BpProcesoconexionController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpProcesoconexionServicio bpProcesoconexionServicio;
        private BpMaeprocesoelementoServicio bpMaeprocesoelementoServicio;
        private PmProyectoServicio pmProyectoServicio;
        private BpTransaccionServicio bpTransaccionServicio;

        public BpProcesoconexionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            bpProcesoconexionServicio = servicioProveedor.GetService<BpProcesoconexionServicio>();
            bpMaeprocesoelementoServicio = servicioProveedor.GetService<BpMaeprocesoelementoServicio>();
            pmProyectoServicio = servicioProveedor.GetService<PmProyectoServicio>();
            bpTransaccionServicio = servicioProveedor.GetService<BpTransaccionServicio>();
        }

        [HttpGet("[action]")]
        public DtoBpProcesoconexion listarTransacciones(Int32? idTransaccionPadre, Int32 idTransaccion, String idProceso)
        {
            return bpProcesoconexionServicio.listarTransacciones(idTransaccionPadre, idTransaccion, idProceso, _usuarioActual);
        }

        [HttpGet("[action]")]
        public DtoTabla obtenerLeyendaPorProceso(String idProceso)
        {
            return new DtoTabla() { Descripcion = bpProcesoconexionServicio.obtenerLeyendaPorProceso(idProceso, _usuarioActual) };
        }

        [HttpPost("[action]")]
        public DtoTabla registrarNotificacionCorreo([FromBody]BpProcesoconexion bpProcesoconexion)
        {
            bpProcesoconexion.AuxTransaccionBean = bpTransaccionServicio.obtenerPorId(bpProcesoconexion.AuxTransaccion.Value);
            bpProcesoconexion.auxElementoEntrada = bpMaeprocesoelementoServicio.obtenerPorId(new BpMaeprocesoelementoPk() { IdElemento = bpProcesoconexion.EntradaIdElemento, IdProceso = bpProcesoconexion.EntradaIdProceso });
            bpProcesoconexion.auxElementoSalida = bpMaeprocesoelementoServicio.obtenerPorId(new BpMaeprocesoelementoPk() { IdElemento = bpProcesoconexion.SalidaIdElemento, IdProceso = bpProcesoconexion.SalidaIdProceso });
            bpProcesoconexion.IdExterno = null;
            bpProcesoconexionServicio.registrarNotificacionCorreo(bpProcesoconexion, _usuarioActual);
            return new DtoTabla() { Descripcion = "OK" };
        }

        [HttpPost("[action]")]
        public DtoTabla validarBpStateMachineServicio([FromBody]BpProcesoconexion bpProcesoconexion)
        {
            bpProcesoconexion.AuxTransaccionBean = bpTransaccionServicio.obtenerPorId(bpProcesoconexion.AuxTransaccion.Value);
            bpProcesoconexion.auxElementoEntrada = bpMaeprocesoelementoServicio.obtenerPorId(new BpMaeprocesoelementoPk() { IdElemento = bpProcesoconexion.EntradaIdElemento, IdProceso = bpProcesoconexion.EntradaIdProceso });
            bpProcesoconexion.auxElementoSalida = bpMaeprocesoelementoServicio.obtenerPorId(new BpMaeprocesoelementoPk() { IdElemento = bpProcesoconexion.SalidaIdElemento, IdProceso = bpProcesoconexion.SalidaIdProceso });
            bpProcesoconexionServicio.validarBpStateMachineServicio(bpProcesoconexion, _usuarioActual);
            return new DtoTabla() { Descripcion = "OK" };
        }

        [HttpPost("[action]")]
        public PmProyecto registrarProyecto([FromBody]PmProyecto pmProyecto)
        {
            return this.pmProyectoServicio.registrarProyecto(pmProyecto, _usuarioActual);
        }

        [HttpPost("[action]")]
        public PmProyecto actualizarProyecto([FromBody]PmProyecto pmProyecto)
        {
            return this.pmProyectoServicio.actualizarProyecto(pmProyecto, _usuarioActual);
        }


        /*ERNESTO*/
        [HttpGet("[action]")]
        public List<DtoBpProcesoconexion> listado(string codigo)
        {
            return bpProcesoconexionServicio.listado(codigo);

        }

        [HttpPost("[action]")]
        public BpProcesoconexion registrar([FromBody]BpProcesoconexion bean)
        {
            return bpProcesoconexionServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpProcesoconexion actualizar([FromBody]BpProcesoconexion bean)
        {
            return bpProcesoconexionServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpProcesoconexion obtenerPorId([FromBody]BpProcesoconexionPk llave)
        {
            return bpProcesoconexionServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public BpProcesoconexion eliminar([FromBody]BpProcesoconexion bean)
        {
            bpProcesoconexionServicio.eliminar(bean.IdProceso, bean.IdConexion, bean.IdVersion);
            return bean;

        }
        /*ERNESTO*/


    }
}