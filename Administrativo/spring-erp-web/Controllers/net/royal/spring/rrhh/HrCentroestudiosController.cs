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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.controladora
{

    [Route("api/spring/rrhh/[controller]")]
    public class HrCentroestudiosController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private HrCentroestudiosServicio hrCentroestudiosServicio;

        public HrCentroestudiosController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrCentroestudiosServicio = servicioProveedor.GetService<HrCentroestudiosServicio>();
        }

        [HttpPost("[action]")]
        public HrCentroestudios registrar([FromBody]HrCentroestudios bean)
        {
            return hrCentroestudiosServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrCentroestudios actualizar([FromBody]HrCentroestudios bean)
        {
            return hrCentroestudiosServicio.coreActualizar(_usuarioActual, bean);
        }


        [HttpPost("[action]")]
        public void eliminar(Nullable<Int32> pCentro)
        {
            hrCentroestudiosServicio.coreEliminar(pCentro);
        }

        [HttpGet("[action]")]
        public HrCentroestudios obtenerporid(Nullable<Int32> pCentro)
        {
            return hrCentroestudiosServicio.obtenerPorId(pCentro);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico solicitudListar([FromBody] FiltroPaginacionCentroEstudio filtro)
        {
            return hrCentroestudiosServicio.listarBusqueda(filtro.paginacion, filtro);
        }

    }
}
