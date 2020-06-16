using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.rrhh.dominio.dtoscomponente;

namespace net.royal.spring.programasocial.dao
{

    public interface PsEntidadDao : GenericoDao<PsEntidad>
    {
        PsEntidad obtenerPorId(Nullable<Int32> pIdEntidad);
        PsEntidad coreActualizar(UsuarioActual usuarioActual, PsEntidad bean, String estado);
        PsEntidad coreInsertar(UsuarioActual usuarioActual, PsEntidad bean, String estado);
        void coreEliminar(PsEntidadPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad);
        PsEntidad coreAnular(UsuarioActual usuarioActual,PsEntidadPk id);
        PsEntidad coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pIdEntidad);
        int generarCodigo();
         ParametroPaginacionGenerico listarFurh(ParametroPaginacionGenerico paginacion, DtoComponenteFurh filtro);
        PsEntidad obtenerDatos(PsEntidad bean);
        List<DtoComponenteFurh> exportarFurh(DtoComponenteFurh filtro);
    }
}
