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
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.filtro;

namespace net.royal.spring.proyecto.controladora
{

    [Route("api/spring/proyecto/[controller]")]
    public class PmProyectoController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private PmProyectoServicio pmProyectoServicio;
        private PmTipoproyectoServicio pmTipoproyectoServicio;

        public PmProyectoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            pmProyectoServicio = servicioProveedor.GetService<PmProyectoServicio>();
            pmTipoproyectoServicio = servicioProveedor.GetService<PmTipoproyectoServicio>();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody]FiltroPaginacionProyecto filtro)
        {
            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;
            return pmProyectoServicio.listarPaginacion(filtro);
        }


        [HttpGet("[action]")]
        public PmProyecto obtenerPorIdProyecto(Int32 idPortafolio, Int32 idPrograma, Int32 idProyecto)
        {
            return pmProyectoServicio.obtenerPorIdProyecto(new PmProyectoPk() { IdPortafolio = idPortafolio, IdPrograma = idPrograma, IdProyecto = idProyecto });
        }

        [HttpGet("[action]")]
        public PmProyecto obtenerPorIdTransaccionParaNotificacion(Int32 idTransaccion)
        {
            PmProyecto bean = pmProyectoServicio.obtenerPorIdTransaccionParaNotificacion(idTransaccion);
            bean.tipoProyecto = pmTipoproyectoServicio.obtenerPorId(bean.IdTipoProyecto);
            return bean;
        }

    }
}
