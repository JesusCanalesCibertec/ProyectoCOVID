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

    public class AcSucursalgrupoServicioImpl : GenericoServicioImpl, AcSucursalgrupoServicio
    {

        private IServiceProvider servicioProveedor;
        private AcSucursalgrupoDao acSucursalgrupoDao;

        public AcSucursalgrupoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            acSucursalgrupoDao = servicioProveedor.GetService<AcSucursalgrupoDao>();
        }

        /* public List<AcSucursalgrupo> listar(FiltroAcSucursalGrupo filtro)
         {
             return acSucursalgrupoDao.listar(filtro);
         }

         public List<AcSucursalgrupo> listarActivoOrdenadoPorCodigo()
         {
             return acSucursalgrupoDao.listarActivoOrdenadoPorCodigo();
         }

         public List<AcSucursalgrupo> listarActivoOrdenadoPorNombre()
         {
             return acSucursalgrupoDao.listarActivoOrdenadoPorNombre();
         }

         public List<AcSucursalgrupo> listarTodos()
         {
             return acSucursalgrupoDao.listarTodos();
         }*/
    }
}
