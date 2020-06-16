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
    public class PsSaludController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsSaludServicio psSaludServicio;

        public PsSaludController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            psSaludServicio = servicioProveedor.GetService<PsSaludServicio>();
        }

        [HttpPost("[action]")]
        public PsSalud registrar([FromBody]DtoPsSalud bean) {
            return psSaludServicio.coreInsertar(_usuarioActual, bean);
        }


        [HttpPost("[action]")]
        public DtoTabla calcularHemoglobina([FromBody]DtoPsSalud bean) {
            return psSaludServicio.calcularHemoglobina(bean);
        }



        [HttpPost("[action]")]
        public PsSalud actualizar([FromBody]DtoPsSalud bean) {
            return psSaludServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsSalud anular(UsuarioActual usuarioActual, PsSaludPk pk) {
            return psSaludServicio.coreAnular(_usuarioActual, pk);
        }



        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroSalud filtro) {
            return psSaludServicio.listarSaludPaginacion(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacionConsulta([FromBody] FiltroSalud filtro) {
            ParametroPaginacionGenerico lista = psSaludServicio.listarPaginacionConsulta(filtro.paginacion, filtro);
            setSessionData("filtro", filtro);
            return lista;
        }


        [HttpGet("[action]")]
        public JsonResult exportar(Boolean esNino) {
            FiltroSalud filtro = getSessionData<FiltroSalud>("filtro");
            filtro.esNino = esNino;

            return Json(new { url = psSaludServicio.Exportar(filtro) });
        }


        [HttpGet("[action]")]
        public DtoPsSalud obtenerPorListados(String IdInstitucion, int? IdBeneficiario, int? IdSalud) {

            return psSaludServicio.obtenerPorListados(IdInstitucion, IdBeneficiario, IdSalud);
        }

        [HttpGet("[action]")]
        public List<DtoPsSalud> listarDiagnosticos(String IdInstitucion, int? IdBeneficiario, String IdTipoRas) {

            return psSaludServicio.listarDiagnosticos(IdInstitucion, IdBeneficiario, IdTipoRas);
        }

        [HttpGet("[action]")]
        public List<DtoPsSalud> listarTratamientos(String IdInstitucion, int? IdBeneficiario, int? IdDetalle, int? IdDiagnostico) {

            return psSaludServicio.listarTratamientos(IdInstitucion, IdBeneficiario, IdDetalle, IdDiagnostico);
        }

        [HttpGet("[action]")]
        public DtoPsSalud obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud) {
            return psSaludServicio.obtenerPorId(pIdInstitucion, pIdBeneficiario, pIdSalud);
        }

    }
}
