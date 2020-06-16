using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.contabilidad.dao;
using net.royal.spring.contabilidad.servicio;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.contabilidad.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.contabilidad.servicio.impl
{

    public class AcCostcentermstServicioImpl : GenericoServicioImpl, AcCostcentermstServicio
    {

        private IServiceProvider servicioProveedor;
        private AcCostcentermstDao acCostcentermstDao;

        public AcCostcentermstServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            acCostcentermstDao = servicioProveedor.GetService<AcCostcentermstDao>();
        }

        public string empleadoEsVendedorEnCentroCosto(int? idEmpleado)
        {
            return acCostcentermstDao.empleadoEsVendedorEnCentroCosto(idEmpleado);
        }

        public List<AcCostcentermst> listar(FiltroAcCostcentermst filtro)
        {
            return acCostcentermstDao.listar(filtro);
        }

        public List<AcCostcentermst> listarActivos()
        {
            FiltroAcCostcentermst filtro = new FiltroAcCostcentermst();
            filtro.Estado = "A";
            return listar(filtro);
        }

        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionAcCostcentermst filtro)
        {
            return acCostcentermstDao.listarBusqueda(paginacion, filtro);
        }

        public List<AcCostcentermst> listarBusqueda(DtoFiltro filtroPaginacionJefatura)
        {
            return acCostcentermstDao.listarBusqueda(filtroPaginacionJefatura);
        }

        public List<AcCostcentermst> listarTodos()
        {
            return acCostcentermstDao.listarTodos();
        }

        public AcCostcentermst obtenerPorId(AcCostcentermstPk pk)
        {
            return acCostcentermstDao.obtenerPorId(pk);
        }
    }
}
