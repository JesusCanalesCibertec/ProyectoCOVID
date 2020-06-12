using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio;
using net.royal.spring.planilla.dominio;
using net.royal.spring.sistema.constante;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.web.servicio;

namespace net.royal.spring.planilla.servicio
{
    public interface PrTipoProcesoServicio : GenericoServicio
    {
        List<PrTipoProceso> listarActivos();
        List<DtoTabla> listarPorPeriodoEmpleado(string periodo, int? personaId);
        List<PrTipoProceso> listarProcesosPrestamo();
    }
}
