using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpMaeprocesoelementofuncionalidadServicio : GenericoServicio
    {
        List<DtoBpMaeprocesoelementofuncionalidad> listado(string pIdProceso, string pIdElemento);
        BpMaeprocesoelementofuncionalidad obtenerPorId(BpMaeprocesoelementofuncionalidadPk llave);
        BpMaeprocesoelementofuncionalidad coreActualizar(UsuarioActual usuarioActual, BpMaeprocesoelementofuncionalidad bean);
        BpMaeprocesoelementofuncionalidad coreInsertar(UsuarioActual usuarioActual, BpMaeprocesoelementofuncionalidad bean);
        void eliminar(string pIdProceso, string pIdElemento, string pIdFuncionalidad);
    }

    
}
