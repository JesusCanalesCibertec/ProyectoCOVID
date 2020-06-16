using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.planilla.dominio;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.planilla.dao
{
    public interface PrTipoProcesoDao : GenericoDao<PrTipoProceso>
    {
        List<PrTipoProceso> listarActivos();
        List<DtoTabla> listarPorPeriodoEmpleado(string periodo, int? personaId);
        List<PrTipoProceso> listarProcesosPrestamo();
    }
}
