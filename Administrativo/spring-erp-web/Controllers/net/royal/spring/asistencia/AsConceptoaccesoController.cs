using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.framework.core;
using net.royal.spring.framework.web.controller;

namespace net.royal.spring.asistencia
{
    [Route("api/spring/asistencia/[controller]")]
    public class AsConceptoaccesoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AsConceptoaccesoServicio asConceptoaccesoServicio;
        public AsConceptoaccesoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            asConceptoaccesoServicio = servicioProveedor.GetService<AsConceptoaccesoServicio>();
        }

        [HttpGet("[action]")]
        public List<AsConceptoacceso> ListarActivos() { return asConceptoaccesoServicio.ListarActivos(); }

        [HttpGet("[action]")]
        public List<AsConceptoacceso> ListarActivosOtros(String flagWeb) {
            return asConceptoaccesoServicio.ListarActivosOtros(UBoolean.validarFlag(flagWeb));
        }
    }
}
