using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;

namespace net.royal.spring.rrhh.controladora
{

    [Route("api/spring/rrhh/[controller]")]
    public class HrEncuestasujetoController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private HrEncuestasujetoServicio hrEncuestasujetoServicio;

        public HrEncuestasujetoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestasujetoServicio = servicioProveedor.GetService<HrEncuestasujetoServicio>();
        }


        [HttpPost("[action]")]
        public HrEncuestasujeto actualizar([FromBody]HrEncuestasujeto bean)
        {
            return hrEncuestasujetoServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrEncuestasujeto registrar([FromBody]HrEncuestasujeto bean)
        {
            return hrEncuestasujetoServicio.coreInsertar(_usuarioActual, bean);
        }

    }
}
