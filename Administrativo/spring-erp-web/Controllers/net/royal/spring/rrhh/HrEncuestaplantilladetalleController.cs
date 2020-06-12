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
    public class HrEncuestaplantilladetalleController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrEncuestaplantilladetalleServicio hrEncuestaplantilladetalleServicio;

        public HrEncuestaplantilladetalleController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestaplantilladetalleServicio = servicioProveedor.GetService<HrEncuestaplantilladetalleServicio>();
        }

        [HttpGet("[action]")]
        public HrEncuestaplantilladetalle obtenerPorId(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta)
        {
            return hrEncuestaplantilladetalleServicio.obtenerPorId(pPlantilla, pPregunta);
        }

        [HttpPost("[action]")]
        public HrEncuestaplantilladetalle actualizar([FromBody]HrEncuestaplantilladetalle bean)
        {
            return hrEncuestaplantilladetalleServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrEncuestaplantilladetalle registrar([FromBody]HrEncuestaplantilladetalle bean)
        {
            return hrEncuestaplantilladetalleServicio.coreInsertar(_usuarioActual, bean);
        }
        
        [HttpPost("[action]")]
        public void eliminar(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta)
        {
            hrEncuestaplantilladetalleServicio.coreEliminar(pPlantilla, pPregunta);
        }
    }
}
