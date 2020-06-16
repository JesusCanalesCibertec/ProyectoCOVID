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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class CompanyownerController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private CompanyownerServicio companyownerServicio;
        public CompanyownerController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            companyownerServicio = servicioProveedor.GetService<CompanyownerServicio>();
        }

        [HttpGet("[action]")]
        public List<Companyowner> listarTodos()
        {
            return companyownerServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public string obtenerNombre(string companyowner)
        {
            return companyownerServicio.obtenerNombre(companyowner);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> listarActivos()
        {
            return companyownerServicio.listarActivos();
        }

        [HttpGet("[action]")]
        public List<DtoTabla> listarCompaniasPorSeguridad()
        {
            return companyownerServicio.listarCompaniasPorSeguridad(_usuarioActual.UsuarioLogin);
        }
    }
}
