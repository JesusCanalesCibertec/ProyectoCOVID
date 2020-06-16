using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core;

namespace net.royal.spring.core.servicio.impl
{

public class ProvinciaServicioImpl : GenericoServicioImpl, ProvinciaServicio {

        private IServiceProvider servicioProveedor;
        private ProvinciaDao provinciaDao;

        public ProvinciaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            provinciaDao = servicioProveedor.GetService<ProvinciaDao>();
        }

        public List<Provincia> listar(FiltroProvincia filtro)
        {
            return provinciaDao.listar(filtro);
        }

        public List<Provincia> listarActivosPorDepartamento(string idDepartamento)
        {
            //FiltroProvincia filtro = new FiltroProvincia();
            //filtro.IdDepartamento = idDepartamento;
            //filtro.Estado = "A";
            return provinciaDao.listar(idDepartamento);
        }

        public List<Provincia> listarTodos()
        {
            return provinciaDao.listarTodos();
        }

        public String obtenerNombrePorId(String departamento, String provincia) {
            departamento = UString.obtenerValorCadenaSinNulo(departamento).Trim();
            provincia = UString.obtenerValorCadenaSinNulo(provincia).Trim();
            Provincia p = provinciaDao.obtenerPorId(new ProvinciaPk(departamento, provincia));
            if (p==null)
                return "";
            return p.Descripcion;
        }
    }
}
