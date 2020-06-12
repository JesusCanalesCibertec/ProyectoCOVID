


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.core.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net.royal.spring.framework.web.controller;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.core {
    [Route("api/spring/core/[controller]")]
    public class EmpleadomastController : SecuredBaseController {
        private IServiceProvider servicioProveedor;
        private EmpleadomastServicio empleadomastServicio;
        public EmpleadomastController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor) {
            servicioProveedor = _servicioProveedor;
            empleadomastServicio = servicioProveedor.GetService<EmpleadomastServicio>();
        }

        [HttpGet("[action]/{persona}/{compania}/{inicio}")]
        public string obtenerHorario(int persona, string compania, DateTime? inicio) {
            return empleadomastServicio.obtenerHorario(persona, compania, inicio);
        }

        [HttpGet("[action]")]
        public DtoEmpleadoBasico obtenerInformacionPorIdPersonaUsuarioActual() {
            return empleadomastServicio.obtenerInformacionPorIdPersonaA(_usuarioActual);
        }

        [HttpGet("[action]")]
        public DtoEmpleadoBasico obtenerInformacionPorIdPersona(string compania, int empleado) {
            UsuarioActual usuarioActual = new UsuarioActual();
            usuarioActual.PersonaId = empleado;
            usuarioActual.CompaniaSocioCodigo = compania;

            return empleadomastServicio.obtenerInformacionPorIdPersona(usuarioActual);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarBusqueda([FromBody]FiltroPaginacionEmpleado filtro) {
            return empleadomastServicio.listarBusqueda(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarCumpleanios([FromBody]FiltroPaginacionCumpleanio filtro) {
            filtro.compania = _usuarioActual.CompaniaSocioCodigo;
            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;
            return empleadomastServicio.listarCumpleanios(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarAniversarios([FromBody]FiltroPaginacionAniversario filtro) {
            filtro.compania = _usuarioActual.CompaniaSocioCodigo;
            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;
            return empleadomastServicio.listarAniversarios(filtro.paginacion, filtro);
        }

        [HttpGet("[action]")]
        public JsonResult obtenerFotoActual() {
            return Json(new {
                base64 = this.empleadomastServicio.obtenerFotoActual(_usuarioActual.PersonaId).ArchivoCadena
            });
        }

        [HttpGet("[action]")]
        public List<DtoTablaEntero> obtenerDiasPorMes(int nroMes) {
            return empleadomastServicio.obtenerDiasPorMes(nroMes);
        }

        /* PENDIENTE-DARIO
        public Empleadomast obtenerPorId(EmpleadomastPk empleadomastPk);

        public Empleadomast registrar(Empleadomast empleadomast);

        public Empleadomast actualizar(Empleadomast empleadomast);

        public void eliminar(EmpleadomastPk empleadomastPk);

        public String confirmarCargo(Int32? idEmpleado, Int32? codigoCargo);

        public Empleadomast obtenerPorCodigoUsuario(String codigousuario);

        public DtoEmpleadoBasico obtenerInformacionPorIdPersona(Int32? idPersona);


        public void fotoActualizar(Int32? idEmpleado, String numeroDocumento, DominioArchivo foto);

        public DtoEmpleado obtenerCompaniaActiva(int empleado);

        public List<DtoTabla> listarEmpresasActivasPorUsuario(string idUsuario);

        public ParametroPaginacionGenerico listarCumpleaniosConFoto(ParametroPaginacionGenerico paginacion, FiltroPaginacionCumpleanio filtro);

        public ParametroPaginacionGenerico listarAniversariosConFoto(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro);

        public DtoEmpleadoBasico obtenerInformacionPorIdPersona(int empleado, string companiaSocio);

        public ParametroPaginacionGenerico listarBusquedaUnidaOperativa(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtroPaginacionEmpleado);

        public ParametroPaginacionGenerico listarBusquedaPuesto(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtroPaginacionEmpleado);

        public string esFiscalizado(int persona);

        public bool esJefe(int? personaId);
        */
    }
}
