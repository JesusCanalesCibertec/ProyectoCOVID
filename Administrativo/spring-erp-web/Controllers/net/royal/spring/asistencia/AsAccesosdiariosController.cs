using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.asistencia.dominio.filtro;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.core.servicio;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;

namespace net.royal.spring.asistencia
{
    [Route("api/spring/asistencia/[controller]")]
    public class AsAccesosdiariosController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AsAccesosdiariosServicio asAccesosdiariosServicio;
        private EmpleadomastServicio empleadomastServicio;

        public AsAccesosdiariosController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            asAccesosdiariosServicio = servicioProveedor.GetService<AsAccesosdiariosServicio>();
            empleadomastServicio = servicioProveedor.GetService<EmpleadomastServicio>();
        }

        [HttpPost("[action]")]
        public List<AsAccesosdiarios> obtenerMarcasPorDia([FromBody] AsAccesosdiarios filtros)
        {
            int? persona = null;
            if (filtros.Empleado.HasValue)
            {
                persona = Convert.ToInt32(filtros.Empleado.Value);
            }

            return asAccesosdiariosServicio.obtenerMarcasPorDia(persona.Value, filtros.compania, filtros.inicio, filtros.fin);

        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarMarcacionesConsolidado([FromBody]FiltroPaginacionEmpleado filtro)
        {

            string usuario = string.Empty;

            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;

            DtoEmpleadoBasico dtoEmpleado = empleadomastServicio.obtenerInformacionPorIdPersona(_usuarioActual);
            usuario = dtoEmpleado == null ? null : dtoEmpleado.CodigoUsuario;

            FiltroPaginacionEmpleado filtroPaginacionEmpleado = new FiltroPaginacionEmpleado()
            {
                IdUnidadOperativa = filtro.IdUnidadOperativa,
                EmpleadoId = filtro.EmpleadoId,
                fechaDesde = filtro.fechaDesde,
                fechaHasta = filtro.fechaHasta,
                EmpleadoJefe = UInteger.esCeroOrNulo(filtro.EmpleadoId) ? _usuarioActual.PersonaId : filtro.EmpleadoId,
                EmpleadoUsuario = UInteger.esCeroOrNulo(filtro.EmpleadoId) ? _usuarioActual.UsuarioLogin : usuario,
                IdCompaniaSocio = UInteger.esCeroOrNulo(filtro.EmpleadoId) ? _usuarioActual.CompaniaSocioCodigo : filtro.IdCompaniaSocio
            };

            setSessionData("filtroPaginacionEmpleado", filtroPaginacionEmpleado);

            return asAccesosdiariosServicio.listarMarcacionesConsolidado(filtro.paginacion, filtroPaginacionEmpleado);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarMarcaciones([FromBody]FiltroPaginacionEmpleado filtro)
        {

            string usuario = string.Empty;

            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;

            DtoEmpleadoBasico dtoEmpleado = empleadomastServicio.obtenerInformacionPorIdPersona(_usuarioActual);
            usuario = dtoEmpleado == null ? null : dtoEmpleado.CodigoUsuario;

            FiltroPaginacionEmpleado filtroPaginacionEmpleado = new FiltroPaginacionEmpleado()
            {
                IdUnidadOperativa = filtro.IdUnidadOperativa,
                EmpleadoId = filtro.EmpleadoId,
                fechaDesde = filtro.fechaDesde,
                fechaHasta = filtro.fechaHasta,
                EmpleadoJefe = UInteger.esCeroOrNulo(filtro.EmpleadoId) ? _usuarioActual.PersonaId : filtro.EmpleadoId,
                EmpleadoUsuario = UInteger.esCeroOrNulo(filtro.EmpleadoId) ? _usuarioActual.UsuarioLogin : usuario,
                IdCompaniaSocio = UInteger.esCeroOrNulo(filtro.EmpleadoId) ? _usuarioActual.CompaniaSocioCodigo : filtro.IdCompaniaSocio
            };

            setSessionData("filtroPaginacionEmpleadoMarcas", filtro);

            return asAccesosdiariosServicio.listarMarcaciones(filtro.paginacion, filtroPaginacionEmpleado);
        }

        [HttpGet("[action]")]
        public JsonResult exportarMarcas()
        {
            ParametroPaginacionGenerico paginacion = new ParametroPaginacionGenerico();
            paginacion.CantidadRegistrosDevolver = 10000;
            paginacion.RegistroInicio = 0;
            FiltroPaginacionEmpleado filtroPaginacionEmpleado = getSessionData<FiltroPaginacionEmpleado>("filtroPaginacionEmpleadoMarcas");
            filtroPaginacionEmpleado.paginacion = paginacion;
            return Json(new { url = asAccesosdiariosServicio.ExportarMarcas(filtroPaginacionEmpleado) });
        }

        [HttpGet("[action]")]
        public JsonResult exportarAsistencias()
        {
            ParametroPaginacionGenerico paginacion = new ParametroPaginacionGenerico();
            paginacion.CantidadRegistrosDevolver = 10000;
            paginacion.RegistroInicio = 0;
            FiltroPaginacionEmpleado filtroPaginacionEmpleado = getSessionData<FiltroPaginacionEmpleado>("filtroPaginacionEmpleado");
            filtroPaginacionEmpleado.paginacion = paginacion;
            return Json(new { url = asAccesosdiariosServicio.ExportarAsistencia(filtroPaginacionEmpleado) });
        }


        /* PENDIENTE-DARIO
        DtoMarcaciones traerMarcas(FiltroPaginacionEmpleado filtroPaginacionEmpleado);
        List<DtoMarcaciones> listarMarcaciones(DateTime fechadesde, DateTime fechahasta, int empleado);
        List<DtoMarcaciones> listarMarcacionesConsolidado(DateTime fechadesde, DateTime fechahasta, int empleado);
        DtoMarcaciones traerMarcas(DateTime fechadesde, DateTime fechahasta, int empleado);
        void registrar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios);
        ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion, FiltroAsAccesosDiarios filtroAsAccesosDiarios);
        void actualizar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios);

        */
    }
}
