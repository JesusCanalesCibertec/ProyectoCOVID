using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.contabilidad.dominio.filtro;

namespace net.royal.spring.contabilidad.dao
{

    public interface AcSucursalDao : GenericoDao<AcSucursal>
    {
        AcSucursal obtenerPorId(AcSucursalPk pk);
        List<AcSucursal> listarActivoOrdenadoPorNombre();
        List<AcSucursal> listarActivoOrdenadoPorCodigo();
        List<AcSucursal> listar(FiltroAcSucursal filtro);
    }
}
