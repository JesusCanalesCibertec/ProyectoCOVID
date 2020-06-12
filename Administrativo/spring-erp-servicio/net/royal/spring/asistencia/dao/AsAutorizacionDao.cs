using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.sistema.dominio.filtro;

namespace net.royal.spring.asistencia.dao
{

    public interface AsAutorizacionDao : GenericoDao<AsAutorizacion>
    {
        int obtenerNumeroProceso();
        int contarCuponeras(Decimal empleado, DateTime fecha, String accion);
        bool validacionFechaIngresoNuevo(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta);
        bool validacionFechaIngresoEdicion(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta);
        AsAutorizacion obtenerPorId(AsAutorizacionPk pk);
        List<DtoPermisos> listarPermisos(DateTime? fechadesde, DateTime? fechahasta, Int32 empleado, String compania, String conceptoacceso, String estado, DateTime? fecregistro, String unidad, bool conJefe);
        List<DtoPermisosDetalleAutorizacion> listarPermisosDetalleAutorizacion(Int32 empleado, String conceptoacceso);
        AsAutorizacion solicitudActualizar(AsAutorizacion autorizacion);
        AsAutorizacion obtenerPorNumeroProceso(Int32 numeroproceso);
        List<DtoPermisosDetalleMarcas> listarPermisosDetalleMarcas(Int32 empleado, DateTime? fechadesde, DateTime? fechahasta);
        List<MensajeUsuario> solicitudValidarAccion(UsuarioActual usuarioActual, String accionSolicitada, Int32 numeroproceso);
        AsAutorizacion solicitudAnular(UsuarioActual usuarioActual, Int32 numeroproceso);
        void solicitudEliminar(UsuarioActual usuarioActual, Int32 numeroproceso);
        List<DtoTabla> listarCruces(AsAutorizacion autorizacion);
        AsAutorizacion registrarPermiso(AsAutorizacion autorizacion);
        AsAutorizacion obtenerPorAutogenerado(int autogenerado);
        String obtenerNoGeneraAsistencia(decimal empleado, String compania);
        bool validacionFechaCumpleanosEdicion(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta);
        bool validacionFechaCumpleanosNuevo(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta);
        ParametroPaginacionGenerico listarPermisosReporte(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro);
        bool contarCumpleanios(decimal? empleado, DateTime fechaComparar);
        bool contarCuponerasHorasLibres(decimal? empleado, DateTime fechaComparar);

        bool contarVacaciones(decimal? empleado, DateTime fechaComparar, DateTime fechaComparar2);

        Int32 obtenerAutorizacionPorId(AsAutorizacionPk pk);
        String obtenerComportamientoSobretiempo(Int32 empleado);
        AsAutorizacion obtenerPorDbPk(AsAutorizacionPk asAutorizacionPk);
        ParametroPaginacionGenerico listarSolicitudes(UsuarioActual usuarioActual, ParametroPaginacionGenerico paginacion,
             FiltroSolicitudes filtro);

        String obtenerCantidadSolicitudesParaAprobar();

        AsAutorizacion obtenerHorario(FiltroSolicitudes filtro);
    }
}
