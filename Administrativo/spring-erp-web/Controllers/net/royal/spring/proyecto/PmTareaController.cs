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
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proyecto.controladora
{

    [Route("api/spring/proyecto/[controller]")]
    public class PmTareaController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private PmTareaServicio pmTareaServicio;
        private PmTipoproyectoServicio pmTipoproyectoServicio;
        private PmProyectoServicio pmProyectoServicio;

        public PmTareaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            pmTareaServicio = servicioProveedor.GetService<PmTareaServicio>();
            pmTipoproyectoServicio = servicioProveedor.GetService<PmTipoproyectoServicio>();
            pmProyectoServicio = servicioProveedor.GetService<PmProyectoServicio>();
        }

        [HttpPost("[action]")]
        public List<DtoTarea> listarTareaSegunGrupoProyecto([FromBody]FiltroTarea filtro)
        {
            return pmTareaServicio.listarTareaSegunGrupoProyecto(filtro);
        }



        [HttpPost("[action]")]
        public PmTarea guardarTareaIper([FromBody]PmTarea pmTarea)
        {
            return this.pmTareaServicio.guardarTareaIper(pmTarea, _usuarioActual);
        }

        [HttpPost("[action]")]
        public PmTarea actualizarTareaIper([FromBody]PmTarea pmTarea)
        {
            return this.pmTareaServicio.actualizarTareaIper(pmTarea, _usuarioActual);
        }

        [HttpGet("[action]")]
        public PmTarea obtenerPorIdTarea(Int32 idProyecto, Int32 idTarea)
        {
            return this.pmTareaServicio.obtenerPorIdTarea(new PmTareaPk() { IdTarea = idTarea, IdProyecto = idProyecto });
        }

        [HttpGet("[action]")]
        public PmTarea obtenerPorIdTransaccionParaNotificacion(Int32 idTransaccion)
        {
            PmTarea bean = pmTareaServicio.obtenerPorIdTransaccionParaNotificacion(idTransaccion);
            var proy = pmProyectoServicio.obtenerPorIdProyecto(new PmProyectoPk() { IdProyecto = bean.IdProyecto, IdPortafolio = 1, IdPrograma = 1 });
            bean.tipoProyecto = pmTipoproyectoServicio.obtenerPorId(proy.IdTipoProyecto);
            return bean;
        }

        //[HttpGet("[action]")]
        //public IList<PmTarea> listarTareasPorPersona()
        //{
        //    return pmTareaServicio.listarTareasPorPersona(_usuarioActual.PersonaId);
        //}

        [HttpPost("[action]")]
        public DtoTarea asignarResponsable([FromBody]List<DtoTarea> lista)
        {
            pmTareaServicio.asignarResponsable(_usuarioActual, lista);
            return new DtoTarea();
        }

        [HttpPost("[action]")]
        public DtoTarea generarPermisos([FromBody]List<DtoTarea> lista)
        {
            pmTareaServicio.generarPermisos(_usuarioActual, lista);
            return new DtoTarea();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPlantillasTarea([FromBody]DtoTabla filtro)
        {
            return pmTareaServicio.listarPlantillasTarea(filtro, filtro.paginacion);
        }

        [HttpGet("[action]")]
        public List<PmPlantilla> listarPlantillas()
        {
            return pmTareaServicio.listarPlantillas();
        }

        [HttpPost("[action]")]
        public DtoProyecto agregarPlantilla([FromBody]DtoProyecto dto)
        {
            pmTareaServicio.agregarPlantilla(_usuarioActual, dto);
            return new DtoProyecto();
        }


    }
}
