using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.interfaz;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.sistema.dominio.filtro;

namespace net.royal.spring.asistencia.servicio
{

    public interface AsAutorizacionServicio : GenericoServicio, SyAccionInterfaz
    {
        List<MensajeUsuario> solicitudValidarAccion(UsuarioActual usuarioActual, string accionSolicitada, AsAutorizacion bean);
        List<DtoPermisos> listarPermisos(DateTime? fechadesde, DateTime? fechahasta, Int32 empleado, String compania, String conceptoacceso, String estado, DateTime? fecregistro, String unidad, bool esJefe);
        List<DtoPermisosDetalleAutorizacion> listarPermisosDetalleAutorizacion(Int32 empleado, String conceptoacceso);
        AsAutorizacion solicitudRegistrar(AsAutorizacion autorizacion, UsuarioActual usuarioActual);
        AsAutorizacion solicitudActualizar(AsAutorizacion autorizacion);
        AsAutorizacion obtenerPorNumeroProceso(Int32 numeroproceso);
        List<DtoPermisosDetalleMarcas> listarPermisosDetalleMarcas(Int32 empleado, DateTime? fechadesde, DateTime? fechahasta);
        List<MensajeUsuario> solicitudValidarAccion(UsuarioActual usuarioActual, String accionSolicitada, Int32 numeroproceso);
        AsAutorizacion solicitudAnular(UsuarioActual usuarioActual, Int32 numeroproceso);
        void solicitudEliminar(UsuarioActual usuarioActual, Int32 numeroproceso);
        AsAutorizacion obtenerPorId(AsAutorizacionPk pk);
        AsAutorizacion obtenerPorAutogenerado(int autogenerado);
        AsAutorizacion obtenerSolicitudPorLlave(string llave);
        AsAutorizacion obtenerValidacionPorConcepto(AsAutorizacion autorizacion);
        bool validacionFechaIngreso(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta, Int32 tipo, string accion, UsuarioActual usuario);
        ParametroPaginacionGenerico listarPermisosReporte(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro);
        Int32 obtenerAutorizacionPorId(AsAutorizacionPk pk);
        String obtenerComportamientoSobretiempo(Int32 empleado);
        String obtenerCantidadSolicitudesParaAprobar();
        ParametroPaginacionGenerico listarSolicitudes(UsuarioActual usuarioActual, ParametroPaginacionGenerico paginacion,
                      FiltroSolicitudes filtro);
        AsAutorizacion obtenerHorario(FiltroSolicitudes filtro);
    }
}
