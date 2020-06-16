using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEncuestapreguntavaloresServicio : GenericoServicio {
        HrEncuestapreguntavalores obtenerPorId(HrEncuestapreguntavaloresPk id);
        HrEncuestapreguntavalores obtenerPorId(Nullable<Int32> pPregunta,Nullable<Int32> pValor);
        HrEncuestapreguntavalores coreActualizar(UsuarioActual usuarioActual, HrEncuestapreguntavalores bean);
        HrEncuestapreguntavalores coreInsertar(UsuarioActual usuarioActual, HrEncuestapreguntavalores bean);
        void coreEliminar(HrEncuestapreguntavaloresPk id);
        void coreEliminar(Nullable<Int32> pPregunta,Nullable<Int32> pValor);
        List<HrEncuestapreguntavalores> listarValores(int? numero);
    }
}
