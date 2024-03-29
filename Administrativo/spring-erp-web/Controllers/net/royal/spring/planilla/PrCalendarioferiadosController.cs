﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.planilla.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.planilla.dominio;

namespace net.royal.spring.planilla
{
    [Route("api/spring/planilla/[controller]")]
    public class PrCalendarioferiadosController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private PrCalendarioferiadosServicio prCalendarioferiadosServicio;

        public PrCalendarioferiadosController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            prCalendarioferiadosServicio = servicioProveedor.GetService<PrCalendarioferiadosServicio>();
        }

        [HttpGet("[action]")]
        public IList<PrCalendarioferiados> listarActivos()
        {
            return prCalendarioferiadosServicio.listarActivos();
        }
    }
}
