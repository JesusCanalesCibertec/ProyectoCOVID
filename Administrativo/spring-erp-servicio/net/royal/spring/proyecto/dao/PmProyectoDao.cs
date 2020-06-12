using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.filtro;

namespace net.royal.spring.proyecto.dao
{

    public interface PmProyectoDao : GenericoDao<PmProyecto>
    {
        PmProyecto obtenerPorId(Nullable<Int32> pIdPortafolio,Nullable<Int32> pIdPrograma,Nullable<Int32> pIdProyecto);
        PmProyecto coreActualizar(UsuarioActual usuarioActual, PmProyecto bean, String estado);
        PmProyecto coreInsertar(UsuarioActual usuarioActual, PmProyecto bean, String estado);
        void coreEliminar(PmProyectoPk id);
        void coreEliminar(Nullable<Int32> pIdPortafolio,Nullable<Int32> pIdPrograma,Nullable<Int32> pIdProyecto);
        PmProyecto coreAnular(UsuarioActual usuarioActual,PmProyectoPk id);
        PmProyecto coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pIdPortafolio,Nullable<Int32> pIdPrograma,Nullable<Int32> pIdProyecto);
        ParametroPaginacionGenerico listarPaginacion(FiltroPaginacionProyecto filtro);
        int? generarIdProyecto();
        PmProyecto obtenerPorIdProyecto(PmProyectoPk pmProyectoPk);
        PmProyecto obtenerPorIdTransaccion(int idTransaccion);
        PmProyecto obtenerPorIdTransaccionParaNotificacion(int idTransaccion);
    }
}
