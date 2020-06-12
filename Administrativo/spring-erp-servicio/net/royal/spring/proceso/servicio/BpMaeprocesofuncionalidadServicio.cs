using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpMaeprocesofuncionalidadServicio : GenericoServicio
    {
        List<DtoBpMaeprocesofuncionalidad> listado(string codigo);
        BpMaeprocesofuncionalidad obtenerPorId(BpMaeprocesofuncionalidadPk llave);
        BpMaeprocesofuncionalidad coreActualizar(UsuarioActual usuarioActual, BpMaeprocesofuncionalidad bean);
        BpMaeprocesofuncionalidad coreInsertar(UsuarioActual usuarioActual, BpMaeprocesofuncionalidad bean);
        void eliminar(string pIdProceso, string pIdfuncionalidad);
    }
}
