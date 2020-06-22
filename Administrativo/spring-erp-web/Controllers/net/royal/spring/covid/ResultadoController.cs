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
using net.royal.spring.covid.dominio.filtro;

namespace net.royal.spring.covid
{
    [Route("api/spring/covid/[controller]")]
    public class   ResultadoController:BaseController
    {
        private IServiceProvider servicioProveedor;
        private ResultadoServicio resultadoServicio;

        public ResultadoController(IServiceProvider _servicioProveedor , IHttpContextAccessor httpContextAccessor):base(httpContextAccessor, _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            resultadoServicio = servicioProveedor.GetService<ResultadoServicio>();
        }

        //[HttpPost("[action]")]
        //public ParametroPaginacionGenerico ListarPaginacion([FromBody] FiltroResultado filtro)
        //{
        //    return ResultadoServicio.listarPaginacion(filtro.paginacion, filtro);
        //}

        [HttpPost("[action]")]
        public Resultado Registrar([FromBody]Resultado bean)
        {
            return resultadoServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public Resultado ObtenerPorId(int pIdResultado)
        {
            return resultadoServicio.obtenerPorId(pIdResultado);
        }


        [HttpPost("[action]")]
        public Resultado Actualizar([FromBody]Resultado bean)
        {
            return resultadoServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public ResultadoPk Cambiarestado([FromBody]ResultadoPk pk)
        {
            return resultadoServicio.cambiarestado(pk);
        }

        [HttpPost("[action]")]
        public List<Resultado> listado([FromBody]DtoTabla filtro)
        {
            return resultadoServicio.listado(filtro);

        }


    }
}
