using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEncuestapreguntaServicio : GenericoServicio {
        HrEncuestapregunta obtenerPorId(HrEncuestapreguntaPk id);
        HrEncuestapregunta obtenerPorId(Nullable<Int32> pPregunta);
        HrEncuestapregunta coreActualizar(UsuarioActual usuarioActual, HrEncuestapregunta bean);
        HrEncuestapregunta coreInsertar(UsuarioActual usuarioActual, HrEncuestapregunta bean);
        void coreEliminar(HrEncuestapreguntaPk id);
        void coreEliminar(Nullable<Int32> pPregunta);
        HrEncuestapregunta coreAnular(UsuarioActual usuarioActual,HrEncuestapreguntaPk id);
        HrEncuestapregunta coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pPregunta);
        ParametroPaginacionGenerico listarEncuestas(ParametroPaginacionGenerico paginacion, FiltroEncuestaPregunta filtro);
        HrEncuestapregunta registrarPregunta(UsuarioActual usuarioActual, HrEncuestapregunta bean);
        HrEncuestapregunta solicitudObtenerPorId(Nullable<Int32> pk);
        HrEncuestapregunta actualizarPregunta(UsuarioActual usuarioActual, HrEncuestapregunta bean);
        void eliminarPregunta(int? pregunta);
    }
}
