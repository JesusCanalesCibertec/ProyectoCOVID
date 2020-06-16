using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCapacitacionServicio : GenericoServicio {
        HrCapacitacion obtenerPorId(HrCapacitacionPk id);
        HrCapacitacion obtenerPorId(String pCompanyowner,String pCapacitacion);
        HrCapacitacion coreActualizar(UsuarioActual usuarioActual, HrCapacitacion bean);
        HrCapacitacion coreInsertar(UsuarioActual usuarioActual, HrCapacitacion bean);
        void coreEliminar(HrCapacitacionPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion);
        HrCapacitacion coreAnular(UsuarioActual usuarioActual,HrCapacitacionPk id);
        HrCapacitacion coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion);
        /*ALEJANDRO INICIO*/
        ParametroPaginacionGenerico solicitudListar(ParametroPaginacionGenerico paginacion, FiltroPaginacionCapacitacion filtro);
        HrCapacitacion solicitudRegistrar(UsuarioActual usuarioActual, HrCapacitacion hrCapacitacion);
        HrCapacitacion solicitudActualizar(UsuarioActual usuarioActual, HrCapacitacion hrCapacitacion);
        HrCapacitacion solicitudObtenerPorId(HrCapacitacionPk hrCapacitacionPk);
        DtoTabla descargarAdjunto(HrCapacitacionFoliosPk pk);
        void eliminarFolio(HrCapacitacionFolios pk);
        /*ALEJANDRO INICIO*/

        List<DtoPrevencionSalud> listarPrevencionSaludCabecera(FiltroGraficos filtro);
        List<DtoPrevencionSalud> listarPrevencionSaludDetalle(FiltroGraficos filtro);
        String Exportar(HrCapacitacionPk pk);
    }
}
