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
    public interface BpMaeprocesoelementoDao : GenericoDao<BpMaeprocesoelemento>
    {
        List<BpMaeprocesoelemento> listarElementosIniciales(string idProceso);

        BpMaeprocesoelemento obtenerPorIdConfiguracion(BpMaeprocesoelementoPk bpMaeProcesoElementoPk,
            Int32? idTransaccionPadre, List<BpProcesoconexion> listaConexionesPermitidas);

        String obtenercadena(string pIdProceso, string pNombre);
        String obtenercodigo(string pIdProceso, string pIdelemento);
        List<DtoBpMaeprocesoelemento> listado(string codigo);
    }
}
