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

    public class HrEncuestapreguntaServicioImpl : GenericoServicioImpl, HrEncuestapreguntaServicio
    {

        private IServiceProvider servicioProveedor;
        private HrEncuestapreguntaDao hrEncuestapreguntaDao;
        private HrEncuestapreguntavaloresDao hrEncuestapreguntavaloresDao;


        public HrEncuestapreguntaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestapreguntaDao = servicioProveedor.GetService<HrEncuestapreguntaDao>();
            hrEncuestapreguntavaloresDao = servicioProveedor.GetService<HrEncuestapreguntavaloresDao>();

        }

        public HrEncuestapregunta coreInsertar(UsuarioActual usuarioActual, HrEncuestapregunta bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrEncuestapreguntaDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public HrEncuestapregunta coreActualizar(UsuarioActual usuarioActual, HrEncuestapregunta bean)
        {
            return hrEncuestapreguntaDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public HrEncuestapregunta coreAnular(UsuarioActual usuarioActual, HrEncuestapreguntaPk id)
        {
            return hrEncuestapreguntaDao.coreAnular(usuarioActual, id);
        }

        public HrEncuestapregunta coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPregunta)
        {
            return hrEncuestapreguntaDao.coreAnular(usuarioActual, pPregunta);
        }

        public void coreEliminar(HrEncuestapreguntaPk id)
        {
            hrEncuestapreguntaDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pPregunta)
        {
            hrEncuestapreguntaDao.coreEliminar(pPregunta);
        }


        public HrEncuestapregunta obtenerPorId(HrEncuestapreguntaPk id)
        {
            return hrEncuestapreguntaDao.obtenerPorId(id);
        }

        public HrEncuestapregunta obtenerPorId(Nullable<Int32> pPregunta)
        {
            return hrEncuestapreguntaDao.obtenerPorId(pPregunta);
        }

        public ParametroPaginacionGenerico listarEncuestas(ParametroPaginacionGenerico paginacion, FiltroEncuestaPregunta filtro)
        {
            return hrEncuestapreguntaDao.listarEncuestas(paginacion, filtro);
        }



        public HrEncuestapregunta registrarPregunta(UsuarioActual usuarioActual, HrEncuestapregunta bean)
        {
            bean.Pregunta = hrEncuestapreguntaDao.generarCodigo();
            this.coreInsertar(usuarioActual, bean);

            foreach (HrEncuestapreguntavalores deta in bean.listaValores)
            {
                deta.Pregunta = bean.Pregunta;
                hrEncuestapreguntavaloresDao.coreInsertar(usuarioActual, deta);
            }

            return bean;
        }



        public HrEncuestapregunta solicitudObtenerPorId(Nullable<Int32> pk)
        {
            HrEncuestapregunta bean = this.obtenerPorId(pk);

            if (bean.Area != null)
            {
                bean.Area = bean.Area.Trim();
            }

            bean.listaValores = hrEncuestapreguntavaloresDao.listarValores(pk);

            if (!UInteger.esCeroOrNulo(bean.Requierepregunta))
            {
                HrEncuestapregunta bean2 = this.obtenerPorId(bean.Requierepregunta);
                bean.nombrePregunta = bean2.Descripcion;
            }

            return bean;

        }

        public HrEncuestapregunta actualizarPregunta(UsuarioActual usuarioActual, HrEncuestapregunta bean)
        {

            this.coreActualizar(usuarioActual, bean);

            hrEncuestapreguntavaloresDao.EliminarDetalle(bean.Pregunta.Value);

            foreach (HrEncuestapreguntavalores deta in bean.listaValores)
            {
                deta.Pregunta = bean.Pregunta;



                hrEncuestapreguntavaloresDao.coreInsertar(usuarioActual, deta);



            }

            return bean;
        }

        public void eliminarPregunta(int? pregunta)
        {
            this.coreEliminar(pregunta);


            HrEncuestapreguntavalores capa = new HrEncuestapreguntavalores();

            capa.Pregunta = pregunta;

            hrEncuestapreguntavaloresDao.EliminarDetalle(capa.Pregunta);

        }



    }
}
