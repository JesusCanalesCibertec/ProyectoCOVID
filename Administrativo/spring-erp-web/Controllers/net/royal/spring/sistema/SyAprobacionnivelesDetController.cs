using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SyAprobacionnivelesDetController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SyAprobacionnivelesDetServicio syAprobacionnivelesDetServicio;
        public SyAprobacionnivelesDetController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            syAprobacionnivelesDetServicio = servicioProveedor.GetService<SyAprobacionnivelesDetServicio>();
        }

        [HttpGet("[action]/{CompanyOwner}/{Codigo}")]
        public List<SyAprobacionnivelesDet> listarPorNivelAprobacion(String CompanyOwner, Nullable<Int32> Codigo) {
            SyAprobacionnivelesPk pk = new SyAprobacionnivelesPk();
            pk.CompanyOwner = CompanyOwner;
            pk.Codigo = Codigo;
            return syAprobacionnivelesDetServicio.listarPorNivelAprobacion(pk);
        }

        [HttpGet("[action]/{procesoAprobar}/{nivelAprobar}")]
        public List<SyAprobacionnivelesDet> obtenerListaCorreoPorProceso(Int32? procesoAprobar, Int32? nivelAprobar) {
            return syAprobacionnivelesDetServicio.obtenerListaCorreoPorProceso(procesoAprobar, nivelAprobar);
        }

        [HttpGet("[action]/{codigo}/{nivel}/{compania}")]
        public List<SyAprobacionnivelesDet> listarPorCodigoNivel(Int32 codigo, Int32 nivel, String compania) {
            return syAprobacionnivelesDetServicio.listarPorCodigoNivel(codigo, nivel, compania);
        }
    }
}
