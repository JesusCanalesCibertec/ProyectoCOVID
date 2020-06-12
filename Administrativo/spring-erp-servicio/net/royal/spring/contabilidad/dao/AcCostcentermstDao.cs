using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.contabilidad.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.contabilidad.dao
{

public interface AcCostcentermstDao : GenericoDao<AcCostcentermst>
{
        AcCostcentermst obtenerPorId(AcCostcentermstPk pk);

        String empleadoEsVendedorEnCentroCosto(Int32? idEmpleado);

        ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion,
                FiltroPaginacionAcCostcentermst filtro);

        List<AcCostcentermst> listar(FiltroAcCostcentermst filtro);

        String obtenerNombrePorPk(AcCostcentermstPk acCostcentermstPk);
        List<AcCostcentermst> listarBusqueda(DtoFiltro filtroPaginacionJefatura);
    }
}
