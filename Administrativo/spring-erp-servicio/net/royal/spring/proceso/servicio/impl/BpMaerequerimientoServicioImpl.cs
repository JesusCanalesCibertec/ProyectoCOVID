using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.constante;
using Microsoft.Extensions.Logging;
using net.royal.spring.proceso.dominio.filtro;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpMaerequerimientoServicioImpl : GenericoServicioImpl, BpMaerequerimientoServicio
    {

        private IServiceProvider servicioProveedor;
        private BpMaerequerimientoDao BpMaerequerimientoDao;


        public BpMaerequerimientoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            BpMaerequerimientoDao = servicioProveedor.GetService<BpMaerequerimientoDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroRequerimiento filtro)
        {
            return BpMaerequerimientoDao.listarPaginacion(paginacion,filtro);
        }

        public List<BpMaerequerimiento> listarTodos()
        {
            return BpMaerequerimientoDao.listarTodos();
        }

        public BpMaerequerimiento obtenerPorId(BpMaerequerimientoPk llave)
        {
            return BpMaerequerimientoDao.obtenerPorId(llave);
        }
    }
}
