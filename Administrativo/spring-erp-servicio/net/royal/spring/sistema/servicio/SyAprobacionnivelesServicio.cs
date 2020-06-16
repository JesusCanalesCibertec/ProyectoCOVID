using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.filtro;

namespace net.royal.spring.sistema.servicio
{

    public interface SyAprobacionnivelesServicio : GenericoServicio {
        SyAprobacionniveles obtenerPorIdCompleto(SyAprobacionnivelesPk syAprobacionnivelesPk);
        List<DtoTabla> listarAplicacionPorUsuario(String idUsuario);
        List<SyAprobacionniveles> listar(FiltroNivelAprobacion filtro);
        List<SyAprobacionniveles> listarPorAplicacionActivos(String idAplicacion);

        SyAprobacionniveles registrar(UsuarioActual usuarioActual, SyAprobacionniveles syAprobacionniveles);
        void actualizar(UsuarioActual usuarioActual, SyAprobacionniveles syAprobacionniveles);
        void eliminar(UsuarioActual usuarioActual, Int32? codigo, string compania);
        Int32? obtenerNroNiveles(SyAprobacionnivelesPk pk);
        void validarConfiguracionPorCompania(string pROCESO_PLANILLA_PRESTAMO, string companiaSocioCodigo);
        List<SyAprobacionniveles> obtenerPorCodigoProcesoCompaniaList(int proceso, string comania);
        SyAprobacionniveles obtenerPorCodigoProcesoCompania(int proceso, string comania);
    }
}
