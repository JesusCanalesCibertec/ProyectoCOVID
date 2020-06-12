
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dominio.filtro;
using Newtonsoft.Json.Linq;
using net.royal.spring.sistema.dominio.dtorest;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SyAprobacionprocesoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SyAprobacionprocesoServicio syAprobacionprocesoServicio;
        public SyAprobacionprocesoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            syAprobacionprocesoServicio = servicioProveedor.GetService<SyAprobacionprocesoServicio>();
        }

        [HttpGet("[action]/{codigoProcesoBase}")]
        public Int32 obtenerCodigoProcesoAprobacion(String codigoProcesoBase)
        {
            return syAprobacionprocesoServicio.obtenerCodigoProcesoAprobacion(codigoProcesoBase);
        }

        [HttpGet("[action]/{codigoProcesoBase}")]
        public List<SyAprobacionproceso> listarCodigoProcesoPorCodigoProcesoBase(String codigoProcesoBase)
        {
            return syAprobacionprocesoServicio.listarCodigoProcesoPorCodigoProcesoBase(codigoProcesoBase);
        }

        [HttpPost("[action]")]
        public void enviarCorreoPorTransaccion(JObject jsonData)
        {
            dynamic json = jsonData;
            JObject jbean = json.bean;

            DtoSolicitud bean = jbean.ToObject<DtoSolicitud>(); ;
            String accion = json.accion;
            syAprobacionprocesoServicio.enviarCorreoPorTransaccion(_usuarioActual, bean, accion);
        }

        [HttpPost("[action]/{codigoProceso}/{numeroProceso}")]
        public DtoSolicitud obtenerSolicitud(int? codigoProceso, int? numeroProceso)
        {
            return syAprobacionprocesoServicio.obtenerSolicitud(codigoProceso, numeroProceso, _usuarioActual);
        }

        [HttpPost("[action]")]
        public List<DtoSolicitud> solicitudEventoEjecutar([FromBody]DtoRestSolicitudLista dto)
        {
            String accion = dto.accion;
            List<DtoSolicitud> listaSolicitudes = dto.listaSolicitudes;
            return syAprobacionprocesoServicio.solicitudEventoEjecutar(_usuarioActual, accion, listaSolicitudes);
        }

        [HttpPost("[action]")]
        public List<DtoSolicitud> solicitudEventoPreparar([FromBody]DtoRestSolicitudLista dto)
        {
            String accion = dto.accion;
            List<DtoSolicitud> listaSolicitudes = dto.listaSolicitudes;
            return syAprobacionprocesoServicio.solicitudEventoPreparar(_usuarioActual, accion, listaSolicitudes);
        }

        [HttpGet("[action]/{idAplicacion}")]
        public List<SyAprobacionproceso> listarPorAplicacion(String idAplicacion)
        {
            return syAprobacionprocesoServicio.listarPorAplicacion(idAplicacion);
        }

        [HttpGet("[action]/{idAplicacion}")]
        public List<SyAprobacionproceso> listarPorAplicacionActivos(String idAplicacion)
        {
            return syAprobacionprocesoServicio.listarPorAplicacionActivos(idAplicacion);
        }

        [HttpPost("[action]")]
        public List<SyAprobacionproceso> listar([FromBody]FiltroAprobacionProceso filtro)
        {
            return syAprobacionprocesoServicio.listar(filtro);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarSolicitudes([FromBody]FiltroSolicitudes filtro)
        {

            filtro.IdPersonaActual = _usuarioActual.PersonaId;
            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;
            return syAprobacionprocesoServicio.listarSolicitudes(_usuarioActual, filtro.paginacion, filtro);
        }

        /* PENDIENTE-DARIO        
        void SincronizarHorarios(string companiaSocioCodigo);
        */
    }
}
