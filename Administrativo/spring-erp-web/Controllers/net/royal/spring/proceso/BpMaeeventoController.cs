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
    public class BpMaeeventoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        //private BpMaeeventoServicio BpMaeeventoServicio;

        public BpMaeeventoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
         //   BpMaeeventoServicio = servicioProveedor.GetService<BpMaeeventoServicio>();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroRequerimiento filtro)
        {
            // return BpMaerequerimientoServicio.listarPaginacion(filtro.paginacion, filtro);
            return null;

        }


    }
}