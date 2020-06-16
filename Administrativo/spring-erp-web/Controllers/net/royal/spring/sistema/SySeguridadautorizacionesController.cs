using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SySeguridadautorizacionesController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SySeguridadautorizacionesServicio sySeguridadautorizacionesServicio;
        public SySeguridadautorizacionesController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            sySeguridadautorizacionesServicio = servicioProveedor.GetService<SySeguridadautorizacionesServicio>();
        }

        [HttpGet("[action]/{idUsuario}")]
        public List<DtoTabla> listarEmpresasPorUsuario(String idUsuario) {
            return sySeguridadautorizacionesServicio.listarEmpresasPorUsuario(idUsuario);
        }

        [HttpGet("[action]/{idUsuario}")]
        public List<DtoTabla> listarEmpresasPorUsuarioEnEmpleadoYSeguridadAutorizacion(String idUsuario) {
            return sySeguridadautorizacionesServicio.listarEmpresasPorUsuarioEnEmpleadoYSeguridadAutorizacion(idUsuario);
        }
    }
}
