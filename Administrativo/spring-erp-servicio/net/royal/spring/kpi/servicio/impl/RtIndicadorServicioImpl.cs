
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.kpi.dao;
using net.royal.spring.kpi.servicio;
using net.royal.spring.kpi.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.kpi.servicio.impl
{

    public class RtIndicadorServicioImpl : GenericoServicioImpl, RtIndicadorServicio
    {

        private IServiceProvider servicioProveedor;
        private RtIndicadorDao rtIndicadorDao;

        public RtIndicadorServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            rtIndicadorDao = servicioProveedor.GetService<RtIndicadorDao>();
        }

        public RtIndicador coreInsertar(UsuarioActual usuarioActual, RtIndicador bean)
        {
            string cadena, codigo;
            codigo = rtIndicadorDao.obtenercodigo(bean.IdIndicador);
            cadena = rtIndicadorDao.obtenercadena(bean.Nombre);

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
                return rtIndicadorDao.coreInsertar(usuarioActual, bean, bean.Estado);
            }
        }

        public RtIndicador coreActualizar(UsuarioActual usuarioActual, RtIndicador bean)
        {

            return rtIndicadorDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public RtIndicador coreAnular(UsuarioActual usuarioActual, RtIndicadorPk id)
        {
            return rtIndicadorDao.coreAnular(usuarioActual, id);
        }

        public RtIndicador coreAnular(UsuarioActual usuarioActual, String pIdIndicador)
        {
            return rtIndicadorDao.coreAnular(usuarioActual, pIdIndicador);
        }

        public void coreEliminar(RtIndicadorPk id)
        {
            rtIndicadorDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdIndicador)
        {
            rtIndicadorDao.coreEliminar(pIdIndicador);
        }


        public RtIndicador obtenerPorId(RtIndicadorPk id)
        {
            return rtIndicadorDao.obtenerPorId(id);
        }

        public RtIndicador obtenerPorId(String pIdIndicador)
        {
            return rtIndicadorDao.obtenerPorId(pIdIndicador);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroIndicador filtro)
        {
            return rtIndicadorDao.listarPaginacion(paginacion, filtro);
        }

        public void eliminarfila(string codigo)
        {

            RtIndicador capa = new RtIndicador() { IdIndicador = codigo };

            rtIndicadorDao.eliminar(capa);

        }

        public List<RtIndicador> listarPorPrograma(string programa)
        {
            return rtIndicadorDao.listarPorPrograma(programa);
        }
    }
}
