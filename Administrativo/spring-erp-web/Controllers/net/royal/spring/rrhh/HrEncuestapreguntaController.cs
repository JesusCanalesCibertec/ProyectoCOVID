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
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.controladora
{

    [Route("api/spring/rrhh/[controller]")]
    public class HrEncuestapreguntaController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private HrEncuestapreguntaServicio hrEncuestapreguntaServicio;
        private HrEncuestapreguntavaloresServicio hrEncuestapreguntavaloresServicio;

        public HrEncuestapreguntaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestapreguntaServicio = servicioProveedor.GetService<HrEncuestapreguntaServicio>();
            hrEncuestapreguntavaloresServicio = servicioProveedor.GetService<HrEncuestapreguntavaloresServicio>();
        }

        [HttpGet("[action]")]
        public HrEncuestapregunta obtenerPorId(Nullable<Int32> pPregunta)
        {
            return hrEncuestapreguntaServicio.obtenerPorId(pPregunta);
        }

        [HttpPost("[action]")]
        public HrEncuestapregunta actualizar([FromBody]HrEncuestapregunta bean)
        {
            return hrEncuestapreguntaServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrEncuestapregunta actualizarPregunta([FromBody]HrEncuestapregunta bean)
        {
            return hrEncuestapreguntaServicio.actualizarPregunta(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrEncuestapregunta registrar([FromBody]HrEncuestapregunta bean)
        {
            return hrEncuestapreguntaServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrEncuestapregunta registrarPregunta([FromBody]HrEncuestapregunta bean)
        {
            return hrEncuestapreguntaServicio.registrarPregunta(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public void eliminar(Nullable<Int32> pPregunta)
        {
            hrEncuestapreguntaServicio.coreEliminar(pPregunta);
        }

        [HttpPost("[action]")]
        public DtoEncuestapregunta eliminarPregunta([FromBody] DtoEncuestapregunta bean)
        {

            hrEncuestapreguntaServicio.eliminarPregunta(bean.pregunta);
            return bean;
        }

        [HttpGet("[action]")]
        public HrEncuestapregunta anular(Nullable<Int32> pPregunta)
        {
            return hrEncuestapreguntaServicio.coreAnular(_usuarioActual, pPregunta);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarEncuestas([FromBody] FiltroEncuestaPregunta filtro)
        {
            return hrEncuestapreguntaServicio.listarEncuestas(filtro.paginacion, filtro);

        }

        [HttpGet("[action]")]
        public HrEncuestapregunta solicitudObtenerPorId(Nullable<Int32> pk)
        {

            return hrEncuestapreguntaServicio.solicitudObtenerPorId(pk);
        }

        [HttpGet("[action]")]
        public IList<HrEncuestapreguntavalores> listarValores(Nullable<Int32> pk)
        {
            return hrEncuestapreguntavaloresServicio.listarValores(pk);
        }

    }
}
