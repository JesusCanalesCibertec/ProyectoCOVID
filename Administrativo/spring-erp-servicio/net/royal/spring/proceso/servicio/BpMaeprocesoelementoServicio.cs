using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpMaeprocesoelementoServicio : GenericoServicio
    {
        BpMaeprocesoelemento obtenerPorId(BpMaeprocesoelementoPk pk);
        List<DtoBpMaeprocesoelemento> listado(string codigo);
        BpMaeprocesoelemento coreActualizar(UsuarioActual usuarioActual, BpMaeprocesoelemento bean);
        BpMaeprocesoelemento coreInsertar(UsuarioActual usuarioActual, BpMaeprocesoelemento bean);
        void eliminar(string pIdProceso, string pIdelemento);

        List<BpMaeprocesoelemento> listaSeg();
    }
}
