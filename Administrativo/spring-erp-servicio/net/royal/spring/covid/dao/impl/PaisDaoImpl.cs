using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.covid.dao;
using net.royal.spring.covid.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.constante;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.framework.core;

namespace net.royal.spring.covid.dao.impl
{
    public class PaisDaoImpl : GenericoDaoImpl<Pais>, PaisDao
    {
        private IServiceProvider servicioProveedor;


        public PaisDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pais")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarUbigeoPorFiltro(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoUbigeo> lstRegistros = new List<DtoUbigeo>();

            if (UString.estaVacio(filtro.valor1))
                filtro.valor1 = "001";
            if (UString.estaVacio(filtro.valor2))
                filtro.valor2 = null;
            if (UString.estaVacio(filtro.valor3))
                filtro.valor3 = null;

            if (UString.estaVacio(filtro.Codigo))
                filtro.Codigo = null;
            if (UString.estaVacio(filtro.Nombre))
                filtro.Nombre = null;
            if (UString.estaVacio(filtro.Descripcion))
                filtro.Descripcion = "ALL";

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.Codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_descripcion", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_des2", ConstanteUtil.TipoDato.String, filtro.Descripcion));
            parametros.Add(new ParametroPersistenciaGenerico("p_pais", ConstanteUtil.TipoDato.String, filtro.valor1));
            parametros.Add(new ParametroPersistenciaGenerico("p_dep", ConstanteUtil.TipoDato.String, filtro.valor2));
            parametros.Add(new ParametroPersistenciaGenerico("p_prov", ConstanteUtil.TipoDato.String, filtro.valor3));

            Int32 contador = this.contar("pais.contarubicaciongeografica", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "pais.listarubicaciongeografica");
            while (_reader.Read())
            {
                DtoUbigeo bean = new DtoUbigeo();
                if (!_reader.IsDBNull(0))
                    bean.DepartamentoNombre = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.ProvinciaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.ZonaPostalNombre = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.Departamento = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Provincia = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.ZonaPostal = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Descripcion = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.codigoElemento = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.Pais = _reader.GetString(8);
                lstRegistros.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }

        public DtoTabla obtenerUbigeo(String ubigeo)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
           
            _parametros.Add(new ParametroPersistenciaGenerico("ubigeo", ConstanteUtil.TipoDato.String, ubigeo));
            _reader = this.listarPorQuery("pais.obtenerUbigeo", _parametros);

            DtoTabla bean = new DtoTabla();
            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);

            }
            this.dispose();
            return bean;

        }

        public Pais obtenerPorId(PaisPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
