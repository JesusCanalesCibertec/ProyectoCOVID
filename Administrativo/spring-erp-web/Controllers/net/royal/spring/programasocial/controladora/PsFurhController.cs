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

    [Route("api/spring/psfurh/[controller]")]
    public class PsFurhController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsFurhServicio psFurhServicio;

        public PsFurhController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            psFurhServicio = servicioProveedor.GetService<PsFurhServicio>();
        }

        [HttpPost("[action]")]
        public DtoComponenteFurh registrar([FromBody]DtoComponenteFurh bean) {
            return psFurhServicio.insertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarFurh([FromBody]DtoComponenteFurh filtro) {

            
            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;

            setSessionData("filtroPaginacionFurh", filtro);

            return psFurhServicio.listarFurh(filtro.paginacion, filtro);
        }


        [HttpGet("[action]")]
        public JsonResult exportar() {
            ParametroPaginacionGenerico paginacion = new ParametroPaginacionGenerico();
            paginacion.CantidadRegistrosDevolver = 10000;
            paginacion.RegistroInicio = 0;
            DtoComponenteFurh filtroPaginacionEmpleado = getSessionData<DtoComponenteFurh>("filtroPaginacionFurh");
            filtroPaginacionEmpleado.paginacion = paginacion;
            return Json(new { url = psFurhServicio.Exportar(filtroPaginacionEmpleado) });
        }

        [HttpPost("[action]")]
        public DtoComponenteFurh actualizar([FromBody]DtoComponenteFurh bean) {
            return psFurhServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public DtoComponenteFurh anular([FromBody]DtoComponenteFurh bean) {
            UsuarioActual usuarioActual = _usuarioActual;
            return psFurhServicio.anular(_usuarioActual, bean.IdInstitucion, bean.IdEntidad);
        }

        [HttpGet("[action]")]
        public DtoComponenteFurh obtenerporid(String idInstitucion, Nullable<Int32> idEmpleado) {
            return psFurhServicio.obtenerPorId(idInstitucion, idEmpleado);
        }
}
}
