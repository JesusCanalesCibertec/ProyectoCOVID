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
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso
{
    [Route("api/spring/proceso/[controller]")]
    public class BpMaeprocesoestadoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpMaeprocesoestadoServicio bpMaeprocesoestadoServicio;

        public BpMaeprocesoestadoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesoestadoServicio = servicioProveedor.GetService<BpMaeprocesoestadoServicio>();
        }

        [HttpGet("[action]")]
        public List<BpMaeprocesoestado> listarEstadoPorProceso(String idProceso)
        {
            return bpMaeprocesoestadoServicio.listarEstadoPorProceso(idProceso);
        }

        [HttpGet("[action]")]
        public List<DtoBpMaeprocesoestado> listado(string codigo)
        {
            return bpMaeprocesoestadoServicio.listado(codigo);

        }

        [HttpPost("[action]")]
        public BpMaeprocesoestado registrar([FromBody]BpMaeprocesoestado bean)
        {
            return bpMaeprocesoestadoServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesoestado actualizar([FromBody]BpMaeprocesoestado bean)
        {
            return bpMaeprocesoestadoServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesoestado obtenerPorId([FromBody]BpMaeprocesoestadoPk llave)
        {
            return bpMaeprocesoestadoServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public BpMaeprocesoestado eliminar([FromBody]BpMaeprocesoestado bean)
        {
            bpMaeprocesoestadoServicio.eliminar(bean.IdProceso, bean.IdEstado);
            return bean;

        }
    }
}