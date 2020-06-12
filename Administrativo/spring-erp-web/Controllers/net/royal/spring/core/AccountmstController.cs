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
    public class AccountmstController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AccountmstServicio accountmstServicio;
        public AccountmstController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            accountmstServicio = servicioProveedor.GetService<AccountmstServicio>();
        }

        [HttpGet("[action]")]
        public List<Accountmst> listarTodos() {
            return accountmstServicio.listarTodos();
        }
    }
}
