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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrEncuestaplantillaServicioImpl : GenericoServicioImpl, HrEncuestaplantillaServicio {

        private IServiceProvider servicioProveedor;
        private HrEncuestaplantillaDao hrEncuestaplantillaDao;
        private HrEncuestapreguntaDao hrEncuestapreguntaDao;
        private HrEncuestaplantilladetalleDao hrEncuestaplantilladetalleDao;

        public HrEncuestaplantillaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrEncuestaplantillaDao = servicioProveedor.GetService<HrEncuestaplantillaDao>();
            hrEncuestapreguntaDao = servicioProveedor.GetService<HrEncuestapreguntaDao>();
            hrEncuestaplantilladetalleDao = servicioProveedor.GetService<HrEncuestaplantilladetalleDao>();
        }

        public HrEncuestaplantilla coreInsertar(UsuarioActual usuarioActual, HrEncuestaplantilla bean)
        {

            bean.Plantilla = hrEncuestaplantillaDao.generarCodigo();


            foreach (HrEncuestaplantilladetalle deta in bean.listadetalle)
            {
                deta.Plantilla = bean.Plantilla;
                hrEncuestaplantilladetalleDao.coreInsertar(usuarioActual, deta);
            }

            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return hrEncuestaplantillaDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public HrEncuestaplantilla coreActualizar(UsuarioActual usuarioActual, HrEncuestaplantilla bean)
        {
            



            hrEncuestaplantillaDao.eliminardetalle(bean.Plantilla);

            foreach (HrEncuestaplantilladetalle deta in bean.listadetalle)
            {
                deta.Plantilla = bean.Plantilla;

                hrEncuestaplantilladetalleDao.registrar(deta);



            }

            return hrEncuestaplantillaDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public HrEncuestaplantilla coreAnular(UsuarioActual usuarioActual, HrEncuestaplantillaPk id)
        {
            return hrEncuestaplantillaDao.coreAnular(usuarioActual,id);
        }

        public HrEncuestaplantilla coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPlantilla)
        {
            return hrEncuestaplantillaDao.coreAnular(usuarioActual,  pPlantilla);
        }

        public void coreEliminar(HrEncuestaplantillaPk id)
        {
            hrEncuestaplantillaDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pPlantilla)
        {
            hrEncuestaplantillaDao.eliminardetalle(pPlantilla);

            hrEncuestaplantillaDao.coreEliminar( pPlantilla);
        }


        public HrEncuestaplantilla obtenerPorId(HrEncuestaplantillaPk id)
        {
            
            return hrEncuestaplantillaDao.obtenerPorId(id);
        }

        public HrEncuestaplantilla obtenerPorId(int pPlantilla)
        {
            HrEncuestaplantilla bean = hrEncuestaplantillaDao.obtenerPorId(new HrEncuestaplantillaPk() {Plantilla = pPlantilla }.obtenerArreglo());

            bean.listadetalle = hrEncuestaplantilladetalleDao.listarPreguntas(pPlantilla);

            
            foreach (var item in bean.listadetalle)
            {
                item.auxNombre = hrEncuestapreguntaDao.obtenerDescripcion(item.Pregunta.Value);
            }
            return bean;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return hrEncuestaplantillaDao.listarPaginacion(paginacion, filtro);
        }

        public List<HrEncuestadetalle> listarPorPlantilla(int id)
        {
            return hrEncuestapreguntaDao.listarPorPlantilla(id);
        }
    }
}
