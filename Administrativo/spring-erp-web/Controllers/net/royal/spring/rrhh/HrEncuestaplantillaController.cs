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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh.controladora
{

    [Route("api/spring/rrhh/[controller]")]
    public class HrEncuestaplantillaController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrEncuestaplantillaServicio hrEncuestaplantillaServicio;

        public HrEncuestaplantillaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        { 
            servicioProveedor = _servicioProveedor;
            hrEncuestaplantillaServicio = servicioProveedor.GetService<HrEncuestaplantillaServicio>();
        }

        [HttpGet("[action]")]
        public HrEncuestaplantilla obtenerPorId(int pPlantilla)
        {
            return hrEncuestaplantillaServicio.obtenerPorId(pPlantilla);
        }

        [HttpPost("[action]")]
        public HrEncuestaplantilla actualizar([FromBody]HrEncuestaplantilla bean)
        {
            return hrEncuestaplantillaServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrEncuestaplantilla registrar([FromBody]HrEncuestaplantilla bean)
        {
            return hrEncuestaplantillaServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public HrEncuestaplantilla anular(Nullable<Int32> pPlantilla)
        {
           return hrEncuestaplantillaServicio.coreAnular(_usuarioActual,pPlantilla);
        }

        [HttpPost("[action]")]
        public DtoTabla eliminar([FromBody] DtoTabla bean)
        {
            hrEncuestaplantillaServicio.coreEliminar(bean.CodigoNumerico);
            return bean;
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] DtoTabla filtro)
        {
            return hrEncuestaplantillaServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public List<HrEncuestadetalle> listarPorPlantilla([FromBody] DtoTabla filtro)
        {
            return hrEncuestaplantillaServicio.listarPorPlantilla(filtro.Id.Value);

        }

       


    }
}
