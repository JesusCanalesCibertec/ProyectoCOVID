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
using System.Linq;
using System.Threading.Tasks;

namespace net.royal.spring.minedu
{
    [Route("api/spring/minedu/[controller]")]
    public class   MpPersonatituloController:SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private MpPersonatituloServicio MpPersonatituloServicio;

        public MpPersonatituloController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor):base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            MpPersonatituloServicio = servicioProveedor.GetService<MpPersonatituloServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] DtoTabla filtro)
        {
            return MpPersonatituloServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public MpPersonatitulo Registrar([FromBody]MpPersonatitulo bean)
        {
            return MpPersonatituloServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public MpPersonatitulo ObtenerPorId(int pIdPersona)
        {
            return MpPersonatituloServicio.obtenerPorId(pIdPersona);
        }

        [HttpPost("[action]")]
        public MpPersonatitulo ObtenerPorId([FromBody] MpPersonatituloPk pk)
        {
            return MpPersonatituloServicio.obtenerPorId(pk);
        }


        [HttpPost("[action]")]
        public MpPersonatitulo Actualizar([FromBody]MpPersonatitulo bean)
        {
            return MpPersonatituloServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MpPersonatitulo eliminar([FromBody] MpPersonatitulo bean)
        {

            MpPersonatituloServicio.eliminar(bean);
            return bean;
        }

        [HttpPost("[action]")]
        public MpPersonatituloPk Cambiarestado([FromBody]MpPersonatituloPk pk)
        {
            return MpPersonatituloServicio.cambiarestado(pk);
        }

        [HttpGet("[action]")]
        public List<DtoPersonatitulo> listado(Int32? pIdPersona)
        {
            return MpPersonatituloServicio.listado(pIdPersona);
        }


    }
}
