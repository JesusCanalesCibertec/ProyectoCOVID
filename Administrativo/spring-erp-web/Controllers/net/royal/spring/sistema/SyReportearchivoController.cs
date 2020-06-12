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
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SyReportearchivoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SyReportearchivoServicio syReportearchivoServicio;
        public SyReportearchivoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            syReportearchivoServicio = servicioProveedor.GetService<SyReportearchivoServicio>();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPorPaginacion([FromBody] DtoSyReporte filtro)
        {
            return syReportearchivoServicio.listarPorPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public SyReporteArchivo obtenerPorId([FromBody]SyReportearchivoPk pk)
        {
          return syReportearchivoServicio.obtenerPorId(pk);
        }

        [HttpPost("[action]")]
        public SyReporteArchivo registrar([FromBody]SyReporteArchivo bean)
        {
            return syReportearchivoServicio.registrarReporteArchivo(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public SyReporteArchivo actualizar([FromBody]SyReporteArchivo bean)
        {
            return syReportearchivoServicio.actualizarReporteArchivo(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public void eliminarReporteArchivo([FromBody]SyReportearchivoPk pk)
        {
             syReportearchivoServicio.eliminarReporteArchivo(pk);
        }

      

        /* PENDIENTE-DARIO
        ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion, SyReportearchivo filtro);
        void registrarReporteArchivo(UsuarioActual usuarioActual, SyReportearchivo syReportearchivo);
        void actualizarReporteArchivo(UsuarioActual usuarioActual, SyReportearchivo syReportearchivo);
        
        SyReportearchivo obtenerPorId(SyReportearchivoPk pk);*/
    }
}
