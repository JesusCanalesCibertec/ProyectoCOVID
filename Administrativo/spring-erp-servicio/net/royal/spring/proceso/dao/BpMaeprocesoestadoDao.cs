using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao
{

    public interface BpMaeprocesoestadoDao : GenericoDao<BpMaeprocesoestado>
    {
        List<BpMaeprocesoestado> listarEstadoPorProceso(String idProceso);

        String obtenercadena(string pIdProceso, string pNombre);
        String obtenercodigo(string pIdProceso, string pIdEstado);
        List<DtoBpMaeprocesoestado> listado(string codigo);
    }
}
