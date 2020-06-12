using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.web.controller;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh
{
    [Route("api/spring/rrhh/[controller]")]
    public class HrUnidadoperativaController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private HrUnidadoperativaServicio hrUnidadoperativaServicio;
        public HrUnidadoperativaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrUnidadoperativaServicio = servicioProveedor.GetService<HrUnidadoperativaServicio>();
        }

        [HttpGet("[action]")]
        public HrUnidadoperativa obtenerPorId(String Unidadoperativa)
        {
            HrUnidadoperativaPk pk = new HrUnidadoperativaPk();
            pk.Unidadoperativa = Unidadoperativa;
            return hrUnidadoperativaServicio.obtenerPorId(pk);
        }

        [HttpGet("[action]")]
        public List<HrUnidadoperativa> listarTodos()
        {
            return hrUnidadoperativaServicio.listarTodos();
        }

        [HttpPost("[action]")]
        public List<HrUnidadoperativa> listarBusqueda([FromBody]DtoFiltro filtroPaginacionJefatura)
        {
            return hrUnidadoperativaServicio.listarBusqueda(filtroPaginacionJefatura);
        }

        [HttpGet("[action]")]
        public List<HrUnidadoperativa> listarActivos()
        {
            return hrUnidadoperativaServicio.listarActivos();
        }

        [HttpGet("[action]")]
        public List<HrUnidadoperativa> listarJerarquico()
        {
            return hrUnidadoperativaServicio.listarJerarquico(_usuarioActual.Unidadoperativa);
        }
    }
}
