
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
public class PsComponenteController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsComponenteServicio psComponenteServicio;

        public PsComponenteController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psComponenteServicio = servicioProveedor.GetService<PsComponenteServicio>();
        }

        [HttpPost("[action]")]
        public PsComponente registrar([FromBody]PsComponente bean)
        {
            return psComponenteServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsComponente actualizar([FromBody]PsComponente bean)
        {
            return psComponenteServicio.coreActualizar(_usuarioActual,bean);
        }
        /*
        [HttpGet("[action]")]
        public PsComponente anular(String pIdInstitucion, Nullable)
        {
            return psComponenteServicio.coreAnular(_usuarioActual,  pIdComponente);
        }*/

        [HttpPost("[action]")]
        public void eliminar(String pIdComponente)
        {
            psComponenteServicio.coreEliminar( pIdComponente);
        }

        [HttpGet("[action]")]
        public PsComponente obtenerporid(String pIdComponente)
        {
            return psComponenteServicio.obtenerPorId( pIdComponente);
        }

       

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroComponente filtro)
        {
            return psComponenteServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public PsComponente eliminarfila([FromBody] PsComponente bean)
        {
            psComponenteServicio.eliminarfila(bean.IdComponente);
            return bean;
        }

        [HttpGet("[action]")]
        public List<PsComponente> listarTodos()
        {
            return psComponenteServicio.listarTodos();
        }


    }
}
