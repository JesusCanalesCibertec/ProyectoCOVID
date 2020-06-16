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
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.proyecto.dominio;

namespace net.royal.spring.rrhh.controladora
{

    [Route("api/spring/proyecto/[controller]")]
    public class PmPlantillaController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PmPlantillaServicio hrEncuestaplantillaServicio;

        public PmPlantillaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        { 
            servicioProveedor = _servicioProveedor;
            hrEncuestaplantillaServicio = servicioProveedor.GetService<PmPlantillaServicio>();
        }

        [HttpGet("[action]")]
        public PmPlantilla obtenerPorId(int pPlantilla)
        {
            return hrEncuestaplantillaServicio.obtenerPorId(pPlantilla);
        }

        [HttpPost("[action]")]
        public PmPlantilla actualizar([FromBody]PmPlantilla bean)
        {
            return hrEncuestaplantillaServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PmPlantilla registrar([FromBody]PmPlantilla bean)
        {
            return hrEncuestaplantillaServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public PmPlantilla anular(Nullable<Int32> pPlantilla)
        {
           return hrEncuestaplantillaServicio.coreAnular(_usuarioActual,pPlantilla);
        }

        [HttpPost("[action]")]
        public DtoTabla eliminar([FromBody] DtoTabla bean)
        {
            hrEncuestaplantillaServicio.coreEliminar(bean.CodigoNumerico);
            return bean;
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] DtoTabla filtro)
        {
            return hrEncuestaplantillaServicio.listarPaginacion(filtro.paginacion, filtro);

        }

       

       


    }
}
