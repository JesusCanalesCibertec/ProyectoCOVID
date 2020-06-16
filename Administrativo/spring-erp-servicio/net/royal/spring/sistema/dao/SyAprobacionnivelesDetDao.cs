using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.dao
{

    public interface SyAprobacionnivelesDetDao : GenericoDao<SyAprobacionnivelesDet>
    {
        List<SyAprobacionnivelesDet> listarPorCodigoNivel(Int32 codigo, Int32 nivel, String compania);
        List<SyAprobacionnivelesDet> listarPorNivelAprobacion(SyAprobacionnivelesPk syAprobacionnivelesPk);
        List<SyAprobacionnivelesDet> obtenerListaCorreoPorProceso(Int32? procesoAprobar, Int32? nivelAprobar);
        SyAprobacionnivelesDet obtenerPorId(SyAprobacionnivelesDetPk pk);
        List<SyAprobacionnivelesDet> listarPorCodigo(int codigo, string compania);

    }
}
