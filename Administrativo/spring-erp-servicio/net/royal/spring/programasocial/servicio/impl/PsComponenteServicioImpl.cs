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

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsComponenteServicioImpl : GenericoServicioImpl, PsComponenteServicio {

        private IServiceProvider servicioProveedor;
        private PsComponenteDao psComponenteDao;

        public PsComponenteServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psComponenteDao = servicioProveedor.GetService<PsComponenteDao>();
        }

        public PsComponente coreInsertar(UsuarioActual usuarioActual, PsComponente bean)
        {
            string cadena, codigo;
            codigo = psComponenteDao.obtenercodigo(bean.IdComponente);
            cadena = psComponenteDao.obtenercadena(bean.Nombre);


            Regex valor = new Regex(@"^[a-zA-ZñÑ]{4}$");

            if (!valor.IsMatch(bean.IdComponente))
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
                bean.IdComponente = bean.IdComponente.ToUpper();
                return psComponenteDao.coreInsertar(usuarioActual, bean, bean.Estado);
            }
        }

        public PsComponente coreActualizar(UsuarioActual usuarioActual, PsComponente bean)
        {
            
            return psComponenteDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public PsComponente coreAnular(UsuarioActual usuarioActual, PsComponentePk id)
        {
            return psComponenteDao.coreAnular(usuarioActual,id);
        }

        public PsComponente coreAnular(UsuarioActual usuarioActual, String pIdComponente)
        {
            return psComponenteDao.coreAnular(usuarioActual,  pIdComponente);
        }

        public void coreEliminar(PsComponentePk id)
        {
            psComponenteDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdComponente)
        {
            psComponenteDao.coreEliminar( pIdComponente);
        }


        public PsComponente obtenerPorId(PsComponentePk id)
        {
            return psComponenteDao.obtenerPorId(id);
        }

        public PsComponente obtenerPorId(String pIdComponente)
        {
            return psComponenteDao.obtenerPorId( pIdComponente);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroComponente filtro)
        {
            return psComponenteDao.listarPaginacion(paginacion,filtro);
        }

        public void eliminarfila(string idComponente)
        {
            PsComponente capa = new PsComponente() { IdComponente = idComponente };

            psComponenteDao.eliminar(capa);
        }

        public List<PsComponente> listarTodos()
        {
            return psComponenteDao.listarTodos();
        }
    }
}
