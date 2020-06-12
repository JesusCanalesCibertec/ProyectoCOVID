
using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using Microsoft.EntityFrameworkCore;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;

namespace net.royal.spring.core.dao.impl {
    public class MaMiscelaneosdetalleDaoImpl : GenericoDaoImpl<MaMiscelaneosdetalle>, MaMiscelaneosdetalleDao {
        private IServiceProvider servicioProveedor;

        public MaMiscelaneosdetalleDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "mamiscelaneosdetalle") {
            servicioProveedor = _servicioProveedor;
        }

        public List<MaMiscelaneosdetalle> listar(FiltroMiscelaneosDetalle filtro) {
            IQueryable<MaMiscelaneosdetalle> query = this.getCriteria();

            if (!UString.estaVacio(filtro.CodigoAplicacion))
                query = query.Where(p => p.Aplicacioncodigo == filtro.CodigoAplicacion);
          
            if (!UString.estaVacio(filtro.CodigoTabla))
                query = query.Where(p => p.Codigotabla == filtro.CodigoTabla);
            if (!UString.estaVacio(filtro.CodigoCompania))
                query = query.Where(p => p.Compania == filtro.CodigoCompania);
            if (!UString.estaVacio(filtro.CodigoElemento))
                query = query.Where(p => p.Codigoelemento == filtro.CodigoElemento);
            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcionlocal.Contains(filtro.Nombre));   /** like **/
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);
            if (!UString.estaVacio(filtro.ValorCodigo1))
                query = query.Where(p => p.ValorCodigo1 == filtro.ValorCodigo1);

            //query = query.OrderBy(p => p.Orden!=null).ThenBy(p => p.Orden).ThenBy(p => p.Descripcionlocal);
            //if (!String.estaVacio(filtro.AtributoOrdenar))
            //query = query.OrderBy(p => new { p.Orden, p.Descripcionlocal });     /** order **/
            //else
            //    query = query.OrderBy(p => filtro.AtributoOrdenar);
            query = query.OrderBy(p => p.Descripcionlocal);

            List<MaMiscelaneosdetalle> s = query.ToList();
            s.ForEach(reg => _context.Entry(reg).State = EntityState.Detached);
            s.ForEach(reg => reg.Codigoelemento = reg.Codigoelemento.Trim());
            return s;
        }

        public List<MaMiscelaneosdetalle> listarActivos(MaMiscelaneosheaderPk maMiscelaneosheaderPk) {
            IQueryable<MaMiscelaneosdetalle> query = this.getCriteria();
            query = query.Where(p => p.Aplicacioncodigo == maMiscelaneosheaderPk.Aplicacioncodigo);
            query = query.Where(p => p.Codigotabla == maMiscelaneosheaderPk.Codigotabla);
            query = query.Where(p => p.Compania == maMiscelaneosheaderPk.Compania);
            query = query.Where(p => p.Estado == "A");
            query = query.OrderBy(p => p.Codigoelemento);
            return query.ToList();
        }

        public string obtenerDescripcion(string aplicacioncodigo, string codigotabla, string codigoelemento) {
            IQueryable<MaMiscelaneosdetalle> query = this.getCriteria();
            query = query.Where(p => p.Aplicacioncodigo == aplicacioncodigo);
            query = query.Where(p => p.Codigotabla == codigotabla);
            query = query.Where(p => p.Codigoelemento == codigoelemento);
            List<MaMiscelaneosdetalle> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Descripcionlocal;
            return null;
        }

        public MaMiscelaneosdetalle obtenerPorId(MaMiscelaneosdetallePk pk) {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, MaMiscelaneosdetalle filtro) {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<MaMiscelaneosdetalle> lst = new List<MaMiscelaneosdetalle>();

            if (UString.estaVacio(filtro.Codigoelemento))
                filtro.Codigoelemento = null;

            if (UString.estaVacio(filtro.Descripcionlocal))
                filtro.Descripcionlocal = null;

            if (UString.estaVacio(filtro.Estado))
                filtro.Estado = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_aplicacion", ConstanteUtil.TipoDato.String, filtro.Aplicacioncodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_tabla", ConstanteUtil.TipoDato.String, filtro.Codigotabla));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.Compania));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_elemento", ConstanteUtil.TipoDato.String, filtro.Codigoelemento));
            parametros.Add(new ParametroPersistenciaGenerico("p_descripcion", ConstanteUtil.TipoDato.String, filtro.Descripcionlocal));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

            Int32 contador = this.contar("mamiscelaneodetalle.paginacioncontar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "mamiscelaneodetalle.paginacionlistar");

            while (_reader.Read()) {
                MaMiscelaneosdetalle bean = new MaMiscelaneosdetalle();
                if (!_reader.IsDBNull(0))
                    bean.Aplicacioncodigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Codigotabla = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.Compania = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.Codigoelemento = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Descripcionlocal = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Estado = _reader.GetString(5);
                lst.Add(bean);
            }

            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lst;

            return paginacion;
        }

        public string obtenerValorCodigo4(string aplicacioncodigo, string codigotabla, string codigoelemento) {
            IQueryable<MaMiscelaneosdetalle> query = this.getCriteria();
            query = query.Where(p => p.Aplicacioncodigo == aplicacioncodigo);
            query = query.Where(p => p.Codigotabla == codigotabla);
            query = query.Where(p => p.Codigoelemento == codigoelemento);
            List<MaMiscelaneosdetalle> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].ValorCodigo4;
            return null;
        }

        public string obtenerValorCodigo4CuandoPkValorCodigo2(string aplicacioncodigo, string codigotabla, string valorCodigo2) {
            IQueryable<MaMiscelaneosdetalle> query = this.getCriteria();
            query = query.Where(p => p.Aplicacioncodigo == aplicacioncodigo);
            query = query.Where(p => p.Codigotabla == codigotabla);
            query = query.Where(p => p.ValorCodigo2 == valorCodigo2);
            query = query.Where(p => !UString.estaVacio(p.ValorCodigo4));
            List<MaMiscelaneosdetalle> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].ValorCodigo4;
            return null;
        }

        public List<MaMiscelaneosdetalle> listarEnSentencia(FiltroMiscelaneosDetalle filtro) {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<MaMiscelaneosdetalle> lst = new List<MaMiscelaneosdetalle>();

            if (UString.estaVacio(filtro.CodigoAplicacion))
                filtro.CodigoAplicacion = null;

            if (UString.estaVacio(filtro.CodigoTabla))
                filtro.CodigoTabla = null;

            if (UString.estaVacio(filtro.CodigoCompania))
                filtro.CodigoCompania = null;

            if (UString.estaVacio(filtro.CodigoElemento))
                filtro.CodigoElemento = null;

            if (UString.estaVacio(filtro.Nombre))
                filtro.Nombre = null;

            if (UString.estaVacio(filtro.Estado))
                filtro.Estado = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_aplicacion", ConstanteUtil.TipoDato.String, filtro.CodigoAplicacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_tabla", ConstanteUtil.TipoDato.String, filtro.CodigoTabla));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.CodigoCompania));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_elemento", ConstanteUtil.TipoDato.String, filtro.CodigoElemento));
            parametros.Add(new ParametroPersistenciaGenerico("p_descripcion", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

            _reader = base.listarPorQuery("mamiscelaneodetalle.listarEnSentenciaOrdenarPorDescripcion", parametros);
            //_reader = base.listarPorQuery("mamiscelaneodetalle.listarPorSentenciaOrdenarPorCodigo", parametros);

            while (_reader.Read()) {
                MaMiscelaneosdetalle bean = new MaMiscelaneosdetalle();
                if (!_reader.IsDBNull(0))
                    bean.Aplicacioncodigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Codigotabla = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.Compania = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.Codigoelemento = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Descripcionlocal = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Estado = _reader.GetString(5);
                lst.Add(bean);
            }

            this.dispose();

            return lst;
        }

        public List<MaMiscelaneosdetalle> listarDetalle(MaMiscelaneosheaderPk llave) {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<MaMiscelaneosdetalle> lst = new List<MaMiscelaneosdetalle>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_codaplicacion", ConstanteUtil.TipoDato.String, llave.Aplicacioncodigo));
            _parametros.Add(new ParametroPersistenciaGenerico("p_codtabla", ConstanteUtil.TipoDato.String, llave.Codigotabla));
            _parametros.Add(new ParametroPersistenciaGenerico("p_codcompania", ConstanteUtil.TipoDato.String, llave.Compania));



            _reader = this.listarPorQuery("mamiscelaneosdetalle.filtro", _parametros);

            while (_reader.Read()) {
                MaMiscelaneosdetalle bean = new MaMiscelaneosdetalle();

                if (!_reader.IsDBNull(0))
                    bean.Codigoelemento = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Descripcionlocal = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Estado = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.valorNumerico = _reader.GetDouble(3);
                if (!_reader.IsDBNull(4))
                    bean.valorNumerico2 = _reader.GetDouble(4);


                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }



        public void coreEliminar(MaMiscelaneosheaderPk id) {

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();



            _parametros.Add(new ParametroPersistenciaGenerico("p_codaplicacion", ConstanteUtil.TipoDato.String, id.Aplicacioncodigo));
            _parametros.Add(new ParametroPersistenciaGenerico("p_codtabla", ConstanteUtil.TipoDato.String, id.Codigotabla));
            _parametros.Add(new ParametroPersistenciaGenerico("p_codcompania", ConstanteUtil.TipoDato.String, id.Compania));


            this.ejecutarPorQuery("mamiscelaneosdetalle.eliminardetalle", _parametros);


        }

        public string cadena(string codelemento) {
            IQueryable<MaMiscelaneosdetalle> query = this.getCriteria();
            query = query.Where(p => p.Codigoelemento == codelemento);

            List<MaMiscelaneosdetalle> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Codigoelemento;
            return null;
        }

        public string generarCodigo()
        {
            List<MaMiscelaneosdetalle> lst = new List<MaMiscelaneosdetalle>();

            _reader = this.listarPorQuery("mamiscelaneosdetalle.listar");

            while (_reader.Read())
            {
                MaMiscelaneosdetalle bean = new MaMiscelaneosdetalle();

                if (!_reader.IsDBNull(0))
                    bean.Codigoelemento = _reader.GetString(0);

                lst.Add(bean);
            }
            this.dispose();

            if (lst.Count > 1)
                return lst[0].Codigoelemento;
            return null;


        }

        public List<MaMiscelaneosdetalle> listarActivosBean(string aplicacionCodigo, string codigoTabla) {
            IQueryable<MaMiscelaneosdetalle> query = this.getCriteria();

            if (!UString.estaVacio(aplicacionCodigo))
                query = query.Where(p => p.Aplicacioncodigo == aplicacionCodigo);
            if (!UString.estaVacio(codigoTabla))
                query = query.Where(p => p.Codigotabla == codigoTabla);


            List<MaMiscelaneosdetalle> lista = query.ToList();

            //lista.Add(new MaMiscelaneosdetalle() { Descripcionlocal = "--Seleccione--", Codigoelemento = "00" });
            //lista.OrderBy(s => s.Codigoelemento.Trim());

            return lista;
        }
    }
}
