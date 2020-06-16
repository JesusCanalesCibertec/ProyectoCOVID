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

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/programasocial/[controller]")]
public class PsConsumoPlantillaController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsConsumoPlantillaServicio psConsumoPlantillaServicio;

        public PsConsumoPlantillaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psConsumoPlantillaServicio = servicioProveedor.GetService<PsConsumoPlantillaServicio>();
        }

        [HttpPost("[action]")]
        public PsConsumoPlantilla registrar([FromBody]PsConsumoPlantilla bean)
        {
            return psConsumoPlantillaServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsConsumoPlantilla actualizar([FromBody]PsConsumoPlantilla bean)
        {
            return psConsumoPlantillaServicio.coreActualizar(_usuarioActual, bean);
        }


        [HttpPost("[action]")]
        public DtoTabla eliminar([FromBody] DtoTabla bean)
        {
            psConsumoPlantillaServicio.coreEliminar(bean.Codigo, bean.CodigoNumerico);
            return bean;
        }

        [HttpGet("[action]")]
        public PsConsumoPlantilla obtenerporid(string pIdInstitucion, int pPlantilla)
        {
            return psConsumoPlantillaServicio.obtenerPorId(pIdInstitucion, pPlantilla);
        }



        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPlantilla([FromBody] DtoTabla filtro)
        {
            return psConsumoPlantillaServicio.listarPlantilla(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] DtoTabla filtro)
        {
            return psConsumoPlantillaServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpGet("[action]")]
        public List<PsConsumoPlantilla> listarPlantillas(string institucion)
        {
            return psConsumoPlantillaServicio.listarPlantillas(institucion);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPlantillasConsumo([FromBody]DtoTabla filtro)
        {
            return psConsumoPlantillaServicio.listarPlantillasConsumo(filtro, filtro.paginacion);
        }


        [HttpGet("[action]")]
        public PsConsumoPlantilla anular(String pIdInstitucion, Nullable<Int32> pPlantilla)
        {
            return psConsumoPlantillaServicio.coreAnular(_usuarioActual, pIdInstitucion, pPlantilla);
        }



    }
}
