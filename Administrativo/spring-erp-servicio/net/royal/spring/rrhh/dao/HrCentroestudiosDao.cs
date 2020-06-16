using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCentroestudiosDao : GenericoDao<HrCentroestudios>
    {
        HrCentroestudios obtenerPorId(Nullable<Int32> pCentro);
        HrCentroestudios coreActualizar(UsuarioActual usuarioActual, HrCentroestudios bean);
        HrCentroestudios coreInsertar(UsuarioActual usuarioActual, HrCentroestudios bean);
        void coreEliminar(HrCentroestudiosPk id);
        void coreEliminar(Nullable<Int32> pCentro);
        ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionCentroEstudio filtro);
    }
}
