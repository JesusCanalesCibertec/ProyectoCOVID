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
using net.royal.spring.sistema.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.proceso
{
    [Route("api/spring/rrhh/[controller]")]
    public class HrPuestoempresaController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private HrPuestoempresaServicio hrPuestoempresaServicio;

        public HrPuestoempresaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrPuestoempresaServicio = servicioProveedor.GetService<HrPuestoempresaServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroPuestoempresa filtro)
        {
            return hrPuestoempresaServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public HrPuestoempresa obtenerPorId([FromBody]HrPuestoempresaPk llave)
        {
            return hrPuestoempresaServicio.obtenerPorId(llave);
        }



    }
}