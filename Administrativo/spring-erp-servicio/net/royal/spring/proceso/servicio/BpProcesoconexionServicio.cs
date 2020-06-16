using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpProcesoconexionServicio : GenericoServicio
    {
        DtoBpProcesoconexion listarTransacciones(Int32? idtransaccionpadre, int idTransaccion, String idProceso, UsuarioActual usuarioActual);
        String obtenerLeyendaPorProceso(string idProceso, UsuarioActual usuarioActual);
        void registrarNotificacionCorreo(BpProcesoconexion bpProcesoconexion, UsuarioActual usuarioActual);
        void validarBpStateMachineServicio(BpProcesoconexion bpProcesoconexion, UsuarioActual usuarioActual);
        BpTransaccionseguimiento registrarSeguimiento(BpProcesoconexion bpProcesoconexion, UsuarioActual usuarioActual);

        /*ERNESTO*/
        List<DtoBpProcesoconexion> listado(string codigo);
        BpProcesoconexion obtenerPorId(BpProcesoconexionPk llave);
        BpProcesoconexion coreActualizar(UsuarioActual usuarioActual, BpProcesoconexion bean);
        BpProcesoconexion coreInsertar(UsuarioActual usuarioActual, BpProcesoconexion bean);
        void eliminar(string pIdProceso, Int32 pIdConexion, Int32 pIdVersion);
        /*ERNESTO*/
    }
}
