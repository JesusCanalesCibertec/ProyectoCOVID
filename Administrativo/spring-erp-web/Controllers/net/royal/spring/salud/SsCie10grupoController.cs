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
public class SsCie10grupoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private SsCie10grupoServicio SsCie10grupoServicio;

        public SsCie10grupoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            SsCie10grupoServicio = servicioProveedor.GetService<SsCie10grupoServicio>();
        }


        [HttpGet("[action]")]
        public List<SsCie10grupo> listarPorCapitulo(string codigo)
        {
            return SsCie10grupoServicio.listarPorCapitulo(codigo);
        }


    }
}
