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
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class ProvinciaController : BaseController
    {
        private IServiceProvider servicioProveedor;
        private ProvinciaServicio provinciaServicio;
        public ProvinciaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            provinciaServicio = servicioProveedor.GetService<ProvinciaServicio>();
        }

        [HttpGet("[action]")]
        public List<Provincia> listarTodos()
        {
            return provinciaServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<Provincia> listarActivosPorDepartamento(String idDepartamento)
        {
            return provinciaServicio.listarActivosPorDepartamento(idDepartamento);
        }

        [HttpGet("[action]")]
        public JsonResult obtenerNombrePorId(String departamento, String provincia)
        {
            var value = provinciaServicio.obtenerNombrePorId(departamento, provincia);
            return Json(new { nombre = value });
        }

        [HttpPost("[action]")]
        public List<Provincia> listar([FromBody]FiltroProvincia filtro)
        {
            return provinciaServicio.listar(filtro);
        }
    }
}
