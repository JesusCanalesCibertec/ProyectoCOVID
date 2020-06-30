using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.covid.servicio;
using net.royal.spring.covid.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using net.royal.spring.covid.dominio.filtro;

namespace net.royal.spring.covid
{
    [Route("api/spring/covid/[controller]")]
    public class   CiudadanoController:BaseController
    {
        private IServiceProvider servicioProveedor;
        private CiudadanoServicio ciudadanoServicio;

        public CiudadanoController(IServiceProvider _servicioProveedor , IHttpContextAccessor httpContextAccessor):base(httpContextAccessor, _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            ciudadanoServicio = servicioProveedor.GetService<CiudadanoServicio>();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] FiltroCiudadano filtro)
        {
            return ciudadanoServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public Ciudadano Registrar([FromBody]Ciudadano bean)
        {
            return ciudadanoServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public Ciudadano ObtenerPorId(int pIdCiudadano)
        {
            return ciudadanoServicio.obtenerPorId(pIdCiudadano);
        }

        [HttpPost("[action]")]
        public Ciudadano Actualizar([FromBody]Ciudadano bean)
        {
            return ciudadanoServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public CiudadanoPk Cambiarestado([FromBody]CiudadanoPk pk)
        {
            return ciudadanoServicio.cambiarestado(pk);
        }

        [HttpGet("[action]")]
        public List<Ciudadano> listado()
        {
            return ciudadanoServicio.listado();

        }

        [HttpGet("[action]")]
        public List<DtoTabla> ListarPiexDepartamento(string pDepa)
        {
            return ciudadanoServicio.listarPiexDepartamento(pDepa);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> ListarPiexProvincia(string pDepa, string pProv)
        {
            return ciudadanoServicio.listarPiexProvincia(pDepa, pProv);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> ListarPiexDistrito(string pDepa, string pProv, string pDist)
        {
            return ciudadanoServicio.listarPiexDistrito(pDepa, pProv, pDist);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> ListarPie()
        {
            return ciudadanoServicio.ListarPie();
        }
    }
}
