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
public class SsCie10subgrupoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private SsCie10subgrupoServicio SsCie10subgrupoServicio;

        public SsCie10subgrupoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            SsCie10subgrupoServicio = servicioProveedor.GetService<SsCie10subgrupoServicio>();
        }


        [HttpGet("[action]")]
        public List<SsCie10subgrupo> listarPorGrupo(string codigo)
        {
            return SsCie10subgrupoServicio.listarPorGrupo(codigo);
        }


    }
}
