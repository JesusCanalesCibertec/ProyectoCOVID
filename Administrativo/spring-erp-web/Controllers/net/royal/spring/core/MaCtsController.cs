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
    public class MaCtsController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private MaCtsServicio maCtsServicio;
        public MaCtsController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            maCtsServicio = servicioProveedor.GetService<MaCtsServicio>();
        }

        [HttpGet("[action]")]
        public List<MaCts> listarTodos() {
            return maCtsServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<MaCts> listarActivos()
        {
            return maCtsServicio.listarActivos();
        }

        [HttpPost("[action]")]
        public List<MaCts> listar([FromBody]DtoFiltroEntero filtro)
        {
            return maCtsServicio.listar(filtro);
        }
    }
}
