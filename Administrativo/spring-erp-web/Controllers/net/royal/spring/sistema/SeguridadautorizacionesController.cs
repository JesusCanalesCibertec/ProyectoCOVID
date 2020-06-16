using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using net.royal.spring.planilla.servicio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.sistema.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.web.controller;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SeguridadautorizacionesController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SeguridadautorizacionesServicio seguridadautorizacionesServicio;
        public SeguridadautorizacionesController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            seguridadautorizacionesServicio = servicioProveedor.GetService<SeguridadautorizacionesServicio>();
        }

        [HttpGet("[action]/{idUsuario}")]
        public List<DtoTabla> listarAplicacionPorUsuario(String idUsuario) {
            return seguridadautorizacionesServicio.listarAplicacionPorUsuario(idUsuario);
        }

        [HttpGet("[action]/{idUsuario}")]
        public string esRRHH(String idUsuario) {
            return seguridadautorizacionesServicio.esRRHH(idUsuario);
        }
    }
}
