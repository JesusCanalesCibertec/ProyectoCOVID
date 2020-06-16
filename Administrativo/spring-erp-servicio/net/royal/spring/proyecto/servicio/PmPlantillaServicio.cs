using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface PmPlantillaServicio : GenericoServicio {
        PmPlantilla obtenerPorId(PmPlantillaPk id);
        PmPlantilla obtenerPorId(int pPlantilla);
        PmPlantilla coreActualizar(UsuarioActual usuarioActual, PmPlantilla bean);
        PmPlantilla coreInsertar(UsuarioActual usuarioActual, PmPlantilla bean);
        void coreEliminar(PmPlantillaPk id);
        void coreEliminar(Nullable<Int32> pPlantilla);
        PmPlantilla coreAnular(UsuarioActual usuarioActual,PmPlantillaPk id);
        PmPlantilla coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pPlantilla);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
   
    }
}
