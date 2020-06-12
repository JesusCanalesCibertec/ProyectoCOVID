using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proyecto.dominio.filtro;

namespace net.royal.spring.proyecto.servicio
{

    public interface PmTareaServicio : GenericoServicio
    {
        PmTarea obtenerPorId(PmTareaPk id);
        PmTarea obtenerPorId(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea);
        PmTarea coreActualizar(UsuarioActual usuarioActual, PmTarea bean);
        PmTarea coreInsertar(UsuarioActual usuarioActual, PmTarea bean);
        void coreEliminar(PmTareaPk id);
        void coreEliminar(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea);
        PmTarea coreAnular(UsuarioActual usuarioActual, PmTareaPk id);
        PmTarea coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea);
        List<DtoTarea> listarTareaSegunGrupoProyecto(FiltroTarea filtro);
        PmTarea actualizarTareaIper(PmTarea pmTarea, UsuarioActual usuarioActual);
        PmTarea guardarTareaIper(PmTarea pmTarea, UsuarioActual usuarioActual);
        PmTarea obtenerPorIdTarea(PmTareaPk pmTareaPk);
        PmTarea obtenerPorIdTransaccionParaNotificacion(int idTransaccion);
        IList<PmTarea> listarTareasPorPersona(int? personaId);
        void asignarResponsable(UsuarioActual usuarioActual, List<DtoTarea> lista);
        void generarPermisos(UsuarioActual usuarioActual, List<DtoTarea> lista);
        ParametroPaginacionGenerico listarPlantillasTarea(DtoTabla filtro, ParametroPaginacionGenerico paginacion);
        List<PmPlantilla> listarPlantillas();
        void agregarPlantilla(UsuarioActual usuarioActual, DtoProyecto dto);
    }
}
