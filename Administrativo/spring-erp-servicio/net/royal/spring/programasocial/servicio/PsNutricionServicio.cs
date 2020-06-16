using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio {

    public interface PsNutricionServicio : GenericoServicio {
        List<PsNutricion> consultar(FiltroNutricion filtro);
        ParametroPaginacionGenerico listarNutricion(ParametroPaginacionGenerico paginacion, FiltroNutricion filtro);
        ParametroPaginacionGenerico consultarNutricion(ParametroPaginacionGenerico paginacion, FiltroNutricion filtro);
        PsNutricion obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdNutricion);
        PsNutricion actualizar(UsuarioActual usuarioActual, PsNutricion bean);
        PsNutricion insertar(UsuarioActual usuarioActual, PsNutricion bean);
        PsNutricion guardar(UsuarioActual usuarioActual, PsNutricion bean);
        PsNutricion calcular(UsuarioActual usuarioActual, PsNutricion bean);
        DtoNutricion obtenerCalculos(DtoNutricion bean);
        PsNutricion anular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdNutricion);
        string Exportar(FiltroNutricion filtro);
    }
}
