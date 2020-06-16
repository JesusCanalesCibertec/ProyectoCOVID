using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao
{

    public interface BpProcesoconexionDao : GenericoDao<BpProcesoconexion>
    {
        List<BpProcesoconexion> listarPorProcesoVersionElementoEntradaUsuario(String idProceso, Int32 idVersion, String idElementoEntrada);
        List<BpProcesoconexion> listarPorProcesoVersionElementoEntradaUsuario(String idProceso, Int32 idVersion, String idElementoEntrada,
           UsuarioActual usuarioActual, Int32 idTransaccionSolicitante, Int32? idTransaccionResponsable, Int32? idTransaccionAprobador);
        BpProcesoconexion obtener(string idProceso, int? idVersion, BpMaeprocesoelementoPk bpMaeprocesoelementoPk1, BpMaeprocesoelementoPk bpMaeprocesoelementoPk2);
        List<BpProcesoconexion> listarPorElementoEntrada(BpMaeprocesoelementoPk bpMaeprocesoelementoPk);
        List<BpMaeprocesoelemento> listarElementoPorElementoEntrada(BpMaeprocesoelementoPk bpMaeprocesoelementoPk);


        /*ERNESTO*/
        Int32 obtenercodigo(string pIdProceso, Int32 pIdConexion, Int32 pIdVersion);

        string obtenercadena(string pIdProceso, int pIdVersion, string pAccion);
        List<DtoBpProcesoconexion> listado(string codigo);

        int generarCodigo(string pIdProceso);
        /*ERNESTO*/
    }
}
