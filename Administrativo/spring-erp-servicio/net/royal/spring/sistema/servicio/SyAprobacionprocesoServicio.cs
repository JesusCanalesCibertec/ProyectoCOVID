using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.dominio.filtro;

namespace net.royal.spring.sistema.servicio
{

    public interface SyAprobacionprocesoServicio : GenericoServicio
    {
        List<SyAprobacionproceso> listarCodigoProcesoPorCodigoProcesoBase(String codigoProcesoBase);
        Int32 obtenerCodigoProcesoAprobacion(String codigoProcesoBase);
        List<SyAprobacionproceso> listarPorAplicacion(String idAplicacion);
        List<SyAprobacionproceso> listarPorAplicacionActivos(String idAplicacion);
        List<SyAprobacionproceso> listar(FiltroAprobacionProceso filtro);
        ParametroPaginacionGenerico listarSolicitudes(UsuarioActual usuarioActual, ParametroPaginacionGenerico paginacion,
                FiltroSolicitudes filtro);
        List<DtoSolicitud> solicitudEventoPreparar(UsuarioActual usuarioActual, String accion, List<DtoSolicitud> listaSolicitudes);
        List<DtoSolicitud> solicitudEventoEjecutar(UsuarioActual usuarioActual, String accion, List<DtoSolicitud> listaSolicitudes);

        void enviarCorreoPorTransaccion(UsuarioActual usuarioActual,
            DtoSolicitud bean,
            String accion);

        DtoSolicitud obtenerSolicitud(int? codigoProceso, int? numeroProceso, UsuarioActual usuarioActual);
        void SincronizarHorarios(string companiaSocioCodigo);
    }
}
