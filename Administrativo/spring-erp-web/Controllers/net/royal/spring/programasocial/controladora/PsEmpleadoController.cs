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
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/programasocial/[controller]")]
public class PsEmpleadoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsEmpleadoServicio psEmpleadoServicio;

        public PsEmpleadoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psEmpleadoServicio = servicioProveedor.GetService<PsEmpleadoServicio>();
        }

        [HttpPost("[action]")]
        public PsEmpleado registrar([FromBody]PsEmpleado bean)
        {
            return psEmpleadoServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsEmpleado actualizar([FromBody]PsEmpleado bean)
        {
            return psEmpleadoServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsEmpleado anular(UsuarioActual usuarioActual, String pIdInstitucion,Nullable<Int32> pIdEmpleado)
        {
            return psEmpleadoServicio.coreAnular(_usuarioActual,  pIdInstitucion, pIdEmpleado);
        }

        [HttpPost("[action]")]
        public void eliminar(String pIdInstitucion,Nullable<Int32> pIdEmpleado)
        {
            psEmpleadoServicio.coreEliminar( pIdInstitucion, pIdEmpleado);
        }

        [HttpGet("[action]")]
        public PsEmpleado obtenerporid(String pIdInstitucion,Nullable<Int32> pIdEmpleado)
        {
            return psEmpleadoServicio.obtenerPorId( pIdInstitucion, pIdEmpleado);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroPsEmpleado filtro)
        {
            return psEmpleadoServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpGet("[action]")]
        public IList<DtoTabla> listarAreas()
        {
            return psEmpleadoServicio.listarAreas();
        }

    }
}
