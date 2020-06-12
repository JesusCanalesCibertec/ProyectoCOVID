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
using net.royal.spring.salud.dominio;
using net.royal.spring.salud.servicio;

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/salud/[controller]")]
public class SsCie10capituloController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private SsCie10capituloServicio SsCie10capituloServicio;

        public SsCie10capituloController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            SsCie10capituloServicio = servicioProveedor.GetService<SsCie10capituloServicio>();
        }


        [HttpGet("[action]")]
        public List<SsCie10capitulo> listarTodos()
        {
            return SsCie10capituloServicio.listarTodos();
        }


    }
}
