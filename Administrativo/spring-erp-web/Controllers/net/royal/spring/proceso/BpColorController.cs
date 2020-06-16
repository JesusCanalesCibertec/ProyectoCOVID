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
    public class BpColorController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpColorServicio BpColorServicio;

        public BpColorController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            BpColorServicio = servicioProveedor.GetService<BpColorServicio>();
        }


        [HttpGet("[action]")]
        public List<BpColor> listarTodos()
        {
            return BpColorServicio.listarTodos();
        }

    }
}