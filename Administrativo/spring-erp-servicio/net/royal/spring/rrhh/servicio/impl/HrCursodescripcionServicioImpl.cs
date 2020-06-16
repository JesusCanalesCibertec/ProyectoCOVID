using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrCursodescripcionServicioImpl : GenericoServicioImpl, HrCursodescripcionServicio {

        private IServiceProvider servicioProveedor;
        private HrCursodescripcionDao hrCursodescripcionDao;

        public HrCursodescripcionServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrCursodescripcionDao = servicioProveedor.GetService<HrCursodescripcionDao>();
        }

        public HrCursodescripcion coreInsertar(UsuarioActual usuarioActual, HrCursodescripcion bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrCursodescripcionDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public HrCursodescripcion coreActualizar(UsuarioActual usuarioActual, HrCursodescripcion bean)
        {         
            return hrCursodescripcionDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public HrCursodescripcion coreAnular(UsuarioActual usuarioActual, HrCursodescripcionPk id)
        {
            return hrCursodescripcionDao.coreAnular(usuarioActual,id);
        }

        public HrCursodescripcion coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pCurso)
        {
            return hrCursodescripcionDao.coreAnular(usuarioActual,  pCurso);
        }

        public void coreEliminar(HrCursodescripcionPk id)
        {
            hrCursodescripcionDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pCurso)
        {
            hrCursodescripcionDao.coreEliminar( pCurso);
        }


        public HrCursodescripcion obtenerPorId(HrCursodescripcionPk id)
        {
            return hrCursodescripcionDao.obtenerPorId(id);
        }

        public HrCursodescripcion obtenerPorId(Nullable<Int32> pCurso)
        {
            return hrCursodescripcionDao.obtenerPorId( pCurso);
        }

  		public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro)
        {
            return hrCursodescripcionDao.listarBusqueda(paginacion, filtro);
        }
        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro)
        {
            return hrCursodescripcionDao.listarPaginacion(filtro.paginacion, filtro);
        }

        public HrCursodescripcion registrarCurso(UsuarioActual usuarioActual, HrCursodescripcion bean)
        {
            string cadena;
            cadena = hrCursodescripcionDao.obtenerDescripcion(bean.Descripcion);

            if(cadena != null)
            {
                throw new UException("La descripción ingresada ya se encuentra registrada");
            }
            else
            {
                if (UString.estaVacio(bean.Estado))
                    bean.Estado = ConstanteEstadoGenerico.ACTIVO;
                return hrCursodescripcionDao.registrarCurso(usuarioActual, bean);
            }

        }

        public void eliminarCurso(int curso)
        {
            
                HrCursodescripcion capa = new HrCursodescripcion() { Curso = curso };

                hrCursodescripcionDao.eliminar(capa);
            
        }
    }
}
