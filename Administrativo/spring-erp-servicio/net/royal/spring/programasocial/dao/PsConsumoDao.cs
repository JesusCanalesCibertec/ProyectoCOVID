using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsConsumoDao : GenericoDao<PsConsumo>
    {
        PsConsumo obtenerPorId(String pIdInstitucion, Int32? pIdConsumo);
        PsConsumo coreActualizar(UsuarioActual usuarioActual, PsConsumo bean, String estado);
        PsConsumo coreInsertar(UsuarioActual usuarioActual, PsConsumo bean, String estado);
        void coreEliminar(PsConsumoPk id);
        void coreEliminar(String pIdInstitucion, Int32? pIdConsumo);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroConsumo filtro);
        int generarCodigo(String pIdInstitucion);
        PsConsumo coreAnular(UsuarioActual usuarioActual, PsConsumoPk id);
        PsConsumo coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdConsumo);
    }
}
