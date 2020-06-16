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
public class PsEntidadController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsEntidadServicio psEntidadServicio;

        public PsEntidadController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psEntidadServicio = servicioProveedor.GetService<PsEntidadServicio>();
        }

        [HttpPost("[action]")]
        public PsEntidad registrar([FromBody]PsEntidad bean)
        {
            return psEntidadServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsEntidad actualizar([FromBody]PsEntidad bean)
        {
            return psEntidadServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsEntidad anular(UsuarioActual usuarioActual, Nullable<Int32> pIdEntidad)
        {
            return psEntidadServicio.coreAnular(_usuarioActual,  pIdEntidad);
        }

        [HttpPost("[action]")]
        public void eliminar(Nullable<Int32> pIdEntidad)
        {
            psEntidadServicio.coreEliminar( pIdEntidad);
        }

        [HttpGet("[action]")]
        public PsEntidad obtenerporid(Nullable<Int32> pIdEntidad)
        {
            return psEntidadServicio.obtenerPorId( pIdEntidad);
        }


       
    }
}
