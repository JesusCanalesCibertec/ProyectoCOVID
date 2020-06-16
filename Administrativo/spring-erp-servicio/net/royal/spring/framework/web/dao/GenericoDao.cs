using net.royal.spring.framework.core.dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace net.royal.spring.framework.web.dao
{
    public interface GenericoDao<T> where T : class
    {
        DbTransaction transaccionIniciar();
        void transaccionFinalizar();
        void transaccionCancelar();
        List<T> listarConPaginacion(IQueryable<T> criteria, ParametroPaginacionGenerico paginacion);

        IQueryable<T> getCriteria();

        void dispose();

        T registrar(T entity);

        void actualizar(T entity);

        void eliminar(T entity);

        T obtenerPorId(params Object[] keyValues);

        List<T> listarTodos();

        List<T> listarSoloLectura(IQueryable<T> query);

        DbDataReader listarPorQuery(String nombreQuery);

        DbDataReader listarPorQuery(String nombreQuery, List<ParametroPersistenciaGenerico> parametros);

        void ejecutarPorQuery(string nombreQuery, List<ParametroPersistenciaGenerico> parametros);

        void ejecutarPorSentenciaSQL(string sql, List<ParametroPersistenciaGenerico> parametros);
    }
}
