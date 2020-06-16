using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.dao
{

    public interface UnidadesmastDao : GenericoDao<Unidadesmast>
    {
        Unidadesmast obtenerPorId(String pUnidadcodigo);
        Unidadesmast coreActualizar(UsuarioActual usuarioActual, Unidadesmast bean, String estado);
        Unidadesmast coreInsertar(UsuarioActual usuarioActual, Unidadesmast bean, String estado);
        void coreEliminar(UnidadesmastPk id);
        void coreEliminar(String pUnidadcodigo);
        Unidadesmast coreAnular(UsuarioActual usuarioActual,UnidadesmastPk id);
        Unidadesmast coreAnular(UsuarioActual usuarioActual,String pUnidadcodigo);
    }
}
