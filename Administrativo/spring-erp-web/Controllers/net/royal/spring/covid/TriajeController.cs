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
    public class   TriajeController:BaseController
    {
        private IServiceProvider servicioProveedor;
        private TriajeServicio triajeServicio;

        public TriajeController(IServiceProvider _servicioProveedor , IHttpContextAccessor httpContextAccessor):base(httpContextAccessor, _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            triajeServicio = servicioProveedor.GetService<TriajeServicio>();
        }

        //[HttpPost("[action]")]
        //public ParametroPaginacionGenerico ListarPaginacion([FromBody] FiltroTriaje filtro)
        //{
        //    return TriajeServicio.listarPaginacion(filtro.paginacion, filtro);
        //}

        [HttpPost("[action]")]
        public Triaje Registrar([FromBody]Triaje bean)
        {
            return triajeServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public Triaje ObtenerPorId(int pIdTriaje)
        {
            return triajeServicio.obtenerPorId(pIdTriaje);
        }


        [HttpPost("[action]")]
        public Triaje Actualizar([FromBody]Triaje bean)
        {
            return triajeServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public TriajePk Cambiarestado([FromBody]TriajePk pk)
        {
            return triajeServicio.cambiarestado(pk);
        }

        [HttpPost("[action]")]
        public List<Triaje> listado([FromBody]DtoTabla filtro)
        {
            return triajeServicio.listado(filtro);

        }


    }
}
