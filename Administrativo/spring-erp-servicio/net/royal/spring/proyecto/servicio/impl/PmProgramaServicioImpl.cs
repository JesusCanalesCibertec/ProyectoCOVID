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

    public class PmProgramaServicioImpl : GenericoServicioImpl, PmProgramaServicio
    {

        private IServiceProvider servicioProveedor;
        private PmProgramaDao pmProgramaDao;
        private AplicacionesmastDao aplicacionesmastDao;

        public PmProgramaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmProgramaDao = servicioProveedor.GetService<PmProgramaDao>();
            aplicacionesmastDao = servicioProveedor.GetService<AplicacionesmastDao>();
        }

        public PmPrograma crearProgramaAnual(UsuarioActual usuarioActual)
        {
            PmPortafolioServicio pmPortafolioServicio = servicioProveedor.GetService<PmPortafolioServicio>();

            PmPortafolio pmPortafolio = pmPortafolioServicio.obtenerPortafolioActual(usuarioActual);
                        
            List<PmPrograma> lst = pmProgramaDao.listarTodos();

            if (lst.Count > 0)
                return lst[0];

            PmPrograma pmPrograma = new PmPrograma();

            pmPrograma.IdPortafolio = pmPortafolio.IdPortafolio;
            pmPrograma.Anio = DateTime.Today.Year.ToString();
            pmPrograma.Nombre = "Programa anual : " + pmPrograma.Anio;

            pmPrograma.CreacionFecha = DateTime.Now;
            pmPrograma.CreacionUsuario = usuarioActual.UsuarioLogin;

            return pmProgramaDao.registrar(pmPrograma);
        }
    }
}
