using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.core.servicio;
using net.royal.spring.rrhh.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.core.dominio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.web.controller;

namespace net.royal.spring.core
{
    [Route("api/spring/rrhh/[controller]")]
    public class HrGradoInstruccionController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private HrGradoinstruccionServicio hrGradoinstruccionServicio;

        public HrGradoInstruccionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrGradoinstruccionServicio = servicioProveedor.GetService<HrGradoinstruccionServicio>();
        }

        [HttpGet("[action]")]
        public List<HrGradoinstruccion> listarActivos()
        {
            return hrGradoinstruccionServicio.listarActivos();
        }
    }
}
