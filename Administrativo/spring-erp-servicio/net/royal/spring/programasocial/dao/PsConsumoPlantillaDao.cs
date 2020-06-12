using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsConsumoPlantillaDao : GenericoDao<PsConsumoPlantilla>
    {
        PsConsumoPlantilla obtenerPorId(String pIdInstitucion, Nullable<Int32> plantilla);
        PsConsumoPlantilla coreActualizar(UsuarioActual usuarioActual, PsConsumoPlantilla bean);
        PsConsumoPlantilla coreInsertar(UsuarioActual usuarioActual, PsConsumoPlantilla bean);
        void coreEliminar(PsConsumoPlantillaPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> plantilla);
        List<DtoConsumoPlantilla> listarPlantilla(DtoTabla filtro);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);

        int generarCodigo(string institucion);

        void eliminardetalle(string institucion, int? plantilla);
        List<PsConsumoPlantilla> listarPlantillas(string institucion);
        ParametroPaginacionGenerico listarPlantilla(ParametroPaginacionGenerico paginacion, DtoTabla filtro);

        PsConsumoPlantilla coreAnular(UsuarioActual usuarioActual, PsConsumoPlantillaPk id);
        PsConsumoPlantilla coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdPlantilla);
    }
}
