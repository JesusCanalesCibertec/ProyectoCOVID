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
    public class CompaniamastController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private CompaniamastServicio companiamastServicio;
        public CompaniamastController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            companiamastServicio = servicioProveedor.GetService<CompaniamastServicio>();
        }

        [HttpGet("[action]")]
        public List<Companiamast> listarTodos()
        {
            return companiamastServicio.listarTodos();
        }

        [HttpGet("[action]/{Companiacodigo}")]
        public Companiamast obtenerPorId(String Companiacodigo)
        {
            CompaniamastPk pk = new CompaniamastPk();
            pk.Companiacodigo = Companiacodigo;
            return companiamastServicio.obtenerPorId(pk);
        }

}
}
