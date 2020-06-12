using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace net.royal.spring.framework.web.dao
{
    public interface GenericoTransaccionDao
    {
        DbTransaction getTransaccion();
        void setTransaccion(DbTransaction transaccion);
        void transaccionFinalizar();
        DbTransaction transaccionIniciar(DbContext context);
        void transaccionCancelar();
    }
}
