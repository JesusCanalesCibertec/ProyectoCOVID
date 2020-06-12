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

public class CompanyownerServicioImpl : GenericoServicioImpl, CompanyownerServicio
    {

        private IServiceProvider servicioProveedor;
        private dao.CompanyownerDao companyownerDao;

        public CompanyownerServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            companyownerDao = servicioProveedor.GetService<dao.CompanyownerDao>();
        }

        public List<DtoTabla> listarActivos()
        {
            return companyownerDao.listarActivos();
        }

        public List<DtoTabla> listarCompaniasPorSeguridad(string usuario)
        {
            return companyownerDao.listarCompaniasPorSeguridad(usuario);
        }

        public List<Companyowner> listarTodos()
        {
            return companyownerDao.listarTodos();
        }

        public string obtenerNombre(string companyowner)
        {
            return companyownerDao.obtenerNombre(companyowner);
        }
    }
}
