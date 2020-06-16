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
    public class BpTransaccionseguimientoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpTransaccionseguimientoServicio bpTransaccionseguimientoServicio;

        public BpTransaccionseguimientoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            bpTransaccionseguimientoServicio = servicioProveedor.GetService<BpTransaccionseguimientoServicio>();
        }

        [HttpGet("[action]")]
        public List<BpTransaccionseguimiento> listarEstadoPorProceso(Int32 idTransaccion)
        {
            return bpTransaccionseguimientoServicio.listarTransaccionSeguimiento(idTransaccion);
        }
    }
}