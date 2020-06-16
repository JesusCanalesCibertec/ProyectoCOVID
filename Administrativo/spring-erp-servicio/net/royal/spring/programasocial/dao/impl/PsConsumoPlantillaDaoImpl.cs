using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.dao.impl
{
public class PsConsumoPlantillaDaoImpl : GenericoDaoImpl<PsConsumoPlantilla>, PsConsumoPlantillaDao
{
        private IServiceProvider servicioProveedor;


        public PsConsumoPlantillaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psconsumoplantilla")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsConsumoPlantilla obtenerPorId(PsConsumoPlantillaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsConsumoPlantilla obtenerPorId(String pIdInstitucion,Nullable<Int32> plantilla)
        {
            return obtenerPorId(new PsConsumoPlantillaPk( pIdInstitucion, plantilla));
        }

        public PsConsumoPlantilla coreInsertar(UsuarioActual usuarioActual, PsConsumoPlantilla bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsConsumoPlantilla coreActualizar(UsuarioActual usuarioActual, PsConsumoPlantilla bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> plantilla)
        {
            coreEliminar(new PsConsumoPlantillaPk( pIdInstitucion, plantilla));
        }

        public void coreEliminar(PsConsumoPlantillaPk id)
        {
            PsConsumoPlantilla bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsConsumoPlantilla coreAnular(UsuarioActual usuarioActual,PsConsumoPlantillaPk id)
        {
            PsConsumoPlantilla bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            bean.Estado = ConstanteEstadoGenerico.INACTIVO;
            return coreActualizar(usuarioActual,bean);
        }

        public PsConsumoPlantilla coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pPlantilla)
        {
            return coreAnular(usuarioActual,new PsConsumoPlantillaPk( pIdInstitucion, pPlantilla));
        }

        public List<DtoConsumoPlantilla> listarPlantilla(DtoTabla filtro)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoConsumoPlantilla> lst = new List<DtoConsumoPlantilla>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.Codigo));
            _parametros.Add(new ParametroPersistenciaGenerico("p_plantilla", ConstanteUtil.TipoDato.Integer, filtro.CodigoNumerico));



            _reader = this.listarPorQuery("psconsumoplantilla.filtro", _parametros);

            while (_reader.Read())
            {
                DtoConsumoPlantilla bean = new DtoConsumoPlantilla();

                if (!_reader.IsDBNull(0))
                    bean.codInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.plantilla = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.nomItem = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nomUnidad = _reader.GetString(3);

              

                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            Int32 contador = 0;

            List<DtoTabla> lstRetorno = new List<DtoTabla>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();


            if (UString.estaVacio(filtro.Codigo))
            {
                filtro.Codigo = null;
            }
            if (UString.estaVacio(filtro.Nombre))
            {
                filtro.Nombre = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.CodigoNumerico));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.label));
            parametros.Add(new ParametroPersistenciaGenerico("p_descripcion", ConstanteUtil.TipoDato.String, filtro.Descripcion));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));




            contador = this.contar("psconsumoplantilla.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psconsumoplantilla.filtroPaginacion");

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.CodigoNumerico = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Estado = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Codigo = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Nombre = _reader.GetString(4);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public int generarCodigo(string institucion)
        {
            IQueryable<PsConsumoPlantilla> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == institucion);
            List<PsConsumoPlantilla> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.Plantilla));
            }

            return var + 1;
        }

        public void eliminardetalle(string institucion, int? plantilla)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, institucion));
            _parametros.Add(new ParametroPersistenciaGenerico("p_plantilla", ConstanteUtil.TipoDato.Integer, plantilla));



            this.ejecutarPorQuery("psconsumoplantilla.eliminardetalle", _parametros);
        }

        public List<PsConsumoPlantilla> listarPlantillas(string institucion)
        {
            IQueryable<PsConsumoPlantilla> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == institucion);
            query = query.Where(p => p.Estado == "A");
            List<PsConsumoPlantilla> lst = query.ToList();

            return lst;
        }

        public ParametroPaginacionGenerico listarPlantilla(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            Int32 contador = 0;

            List<DtoConsumoPlantilla> lstRetorno = new List<DtoConsumoPlantilla>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.Codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_plantilla", ConstanteUtil.TipoDato.Integer, filtro.CodigoNumerico));

            contador = this.contar("psconsumoplantilla.paginacionContar", parametros);

            _reader = this.listarPorQuery("psconsumoplantilla.paginacionFiltro", parametros);

            while (_reader.Read())
            {
                DtoConsumoPlantilla bean = new DtoConsumoPlantilla();

                if (!_reader.IsDBNull(0))
                    bean.codInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.codItem = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nomItem = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nomUnidad = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.plantilla = _reader.GetInt32(4);



                lstRetorno.Add(bean);
            }
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRetorno;

            return paginacion;
        }
    }
}
