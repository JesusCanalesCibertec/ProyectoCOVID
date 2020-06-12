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

    public interface PsItemDao : GenericoDao<PsItem>
    {
        String obtenerCadenaxCodigo(string pIdItem);
        String obtenercadena(string pNombre);
       
        String obtenercodigo(string pIdItem);
        PsItem obtenerPorId(String pIdItem);
        PsItem coreActualizar(UsuarioActual usuarioActual, PsItem bean, String estado);
        PsItem coreInsertar(UsuarioActual usuarioActual, PsItem bean, String estado);
        void coreEliminar(PsItemPk id);
        void coreEliminar(String pIdItem);
        PsItem coreAnular(UsuarioActual usuarioActual,PsItemPk id);
        PsItem coreAnular(UsuarioActual usuarioActual,String pIdItem);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroItem filtro);

        string obtenerDescripcion(string pIdItem);
    }
}
