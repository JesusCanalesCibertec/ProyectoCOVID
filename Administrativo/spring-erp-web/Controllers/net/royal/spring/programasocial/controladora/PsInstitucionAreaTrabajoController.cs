using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/programasocial/[controller]")]
public class PsInstitucionAreaTrabajoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsInstitucionAreaTrabajoServicio psInstitucionAreaTrabajoServicio;

        public PsInstitucionAreaTrabajoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psInstitucionAreaTrabajoServicio = servicioProveedor.GetService<PsInstitucionAreaTrabajoServicio>();
        }

        [HttpPost("[action]")]
        public PsInstitucionAreaTrabajo registrar([FromBody]PsInstitucionAreaTrabajo bean)
        {
            return psInstitucionAreaTrabajoServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsInstitucionAreaTrabajo actualizar([FromBody]PsInstitucionAreaTrabajo bean)
        {
            return psInstitucionAreaTrabajoServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public void eliminar()
        {
            psInstitucionAreaTrabajoServicio.coreEliminar();
        }

        [HttpGet("[action]")]
        public PsInstitucionAreaTrabajo obtenerporid()
        {
            return psInstitucionAreaTrabajoServicio.obtenerPorId();
        }

        [HttpGet("[action]")]
        public List<PsInstitucionAreaTrabajo> listarActivos() {
            return psInstitucionAreaTrabajoServicio.listarActivos();
        }

    }
}
