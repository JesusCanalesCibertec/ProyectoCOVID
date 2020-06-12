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

namespace net.royal.spring.proceso
{
    [Route("api/spring/proceso/[controller]")]
    public class BpMaeprocesorolController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpMaeprocesorolServicio BpMaeprocesorolServicio;

        public BpMaeprocesorolController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            BpMaeprocesorolServicio = servicioProveedor.GetService<BpMaeprocesorolServicio>();
        }

        [HttpGet("[action]")]
        public List<BpMaeprocesorol> listado(string codigo)
        {
            return BpMaeprocesorolServicio.listado(codigo);

        }

        [HttpPost("[action]")]
        public BpMaeprocesorol registrar([FromBody]BpMaeprocesorol bean)
        {
            return BpMaeprocesorolServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesorol actualizar([FromBody]BpMaeprocesorol bean)
        {
            return BpMaeprocesorolServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesorol obtenerPorId([FromBody]BpMaeprocesorolPk llave)
        {
            return BpMaeprocesorolServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public BpMaeprocesorol eliminar([FromBody]BpMaeprocesorol bean)
        {
            BpMaeprocesorolServicio.eliminar(bean.IdProceso, bean.IdRol);
            return bean;
        
        }




    }
}