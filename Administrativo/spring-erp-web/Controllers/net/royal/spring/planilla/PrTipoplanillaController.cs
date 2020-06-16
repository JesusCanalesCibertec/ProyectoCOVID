using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.planilla.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.planilla.dominio;

namespace net.royal.spring.planilla
{
    [Route("api/spring/planilla/[controller]")]
    public class PrTipoplanillaController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private PrTipoplanillaServicio prTipoplanillaServicio;
        public PrTipoplanillaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            prTipoplanillaServicio = servicioProveedor.GetService<PrTipoplanillaServicio>();
        }

        [HttpGet("[action]")]
        public List<PrTipoplanilla> listarTodos() {
            return prTipoplanillaServicio.listarTodos();
        }

        [HttpGet("[action]/{tipoplanilla}")]
        public PrTipoplanilla obtenerPorId(String tipoplanilla) {
            PrTipoplanillaPk pk = new PrTipoplanillaPk();
            return prTipoplanillaServicio.obtenerPorId(pk);
        }

    }
}
