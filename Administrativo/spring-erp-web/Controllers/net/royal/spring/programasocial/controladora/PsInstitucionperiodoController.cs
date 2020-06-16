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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.controladora {

    [Route("api/spring/programasocial/[controller]")]
    public class PsInstitucionperiodoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsInstitucionperiodoServicio psInstitucionperiodoServicio;

        public PsInstitucionperiodoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            psInstitucionperiodoServicio = servicioProveedor.GetService<PsInstitucionperiodoServicio>();
        }



        [HttpGet("[action]")]
        public List<DtoInstitucionperiodo> listado(string pIdInstitucion,string pIdPeriodo) {
            return psInstitucionperiodoServicio.listado(pIdInstitucion, pIdPeriodo);

        }

        [HttpPost("[action]")]
        public List<DtoInstitucionperiodo> actualizar([FromBody]List<DtoInstitucionperiodo> list) {
            return psInstitucionperiodoServicio.coreActualizar(list);

        }

        [HttpPost("[action]")]
        public DtoInstitucionperiodo copiarPeriodo([FromBody] DtoInstitucionperiodo dtoInstitucionperiodo) {
            psInstitucionperiodoServicio.copiarPeriodo(dtoInstitucionperiodo);
            return dtoInstitucionperiodo;

        }

        [HttpGet("[action]")]
        public List<DtoTabla> periodoListarSimple(String idPeriodo) {
            return psInstitucionperiodoServicio.periodoListarSimple(idPeriodo);
        }


        [HttpGet("[action]")]
        public DtoTabla periodoActivo(string pIdInstitucion, String pIdPrograma, String pIdComponente) {
            DtoTabla periodo = new DtoTabla();
            periodo.Codigo = "2018S2";
            periodo.Nombre = "2018S2";
            return periodo;
        }

        [HttpGet("[action]")]
        public List<DtoTabla> listarPeriodoPorComponente(string pIdInstitucion, String pIdPrograma, String pIdComponente) {
            return psInstitucionperiodoServicio.listarPeriodoPorComponente(pIdInstitucion, pIdPrograma, pIdComponente);
        }
    }
}
