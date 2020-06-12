using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.contabilidad.dao;
using net.royal.spring.contabilidad.servicio;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.contabilidad.dominio.filtro;

namespace net.royal.spring.contabilidad.servicio.impl
{

    public class AcSucursalServicioImpl : GenericoServicioImpl, AcSucursalServicio
    {

        private IServiceProvider servicioProveedor;
        private AcSucursalDao acSucursalDao;

        public AcSucursalServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            acSucursalDao = servicioProveedor.GetService<AcSucursalDao>();
        }

        public List<AcSucursal> listar(FiltroAcSucursal filtro)
        {
            return acSucursalDao.listar(filtro);
        }

        public List<AcSucursal> listarActivoOrdenadoPorCodigo()
        {
            return acSucursalDao.listarActivoOrdenadoPorCodigo();
        }

        public List<AcSucursal> listarActivoOrdenadoPorNombre()
        {
            return acSucursalDao.listarActivoOrdenadoPorNombre();
        }

        public List<AcSucursal> listarTodos()
        {
            return acSucursalDao.listarTodos();
        }

        AcSucursal obtenerPorId(AcSucursalPk acSucursalPk)
        {
            return acSucursalDao.obtenerPorId(acSucursalPk.obtenerArreglo());
        }

    }
}
