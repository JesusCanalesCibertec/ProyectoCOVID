using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.covid.servicio;
using net.royal.spring.covid.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace net.royal.spring.covid
{
    [Route("api/spring/covid/[controller]")]
    public class   ConocimientoController:BaseController
    {
        private IServiceProvider servicioProveedor;
        private ConocimientoServicio conocimientoServicio;

        public ConocimientoController(IServiceProvider _servicioProveedor , IHttpContextAccessor httpContextAccessor):base(httpContextAccessor, _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            conocimientoServicio = servicioProveedor.GetService<ConocimientoServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] DtoTabla filtro)
        {
            return conocimientoServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public Conocimiento Registrar([FromBody]Conocimiento bean)
        {
            return conocimientoServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public Conocimiento ObtenerPorId(int pIdConocimiento)
        {
            return conocimientoServicio.obtenerPorId(pIdConocimiento);
        }


        [HttpPost("[action]")]
        public Conocimiento Actualizar([FromBody]Conocimiento bean)
        {
            return conocimientoServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public ConocimientoPk Cambiarestado([FromBody]ConocimientoPk pk)
        {
            return conocimientoServicio.cambiarestado(pk);
        }

        [HttpPost("[action]")]
        public List<Conocimiento> listado([FromBody]DtoTabla filtro)
        {
            return conocimientoServicio.listado(filtro);

        }


    }
}
