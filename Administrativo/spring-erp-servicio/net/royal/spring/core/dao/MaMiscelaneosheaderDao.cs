using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.core.dao
{

    public interface MaMiscelaneosheaderDao : GenericoDao<MaMiscelaneosheader>
    {
        MaMiscelaneosheader obtenerPorId(MaMiscelaneosheaderPk pk);
   
        MaMiscelaneosheader obtenerPorId(Nullable<Int32> pPregunta);
        MaMiscelaneosheader coreActualizar(UsuarioActual usuarioActual, MaMiscelaneosheader bean, String estado);
        MaMiscelaneosheader coreInsertar(UsuarioActual usuarioActual, MaMiscelaneosheader bean, String estado);
        void coreEliminar(MaMiscelaneosheaderPk id);
        void coreEliminar(Nullable<Int32> pPregunta);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroMiscelaneosheader filtro);
        
    }
}
