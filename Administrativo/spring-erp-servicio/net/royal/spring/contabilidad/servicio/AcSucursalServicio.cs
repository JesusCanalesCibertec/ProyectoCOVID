using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.contabilidad.dominio.filtro;

namespace net.royal.spring.contabilidad.servicio
{

    public interface AcSucursalServicio : GenericoServicio
    {
        List<AcSucursal> listarTodos();

        List<AcSucursal> listar(FiltroAcSucursal filtro);

        List<AcSucursal> listarActivoOrdenadoPorNombre();

        List<AcSucursal> listarActivoOrdenadoPorCodigo();

    }
}
