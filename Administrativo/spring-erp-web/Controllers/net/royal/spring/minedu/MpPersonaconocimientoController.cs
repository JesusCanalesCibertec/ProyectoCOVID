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
    public class   MpPersonaconocimientoController:SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private MpPersonaconocimientoServicio MpPersonaconocimientoServicio;

        public MpPersonaconocimientoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor):base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            MpPersonaconocimientoServicio = servicioProveedor.GetService<MpPersonaconocimientoServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] DtoTabla filtro)
        {
            return MpPersonaconocimientoServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public MpPersonaconocimiento Registrar([FromBody]MpPersonaconocimiento bean)
        {
            return MpPersonaconocimientoServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public MpPersonaconocimiento ObtenerPorId(int pIdPersona)
        {
            return MpPersonaconocimientoServicio.obtenerPorId(pIdPersona);
        }


        [HttpPost("[action]")]
        public MpPersonaconocimiento Actualizar([FromBody]MpPersonaconocimiento bean)
        {
            return MpPersonaconocimientoServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MpPersonaconocimiento eliminar([FromBody] MpPersonaconocimiento bean)
        {

            MpPersonaconocimientoServicio.eliminar(bean);
            return bean;
        }

        [HttpPost("[action]")]
        public MpPersonaconocimientoPk Cambiarestado([FromBody]MpPersonaconocimientoPk pk)
        {
            return MpPersonaconocimientoServicio.cambiarestado(pk);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> listado(Int32? pIdPersona)
        {
            return MpPersonaconocimientoServicio.listado(pIdPersona);
        }


    }
}
