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

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/programasocial/[controller]")]
public class PsProgramaController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsProgramaServicio psProgramaServicio;

        public PsProgramaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psProgramaServicio = servicioProveedor.GetService<PsProgramaServicio>();
        }

        [HttpPost("[action]")]
        public PsPrograma registrar([FromBody]PsPrograma bean)
        {
            return psProgramaServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsPrograma actualizar([FromBody]PsPrograma bean)
        {
            return psProgramaServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpGet("[action]")]
        public PsPrograma anular(UsuarioActual usuarioActual, String pIdPrograma)
        {
            return psProgramaServicio.coreAnular(_usuarioActual,  pIdPrograma);
        }

        [HttpGet("[action]")]
        public PsPrograma obtenerporid(String pIdPrograma)
        {
            return psProgramaServicio.obtenerPorId( pIdPrograma);
        }


        [HttpPost("[action]")]
        public PsPrograma eliminar([FromBody] PsPrograma bean)
        {

            psProgramaServicio.eliminar(bean.IdPrograma);
            return bean;
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroPrograma filtro)
        {
            return psProgramaServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpGet("[action]")]
        public List<PsPrograma> listarTodos()
        {
            return psProgramaServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<PsPrograma> listarTodosActivos() {
            return psProgramaServicio.listarTodosActivos();
        }

        [HttpGet("[action]")]
        public List<PsPrograma> listarPorInstitucion(String pIdInstitucion)
        {            
            return psProgramaServicio.listarPorInstitucion(_usuarioActual.UsuarioLogin, pIdInstitucion);
        }

    }
}
