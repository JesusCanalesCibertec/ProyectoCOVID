using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.constante;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.proceso.dominio;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SyReporteController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SyReporteServicio syReporteServicio;
        public SyReporteController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            syReporteServicio = servicioProveedor.GetService<SyReporteServicio>();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPorPaginacion([FromBody] DtoSyReporte filtro)
        {
            return syReporteServicio.listarPorPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public SyReporte obtenerPorId([FromBody]SyReportePk bean)
        {
            SyReportePk pk = new SyReportePk();
            pk.Aplicacioncodigo = bean.Aplicacioncodigo;
            pk.Reportecodigo = bean.Reportecodigo;

            return syReporteServicio.obtenerPorId(pk);
        }

        [HttpPost("[action]")]
        public SyReporte registrar([FromBody]SyReporte bean)
        {
            return syReporteServicio.registrarReporte(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public SyReporte actualizar([FromBody]SyReporte bean)
        {
            return syReporteServicio.actualizar(bean);
        }

        [HttpGet("[action]")]
        public List<SyReporte> listar()
        {
            return syReporteServicio.listar();
        }


        /* PENDIENTE-DARIO
        String ejecutarReporteComoUrlTemporal(UsuarioActual usuarioctual, DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros);
        byte[] ejecutarReporte(DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros);
        List<Email> generarListaCorreo(DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros, List<DtoCorreo> listaCorreos);
        string obtenerImagenComoCadena(string compania, ConstanteReporte.TipoImagen tipoImagen, string periodo, string tipoRecurso);
     
      
        void actualizarReporte(UsuarioActual usuarioActual, SyReporte bean);
        void actualizar(SyReporte recurso);
        string crearReporteUrl(string ruta, Byte[] archivo);
        string[] obtenerTextoReporte(SyReportearchivoPk pk);*/
    }
}
