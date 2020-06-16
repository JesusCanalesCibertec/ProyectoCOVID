using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.kpi.dao;
using net.royal.spring.kpi.servicio;
using net.royal.spring.kpi.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.kpi.controladora
{

    [Route("api/spring/kpi/[controller]")]
    public class RtIndicadorController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private RtIndicadorServicio rtIndicadorServicio;

        public RtIndicadorController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            rtIndicadorServicio = servicioProveedor.GetService<RtIndicadorServicio>();
        }

        [HttpPost("[action]")]
        public RtIndicador registrar([FromBody]RtIndicador bean)
        {
            return rtIndicadorServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public RtIndicador actualizar([FromBody]RtIndicador bean)
        {
            return rtIndicadorServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public RtIndicador anular(String pIdIndicador)
        {
            return rtIndicadorServicio.coreAnular(_usuarioActual, pIdIndicador);
        }

        [HttpPost("[action]")]
        public void eliminar(String pIdIndicador)
        {
            rtIndicadorServicio.coreEliminar(pIdIndicador);
        }

        [HttpGet("[action]")]
        public RtIndicador obtenerporid(String pIdIndicador)
        {
            return rtIndicadorServicio.obtenerPorId(pIdIndicador);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroIndicador filtro)
        {
            return rtIndicadorServicio.listarPaginacion(filtro.paginacion, filtro);

        }



        [HttpPost("[action]")]
        public DtoIndicador eliminarfila([FromBody] DtoIndicador bean)
        {

            rtIndicadorServicio.eliminarfila(bean.codigo);
            return bean;
        }


        [HttpGet("[action]")]
        public List<RtIndicador> listarPorPrograma(String programa)
        {
            return rtIndicadorServicio.listarPorPrograma(programa);
        }

    }
}
