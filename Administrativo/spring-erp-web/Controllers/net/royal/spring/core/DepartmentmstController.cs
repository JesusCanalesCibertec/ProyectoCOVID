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
    public class DepartmentmstController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private DepartmentmstServicio departmentmstServicio;
        public DepartmentmstController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            departmentmstServicio = servicioProveedor.GetService<DepartmentmstServicio>();
        }

        [HttpGet("[action]")]
        public List<Departmentmst> listarTodos()
        {
            return departmentmstServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<Departmentmst> listarActivos()
        {
            return departmentmstServicio.listarActivos();
        }

        [HttpPost("[action]")]
        public List<Departmentmst> listar([FromBody]DtoFiltro filtro)
        {
            return departmentmstServicio.listar(filtro);
        }

        [HttpPost("[action]")]
        public List<Departmentmst> listarBusqueda([FromBody]DtoFiltro filtroPaginacionJefatura)
        {
            return departmentmstServicio.listarBusqueda(filtroPaginacionJefatura);
        }
    }
}
