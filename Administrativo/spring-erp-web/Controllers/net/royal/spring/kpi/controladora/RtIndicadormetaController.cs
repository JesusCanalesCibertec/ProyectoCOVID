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
using net.royal.spring.kpi.servicio;
using net.royal.spring.kpi.dominio;

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/kpi/[controller]")]
public class RtIndicadorMetaController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private RtIndicadorMetaServicio RtIndicadorMetaServicio;

        public RtIndicadorMetaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            RtIndicadorMetaServicio = servicioProveedor.GetService<RtIndicadorMetaServicio>();
        }

        [HttpPost("[action]")]
        public RtIndicadorMeta registrar([FromBody]RtIndicadorMeta bean)
        {
            return RtIndicadorMetaServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public RtIndicadorMeta actualizar([FromBody]RtIndicadorMeta bean)
        {
            return RtIndicadorMetaServicio.coreActualizar(_usuarioActual,bean);
        }


        [HttpPost("[action]")]
        public RtIndicadorMeta eliminar([FromBody] RtIndicadorMeta bean)
        {

            RtIndicadorMetaServicio.coreEliminar(bean.IdIndicador,bean.IdMeta);
            return bean;
        }

        [HttpGet("[action]")]
        public RtIndicadorMeta anular(String pIdIndicador, Nullable<Int32> pIdMeta)
        {
            return RtIndicadorMetaServicio.coreAnular(_usuarioActual, pIdIndicador, pIdMeta);
        }

        [HttpPost("[action]")]
        public RtIndicadorMeta obtenerPorId([FromBody] RtIndicadorMetaPk llave)
        {
            return RtIndicadorMetaServicio.obtenerPorId(llave);
        }

        [HttpGet("[action]")]
        public List<RtIndicadorMeta> listado(string pIdIndicador)
        {
            return RtIndicadorMetaServicio.listado(pIdIndicador);

        }



    }
}
