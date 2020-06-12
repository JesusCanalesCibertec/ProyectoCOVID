using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio.filtro;
using net.royal.spring.framework.core;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dao;

namespace net.royal.spring.sistema.servicio.impl
{

public class SyAprobacionnivelesServicioImpl : GenericoServicioImpl, SyAprobacionnivelesServicio {

        private IServiceProvider servicioProveedor;
        private SyAprobacionnivelesDao syAprobacionnivelesDao;
        private SyAprobacionnivelesDetDao syAprobacionnivelesDetDao;
        private PersonamastDao personamastDao;

        public SyAprobacionnivelesServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            syAprobacionnivelesDao = servicioProveedor.GetService<SyAprobacionnivelesDao>();
            syAprobacionnivelesDetDao = servicioProveedor.GetService<SyAprobacionnivelesDetDao>();
            personamastDao = servicioProveedor.GetService<PersonamastDao>();
        }

        public void actualizar(UsuarioActual usuarioActual, SyAprobacionniveles syAprobacionniveles)
        {
            syAprobacionnivelesDao.actualizar(usuarioActual, syAprobacionniveles);
        }

        public void eliminar(UsuarioActual usuarioActual, int? codigo, string compania)
        {
            syAprobacionnivelesDao.eliminar(usuarioActual, codigo, compania);
        }

        public List<SyAprobacionniveles> listar(FiltroNivelAprobacion filtro)
        {
            List<SyAprobacionniveles> lst = syAprobacionnivelesDao.listar(filtro);
            List<SyAprobacionniveles> lstRetorno = new List<SyAprobacionniveles>();

            List<DtoTabla> lstAprobacion = new List<DtoTabla>();
            lstAprobacion.Add(new DtoTabla("HR", "Recursos Humanos"));
            lstAprobacion.Add(new DtoTabla("PR", "Planillas y RRHH."));
            lstAprobacion.Add(new DtoTabla("AS", "Asistencia"));


            foreach (SyAprobacionniveles syAprobacionniveles in lst)
            {
                if (UString.obtenerValorCadenaSinNulo(syAprobacionniveles.Estado).Equals("A"))
                    syAprobacionniveles.AuxEstadoNombre="Activo";
                else
                    syAprobacionniveles.AuxEstadoNombre="Inactivo";
                syAprobacionniveles.AuxAplicacionNombre=syAprobacionniveles.Aplicacion.Equals("HR") ? "Recursos Humanos" :
                    syAprobacionniveles.Aplicacion.Equals("PR") ? "Planillas y RRHH." : syAprobacionniveles.Aplicacion.Equals("AS") ? "Asistencia" : "";
                lstRetorno.Add(syAprobacionniveles);
            }

            return lstRetorno;
        }

        public List<DtoTabla> listarAplicacionPorUsuario(string idUsuario)
        {
            return syAprobacionnivelesDao.listarAplicacionPorUsuario(idUsuario);
        }

        public List<SyAprobacionniveles> listarPorAplicacionActivos(string idAplicacion)
        {
            return syAprobacionnivelesDao.listarPorAplicacionActivos(idAplicacion);
        }

        public int? obtenerNroNiveles(SyAprobacionnivelesPk pk)
        {
            return syAprobacionnivelesDao.obtenerNroNiveles(pk);
        }

        public SyAprobacionniveles obtenerPorCodigoProcesoCompania(int proceso, string comania)
        {
            return syAprobacionnivelesDao.obtenerPorCodigoProcesoCompania(proceso, comania);
        }

        public List<SyAprobacionniveles> obtenerPorCodigoProcesoCompaniaList(int proceso, string comania)
        {
            return syAprobacionnivelesDao.obtenerPorCodigoProcesoCompaniaList(proceso, comania);
        }

        public SyAprobacionniveles obtenerPorIdCompleto(SyAprobacionnivelesPk syAprobacionnivelesPk)
        {
            var syAprobacionniveles = syAprobacionnivelesDao.obtenerPorIdCompleto(syAprobacionnivelesPk);

            if (syAprobacionniveles.Usuario != null && syAprobacionniveles.Usuario != null)
            {
                PersonamastPk perPk = new PersonamastPk()
                {
                    Persona = syAprobacionniveles.Usuario
                };
                syAprobacionniveles.AuxUsuarioNombre = personamastDao.obtenerNombrePorPk(perPk);
            }

            if (syAprobacionniveles.Usuarioadministrador != null && syAprobacionniveles.Usuarioadministrador != null)
            {
                PersonamastPk perPk = new PersonamastPk()
                {
                    Persona = syAprobacionniveles.Usuarioadministrador
                };
                syAprobacionniveles.AuxUsuarioAdmNombre = personamastDao.obtenerNombrePorPk(perPk);
            }

            return syAprobacionniveles;
        }

        public SyAprobacionniveles registrar(UsuarioActual usuarioActual, SyAprobacionniveles syAprobacionniveles)
        {
            return syAprobacionnivelesDao.registrar(usuarioActual, syAprobacionniveles);
        }


        public void validarConfiguracionPorCompania(string proceso, string companiaSocioCodigo)
        {
            syAprobacionnivelesDao.validarConfiguracionPorCompania(proceso, companiaSocioCodigo);
        }
    }
}
