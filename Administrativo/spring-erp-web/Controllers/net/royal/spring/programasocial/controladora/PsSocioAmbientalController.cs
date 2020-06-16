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

namespace net.royal.spring.programasocial.controladora {

    [Route("api/spring/programasocial/[controller]")]
    public class PsSocioAmbientalController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsSocioAmbientalServicio psSocioAmbientalServicio;

        public PsSocioAmbientalController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            psSocioAmbientalServicio = servicioProveedor.GetService<PsSocioAmbientalServicio>();
        }

        [HttpPost("[action]")]
        public PsSocioAmbiental registrar([FromBody]PsSocioAmbiental bean) {
            return psSocioAmbientalServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsSocioAmbiental actualizar([FromBody]PsSocioAmbiental bean) {
            return psSocioAmbientalServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsSocioAmbiental anular(UsuarioActual usuarioActual, PsSocioAmbientalPk pk) {
            return psSocioAmbientalServicio.coreAnular(_usuarioActual, pk);
        }


        [HttpGet("[action]")]
        public PsSocioAmbiental obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental) {

            PsSocioAmbientalPk pk = new PsSocioAmbientalPk();
            pk.IdBeneficiario = pIdBeneficiario;
            pk.IdInstitucion = pIdInstitucion;
            pk.IdSocioAmbiental = pIdSocioAmbiental;
            return psSocioAmbientalServicio.obtenerPorId(pk);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroSocioAmbiental filtro) {
            return psSocioAmbientalServicio.listarSocioAmbiental(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacionConsulta([FromBody] FiltroSocioAmbiental filtro) {
            ParametroPaginacionGenerico lista = psSocioAmbientalServicio.listarPaginacionConsulta(filtro.paginacion, filtro);
            setSessionData("filtro", filtro);
            return lista;
        }

        [HttpGet("[action]")]
        public JsonResult exportar(Boolean esNino) {
            FiltroSocioAmbiental filtro = getSessionData<FiltroSocioAmbiental>("filtro");
            filtro.esNino = esNino;
            return Json(new { url = psSocioAmbientalServicio.Exportar(filtro) });
        }

    }
}
