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
    public class TipocambiomastController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private TipocambiomastServicio tipocambiomastServicio;
        public TipocambiomastController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            tipocambiomastServicio = servicioProveedor.GetService<TipocambiomastServicio>();
        }

        [HttpGet("[action]")]
        public List<Tipocambiomast> listarTodos()
        {
            return tipocambiomastServicio.listarTodos();
        }

        [HttpGet("[action]/{fecha}")]
        public Tipocambiomast obtenerPorFecha(DateTime fecha)
        {
            return tipocambiomastServicio.obtenerPorFecha(fecha);
        }

        [HttpGet("[action]/{fechaCadena}")]
        public Tipocambiomast obtenerPorFecha(String fechaCadena)
        {
            return tipocambiomastServicio.obtenerPorFecha(fechaCadena);
        }

        [HttpGet("[action]/{fechaCadena}")]
        public float? obtenerFactorCompraPorFecha(String fechaCadena)
        {
            return tipocambiomastServicio.obtenerFactorCompraPorFecha(fechaCadena);
        }

    }
}
