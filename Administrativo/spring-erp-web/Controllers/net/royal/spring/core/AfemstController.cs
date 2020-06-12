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
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class AfemstController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AfemstServicio afemstServicio;
        public AfemstController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            afemstServicio = servicioProveedor.GetService<AfemstServicio>();
        }

        [HttpGet("[action]")]
        public List<Afemst> listarTodos()
        {
            return afemstServicio.listarTodos();
        }

        [HttpPost("[action]")]
        public List<Afemst> listar([FromBody]FiltroAfe filtro)
        {
            return afemstServicio.listar(filtro);
        }

        [HttpPost("[action]")]
        public Afemst obtenerporid([FromBody]AfemstPk pk)
        {
            return afemstServicio.obtenerporid(pk);
        }

        [HttpGet("[action]")]
        public List<Afemst> listarActivos()
        {
            return afemstServicio.listarActivos();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] DtoTabla filtro)
        {
            return afemstServicio.listarPaginacion(filtro, filtro.paginacion);
        }

    }
}
