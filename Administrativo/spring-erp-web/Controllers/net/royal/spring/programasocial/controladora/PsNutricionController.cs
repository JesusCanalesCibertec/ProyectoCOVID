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
using net.royal.spring.rrhh.dominio.dtoscomponente;

namespace net.royal.spring.programasocial.controladora {

    [Route("api/spring/psnutricion/[controller]")]
    public class PsNutricionController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsNutricionServicio psNutricionServicio;

        public PsNutricionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            psNutricionServicio = servicioProveedor.GetService<PsNutricionServicio>();
        }

        [HttpPost("[action]")]
        public PsNutricion registrar([FromBody]PsNutricion bean) {
            return psNutricionServicio.insertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarNutricion([FromBody] FiltroNutricion filtro) {
            return psNutricionServicio.listarNutricion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico consultarNutricion([FromBody] FiltroNutricion filtro) {

            ParametroPaginacionGenerico lista = psNutricionServicio.consultarNutricion(filtro.paginacion, filtro);
            setSessionData("filtro", filtro);
            return lista;

        }

        [HttpPost("[action]")]
        public List<PsNutricion> consultar([FromBody] FiltroNutricion filtro) {
            return psNutricionServicio.consultar(filtro);
        }

        [HttpPost("[action]")]
        public PsNutricion actualizar([FromBody]PsNutricion bean) {
            return psNutricionServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public DtoNutricion obtenerCalculos([FromBody]PsNutricion bean) {

            DtoNutricion obj = new DtoNutricion();
            obj.IdBeneficiario = bean.IdBeneficiario;
            obj.Peso = bean.Peso;
            obj.Talla = bean.Talla;
            obj.Hemoglobina = null;
            obj.IdProceso = bean.IdNutricion;
            obj.IdTipoProceso = "NUTR";
            return psNutricionServicio.obtenerCalculos(obj);
        }

        [HttpPost("[action]")]
        public PsNutricion anular([FromBody]PsNutricion bean) {
            UsuarioActual usuarioActual = _usuarioActual;
            return psNutricionServicio.anular(_usuarioActual, bean.IdInstitucion, bean.IdBeneficiario, bean.IdNutricion);
        }

        [HttpGet("[action]")]
        public PsNutricion obtenerporid(String idInstitucion, Nullable<Int32> IdBeneficiario, Nullable<Int32> IdNutricion) {
            return psNutricionServicio.obtenerPorId(idInstitucion, IdBeneficiario, IdNutricion);
        }


        [HttpGet("[action]")]
        public JsonResult exportar(Boolean esNino) {
            FiltroNutricion filtro = getSessionData<FiltroNutricion>("filtro");
            filtro.esNino = esNino;
            return Json(new { url = psNutricionServicio.Exportar(filtro) });
        }
    }
}
