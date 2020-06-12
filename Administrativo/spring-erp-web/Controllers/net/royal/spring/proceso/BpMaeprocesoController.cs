using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.framework.web.controller;
using net.royal.spring.proceso.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.proceso.dominio;

namespace net.royal.spring.proceso
{
    [Route("api/spring/proceso/[controller]")]
    public class BpMaeprocesoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpMaeprocesoServicio BpMaeprocesoServicio;

        public BpMaeprocesoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            BpMaeprocesoServicio = servicioProveedor.GetService<BpMaeprocesoServicio>();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroBpMaeproceso filtro)
        {
            return BpMaeprocesoServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public BpMaeproceso registrar([FromBody]BpMaeproceso bean)
        {
            return BpMaeprocesoServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeproceso actualizar([FromBody]BpMaeproceso bean)
        {
            return BpMaeprocesoServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public BpMaeproceso obtenerPorId(string codigo)
        {
            return BpMaeprocesoServicio.obtenerPorId(codigo);
        }

        [HttpGet("[action]")]
        public List<BpMaeproceso> listarTodos()
        {
            return BpMaeprocesoServicio.listarTodos();
        }



    }
}