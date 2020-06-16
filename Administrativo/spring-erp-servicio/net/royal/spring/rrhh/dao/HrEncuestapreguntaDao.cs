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

    public interface HrEncuestapreguntaDao : GenericoDao<HrEncuestapregunta>
    {
        HrEncuestapregunta obtenerPorId(Nullable<Int32> pPregunta);
        HrEncuestapregunta coreActualizar(UsuarioActual usuarioActual, HrEncuestapregunta bean, String estado);
        HrEncuestapregunta coreInsertar(UsuarioActual usuarioActual, HrEncuestapregunta bean, String estado);
        void coreEliminar(HrEncuestapreguntaPk id);
        void coreEliminar(Nullable<Int32> pPregunta);
        HrEncuestapregunta coreAnular(UsuarioActual usuarioActual,HrEncuestapreguntaPk id);
        HrEncuestapregunta coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pPregunta);

        ParametroPaginacionGenerico listarEncuestas(ParametroPaginacionGenerico paginacion, FiltroEncuestaPregunta filtro);
        int generarCodigo();
        List<HrEncuestadetalle> listarPorPlantilla(int id);

        string obtenerDescripcion(int pregunta);
    }
}
