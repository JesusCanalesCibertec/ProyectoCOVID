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
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.programasocial.dominio.dtos;

namespace net.royal.spring.programasocial.controladora {

    [Route("api/spring/programasocial/[controller]")]
    public class PsAntencionController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsAtencionServicio psAtencionServicio;

        public PsAntencionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            psAtencionServicio = servicioProveedor.GetService<PsAtencionServicio>();
        }

        [HttpPost("[action]")]
        public PsAtencion registrar([FromBody]DtoAtencion bean) {
            return psAtencionServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsAtencion actualizar([FromBody]DtoAtencion bean) {
            return psAtencionServicio.coreActualizar(_usuarioActual, bean);
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroRas filtro) {
            return psAtencionServicio.listarPaginacion(filtro.paginacion, filtro);

        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacionConsulta([FromBody] FiltroRas filtro) {
            ParametroPaginacionGenerico lista = psAtencionServicio.listarPaginacionConsulta(filtro.paginacion, filtro);
            setSessionData("filtro", filtro);
            return lista;

        }


        [HttpGet("[action]")]
        public JsonResult exportar() {
            FiltroRas filtro = getSessionData<FiltroRas>("filtro");

            return Json(new { url = psAtencionServicio.Exportar(filtro) });
        }

    }
}
