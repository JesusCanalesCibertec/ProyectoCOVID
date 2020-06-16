using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpTransaccionseguimientoServicioImpl : GenericoServicioImpl, BpTransaccionseguimientoServicio
    {

        private IServiceProvider servicioProveedor;
        private BpTransaccionseguimientoDao bpTransaccionseguimientoDao;

        public BpTransaccionseguimientoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpTransaccionseguimientoDao = servicioProveedor.GetService<BpTransaccionseguimientoDao>();
        }

        public List<BpTransaccionseguimiento> listarTransaccionSeguimiento(int idTransaccion)
        {
            return bpTransaccionseguimientoDao.listarTransaccionSeguimiento(idTransaccion);
        }
    }
}
