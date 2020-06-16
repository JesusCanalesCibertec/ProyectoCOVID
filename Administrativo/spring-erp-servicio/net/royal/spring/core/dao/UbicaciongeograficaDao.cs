using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.dominio;

namespace net.royal.spring.dao
{

    public interface UbicaciongeograficaDao : GenericoDao<Ubicaciongeografica>
    {
        Ubicaciongeografica obtenerPorId(String pUbigeo);
        Ubicaciongeografica coreActualizar(UsuarioActual usuarioActual, Ubicaciongeografica bean, String estado);
        Ubicaciongeografica coreInsertar(UsuarioActual usuarioActual, Ubicaciongeografica bean, String estado);
        void coreEliminar(UbicaciongeograficaPk id);
        void coreEliminar(String pUbigeo);
        Ubicaciongeografica coreAnular(UsuarioActual usuarioActual,UbicaciongeograficaPk id);
        Ubicaciongeografica coreAnular(UsuarioActual usuarioActual,String pUbigeo);
    }
}
