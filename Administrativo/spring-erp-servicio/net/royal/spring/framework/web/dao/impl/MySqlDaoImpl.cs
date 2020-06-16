using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Data.Common;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core;
using System.Xml;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace net.royal.spring.framework.web.dao.impl
{
    public class MySqlDaoImpl<T> : GenericoDao<T> where T : class
    {
        protected MySqlDbContext _context;
        protected DbCommand _command;
        protected DbDataReader _reader;
        protected String _nombreAlias;
        protected XmlNodeList _listaSentencias = null;
        protected IConfigurationRoot _configuration;
        protected DbTransaction _transaccion;

        public MySqlDaoImpl(MySqlDbContext context, String nombreAlias)
        {
            _context = context;
            _nombreAlias = nombreAlias;

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public MySqlDbContext getContext()
        {
            return _context;
        }

        public void actualizar(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void eliminar(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public List<T> listarTodos()
        {
            List<T> s = _context.Set<T>().ToList();
            return s;
        }

        public List<T> listarSoloLectura(IQueryable<T> query)
        {
            List<T> lst = query.ToList();
            lst.ForEach(reg => _context.Entry(reg).State = EntityState.Detached);
            return lst;
        }

        public T obtenerPorId(params Object[] keyValues)
        {
            return _context.Set<T>().Find(keyValues);
        }

        public T registrar(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Int32 contar(string nombreQuery, List<ParametroPersistenciaGenerico> parametros)
        {
            Int32 cantidad = 0;
            _reader = this.listarPorQuery(nombreQuery, parametros);
            while (_reader.Read())
            {
                cantidad = _reader.GetInt32(0);
            }
            this.dispose();
            return cantidad;
        }

        public void ejecutarPorQuery(string nombreQuery, List<ParametroPersistenciaGenerico> parametros)
        {
            String sql = null;
            if (_listaSentencias == null)
                _listaSentencias = GenericoBase.obtenerSentenciaXml(this._nombreAlias, nombreQuery);
            sql = GenericoBase.obtenerSentencia(_listaSentencias, nombreQuery);

            sql = GenericoBase.getNamedQueryByPatametersSet(_configuration, sql, parametros);

            _command = this.crearComando();
            _command.CommandText = sql;
            _command.CommandType = System.Data.CommandType.Text;
            _command.ExecuteNonQuery();
        }

        public void ejecutarPorSentenciaSQL(string sql, List<ParametroPersistenciaGenerico> parametros)
        {
            sql = GenericoBase.getNamedQueryByPatametersSet(_configuration, sql, parametros);

            _command = this.crearComando();
            _command.CommandText = sql;
            _command.CommandType = System.Data.CommandType.Text;
            _command.ExecuteNonQuery();
        }

        public DbDataReader listarPorQuery(string nombreQuery)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            return listarPorQuery(nombreQuery, parametros);
        }
        public DbDataReader listarPorQuery(string nombreQuery, List<ParametroPersistenciaGenerico> parametros)
        {
            String sql = null;
            if (_listaSentencias == null)
                _listaSentencias = GenericoBase.obtenerSentenciaXml(this._nombreAlias, nombreQuery);
            sql = GenericoBase.obtenerSentencia(_listaSentencias, nombreQuery);

            sql = GenericoBase.getNamedQueryByPatametersSet(_configuration, sql, parametros);

            _command = this.crearComando();
            _command.CommandText = sql;
            _command.CommandType = System.Data.CommandType.Text;
            _command.CommandTimeout = 240;
            _reader = _command.ExecuteReader();
            return _reader;
        }

        public DbDataReader listarConPaginacion(ParametroPaginacionGenerico paginacion, List<ParametroPersistenciaGenerico> parametros, string nombreQuery)
        {
            String sql = null;
            if (_listaSentencias == null)
                _listaSentencias = GenericoBase.obtenerSentenciaXml(this._nombreAlias, nombreQuery);
            sql = "SELECT * FROM (";
            sql = sql + GenericoBase.obtenerSentencia(_listaSentencias, nombreQuery);

            sql = GenericoBase.getNamedQueryByPatametersSet(_configuration, sql, parametros);

            if (paginacion.CantidadRegistrosDevolver == null)
                paginacion.CantidadRegistrosDevolver = 10;

            if (paginacion.RegistroInicio == null)
            {
                paginacion.RegistroInicio = 1;
            }
            else
            {
                paginacion.RegistroInicio++;
            }

            sql = sql + ") t WHERE Seq BETWEEN " + paginacion.RegistroInicio.ToString();

            sql = sql + " AND " + (paginacion.CantidadRegistrosDevolver + paginacion.RegistroInicio - 1).ToString()+" order by Seq";

            _command = this.crearComando();
            _command.CommandText = sql;
            _command.CommandType = System.Data.CommandType.Text;
            _command.CommandTimeout = 240;
            _reader = _command.ExecuteReader();

            return _reader;
        }

        public DbDataReader listarPorSentenciaSQL(string sentenciaSQL, List<ParametroPersistenciaGenerico> parametros)
        {
            sentenciaSQL = GenericoBase.getNamedQueryByPatametersSet(_configuration, sentenciaSQL, parametros);
            _command = this.crearComando();
            _command.CommandText = sentenciaSQL;
            _command.CommandType = System.Data.CommandType.Text;
            _command.CommandTimeout = 240;
            _reader = _command.ExecuteReader();
            return _reader;
        }

        public List<T> listarConPaginacion(IQueryable<T> criteria, ParametroPaginacionGenerico paginacion)
        {
            if (UInteger.esCeroOrNulo(paginacion.RegistroInicio))
                paginacion.RegistroInicio = 0;
            if (UInteger.esCeroOrNulo(paginacion.CantidadRegistrosDevolver))
                paginacion.CantidadRegistrosDevolver = 10;

            /*if (!UString.estaVacio(paginacion.AtributoOrdenar))
            {
                if (paginacion.DireccionOrdenAtributo == ConstanteDatos.SORT_ORDER.ASC)
                    criteria.OrderBy(p => paginacion.AtributoOrdenar);
                else
                    criteria.OrderByDescending(p => paginacion.AtributoOrdenar);
            }*/

            return criteria.Skip(paginacion.RegistroInicio.Value).Take(paginacion.CantidadRegistrosDevolver.Value).ToList();
        }

        /***/
        public IQueryable<T> getCriteria()
        {
            if (_transaccion != null)
            {
                if (_context.Database.CurrentTransaction == null)
                    _context.Database.UseTransaction(_transaccion);
            }
            return _context.Set<T>().AsQueryable();
        }
        private DbCommand crearComando()
        {
            DbCommand comandito = _context.Database.GetDbConnection().CreateCommand();
            if (_transaccion != null)
            {
                if (comandito.Transaction == null)
                    comandito.Transaction = _transaccion;
            }
            if (comandito.Connection.State != System.Data.ConnectionState.Open)
                comandito.Connection.Open();
            return comandito;
        }

        public DbTransaction transaccionIniciar()
        {
            /*if (_context.Database.GetDbConnection().State != System.Data.ConnectionState.Open)
                _context.Database.GetDbConnection().Open();
            _transaccion = _context.Database.GetDbConnection().BeginTransaction();*/
            return _transaccion;
        }
        public void transaccionFinalizar()
        {
            /*if (_transaccion == null)
                 return;
             _transaccion.Commit();
             _transaccion.Dispose();
             _transaccion = null;*/
        }
        public void transaccionCancelar()
        {
            /*if (_transaccion == null)
                return;
            _transaccion.Rollback();
            _transaccion.Dispose();
            _transaccion = null*/
        }
        public void dispose()
        {


            if (_reader != null)
            {
                _reader.Close();
                _reader.Dispose();
            }
            if (_command != null)
            {
                _command.Dispose();
                if (_command.Connection != null)
                {
                    // DARIO SE COMENTA ESTA LINEA PARA PROBAR DEBE CONJUGAR CON LAS TRANSACCIONES ABIERTAS
                    // _command.Connection.Close();
                }
            }
        }
        /******/
    }
}
