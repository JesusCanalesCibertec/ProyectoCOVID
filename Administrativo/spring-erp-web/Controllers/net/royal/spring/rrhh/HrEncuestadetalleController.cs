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
    public class HrEncuestadetalleController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrEncuestadetalleServicio hrEncuestadetalleServicio;

        public HrEncuestadetalleController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestadetalleServicio = servicioProveedor.GetService<HrEncuestadetalleServicio>();
        }

        [HttpGet("[action]")]
        public HrEncuestadetalle obtenerPorId(Nullable<Int32> pSecuencia, Nullable<Int32> pPregunta)
        {
            return hrEncuestadetalleServicio.obtenerPorId(pSecuencia, pPregunta);
        }

        [HttpPost("[action]")]
        public HrEncuestadetalle actualizar([FromBody]HrEncuestadetalle bean)
        {
            return hrEncuestadetalleServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrEncuestadetalle registrar([FromBody]HrEncuestadetalle bean)
        {
            return hrEncuestadetalleServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public void eliminar(Nullable<Int32> pSecuencia, Nullable<Int32> pPregunta)
        {
            hrEncuestadetalleServicio.coreEliminar(pSecuencia, pPregunta);
        }
    }
}
