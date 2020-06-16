using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEncuestaServicio : GenericoServicio {
        HrEncuesta obtenerPorId(HrEncuestaPk id);
        HrEncuesta obtenerPorId(String pCompanyowner,String pPeriodo,Nullable<Int32> pSecuencia);
        HrEncuesta coreActualizar(UsuarioActual usuarioActual, HrEncuesta bean);
        HrEncuesta coreInsertar(UsuarioActual usuarioActual, HrEncuesta bean);
        void coreEliminar(HrEncuestaPk id);
        void coreEliminar(String pCompanyowner,String pPeriodo,Nullable<Int32> pSecuencia);
        ParametroPaginacionGenerico listar(FiltroPaginacionEncuesta filtroPaginacion);
        DtoEncuesta obtenerPlantilla(HrEncuestaPk hrEncuestaPk);
        DtoEncuesta solicitudRegistrar(DtoEncuesta dtoEncuesta, UsuarioActual usuarioActual);
        ParametroPaginacionGenerico listarSujetos(FiltroPaginacionSujeto filtroPaginacion);
        DtoEncuesta obtenerEncuesta(HrEncuestaPk hrEncuestaPk, int? pSujeto);
        DtoEncuesta solicitudActualizar(DtoEncuesta dtoEncuesta, UsuarioActual usuarioActual);
        HrEncuesta obtenerCompleto(HrEncuestaPk bean);
        HrEncuesta solicitudRegistrarBean(HrEncuesta bean, UsuarioActual usuarioActual);
        HrEncuesta solicitudActualizarBean(HrEncuesta bean, UsuarioActual usuarioActual);
        DtoEncuesta actualizarEjecucion(DtoEncuesta dtoEncuesta, UsuarioActual usuarioActual);
        DtoEncuesta anularEncuesta(DtoEncuesta dtoEncuesta, UsuarioActual usuarioActual);
        List<DtoEncuestaAnalisis> analizarEncuesta(FiltroEncuestaAnalisis filtro);
        String exportarEncuesta(FiltroEncuestaAnalisis filtro);
        DtoEncuesta anularSujeto(DtoEncuestaSujeto dto);
        DtoEncuesta copiar(HrEncuestaPk pk);
        ParametroPaginacionGenerico consulta(FiltroPaginacionEncuesta filtroPaginacion);
    }
}
