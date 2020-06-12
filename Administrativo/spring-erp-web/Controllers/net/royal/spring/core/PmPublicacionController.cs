using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.core.servicio;
using net.royal.spring.rrhh.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.web.controller;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class PmPublicacionController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private PmPublicacionServicio pmPublicacionServicio;

        public PmPublicacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            pmPublicacionServicio = servicioProveedor.GetService<PmPublicacionServicio>();
        }

        [HttpGet("[action]")]
        public IList<DtoTabla> listarPublicacionesDashboard()
        {
            return pmPublicacionServicio.listarPublicacionesDashboard();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarBusqueda([FromBody]FiltroPaginacionPublicaciones filtro)
        {
            return pmPublicacionServicio.listarBusqueda(filtro);
        }

        [HttpPost("[action]")]
        public PmPublicacion obtenerPorid([FromBody]PmPublicacionPk pk)
        {
            return pmPublicacionServicio.obtenerPorid(pk);
        }

        [HttpPost("[action]")]
        public PmPublicacion registrar([FromBody]PmPublicacion bean)
        {
            return pmPublicacionServicio.registrar(bean, _usuarioActual);
        }

        [HttpPost("[action]")]
        public PmPublicacion actualizar([FromBody]PmPublicacion bean)
        {
            return pmPublicacionServicio.actualizar(bean, _usuarioActual);
        }

        [HttpGet("[action]")]
        public IList<DtoTabla> listarIndicadores()
        {
            return pmPublicacionServicio.listarIndicadores(_usuarioActual);
        }
    }
}
