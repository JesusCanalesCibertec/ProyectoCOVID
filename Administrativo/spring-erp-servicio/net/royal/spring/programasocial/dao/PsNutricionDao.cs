using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao {

    public interface PsNutricionDao : GenericoDao<PsNutricion> {
        PsNutricion obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdNutricion);
        PsNutricion coreActualizar(UsuarioActual usuarioActual, PsNutricion bean, String estado);
        PsNutricion coreInsertar(UsuarioActual usuarioActual, PsNutricion bean, String estado);
        void coreEliminar(PsNutricionPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdNutricion);
        PsNutricion coreAnular(UsuarioActual usuarioActual, PsNutricionPk id);
        PsNutricion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdNutricion);
        ParametroPaginacionGenerico listarNutricion(ParametroPaginacionGenerico paginacion, FiltroNutricion filtro);
        List<PsNutricion> consultar(FiltroNutricion filtro);
        int generarCodigo();
        DtoNutricion obtenerCalculos(DtoNutricion bean);
        ParametroPaginacionGenerico consultarNutricion(ParametroPaginacionGenerico paginacion, FiltroNutricion filtro);
    }
}
