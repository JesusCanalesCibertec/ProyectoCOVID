using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using net.royal.spring.minedu.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net.royal.spring.minedu
{
    [Route("api/spring/minedu/[controller]")]
    public class   MpContratacionController:SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private MpContratacionServicio mpContratacionServicio;

        public MpContratacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor):base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            mpContratacionServicio = servicioProveedor.GetService<MpContratacionServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] DtoTabla filtro)
        {
            return mpContratacionServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public MpContratacion Registrar([FromBody]MpContratacion bean)
        {
            return mpContratacionServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public MpContratacion ObtenerPorId(int pIdContratacion)
        {
            return mpContratacionServicio.obtenerPorId(pIdContratacion);
        }


        [HttpPost("[action]")]
        public MpContratacion Actualizar([FromBody]MpContratacion bean)
        {
            return mpContratacionServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MpContratacionPk Cambiarestado([FromBody]MpContratacionPk pk)
        {
            return mpContratacionServicio.cambiarestado(pk);
        }

        [HttpPost("[action]")]
        public MpContratacion Registrarlista([FromBody]MpContratacion bean)
        {
            return mpContratacionServicio.registrarlista(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public List<DtoContratacion> Contratosactivos()
        {
            return mpContratacionServicio.Contratosactivos();
        }

        [HttpPost("[action]")]
        public List<DtoContratacion> ListarPersonaldisponible([FromBody] DtoTabla filtro)
        {
            return mpContratacionServicio.ListarPersonaldisponible(filtro);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPersonaldisponible1([FromBody] DtoTabla filtro)
        {
            return mpContratacionServicio.ListarPersonaldisponible1(filtro.paginacion, filtro);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> ListarPie()
        {
            return mpContratacionServicio.ListarPie();
        }

        [HttpGet("[action]")]
        public List<DtoContratacion> ListarContratoPorPersona(int pIdPersona)
        {
            return mpContratacionServicio.ListarContratoPorPersona(pIdPersona);
        }


    }
}
