using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio
{

    public interface BpMaeprocesoestadoServicio : GenericoServicio
    {
        List<BpMaeprocesoestado> listarEstadoPorProceso(String idProceso);

        List<DtoBpMaeprocesoestado> listado(string codigo);
        BpMaeprocesoestado obtenerPorId(BpMaeprocesoestadoPk llave);
        BpMaeprocesoestado coreActualizar(UsuarioActual usuarioActual, BpMaeprocesoestado bean);
        BpMaeprocesoestado coreInsertar(UsuarioActual usuarioActual, BpMaeprocesoestado bean);
        void eliminar(string pIdProceso, string pIdEstado);
    }
}
