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

public class ZonapostalServicioImpl : GenericoServicioImpl, ZonapostalServicio {

        private IServiceProvider servicioProveedor;
        private ZonapostalDao zonapostalDao;

        public ZonapostalServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            zonapostalDao = servicioProveedor.GetService<ZonapostalDao>();
        }

        public List<Zonapostal> listar(FiltroZonaPostal filtro)
        {
            return zonapostalDao.listar(filtro);
        }

        public List<Zonapostal> listarActivosPorProvincia(string idDepartamento, string idProvincia)
        {
            //FiltroZonaPostal filtro = new FiltroZonaPostal();
            //filtro.IdDepartamento = idDepartamento;
            //filtro.IdProvincia = idProvincia;
            //filtro.Estado = "A";
            return zonapostalDao.listar(idDepartamento, idProvincia);
        }

        public List<Zonapostal> listarTodos()
        {
            return zonapostalDao.listarTodos();
        }

        public String obtenerNombrePorId(String departamento, String provincia, String distrito) {
            departamento = UString.obtenerValorCadenaSinNulo(departamento).Trim();
            provincia = UString.obtenerValorCadenaSinNulo(provincia).Trim();
            distrito = UString.obtenerValorCadenaSinNulo(distrito).Trim();
            Zonapostal p = zonapostalDao.obtenerPorId(new ZonapostalPk(departamento, provincia, distrito));
            if (p == null)
                return "";
            return p.Descripcion;
        }
    }
}
