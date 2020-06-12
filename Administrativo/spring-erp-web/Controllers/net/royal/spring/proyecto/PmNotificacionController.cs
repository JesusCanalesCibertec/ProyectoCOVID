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
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.filtro;

namespace net.royal.spring.proyecto.controladora
{

    [Route("api/spring/proyecto/[controller]")]
    public class PmNotificacionController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private PmNotificacionServicio pmNotificacionServicio;

        public PmNotificacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            pmNotificacionServicio = servicioProveedor.GetService<PmNotificacionServicio>();
        }

        //[HttpGet("[action]")]
        //public List<PmNotificacion> listarNotificacionesPorPersona()
        //{
        //    return pmNotificacionServicio.listarNotificacionesPorPersona(_usuarioActual);
          
        //}

    }
}
