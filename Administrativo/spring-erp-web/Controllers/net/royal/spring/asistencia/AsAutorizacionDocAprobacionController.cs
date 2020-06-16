using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.planilla.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia
{
    [Route("api/spring/asistencia/[controller]")]
    public class AsAutorizacionDocAprobacionController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AsAutorizacionDocAprobacionServicio asAutorizacionDocAprobacionServicio;
        public AsAutorizacionDocAprobacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            asAutorizacionDocAprobacionServicio = servicioProveedor.GetService<AsAutorizacionDocAprobacionServicio>();
        }

        [HttpGet("[action]/{numeroProceso}")]
        public List<DtoProcesoSeguimiento> listarSeguiemiento(Int32 numeroProceso)
        {
            return asAutorizacionDocAprobacionServicio.listarSeguiemiento(numeroProceso);
        }

        [HttpPost("[action]")]
        public void registrar([FromBody]AsAutorizacion bean)
        {
            asAutorizacionDocAprobacionServicio.registrar(_usuarioActual, bean, bean.auxComentario);
        }

    }
}
