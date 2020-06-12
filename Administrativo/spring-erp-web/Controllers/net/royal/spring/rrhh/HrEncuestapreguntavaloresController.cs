using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace net.royal.spring.rrhh.controladora
{

    [Route("api/spring/rrhh/[controller]")]
    public class HrEncuestapreguntavaloresController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrEncuestapreguntavaloresServicio hrEncuestapreguntavaloresServicio;

        public HrEncuestapreguntavaloresController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestapreguntavaloresServicio = servicioProveedor.GetService<HrEncuestapreguntavaloresServicio>();
        }

        [HttpGet("[action]")]
        public HrEncuestapreguntavalores obtenerPorId(Nullable<Int32> pPregunta, Nullable<Int32> pValor)
        {
            return hrEncuestapreguntavaloresServicio.obtenerPorId(pPregunta, pValor);
        }

        [HttpPost("[action]")]
        public HrEncuestapreguntavalores actualizar([FromBody]HrEncuestapreguntavalores bean)
        {
            return hrEncuestapreguntavaloresServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
    
        public HrEncuestapreguntavalores registrar([FromBody]HrEncuestapreguntavalores bean)
        {
            return hrEncuestapreguntavaloresServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public void eliminar(Nullable<Int32> pPregunta, Nullable<Int32> pValor)
        {
            hrEncuestapreguntavaloresServicio.coreEliminar(pPregunta, pValor);
        }


        [HttpPost("[action]")]
        public List<HrEncuestapreguntavalores> listarValores(Nullable<Int32> numero)
        {
            return hrEncuestapreguntavaloresServicio.listarValores(numero);

        }



    }
}
