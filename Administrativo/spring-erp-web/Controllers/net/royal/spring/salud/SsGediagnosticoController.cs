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
using net.royal.spring.salud.dominio;
using net.royal.spring.salud.dominio.dto;
using net.royal.spring.salud.servicio;

namespace net.royal.spring.salud.controladora {

    [Route("api/spring/salud/[controller]")]
    public class SsGediagnosticoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private SsGediagnosticoServicio SsGediagnosticoServicio;
        private SsCie10capituloServicio ssCie10capituloServicio;

        public SsGediagnosticoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            SsGediagnosticoServicio = servicioProveedor.GetService<SsGediagnosticoServicio>();
            ssCie10capituloServicio = servicioProveedor.GetService<SsCie10capituloServicio>();
        }

        [HttpPost("[action]")]
        public SsGediagnostico registrar([FromBody]SsGediagnostico bean) {
            return SsGediagnosticoServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public SsGediagnostico actualizar([FromBody]SsGediagnostico bean) {
            return SsGediagnosticoServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public DtoDiagnostico eliminar([FromBody] DtoDiagnostico bean) {
            SsGediagnosticoServicio.eliminar(bean.idDiagnostico);
            return bean;
        }

        [HttpGet("[action]")]
        public SsGediagnostico obtenerporId(int codigo) {
            return SsGediagnosticoServicio.obtenerPorId(codigo);
        }

        [HttpGet("[action]")]
        public SsGediagnostico obtenerNombrePorCodigo(string codigo) {
            return SsGediagnosticoServicio.obtenerNombrePorCodigo(codigo);
        }

        [HttpGet("[action]")]
        public List<SsGediagnostico> listarActivosFlgCronicos() {
            return SsGediagnosticoServicio.listarActivosFlgCronicos();
        }

       [HttpGet("[action]")]
        public List<SsCie10capitulo> listarTodosActivos() {
            return ssCie10capituloServicio.listarTodosActivos();
        }

       
        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroDiagnostico filtro) {
            return SsGediagnosticoServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpGet("[action]")]
        public SsGediagnostico anular(Nullable<Int32> pIdDiagnostico)
        {
            return SsGediagnosticoServicio.coreAnular(_usuarioActual, pIdDiagnostico);
        }
    }
}
