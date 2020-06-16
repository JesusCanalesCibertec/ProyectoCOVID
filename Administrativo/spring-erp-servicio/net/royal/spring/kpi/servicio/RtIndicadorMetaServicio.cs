using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.kpi.dominio;

namespace net.royal.spring.kpi.servicio
{

    public interface RtIndicadorMetaServicio : GenericoServicio {
        RtIndicadorMeta obtenerPorId(RtIndicadorMetaPk id);
        RtIndicadorMeta obtenerPorId(String pIdIndicador,Nullable<Int32> pIdMeta);
        RtIndicadorMeta coreActualizar(UsuarioActual usuarioActual, RtIndicadorMeta bean);
        RtIndicadorMeta coreAnular(UsuarioActual usuarioActual, RtIndicadorMetaPk id);
        RtIndicadorMeta coreAnular(UsuarioActual usuarioActual, String pIdIndicador, Nullable<Int32> pIdMeta);
        RtIndicadorMeta coreInsertar(UsuarioActual usuarioActual, RtIndicadorMeta bean);
        void coreEliminar(RtIndicadorMetaPk id);
        void coreEliminar(String pIdIndicador,Nullable<Int32> pIdMeta);

        List<RtIndicadorMeta> listado(string pIdIndicador);
    }
}
