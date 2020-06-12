using System;
using System.Collections.Generic;
using System.Text;

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

public class PsItemServicioImpl : GenericoServicioImpl, PsItemServicio {

        private IServiceProvider servicioProveedor;
        private PsItemDao psItemDao;

        public PsItemServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psItemDao = servicioProveedor.GetService<PsItemDao>();
        }

        public PsItem coreInsertar(UsuarioActual usuarioActual, PsItem bean)
        {

            if (psItemDao.obtenercodigo(bean.IdItem) != null)
            {
                throw new UException("El código ingresado ya se encuentra registrado");
                
            }
            if (psItemDao.obtenercadena(bean.Nombre) != null)
            {
                throw new UException("El nombre ingresado ya se encuentra registrado");
;           }

            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psItemDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public PsItem coreActualizar(UsuarioActual usuarioActual, PsItem bean)
        {
            string Auxcodigo = bean.IdItem;
            string AuxNombre = bean.Nombre;
            
            /*
            if (psItemDao.obtenerCadenaxCodigo(Auxcodigo) != AuxNombre)
                {
                if(psItemDao.obtenercadena(AuxNombre) != null)
                {
                    throw new UException("El nombre ingresado ya se encuentra registrado");
                }
                return psItemDao.coreActualizar(usuarioActual, bean, bean.Estado);
                }*/
           
                return psItemDao.coreActualizar(usuarioActual, bean, bean.Estado);
            
            
        }

        public PsItem coreAnular(UsuarioActual usuarioActual, PsItemPk id)
        {
            return psItemDao.coreAnular(usuarioActual,id);
        }

        public PsItem coreAnular(UsuarioActual usuarioActual, String pIdItem)
        {
            return psItemDao.coreAnular(usuarioActual,  pIdItem);
        }

        public void coreEliminar(PsItemPk id)
        {
            psItemDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdItem)
        {
            psItemDao.coreEliminar( pIdItem);
        }


        public PsItem obtenerPorId(PsItemPk id)
        {
            return psItemDao.obtenerPorId(id);
        }

        public PsItem obtenerPorId(String pIdItem)
        {
            return psItemDao.obtenerPorId( pIdItem);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroItem filtro)
        {
            return psItemDao.listarPaginacion(paginacion, filtro);
        }

        public void eliminar(string idItem)
        {
            psItemDao.coreEliminar(idItem);
        }

        
        
    }
}
