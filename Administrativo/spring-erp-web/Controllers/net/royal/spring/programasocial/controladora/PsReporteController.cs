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
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.constante;

namespace net.royal.spring.programasocial.controladora {

    [Route("api/spring/programasocial/[controller]")]
    public class PsReporteController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsMarcologicoServicio PsMarcologicoServicio;
        private SyPreferencesDao syPreferencesDao;

        public PsReporteController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            PsMarcologicoServicio = servicioProveedor.GetService<PsMarcologicoServicio>();
            syPreferencesDao = servicioProveedor.GetService<SyPreferencesDao>();
        }

        [HttpPost("[action]")]
        public List<DtoReportesGraficos> ListarReportePoblacionPorInstitucionyAnio([FromBody]FiltroGraficos filtro) {
            List<DtoReportesGraficos> ListaNutricionEstandares = PsMarcologicoServicio.ListarReportePoblacionPorInstitucionyAnio(filtro);
            setSessionData("ListarReportePoblacionPorInstitucionyAnio", ListaNutricionEstandares);
            return ListaNutricionEstandares;
        }
        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarReportePoblacionPorInstitucionyAnio() {
            List<DtoReportesGraficos> ListarReportePoblacionPorInstitucionyAnio = getSessionData<List<DtoReportesGraficos>>("ListarReportePoblacionPorInstitucionyAnio");
            if (ListarReportePoblacionPorInstitucionyAnio != null) {
                return PsMarcologicoServicio.MostarReportePoblacionPorInstitucionyAnio(ListarReportePoblacionPorInstitucionyAnio);
            }
            else {
                return null;


            }
        }


        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarReportePoblacionBeneficiaria() {
            List<DtoReportesGraficos> ListarReporteSubvenciones = getSessionData<List<DtoReportesGraficos>>("ListarReportePoblacionPorInstitucionyAnio");
            if (ListarReporteSubvenciones != null) {
                return PsMarcologicoServicio.MostarReportePoblacionBeneficiaria(ListarReporteSubvenciones);
            }
            else {
                return null;
            }


        }


        [HttpPost("[action]")]
        public List<DtoReportesGraficos> ListarReporteSubvencionesPorPrograma([FromBody]FiltroGraficos filtro) {
            List<DtoReportesGraficos> ListaNutricionEstandares = PsMarcologicoServicio.ListarReporteSubvencionesPorPrograma(filtro);
            setSessionData("ListarReporteSubvencionesPorPrograma", ListaNutricionEstandares);
            return ListaNutricionEstandares;
        }
        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarListarReporteSubvencionesPorPrograma() {
            List<DtoReportesGraficos> ListarReporteSubvenciones = getSessionData<List<DtoReportesGraficos>>("ListarReporteSubvencionesPorPrograma");
            if (ListarReporteSubvenciones != null) {
                return PsMarcologicoServicio.MostarListarReporteSubvencionesPorPrograma(ListarReporteSubvenciones);
            }
            else {
                return null;
            }
        }

        [HttpPost("[action]")]
        public List<DtoReportesGraficos> ListarReporteSubvencionesPorInstitucion([FromBody]FiltroGraficos filtro) {
            List<DtoReportesGraficos> ListaNutricionEstandares = PsMarcologicoServicio.ListarReporteSubvencionesPorInstitucion(filtro);
            setSessionData("ListarReporteSubvencionesPorInstitucion", ListaNutricionEstandares);
            return ListaNutricionEstandares;
        }
        [HttpGet("[action]")]
        public List<DtoGraficoMultiple> MostarReporteSubvencionesPorInstitucion() {
            List<DtoReportesGraficos> ListarReporteSubvenciones = getSessionData<List<DtoReportesGraficos>>("ListarReporteSubvencionesPorInstitucion");
            if (ListarReporteSubvenciones != null) {
                return PsMarcologicoServicio.MostarReporteSubvencionesPorInstitucion(ListarReporteSubvenciones);
            }
            else {
                return null;
            }

        }



    }
}
