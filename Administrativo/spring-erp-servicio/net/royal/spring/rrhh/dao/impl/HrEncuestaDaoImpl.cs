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
    public class HrEncuestaDaoImpl : GenericoDaoImpl<HrEncuesta>, HrEncuestaDao
    {
        private IServiceProvider servicioProveedor;


        public HrEncuestaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrencuesta")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrEncuesta obtenerPorId(HrEncuestaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEncuesta obtenerPorId(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia)
        {
            return obtenerPorId(new HrEncuestaPk(pCompanyowner, pPeriodo, pSecuencia));
        }

        public HrEncuesta coreInsertar(UsuarioActual usuarioActual, HrEncuesta bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrEncuesta coreActualizar(UsuarioActual usuarioActual, HrEncuesta bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia)
        {
            coreEliminar(new HrEncuestaPk(pCompanyowner, pPeriodo, pSecuencia));
        }

        public void coreEliminar(HrEncuestaPk id)
        {
            HrEncuesta bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrEncuesta coreAnular(UsuarioActual usuarioActual, HrEncuestaPk id)
        {
            HrEncuesta bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public HrEncuesta coreAnular(UsuarioActual usuarioActual, String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia)
        {
            return coreAnular(usuarioActual, new HrEncuestaPk(pCompanyowner, pPeriodo, pSecuencia));
        }

        public ParametroPaginacionGenerico listar(FiltroPaginacionEncuesta filtroPaginacion)
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoEncuesta> lstRegistros = new List<DtoEncuesta>();

            if (UInteger.esCeroOrNulo(filtroPaginacion.secuencia))
                filtroPaginacion.secuencia = null;
            if (UString.estaVacio(filtroPaginacion.periodo))
            {
                filtroPaginacion.periodo = null;
            }
            else
            {
                filtroPaginacion.periodo = filtroPaginacion.periodo.Replace("-", string.Empty);
            }
            if (UString.estaVacio(filtroPaginacion.titulo))
                filtroPaginacion.titulo = null;
            if (UString.estaVacio(filtroPaginacion.estado))
                filtroPaginacion.estado = null;
            if (UString.estaVacio(filtroPaginacion.compania))
                filtroPaginacion.compania = null;


            if (UString.estaVacio(filtroPaginacion.tipo))
                filtroPaginacion.tipo = null;
            if (UString.estaVacio(filtroPaginacion.programa))
                filtroPaginacion.programa = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, filtroPaginacion.secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtroPaginacion.periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_titulo", ConstanteUtil.TipoDato.String, filtroPaginacion.titulo));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtroPaginacion.estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtroPaginacion.compania));
            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.String, filtroPaginacion.tipo));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, filtroPaginacion.programa));
            parametros.Add(new ParametroPersistenciaGenerico("p_encuesta", ConstanteUtil.TipoDato.String, filtroPaginacion.encuesta));

            Int32 contador = this.contar("hrencuesta.listarContar", parametros);

            _reader = this.listarConPaginacion(filtroPaginacion.paginacion, parametros, "hrencuesta.listarPaginacion");
            while (_reader.Read())
            {
                DtoEncuesta bean = new DtoEncuesta();
                if (!_reader.IsDBNull(0))
                    bean.secuencia = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.periodo = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.titulo = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.muestras = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.estadoDescripcion = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.registrar = _reader.GetInt32(5) == 1 ? true : false;
                if (!_reader.IsDBNull(6))
                    bean.fecha = _reader.GetDateTime(6);
                if (!_reader.IsDBNull(7))
                    bean.desde = _reader.GetDateTime(7);
                if (!_reader.IsDBNull(8))
                    bean.hasta = _reader.GetDateTime(8);
                if (!_reader.IsDBNull(9))
                    bean.compania = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.muestrasDescripcion = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.tipoDescripcion = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.finalizada = _reader.GetInt32(12);
                lstRegistros.Add(bean);
            }
            this.dispose();

            filtroPaginacion.paginacion.RegistroEncontrados = contador;
            filtroPaginacion.paginacion.ListaResultado = lstRegistros;

            return filtroPaginacion.paginacion;
        }

        public ParametroPaginacionGenerico listarSujetos(FiltroPaginacionSujeto filtroPaginacion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoEncuestaSujeto> lstRegistros = new List<DtoEncuestaSujeto>();

            if (UInteger.esCeroOrNulo(filtroPaginacion.sujeto))
                filtroPaginacion.sujeto = null;

            if (!filtroPaginacion.fecha.HasValue)
            {
                filtroPaginacion.fecha = new DateTime(2018, 01, 01);
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, filtroPaginacion.secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtroPaginacion.periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtroPaginacion.companyowner));
            parametros.Add(new ParametroPersistenciaGenerico("p_sujeto", ConstanteUtil.TipoDato.Integer, filtroPaginacion.sujeto));
            parametros.Add(new ParametroPersistenciaGenerico("p_dia", ConstanteUtil.TipoDato.Date, filtroPaginacion.fecha));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtroPaginacion.institucion));

            Int32 contador = this.contar("hrencuesta.contarSujetos", parametros);

            _reader = this.listarConPaginacion(filtroPaginacion.paginacion, parametros, "hrencuesta.listarSujetosPaginacion");
            while (_reader.Read())
            {
                DtoEncuestaSujeto bean = new DtoEncuestaSujeto();
                if (!_reader.IsDBNull(0))
                    bean.sujeto = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.sexo = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.edad = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.fecha = _reader.GetDateTime(3);
                if (!_reader.IsDBNull(4))
                    bean.institucion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.tipo = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.residencia = _reader.GetString(6);

                lstRegistros.Add(bean);
            }
            this.dispose();

            filtroPaginacion.paginacion.RegistroEncontrados = contador;
            filtroPaginacion.paginacion.ListaResultado = lstRegistros;

            return filtroPaginacion.paginacion;
        }

        public int? generarSecuencia(HrEncuestaPk hrEncuestaPk)
        {
            IQueryable<HrEncuesta> query = this.getCriteria();
            query = query.Where(x => x.Companyowner == hrEncuestaPk.Companyowner);
            query = query.Where(x => x.Periodo == hrEncuestaPk.Periodo);
            Int32? contador = query.Select(p => p.Secuencia).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public List<DtoEncuestaAnalisis> analizarEncuesta(FiltroEncuestaAnalisis filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoEncuestaAnalisis> lstRegistros = new List<DtoEncuestaAnalisis>();


            if (UString.estaVacio(filtro.institucion))
                filtro.institucion = null;
            if (UString.estaVacio(filtro.institucionArea))
                filtro.institucionArea = null;
            if (UString.estaVacio(filtro.area))
                filtro.area = null;
            if (UString.estaVacio(filtro.componente))
                filtro.componente = null;
            if (UString.estaVacio(filtro.programa))
                filtro.programa = null;
            if (UString.estaVacio(filtro.sexo))
                filtro.sexo = null;
            if (UString.estaVacio(filtro.edad))
                filtro.edad = null;

            if (UString.estaVacio(filtro.miscelaneo1))
                filtro.miscelaneo1 = null;
            if (UString.estaVacio(filtro.miscelaneo2))
                filtro.miscelaneo2 = null;
            if (UString.estaVacio(filtro.miscelaneo3))
                filtro.miscelaneo3 = null;
            if (UString.estaVacio(filtro.miscelaneo4))
                filtro.miscelaneo4 = null;
            if (UString.estaVacio(filtro.afe))
                filtro.afe = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.companyonwer));
            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, filtro.secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.periodo));

            parametros.Add(new ParametroPersistenciaGenerico("p_m1", ConstanteUtil.TipoDato.String, filtro.miscelaneo1));
            parametros.Add(new ParametroPersistenciaGenerico("p_m2", ConstanteUtil.TipoDato.String, filtro.miscelaneo2));
            parametros.Add(new ParametroPersistenciaGenerico("p_m3", ConstanteUtil.TipoDato.String, filtro.miscelaneo3));
            parametros.Add(new ParametroPersistenciaGenerico("p_m4", ConstanteUtil.TipoDato.String, filtro.miscelaneo4));

            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.institucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_iArea", ConstanteUtil.TipoDato.String, filtro.institucionArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.area));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, filtro.programa));
            parametros.Add(new ParametroPersistenciaGenerico("p_componente", ConstanteUtil.TipoDato.String, filtro.componente));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.sexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_edad", ConstanteUtil.TipoDato.String, filtro.edad));
            parametros.Add(new ParametroPersistenciaGenerico("p_afe", ConstanteUtil.TipoDato.String, filtro.afe));

            _reader = this.listarPorQuery("hrencuesta.analizarEncuesta", parametros);
            while (_reader.Read())
            {
                DtoEncuestaAnalisis bean = new DtoEncuestaAnalisis();

                if (!_reader.IsDBNull(0))
                    bean.area = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.pregunta = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.respuesta = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.cantidad = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.peso = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.puntaje = _reader.GetDecimal(5);
                if (!_reader.IsDBNull(6))
                    bean.maximo = _reader.GetDecimal(6);

                lstRegistros.Add(bean);
            }
            this.dispose();

            return lstRegistros;
        }

        public ParametroPaginacionGenerico consulta(FiltroPaginacionEncuesta filtroPaginacion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoEncuesta> lstRegistros = new List<DtoEncuesta>();

            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, filtroPaginacion.secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_indicador", ConstanteUtil.TipoDato.String, filtroPaginacion.Indicador));

            Int32 contador = 0;

            if (!String.IsNullOrEmpty(filtroPaginacion.Pregunta))
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_pregunta", ConstanteUtil.TipoDato.ArrayInteger, filtroPaginacion.Pregunta));
                contador = this.contar("hrencuesta.contarEncuestaIN", parametros);
                _reader = this.listarConPaginacion(filtroPaginacion.paginacion, parametros, "hrencuesta.consultarEncuestaIN");
            }
            else
            {
                contador = this.contar("hrencuesta.contarEncuesta", parametros);
                _reader = this.listarConPaginacion(filtroPaginacion.paginacion, parametros, "hrencuesta.consultarEncuesta");
            }



            while (_reader.Read())
            {
                DtoEncuesta bean = new DtoEncuesta();
                if (!_reader.IsDBNull(0))
                    bean.secuencia = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.Sujeto = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.Pregunta = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.Descripcion = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Valor = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.DescripcionValor = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Peso = _reader.GetDecimal(6);

                lstRegistros.Add(bean);
            }
            this.dispose();

            filtroPaginacion.paginacion.RegistroEncontrados = contador;
            filtroPaginacion.paginacion.ListaResultado = lstRegistros;

            return filtroPaginacion.paginacion;
        }
    }
}
