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

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class PaisController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private PaisServicio paisServicio;
        public PaisController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            paisServicio = servicioProveedor.GetService<PaisServicio>();
        }

        [HttpGet("[action]")]
        public List<Pais> listarTodos()
        {
            return paisServicio.listarTodos();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarUbigeoPorFiltro([FromBody]DtoTabla filtro)
        {
            return paisServicio.listarUbigeoPorFiltro(filtro.paginacion, filtro);
        }

        [HttpGet("[action]/{ubigeo}")]
        public DtoTabla obtenerUbigeo(String ubigeo)
        {
            return paisServicio.obtenerUbigeo(ubigeo);
        }
    }
}
