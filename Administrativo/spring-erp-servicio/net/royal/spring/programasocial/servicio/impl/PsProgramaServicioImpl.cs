using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;

namespace net.royal.spring.programasocial.servicio.impl
{

    public class PsProgramaServicioImpl : GenericoServicioImpl, PsProgramaServicio
    {

        private IServiceProvider servicioProveedor;
        private PsProgramaDao psProgramaDao;
        private PsInstitucionDao psInstitucionDao;

        public PsProgramaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            psProgramaDao = servicioProveedor.GetService<PsProgramaDao>();
            psInstitucionDao = servicioProveedor.GetService<PsInstitucionDao>();
        }

        public PsPrograma coreInsertar(UsuarioActual usuarioActual, PsPrograma bean)
        {
            string cadena, codigo;
            codigo = psProgramaDao.obtenercodigo(bean.IdPrograma);
            cadena = psProgramaDao.obtenercadena(bean.Nombre);


            Regex valor = new Regex(@"^[a-zA-ZñÑ]{4}$");

            if (!valor.IsMatch(bean.IdPrograma))
            {
                throw new UException("El código debe contener 4 letras sin tildes");
            }
            if (codigo != null)
            {
                throw new UException("El código ingresado ya se encuentra registrado");
            }
            if (cadena != null)
            {
                throw new UException("El nombre ingresado ya se encuentra registrado");
            }
            else
            {
                if (UString.estaVacio(bean.Estado))
                    bean.Estado = ConstanteEstadoGenerico.ACTIVO;
                bean.IdPrograma = bean.IdPrograma.ToUpper();
                return psProgramaDao.coreInsertar(usuarioActual, bean, bean.Estado);
            }
        }

        public PsPrograma coreActualizar(UsuarioActual usuarioActual, PsPrograma bean)
        {

            return psProgramaDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PsPrograma coreAnular(UsuarioActual usuarioActual, PsProgramaPk id)
        {
            return psProgramaDao.coreAnular(usuarioActual, id);
        }

        public PsPrograma coreAnular(UsuarioActual usuarioActual, String pIdPrograma)
        {
            return psProgramaDao.coreAnular(usuarioActual, pIdPrograma);
        }

        public void coreEliminar(PsProgramaPk id)
        {
            psProgramaDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdPrograma)
        {
            psProgramaDao.coreEliminar(pIdPrograma);
        }


        public PsPrograma obtenerPorId(PsProgramaPk id)
        {
            return psProgramaDao.obtenerPorId(id);
        }

        public PsPrograma obtenerPorId(String pIdPrograma)
        {
            return psProgramaDao.obtenerPorId(pIdPrograma);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPrograma filtro)
        {
            return psProgramaDao.listarPaginacion(paginacion, filtro);
        }

        public void eliminar(string idPrograma)
        {

            PsPrograma capa = new PsPrograma() { IdPrograma = idPrograma };

            psProgramaDao.eliminar(capa);
        }

        public List<PsPrograma> listarTodos()
        {
            return psProgramaDao.listarTodos();
        }

        public List<PsPrograma> listarPorId(String idPrograma)
        {
            List<PsPrograma> lst = new List<PsPrograma>();
            PsPrograma psPrograma = psProgramaDao.obtenerPorId(idPrograma);
            if (psPrograma != null)
                lst.Add(psPrograma);
            return lst;
        }

        public List<PsPrograma> listarPorInstitucion(String pIdUsuaio, String pIdInstitucion)
        {
            if (UString.estaVacio(pIdInstitucion))
            {
                return new List<PsPrograma>();
            }

            /*canevaro AAM*/
            if (pIdInstitucion.Equals("CANEV"))
                return listarPorId("AAM");

            /* misioneras de la caridad : NNA AAM */
            if (pIdInstitucion.Equals("CARID"))
                return psProgramaDao.listarTodos();

            /* los desamparados : AAM */
            if (pIdInstitucion.Equals("DESAM"))
                return listarPorId("AAM");

            /* san francisso de asis : NNA */
            if (pIdInstitucion.Equals("FRASI"))
                return listarPorId("NNA");

            /* inmaculada : NNA */
            if (pIdInstitucion.Equals("INMAC"))
                return listarPorId("NNA");

            /* PUERICULTORIO PÉREZ ARANIBAR : NNA */
            if (pIdInstitucion.Equals("PURIC"))
                return listarPorId("NNA");

            /* CARGG "SAN VICENTE DE PAUL" : AAM */
            if (pIdInstitucion.Equals("SVPAU"))
                return listarPorId("AAM");

            return new List<PsPrograma>();
        }

        public List<PsInstitucion> listarPorPrograma(string pIdPrograma)
        {
            if (pIdPrograma == "AAM")
            {
                List<String> ids = new List<string>() { "SVPAU", "DESAM", "CARID", "CANEV" };
                return psInstitucionDao.listarPorPks(ids);

            }
            if (pIdPrograma == "NNA")
            {
                List<String> ids = new List<string>() { "PURIC", "INMAC", "FRASI", "CARID" };
                return psInstitucionDao.listarPorPks(ids);

            }
            return new List<PsInstitucion>();
        }

        public List<PsPrograma> listarTodosActivos() {
            return psProgramaDao.listarTodosActivos();
        }
    }
}
