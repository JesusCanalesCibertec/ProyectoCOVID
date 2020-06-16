using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.core.dao
{

    public interface PmPublicacionDao : GenericoDao<PmPublicacion>
    {
        IList<DtoTabla> listarPublicacionesDashboard();
        ParametroPaginacionGenerico listarBusqueda(FiltroPaginacionPublicaciones filtro);
        int? generarId(string idAplicacion);
        IList<DtoTabla> listarIndicadores(UsuarioActual usuarioActual);
    }
}
