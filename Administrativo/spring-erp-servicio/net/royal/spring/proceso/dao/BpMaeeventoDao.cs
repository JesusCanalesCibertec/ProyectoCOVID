using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.dao
{
    public interface BpMaeeventoDao : GenericoDao<BpMaeevento>
    {
        List<BpMaeevento> listarEventosPorConexion(BpProcesoconexionPk bpProcesoconexionPk, string idTipoEvento);
        MensajeUsuario ejecutarEvento(BpMaeevento item, List<ParametroPersistenciaGenerico> parametros);
    }
}
