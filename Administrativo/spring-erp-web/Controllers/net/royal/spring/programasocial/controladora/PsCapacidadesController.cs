
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
    public class PsCapacidadController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsCapacidadServicio psCapacidadServicio;

        public PsCapacidadController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            psCapacidadServicio = servicioProveedor.GetService<PsCapacidadServicio>();
        }

        [HttpPost("[action]")]
        public PsCapacidad registrar([FromBody]DtoCapacidad bean) {
            return psCapacidadServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public DtoCapacidad calcularBarthel([FromBody]DtoCapacidad bean) {
            return psCapacidadServicio.calcularBarthel(bean);
        }

        [HttpPost("[action]")]
        public DtoCapacidad calcularKatz([FromBody]DtoCapacidad bean) {
            return psCapacidadServicio.calcularKatz(bean);
        }

        [HttpPost("[action]")]
        public PsCapacidad actualizar([FromBody]DtoCapacidad bean) {
            return psCapacidadServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsCapacidad anular(UsuarioActual usuarioActual, PsCapacidadPk pk) {
            return psCapacidadServicio.coreAnular(_usuarioActual, pk);
        }

        [HttpPost("[action]")]
        public DtoCapacidad calcularAniosRetraso([FromBody] DtoCapacidad bean) {
            return psCapacidadServicio.calcularAniosRetraso(bean);
        }


        [HttpGet("[action]")]
        public DtoCapacidad obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad) {

            PsCapacidadPk pk = new PsCapacidadPk();
            pk.IdInstitucion = pIdInstitucion;
            pk.IdCapacidad = pIdCapacidad;
            pk.IdBeneficiario = pIdBeneficiario;

            return psCapacidadServicio.obtenerPorId(pk);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroCapacidad filtro) {
            return psCapacidadServicio.listarCapacidades(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacionConsulta([FromBody] FiltroCapacidad filtro) {

            ParametroPaginacionGenerico lista = psCapacidadServicio.listarPaginacionConsulta(filtro.paginacion, filtro);
            setSessionData("filtro", filtro);
            return lista;
        }


        [HttpGet("[action]")]
        public JsonResult exportar(Boolean esNino) {
            FiltroCapacidad filtro = getSessionData<FiltroCapacidad>("filtro");
            filtro.esNino = esNino;

            return Json(new { url = psCapacidadServicio.Exportar(filtro) });
        }

    }
}
