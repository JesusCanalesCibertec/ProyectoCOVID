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

namespace net.royal.spring.programasocial.controladora {

    [Route("api/spring/programasocial/[controller]")]
    public class PsInstitucionAreaController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsInstitucionAreaServicio PsInstitucionAreaServicio;

        public PsInstitucionAreaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            PsInstitucionAreaServicio = servicioProveedor.GetService<PsInstitucionAreaServicio>();
        }

        [HttpPost("[action]")]
        public PsInstitucionArea registrar([FromBody]PsInstitucionArea bean) {
            return PsInstitucionAreaServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsInstitucionArea actualizar([FromBody]PsInstitucionArea bean) {
            return PsInstitucionAreaServicio.coreActualizar(_usuarioActual, bean);
        }


        [HttpPost("[action]")]
        public PsInstitucionArea eliminar([FromBody] PsInstitucionArea bean) {

            PsInstitucionAreaServicio.eliminar(bean.IdInstitucion, bean.IdArea);
            return bean;
        }

        [HttpPost("[action]")]
        public PsInstitucionArea obtenerPorId([FromBody] PsInstitucionAreaPk llave) {
            return PsInstitucionAreaServicio.obtenerPorId(llave);
        }

        [HttpGet("[action]")]
        public List<PsInstitucionArea> listado(string pIdInstitucion) {
            return PsInstitucionAreaServicio.listado(pIdInstitucion);
        }

        [HttpGet("[action]")]
        public List<PsInstitucionArea> listadoPorPrograma(string pIdInstitucion, String idPrograma) {
            return PsInstitucionAreaServicio.listadoPorPrograma(pIdInstitucion, idPrograma);
        }

        [HttpGet("[action]")]
        public List<PsInstitucionArea> listarTodo()
        {
            return PsInstitucionAreaServicio.listarTodo();
        }

        [HttpGet("[action]")]
        public PsInstitucionArea anular(String pIdInstitucion, String pIdArea)
        {
            return PsInstitucionAreaServicio.coreAnular(_usuarioActual, pIdInstitucion, pIdArea);
        }

    }
}
