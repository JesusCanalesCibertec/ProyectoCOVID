using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.rrhh.dao.impl
{


    public class HrEncuestapreguntaDaoImpl : GenericoDaoImpl<HrEncuestapregunta>, HrEncuestapreguntaDao
    {
        private IServiceProvider servicioProveedor;



        public HrEncuestapreguntaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrencuestapregunta")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrEncuestapregunta obtenerPorId(HrEncuestapreguntaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEncuestapregunta obtenerPorId(Nullable<Int32> pPregunta)
        {
            return obtenerPorId(new HrEncuestapreguntaPk(pPregunta));
        }

        public HrEncuestapregunta coreInsertar(UsuarioActual usuarioActual, HrEncuestapregunta bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrEncuestapregunta coreActualizar(UsuarioActual usuarioActual, HrEncuestapregunta bean, String estado)
        {
            if (UString.estaVacio(estado))
            {
                estado = ConstanteEstadoGenerico.ACTIVO;
            }
            bean.Estado = estado;   
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pPregunta)
        {
            coreEliminar(new HrEncuestapreguntaPk(pPregunta));
        }

        public void coreEliminar(HrEncuestapreguntaPk id)
        {
            HrEncuestapregunta bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrEncuestapregunta coreAnular(UsuarioActual usuarioActual, HrEncuestapreguntaPk id)
        {
            HrEncuestapregunta bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public HrEncuestapregunta coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPregunta)
        {
            return coreAnular(usuarioActual, new HrEncuestapreguntaPk(pPregunta));
        }

        public ParametroPaginacionGenerico listarEncuestas(ParametroPaginacionGenerico paginacion, FiltroEncuestaPregunta filtro)
        {
            Int32 contador = 0;

            List<DtoEncuestapregunta> lstRetorno = new List<DtoEncuestapregunta>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(filtro.TipoEncuesta))
            {
                filtro.TipoEncuesta = null;
            }
            if (UString.estaVacio(filtro.AreaEncuesta))
            {
                filtro.AreaEncuesta = null;
            }
            if (UString.estaVacio(filtro.EstadoEncuesta))
            {
                filtro.EstadoEncuesta = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_pregunta", ConstanteUtil.TipoDato.Integer, filtro.CodEncuesta));
            parametros.Add(new ParametroPersistenciaGenerico("p_descripcion", ConstanteUtil.TipoDato.String, filtro.DesEncuesta));
            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.String, filtro.TipoEncuesta));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.AreaEncuesta));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.EstadoEncuesta));

            contador = this.contar("hrencuestapregunta.filtrocontar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "hrencuestapregunta.filtropaginacion");

            while (_reader.Read())
            {
                DtoEncuestapregunta bean = new DtoEncuestapregunta();

                if (!_reader.IsDBNull(0))
                    bean.pregunta = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.descripcion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.tipoencuestaId = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.tipoencuestaNombre = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.areaId = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.areaNombre = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.estado = _reader.GetString(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public int generarCodigo()
        {
            IQueryable<HrEncuestapregunta> query = this.getCriteria();

            List<HrEncuestapregunta> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.Pregunta));
            }

            return var + 1;
        }

        public List<HrEncuestadetalle> listarPorPlantilla(int id)
        {
            List<HrEncuestadetalle> lstRetorno = new List<HrEncuestadetalle>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_plantilla", ConstanteUtil.TipoDato.Integer, id));

            _reader = this.listarPorQuery("hrencuestapregunta.listarPorPlantilla", parametros);

            while (_reader.Read())
            {
                HrEncuestadetalle bean = new HrEncuestadetalle();

                if (!_reader.IsDBNull(0))
                    bean.Pregunta = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.auxPregunta = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.auxArea = _reader.GetString(2);

                bean.orden = 0;

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public string obtenerDescripcion(int pregunta)
        {
            IQueryable<HrEncuestapregunta> query = this.getCriteria();
            query = query.Where(p => p.Pregunta == pregunta);
        
            List<HrEncuestapregunta> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Descripcion;
            return null;
        }
    }
}
