using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.dominio.filtro;
using Newtonsoft.Json.Linq;

namespace net.royal.spring.asistencia
{
    [Route("api/spring/asistencia/[controller]")]
    public class AsAutorizacionController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AsAutorizacionServicio asAutorizacionServicio;
        public AsAutorizacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            asAutorizacionServicio = servicioProveedor.GetService<AsAutorizacionServicio>();
        }

        [HttpPost("[action]")]
        public List<DtoPermisos> listarPermisos([FromBody] DtoPermisos filtro)
        {
            return asAutorizacionServicio.listarPermisos(filtro.fechadesde, filtro.fechahasta, _usuarioActual.PersonaId.Value, filtro.compania, filtro.conceptoacceso, filtro.estado, filtro.fechaRegistro, filtro.unidad, UBoolean.validarFlag(filtro.esJefe));
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarSolicitudes([FromBody] FiltroSolicitudes filtro)
        {

            filtro.IdPersonaActual = _usuarioActual.PersonaId;
            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;
            return asAutorizacionServicio.listarSolicitudes(_usuarioActual, filtro.paginacion, filtro);
        }

        [HttpGet("[action]")]
        public AsAutorizacion obtenerPorNumeroProceso(Int32 numeroproceso)
        {
            return asAutorizacionServicio.obtenerPorNumeroProceso(numeroproceso);
        }

        [HttpPost("[action]")]
        public AsAutorizacion obtenerHorario([FromBody] FiltroSolicitudes filtro)
        {
            filtro.Empleado = _usuarioActual.PersonaId.Value;
            filtro.CompaniaSocio = _usuarioActual.CompaniaSocioCodigo;
            return asAutorizacionServicio.obtenerHorario(filtro);
        }

        [HttpGet("[action]")]
        public List<MensajeUsuario> solicitudValidarAccion(String accionSolicitada, Int32 numeroproceso)
        {
            return asAutorizacionServicio.solicitudValidarAccion(_usuarioActual, accionSolicitada, numeroproceso);
        }


        [HttpGet("[action]")]
        public List<DtoPermisosDetalleAutorizacion> listarPermisosDetalleAutorizacion(Int32 empleado, String conceptoacceso)
        {
            return asAutorizacionServicio.listarPermisosDetalleAutorizacion(empleado, conceptoacceso);
        }

        [HttpPost("[action]")]
        public AsAutorizacion solicitudRegistrar([FromBody]AsAutorizacion autorizacion)
        {
            return asAutorizacionServicio.solicitudRegistrar(autorizacion, _usuarioActual);
        }

        [HttpGet("[action]")]
        public AsAutorizacion obtenerSolicitudPorLlave(string llave)
        {
            return asAutorizacionServicio.obtenerSolicitudPorLlave(llave);
        }

        [HttpPost("[action]")]
        public AsAutorizacion obtenerValidacionPorConcepto([FromBody]AsAutorizacion autorizacion)
        {
            return asAutorizacionServicio.obtenerValidacionPorConcepto(autorizacion);
        }

        [HttpGet("[action]")]
        public AsAutorizacion solicitudAnular(Int32 numeroProceso)
        {
            return asAutorizacionServicio.solicitudAnular(_usuarioActual, numeroProceso);
        }

        [HttpGet("[action]")]
        public String obtenerCantidadSolicitudesParaAprobar()
        {
            return asAutorizacionServicio.obtenerCantidadSolicitudesParaAprobar();
        }


        [HttpPost("[action]")]
        public AsAutorizacion solicitudEliminar([FromBody] DtoSolicitud bean)
        {
            AsAutorizacion obj = new AsAutorizacion();
            asAutorizacionServicio.solicitudEliminar(_usuarioActual, bean.ProcesoNro.Value);
            return obj;
        }

        [HttpPost("[action]")]
        public AsAutorizacion solicitudActualizar([FromBody]AsAutorizacion autorizacion)
        {
            return asAutorizacionServicio.solicitudActualizar(autorizacion);
        }


        [HttpGet("[action]")]
        public JsonResult obtenerComportamientoSobretiempo(Int32 empleado)
        {
            return Json(new { valor = asAutorizacionServicio.obtenerComportamientoSobretiempo(empleado) });
        }


        /* PENDIENTE-DARIO
        AsAutorizacion obtenerPorId(AsAutorizacionPk pk);
        AsAutorizacion obtenerPorAutogenerado(int autogenerado);
        
        bool validacionFechaIngreso(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta, Int32 tipo, string accion, UsuarioActual usuario);
        ParametroPaginacionGenerico listarPermisosReporte(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro);
        */
    }
}
