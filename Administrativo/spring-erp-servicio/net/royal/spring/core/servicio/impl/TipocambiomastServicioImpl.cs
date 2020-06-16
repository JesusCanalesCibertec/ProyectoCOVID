using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.servicio.impl
{

public class TipocambiomastServicioImpl : GenericoServicioImpl, TipocambiomastServicio {

        private IServiceProvider servicioProveedor;
        private TipocambiomastDao tipocambiomastDao;

        public TipocambiomastServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            tipocambiomastDao = servicioProveedor.GetService<TipocambiomastDao>();
        }

        public List<Tipocambiomast> listarTodos()
        {
            return tipocambiomastDao.listarTodos();
        }

        public float? obtenerFactorCompraPorFecha(string fechaCadena)
        {
            Tipocambiomast tipocambio = tipocambiomastDao.obtenerPorFecha(fechaCadena);
            if (tipocambio == null)
                return null;
            return tipocambio.Factorcompra;
        }

        public Tipocambiomast obtenerPorFecha(DateTime fecha)
        {
            String fechaCadena = "";

            if (fecha == null)
                return null;
            return tipocambiomastDao.obtenerPorFecha(fechaCadena);
        }

        public Tipocambiomast obtenerPorFecha(string fechaCadena)
        {
            return tipocambiomastDao.obtenerPorFecha(fechaCadena);
        }
    }
}
