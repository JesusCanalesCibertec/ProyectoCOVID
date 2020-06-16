using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.proyecto.dominio.filtro;
using net.royal.spring.sistema.dao;

namespace net.royal.spring.proyecto.servicio.impl
{

    public class PmPortafolioServicioImpl : GenericoServicioImpl, PmPortafolioServicio
    {
        private IServiceProvider servicioProveedor;
        private PmPortafolioDao pmPortafolioDao;

        public PmPortafolioServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmPortafolioDao = servicioProveedor.GetService<PmPortafolioDao>();
        }

        public PmPortafolio obtenerPortafolioActual(UsuarioActual usuarioActual)
        {
            List<PmPortafolio> lst = pmPortafolioDao.listarActivos();

            if (lst.Count > 0)
                return lst[0];

            PmPortafolio pmPortafolio = new PmPortafolio();
            pmPortafolio.Nombre = "Portafolio por Defecto";
            pmPortafolio.CreacionFecha = DateTime.Now;
            pmPortafolio.CreacionUsuario = usuarioActual.UsuarioLogin;

            return pmPortafolioDao.registrar(pmPortafolio);
        }
    }
}
