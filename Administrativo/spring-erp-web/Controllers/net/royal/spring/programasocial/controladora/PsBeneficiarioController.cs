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
using Microsoft.AspNetCore.Hosting;

namespace net.royal.spring.programasocial.controladora
{

    [Route("api/spring/programasocial/[controller]")]
    public class PsBeneficiarioController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private PsBeneficiarioServicio psBeneficiarioServicio;

        public PsBeneficiarioController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psBeneficiarioServicio = servicioProveedor.GetService<PsBeneficiarioServicio>();
            
        }

        [HttpPost("[action]")]
        public PsBeneficiario registrar([FromBody]PsBeneficiario bean)
        {
            return psBeneficiarioServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsBeneficiario actualizar([FromBody]PsBeneficiario bean)
        {
            return psBeneficiarioServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public List<MaMiscelaneosdetalle> listarValoracionNutricional(Int32 IdBeneficiario, String IdInstitucion)
        {
            return psBeneficiarioServicio.listarValoracionNutricional(IdBeneficiario, IdInstitucion);
        }

        [HttpPost("[action]")]
        public PsBeneficiario anular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            return psBeneficiarioServicio.coreAnular(_usuarioActual, pIdInstitucion, pIdBeneficiario);
        }

        [HttpPost("[action]")]
        public void eliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            psBeneficiarioServicio.coreEliminar(pIdInstitucion, pIdBeneficiario);
        }

        [HttpGet("[action]")]
        public PsBeneficiario obtenerporid(String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            return psBeneficiarioServicio.obtenerPorId(pIdInstitucion, pIdBeneficiario);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroBeneficiario filtro)
        {

            setSessionData("filtroPaginacionBeneficiario", filtro);

            return psBeneficiarioServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarBeneficiarios([FromBody] FiltroBeneficiario filtro)
        {
            
            return psBeneficiarioServicio.listarBeneficiarios(filtro.paginacion, filtro);

        }

        [HttpGet("[action]")]
        public JsonResult exportar()
        {
            ParametroPaginacionGenerico paginacion = new ParametroPaginacionGenerico();
            paginacion.CantidadRegistrosDevolver = 10000;
            paginacion.RegistroInicio = 0;
            FiltroBeneficiario filtroBeneficiario = getSessionData<FiltroBeneficiario>("filtroPaginacionBeneficiario");
            filtroBeneficiario.paginacion = paginacion;
            return Json(new { url = psBeneficiarioServicio.Exportar(filtroBeneficiario) });
        }


        [HttpPost("[action]")]
        public PsEntidad registrarCompleto([FromBody]PsEntidad bean)
        {
            return psBeneficiarioServicio.registrarCompleto(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsEntidad actualizarCompleto([FromBody]PsEntidad bean)
        {
            return psBeneficiarioServicio.actualizarCompleto(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsEntidad obtenerCompleto([FromBody]PsBeneficiarioPk bean)
        {
            return psBeneficiarioServicio.obtenerCompleto(bean.IdInstitucion, bean.IdBeneficiario.Value);
        }

        [HttpPost("[action]")]
        public PsEntidad obtenerDatosBasicos([FromBody]PsEntidad bean)
        {
            return psBeneficiarioServicio.obtenerDatosBasicos(bean);
        }

        [HttpPost("[action]")]
        public DtoBeneficiario anularBeneficiario([FromBody]DtoBeneficiario bean)
        {
            return psBeneficiarioServicio.anularBeneficiario(bean, _usuarioActual);
        }


        [HttpPost("[action]")]
        public List<DtoPrevencionSalud> ListarBeneficiariosSinEvaluacion([FromBody]FiltroGraficos filtro)
        {
            return psBeneficiarioServicio.ListarBeneficiariosSinEvaluacion(filtro);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico paginacionBeneficiariosSinEvaluacion([FromBody] FiltroGraficos filtro)
        {

            return psBeneficiarioServicio.paginacionBeneficiariosSinEvaluacion(filtro.paginacion, filtro);

        }



    }
}
