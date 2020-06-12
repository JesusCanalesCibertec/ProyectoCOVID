using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsEmpleadoServicio : GenericoServicio {
        PsEmpleado obtenerPorId(PsEmpleadoPk id);
        PsEmpleado obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdEmpleado);
        PsEmpleado coreActualizar(UsuarioActual usuarioActual, PsEmpleado bean);
        PsEmpleado coreInsertar(UsuarioActual usuarioActual, PsEmpleado bean);
        void coreEliminar(PsEmpleadoPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdEmpleado);
        PsEmpleado coreAnular(UsuarioActual usuarioActual,PsEmpleadoPk id);
        PsEmpleado coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdEmpleado);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPsEmpleado filtro);
        IList<DtoTabla> listarAreas();
    }
}
