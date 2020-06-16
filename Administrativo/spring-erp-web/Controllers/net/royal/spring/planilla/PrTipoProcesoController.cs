
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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.planilla
{
    [Route("api/spring/planilla/[controller]")]
    public class PrTipoProcesoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private PrTipoProcesoServicio prTipoProcesoServicio;
        public PrTipoProcesoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            prTipoProcesoServicio = servicioProveedor.GetService<PrTipoProcesoServicio>();
        }

        [HttpGet("[action]")]
        public List<PrTipoProceso> listarProcesosPrestamo() {
            return prTipoProcesoServicio.listarProcesosPrestamo();
        }

        [HttpGet("[action]")]
        public List<PrTipoProceso> listarActivos() {
            return prTipoProcesoServicio.listarActivos();
        }

        [HttpGet("[action]/{periodo}/{personaId}")]
        public List<DtoTabla> listarPorPeriodoEmpleado(string periodo, int? personaId) {
            return prTipoProcesoServicio.listarPorPeriodoEmpleado(periodo, personaId);
        }
        
    }
}
