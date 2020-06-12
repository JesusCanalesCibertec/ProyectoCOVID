using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.filtro;

namespace net.royal.spring.proyecto.servicio
{

    public interface PmProyectoServicio : GenericoServicio {
        PmProyecto obtenerPorId(PmProyectoPk id);
        PmProyecto obtenerPorId(Nullable<Int32> pIdPortafolio,Nullable<Int32> pIdPrograma,Nullable<Int32> pIdProyecto);
        PmProyecto coreActualizar(UsuarioActual usuarioActual, PmProyecto bean);
        PmProyecto coreInsertar(UsuarioActual usuarioActual, PmProyecto bean);
        void coreEliminar(PmProyectoPk id);
        void coreEliminar(Nullable<Int32> pIdPortafolio,Nullable<Int32> pIdPrograma,Nullable<Int32> pIdProyecto);
        PmProyecto coreAnular(UsuarioActual usuarioActual,PmProyectoPk id);
        PmProyecto coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pIdPortafolio,Nullable<Int32> pIdPrograma,Nullable<Int32> pIdProyecto);
        ParametroPaginacionGenerico listarPaginacion(FiltroPaginacionProyecto filtro);
        PmProyecto registrarProyecto(PmProyecto pmProyecto, UsuarioActual usuarioActual);
        PmProyecto actualizarProyecto(PmProyecto pmProyecto, UsuarioActual usuarioActual);
        PmProyecto obtenerPorIdProyecto(PmProyectoPk pmProyectoPk);
        PmProyecto obtenerPorIdTransaccionParaNotificacion(int idTransaccion);
    }
}
