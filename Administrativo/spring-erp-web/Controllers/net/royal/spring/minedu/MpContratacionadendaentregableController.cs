using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net.royal.spring.minedu
{
    [Route("api/spring/minedu/[controller]")]
    public class   MpContratacionadendaentregableController:SecuredBaseController
    {
        
        private IServiceProvider servicioProveedor;
        private MpContratacionadendaentregableServicio mpContratacionadendaentregableServicio;

        public MpContratacionadendaentregableController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor):base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            mpContratacionadendaentregableServicio = servicioProveedor.GetService<MpContratacionadendaentregableServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] DtoTabla filtro)
        {
            return mpContratacionadendaentregableServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public MpContratacionadendaentregable Registrar([FromBody]MpContratacionadendaentregable bean)
        {
            return mpContratacionadendaentregableServicio.registrar(_usuarioActual, bean);
        }

       

        [HttpGet("[action]")]
        public MpContratacionadendaentregable ObtenerPorId(int pIdContratacionadendaentregable)
        {
            return mpContratacionadendaentregableServicio.obtenerPorId(pIdContratacionadendaentregable);
        }


        [HttpPost("[action]")]
        public MpContratacionadendaentregable Actualizar([FromBody]MpContratacionadendaentregable bean)
        {
            return mpContratacionadendaentregableServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MpContratacionadendaentregablePk Cambiarestado([FromBody]MpContratacionadendaentregablePk pk)
        {
            return mpContratacionadendaentregableServicio.cambiarestado(pk);
        }

        [HttpPost("[action]")]
        public List<MpContratacionadendaentregable> listado([FromBody]DtoTabla filtro)
        {
            return mpContratacionadendaentregableServicio.listado(filtro);

        }

        [HttpGet("[action]")]
        public List<MpContratacionadendaentregable> listado(Int32? pIdContratacion)
        {
            return mpContratacionadendaentregableServicio.listado(pIdContratacion);
        }


    }
}
