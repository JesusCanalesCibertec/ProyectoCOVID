using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.salud.dominio;
using net.royal.spring.salud.dominio.dto;

namespace net.royal.spring.salud.dao
{

    public interface SsGediagnosticoDao : GenericoDao<SsGediagnostico>
    {
        SsGediagnostico obtenerPorId(int codigo);
        SsGediagnostico coreActualizar(UsuarioActual usuarioActual, SsGediagnostico bean, Int32? estado);
        SsGediagnostico coreInsertar(UsuarioActual usuarioActual, SsGediagnostico bean, Int32? estado);
        void coreEliminar(SsGediagnosticoPk id);
        void coreEliminar(Nullable<Int32> codigo);
       
       
		
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroDiagnostico filtro);
        SsGediagnostico registrarCurso(UsuarioActual usuarioActual, SsGediagnostico bean);

        SsGediagnostico obtenerNombrePorCodigo(string codigo);
        String obtenercadena(string cadena);
        List<SsGediagnostico> listarActivosFlgCronicos();

        SsGediagnostico coreAnular(UsuarioActual usuarioActual, SsGediagnosticoPk id);
        SsGediagnostico coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdDiagnostico);


    }
}
