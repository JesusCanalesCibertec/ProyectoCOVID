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
    public class BpMaeprocesofuncionalidadController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpMaeprocesofuncionalidadServicio bpMaeprocesofuncionalidadServicio;

        public BpMaeprocesofuncionalidadController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesofuncionalidadServicio = servicioProveedor.GetService<BpMaeprocesofuncionalidadServicio>();
        }

        [HttpGet("[action]")]
        public List<DtoBpMaeprocesofuncionalidad> listado(string codigo)
        {
            return bpMaeprocesofuncionalidadServicio.listado(codigo);

        }

        [HttpPost("[action]")]
        public BpMaeprocesofuncionalidad registrar([FromBody]BpMaeprocesofuncionalidad bean)
        {
            return bpMaeprocesofuncionalidadServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesofuncionalidad actualizar([FromBody]BpMaeprocesofuncionalidad bean)
        {
            return bpMaeprocesofuncionalidadServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesofuncionalidad obtenerPorId([FromBody]BpMaeprocesofuncionalidadPk llave)
        {
            return bpMaeprocesofuncionalidadServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public BpMaeprocesofuncionalidad eliminar([FromBody]BpMaeprocesofuncionalidad bean)
        {
            bpMaeprocesofuncionalidadServicio.eliminar(bean.IdProceso, bean.IdFuncionalidad);
            return bean;

        }
    }
}