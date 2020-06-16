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

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/programasocial/[controller]")]
public class PsMarcologicoresultadoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsMarcologicoresultadoServicio PsMarcologicoresultadoServicio;

        public PsMarcologicoresultadoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            PsMarcologicoresultadoServicio = servicioProveedor.GetService<PsMarcologicoresultadoServicio>();
        }

        [HttpPost("[action]")]
        public PsMarcologicoresultado registrar([FromBody]PsMarcologicoresultado bean)
        {
            return PsMarcologicoresultadoServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsMarcologicoresultado actualizar([FromBody]PsMarcologicoresultado bean)
        {
            return PsMarcologicoresultadoServicio.coreActualizar(_usuarioActual,bean);
        }


        [HttpPost("[action]")]
        public PsMarcologicoresultado eliminar([FromBody] PsMarcologicoresultado bean)
        {

            PsMarcologicoresultadoServicio.eliminar(bean.IdMarcologico);
            return bean;
        }

        [HttpPost("[action]")]
        public PsMarcologicoresultado obtenerPorId([FromBody] PsMarcologicoresultadoPk llave)
        {
            return PsMarcologicoresultadoServicio.obtenerPorId(llave);
        }



    }
}
