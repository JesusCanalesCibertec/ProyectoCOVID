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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrEncuestadetalleDaoImpl : GenericoDaoImpl<HrEncuestadetalle>, HrEncuestadetalleDao
    {
        private IServiceProvider servicioProveedor;


        public HrEncuestadetalleDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrencuestadetalle")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrEncuestadetalle obtenerPorId(HrEncuestadetallePk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEncuestadetalle obtenerPorId(Nullable<Int32> pSecuencia, Nullable<Int32> pPregunta)
        {
            return obtenerPorId(new HrEncuestadetallePk(pSecuencia, pPregunta));
        }

        public HrEncuestadetalle coreInsertar(UsuarioActual usuarioActual, HrEncuestadetalle bean)
        {
            this.registrar(bean);
            return bean;
        }

        public HrEncuestadetalle coreActualizar(UsuarioActual usuarioActual, HrEncuestadetalle bean)
        {
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pSecuencia, Nullable<Int32> pPregunta)
        {
            coreEliminar(new HrEncuestadetallePk(pSecuencia, pPregunta));
        }

        public void coreEliminar(HrEncuestadetallePk id)
        {
            HrEncuestadetalle bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrEncuestadetalle coreAnular(UsuarioActual usuarioActual, HrEncuestadetallePk id)
        {
            HrEncuestadetalle bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public HrEncuestadetalle coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pSecuencia, Nullable<Int32> pPregunta)
        {
            return coreAnular(usuarioActual, new HrEncuestadetallePk(pSecuencia, pPregunta));
        }

        public IList<DtoPreguntas> obtenerPreguntas(HrEncuestaPk hrEncuestaPk)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPreguntas> lstRegistros = new List<DtoPreguntas>();

            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, hrEncuestaPk.Secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, hrEncuestaPk.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, hrEncuestaPk.Companyowner));

            _reader = this.listarPorQuery("hrencuestadetalle.obtenerPreguntas", parametros);

            while (_reader.Read())
            {
                DtoPreguntas bean = new DtoPreguntas();
                if (!_reader.IsDBNull(0))
                    bean.pregunta = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.orden = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.conValor = _reader.GetInt32(2) == 1 ? true : false;
                if (!_reader.IsDBNull(3))
                    bean.conComentario = _reader.GetInt32(3) == 1 ? true : false;
                if (!_reader.IsDBNull(4))
                    bean.preguntaDescripcion = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.tipo = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.area = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.requiereFlag = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.requierePregunta = _reader.GetInt32(8);
                if (!_reader.IsDBNull(9))
                    bean.requiereValor = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.grupo = _reader.GetString(10);

                lstRegistros.Add(bean);
            }
            this.dispose();

            return lstRegistros;
        }

        public IList<DtoTabla> obtenerAreas(HrEncuestaPk hrEncuestaPk)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lstRegistros = new List<DtoTabla>();

            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, hrEncuestaPk.Secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, hrEncuestaPk.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, hrEncuestaPk.Companyowner));

            _reader = this.listarPorQuery("hrencuestadetalle.obtenerAreas", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);

                lstRegistros.Add(bean);
            }
            this.dispose();

            return lstRegistros;
        }

        public List<HrEncuestadetalle> listarPorEncuesta(HrEncuestaPk pk)
        {
            IQueryable<HrEncuestadetalle> query = getCriteria();
            query = query.Where(x => x.Companyowner == pk.Companyowner);
            query = query.Where(x => x.Periodo == pk.Periodo);
            query = query.Where(x => x.Secuencia == pk.Secuencia);
            query = query.OrderBy(p => p.orden);
            return query.ToList();
        }

        public void eliminarPorEncuesta(HrEncuestaPk hrEncuestaPk)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, hrEncuestaPk.Secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, hrEncuestaPk.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, hrEncuestaPk.Companyowner));

            this.ejecutarPorQuery("hrencuestadetalle.eliminarPorEncuesta", parametros);
            this.dispose();
        }
    }
}
