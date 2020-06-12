using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.dominio;

namespace net.royal.spring.servicio
{

    public interface UbicaciongeograficaServicio : GenericoServicio {
        Ubicaciongeografica obtenerPorId(UbicaciongeograficaPk id);
        Ubicaciongeografica obtenerPorId(String pUbigeo);
        Ubicaciongeografica coreActualizar(UsuarioActual usuarioActual, Ubicaciongeografica bean);
        Ubicaciongeografica coreInsertar(UsuarioActual usuarioActual, Ubicaciongeografica bean);
        void coreEliminar(UbicaciongeograficaPk id);
        void coreEliminar(String pUbigeo);
        Ubicaciongeografica coreAnular(UsuarioActual usuarioActual,UbicaciongeograficaPk id);
        Ubicaciongeografica coreAnular(UsuarioActual usuarioActual,String pUbigeo);
    }
}
