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

namespace net.royal.spring.programasocial.controladora
{

    [Route("api/spring/programasocial/[controller]")]
    public class PsInstitucionController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private PsInstitucionServicio psInstitucionServicio;
        private PsProgramaServicio psProgramaServicio;

        public PsInstitucionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psInstitucionServicio = servicioProveedor.GetService<PsInstitucionServicio>();
            psProgramaServicio = servicioProveedor.GetService<PsProgramaServicio>();
        }

        [HttpPost("[action]")]
        public PsInstitucion Registrar([FromBody]PsInstitucion bean)
        {
            return psInstitucionServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsInstitucion Actualizar([FromBody]PsInstitucion bean)
        {
            return psInstitucionServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public PsInstitucion Anular(string pIdInstitucion)
        {
            return psInstitucionServicio.coreAnular(_usuarioActual, pIdInstitucion);
        }



        [HttpPost("[action]")]
        public DtoInstitucion Eliminar([FromBody] DtoInstitucion bean)
        {

            psInstitucionServicio.eliminar(bean.codigo);
            return bean;
        }

        [HttpGet("[action]")]
        public PsInstitucion ObtenerPorId(string codigo)
        {
            return psInstitucionServicio.obtenerPorId(codigo);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] FiltroInstitucion filtro)
        {
            return psInstitucionServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpGet("[action]")]
        public List<DtoTabla> listarPeriodos(String Institucion, String IdPrograma, String IdComponente, String IdUsuario, Nullable<Int32> IdBeneficiario)
        {
            return psInstitucionServicio.listarPeriodos(Institucion, IdPrograma, IdComponente, IdUsuario, IdBeneficiario);
        }

        [HttpGet("[action]")]
        public List<PsInstitucion> listarTodos()
        {
            return psInstitucionServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<PsInstitucion> listarTodosActivas()
        {
            return psInstitucionServicio.listarTodosActivas();
        }


        [HttpGet("[action]")]
        public DtoTabla flgSeleccionarInstitucion()
        {
            DtoTabla retorno = new DtoTabla();
            retorno.valorFlag = psInstitucionServicio.flgSeleccionarInstitucion(_usuarioActual);
            return retorno;
        }

        [HttpGet("[action]")]
        public List<PsInstitucion> listarPorPrograma(String pIdPrograma)
        {
            pIdPrograma = pIdPrograma == "NCD" ? "NNA" : pIdPrograma;
            return psProgramaServicio.listarPorPrograma(pIdPrograma);
        }
    }
}
