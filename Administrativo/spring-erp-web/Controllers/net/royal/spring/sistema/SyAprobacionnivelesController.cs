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
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SyAprobacionnivelesController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SyAprobacionnivelesServicio syAprobacionnivelesServicio;
        public SyAprobacionnivelesController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            syAprobacionnivelesServicio = servicioProveedor.GetService<SyAprobacionnivelesServicio>();
        }

        [HttpGet("[action]/{proceso}/{comania}")]
        public SyAprobacionniveles obtenerPorCodigoProcesoCompania(int proceso, string comania)
        {
            return syAprobacionnivelesServicio.obtenerPorCodigoProcesoCompania(proceso, comania);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> listarAplicacionPorUsuario()
        {
            return syAprobacionnivelesServicio.listarAplicacionPorUsuario(_usuarioActual.UsuarioLogin);
        }

        [HttpPost("[action]")]
        public List<SyAprobacionniveles> listar([FromBody]FiltroNivelAprobacion filtro)
        {
            return syAprobacionnivelesServicio.listar(filtro);
        }

        [HttpPost("[action]")]
        public SyAprobacionniveles obtenerPorIdCompleto([FromBody]SyAprobacionnivelesPk syAprobacionnivelesPk)
        {
            return syAprobacionnivelesServicio.obtenerPorIdCompleto(syAprobacionnivelesPk);
        }

        [HttpPost("[action]")]
        public void actualizar([FromBody]SyAprobacionniveles syAprobacionniveles)
        {
            syAprobacionnivelesServicio.actualizar(_usuarioActual, syAprobacionniveles);
        }

        [HttpGet("[action]")]
        public void eliminar(Int32? codigo, string compania)
        {
            syAprobacionnivelesServicio.eliminar(_usuarioActual, codigo, compania);
        }

        [HttpPost("[action]")]
        public SyAprobacionniveles registrar([FromBody]SyAprobacionniveles syAprobacionniveles)
        {
            return syAprobacionnivelesServicio.registrar(_usuarioActual, syAprobacionniveles);
        }



        /* PENDIENTE-DARIO
        public List<DtoTabla> listarAplicacionPorUsuario(String idUsuario) { }
        public List<SyAprobacionniveles> listarPorAplicacionActivos(String idAplicacion) { }
        public Int32? obtenerNroNiveles(SyAprobacionnivelesPk pk) { }
        public void validarConfiguracionPorCompania(string proceso, string companiaSocioCodigo) { }
        public List<SyAprobacionniveles> obtenerPorCodigoProcesoCompaniaList(int proceso, string comania) { }
        
        */
    }
}
