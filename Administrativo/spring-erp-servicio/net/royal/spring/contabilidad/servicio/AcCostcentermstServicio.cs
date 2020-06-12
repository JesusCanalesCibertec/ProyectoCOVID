using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.contabilidad.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.contabilidad.servicio
{

    public interface AcCostcentermstServicio : GenericoServicio
    {
        AcCostcentermst obtenerPorId(AcCostcentermstPk pk);

        List<AcCostcentermst> listarTodos();

        String empleadoEsVendedorEnCentroCosto(Int32? idEmpleado);

        List<AcCostcentermst> listar(FiltroAcCostcentermst filtro);

        List<AcCostcentermst> listarActivos();

        ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion,
                FiltroPaginacionAcCostcentermst filtro);
        List<AcCostcentermst> listarBusqueda(DtoFiltro filtroPaginacionJefatura);
    }
}
