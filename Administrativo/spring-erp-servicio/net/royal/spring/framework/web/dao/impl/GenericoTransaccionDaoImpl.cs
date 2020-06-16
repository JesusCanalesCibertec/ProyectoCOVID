using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace net.royal.spring.framework.web.dao.impl
{
    public class GenericoTransaccionDaoImpl : GenericoTransaccionDao
    {
        protected static DbTransaction _transaccion;

        public DbTransaction getTransaccion() {
            return _transaccion;
        }

        public void setTransaccion(DbTransaction tran)
        {
            _transaccion=tran;
        }

        public void transaccionFinalizar() {
            if (_transaccion == null)
                return;
            _transaccion.Commit();
            _transaccion.Connection.Close();
            //_transaccion.Dispose();
            //_transaccion = null;
        }

        public DbTransaction transaccionIniciar(DbContext _context)
        {
            if (_context.Database.GetDbConnection().State != System.Data.ConnectionState.Open)
                _context.Database.GetDbConnection().Open();
            _transaccion = _context.Database.GetDbConnection().BeginTransaction();                        
            return _transaccion;
        }

        public void transaccionCancelar()
        {
            if (_transaccion == null)
                return;
            _transaccion.Rollback();
            _transaccion.Connection.Close();
            //_transaccion.Dispose();
            //_transaccion = null;
        }
    }
}
