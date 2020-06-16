using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrEncuestapreguntavaloresDao : GenericoDao<HrEncuestapreguntavalores>
    {
        HrEncuestapreguntavalores obtenerPorId(Nullable<Int32> pPregunta, Nullable<Int32> pValor);
        HrEncuestapreguntavalores coreActualizar(UsuarioActual usuarioActual, HrEncuestapreguntavalores bean);
        HrEncuestapreguntavalores coreInsertar(UsuarioActual usuarioActual, HrEncuestapreguntavalores bean);
        void coreEliminar(HrEncuestapreguntavaloresPk id);
        void coreEliminar(Nullable<Int32> pPregunta, Nullable<Int32> pValor);
        IList<DtoTabla> obtenerValores(HrEncuestapreguntaPk hrEncuestapreguntaPk);
        void EliminarDetalle(int? pregunta);
        List<HrEncuestapreguntavalores> listarValores(int? numero);
    }
}
