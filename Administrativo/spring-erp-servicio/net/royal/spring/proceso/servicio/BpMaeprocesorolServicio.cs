using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpMaeprocesorolServicio : GenericoServicio
    {
        List<BpMaeprocesorol> listado(string codigo);
        BpMaeprocesorol obtenerPorId(BpMaeprocesorolPk llave);
        BpMaeprocesorol coreActualizar(UsuarioActual usuarioActual, BpMaeprocesorol bean);
        BpMaeprocesorol coreInsertar(UsuarioActual usuarioActual, BpMaeprocesorol bean);
        void eliminar(string pIdProceso, string pIdRol);
        
    }
}
