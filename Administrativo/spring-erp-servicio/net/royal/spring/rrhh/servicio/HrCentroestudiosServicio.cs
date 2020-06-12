using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrCentroestudiosServicio : GenericoServicio {
        HrCentroestudios obtenerPorId(HrCentroestudiosPk id);
        HrCentroestudios obtenerPorId(Nullable<Int32> pCentro);
        HrCentroestudios coreActualizar(UsuarioActual usuarioActual, HrCentroestudios bean);
        HrCentroestudios coreInsertar(UsuarioActual usuarioActual, HrCentroestudios bean);
        void coreEliminar(HrCentroestudiosPk id);
        void coreEliminar(Nullable<Int32> pCentro);
        ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionCentroEstudio filtro);
    }
}
