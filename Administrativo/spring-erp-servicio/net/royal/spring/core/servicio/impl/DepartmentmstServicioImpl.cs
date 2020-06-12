using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio.impl
{

    public class DepartmentmstServicioImpl : GenericoServicioImpl, DepartmentmstServicio
    {

        private IServiceProvider servicioProveedor;
        private DepartmentmstDao departmentmstDao;

        public DepartmentmstServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            departmentmstDao = servicioProveedor.GetService<DepartmentmstDao>();
        }

        public List<Departmentmst> listar(DtoFiltro filtro)
        {
            return departmentmstDao.listar(filtro);
        }

        public List<Departmentmst> listarActivos()
        {
            DtoFiltro filtro = new DtoFiltro();
            filtro.Estado = "A";
            return listar(filtro);
        }

        public List<Departmentmst> listarBusqueda(DtoFiltro filtroPaginacionJefatura)
        {
            return departmentmstDao.listarBusqueda(filtroPaginacionJefatura);
        }

        public List<Departmentmst> listarTodos()
        {
            return departmentmstDao.listarTodos();
        }
    }
}
