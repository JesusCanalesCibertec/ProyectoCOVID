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
    public class MpConocimientoController:SecuredBaseController
    {
        
        private IServiceProvider servicioProveedor;
        private MpConocimientoServicio mpConocimientoServicio;

        public MpConocimientoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor):base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            mpConocimientoServicio = servicioProveedor.GetService<MpConocimientoServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] DtoTabla filtro)
        {
            return mpConocimientoServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public MpConocimiento Registrar([FromBody]MpConocimiento bean)
        {
            return mpConocimientoServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public MpConocimiento ObtenerPorId(int pIdConocimiento)
        {
            return mpConocimientoServicio.obtenerPorId(pIdConocimiento);
        }


        [HttpPost("[action]")]
        public MpConocimiento Actualizar([FromBody]MpConocimiento bean)
        {
            return mpConocimientoServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MpConocimientoPk Cambiarestado([FromBody]MpConocimientoPk pk)
        {
            return mpConocimientoServicio.cambiarestado(pk);
        }

        [HttpPost("[action]")]
        public List<MpConocimiento> listado([FromBody]DtoTabla filtro)
        {
            return mpConocimientoServicio.listado(filtro);

        }


    }
}
