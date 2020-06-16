using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpProcesoconexioneventoServicio : GenericoServicio
    {
        List<DtoBpProcesoconexionevento> listado(string pIdProceso, int pIdConexion);
        BpProcesoconexionevento obtenerPorId(BpProcesoconexioneventoPk llave);
        BpProcesoconexionevento coreActualizar(UsuarioActual usuarioActual, BpProcesoconexionevento bean);
        BpProcesoconexionevento coreInsertar(UsuarioActual usuarioActual, BpProcesoconexionevento bean);
        void eliminar(string pIdProceso, int pIdVersion, int pIdConexion, string pIdEvento);
    }


}
