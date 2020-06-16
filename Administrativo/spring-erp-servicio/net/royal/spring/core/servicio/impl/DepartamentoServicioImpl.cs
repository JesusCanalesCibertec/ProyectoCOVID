using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core.servicio.impl
{

public class DepartamentoServicioImpl : GenericoServicioImpl, DepartamentoServicio {

        private IServiceProvider servicioProveedor;
        private DepartamentoDao departamentoDao;

        public DepartamentoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            departamentoDao = servicioProveedor.GetService<DepartamentoDao>();
        }

        public List<Departamento> listar(FiltroDepartamento filtro)
        {
            return departamentoDao.listar(filtro);
        }

        public List<Departamento> listarActivosPorPais(string idPais)
        {
            FiltroDepartamento filtro = new FiltroDepartamento();
            filtro.IdPais = idPais;
            filtro.Estado = "A";
            return listar(filtro);
        }

        public List<Departamento> listarTodos()
        {
            return departamentoDao.listarTodos();
        }

        public Departamento obtenerPorId(DepartamentoPk pk)
        {
            return departamentoDao.obtenerPorId(pk);
        }
    }
}
