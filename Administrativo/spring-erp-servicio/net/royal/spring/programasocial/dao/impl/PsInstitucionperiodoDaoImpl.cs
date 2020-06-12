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
    public class PsInstitucionperiodoDaoImpl : GenericoDaoImpl<PsInstitucionperiodo>, PsInstitucionperiodoDao
    {
        private IServiceProvider servicioProveedor;


        public PsInstitucionperiodoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psinstitucionperiodo")
        {
            servicioProveedor = _servicioProveedor;
        }

        public void copiarPeriodo(DtoInstitucionperiodo dtoInstitucionperiodo)
        {

            List<DtoInstitucionperiodo> lst = new List<DtoInstitucionperiodo>();

            lst = this.listado(dtoInstitucionperiodo.codInstitucion, dtoInstitucionperiodo.codPeriodoCopiar);

            if (lst.Count == 0)
            {
                throw new UException("El periodo que se intenta copiar no existe");

            }

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            _parametros.Add(new ParametroPersistenciaGenerico("pIdInstitucion", ConstanteUtil.TipoDato.String, dtoInstitucionperiodo.codInstitucion));
            _parametros.Add(new ParametroPersistenciaGenerico("pPeriodo", ConstanteUtil.TipoDato.String, dtoInstitucionperiodo.codPeriodo));
            _parametros.Add(new ParametroPersistenciaGenerico("pcopiar", ConstanteUtil.TipoDato.String, dtoInstitucionperiodo.codPeriodoCopiar));
            _parametros.Add(new ParametroPersistenciaGenerico("pFechaInicio", ConstanteUtil.TipoDato.Date, dtoInstitucionperiodo.fechainicio));
            _parametros.Add(new ParametroPersistenciaGenerico("pFechaFin", ConstanteUtil.TipoDato.Date, dtoInstitucionperiodo.fechafin));
            _parametros.Add(new ParametroPersistenciaGenerico("pFechaRegistroInicio", ConstanteUtil.TipoDato.Date, dtoInstitucionperiodo.fechainicioregistro));
            _parametros.Add(new ParametroPersistenciaGenerico("pFechaRegistroFin", ConstanteUtil.TipoDato.Date, dtoInstitucionperiodo.fechafinregistro));

            List<DtoTabla> lista = this.periodoListarSimple2(dtoInstitucionperiodo.codPeriodo, dtoInstitucionperiodo.codInstitucion);
            if (lista.Count > 0)
            {
                throw new UException("El nuevo periodo que intenta registrar ya existe");

            }
            else
            {
                this.ejecutarPorQuery("psinstitucionperiodo.copiarPeriodo", _parametros);
            }
        }

        public List<DtoInstitucionperiodo> listado(string pIdInstitucion, string pIdPeriodo)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();

            if (String.IsNullOrEmpty(pIdInstitucion))
            {
                pIdInstitucion = null;
            }

            _parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, pIdInstitucion));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, pIdPeriodo));

            List<DtoInstitucionperiodo> lst = new List<DtoInstitucionperiodo>();

            _reader = this.listarPorQuery("psinstitucionperiodo.filtro", _parametros);

            while (_reader.Read())
            {
                DtoInstitucionperiodo bean = new DtoInstitucionperiodo();

                if (!_reader.IsDBNull(0))
                    bean.codInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.codAplicacion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.codGrupo = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.codConcepto = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.codPeriodo = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.fechainicio = _reader.GetDateTime(5);

                if (!_reader.IsDBNull(6))
                    bean.fechafin = _reader.GetDateTime(6);

                if (!_reader.IsDBNull(7))
                    bean.nomConcepto = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.fechainicioregistro = _reader.GetDateTime(8);

                if (!_reader.IsDBNull(9))
                    bean.fechafinregistro = _reader.GetDateTime(9);


                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }

        public List<DtoTabla> listarPeriodoPorComponente(string pIdInstitucion, string pIdPrograma, string pIdComponente)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, pIdInstitucion));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, pIdPrograma));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdComponente", ConstanteUtil.TipoDato.String, pIdComponente));

            List<DtoTabla> lst = new List<DtoTabla>();

            _reader = this.listarPorQuery("psinstitucionperiodo.listarPeriodos", _parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);
                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }

        public List<DtoTabla> periodoListarSimple(string idPeriodo)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            _parametros.Add(new ParametroPersistenciaGenerico("pPeriodo", ConstanteUtil.TipoDato.String, idPeriodo));

            List<DtoTabla> lst = new List<DtoTabla>();

            _reader = this.listarPorQuery("psinstitucionperiodo.listarPeriodosSimple", _parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);
                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }
        public List<DtoTabla> periodoListarSimple2(string idPeriodo, string ins)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            _parametros.Add(new ParametroPersistenciaGenerico("pPeriodo", ConstanteUtil.TipoDato.String, idPeriodo));
            _parametros.Add(new ParametroPersistenciaGenerico("pIns", ConstanteUtil.TipoDato.String, ins));

            List<DtoTabla> lst = new List<DtoTabla>();

            _reader = this.listarPorQuery("psinstitucionperiodo.listarPeriodosSimple2", _parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);
                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }
    }
}
