using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proyecto.dominio.filtro;

namespace net.royal.spring.proyecto.dao
{

    public interface PmTareaDao : GenericoDao<PmTarea>
    {
        PmTarea obtenerPorId(Nullable<Int32> pIdProyecto,Nullable<Int32> pIdTarea);
        PmTarea coreActualizar(UsuarioActual usuarioActual, PmTarea bean, String estado);
        PmTarea coreInsertar(UsuarioActual usuarioActual, PmTarea bean, String estado);
        void coreEliminar(PmTareaPk id);
        void coreEliminar(Nullable<Int32> pIdProyecto,Nullable<Int32> pIdTarea);
        PmTarea coreAnular(UsuarioActual usuarioActual,PmTareaPk id);
        PmTarea coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pIdProyecto,Nullable<Int32> pIdTarea);
        List<DtoTarea> listarTareaSegunGrupoProyecto(FiltroTarea filtro);
        int? generarIdTarea(int? idProyecto);
        PmTarea obtenerPorIdTransaccion(int value);
        List<DtoTarea> listarTareasNoFinalizadas(UsuarioActual usuarioActual, PmProyecto pmProyecto);
        List<DtoTarea> listarRegistrosValidos(UsuarioActual usuarioActual, PmTarea pmTarea);
        List<DtoTarea> listarTareasFinalizadasExitosamente(UsuarioActual usuarioActual, PmProyecto pmProyecto);
        List<PmTarea> listarPorProyecto(int? idProyecto);
        PmTarea obtenerPorIdTransaccionParaNotificacion(int idTransaccion);
        IList<PmTarea> listarTareasPorPersona(int value);
    }
}
