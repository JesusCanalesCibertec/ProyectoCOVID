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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SyUnidadreplicacionController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SyUnidadreplicacionServicio syUnidadreplicacionServicio;
        public SyUnidadreplicacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            syUnidadreplicacionServicio = servicioProveedor.GetService<SyUnidadreplicacionServicio>();
        }

        [HttpGet("[action]/{unidadReplicacion}")]
        public SyUnidadreplicacion obtenerPorId(String unidadReplicacion) {
            SyUnidadreplicacionPk pk = new SyUnidadreplicacionPk();
            pk.Unidadreplicacion = unidadReplicacion;
            return syUnidadreplicacionServicio.obtenerPorId(pk);
        }

        [HttpGet("[action]")]
        public List<SyUnidadreplicacion> listarTodos() {
            return syUnidadreplicacionServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<SyUnidadreplicacion> listarActivos() {
            return syUnidadreplicacionServicio.listarActivos();
        }

        [HttpPost("[action]")]
        public List<SyUnidadreplicacion> listar([FromBody]DtoFiltro filtro) {
            return syUnidadreplicacionServicio.listar(filtro);
        }
    }
}
