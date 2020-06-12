using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.core.servicio
{

    public interface PmPublicacionServicio : GenericoServicio
    {
        IList<DtoTabla> listarPublicacionesDashboard();
        ParametroPaginacionGenerico listarBusqueda(FiltroPaginacionPublicaciones filtro);
        PmPublicacion obtenerPorid(PmPublicacionPk pk);
        PmPublicacion registrar(PmPublicacion bean, UsuarioActual usuarioActual);
        PmPublicacion actualizar(PmPublicacion bean, UsuarioActual usuarioActual);
        IList<DtoTabla> listarIndicadores(UsuarioActual usuarioActual);
    }
}
