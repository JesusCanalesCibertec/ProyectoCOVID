using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class AplicacionesmastController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AplicacionesmastServicio aplicacionesmastServicio;
        public AplicacionesmastController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            aplicacionesmastServicio = servicioProveedor.GetService<AplicacionesmastServicio>();
        }

        [HttpGet("[action]")]
        public  List<Aplicacionesmast> ListarActivos() {
            return aplicacionesmastServicio.ListarActivos();
        }
    }
}
