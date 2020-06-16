using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.servicio.impl
{

public class SyUnidadreplicacionServicioImpl : GenericoServicioImpl, SyUnidadreplicacionServicio {

        private IServiceProvider servicioProveedor;
        private SyUnidadreplicacionDao syUnidadreplicacionDao;

        public SyUnidadreplicacionServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            syUnidadreplicacionDao = servicioProveedor.GetService<SyUnidadreplicacionDao>();
        }

        public List<SyUnidadreplicacion> listar(DtoFiltro filtro)
        {
            return syUnidadreplicacionDao.listar(filtro);
        }

        public List<SyUnidadreplicacion> listarActivos()
        {
            DtoFiltro filtro = new DtoFiltro();
            filtro.Estado="A";
            return listar(filtro);
        }

        public List<SyUnidadreplicacion> listarTodos()
        {
            return syUnidadreplicacionDao.listarTodos();
        }

        public SyUnidadreplicacion obtenerPorId(SyUnidadreplicacionPk syUnidadreplicacionPk)
        {
            return syUnidadreplicacionDao.obtenerPorId(syUnidadreplicacionPk);
        }
    }
}
