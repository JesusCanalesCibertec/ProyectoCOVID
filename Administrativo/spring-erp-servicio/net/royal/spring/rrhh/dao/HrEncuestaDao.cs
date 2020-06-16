using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.dao {

    public interface HrEncuestaDao : GenericoDao<HrEncuesta> {
        HrEncuesta obtenerPorId(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia);
        HrEncuesta coreActualizar(UsuarioActual usuarioActual, HrEncuesta bean);
        HrEncuesta coreInsertar(UsuarioActual usuarioActual, HrEncuesta bean);
        void coreEliminar(HrEncuestaPk id);
        void coreEliminar(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia);
        ParametroPaginacionGenerico listar(FiltroPaginacionEncuesta filtroPaginacion);
        ParametroPaginacionGenerico listarSujetos(FiltroPaginacionSujeto filtroPaginacion);
        int? generarSecuencia(HrEncuestaPk hrEncuestaPk);
        List<DtoEncuestaAnalisis> analizarEncuesta(FiltroEncuestaAnalisis filtro);
        ParametroPaginacionGenerico consulta(FiltroPaginacionEncuesta filtroPaginacion);
    }
}
