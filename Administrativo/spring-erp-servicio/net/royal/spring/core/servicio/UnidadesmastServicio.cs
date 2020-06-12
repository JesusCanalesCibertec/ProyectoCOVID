using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.servicio
{

    public interface UnidadesmastServicio : GenericoServicio {
        Unidadesmast obtenerPorId(UnidadesmastPk id);
        Unidadesmast obtenerPorId(String pUnidadcodigo);
        Unidadesmast coreActualizar(UsuarioActual usuarioActual, Unidadesmast bean);
        Unidadesmast coreInsertar(UsuarioActual usuarioActual, Unidadesmast bean);
        void coreEliminar(UnidadesmastPk id);
        void coreEliminar(String pUnidadcodigo);
        Unidadesmast coreAnular(UsuarioActual usuarioActual,UnidadesmastPk id);
        Unidadesmast coreAnular(UsuarioActual usuarioActual,String pUnidadcodigo);
        List<Unidadesmast> listarTodos();
    }
}
