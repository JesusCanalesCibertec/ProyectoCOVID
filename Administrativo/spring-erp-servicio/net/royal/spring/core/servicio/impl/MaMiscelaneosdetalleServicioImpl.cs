using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio.dto;
using System.Linq;
using net.royal.spring.framework.constante;

namespace net.royal.spring.core.servicio.impl
{

    public class MaMiscelaneosdetalleServicioImpl : GenericoServicioImpl, MaMiscelaneosdetalleServicio
    {

        private IServiceProvider servicioProveedor;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;
        private MaMiscelaneosheaderDao miscelaneosheaderDao;

        public MaMiscelaneosdetalleServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
            miscelaneosheaderDao = servicioProveedor.GetService<MaMiscelaneosheaderDao>();
        }

        public MaMiscelaneosdetalle actualizar(MaMiscelaneosdetalle maMiscelaneosdetalle)
        {
            maMiscelaneosdetalleDao.actualizar(maMiscelaneosdetalle);
            return maMiscelaneosdetalle;
        }

        public void eliminar(MaMiscelaneosdetallePk maMiscelaneosdetallePk)
        {
            MaMiscelaneosdetalle maMiscelaneosdetalle = maMiscelaneosdetalleDao.obtenerPorId(maMiscelaneosdetallePk);
            maMiscelaneosdetalleDao.eliminar(maMiscelaneosdetalle);
        }

        public List<MaMiscelaneosdetalle> listar(FiltroMiscelaneosDetalle filtro)
        {
            return maMiscelaneosdetalleDao.listar(filtro);
        }

        public List<MaMiscelaneosdetalle> listarActivos(MaMiscelaneosheaderPk maMiscelaneosheaderPk)
        {
            return maMiscelaneosdetalleDao.listarActivos(maMiscelaneosheaderPk);
        }

        public List<MaMiscelaneosdetalle> listarActivos(string codigoTabla)
        {
            FiltroMiscelaneosDetalle filtro = new FiltroMiscelaneosDetalle();
            filtro.CodigoTabla = codigoTabla;
            filtro.Estado = "A";
            return listar(filtro);
        }

        public List<MaMiscelaneosdetalle> listarActivos(string aplicacionCodigo, string codigoTabla)
        {

            FiltroMiscelaneosDetalle filtro = new FiltroMiscelaneosDetalle();
            filtro.CodigoAplicacion = aplicacionCodigo;
            filtro.CodigoTabla = codigoTabla;
            filtro.Estado = "A";
            return listar(filtro);
        }
        public List<MaMiscelaneosdetalle> listarActivosPorCodigo1(String aplicacionCodigo, String codigoTabla, String valorCodigo1)
        {
            FiltroMiscelaneosDetalle filtro = new FiltroMiscelaneosDetalle();
            filtro.CodigoAplicacion = aplicacionCodigo;
            filtro.CodigoTabla = codigoTabla;
            filtro.Estado = "A";
            filtro.ValorCodigo1 = valorCodigo1;
            return listar(filtro);
        }

        public MaMiscelaneosdetalle obtenerPorId(MaMiscelaneosdetallePk maMiscelaneosdetallePk)
        {
            return maMiscelaneosdetalleDao.obtenerPorId(maMiscelaneosdetallePk);
        }

        public MaMiscelaneosdetalle registrar(MaMiscelaneosdetalle maMiscelaneosdetalle)
        {
            if(maMiscelaneosdetalle.Codigoelemento == null)
            {
                maMiscelaneosdetalle.Codigoelemento = maMiscelaneosdetalleDao.generarCodigo();
            }
            maMiscelaneosdetalle.Estado = ConstanteEstadoGenerico.ACTIVO;
            MaMiscelaneosdetalle bean = maMiscelaneosdetalleDao.registrar(maMiscelaneosdetalle);
            return bean;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, MaMiscelaneosdetalle filtro)
        {
            return maMiscelaneosdetalleDao.listarPaginacion(paginacion, filtro);
        }

        public List<MaMiscelaneosdetalle> listarEnSentencia(FiltroMiscelaneosDetalle filtro)
        {
            return maMiscelaneosdetalleDao.listarEnSentencia(filtro);
        }

        public String obtenerDescripcionPorId(MaMiscelaneosdetallePk maMiscelaneosdetallePk)
        {
            MaMiscelaneosdetalle d = maMiscelaneosdetalleDao.obtenerPorId(maMiscelaneosdetallePk);
            if (d == null)
                return "";
            return d.Descripcionlocal;
        }

        public String obtenerDescripcionPorId(String aplicacion, String tabla, String elemento)
        {
            elemento = UString.obtenerValorCadenaSinNulo(elemento).Trim();
            MaMiscelaneosdetallePk pk = new MaMiscelaneosdetallePk();
            pk.Aplicacioncodigo = aplicacion;
            pk.Codigoelemento = elemento;
            pk.Codigotabla = tabla;
            pk.Compania = "999999";
            return obtenerDescripcionPorId(pk);
        }

        public List<MaMiscelaneosdetalle> listarDetalle(MaMiscelaneosheaderPk llave)
        {
            return maMiscelaneosdetalleDao.listarDetalle(llave);
        }

        public List<MaMiscelaneosdetalle> listarActivosBean(string aplicacionCodigo, string codigoTabla)
        {
            return maMiscelaneosdetalleDao.listarActivosBean(aplicacionCodigo, codigoTabla);
        }

        public List<DtoTabla> listarHeaderPorAplicacion(string aplicacionCodigo, string compania)
        {
            List<DtoTabla> lista = new List<DtoTabla>();
            IQueryable<MaMiscelaneosheader> query = miscelaneosheaderDao.getCriteria();
            query = query.Where(x => x.Aplicacioncodigo == aplicacionCodigo);
            query = query.Where(x => x.Compania == compania);

            foreach (var item in query.ToList())
            {
                lista.Add(new DtoTabla() { Codigo = item.Codigotabla.Trim(), Nombre = item.Descripcionlocal });
            }
            return lista;
        }

        
    }
}
