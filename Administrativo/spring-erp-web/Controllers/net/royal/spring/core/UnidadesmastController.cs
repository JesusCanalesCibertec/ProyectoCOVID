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
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.core.dominio;
using net.royal.spring.core.servicio;

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/core/[controller]")]
public class UnidadesmastController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private UnidadesmastServicio UnidadesmastServicio;

        public UnidadesmastController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            UnidadesmastServicio = servicioProveedor.GetService<UnidadesmastServicio>();
        }

        [HttpPost("[action]")]
        public Unidadesmast registrar([FromBody]Unidadesmast bean)
        {
            return UnidadesmastServicio.coreInsertar(_usuarioActual,bean);
        }

       

        [HttpGet("[action]")]
        public List<Unidadesmast> listarTodos()
        {
            return UnidadesmastServicio.listarTodos();
        }

    }
}
