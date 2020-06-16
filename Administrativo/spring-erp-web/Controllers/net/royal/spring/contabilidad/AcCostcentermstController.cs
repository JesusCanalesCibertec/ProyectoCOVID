using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.contabilidad.dominio.filtro;
using net.royal.spring.contabilidad.servicio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;

namespace royal.spring.contabilidad
{
    [Route("api/spring/contabilidad/[controller]")]
    public class AcCostcentermstController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AcCostcentermstServicio acCostcentermstServicio;
        public AcCostcentermstController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            acCostcentermstServicio = servicioProveedor.GetService<AcCostcentermstServicio>();
        }

        [HttpGet("[action]")]
        public AcCostcentermst obtenerPorId(String Costcenter)
        {
            AcCostcentermstPk pk = new AcCostcentermstPk();
            pk.Costcenter = Costcenter;
            return acCostcentermstServicio.obtenerPorId(pk);
        }

        [HttpGet("[action]")]
        public List<AcCostcentermst> listarTodos()
        {
            return acCostcentermstServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public String empleadoEsVendedorEnCentroCosto(Int32? idEmpleado)
        {
            return acCostcentermstServicio.empleadoEsVendedorEnCentroCosto(idEmpleado);
        }

        [HttpGet("[action]")]
        public List<AcCostcentermst> listarActivos()
        {
            return acCostcentermstServicio.listarActivos();
        }

        [HttpPost("[action]")]
        public List<AcCostcentermst> listar([FromBody]FiltroAcCostcentermst filtro)
        {
            return acCostcentermstServicio.listar(filtro);
        }

        [HttpPost("[action]")]
        public List<AcCostcentermst> listarBusqueda([FromBody]DtoFiltro filtroPaginacionJefatura)
        {
            return acCostcentermstServicio.listarBusqueda(filtroPaginacionJefatura);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarBusquedas([FromBody]FiltroPaginacionAcCostcentermst filtro)
        {
            return acCostcentermstServicio.listarBusqueda(filtro.paginacion, filtro);
        }

    }
}

