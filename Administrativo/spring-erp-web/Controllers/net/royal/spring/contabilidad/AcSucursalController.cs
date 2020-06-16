using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.contabilidad.dominio.filtro;
using net.royal.spring.contabilidad.servicio;
using net.royal.spring.framework.web.controller;

namespace net.royal.spring.contabilidad
{
    [Route("api/spring/contabilidad/[controller]")]
    public class AcSucursalController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AcSucursalServicio acSucursalServicio;
        public AcSucursalController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            acSucursalServicio = servicioProveedor.GetService<AcSucursalServicio>();
        }

        [HttpPost("[action]")]
        public List<AcSucursal> listar([FromBody]FiltroAcSucursal filtro) {
            return acSucursalServicio.listar(filtro);
        }

        [HttpGet("[action]")]
        public List<AcSucursal> listarTodos()
        {
            return acSucursalServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<AcSucursal> listarActivoOrdenadoPorNombre()
        {
            return acSucursalServicio.listarActivoOrdenadoPorNombre();
        }

        [HttpGet("[action]")]
        public List<AcSucursal> listarActivoOrdenadoPorCodigo()
        {
            return acSucursalServicio.listarActivoOrdenadoPorCodigo();
        }
    }
}
