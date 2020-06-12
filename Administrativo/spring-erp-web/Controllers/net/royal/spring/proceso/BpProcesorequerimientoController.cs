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
    public class BpProcesorequerimientoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpProcesorequerimientoServicio BpProcesorequerimientoServicio;

        public BpProcesorequerimientoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            BpProcesorequerimientoServicio = servicioProveedor.GetService<BpProcesorequerimientoServicio>();
        }

      

        [HttpGet("[action]")]
        public List<DtoBpProcesorequerimiento> listado(string codigo)
        {
            return BpProcesorequerimientoServicio.listado(codigo);

        }

        [HttpPost("[action]")]
        public BpProcesorequerimiento registrar([FromBody]BpProcesorequerimiento bean)
        {
            return BpProcesorequerimientoServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpProcesorequerimiento actualizar([FromBody]BpProcesorequerimiento bean)
        {
            return BpProcesorequerimientoServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpProcesorequerimiento obtenerPorId([FromBody]BpProcesorequerimientoPk llave)
        {
            return BpProcesorequerimientoServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public BpProcesorequerimiento eliminar([FromBody]BpProcesorequerimiento bean)
        {
            BpProcesorequerimientoServicio.eliminar(bean.IdProceso, bean.IdRequerimiento,bean.IdVersion);
            return bean;

        }
    }
}