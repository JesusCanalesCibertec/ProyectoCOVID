using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.core.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.web.controller;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class ZonapostalController : BaseController
    {
        private IServiceProvider servicioProveedor;
        private ZonapostalServicio zonapostalServicio;
        public ZonapostalController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base( httpContextAccessor, _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            zonapostalServicio = servicioProveedor.GetService<ZonapostalServicio>();
        }

        [HttpGet("[action]")]
        public List<Zonapostal> listarTodos()
        {
            return zonapostalServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<Zonapostal> listarActivosPorProvincia(String idDepartamento, String idProvincia)
        {
            return zonapostalServicio.listarActivosPorProvincia(idDepartamento, idProvincia);
        }

        [HttpGet("[action]")]
        public JsonResult obtenerNombrePorId(String departamento, String provincia, String distrito)
        {
            var value = zonapostalServicio.obtenerNombrePorId(departamento, provincia, distrito);
            return Json(new { nombre = value });
        }

        [HttpPost("[action]")]
        public List<Zonapostal> listar(FiltroZonaPostal filtro)
        {
            return zonapostalServicio.listar(filtro);
        }
    }
}
