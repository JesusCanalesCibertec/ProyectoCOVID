using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using net.royal.spring.minedu.servicio;
using System;
using System.Collections.Generic;


namespace net.royal.spring.minedu
{
    [Route("api/spring/minedu/[controller]")]
    public class   MpProyectoController:SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private MpProyectoServicio mpProyectoServicio;

        public MpProyectoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor):base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            mpProyectoServicio = servicioProveedor.GetService<MpProyectoServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] DtoTabla filtro)
        {
            return mpProyectoServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public MpProyecto Registrar([FromBody]MpProyecto bean)
        {
            return mpProyectoServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public MpProyecto ObtenerPorId(int pIdProyecto)
        {
            return mpProyectoServicio.obtenerPorId(pIdProyecto);
        }


        [HttpPost("[action]")]
        public MpProyecto Actualizar([FromBody]MpProyecto bean)
        {
            return mpProyectoServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MpProyectoPk Cambiarestado([FromBody]MpProyectoPk pk)
        {
            return mpProyectoServicio.cambiarestado(pk);
        }

        [HttpGet("[action]")]
        public List<MpProyecto> ListarNombres()
        {
            return mpProyectoServicio.ListarNombres();
        }

        [HttpGet("[action]")]
        public List<DtoProyecto> Listardetalle(string pTipo, int? pAnio, string pEstado)
        {
            return mpProyectoServicio.Listardetalle(pTipo,pAnio,pEstado);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> ListarBarraEstados()
        {
            return mpProyectoServicio.ListarBarraEstados();
        }

        [HttpGet("[action]")]
        public List<DtoTabla> ListarBarraTipos()
        {
            return mpProyectoServicio.ListarBarraTipos();
        }

        [HttpGet("[action]")]
        public List<MpProyectorecurso> ListarEquipotecnico(int pIdProyecto)
        {
            return mpProyectoServicio.ListarEquipotecnico(pIdProyecto);
        }


    }
}
