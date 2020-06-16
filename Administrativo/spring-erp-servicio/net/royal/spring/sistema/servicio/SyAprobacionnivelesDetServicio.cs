using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.servicio
{

    public interface SyAprobacionnivelesDetServicio : GenericoServicio
    {
        List<SyAprobacionnivelesDet> listarPorNivelAprobacion(SyAprobacionnivelesPk syAprobacionnivelesPk);
        List<SyAprobacionnivelesDet> obtenerListaCorreoPorProceso(Int32? procesoAprobar, Int32? nivelAprobar);

        List<SyAprobacionnivelesDet> listarPorCodigoNivel(Int32 codigo, Int32 nivel, String compania);
    }
}
