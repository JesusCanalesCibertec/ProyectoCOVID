using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;

namespace net.royal.spring.core.servicio.impl
{

public class MaMiscelaneosheaderServicioImpl : GenericoServicioImpl, MaMiscelaneosheaderServicio {

        private IServiceProvider servicioProveedor;
        private MaMiscelaneosheaderDao maMiscelaneosheaderDao;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;

        public MaMiscelaneosheaderServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            maMiscelaneosheaderDao = servicioProveedor.GetService<MaMiscelaneosheaderDao>();
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
        }

        public MaMiscelaneosheader coreInsertar(UsuarioActual usuarioActual, MaMiscelaneosheader bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return maMiscelaneosheaderDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public MaMiscelaneosheader coreActualizar(UsuarioActual usuarioActual, MaMiscelaneosheader bean)
        {
            return maMiscelaneosheaderDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }


        public void coreEliminar(MaMiscelaneosheaderPk id)
        {
            maMiscelaneosheaderDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pPregunta)
        {
            maMiscelaneosheaderDao.coreEliminar(pPregunta);
        }


        public MaMiscelaneosheader obtenerPorId(MaMiscelaneosheaderPk id)
        {
            return maMiscelaneosheaderDao.obtenerPorId(id);
        }

        public MaMiscelaneosheader obtenerPorId(Nullable<Int32> pPregunta)
        {
            return maMiscelaneosheaderDao.obtenerPorId(pPregunta);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroMiscelaneosheader filtro)
        {
            return maMiscelaneosheaderDao.listarPaginacion(paginacion, filtro);
        }



     


        public MaMiscelaneosheader solicitudObtenerPorId(string aplicacion, string compania, string codigo)
        {
            
            MaMiscelaneosheader bean = this.obtenerPorId(new MaMiscelaneosheaderPk() {Aplicacioncodigo = aplicacion, Compania=compania, Codigotabla=codigo} );
            
            MaMiscelaneosheaderPk llave = new MaMiscelaneosheaderPk();

            llave.Aplicacioncodigo = aplicacion;
            llave.Compania = compania;
            llave.Codigotabla = codigo.Trim();
            
            bean.listadetalle = maMiscelaneosdetalleDao.listarDetalle(llave);

            return bean;

        }

        public MaMiscelaneosheader actualizarMiscelaneo(UsuarioActual usuarioActual, MaMiscelaneosheader bean)
        {

            MaMiscelaneosheaderPk llave = new MaMiscelaneosheaderPk();

            llave.Aplicacioncodigo = bean.Aplicacioncodigo;
            llave.Compania = bean.Compania;
            llave.Codigotabla = bean.Codigotabla;

            maMiscelaneosdetalleDao.coreEliminar(llave);

            foreach (MaMiscelaneosdetalle deta in bean.listadetalle)
            {
                deta.Aplicacioncodigo = bean.Aplicacioncodigo;
                deta.Codigotabla = bean.Codigotabla;
                deta.Compania = bean.Compania;

                maMiscelaneosdetalleDao.registrar(deta);

            }

            return bean;
        }

        public void eliminarPregunta(int? pregunta)
        {
            /*
            this.coreEliminar(pregunta);


            MaMiscelaneosheadervalores capa = new MaMiscelaneosheadervalores();

            capa.Pregunta = pregunta;

            MaMiscelaneosheadervaloresDao.EliminarDetalle(capa.Pregunta);*/

        }

        public MaMiscelaneosheader registrarPregunta(UsuarioActual usuarioActual, MaMiscelaneosheader bean)
        {
            MaMiscelaneosheaderPk llave = new MaMiscelaneosheaderPk();

            llave.Aplicacioncodigo = bean.Aplicacioncodigo;
            llave.Compania = bean.Compania;
            llave.Codigotabla = bean.Codigotabla;

            foreach (MaMiscelaneosdetalle deta in bean.listadetalle)
            {
                deta.Aplicacioncodigo = bean.Aplicacioncodigo;
                deta.Codigotabla = bean.Codigotabla;
                deta.Compania = bean.Compania;

                maMiscelaneosdetalleDao.registrar(deta);

            }

            return bean;
        }
    }
}
