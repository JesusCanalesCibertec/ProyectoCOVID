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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.controladora {
    [Route("api/spring/rrhh/[controller]")]
    public class HrEncuestaController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrEncuestaServicio hrEncuestaServicio;

        public HrEncuestaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            hrEncuestaServicio = servicioProveedor.GetService<HrEncuestaServicio>();
        }

        [HttpGet("[action]")]
        public HrEncuesta obtenerPorId(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia) {
            return hrEncuestaServicio.obtenerPorId(pCompanyowner, pPeriodo, pSecuencia);
        }

        [HttpPost("[action]")]
        public HrEncuesta actualizar([FromBody]HrEncuesta bean) {
            return hrEncuestaServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrEncuesta registrar([FromBody]HrEncuesta bean) {
            return hrEncuestaServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia) {
            hrEncuestaServicio.coreEliminar(pCompanyowner, pPeriodo, pSecuencia);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listar([FromBody] FiltroPaginacionEncuesta filtroPaginacion) {
            filtroPaginacion.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;
            return hrEncuestaServicio.listar(filtroPaginacion);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico consulta([FromBody] FiltroPaginacionEncuesta filtroPaginacion) {
            return hrEncuestaServicio.consulta(filtroPaginacion);
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarSujetos([FromBody] FiltroPaginacionSujeto filtroPaginacion) {
            filtroPaginacion.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;
            return hrEncuestaServicio.listarSujetos(filtroPaginacion);
        }

        [HttpGet("[action]")]
        public DtoEncuesta obtenerPlantilla(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia) {
            return hrEncuestaServicio.obtenerPlantilla(new HrEncuestaPk() { Companyowner = pCompanyowner, Periodo = pPeriodo, Secuencia = pSecuencia });
        }

        [HttpGet("[action]")]
        public DtoEncuesta obtenerEncuesta(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia, Nullable<Int32> pSujeto) {
            return hrEncuestaServicio.obtenerEncuesta(new HrEncuestaPk() { Companyowner = pCompanyowner, Periodo = pPeriodo, Secuencia = pSecuencia }, pSujeto);
        }

        [HttpPost("[action]")]
        public DtoEncuesta solicitudRegistrar([FromBody] DtoEncuesta dtoEncuesta) {
            return hrEncuestaServicio.solicitudRegistrar(dtoEncuesta, _usuarioActual);
        }

        [HttpPost("[action]")]
        public DtoEncuesta solicitudActualizar([FromBody] DtoEncuesta dtoEncuesta) {
            return hrEncuestaServicio.solicitudActualizar(dtoEncuesta, _usuarioActual);
        }

        [HttpPost("[action]")]
        public HrEncuesta obtenerCompleto([FromBody] HrEncuestaPk bean) {
            return hrEncuestaServicio.obtenerCompleto(bean);

        }

        [HttpPost("[action]")]
        public HrEncuesta solicitudRegistrarBean([FromBody] HrEncuesta bean) {
            bean.Companyowner = _usuarioActual.CompaniaSocioCodigo;
            return hrEncuestaServicio.solicitudRegistrarBean(bean, _usuarioActual);
        }

        [HttpPost("[action]")]
        public HrEncuesta solicitudActualizarBean([FromBody] HrEncuesta bean) {
            return hrEncuestaServicio.solicitudActualizarBean(bean, _usuarioActual);
        }


        [HttpPost("[action]")]
        public DtoEncuesta actualizarEjecucion([FromBody] DtoEncuesta dtoEncuesta) {
            return hrEncuestaServicio.actualizarEjecucion(dtoEncuesta, _usuarioActual);
        }

        [HttpPost("[action]")]
        public DtoEncuesta anularEncuesta([FromBody] DtoEncuesta dtoEncuesta) {
            return hrEncuestaServicio.anularEncuesta(dtoEncuesta, _usuarioActual);
        }

        [HttpPost("[action]")]
        public DtoEncuesta anularSujeto([FromBody] DtoEncuestaSujeto dto) {
            return hrEncuestaServicio.anularSujeto(dto);
        }


        [HttpPost("[action]")]
        public List<DtoEncuestaAnalisis> analizarEncuesta([FromBody] FiltroEncuestaAnalisis filtro) {
            return hrEncuestaServicio.analizarEncuesta(filtro);
        }

        [HttpPost("[action]")]
        public JsonResult exportarEncuesta([FromBody] FiltroEncuestaAnalisis filtro) {
            return Json(new { url = hrEncuestaServicio.exportarEncuesta(filtro) });
        }

        [HttpPost("[action]")]
        public DtoEncuesta copiar([FromBody] HrEncuestaPk pk) {
            return hrEncuestaServicio.copiar(pk);
        }


    }
}
