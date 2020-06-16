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

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class MonedamastController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private MonedamastServicio monedamastServicio;
        public MonedamastController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            monedamastServicio = servicioProveedor.GetService<MonedamastServicio>();
        }

        [HttpGet("[action]")]
        public List<Monedamast> listarTodos() {
            return monedamastServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<Monedamast> listarActivos()
        {
            return monedamastServicio.listarActivos();
        }

    }
}
