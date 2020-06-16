using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsConsumoPlantillaServicio : GenericoServicio {
        PsConsumoPlantilla obtenerPorId(PsConsumoPlantillaPk id);
        PsConsumoPlantilla obtenerPorId(String pIdInstitucion,int pPlantilla);
        PsConsumoPlantilla coreActualizar(UsuarioActual usuarioActual, PsConsumoPlantilla bean);
        PsConsumoPlantilla coreInsertar(UsuarioActual usuarioActual, PsConsumoPlantilla bean);
        void coreEliminar(PsConsumoPlantillaPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> plantilla);
        List<DtoConsumoPlantilla> listarPlantilla(DtoTabla filtro);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        List<PsConsumoPlantilla> listarPlantillas(string institucion);
        ParametroPaginacionGenerico listarPlantillasConsumo(DtoTabla filtro, ParametroPaginacionGenerico paginacion);
        ParametroPaginacionGenerico listarPlantilla(ParametroPaginacionGenerico paginacion, DtoTabla filtro);

        PsConsumoPlantilla coreAnular(UsuarioActual usuarioActual, PsConsumoPlantillaPk id);
        PsConsumoPlantilla coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pPlantilla);
    }
}
