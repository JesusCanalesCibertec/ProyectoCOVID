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

public class BancoServicioImpl : GenericoServicioImpl, BancoServicio {

        private IServiceProvider servicioProveedor;
        private BancoDao bancoDao;

        public BancoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            bancoDao = servicioProveedor.GetService<BancoDao>();
}

        public List<Banco> listar(DtoFiltro filtro)
        {
            return bancoDao.listar(filtro);
        }

        public List<Banco> listarActivos()
        {
            DtoFiltro filtro = new DtoFiltro();
            filtro.Estado = "A";
            return listar(filtro);
        }

        public List<Banco> listarTodos()
        {
            return bancoDao.listarTodos();
        }

        public Banco obtenerPorId(BancoPk pk)
        {
            return bancoDao.obtenerPorId(pk);
        }
    }
}
