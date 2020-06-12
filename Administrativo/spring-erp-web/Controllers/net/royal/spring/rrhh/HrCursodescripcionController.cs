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
    public class HrCursodescripcionController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private HrCursodescripcionServicio hrCursodescripcionServicio;

        public HrCursodescripcionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrCursodescripcionServicio = servicioProveedor.GetService<HrCursodescripcionServicio>();
        }

        [HttpPost("[action]")]
        public HrCursodescripcion registrar([FromBody]HrCursodescripcion bean)
        {
            return hrCursodescripcionServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrCursodescripcion registrarCurso([FromBody]HrCursodescripcion bean)
        {
            return hrCursodescripcionServicio.registrarCurso(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrCursodescripcion actualizar([FromBody]HrCursodescripcion bean)
        {
            return hrCursodescripcionServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public HrCursodescripcion anular(Nullable<Int32> pCurso)
        {
            return hrCursodescripcionServicio.coreAnular(_usuarioActual, pCurso);
        }

        [HttpPost("[action]")]
        public void eliminar(Nullable<Int32> pCurso)
        {
            hrCursodescripcionServicio.coreEliminar(pCurso);
        }

        [HttpPost("[action]")]
        public DtoCurso eliminarCurso([FromBody] DtoCurso bean)
        {
            hrCursodescripcionServicio.eliminarCurso(bean.idCurso.Value);
            return bean;
        }

        [HttpGet("[action]")]
        public HrCursodescripcion obtenerporId(Nullable<Int32> pCurso)
        {
            return hrCursodescripcionServicio.obtenerPorId(pCurso);
        }

		/*alejandro*/
        [HttpPost("[action]")]
        public ParametroPaginacionGenerico solicitudListar([FromBody] FiltroPaginacionCurso filtro)
        {
            return hrCursodescripcionServicio.listarBusqueda(filtro.paginacion, filtro);
        }
        /*alejandro*/

		[HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroPaginacionCurso filtro)
        {
            return hrCursodescripcionServicio.listarPaginacion(filtro.paginacion, filtro);

        }
    }
}
