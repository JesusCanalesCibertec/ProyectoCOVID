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
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dao;

namespace net.royal.spring.rrhh.servicio.impl
{

public class PmPlantillaServicioImpl : GenericoServicioImpl, PmPlantillaServicio {

        private IServiceProvider servicioProveedor;
        private PmPlantillaDao pmPlantillaDao;
        private PmPlantillaTareasDao pmPlantillaTareasDao;

        public PmPlantillaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            pmPlantillaDao = servicioProveedor.GetService<PmPlantillaDao>();
            pmPlantillaTareasDao = servicioProveedor.GetService<PmPlantillaTareasDao>();
        }

        public PmPlantilla coreInsertar(UsuarioActual usuarioActual, PmPlantilla bean)
        {

            bean.Plantilla = pmPlantillaDao.generarCodigo();


            foreach (PmPlantillaTareas deta in bean.listadetalle)
            {
                deta.Plantilla = bean.Plantilla;
                pmPlantillaTareasDao.coreInsertar(usuarioActual, deta);
            }

            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return pmPlantillaDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public PmPlantilla coreActualizar(UsuarioActual usuarioActual, PmPlantilla bean)
        {




            pmPlantillaDao.eliminardetalle(bean.Plantilla);

            foreach (PmPlantillaTareas deta in bean.listadetalle)
            {
                deta.Plantilla = bean.Plantilla;

                pmPlantillaTareasDao.registrar(deta);



            }

            return pmPlantillaDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

        public PmPlantilla coreAnular(UsuarioActual usuarioActual, PmPlantillaPk id)
        {
            return pmPlantillaDao.coreAnular(usuarioActual,id);
        }

        public PmPlantilla coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPlantilla)
        {
            return pmPlantillaDao.coreAnular(usuarioActual,  pPlantilla);
        }

        public void coreEliminar(PmPlantillaPk id)
        {
            pmPlantillaDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pPlantilla)
        {
            pmPlantillaDao.eliminardetalle(pPlantilla);

            pmPlantillaDao.coreEliminar( pPlantilla);
        }


        public PmPlantilla obtenerPorId(PmPlantillaPk id)
        {
            
            return pmPlantillaDao.obtenerPorId(id);
        }

        public PmPlantilla obtenerPorId(int pPlantilla)
        {
            PmPlantilla bean = pmPlantillaDao.obtenerPorId(new PmPlantillaPk() {Plantilla = pPlantilla }.obtenerArreglo());

            bean.listadetalle = pmPlantillaTareasDao.listarTareas(pPlantilla);

            
            return bean;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return pmPlantillaDao.listarPaginacion(paginacion, filtro);
        }

       
    }
}
