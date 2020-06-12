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
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dao;

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsConsumoPlantillaServicioImpl : GenericoServicioImpl, PsConsumoPlantillaServicio {

        private IServiceProvider servicioProveedor;
        private PsConsumoPlantillaDao psConsumoPlantillaDao;
        private PsConsumoPlantillaDetalleDao psConsumoPlantillaDetalleDao;
        private PsItemDao psItemDao;

        public PsConsumoPlantillaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psConsumoPlantillaDao = servicioProveedor.GetService<PsConsumoPlantillaDao>();
            psConsumoPlantillaDetalleDao = servicioProveedor.GetService<PsConsumoPlantillaDetalleDao>();
            psItemDao = servicioProveedor.GetService<PsItemDao>();
        }

        public PsConsumoPlantilla coreInsertar(UsuarioActual usuarioActual, PsConsumoPlantilla bean)
        {
            bean.Plantilla = psConsumoPlantillaDao.generarCodigo(bean.IdInstitucion);


            foreach (PsConsumoPlantillaDetalle deta in bean.listadetalle)
            {
                deta.IdInstitucion = bean.IdInstitucion;
                deta.Plantilla = bean.Plantilla;
                psConsumoPlantillaDetalleDao.coreInsertar(usuarioActual, deta);
            }
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psConsumoPlantillaDao.coreInsertar(usuarioActual,bean);
        }

        public PsConsumoPlantilla coreActualizar(UsuarioActual usuarioActual, PsConsumoPlantilla bean)
        {
            psConsumoPlantillaDao.eliminardetalle(bean.IdInstitucion,bean.Plantilla);

            foreach (PsConsumoPlantillaDetalle deta in bean.listadetalle)
            {
                deta.Plantilla = bean.Plantilla;
                deta.IdInstitucion = bean.IdInstitucion;
                psConsumoPlantillaDetalleDao.registrar(deta);


            }
            return psConsumoPlantillaDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(PsConsumoPlantillaPk id)
        {
            psConsumoPlantillaDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> plantilla)
        {


            psConsumoPlantillaDao.eliminardetalle(pIdInstitucion, plantilla);


            psConsumoPlantillaDao.coreEliminar( pIdInstitucion, plantilla);
        }


        public PsConsumoPlantilla obtenerPorId(PsConsumoPlantillaPk id)
        {
            return psConsumoPlantillaDao.obtenerPorId(id);
        }

        public PsConsumoPlantilla obtenerPorId(String pIdInstitucion, int pPlantilla)
        {
            PsConsumoPlantilla bean = psConsumoPlantillaDao.obtenerPorId(new PsConsumoPlantillaPk() {IdInstitucion = pIdInstitucion, Plantilla = pPlantilla }.obtenerArreglo());

            bean.listadetalle = psConsumoPlantillaDetalleDao.listarDetalle(pIdInstitucion, pPlantilla);


            foreach (var item in bean.listadetalle)
            {
                item.auxNombre = psItemDao.obtenerDescripcion(item.IdItem);
            }
            return bean;
        }

        public List<DtoConsumoPlantilla> listarPlantilla(DtoTabla filtro)
        {
            return psConsumoPlantillaDao.listarPlantilla(filtro);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return psConsumoPlantillaDao.listarPaginacion(paginacion, filtro);
        }

        public List<PsConsumoPlantilla> listarPlantillas(string institucion)
        {
            return psConsumoPlantillaDao.listarPlantillas(institucion);
        }

        public ParametroPaginacionGenerico listarPlantillasConsumo(DtoTabla filtro, ParametroPaginacionGenerico paginacion)
        {
            throw new NotImplementedException();
        }

        public ParametroPaginacionGenerico listarPlantilla(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            return psConsumoPlantillaDao.listarPlantilla(paginacion,filtro);
        }

        public PsConsumoPlantilla coreAnular(UsuarioActual usuarioActual, PsConsumoPlantillaPk id)
        {
            return psConsumoPlantillaDao.coreAnular(usuarioActual, id);
        }

        public PsConsumoPlantilla coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pPlantilla)
        {
            return psConsumoPlantillaDao.coreAnular(usuarioActual, pIdInstitucion, pPlantilla);
        }
    }
}
