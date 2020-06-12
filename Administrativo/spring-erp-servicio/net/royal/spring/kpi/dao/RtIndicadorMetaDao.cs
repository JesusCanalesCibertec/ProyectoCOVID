using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.kpi.dominio;

namespace net.royal.spring.kpi.dao
{

    public interface RtIndicadorMetaDao : GenericoDao<RtIndicadorMeta>
    {
        RtIndicadorMeta obtenerPorId(String pIdIndicador,Nullable<Int32> pIdMeta);
        RtIndicadorMeta coreActualizar(UsuarioActual usuarioActual, RtIndicadorMeta bean);

        RtIndicadorMeta coreAnular(UsuarioActual usuarioActual, RtIndicadorMetaPk id);
        RtIndicadorMeta coreAnular(UsuarioActual usuarioActual, String pIdIndicador, Nullable<Int32> pIdMeta);
        RtIndicadorMeta coreInsertar(UsuarioActual usuarioActual, RtIndicadorMeta bean);
        void coreEliminar(RtIndicadorMetaPk id);
        void coreEliminar(String pIdIndicador,Nullable<Int32> pIdMeta);
        List<RtIndicadorMeta> listado(string pIdIndicador);

        int generarCodigo();
    }
}
