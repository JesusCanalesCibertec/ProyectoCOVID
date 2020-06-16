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
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso
{
    [Route("api/spring/proceso/[controller]")]
    public class BpTransaccionController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpTransaccionServicio bpTransaccionServicio;

        public BpTransaccionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            bpTransaccionServicio = servicioProveedor.GetService<BpTransaccionServicio>();
        }

        [HttpGet("[action]")]
        public BpTransaccion obtenerPorId(Int32 idTransaccion)
        {
            return bpTransaccionServicio.obtenerPorId(idTransaccion);
        }

        [HttpGet("[action]")]
        public List<DtoTransaccionNotificacion> listarTransaccionesPorUsuario(String idProceso)
        {
            return bpTransaccionServicio.listarTransaccionesPorUsuario(_usuarioActual, idProceso);
        }
    }
}