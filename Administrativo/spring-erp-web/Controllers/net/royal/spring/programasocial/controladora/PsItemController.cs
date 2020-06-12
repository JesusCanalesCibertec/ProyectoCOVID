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
public class PsItemController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsItemServicio PsItemServicio;

        public PsItemController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            PsItemServicio = servicioProveedor.GetService<PsItemServicio>();
        }

        [HttpPost("[action]")]
        public PsItem registrar([FromBody]PsItem bean)
        {
            return PsItemServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsItem actualizar([FromBody]PsItem bean)
        {
            return PsItemServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpGet("[action]")]
        public PsItem anular(String pIdItem)
        {
            return PsItemServicio.coreAnular(_usuarioActual,  pIdItem);
        }

        [HttpPost("[action]")]
        public void eliminarfila(String pIdItem)
        {
              PsItemServicio.coreEliminar( pIdItem);
        }

        [HttpPost("[action]")]
        public DtoItem eliminar([FromBody] DtoItem bean)
        {
            PsItemServicio.eliminar(bean.idItem);
            return bean;
        }

        [HttpGet("[action]")]
        public PsItem obtenerporid(String pIdItem)
        {
            return PsItemServicio.obtenerPorId( pIdItem);
        }

       

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroItem filtro)
        {
            return PsItemServicio.listarPaginacion(filtro.paginacion, filtro);

        }


      

    }
}
