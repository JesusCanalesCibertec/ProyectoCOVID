using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.framework.web.controller;

namespace net.royal.spring.asistencia
{
    [Route("api/spring/asistencia/[controller]")]
    public class AsAreaController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AsAreaServicio asAreaServicio;
        public AsAreaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            asAreaServicio = servicioProveedor.GetService<AsAreaServicio>();
        }

        [HttpGet("[action]")]
        public List<AsArea> listarTodos() { return asAreaServicio.listarTodos();  }
    }
}
