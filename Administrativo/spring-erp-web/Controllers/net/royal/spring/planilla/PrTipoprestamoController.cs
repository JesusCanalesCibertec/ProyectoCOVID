
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.planilla.dominio;
using net.royal.spring.planilla.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net.royal.spring.framework.web.controller;

namespace net.royal.spring.planilla
{
    [Route("api/spring/planilla/[controller]")]
    public class PrTipoprestamoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private PrTipoprestamoServicio prTipoprestamoServicio;

        public PrTipoprestamoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            prTipoprestamoServicio = servicioProveedor.GetService<PrTipoprestamoServicio>();
        }

        [HttpGet("[action]")]
        public List<PrTipoprestamo> listarTodos() {
            return prTipoprestamoServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<PrTipoprestamo> listarSoloWeb() {
            return prTipoprestamoServicio.listarSoloWeb();
        }

        [HttpGet("[action]")]
        public List<PrTipoprestamo> listarActivos()
        {
            return prTipoprestamoServicio.listarActivos();
        }

        [HttpGet("[action]/{Tipoprestamo}")]
        public PrTipoprestamo obtenerPorId(String Tipoprestamo) {
            return prTipoprestamoServicio.obtenerPorId(new PrTipoprestamoPk(Tipoprestamo));
        }
    }
}
