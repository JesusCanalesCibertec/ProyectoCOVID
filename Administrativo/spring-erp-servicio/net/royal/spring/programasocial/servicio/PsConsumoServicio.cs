using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsConsumoServicio : GenericoServicio {

        PsConsumo obtenerPorId(PsConsumoPk id);

        PsConsumo solicitarObtenerporid(PsConsumoPk llave);
        PsConsumo coreActualizar(UsuarioActual usuarioActual, PsConsumo bean);
        PsConsumo coreInsertar(UsuarioActual usuarioActual, PsConsumo bean);
        void coreEliminar(PsConsumoPk id);
        void coreEliminar(String pIdInstitucion, Int32? pIdConsumo);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroConsumo filtro);
        string Exportar(DtoConsumo bean);
        PsConsumo coreAnular(UsuarioActual usuarioActual, PsConsumoPk id);
        PsConsumo coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdConsumo);
    }
}
