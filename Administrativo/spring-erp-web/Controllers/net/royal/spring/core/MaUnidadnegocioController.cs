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
    public class MaUnidadnegocioController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private MaUnidadnegocioServicio maUnidadnegocioServicio;
        public MaUnidadnegocioController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            maUnidadnegocioServicio = servicioProveedor.GetService<MaUnidadnegocioServicio>();
        }

        [HttpGet("[action]")]
        public List<MaUnidadnegocio> listarTodos() {
            return maUnidadnegocioServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<MaUnidadnegocio> listarActivos()
        {
            return maUnidadnegocioServicio.listarActivos();
        }

    }
}
